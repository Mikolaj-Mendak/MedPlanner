import { Injectable } from "@angular/core";
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from "@angular/router";
import { UserRolesEnum } from "../enums/user-roles-enum";

@Injectable({
    providedIn: 'root'
})
export class DoctorOrClinicOwnerGuard implements CanActivate {
    constructor(private router: Router) { }

    userRoleEnum = UserRolesEnum;

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        const currentUser = JSON.parse(localStorage.getItem('currentUser'));
        const userRole = currentUser.role;

        if (userRole === this.userRoleEnum.Doctor || userRole === this.userRoleEnum.ClinicOwner) {
            return true;
        } else {
            this.router.navigate(['/']);
            return false;
        }
    }
}