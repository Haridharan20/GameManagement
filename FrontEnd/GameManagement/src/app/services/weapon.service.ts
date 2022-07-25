import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class WeaponService {
  constructor(private http: HttpClient) {}

  createWeapon(name: string, price: number): Observable<any> {
    return this.http.post('https://localhost:7148/api/Weapon/add', {
      name,
      price,
    });
  }

  getWeapons(): Observable<any> {
    return this.http.get('https://localhost:7148/api/Weapon/weapons');
  }

  getWeapon(id: number): Observable<any> {
    return this.http.get(`https://localhost:7148/api/Weapon/weapon/${id}`);
  }

  buyWeapon(id: number, price: number): Observable<any> {
    return this.http.get(
      `https://localhost:7148/api/User/buyweapon/${id}?price=${price}`
    );
  }

  editWeapon(name: string, price: string, id: number) {
    return this.http.put(`https://localhost:7148/api/Weapon/edit/${id}`, {
      name,
      price,
    });
  }

  deleteWeapon(id: number) {
    return this.http.delete(`https://localhost:7148/api/Weapon/delete/${id}`);
  }
}
