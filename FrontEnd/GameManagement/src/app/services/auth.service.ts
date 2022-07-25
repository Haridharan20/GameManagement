import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  userName = new BehaviorSubject<string>('');
  isLoading = new BehaviorSubject<boolean>(false);
  constructor(private http: HttpClient, private route: Router) {}

  Login(username: string, password: string): Observable<any> {
    console.log(username, password);
    return this.http.post('https://localhost:7148/api/auth/login', {
      email: username,
      password: password,
    });
  }

  Register(name: string, email: string, password: string): Observable<any> {
    return this.http.post('https://localhost:7148/api/auth/register', {
      name,
      email,
      password,
    });
  }

  changeAdmin(id: number, isAdmin: number) {
    return this.http.put(`https://localhost:7148/api/User/edit/${id}`, isAdmin);
  }
  getUsers(): Observable<any> {
    return this.http.get('https://localhost:7148/api/User/getUsers');
  }

  loggedIn() {
    // console.log('log');
    if (
      !localStorage.getItem('token') ||
      localStorage.getItem('token') == 'Not Found'
    ) {
      return false;
    } else {
      var data = this.decode();
      this.userName.next(data['Email']);
      return true;
    }
  }

  logOut() {
    localStorage.clear();
    this.route.navigate(['']);
  }

  isAdmin() {
    if (!localStorage.getItem('token')) {
      return false;
    } else {
      var data = this.decode();
      var claim =
        'http://schemas.microsoft.com/ws/2008/06/identity/claims/role';

      // console.log(data[claim]);
      if (data[claim] == '1') {
        return true;
      } else {
        return false;
      }
    }
  }

  setToken(token: string, id: any) {
    if (token != undefined && id != 'Not Found') {
      localStorage.setItem('token', token);
      localStorage.setItem('uid', id);
    }
  }
  getToken() {
    return localStorage.getItem('token');
  }

  decode() {
    const role = localStorage.getItem('token') || '';
    let jwtData = role.split('.')[1];
    let decode = atob(jwtData);
    let decodeData = JSON.parse(decode);
    return decodeData;
  }
}
