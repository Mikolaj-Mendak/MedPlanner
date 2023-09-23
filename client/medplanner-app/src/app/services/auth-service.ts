import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environment';
import { LoginDto } from '../DTOs/login-dto';
import { UserDto } from '../DTOs/user-dto';

@Injectable({
    providedIn: 'root'
})


export class AuthService {
    private apiUrl = environment.apiUrl;

    constructor(private http: HttpClient) { }

    login(loginDto: LoginDto): Observable<UserDto> {
        return this.http.post<UserDto>(`${this.apiUrl}/authorization/login`, loginDto);
    }
}