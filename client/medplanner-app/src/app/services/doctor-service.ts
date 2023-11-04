import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doctor } from '../models/doctor';
import { Observable } from 'rxjs';
import { environment } from 'src/environment';
import { DoctorAdmissionConditions } from '../models/doctor-admission';
import { Clinic } from '../models/clinic';

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
        return this.http.get<DoctorAdmissionConditions>(url);
    }

    getClinicsForDoctor(): Observable<Clinic[]> {
        const url = `${environment.apiUrl}/doctor/clinics`;
        return this.http.get<Clinic[]>(url);
    }

    resingFromClinic(clinicId: string): Observable<void> {
        const url = `${environment.apiUrl}/doctor/clinics/${clinicId}`;
        return this.http.delete<void>(url);
    }

    getAdmissionByClinicForDoctor(clinicId: string): Observable<DoctorAdmissionConditions> {
        const url = `${environment.apiUrl}/doctor/getAdmissionByClinicForDoctor/${clinicId}`;
        return this.http.get<DoctorAdmissionConditions>(url);
    }
    addAdmissionCondition(admissionConditionDto: DoctorAdmissionConditions): Observable<void> {
        console.log(admissionConditionDto)
        const url = `${environment.apiUrl}/doctor/admission`;
        return this.http.post<void>(url, admissionConditionDto);
    }

}