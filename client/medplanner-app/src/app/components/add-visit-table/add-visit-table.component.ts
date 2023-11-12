import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { GetVisitAppointmentDto } from 'src/app/DTOs/get-visit-appointment-dto';
import { PatientService } from 'src/app/services/patient.service';

@Component({
    selector: 'app-add-visit-table',
    templateUrl: './add-visit-table.component.html',
    styleUrls: ['./add-visit-table.component.scss']
})
export class AddVisitTableComponent implements OnInit {
    appointmentVisits$: Observable<GetVisitAppointmentDto[]>;
    currentPage = 1;
    pageSize = 10;
    firstNameFilter = '';
    lastNameFilter = '';
    peselFilter = '';
    sortOption = '';
    address = '';
    clinicName = '';

    constructor(private patientService: PatientService, private router: Router) {

    }

    ngOnInit(): void {
        this.getVisitAppointments();
    }

    getVisitAppointments(): void {
        this.appointmentVisits$ = this.patientService.getVisitAppointmentForPatient(
            this.currentPage,
            this.pageSize,
            this.firstNameFilter,
            this.lastNameFilter,
            this.address,
            this.clinicName,
            this.sortOption);
    }

    formatDateTime(dateTimeString: string): string {
        const date = new Date(dateTimeString);
        const formattedDate = date.toLocaleDateString();
        const formattedTime = date.toLocaleTimeString();
        return `${formattedDate} ${formattedTime}`;
    }

    routeToCreateVisitComponent(doctorId: string, clinicId: string): void {
        this.router.navigate(['createVisit', doctorId, clinicId]);
    }

    onPageChange(newPage: number) {
        this.currentPage = newPage;
        this.getVisitAppointments();
    }

}
