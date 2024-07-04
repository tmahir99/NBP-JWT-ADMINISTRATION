import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { DocumentsComponent } from './documents/documents.component';
import { OrdersComponent } from './orders/orders.component';
import { JobComponent } from './job/job.component';
import { ContactComponent } from './contact/contact.component';
import { ManageUsersComponent } from './manage-users/manage-users.component';

const routes: Routes = [
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'home', component: HomeComponent},
  {path: 'documents', component: DocumentsComponent},
  {path: 'orders', component: OrdersComponent},
  {path: 'jobs', component: JobComponent},
  {path: 'contact', component: ContactComponent},
  {path: 'manage-users', component: ManageUsersComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
