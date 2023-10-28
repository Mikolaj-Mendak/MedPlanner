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
        DoctorAdmissionPageComponent
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
        NgxMaterialTimepickerModule
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
