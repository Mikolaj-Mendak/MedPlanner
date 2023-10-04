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
import { HttpClientModule } from '@angular/common/http';
import { PatientRegisterDialogComponent } from './components/patient-register-dialog/patient-register-dialog.component';
import { ToastrModule, provideToastr } from 'ngx-toastr';
import { OwnerRegisterDialogComponent } from './components/owner-register-dialog/owner-register-dialog.component';
import { DoctorRegisterDialogComponent } from './components/doctor-register-dialog/doctor-register-dialog.component';


@NgModule({
    declarations: [
        AppComponent,
        MenuComponent,
        HeaderComponent,
        PatientRegisterDialogComponent,
        OwnerRegisterDialogComponent,
        DoctorRegisterDialogComponent
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
        BrowserAnimationsModule
    ],
    providers: [
        provideAnimations(),
        provideToastr()
    ],
    bootstrap: [AppComponent]
})
export class AppModule { }
