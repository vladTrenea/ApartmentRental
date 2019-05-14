import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewApartmentComponent } from './view-apartment.component';

describe('ViewApartmentComponent', () => {
  let component: ViewApartmentComponent;
  let fixture: ComponentFixture<ViewApartmentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ ViewApartmentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(ViewApartmentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
