import { Component, OnInit } from '@angular/core';
import { ChatService } from 'src/app/services/chat.service';
import { BotService } from 'src/app/services/bot.service';
import { MessageDto } from 'src/app/Dto/MessageDto';
import { User } from '../models';
import { AccountService } from '../services/account.service';
import * as moment from 'moment';

@Component({
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {
  user?: User | null;

  constructor(
    private chatService: ChatService,
    private accountService: AccountService,
    private botService: BotService,) {}

  ngOnInit(): void {
    this.chatService.retrieveMappedObject().subscribe( (receivedObj: MessageDto) => { this.addToInbox(receivedObj);});
                                                     
  }

  msgDto: MessageDto = new MessageDto();
  msgInboxArray: MessageDto[] = [];

  send(): void {
    let user = localStorage.getItem('userName');
    if(this.msgDto && user) {
      this.msgDto.user = user;
      if(this.msgDto.msgText.length == 0){
        return;
      } 
      if(this.msgDto.msgText.startsWith('/stock='))
      {        
        this.chatService.broadcastMessage(this.msgDto);
        this.botService.getStockPrice(this.msgDto.msgText.replace('/stock=',''));        
        this.msgDto.msgText = '';
      } else {
        this.chatService.broadcastMessage(this.msgDto);
        this.msgDto.msgText = '';
      }
    }
  }

  logout() {
      this.accountService.logout();
  }

  addToInbox(obj: MessageDto) {
    let newObj = new MessageDto();
    newObj.user = obj.user;
    newObj.msgText = obj.msgText;
    newObj.time = moment(Date.now());
    this.msgInboxArray.push(newObj);
    this.msgInboxArray.sort((a,b) => b.time.toDate().getTime() - a.time.toDate().getTime());
    if (this.msgInboxArray.length > 50) {
      this.msgInboxArray.pop();      
    }
  }
}

