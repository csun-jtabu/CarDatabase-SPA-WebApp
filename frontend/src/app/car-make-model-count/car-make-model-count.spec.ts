import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CarMakeModelCount } from './car-make-model-count';

describe('CarMakeModelCount', () => {
  let component: CarMakeModelCount;
  let fixture: ComponentFixture<CarMakeModelCount>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CarMakeModelCount]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CarMakeModelCount);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
