import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PatientRegisterDialogComponent } from './components/patient-register-dialog/patient-register-dialog.component';
import { ToastrService } from 'ngx-toastr';
import { OwnerRegisterDialogComponent } from './components/owner-register-dialog/owner-register-dialog.component';

@Component({
    selector: 'app-root',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.scss']
})
export class AppComponent {

    title = 'medplanner-app';

    constructor(private dialog: MatDialog, private toastr: ToastrService) {

    }

    openPatientRegister(): void {
        const dialogRef = this.dialog.open(PatientRegisterDialogComponent, {
            width: '800px',
            height: '500px'
        });

        dialogRef.afterClosed().subscribe(result => {
        });
    }


    openOwnerRegister(): void {
        const dialogRef = this.dialog.open(OwnerRegisterDialogComponent, {
            width: '800px',
            height: '500px'
        });

        dialogRef.afterClosed().subscribe(result => {
        });
    }

}
