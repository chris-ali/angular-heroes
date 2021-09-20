import { Injectable } from '@angular/core';
import { InMemoryDbService } from 'angular-in-memory-web-api';

import { Hero } from './hero';
import { LogMessage } from './logmessage';

@Injectable({
  providedIn: 'root'
})
export class InMemoryDataService {

  constructor() { }

  createDb() {
    const heroes = [
      { id: 11, name: 'Dr Nice' },
      { id: 12, name: 'Narco' },
      { id: 13, name: 'Bombasto' },
      { id: 14, name: 'Celeritas' },
      { id: 15, name: 'Magneta' },
      { id: 16, name: 'RubberMan' },
      { id: 17, name: 'Dynama' },
      { id: 18, name: 'Dr IQ' },
      { id: 19, name: 'Magma' },
      { id: 20, name: 'Tornado' }
    ] as Hero[];

    const messages: LogMessage[] = [
      { id: 10, contents: "test log message", createdBy: "chris", createdDate: new Date() }
    ] as LogMessage[];

    return {heroes, messages};
  }
  
  // Overrides the genId method to ensure that a hero always has an id.
  // If the heroes array is empty,
  // the method below returns the initial number (11).
  // if the heroes array is not empty, the method below returns the highest
  // hero id + 1.
  genHeroId(heroes: Hero[]): number {
    return heroes.length > 0 ? 
      Math.max(...heroes.map(hero => hero.id)) + 1
      : 11;
  }
  
  // Overrides the genId method to ensure that a message always has an id.
  // If the message array is empty,
  // the method below returns the initial number (10).
  // if the message array is not empty, the method below returns the highest
  // message id + 1.
  genMessageId(messages: LogMessage[]): number {
    return messages.length > 0 ? 
      Math.max(...messages.map(message => message.id)) + 1
      : 10;
  }
}
