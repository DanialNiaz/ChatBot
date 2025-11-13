import { Injectable, OnDestroy } from '@angular/core';
import { BehaviorSubject, Subject, timer } from 'rxjs';
import { webSocket, WebSocketSubject } from 'rxjs/webSocket';
import { ChatMessage } from '../app/Models/chat-message.model';

@Injectable({
  providedIn: 'root'
})
export class SocketService implements OnDestroy {
  private socket$!: WebSocketSubject<string>;
  private reconnectInterval = 5000;
  private isConnected = false;

  private messagesSubject = new BehaviorSubject<ChatMessage[]>([]);
  messages$ = this.messagesSubject.asObservable();

  constructor() {
    this.connect();
  }

  private connect() {
    const wsUrl = 'wss://localhost:44361/ws/chat'; 

    this.socket$ = webSocket({
      url: wsUrl,
      deserializer: e => e.data,
      serializer: value => value,
      openObserver: {
        next: () => {
          this.isConnected = true;
        }
      },
      closeObserver: {
        next: () => {
          this.isConnected = false;
          timer(this.reconnectInterval).subscribe(() => this.connect());
        }
      }
    });

    this.socket$.subscribe({
      next: (data: any) => {
        const msg: ChatMessage = { sender: 'bot', text: data };
        this.addMessage(msg);
      },
      error: (err) => console.error('WebSocket Error:', err),
    });
  }

  sendMessage(text: string) {
    const userMsg: ChatMessage = { sender: 'user', text };
    this.addMessage(userMsg);

    if (this.socket$ && this.isConnected) {
      this.socket$.next(text);
    } else {
      console.warn('WebSocket not connected yet.');
    }
  }

  private addMessage(msg: ChatMessage) {
    const current = this.messagesSubject.getValue();
    this.messagesSubject.next([...current, msg]);
  }

  ngOnDestroy(): void {
    this.socket$?.complete();
  }
}
