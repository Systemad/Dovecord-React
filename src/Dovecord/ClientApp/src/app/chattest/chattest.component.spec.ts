import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ChattestComponent } from './chattest.component';

describe('ChattestComponent', () => {
  let component: ChattestComponent;
  let fixture: ComponentFixture<ChattestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ChattestComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ChattestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
