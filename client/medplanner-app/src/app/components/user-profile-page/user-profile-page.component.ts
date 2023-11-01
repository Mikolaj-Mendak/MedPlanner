import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { UserDetailsDto } from 'src/app/DTOs/user-details-dto';
import { UsersService } from 'src/app/services/users-service';

@Component({
    selector: 'app-user-profile-page',
    templateUrl: './user-profile-page.component.html',
    styleUrls: ['./user-profile-page.component.scss']
})

export class UserProfilePageComponent implements OnInit {
    userDetails$: Observable<UserDetailsDto>;

    constructor(private usersService: UsersService) {

    }

    ngOnInit(): void {
        this.fetchUserDetails();

    }

    private fetchUserDetails() {
        const userEmail = localStorage.getItem('currentUser') ? JSON.parse(localStorage.getItem('currentUser')).email : null;
        this.userDetails$ = this.usersService.getUserDetailsByEmail(userEmail.toString());
    }
}
