import { Component } from '@angular/core';
import { FormGroup, FormControl, Validators, FormArray, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { DataService } from '../../Services/data.service';
import { Skills } from '../../models/skills';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-masteradd',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,RouterModule,CommonModule],
  templateUrl: './masteradd.component.html',
  styleUrl: './masteradd.component.css'
})
export class MasteraddComponent {
  fileToUpload: any;
  isNewDiv:boolean=false;
  url = '/assets/no_image.png';
  skillList: Skills[] = [];
  candidatePicture: File = null!;
  
  constructor(
    public dataSvc: DataService,
    private router: Router,
    
  ) { }
  
  candidateForm: FormGroup = new FormGroup({
    candidateId: new FormControl(undefined, Validators.required),
    candidateName: new FormControl(undefined, Validators.required),
    dateOfBirth: new FormControl(undefined),
    mobileNo: new FormControl(undefined, Validators.required),
    isFresher: new FormControl(undefined, Validators.required),
    skillsList: new FormArray([])
  });
  
  get SkillListArray() {
    return this.candidateForm.controls["skillsList"] as FormArray;
  }
  
  addSkills() {
    this.SkillListArray.push(new FormGroup({
      skillId: new FormControl(undefined, Validators.required)
    }));
  }
  
  removeSkillList(index: number) {
    if (this.SkillListArray.controls.length > 0)
      this.SkillListArray.removeAt(index);
  }
  
  ngOnInit() {
    this.dataSvc.getSkillList().subscribe((result) => {
      this.skillList = result;
    });
    this.addSkills();
  }
  
  onFileSelected(event: any) {
    this.fileToUpload = event?.target?.files[0];
    let fileType = event.target.files[0].type;
    if (fileType.match(/image\/*/)) {
      let reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]);
      this.candidatePicture = event.target.files[0];
      reader.onload = (event: any) => {
        this.url = event.target.result;
      };      
    } 
    else { window.alert('Please select correct image format'); }
    this.candidatePicture = event.target.files[0];
  }
  
  InsertCandidate() {
  
    var formData = new FormData();
  
    formData.append("skillStringify", JSON.stringify(this.candidateForm.get("skillsList")?.value));
    formData.append("candidateName", this.candidateForm.get("candidateName")?.value);
    formData.append("dateOfBirth", this.candidateForm.get("dateOfBirth")?.value);
    formData.append("mobileNo", this.candidateForm.get("mobileNo")?.value);
    formData.append("isFresher", this.candidateForm.get("isFresher")?.value);
    formData.append("pictureFile", this.candidatePicture, this.candidatePicture.name);
  
    this.dataSvc.postCandidateSkill(formData).subscribe(
      {
        next: r => {
          console.log(r);
          this.router.navigate(['/details']);
          //this.notifySvc.message('Data insered successfull!!!', 'DISMISS');
        },
        error: err => {
          console.log(err);
        }
      }
    );
  
  }
}
