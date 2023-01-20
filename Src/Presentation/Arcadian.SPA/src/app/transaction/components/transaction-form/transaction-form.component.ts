import { Component, Input, Output, EventEmitter } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  Validators,
  ValidatorFn,
  ValidationErrors,
  AbstractControl,
  FormControl
} from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Transaction } from '../../models/transaction.model';
import { AbstractTransactionService } from '../../services/transaction.service';
import { ErrorDialogComponent } from '../shared/error-dialog/error-dialog.component';
import { OperationSuccessfulDialogComponent } from '../shared/operation-successful-dialog/operation-successful-dialog.component';

type FormMode = 'Create' | 'Update';

@Component({
  selector: 'app-transaction-form',
  templateUrl: './transaction-form.component.html',
  styleUrls: ['./transaction-form.component.css']
})
export class TransactionFormComponent {

  @Input()
  transaction: Transaction;

  @Output()
  formCompleted: EventEmitter<Transaction>;

  formGroup: FormGroup;

  isFormSubmitting: boolean = false;

  get tNameInput(): FormControl {
    return this.formGroup.get('transactionName') as FormControl;
  }

  get tCostInput(): FormControl {
    return this.formGroup.get('cost') as FormControl;
  }

  get tDateInput(): FormControl {
    return this.formGroup.get('date') as FormControl;
  }

  get formMode(): FormMode {
    return this.transaction.transactionId == null ? 'Create' : 'Update';
  }

  constructor(formBuilder: FormBuilder, private transactionService: AbstractTransactionService, private dialog: MatDialog) {

    this.transaction = new Transaction('', '', 0);
    this.formCompleted = new EventEmitter<Transaction>();

    this.formGroup = formBuilder.group({
      transactionName: formBuilder.nonNullable.control(this.transaction.transactionName, [Validators.required]),
      cost: formBuilder.nonNullable.control(this.transaction.cost, [Validators.required, CustomValidators.numericOnly]),
      date: formBuilder.nonNullable.control(this.transaction.date, [Validators.required]),
    });

    this.tNameInput.valueChanges.subscribe((value: string) => {
      this.transaction.transactionName = value;
    });

    this.tCostInput.valueChanges.subscribe((value: number) => {
      this.transaction.cost = value;
    });

    this.tDateInput.valueChanges.subscribe((value: string) => {
      this.transaction.date = value;
    });
  }

  onFormSubmitted(): void {
    this.isFormSubmitting = true;
    this.transactionService.createTransaction(this.transaction)
      .subscribe((inserted) => {
        if (inserted > 0) {
          const dialogRef = this.dialog.open(OperationSuccessfulDialogComponent, {
            hasBackdrop: false
          });

          dialogRef.afterOpened()
            .subscribe(() => {
              setTimeout(() => {
                dialogRef.close();
                this.formCompleted.emit(this.transaction);
              }, 1000);
            });
        } else {
          const dialogRef = this.dialog.open(ErrorDialogComponent, {
            hasBackdrop: false
          });

          dialogRef.afterOpened()
            .subscribe(() => {
              setTimeout(() => dialogRef.close(), 1000);
            });
        }

        this.isFormSubmitting = false;
      });
  }

  onResetClicked(): boolean {
    this.formGroup.reset();
    return false;
  }
}

export class CustomValidators {

  static get nonZeroVal(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const { value } = control;
      return !isNaN(value) && value == 0 ? { nonZeroVal: { value } } : null;
    }
  }

  static get numericOnly(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const numberRegex = new RegExp(/^\s*[+-]?(\d+|\d*\.\d+|\d+\.\d*)([Ee][+-]?\d+)?\s*$/);
      const { value } = control;
      return value && !numberRegex.test(value) ? { numericOnly: { value } } : null;
    }
  }
}
