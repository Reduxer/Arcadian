import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { MatTable } from '@angular/material/table';
import { Router } from '@angular/router';
import { Transaction } from '../../models/transaction.model';
import { AbstractTransactionService, TransactionFilter } from '../../services/transaction.service';
import { ConfirmDialogComponent } from '../shared/confirm-dialog/confirm-dialog.component';
import { ErrorDialogComponent } from '../shared/error-dialog/error-dialog.component';
import { OperationSuccessfulDialogComponent } from '../shared/operation-successful-dialog/operation-successful-dialog.component';

@Component({
  selector: 'app-transaction-list',
  templateUrl: './transaction-list.component.html',
  styleUrls: ['./transaction-list.component.css']
})
export class TransactionListComponent implements OnInit {

  @ViewChild(MatTable) table!: MatTable<any>;

  displayedColumns: Array<string> = ['transactionId', 'transactionName', 'cost', 'date', 'actions'];
  totalItems: number = 0;
  itemsPerPage: number = 10;
  pageIndex: number = 0;

  transactions: Array<Transaction> = [];

  constructor(private transactionService: AbstractTransactionService, private dialog: MatDialog, private router: Router) { }

  ngOnInit(): void {
    this.getTransactions();
  }

  private getTransactions(): void {
    this.transactionService.getTransactions({
      pageSize: this.itemsPerPage,
      pageIndex: 0
    })
      .subscribe((response) => {
        this.totalItems = response.total;
        this.transactions = response.data;
        this.table.renderRows;
      });
  }

  onPageChange(event: PageEvent): void {

    this.pageIndex = event.pageIndex;
    this.itemsPerPage = event.pageSize;

    const filter: TransactionFilter = {
      pageIndex: event.pageIndex,
      pageSize: event.pageSize
    }
    this.transactionService.getTransactions(filter)
      .subscribe((response) => {
        this.totalItems = response.total;
        this.transactions = response.data;
        this.table.renderRows();
      });
  }

  onDeleteClicked(id: string): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      hasBackdrop: false
    });

    dialogRef.afterClosed()
      .subscribe((shouldProceed) => {
        if (shouldProceed) {
          this.transactionService.deleteTransaction(id)
            .subscribe((deleted) => {
              if (deleted > 0) {

                const dialogRef = this.dialog.open(OperationSuccessfulDialogComponent, {
                  hasBackdrop: false
                });

                this.getTransactions();

                dialogRef.afterOpened()
                  .subscribe(() => {
                    setTimeout(() => {
                      dialogRef.close();
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
            });
        }
      });
  }

  onViewClicked(id: string): void {
    this.router.navigate(['/transaction/details', id]);
  }
}
