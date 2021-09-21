import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators'

import { Hero } from './hero';
import { LogMessageService } from './logmessage.service';
import { BaseService } from './base.service';
import { LogMessage } from './logmessage';

@Injectable({
  providedIn: 'root'
})
export class HeroService extends BaseService {

  // URL to web API
  private heroesUrl = `${this.baseUrl}Heroes`;
  
  constructor(
    private http: HttpClient, 
    private messageService: LogMessageService
  ) {
    super();
  }

  getHeroes(): Observable<Hero[]> {
    return this.http.get<Hero[]>(this.heroesUrl, this.httpOptions)
      .pipe(
        tap(_ => this.log('got heroes')),
        catchError(this.handleError<Hero[]>('getHeroes', [])),
      );
  }

  getHero(id: number): Observable<Hero> {
    const url = `${this.heroesUrl}/${id}`;
    return this.http.get<Hero>(url, this.httpOptions)
      .pipe(
        tap(_ => this.log(`got hero id=${id}`)),
        catchError(this.handleError<Hero>('getHero')),
      );
  }

  updateHero(hero: Hero): Observable<any> {
    return this.http.put(this.heroesUrl, hero, this.httpOptions)
      .pipe(
        tap(_ => this.log(`updated hero id=${hero.id}`)),
        catchError(this.handleError<any>('updateHero')),
      );
  }

  addHero(hero: Hero): Observable<Hero> {
    return this.http.post<Hero>(this.heroesUrl, hero, this.httpOptions)
      .pipe(
        tap((newHero: Hero) => this.log(`added hero with id=${newHero.id}`)),
        catchError(this.handleError<Hero>('addHero')),
      );
  }

  deleteHero(id: number): Observable<Hero> {
    const url = `${this.heroesUrl}/${id}`;
    return this.http.delete<Hero>(url, this.httpOptions)
      .pipe(
        tap(_ => this.log(`deleted hero id=${id}`)),
        catchError(this.handleError<Hero>('deleteHero')),
      );
  }

  // Logs a HeroService message with the MessageService
  protected log(message: string) {
    let messageObj: LogMessage = {
      contents: `HeroService: ${message}`,
      id: 0,
      createdBy: 'chris',
      createdDate: new Date()
    };

    this.messageService.addMessage(messageObj).subscribe();
  }

  protected handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      // TODO: send error to remote logger
      console.error(error);

      // TODO: better transform error message for end user
      this.log(`${operation} failed: ${error.message}`);

      // Return empty result to keep app running
      return of(result as T);
    };
  }
}
