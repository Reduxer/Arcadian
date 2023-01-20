import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Transaction } from 'src/app/transaction/models/transaction.model';
import { AbstractTransactionService } from 'src/app/transaction/services/transaction.service';

@Component({
  selector: 'app-details.page',
  templateUrl: './details.page.component.html',
  styleUrls: ['./details.page.component.css']
})
export class DetailsPageComponent implements OnInit {
  
  transaction?: Transaction;
  
  constructor(private aroute: ActivatedRoute, private transactionService: AbstractTransactionService){ }

  private getTransaction(id: string){
    this.transactionService.getTransaction(id)
      .subscribe(response => {
        if(response.total > 0){
          this.transaction = response.data[0];
        }
      });
  }

  ngOnInit(): void {
    this.aroute.paramMap.subscribe((pm) => {
      const id = pm.get('id');

      if(id){
        this.getTransaction(pm.get('id')!);
      }      
    });
  }
}
