import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { NgDompurifySanitizer } from '@tinkoff/ng-dompurify';
import { TuiRootModule, TuiDialogModule, TuiButtonModule, TuiAlertModule, TUI_SANITIZER } from '@taiga-ui/core';
import { TuiInputModule } from '@taiga-ui/kit';
import { MatDialogModule } from '@angular/material/dialog'; // Import MatDialogModule

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { AuthService } from './auth.service';
import { AppRoutingModule } from './app-routing.module';
import { MakeRoleComponent } from './make-role/make-role.component';
import { TuiTableModule } from '@taiga-ui/addon-table';
import { TuiTagModule } from '@taiga-ui/kit';
import { DocumentsComponent } from './documents/documents.component';

import { HttpClientModule } from '@angular/common/http';
import { TableModule } from 'primeng/table';
import { ButtonModule } from 'primeng/button';
import { DialogModule } from 'primeng/dialog';
import { InputTextModule } from 'primeng/inputtext';
import { OrdersComponent } from './orders/orders.component';
import { JobComponent } from './job/job.component';

import { MatSnackBarModule } from '@angular/material/snack-bar';

import { HttpErrorInterceptor } from './http-error.interceptor';
import { FooterComponent } from './footer/footer.component';
import { ContactComponent } from './contact/contact.component';
import { ManageUsersComponent } from './manage-users/manage-users.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    HomeComponent,
    HeaderComponent,
    MakeRoleComponent,
    DocumentsComponent,
    OrdersComponent,
    JobComponent,
    FooterComponent,
    ContactComponent,
    ManageUsersComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    ReactiveFormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    TuiRootModule,
    TuiDialogModule,
    TuiAlertModule,
    TuiInputModule,
    TuiTableModule,
    TuiTagModule,
    TuiButtonModule,
    MatDialogModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    TableModule,
    ButtonModule,
    DialogModule,
    InputTextModule,
    FormsModule,
    MatSnackBarModule,
  ],
  providers: [
    AuthService,
    { provide: TUI_SANITIZER, useClass: NgDompurifySanitizer },
    { provide: HTTP_INTERCEPTORS, useClass: HttpErrorInterceptor, multi: true },
    provideHttpClient(withInterceptorsFromDi()),
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
