import { Routes } from '@angular/router';
import { MasterviewComponent } from './Components/masterview/masterview.component';
import { MasteraddComponent } from './Components/masteradd/masteradd.component';
import { MastereditComponent } from './Components/masteredit/masteredit.component';

export const routes: Routes = [
{path:'',component:MasterviewComponent},
{path:'details',component:MasterviewComponent},
{path:'details/add',component:MasteraddComponent},
{path:'details/edit/:id',component:MastereditComponent}
];
