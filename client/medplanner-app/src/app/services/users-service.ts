import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environment";
import { Doctor } from "../models/doctor";
import { UserDetailsDto } from "../DTOs/user-details-dto";

@Injectable({
    providedIn: 'root'
})
export class UsersService {

    constructor(private http: HttpClient) { }

    getUserDetailsByEmail(email: string): Observable<UserDetailsDto> {
        const url = `${environment.apiUrl}/users/details/by-email/?email=${email}`;
        return this.http.get<UserDetailsDto>(url);
    }
}