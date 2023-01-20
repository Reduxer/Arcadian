export class Transaction {
    
    transactionId?: string;
    transactionName: string;
    date: string;
    cost: number;

    constructor(transactionName: string, date: string, cost: number) {
        this.transactionName = transactionName;
        this.date = date;
        this.cost = cost;
    }
}