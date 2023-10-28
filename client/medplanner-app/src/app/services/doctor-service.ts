import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doctor } from '../models/doctor';
import { Observable } from 'rxjs';
import { environment } from 'src/environment';
import { DoctorAdmissionConditions } from '../models/doctor-admission';

@Injectable({
    providedIn: 'root'
})
export class DoctorService {

    constructor(private http: HttpClient) { }

    getDoctorsForClinic(clinicId: string): Observable<Doctor[]> {
        const url = `${environment.apiUrl}/doctor/doctorsByClinic/${clinicId}`;
        return this.http.get<Doctor[]>(url);
    }

    getAllDoctors(): Observable<Doctor[]> {
        const url = `${environment.apiUrl}/doctor`;
        return this.http.get<Doctor[]>(url);
    }

    getAdmissionByClinicAndDoctor(doctorId: string, clinicId: string): Observable<DoctorAdmissionConditions> {
        const url = `${environment.apiUrl}/doctor/getAdmissionByClinicAndDoctor/${doctorId}/${clinicId}`;
        console.log(url)
        return this.http.get<DoctorAdmissionConditions>(url);
    }

}