import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { User } from "../models/user.model";
import { Observable } from "rxjs";
import { UpdateUserRequest } from "../models/requests/update-user-request";
import { AddUserRequest } from "../models/requests/add-user-request";

@Injectable({
  providedIn: "root",
})
export class UserService {
  private url: string = "http://localhost:5020";
  private api: string = "api/users";

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<User[]> {
    return this.httpClient.get<User[]>(`${this.url}/${this.api}`);
  }

  getById(userId: string): Observable<User> {
    return this.httpClient.get<User>(`${this.url}/${this.api}/id/${userId}`);
  }

  searchByName(name: string): Observable<User[]> {
    return this.httpClient.get<User[]>(`${this.url}/${this.api}/name/${name}`);
  }

  add(request: AddUserRequest): Observable<User> {
    return this.httpClient.post<User>(`${this.url}/${this.api}`, request);
  }

  update(id: string, request: UpdateUserRequest): Observable<User> {
    return this.httpClient.put<User>(`${this.url}/${this.api}/${id}`, request);
  }

  delete(id: string): Observable<void> {
    return this.httpClient.delete<void>(`${this.url}/${this.api}/${id}`);
  }
}
