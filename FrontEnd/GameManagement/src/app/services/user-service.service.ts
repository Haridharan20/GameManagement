import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class UserServiceService {
  constructor(private http: HttpClient) {}

  GetMyWeapons(id: number): Observable<any> {
    return this.http.get(`https://localhost:7148/api/User/myWeapons/${id}`);
  }
}
