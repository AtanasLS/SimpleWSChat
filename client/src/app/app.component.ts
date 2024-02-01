import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import ReconnectingWebSocket from 'reconnecting-websocket';
import { BaseDto, ServerEchoClientDto } from '../BaseDto';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet , CommonModule, ReactiveFormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'my-app';
  messages: string[] = [];
  sentMessages: string[] = [];
  receivedMessages: string[] = [];
  ws: WebSocket = new WebSocket("ws://localhost:8181");

  rws: ReconnectingWebSocket = new ReconnectingWebSocket("ws://localhost:8181");

  messageContent = new FormControl('');

  constructor()
  {
    this.rws.onmessage = message => {
        const messageFromServer = JSON.parse(message.data) as BaseDto<any>;
        // @ts-ignore
        this[messageFromServer.eventType].call(this, messageFromServer);
      }
  }

  ServerEchoClient(dto: ServerEchoClientDto) {
    this.messages.push(dto.echoValue!);
  }

  sendMessage(){
    /*
    this.rws.send(this.messageContent.value!);
    this.sentMessages.push(this.messageContent.value!);
    this.messageContent.setValue('');
    */

    var object = {
      eventType: "ClientWantsToEchoServer",
      messageContent: this.messageContent.value!
    }
    this.rws.send(JSON.stringify(object));
  }

  poke()
  {
    this.rws.send("poke");
  }




}
