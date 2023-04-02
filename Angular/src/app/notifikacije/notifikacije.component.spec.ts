import { ComponentFixture, TestBed } from '@angular/core/testing';

import { NotifikacijeComponent } from './notifikacije.component';

describe('NotifikacijeComponent', () => {
  let component: NotifikacijeComponent;
  let fixture: ComponentFixture<NotifikacijeComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ NotifikacijeComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(NotifikacijeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
