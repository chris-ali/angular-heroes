import { Component, OnInit } from '@angular/core';
import { LogMessage } from '../core/models/logmessage';
import { LogMessageService } from '../core/services/logmessage.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  messages: LogMessage[] = [];

  constructor(public messageService:LogMessageService) { }

  ngOnInit(): void {
    this.getMessages();
  }

  getMessages(): void  {
    this.messageService.getMessages()
      .subscribe(messages => this.messages = messages);
  }

  clearMessages(): void {
    this.messageService.clearMessages()
      .subscribe(_ => this.messages = []);
  }
}
