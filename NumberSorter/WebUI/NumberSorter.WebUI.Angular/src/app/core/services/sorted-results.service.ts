import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SortedResultsCreateDto, SortedResultsDetailsDto, } from '../models/sorted-results.models';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class SortedResultsService {
  constructor(private http: HttpClient) {}

  private readonly baseUrl = `https://localhost:7272/sorted-results`;

  public getAll(): Observable<SortedResultsDetailsDto[]> {
    return this.http.get<SortedResultsDetailsDto[]>(this.baseUrl);
  }

  public getById(id: number): Observable<SortedResultsDetailsDto> {
    return this.http.get<SortedResultsDetailsDto>(`${this.baseUrl}/${id}`);
  }

  public create(dto: SortedResultsCreateDto): Observable<SortedResultsDetailsDto> {
    return this.http.post<SortedResultsDetailsDto>(this.baseUrl, dto);
  }

  public delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
