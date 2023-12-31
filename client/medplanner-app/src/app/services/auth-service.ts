import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable, map } from 'rxjs';
import { environment } from '../../environment';
import { LoginDto } from '../DTOs/login-dto';
import { UserDto } from '../DTOs/user-dto';
import { User } from '../models/user';
import { RegisterDto } from '../DTOs/register-dto';

@Injectable({
    providedIn: 'root'
})


export class AuthService {

    private apiUrl = environment.apiUrl;

    private currentUserSubject: BehaviorSubject<User | null>;
    public currentUser: Observable<User | null>;

    constructor(private http: HttpClient) {
        this.currentUserSubject = new BehaviorSubject<User | null>(JSON.parse(localStorage.getItem('currentUser')) || null);
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): User | null {
        return this.currentUserSubject.value;
    }

    login(email: string, password: string): Observable<User> {
        return this.http.post<any>(`${this.apiUrl}/authorization/login`, { email, password })
            .pipe(map(user => {
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                return user;
            }));
    }

    logout() {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
    }

    register(registerData: RegisterDto): Observable<User> {
        console.log(registerData)
        return this.http.post<any>(`${this.apiUrl}/authorization/register`, registerData)
            .pipe(map(user => {
                localStorage.setItem('currentUser', JSON.stringify(user));
                this.currentUserSubject.next(user);
                return user;
            }));
    }

    ownerRegister(registerData: RegisterDto): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}/authorization/ownerRegister`, registerData)
            .pipe(map(response => {
                localStorage.setItem('currentUser', JSON.stringify(response));
                this.currentUserSubject.next(response);
                return response;
            }));
    }

    doctorRegister(registerData: RegisterDto): Observable<any> {
        return this.http.post<any>(`${this.apiUrl}/authorization/doctorRegister`, registerData)
            .pipe(map(response => {
                localStorage.setItem('currentUser', JSON.stringify(response));
                this.currentUserSubject.next(response);
                return response;
            }));
    }


    isLoggedIn(): boolean {
        const userToken = localStorage.getItem('token');
        return !!userToken;
    }
}