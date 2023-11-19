import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth-service';
import { UserRolesEnum } from 'src/app/enums/user-roles-enum';
import { Router } from '@angular/router';

@Component({
    selector: 'app-header',
    templateUrl: './header.component.html',
    styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit {

    loginForm: FormGroup;
    searchForm: FormGroup;
    isLogged: boolean;
    currentUser: any;
    currentTime: Date;

    constructor(private dialog: MatDialog,
        private formBuilder: FormBuilder,
        private authService: AuthService,
        private router: Router) {
        this.setLoginForm();
        this.initSearchForm();
    }

    ngOnInit(): void {
        const currentUser = localStorage.getItem('currentUser');
        if (currentUser) {
            this.currentUser = JSON.parse(currentUser);
            this.isLogged = true;
        }
        this.updateCurrentTime();
        setInterval(() => {
            this.updateCurrentTime();
        }, 1000);
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

    initSearchForm(): void {
        this.searchForm = this.formBuilder.group({
            city: ['', Validators.required],
            specialization: ['', Validators.required],
            sortBy: [null, Validators.required]
        });
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

    updateCurrentTime() {
        this.currentTime = new Date();
    }

    userRoleEnum = UserRolesEnum;

    getUserRole(): string {
        const currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.role) {
            return currentUser.role;
        }
    }

    onSearchClick(): void {
        const { city, specialization, sortBy } = this.searchForm.value;
        this.router.navigate(['/visitAppointment'], {
            queryParams: {
                city,
                specialization,
                sortBy
            }
        });
    }
}
