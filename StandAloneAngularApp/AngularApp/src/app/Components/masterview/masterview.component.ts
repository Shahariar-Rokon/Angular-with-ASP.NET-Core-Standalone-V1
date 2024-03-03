import { Component } from '@angular/core';
import { CandidateSkills } from '../../models/candidate-skills';
import { Skills } from '../../models/skills';
import { DataService } from '../../Services/data.service';
import { Router, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-masterview',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CommonModule,RouterModule,CommonModule],
  templateUrl: './masterview.component.html',
  styleUrl: './masterview.component.css'
})
export class MasterviewComponent {
  skillList: Skills[] = [];
  candidateList: CandidateSkills[] = [];
  baseUrl='http://localhost:5203/';
  imageUrl:string=this.baseUrl+'Images/';
  constructor(private dataSvc:DataService,private router:Router){}
  ngOnInit(): void {
    this.dataSvc.getSkillList().subscribe(x => {
      this.skillList = x;
    });
    this.dataSvc.getCandidateSkill().subscribe(x => {
      this.candidateList = x;
    });
  }
 
  getSkillName(id: any) {
    let data = this.skillList.find(x => x.skillId == id);
    return data ? data.skillName : '';
  }
  OnDelete(item: CandidateSkills):void {
    if (item.candidateId){
      this.dataSvc.deleteCandidateSkill(item.candidateId).subscribe({
        next: (response) => {
          let currentUrl = this.router.url;
          this.router
            .navigateByUrl('/', { skipLocationChange: true })
            .then(() => {
              this.router.navigate([currentUrl]);
            });
        }
      })
    }
    ;
  }
}
