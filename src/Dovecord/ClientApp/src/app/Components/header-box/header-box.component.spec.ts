import { ComponentFixture, TestBed } from '@angular/core/testing';

import { HeaderBoxComponent } from './header-box.component';

describe('HeaderBoxComponent', () => {
  let component: HeaderBoxComponent;
  let fixture: ComponentFixture<HeaderBoxComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ HeaderBoxComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(HeaderBoxComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
