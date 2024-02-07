export class MessageData {
    username: string;
    message: string;
    roomId: number;

    constructor(username: string, message: string, roomId: number){
        this.username = username;
        this.message = message;
        this.roomId = roomId;
    }
}