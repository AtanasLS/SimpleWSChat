import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormControl, ReactiveFormsModule } from '@angular/forms';
import { RouterOutlet } from '@angular/router';
import ReconnectingWebSocket from 'reconnecting-websocket';
import { BaseDto, ServerAddsClientToRoomDto, ServerAddsUserToClientDto, ServerBroadcastsMessageWithUsernameDto, ServerBroadcastsUserDto, ServerEchoClientDto } from '../BaseDto';
import { MessageData } from '../MessageData';

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
  usernames: string[] = [];
  sentMessages: string[] = [];
  receivedMessages: string[] = [];

  messageData: MessageData[] = [];

  rws: ReconnectingWebSocket = new ReconnectingWebSocket("ws://localhost:8181");

  messageContent = new FormControl('');

  username = new FormControl('');

  roomId = new FormControl('');

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
    this.messageData.push(JSON.parse(dto.message!));
  }

  ServerEchoClient(dto: ServerEchoClientDto) {
    this.messages.push(dto.echoValue!);
  }
  
  ServerBroadcastsMessageWithUsername(dto: ServerBroadcastsMessageWithUsernameDto){
    const messageData = new MessageData(dto.message!, dto.username!, 0)
    this.messageData.push(messageData);
   // this.messageData.push(JSON.parse(dto.username!));
  }
  ServerAddsClientToRoom(dto: ServerAddsClientToRoomDto){
    const messageData = new MessageData(dto.message!, "", 0);
    this.messageData.push(messageData);
  }

  ServerAddsUserToClient(dto: ServerAddsUserToClientDto){
    const messageData = new MessageData(dto.message!, dto.username!, 0);
    this.messageData.push(messageData);
  }

  sendBroadcastedMessage(){
    var object = {
      eventType: "ClientWantsToBroadcastUser",
      username: this.username.value!
    }
    this.rws.send(JSON.stringify(object));
  }

  sendMessageToRoom()
  {
    var object = {
      eventType: "ClientWantsToBroadcastToRoom",
      message: this.messageContent.value!,
      roomId: +this.roomId.value!
    }

    this.rws.send(JSON.stringify(object));
  }

  enterRoom(){
    var object = {
      eventType: "ClientWantsToEnterRoom",
      roomId: +this.roomId.value!
    }
    this.rws.send(JSON.stringify(object));
    console.log("test");
  }

  userSignUp(){
    var object = {
      eventType: "ClientWantsToSignIn",
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
