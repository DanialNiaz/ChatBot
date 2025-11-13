import { Component, OnInit, OnDestroy } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ChatMessage } from '../app/Models/chat-message.model';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css'],
  imports: [CommonModule, FormsModule]
})
export class AppComponent implements OnInit, OnDestroy {
  messages: ChatMessage[] = [];
  messageText = '';
  private socket!: WebSocket;

  departureCity: string = '';
  arrivalCity: string = '';

  constructor() {}

  ngOnInit(): void {
    // Connect to WebSocket server
    this.socket = new WebSocket('wss://localhost:44361/ws/chat');

    // Handle incoming messages from the WebSocket server (backend)
    this.socket.onmessage = (event: MessageEvent) => {
      const response = JSON.parse(event.data);

      // If StatusCode is 200, show fulfillmentText;
      if (response.StatusCode === 200) {
        this.departureCity = response.departureCity || 'N/A';
        this.arrivalCity = response.arrivalCity || 'N/A';

        // Add bot's response message
        this.addMessage({ sender: 'bot', text: response.Result.QueryResult.FulfillmentText });
      } else {

        this.addMessage({ sender: 'bot', text: 'Error: Something went wrong with the response.' });
      }
    };
  }

  // Send a message to the WebSocket server
  sendMessage(): void {
    if (this.messageText.trim()) {
      const userMessage: ChatMessage = { sender: 'user', text: this.messageText };

      // Add the user message to the chat
      this.addMessage(userMessage);

      // Send the user message to the WebSocket server
      this.socket.send(this.messageText);

      // Clear the input field after sending the message
      this.messageText = '';
    }
  }

  // Close the WebSocket connection when the component is destroyed
  ngOnDestroy(): void {
    if (this.socket) {
      this.socket.close();
    }
  }

  // Helper function to add a message to the chat
  private addMessage(msg: ChatMessage): void {
    this.messages.push(msg);
  }
}
