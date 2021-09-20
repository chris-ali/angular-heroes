import { TestBed } from '@angular/core/testing';

import { LogMessageService } from './logmessage.service';

describe('MessageService', () => {
  let service: LogMessageService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LogMessageService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
