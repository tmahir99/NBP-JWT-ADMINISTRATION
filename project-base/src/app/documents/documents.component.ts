import { Component, OnInit } from '@angular/core';
import { Document, DocumentService } from './document.service'
import { AuthService } from '../auth.service';
import { PermissionService } from '../permission.service';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
})
export class DocumentsComponent implements OnInit {
  documents: Document[] = [];
  displayDialog: boolean = false;
  document: Document = {} as Document;
  selectedDocument: Document | null = null;
  isNewDocument: boolean = false;

  displayDocumentCreation = false;

  constructor(private documentService: DocumentService, public auth: AuthService, private prermisionService: PermissionService) { }

  ngOnInit() {
    this.loadDocuments();
  }

  loadDocuments() {
    this.documentService.getDocuments().subscribe((data) => {
      this.documents = data;
    });
  }

  showAddDialog() {
    this.isNewDocument = true;
    this.document = {} as Document;
    this.displayDialog = true;
  }

  showEditDialog(document: Document) {
    this.isNewDocument = false;
    this.document = { ...document };
    this.displayDialog = true;
  }

  saveDocument() {
    if (this.isNewDocument) {
      this.documentService.addDocument(this.document).subscribe(() => {
        this.loadDocuments();
        this.displayDialog = false;
      });
    } else {
      this.documentService.updateDocument(this.document).subscribe(() => {
        this.loadDocuments();
        this.displayDialog = false;
      });
    }
  }

  deleteDocument(document: Document) {
    this.documentService.deleteDocument(document.id).subscribe(() => {
      this.loadDocuments();
    });
  }

  onRowSelect(event: any) {
    this.showEditDialog(event.data);
  }

  canCreateDocuments(): boolean{
    return this.prermisionService.isAdmin() || this.prermisionService.isSuperAdmin()
   }
}
