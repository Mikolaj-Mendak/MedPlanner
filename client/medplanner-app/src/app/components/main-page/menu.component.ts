import { Component, OnInit } from '@angular/core';
import { UserRolesEnum } from 'src/app/enums/user-roles-enum';
import { AuthService } from 'src/app/services/auth-service';

@Component({
    selector: 'app-menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.scss']
})
export class MenuComponent implements OnInit {
    constructor() {

    }
    ngOnInit(): void {

    }

    userRoleEnum = UserRolesEnum;

    getUserRole(): string {
        const currentUser = JSON.parse(localStorage.getItem('currentUser'));
        if (currentUser && currentUser.role) {
            return currentUser.role;
        }
    }

    getIsLogged() {
        const currentUser = localStorage.getItem('currentUser');
        if (currentUser) {
            return true;
        } else {
            return false;
        }
    }

}
