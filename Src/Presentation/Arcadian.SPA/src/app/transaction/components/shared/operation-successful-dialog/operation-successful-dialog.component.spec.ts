import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OperationSuccessfulDialogComponent } from './operation-successful-dialog.component';

describe('OperationSuccessfulDialogComponent', () => {
  let component: OperationSuccessfulDialogComponent;
  let fixture: ComponentFixture<OperationSuccessfulDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OperationSuccessfulDialogComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(OperationSuccessfulDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
