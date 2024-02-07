export class BaseDto<T>{
    eventType: string;

    constructor(init?: Partial<T>){
        this.eventType = this.constructor.name;
        Object.assign(this, init);
    }

}


export class ServerEchoClientDto extends BaseDto<ServerEchoClientDto>{
    echoValue?: string;
}

export class ServerBroadcastsUserDto extends BaseDto<ServerBroadcastsUserDto>{
    message? : string;
}

export class ServerAddsClientToRoomDto extends BaseDto<ServerAddsClientToRoomDto>{
    message? : string;
}


export class ServerBroadcastsMessageWithUsernameDto extends BaseDto<ServerBroadcastsMessageWithUsernameDto>{
    message? : string;
    username? : string;
}

export class ServerAddsUserToClientDto extends BaseDto<ServerAddsUserToClientDto>
{
    message?: string;
    username?: string;
}
