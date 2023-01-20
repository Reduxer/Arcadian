import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { TransactionModule } from './transaction/transaction.module';
import { AppComponent } from './app.component';
import { environmentInjectables } from './shared/services/environment.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { LayoutModule } from '@angular/cdk/layout';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatButtonModule } from '@angular/material/button';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { HomePageComponent } from './pages/transactions/home/home.component';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { RouterModule, Routes } from '@angular/router';
import { CreatePageComponent } from './pages/transactions/create/create.page/create.page.component';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatRadioModule } from '@angular/material/radio';
import { ReactiveFormsModule } from '@angular/forms';
import { DetailsPageComponent } from './pages/transactions/details/details.page.component';

const routes: Routes = [
  { path: '', component: HomePageComponent, pathMatch: 'full' },
  { path: 'transaction/create', component: CreatePageComponent },
  { path: 'transaction/details/:id', component: DetailsPageComponent }
]

@NgModule({
  declarations: [
    AppComponent,
    HomePageComponent,
    CreatePageComponent,
    DetailsPageComponent,
  ],
  imports: [
    BrowserModule,
    TransactionModule,
    BrowserAnimationsModule,
    LayoutModule,
    MatToolbarModule,
    MatButtonModule,
    MatSidenavModule,
    MatIconModule,
    MatListModule,
    MatGridListModule,
    MatCardModule,
    MatMenuModule,
    RouterModule.forRoot(routes),
    MatInputModule,
    MatSelectModule,
    MatRadioModule,
    ReactiveFormsModule
  ],
  providers: [
    environmentInjectables
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
