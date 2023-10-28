import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OwnerClinicsComponent } from './components/owner-clinics/owner-clinics.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { ClinicDetailsComponent } from './components/clinic-details/clinic-details.component';
import { DoctorAdmissionPageComponent } from './components/doctor-admission-page/doctor-admission-page.component';

const routes: Routes = [
    { path: 'ownerClinics', component: OwnerClinicsComponent },
    { path: '', component: RegisterPageComponent },
    { path: 'ownerClinics/:id', component: ClinicDetailsComponent },
    { path: 'doctorAdmission/:clinicId/:doctorId', component: DoctorAdmissionPageComponent },
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
