import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { OwnerClinicsComponent } from './components/owner-clinics/owner-clinics.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { ClinicDetailsComponent } from './components/clinic-details/clinic-details.component';
import { DoctorAdmissionPageComponent } from './components/doctor-admission-page/doctor-admission-page.component';
import { UserProfilePageComponent } from './components/user-profile-page/user-profile-page.component';
import { DoctorClinicPageComponent } from './components/doctor-clinic-page/doctor-clinic-page.component';
import { DoctorAdmissionPageForDoctorComponent } from './components/doctor-admission-page-for-doctor/doctor-admission-page-for-doctor.component';
import { ClinicOwnerGuard } from './guards/clinic-owner-avtivate-guard';
import { DoctorGuard } from './guards/doctor-activate-guard';
import { DoctorOrClinicOwnerGuard } from './guards/doctor-clinic-owner-guard';
import { DoctorInocomingVisitsComponent } from './components/doctor-inocoming-visits/doctor-inocoming-visits.component';
import { DoctorHistoryVisitsComponent } from './components/doctor-history-visits/doctor-history-visits.component';
import { VisitDoctorDetailsComponent } from './components/visit-doctor-details/visit-doctor-details.component';

const routes: Routes = [
    { path: 'ownerClinics', component: OwnerClinicsComponent, canActivate: [ClinicOwnerGuard] },
    { path: '', component: RegisterPageComponent },
    { path: 'ownerClinics/:id', component: ClinicDetailsComponent, canActivate: [ClinicOwnerGuard] },
    { path: 'doctorAdmission/:clinicId/:doctorId', component: DoctorAdmissionPageComponent, canActivate: [DoctorOrClinicOwnerGuard] },
    { path: 'myProfile', component: UserProfilePageComponent },
    { path: 'doctor/clinics', component: DoctorClinicPageComponent, canActivate: [DoctorGuard] },
    { path: 'doctor/clinics/admission/:clinicId', component: DoctorAdmissionPageForDoctorComponent, canActivate: [DoctorGuard] },
    { path: 'doctor/incomingVisits', component: DoctorInocomingVisitsComponent, canActivate: [DoctorGuard] },
    { path: 'doctor/historyVisit', component: DoctorHistoryVisitsComponent, canActivate: [DoctorGuard] },
    { path: 'doctor/historyVisit/:id', component: VisitDoctorDetailsComponent, canActivate: [DoctorGuard] },
    { path: 'doctor/incomingVisit/:id', component: VisitDoctorDetailsComponent, canActivate: [DoctorGuard] }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule { }
