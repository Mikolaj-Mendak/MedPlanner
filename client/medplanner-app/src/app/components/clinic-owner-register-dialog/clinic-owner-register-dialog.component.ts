import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { RegisterDto } from 'src/app/DTOs/register-dto';
import { AuthService } from 'src/app/services/auth-service';

@Component({
    selector: 'app-clinic-owner-register-dialog',
    templateUrl: './clinic-owner-register-dialog.component.html',
    styleUrls: ['./clinic-owner-register-dialog.component.scss']
})
export class ClinicOwnerRegisterDialogComponent {

    registerForm: FormGroup;


    constructor(
        private formBuilder: FormBuilder,
        private dialogRef: MatDialogRef<ClinicOwnerRegisterDialogComponent>,
        private authService: AuthService,
        private toastr: ToastrService
    ) {
        this.setPatientRegisterForm();
    }

    onSubmit() {
        if (this.registerForm.valid) {
            const registerData: RegisterDto = {
                firstName: this.registerForm.get('firstName').value,
                lastName: this.registerForm.get('lastName').value,
                email: this.registerForm.get('email').value,
                password: this.registerForm.get('password').value,
                pesel: this.registerForm.get('pesel').value,
            };

            this.authService.register(registerData).subscribe(
                (user) => {
                    this.toastr.success('Założono konto. Teraz się zaloguj.', 'Sukces');
                    this.dialogRef.close();
                },
                (error) => {
                    this.toastr.error('Błąd rejestracji, spróbuj ponownie', 'Błąd rejestracji');
                    this.dialogRef.close();
                }
            );
        }
    }

    setPatientRegisterForm(): void {
        this.registerForm = this.formBuilder.group({
            firstName: ['', Validators.required],
            lastName: ['', Validators.required],
            email: ['', [Validators.required, Validators.email]],
            password: ['', [Validators.required, Validators.minLength(6), Validators.maxLength(20), Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).{6,20}$/)]],
            passwordVerify: ['', Validators.required],
            pesel: ['', [Validators.required, Validators.minLength(11), Validators.maxLength(11)]]
        }, {
            validators: [this.passwordsMatch]
        });
    }

    setClinicRegisterForm(): void {

    }

    passwordsMatch(formGroup: FormGroup): { [key: string]: boolean } | null {
        const password = formGroup.get('password');
        const passwordVerify = formGroup.get('passwordVerify');

        if (password.value !== passwordVerify.value) {
            return { passwordsNotMatch: true };
        }
        return null;
    }

}
