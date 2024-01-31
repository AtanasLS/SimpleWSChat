import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import ReconnectingWebSocket from 'reconnecting-websocket';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet , CommonModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'my-app';
  sentMessages: string[] = [];
  receivedMessages: string[] = [];
  ws: WebSocket = new WebSocket("ws://localhost:8181");

  rws: ReconnectingWebSocket = new ReconnectingWebSocket("ws://localhost:8181");

  messageContent = new FormControl('');

  constructor()
  {
    this.rws.onmessage = message => {
      this.receivedMessages.push(message.data);
    }
    

  }

  sendMessage(){
    this.rws.send(this.messageContent.value!);
    this.sentMessages.push(this.messageContent.value!);
    this.messageContent.setValue('');

  }

  poke()
  {
    this.rws.send("poke");
  }




}
