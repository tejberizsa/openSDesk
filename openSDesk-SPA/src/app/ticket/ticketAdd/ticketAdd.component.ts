import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { AuthService } from 'src/app/_services/auth.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { Ticket } from 'src/app/_models/ticket';
import { TicketService } from 'src/app/_services/ticket.service';
import { Router } from '@angular/router';
import { environment } from 'src/environments/environment';
import { FileUploader } from 'ng2-file-upload';

@Component({
  selector: 'app-ticketadd',
  templateUrl: './ticketAdd.component.html',
  styleUrls: ['./ticketAdd.component.css']
})
export class TicketAddComponent implements OnInit {
  ticketForm: FormGroup;
  ticket: Ticket;
  ticketId: number;
  baseUrl = environment.apiUrl;
  uploader: FileUploader;
  hasBaseDropZoneOver = false;

  constructor(private authService: AuthService, private alertify: AlertifyService,
    private fb: FormBuilder, private ticketService: TicketService, private router: Router) { }

  ngOnInit() {
    this.createTicketForm();
    // this.ticketId = 1;
    // this.initializeUploader();
  }

  fileOverBase(e: any): void {
    this.hasBaseDropZoneOver = e;
  }

  createTicketForm() {
    this.ticketForm = this.fb.group({
      requesterId: [],
      type: [''],
      sourceId: [''],
      category: [''],
      priority: [''],
      summary: [''],
      description: [''],
      location: ['']
    });
  }

  createTicket() {
    if (this.ticketForm.valid) {
      this.ticket = Object.assign({}, this.ticketForm.value);
      this.ticketService.addTicket(this.ticket).subscribe((ticketReturn: Ticket) => {
        this.ticketId = ticketReturn.id;
        this.initializeUploader();
        this.alertify.success('Ticket created');
      }, error => {
        this.alertify.error(error);
      });
    }
  }

  initializeUploader() {
    this.uploader = new FileUploader({
      url: this.baseUrl,
      authToken: 'Bearer ' + localStorage.getItem('token'),
      isHTML5: true,
      allowedFileType: ['image', 'video', 'pdf', 'audio', 'pdf', 'compress', 'doc', 'xls', 'ppt'],
      removeAfterUpload: true,
      autoUpload: false,
      maxFileSize: 10 * 1024 * 1024
    });
  }

}
