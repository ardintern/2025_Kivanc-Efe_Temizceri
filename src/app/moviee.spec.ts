import { TestBed } from '@angular/core/testing';

import { Moviee } from './moviee';

describe('Moviee', () => {
  let service: Moviee;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(Moviee);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
