import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DeleteMake } from './delete-make';

describe('DeleteMake', () => {
  let component: DeleteMake;
  let fixture: ComponentFixture<DeleteMake>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DeleteMake]
    })
    .compileComponents();

    fixture = TestBed.createComponent(DeleteMake);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
