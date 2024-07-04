import { Component, OnInit } from '@angular/core';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';
import { JobService } from './job.service';
import { AuthService } from '../auth.service';
import { PermissionService } from '../permission.service';

@Component({
  selector: 'app-job',
  templateUrl: './job.component.html',
  styleUrls: ['./job.component.css']
})
export class JobComponent implements OnInit {
  jobs: any[] = [];
  filteredJobs: any[] = [];
  newJob: any = {};
  selectedFile: File | null = null;
  userNames: string[] = [];
  currentUser: string | null = '';
  roles: string[] | null = [];

  departments = ['Engineering', 'Production', 'Administrators', 'Procurement'];

  // Initialize filter object with default values
  filter: any = {
    department: this.departments[0], // Assuming departments[0] exists and is valid
    asignedBy: '',
    asignedTo: '',
    dateFrom: '',
    dateTo: ''
  };

  displayJobCreation: boolean = false;
  displayFilter: boolean = false;
  canCreateJob: boolean = true;
  constructor(
    private jobService: JobService,
    private authService: AuthService,
    private snackBar: MatSnackBar,
    public prermisionService: PermissionService,
  ) { }

  ngOnInit(): void {
    this.currentUser = this.authService.getUserUsername()?.toString() || 'No User';
    this.newJob.asignedBy = this.currentUser;
    this.loadJobs();
    this.loadUserNames();
    this.roles = this.authService.getUserRoles();
    console.log(this.roles);

    // Call the filterJobs method to apply initial filtering based on roles
    this.filterJobs();
  }

  loadJobs() {
    this.jobService.getJobs().subscribe(data => {
      this.jobs = data;
      this.filterJobs(); // Update filtered jobs whenever jobs are loaded
    });
  }

  loadUserNames() {
    this.authService.getAllUserNames().subscribe(data => {
      this.userNames = data;
    });
  }

  createJob() {
    const formData = new FormData();
    formData.append('name', this.newJob.name);
    formData.append('description', this.newJob.description);
    formData.append('department', this.newJob.department);
    formData.append('asignedBy', this.newJob.asignedBy);
    formData.append('asignedTo', this.newJob.asignedTo);
    formData.append('asignedOn', this.newJob.asignedOn);
    if (this.selectedFile) {
      formData.append('image', this.selectedFile);
    }

    this.jobService.createJob(formData).subscribe(() => {
      this.newJob = {};
      this.selectedFile = null;
      this.loadJobs();
      this.showSnackBar('Job created successfully', 'Success');
    });
  }

  onFileSelected(event: any) {
    this.selectedFile = event.target.files[0];
  }

  updateJob(job: any) {
    const formData = new FormData();
    formData.append('name', job.name);
    formData.append('description', job.description);
    formData.append('department', job.department);
    formData.append('asignedBy', job.asignedBy);
    formData.append('asignedTo', job.asignedTo);
    formData.append('asignedOn', job.asignedOn);
    if (this.selectedFile) {
      formData.append('image', this.selectedFile);
    }

    this.jobService.updateJob(job.id, formData).subscribe(() => {
      this.selectedFile = null;
      this.loadJobs();
      this.showSnackBar('Job updated successfully', 'Success');
    });
  }

  deleteJob(id: number) {
    this.jobService.deleteJob(id).subscribe(() => {
      this.loadJobs();
      this.showSnackBar('Job deleted successfully', 'Success');
    });
  }

  markJobAsDone(id: number) {
    this.jobService.markJobAsDone(id, this.currentUser? this.currentUser : "No User").subscribe(() => {
      this.loadJobs();
      this.showSnackBar('Job marked as done', 'Success');
    });
  }

  filterJobs() {
    if(!this.roles)
      {
        return;
      }
    if (this.roles.includes("ADMIN") || this.roles.includes("SUPERADMIN") || this.roles.includes("PRODUCTION_WORKER")) {
      this.filteredJobs = [...this.jobs];

    } else if (this.roles.includes("CARRIER")) {
      this.filteredJobs = this.jobs.filter(job => job.isDone);
      this.canCreateJob = false;
    } else {
      this.showSnackBar('You do not have permission to view jobs. Please contact your admin.', 'Info');
      this.filteredJobs = [];
      this.canCreateJob = false;
    }
  }

  showSnackBar(message: string, action: string) {
    const config = new MatSnackBarConfig();
    config.duration = 3000;
    config.horizontalPosition = 'right';
    config.verticalPosition = 'top';
    config.panelClass = ['custom-snackbar'];

    this.snackBar.open(message, action, config);
  }

  canCreateJobJobs(): boolean { return this.prermisionService.isAdmin() || this.prermisionService.isSuperAdmin() || this.prermisionService.isOwner()
   }
}
