import { Component, OnInit } from '@angular/core';
import { LogMessage } from '../logmessage';
import { LogMessageService } from '../logmessage.service';

@Component({
  selector: 'app-messages',
  templateUrl: './messages.component.html',
  styleUrls: ['./messages.component.css']
})
export class MessagesComponent implements OnInit {

  createdBy: string = 'chris';
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
