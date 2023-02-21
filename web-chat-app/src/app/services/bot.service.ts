import { Injectable, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MessageDto } from '../Dto/MessageDto';
import { environment } from '../environments/environment';
import { ChatService } from './chat.service';

@Injectable({
  providedIn: 'root'
})
export class BotService {

   readonly GET_URL = environment.botUrl;

  constructor(private http: HttpClient,
    private chatService: ChatService) { }


  public getStockPrice(stockName: string) {
    var msgDto = new MessageDto();
    msgDto.user = 'bot';
    debugger;
    this.http.get<string>(this.GET_URL  + "/Bot?stockCode=" + stockName).subscribe();
  }


}