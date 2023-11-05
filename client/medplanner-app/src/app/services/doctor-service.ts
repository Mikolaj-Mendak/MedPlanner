import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Doctor } from '../models/doctor';
import { Observable } from 'rxjs';
import { environment } from 'src/environment';
import { DoctorAdmissionConditions } from '../models/doctor-admission';
import { Clinic } from '../models/clinic';
import { Visit } from '../models/visit';

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
        const url = `${environment.apiUrl}/doctor/admission`;
        return this.http.post<void>(url, admissionConditionDto);
    }

    getIncominVisits(): Observable<Visit[]> {
        const url = `${environment.apiUrl}/visit/doctor/incoming`;
        return this.http.get<Visit[]>(url);
    }

    getHistoryVisits(): Observable<Visit[]> {
        const url = `${environment.apiUrl}/visit/doctor/history`;
        return this.http.get<Visit[]>(url);
    }

    setInactiveVisit(visitId: string): Observable<void> {
        const url = `${environment.apiUrl}/visit/cancel/${visitId}`;
        return this.http.put<void>(url, null);
    }

    getVisitForDoctor(visitId: string): Observable<Visit> {
        const url = `${environment.apiUrl}/visit/${visitId}`;
        return this.http.get<Visit>(url);
    }


}