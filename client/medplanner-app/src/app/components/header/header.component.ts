import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth-service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent {

    loginForm: FormGroup;
    isLogged: boolean = false;
    currentUserEmail: string | null = null;

    constructor(private dialog: MatDialog,
        private formBuilder: FormBuilder,
        private authService: AuthService) {
        this.setLoginForm();
    }

    setLoginForm(): void {
        this.loginForm = this.formBuilder.group({
            email: ['', Validators.required],
            password: ['', Validators.required]
        });
    }

    onLoginSubmit(): void {
        if (this.loginForm.valid) {
            const email = this.loginForm.value.email;
            const password = this.loginForm.value.password;

            this.authService.login(email, password).subscribe(
                (userDto) => {
                    this.isLogged = true;
                    this.currentUserEmail = userDto.email;
                }
            );
        }
    }

    logout(): void {
        this.authService.logout();
        this.isLogged = false;
        this.currentUserEmail = null;
    }

    checkLoginValidator(controlName: string): boolean {
        return this.loginForm.get(controlName).invalid && this.loginForm.get(controlName).touched;
    }
}
