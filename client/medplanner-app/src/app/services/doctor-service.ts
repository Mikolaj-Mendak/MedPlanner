import { HttpClient, HttpParams } from '@angular/common/http';
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

    getDoctorsForClinic(
        clinicId: string,
        currentPage: number = 1,
        pageSize: number = 10,
        firstName: string = null,
        lastName: string = null,
        pesel: string = null,
        doctorNumber: string = null
    ): Observable<Doctor[]> {

        let params = new HttpParams()
            .set('page', currentPage.toString())
            .set('pageSize', pageSize.toString());

        if (firstName) {
            params = params.set('firstName', firstName);
        }

        if (lastName) {
            params = params.set('lastName', lastName);
        }

        if (pesel) {
            params = params.set('pesel', pesel);
        }

        if (doctorNumber) {
            params = params.set('doctorNumber', doctorNumber);
        }

        const url = `${environment.apiUrl}/doctor/doctorsByClinic/${clinicId}`;
        return this.http.get<Doctor[]>(url, { params });
    }


    getAllDoctors(): Observable<Doctor[]> {
        const url = `${environment.apiUrl}/doctor`;
        return this.http.get<Doctor[]>(url);
    }

    getDoctorById(doctorId: string): Observable<Doctor> {
        const url = `${environment.apiUrl}/doctor/${doctorId}`;
        return this.http.get<Doctor>(url);
    }

    getAdmissionByClinicAndDoctor(doctorId: string, clinicId: string): Observable<DoctorAdmissionConditions> {
        const url = `${environment.apiUrl}/doctor/getAdmissionByClinicAndDoctor/${doctorId}/${clinicId}`;
        return this.http.get<DoctorAdmissionConditions>(url);
    }

    getClinicsForDoctor(
        page: number = 1,
        pageSize: number = 10,
        address: string = null,
        name: string = null
    ): Observable<Clinic[]> {

        let params = new HttpParams()
            .set('page', page.toString())
            .set('pageSize', pageSize.toString());

        if (name) {
            params = params.set('name', name);
        }

        if (address) {
            params = params.set('address', address);
        }

        const url = `${environment.apiUrl}/doctor/clinics`;
        return this.http.get<Clinic[]>(url, { params });
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

    getIncominVisits(
        page: number = 1,
        pageSize: number = 10,
        firstName: string = null,
        lastName: string = null,
        pesel: string = null,
        sortBy: string = null
    ): Observable<Visit[]> {
        const url = `${environment.apiUrl}/visit/doctor/incoming`;

        let params = new HttpParams()
            .set('page', page.toString())
            .set('pageSize', pageSize.toString());

        if (firstName) {
            params = params.set('firstName', firstName);
        }

        if (lastName) {
            params = params.set('lastName', lastName);
        }

        if (pesel) {
            params = params.set('pesel', pesel);
        }


        if (sortBy) {
            params = params.set('sortBy', sortBy);
        }

        return this.http.get<Visit[]>(url, { params });

    }

    getHistoryVisits(
        page: number = 1,
        pageSize: number = 10,
        firstName: string = null,
        lastName: string = null,
        pesel: string = null,
        sortBy: string = null
    ): Observable<Visit[]> {
        const url = `${environment.apiUrl}/visit/doctor/history`;

        let params = new HttpParams()
            .set('page', page.toString())
            .set('pageSize', pageSize.toString());

        if (firstName) {
            params = params.set('firstName', firstName);
        }

        if (lastName) {
            params = params.set('lastName', lastName);
        }

        if (pesel) {
            params = params.set('pesel', pesel);
        }


        if (sortBy) {
            params = params.set('sortBy', sortBy);
        }

        return this.http.get<Visit[]>(url, { params });
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