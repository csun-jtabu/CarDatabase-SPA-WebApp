import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddMake } from './add-make';

describe('AddMake', () => {
  let component: AddMake;
  let fixture: ComponentFixture<AddMake>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddMake]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddMake);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
