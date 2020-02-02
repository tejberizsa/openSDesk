import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Ticket } from '../_models/ticket';
import { Note } from '../_models/note';
import { Resolution } from '../_models/resolution';
import { Survey } from '../_models/survey';
import { Observable } from 'rxjs';
import { PaginatedResult } from '../_models/pagination';
import { map } from 'rxjs/operators';
import { User } from '../_models/user';
import { Category } from '../_models/category';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  addTicket(ticket: Ticket) {
    return this.http.post(this.baseUrl + 'Ticket/AddTicket', ticket);
  }

  addNote(ticketId: number, note: Note) {
    return this.http.post(`${this.baseUrl}Ticket/${ticketId}/AddNote`, note);
  }

  assignToGroup(ticketId: number, groupId: number) {
    return this.http.post(`${this.baseUrl}Ticket/${ticketId}/AssignToGroup/${groupId}`, {});
  }

  assignToUser(ticketId: number, userId: number) {
    return this.http.post(this.baseUrl + 'Ticket/' + ticketId + '/AssignToUser/' + userId, {});
  }

  getUserList(): Observable<User[]> {
    return this.http.get<User[]>(this.baseUrl + 'Ticket/GetUserList');
  }

  getCategoryList(): Observable<Category[]> {
    return this.http.get<Category[]>(this.baseUrl + 'Settings/GetCategoryList');
  }

  updateStatus(ticketId: number, ticket) {
    return this.http.post(this.baseUrl + 'Ticket/' + ticketId + '/UpdateStatus', ticket);
  }

  resolveTicket(ticketId: number, resolution: Resolution) {
    return this.http.post(this.baseUrl + 'Ticket/' + ticketId + '/ResolveTicket', resolution);
  }

  surveyTicket(ticketId: number, survey: Survey) {
    return this.http.post(this.baseUrl + 'Ticket/' + ticketId + '/SurveyTicket', survey);
  }

  getTicket(id: number): Observable<Ticket> {
    return this.http.get<Ticket>(this.baseUrl + 'Ticket/' + id);
  }

  getTicketThread(container: string, userId?, categoryId?, statusId?, userGrouId?) {
    const paginatedResult: PaginatedResult<Ticket[]> = new PaginatedResult<Ticket[]>();
    let params = new HttpParams();
    params = params.append('TicketContainer', container);
    if (userId != null) {
      params = params.append('UserId', userId);
    }
    if (categoryId != null) {
      params = params.append('CategoryId', categoryId);
    }
    if (statusId != null) {
      params = params.append('StatusId', statusId);
    }
    if (userGrouId != null) {
      params = params.append('UserGrouId', userGrouId);
    }

    return this.http.get<Ticket[]>(this.baseUrl + 'Ticket/GetTicketThread', {observe: 'response', params})
      .pipe(
        map(response => {
          paginatedResult.result = response.body;
          if (response.headers.get('Pagination') !== null) {
            paginatedResult.pagination = JSON.parse(response.headers.get('Pagination'));
          }
          return paginatedResult;
        })
      );
  }
}
