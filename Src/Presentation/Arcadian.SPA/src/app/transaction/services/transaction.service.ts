import { Injectable, Inject, InjectionToken } from '@angular/core';
import { Transaction } from '../models/transaction.model';
import { HttpClient } from '@angular/common/http';
import { API_URL } from 'src/app/shared/services/environment.service';
import { Observable } from 'rxjs';

export type GetTransactionResponse = {
  total: number;
  data: Transaction[];
}

export type TransactionFilter = {
  searchString?: string;
  pageIndex: number;
  pageSize: number 
}

export abstract class AbstractTransactionService {
  abstract getTransactions(filter?: TransactionFilter): Observable<GetTransactionResponse>;
  abstract createTransaction(transaction: Transaction): Observable<number>;
  abstract deleteTransaction(id: string): Observable<number>;
  abstract getTransaction(id: string): Observable<GetTransactionResponse>;
}

@Injectable()
export class TransactionService implements AbstractTransactionService {

  constructor(private httpClient: HttpClient, @Inject(API_URL) private apiUrl: string) { }
  createTransaction(transaction: Transaction): Observable<number> {
    return this.httpClient.post<number>(`${this.apiUrl}/api/transactions`, transaction);
  }

  getTransactions(filter?: TransactionFilter): Observable<GetTransactionResponse> {
    return this.httpClient.get<GetTransactionResponse>(`${this.apiUrl}/api/transactions`, {
      params: { ...filter }
    });
  }

  getTransaction(id: string): Observable<GetTransactionResponse> {
    return this.httpClient.get<GetTransactionResponse>(`${this.apiUrl}/api/transactions/${id}`);
  }

  deleteTransaction(id: string): Observable<number> {
    return this.httpClient.delete<number>(`${this.apiUrl}/api/transactions`, {
      body: { id }
    });
  }
}


