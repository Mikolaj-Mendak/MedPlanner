import { HttpClient, HttpParams } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environment";
import { Visit } from "../models/visit";
import { GetVisitAppointmentDto } from "../DTOs/get-visit-appointment-dto";
import { CreateVisitDto } from "../DTOs/add-visit-dto";

@Injectable({
    providedIn: 'root'
})

export class PatientService {

    constructor(private http: HttpClient) { }

    getPatientIncomingVisits(
        page: number = 1,
        pageSize: number = 10,
        firstName: string = null,
        lastName: string = null,
        pesel: string = null,
        sortBy: string = null
    ): Observable<Visit[]> {
        const url = `${environment.apiUrl}/visit/patient/incoming`;

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

    getPatientHistoryVisits(
        page: number = 1,
        pageSize: number = 10,
        firstName: string = null,
        lastName: string = null,
        pesel: string = null,
        sortBy: string = null
    ): Observable<Visit[]> {
        const url = `${environment.apiUrl}/visit/patient/history`;

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


    getVisitForPatient(visitId: string): Observable<Visit> {
        const url = `${environment.apiUrl}/visit/${visitId}`;
        return this.http.get<Visit>(url);
    }

    getVisitAppointmentForPatient(
        page: number = 1,
        pageSize: number = 10,
        firstName: string = null,
        lastName: string = null,
        address: string = null,
        clinicName: string = null,
        sortBy: string = null):
        Observable<GetVisitAppointmentDto[]> {
        const url = `${environment.apiUrl}/visit/visitAppointments`;

        let params = new HttpParams()
            .set('page', page.toString())
            .set('pageSize', pageSize.toString());

        if (firstName) {
            params = params.set('firstName', firstName);
        }

        if (lastName) {
            params = params.set('lastName', lastName);
        }

        if (address) {
            params = params.set('address', address);
        }

        if (clinicName) {
            params = params.set('clinicName', clinicName);
        }

        if (sortBy) {
            params = params.set('sortBy', sortBy);
        }

        return this.http.get<GetVisitAppointmentDto[]>(url, { params });
    }

    addVisit(visitDto: CreateVisitDto): Observable<void> {
        const url = `${environment.apiUrl}/visit`;
        return this.http.post<void>(url, visitDto);
    }

    GetAvaliableDatesForPatient(doctorId: string, clinicId: string): Observable<Date[]> {
        const url = `${environment.apiUrl}/visit/avaliableVisitDates/${doctorId}/${clinicId}`;
        return this.http.get<Date[]>(url);
    }

}

