import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import ReconnectingWebSocket from 'reconnecting-websocket';
import { BaseDto, ServerBroadcastsUserDto, ServerEchoClientDto } from '../BaseDto';

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

  rws: ReconnectingWebSocket = new ReconnectingWebSocket("ws://localhost:8181");

  messageContent = new FormControl('');

  username = new FormControl('');

  constructor()
  {
    this.rws.onmessage = message => {
      const messageFromServer = JSON.parse(message.data) as BaseDto<any>;
      console.log("Recieved: " + JSON.stringify(messageFromServer));
      //@ts-ignore
      this[messageFromServer.eventType].call(this, messageFromServer);
    }
  }

  handleEvent(){
    
  }

  ServerBroadcastsUser(dto: ServerBroadcastsUserDto){
    this.messages.push(dto.message!);
  }

  ServerEchoClient(dto: ServerEchoClientDto) {
    this.messages.push(dto.echoValue!);
  }

  sendBroadcastedMessage(){
    var object = {
      eventType: "ClientWantsToBroadcastUser",
      username: this.username.value!
    }
    this.rws.send(JSON.stringify(object));
  }

  sendEchoMessage(){
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
