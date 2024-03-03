import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MastereditComponent } from './masteredit.component';

describe('MastereditComponent', () => {
  let component: MastereditComponent;
  let fixture: ComponentFixture<MastereditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MastereditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(MastereditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
