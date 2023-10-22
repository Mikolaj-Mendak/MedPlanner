import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { DoctorRegisterDialogComponent } from '../doctor-register-dialog/doctor-register-dialog.component';
import { OwnerRegisterDialogComponent } from '../owner-register-dialog/owner-register-dialog.component';
import { PatientRegisterDialogComponent } from '../patient-register-dialog/patient-register-dialog.component';

@Component({
    selector: 'app-register-page',
    templateUrl: './register-page.component.html',
    styleUrls: ['./register-page.component.scss']
})
export class RegisterPageComponent {

    constructor(private dialog: MatDialog, private toastr: ToastrService) {

    }

    openPatientRegister(): void {
        const dialogRef = this.dialog.open(PatientRegisterDialogComponent, {
            width: '400px',
            height: '600px'
        });

        dialogRef.afterClosed().subscribe(result => {
        });
    }


    openOwnerRegister(): void {
        const dialogRef = this.dialog.open(OwnerRegisterDialogComponent, {
            width: '400px',
            height: '600px'
        });

        dialogRef.afterClosed().subscribe(result => {
        });
    }

    openDoctorRegister(): void {
        const dialogRef = this.dialog.open(DoctorRegisterDialogComponent, {
            width: '400px',
            height: '600px'
        });

        dialogRef.afterClosed().subscribe(result => {
        });
    }
}
