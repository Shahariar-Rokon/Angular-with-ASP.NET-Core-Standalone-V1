import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MasteraddComponent } from './masteradd.component';

describe('MasteraddComponent', () => {
  let component: MasteraddComponent;
  let fixture: ComponentFixture<MasteraddComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MasteraddComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MasteraddComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
