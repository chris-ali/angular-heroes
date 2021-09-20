import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';

import { BaseService } from './base.service';
import { LogMessage } from './logmessage';

@Injectable({
  providedIn: 'root'
})
export class LogMessageService extends BaseService {
  
  // URL to web API
  private messagesUrl = `${this.baseUrl}messages`;

  constructor(private http: HttpClient) {
    super();
  }

  addMessage(message: LogMessage): Observable<LogMessage> {
    return this.http.post<LogMessage>(this.messagesUrl, message, this.httpOptions)
      .pipe(
        tap((newLogMessage: LogMessage) => this.log(`added message: ${newLogMessage.contents}`)),
        catchError(this.handleError<LogMessage>('addMessage'))
      );
  }

  getMessages(): Observable<LogMessage[]> {
    const url = `${this.messagesUrl}`;
    return this.http.get<LogMessage[]>(url, this.httpOptions)
      .pipe(
        tap(_ => this.log('getting messages')),
        catchError(this.handleError<LogMessage[]>('getMessages'))
      );
  }

  clearMessages(): Observable<LogMessage[]> {
    const url = `${this.messagesUrl}`;
    return this.http.delete<LogMessage[]>(url, this.httpOptions)
      .pipe(
        tap(_ => this.log('cleared messages')),
        catchError(this.handleError<LogMessage[]>('getMessages'))
      );
  }

  protected log(message: string): void {
    console.log(`LogMessageService: ${message}`);
  }

  protected handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send error to remote logger
      console.error(error);

      // Return empty result to keep app running
      return of(result as T);
    };
  }
}
