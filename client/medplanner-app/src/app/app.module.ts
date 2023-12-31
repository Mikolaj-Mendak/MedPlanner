import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { MatIconModule } from '@angular/material/icon';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MenuComponent } from './components/main-page/menu.component';
import { BrowserAnimationsModule, provideAnimations } from '@angular/platform-browser/animations';
import { HeaderComponent } from './components/header/header.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule } from '@angular/material/dialog';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { PatientRegisterDialogComponent } from './components/patient-register-dialog/patient-register-dialog.component';
import { ToastrModule, provideToastr } from 'ngx-toastr';
import { OwnerRegisterDialogComponent } from './components/owner-register-dialog/owner-register-dialog.component';
import { DoctorRegisterDialogComponent } from './components/doctor-register-dialog/doctor-register-dialog.component';
import { OwnerClinicsComponent } from './components/owner-clinics/owner-clinics.component';
import { RegisterPageComponent } from './components/register-page/register-page.component';
import { AuthInterceptor } from './auth-Interceptor';
import { MatMenuModule } from '@angular/material/menu';
import { AddClinicComponent } from './components/add-clinic/add-clinic.component';
import { TimepickerModule } from 'ngx-bootstrap/timepicker';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { NgxMaterialTimepickerModule } from 'ngx-material-timepicker';
import { ClinicDetailsComponent } from './components/clinic-details/clinic-details.component';
import { AddDoctorToClinicComponent } from './components/add-doctor-to-clinic/add-doctor-to-clinic.component';
import { DoctorAdmissionPageComponent } from './components/doctor-admission-page/doctor-admission-page.component';
import { UserProfilePageComponent } from './components/user-profile-page/user-profile-page.component';
import { DoctorClinicPageComponent } from './components/doctor-clinic-page/doctor-clinic-page.component';
import { DoctorAdmissionPageForDoctorComponent } from './components/doctor-admission-page-for-doctor/doctor-admission-page-for-doctor.component';
import { SetDoctorAdmissionForClinicDialogComponent } from './components/set-doctor-admission-for-clinic-dialog/set-doctor-admission-for-clinic-dialog.component';
import { DoctorInocomingVisitsComponent } from './components/doctor-inocoming-visits/doctor-inocoming-visits.component';
import { DoctorHistoryVisitsComponent } from './components/doctor-history-visits/doctor-history-visits.component';
import { VisitDoctorDetailsComponent } from './components/visit-doctor-details/visit-doctor-details.component';
import { PatientIncomingVisitsComponent } from './components/patient-incoming-visits/patient-incoming-visits.component';
import { PatientVisitsHistoryComponent } from './components/patient-visits-history/patient-visits-history.component';
import { PatientVisitDetailsComponent } from './components/patient-visit-details/patient-visit-details.component';
import { AddVisitTableComponent } from './components/add-visit-table/add-visit-table.component';
import { CreateVisitComponent } from './components/create-visit/create-visit.component';
import { VisitAppointmentDetailsComponent } from './components/visit-appointment-details/visit-appointment-details.component';
import { FullCalendarModule } from '@fullcalendar/angular';


@NgModule({
    declarations: [
        AppComponent,
        MenuComponent,
        HeaderComponent,
        PatientRegisterDialogComponent,
        OwnerRegisterDialogComponent,
        DoctorRegisterDialogComponent,
        OwnerClinicsComponent,
        RegisterPageComponent,
        AddClinicComponent,
        ClinicDetailsComponent,
        AddDoctorToClinicComponent,
        DoctorAdmissionPageComponent,
        UserProfilePageComponent,
        DoctorClinicPageComponent,
        DoctorAdmissionPageForDoctorComponent,
        SetDoctorAdmissionForClinicDialogComponent,
        DoctorInocomingVisitsComponent,
        DoctorHistoryVisitsComponent,
        VisitDoctorDetailsComponent,
        PatientIncomingVisitsComponent,
        PatientVisitsHistoryComponent,
        PatientVisitDetailsComponent,
        AddVisitTableComponent,
        CreateVisitComponent,
        VisitAppointmentDetailsComponent

    ],
    imports: [
        BrowserModule,
        AppRoutingModule,
        MatIconModule,
        BrowserAnimationsModule,
        MatFormFieldModule,
        FormsModule,
        ReactiveFormsModule,
        MatDialogModule,
        HttpClientModule,
        ToastrModule.forRoot(),
        BrowserAnimationsModule,
        MatMenuModule,
        TimepickerModule.forRoot(),
        BsDatepickerModule.forRoot(),
        NgxMaterialTimepickerModule,
        FullCalendarModule
    ],
    providers: [
        provideAnimations(),
        provideToastr(),
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true,
        },
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
