import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProfilecardComponent } from './profilecard.component';

describe('ProfilecardComponent', () => {
  let component: ProfilecardComponent;
  let fixture: ComponentFixture<ProfilecardComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ProfilecardComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ProfilecardComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
