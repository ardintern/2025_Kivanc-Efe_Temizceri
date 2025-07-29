import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Logging } from './logging';

describe('Logging', () => {
  let component: Logging;
  let fixture: ComponentFixture<Logging>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [Logging]
    })
    .compileComponents();

    fixture = TestBed.createComponent(Logging);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
