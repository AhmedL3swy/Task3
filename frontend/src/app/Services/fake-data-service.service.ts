import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
@Injectable({
  providedIn: 'root',
})
export class ApiService {
  constructor(private httpClient: HttpClient) {}

  getData(apiUrl: string, params: HttpParams) {
    console.log(`${apiUrl}?${params.toString()}`);
    return this.httpClient.get(apiUrl, { params: params });
  }
  deleteEntity(apiUrl: string, id: string) {
    console.log(`Delete Singal Sent to ` + `${apiUrl}/${id}`);
    return this.httpClient.delete(`${apiUrl}/${id}`);
  }
  // Search as Post
  searchEntity(apiUrl: string, search: any) {
    console.log(`Search Singal Sent to ` + `${apiUrl}`);
    return this.httpClient.post(apiUrl, search);
  }
  addEntity(apiUrl: string, entity: any) {
    console.log(`Add Singal Sent to ` + `${apiUrl}`);
    return this.httpClient.post(apiUrl, entity);
  }
  updateEntity(apiUrl: string, entity: any) {
    console.log(`Update Singal Sent to ` + `${apiUrl}`);
    return this.httpClient.put(apiUrl, entity);
  }
  getEntityById(apiUrl: string, id: string) {
    console.log(`Get Singal Sent to ` + `${apiUrl}/${id}`);
    return this.httpClient.get(`${apiUrl}/${id}`);
  }
}
