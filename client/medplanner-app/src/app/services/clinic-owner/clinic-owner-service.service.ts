import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { AddClinicDto } from 'src/app/DTOs/add-clinic-dto';
import { Clinic } from 'src/app/models/clinic';
import { environment } from 'src/environment';

@Injectable({
    providedIn: 'root'
})
export class ClinicOwnerServiceService {

    constructor(private http: HttpClient) { }

    getAllClinics(): Observable<Clinic[]> {
        const url = `${environment.apiUrl}/clinicowner/clinics`;
        return this.http.get<Clinic[]>(url);
    }

    addClinic(addClinicDto: AddClinicDto): Observable<Clinic> {
        const url = `${environment.apiUrl}/clinicowner/clinics`;
        return this.http.post<Clinic>(url, addClinicDto);
    }

    deleteClinic(clinicId: string): Observable<void> {
        const url = `${environment.apiUrl}/clinicowner/${clinicId}/clinics`;
        return this.http.delete<void>(url);
    }

    getSingleClinic(clinicId: string): Observable<Clinic> {
        const url = `${environment.apiUrl}/clinicowner/clinics/${clinicId}`;
        return this.http.get<Clinic>(url);
    }

    addDoctorToClinic(clinicId: string, doctorId: string): Observable<any> {
        const url = `${environment.apiUrl}/clinicowner/clinics/${clinicId}/doctors/${doctorId}`;
        return this.http.post(url, null);
    }

    addDoctorToClinicByNumber(clinicId: string, doctorNumber: string): Observable<any> {
        const url = `${environment.apiUrl}/clinicowner/clinics/${clinicId}/doctorNumber/${doctorNumber}`;
        return this.http.post(url, null);
    }

    removeDoctorFromClinic(clinicId: string, doctorId: string): Observable<void> {
        const url = `${environment.apiUrl}/clinicowner/removeDoctor/clinics/${clinicId}/doctors/${doctorId}`;
        return this.http.delete<void>(url);
    }

}
