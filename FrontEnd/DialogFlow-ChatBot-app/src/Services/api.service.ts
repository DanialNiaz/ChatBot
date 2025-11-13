import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { ChatMessage } from '../app/Models/chat-message.model';
import { BaseService } from './base.service';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private messagesSubject = new BehaviorSubject<ChatMessage[]>([]);
  messages$ = this.messagesSubject.asObservable();

  constructor(private baseService: BaseService) {}

 departureCity:string='';
 arrivalCity:string='';
  
  SendMessages(text: string) {
    const userMessage: ChatMessage = { sender: 'user', text };
    this.addMessage(userMessage);
  
    this.baseService.post<any>('SendMessage', { Text: text })
      .subscribe({
        next: (res) => {
          if (res.statusCode === 200) {
            let botMessageText = res.result?.queryResult?.fulfillmentText || 'No reply available';
  
            const messages = res.result?.queryResult?.fulfillmentMessages;
            if (messages?.length) {
              const firstMessage = messages[0];
              const textArray = firstMessage?.text?.text_;
              if (textArray?.length) {
                botMessageText = textArray.join(' ');
              }
            }
  
            if (res.departureCity) {
              this.departureCity = res.departureCity;
            }
            if (res.arrivalCity) {
              this.arrivalCity = res.arrivalCity;
            }
            const departure = this.departureCity || 'N/A';
            const arrival = this.arrivalCity || 'N/A';
            botMessageText = botMessageText
              .replace('#citiescontext.DepartureCity', departure)
              .replace('#citiescontext.ArivalCity', arrival);
  
            const botMessage: ChatMessage = { sender: 'bot', text: botMessageText };
            this.addMessage(botMessage);
          } else {
            alert(`Error: ${res.messages?.join(', ') || 'Unexpected error occurred.'}`);
          }
        },
        error: (err) => {
          const botMessage: ChatMessage = { sender: 'bot', text: 'Error: Could not reach server.' };
          this.addMessage(botMessage);
          console.error(err);
        }
      });
  }
  

  private addMessage(msg: ChatMessage) {
    const current = this.messagesSubject.getValue();
    this.messagesSubject.next([...current, msg]);
  }

  disconnect() {
    this.messagesSubject.complete();
  }
}
