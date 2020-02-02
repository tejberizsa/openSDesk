import { Component, OnInit } from '@angular/core';
import { Ticket } from 'src/app/_models/ticket';
import { TicketService } from 'src/app/_services/ticket.service';
import { AlertifyService } from 'src/app/_services/alertify.service';
import { ActivatedRoute } from '@angular/router';
import { Note } from 'src/app/_models/note';
import { AuthService } from 'src/app/_services/auth.service';

@Component({
  selector: 'app-ticketdetail',
  templateUrl: './ticketDetail.component.html',
  styleUrls: ['./ticketDetail.component.css']
})
export class TicketDetailComponent implements OnInit {
  ticket: Ticket;
  newNote: any = {};

  constructor(private ticketService: TicketService, private alertify: AlertifyService,
    private route: ActivatedRoute, private authService: AuthService) { }

  ngOnInit() {
    this.route.data.subscribe(data => {
      this.ticket = data['ticket'];
    });
    console.log(this.ticket);
  }

  sendNote() {
    this.newNote.ownerId = this.authService.decodedToken.nameid;
    this.ticketService.addNote(this.ticket.id, this.newNote).subscribe((note: Note) => {
      this.ticket.notes.push(note);
      this.newNote = {};
    }, error => {
      this.alertify.error(error);
    });
  }

}
