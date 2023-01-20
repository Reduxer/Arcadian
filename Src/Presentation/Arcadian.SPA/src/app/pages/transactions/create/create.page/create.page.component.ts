import { Component } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-create.page',
  templateUrl: './create.page.component.html',
  styleUrls: ['./create.page.component.css']
})
export class CreatePageComponent {

  constructor(private router: Router) { }

  onFormCompleted(): void {
    this.router.navigate(['/']);
  }
}
