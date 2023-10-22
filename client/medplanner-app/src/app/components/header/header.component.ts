import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth-service';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

    loginForm: FormGroup;
    isLogged: boolean;
    currentUser: any; // Zmieniamy typ na 'any', aby pomieścić cały obiekt currentUser

    constructor(private dialog: MatDialog,
        private formBuilder: FormBuilder,
        private authService: AuthService) {
        this.setLoginForm();
    }

    ngOnInit(): void {
        const currentUser = localStorage.getItem('currentUser');
        if (currentUser) {
            this.currentUser = JSON.parse(currentUser);
            this.isLogged = true;
        }
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
                    localStorage.setItem('currentUser', JSON.stringify(userDto));
                    this.currentUser = userDto;
                    this.isLogged = true;
                }
            );
        }
    }

    logout(): void {
        this.authService.logout();
        localStorage.removeItem('currentUser');
        this.currentUser = null;
        this.isLogged = false;
    }

    checkLoginValidator(controlName: string): boolean {
        return this.loginForm.get(controlName).invalid && this.loginForm.get(controlName).touched;
    }
}
