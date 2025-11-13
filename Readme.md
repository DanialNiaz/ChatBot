# Chatbot Application with Angular and .NET

## Overview
This project is a **real-time chatbot application** using Angular for the frontend and .NET 7 Web API with WebSocket for the backend.  
The chatbot integrates **Dialogflow ES** for natural language understanding.

---

## Features
- Real-time chat interface via **WebSocket**
- Dialogflow ES integration using **REST API**
- Handles multiple concurrent sessions
- Clean Angular frontend with dynamic message updates
- Backend structured with services and DTOs

---

## Technology Stack
| Layer           | Technology/Library              |
|-----------------|--------------------------------|
| Frontend        | Angular 16                     |
| Backend         | .NET 7 (C#) Web API           |
| Real-time Comm  | WebSocket                      |
| Chatbot NLU     | Dialogflow ES (REST API)      |
| JSON Handling   | Newtonsoft.Json                |
| Authentication  | Google Service Account         |
| HTTP Requests   | HttpClient                     |

---

## Project Structure

### Backend (.NET)
DialogFlowChatBot/
├─ Services/
│ ├─ DialogFlowService.cs
│ ├─ IDialogFlowService.cs
│ └─ WebSocketService.cs
├─ DTOs/
│ ├─ ChatBotResponse.cs
│ ├─ userMessage.cs
│ └─ BaseResponseModel.cs
├─ Program.cs
├─ appsettings.json
└─ DialogBotJsonKey.json (ignored in Git)


### Frontend (Angular)

chatbot-angular/
├─ src/
│  ├─ app/
│  │  ├─ app.component.ts
│  │  ├─ app.component.html
│  │  ├─ app.component.css
│  │  ├─ app.module.ts
│  │  ├─ services/
│  │  │  └─ WebSocketServiceService.ts
│  │  └─ models/
│  │     └─ chat-message.model.ts
│  └─ assets/
├─ angular.json
├─ package.json
└─ tsconfig.json

## Setup Instructions

### Backend (.NET)
1. Clone the repository
   ```bash
   URL : (https://github.com/DanialNiaz/ChatBot.git)
   Install NuGet packages:

Install NuGet packages:
   Google.Cloud.Dialogflow.V2
   Newtonsoft.Json

**Make sure your service account JSON key is in the backend folder and path is set correctly.**


   Frontend (Angular)

     cd chatbot-angular
     npm install
     ng serve
