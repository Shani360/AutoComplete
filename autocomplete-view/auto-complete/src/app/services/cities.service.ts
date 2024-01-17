import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';
import { City } from '../interfaces/city';

@Injectable({
  providedIn: 'root'
})
export class CitiesService {
  public isSubmittimg : boolean = false;

  constructor(private httpClient : HttpClient) { }

  searchCities(searchQuery: string): Observable<City[]> {
    this.isSubmittimg = true;
    const url = `${environment.apiUrl}/Cities?searchQuery=${searchQuery}`;
    return this.httpClient.get<City[]>(url);
  }
}
