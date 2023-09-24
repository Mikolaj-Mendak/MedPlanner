import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
    selector: 'app-patient-register-dialog',
    templateUrl: './patient-register-dialog.component.html',
    styleUrls: ['./patient-register-dialog.component.scss']
})
export class PatientRegisterDialogComponent {
    loginForm: FormGroup;

    constructor(
        private formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<PatientRegisterDialogComponent>
    ) {
        this.loginForm = this.formBuilder.group({
            username: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    onSubmit() {
        if (this.loginForm.valid) {
            this.dialogRef.close();
        }
    }

}
