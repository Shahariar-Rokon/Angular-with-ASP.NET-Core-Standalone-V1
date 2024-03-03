import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Candidates } from '../models/candidates';
import { Observable } from 'rxjs';
import { CandidateSkills } from '../models/candidate-skills';
import { Skills } from '../models/skills';

const apiUrl = "http://localhost:5203/api/";
@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http:HttpClient) { }
  getSkillList(): Observable<Skills[]> {
    return this.http.get<Skills[]>(apiUrl + "Candidates/GetSkill");
  }
  
  postCandidateSkill(data: FormData): Observable<CandidateSkills> {
    return this.http.post<CandidateSkills>(apiUrl + "Candidates", data);
  }
  
  updateCandidateSkill(data: FormData): Observable<CandidateSkills> {
    return this.http.post<CandidateSkills>(apiUrl + "Candidates/Update", data);
  }
  
  deleteCandidateSkill(id: number): Observable<CandidateSkills> {
    return this.http.post<CandidateSkills>(apiUrl + "Candidates/Delete/" + id, null);
  }
  
  getCandidates(): Observable<Candidates[]> {
    return this.http.get<Candidates[]>(apiUrl + "Candidates/GetCandidates");
  }
  
  getCandidateSkill(): Observable<CandidateSkills[]> {
    return this.http.get<CandidateSkills[]>(apiUrl + "Candidates");
  }
  
  getCandidateSkillById(id: number): Observable<CandidateSkills> {
    return this.http.get<CandidateSkills>(apiUrl + "Candidates/" + id);
  }
}
