import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { SeriesPickerComponent } from './series-picker.component';

describe('SeriesPickerComponent', () => {
  let component: SeriesPickerComponent;
  let fixture: ComponentFixture<SeriesPickerComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SeriesPickerComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SeriesPickerComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
