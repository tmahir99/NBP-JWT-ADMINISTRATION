import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { PermissionService } from '../permission.service';

@Component({
  selector: 'app-make-role',
  templateUrl: './make-role.component.html',
})
export class MakeRoleComponent implements OnInit {
  userRole: string = this.authService.getUserRoles()?.toString() || '';
  responseMessage: string = '';
  userFullName: string = this.authService.getUserUsername()?.toString() || '';
  users: any[] = [];
  displayedColumns: string[] = ['userName', 'firstName', 'lastName', 'email', 'roles', 'actionsAdd', 'actionsRemove'];
  canViewTable: boolean = false;

  constructor(
    private authService: AuthService,
    private http: HttpClient,
    private router: Router,
    private permissionService: PermissionService
  ) {}

  ngOnInit(): void {
    this.checkPermissions();
    this.getUserRole();
  }

  checkPermissions(): void {
    this.canViewTable = this.permissionService.isAdmin() || this.permissionService.isOwner() || this.permissionService.isSuperAdmin();
  }

  getUserRole() {
    const token = this.authService.getToken(); // Retrieve token from AuthService

    if (token) {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`, // Set bearer token in request headers
      });

      this.http.get<any[]>('https://naprednebaze.azurewebsites.net//api/Auth/GetAllUsers', { headers })
        .subscribe(
          (response) => {
            console.log(response);
            this.users = response;
          },
          (error) => {
            console.error('Error fetching user role:', error);
          }
        );
    } else {
      console.error('Token not found.');
      this.router.navigate(['/login']);
    }
  }

  makeRole(userName: string, role: string) {
    const token = this.authService.getToken();

    if (token) {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`, // Set bearer token in request headers
      });

      const body = { userName: userName };

      this.http.post(`https://naprednebaze.azurewebsites.net//api/Auth/${role}`, body, { headers })
        .subscribe(
          (response: any) => {
            this.responseMessage = response.message;
            this.getUserRole(); // Refresh the user list
          },
          (error) => {
            console.error(`Error assigning role ${role} to user ${userName}:`, error);
          }
        );
    } else {
      console.error('Token not found.');
      this.router.navigate(['/login']);
    }
  }

  removeRole(userName: string, role: string) {
    const token = this.authService.getToken();

    if (token) {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}`, // Set bearer token in request headers
      });

      const body = { userName: userName };

      this.http.post(`https://naprednebaze.azurewebsites.net//api/Auth/${role}`, body, { headers })
        .subscribe(
          (response: any) => {
            this.responseMessage = response.message;
            this.getUserRole(); // Refresh the user list
          },
          (error) => {
            console.error(`Error removing role ${role} to user ${userName}:`, error);
          }
        );
    } else {
      console.error('Token not found.');
      this.router.navigate(['/login']);
    }
  }
}
