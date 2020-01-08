import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class SettingService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  updateCategory(id: number, text: string) {
    return this.http.post(this.baseUrl + 'Settings/UpdateCategory/' + id, text);
  }

  updateStatus(id: number, text: string) {
    return this.http.post(this.baseUrl + 'Settings/UpdateStatus/' + id, text);
  }

  updateSubStatus(id: number, text: string) {
    return this.http.post(this.baseUrl + 'Settings/UpdateSubStatus/' + id, text);
  }

  assignUserToGroup(userId: number, groupId: number) {
    return this.http.post(this.baseUrl + 'Settings/AssignUserToGroup/' + userId + '/' + groupId, {});
  }

  removeUserFromGroup(userId: number, groupId: number) {
    return this.http.post(this.baseUrl + 'Settings/RemoveUserFromGroup/' + userId + '/' + groupId, {});
  }

  addUserGroup(groupName: string) {
    return this.http.post(this.baseUrl + 'Settings/AddUserGroup', groupName);
  }

  removeUserGroup(groupId: number) {
    return this.http.delete(this.baseUrl + 'Settings/RemoveUserGroup/' + groupId);
  }
}
