import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Visit } from 'src/app/models/visit';
import { PatientService } from 'src/app/services/patient.service';

@Component({
    selector: 'app-patient-visits-history',
    templateUrl: './patient-visits-history.component.html',
    styleUrls: ['./patient-visits-history.component.scss']
})
export class PatientVisitsHistoryComponent implements OnInit {

    visits$: Observable<Visit[]>;
    currentPage = 1;
    pageSize = 10;
    firstNameFilter = '';
    lastNameFilter = '';
    peselFilter = '';
    sortOption = '';

    constructor(private patientService: PatientService, private router: Router) {

    }

    ngOnInit(): void {
        this.loadVisits();
    }

    formatDateTime(dateTimeString: string): string {
        const date = new Date(dateTimeString);
        const formattedDate = date.toLocaleDateString();
        const formattedTime = date.toLocaleTimeString();
        return `${formattedDate} ${formattedTime}`;
    }

    openDetails(visitId: string) {
        this.router.navigate(['/patient/incomingVisit', visitId]);
    }

    cancelVisit(visitId: string) {
        this.patientService.setInactiveVisit(visitId).subscribe(() => {
            console.log('Wizyta została anulowana.');
        }, error => {
            console.error('Błąd anulowania wizyty:', error);
        });
    }

    loadVisits() {
        this.visits$ = this.patientService.getPatientHistoryVisits(
            this.currentPage,
            this.pageSize,
            this.firstNameFilter,
            this.lastNameFilter,
            this.peselFilter,
            this.sortOption
        );
    }

    onPageChange(newPage: number) {
        this.currentPage = newPage;
        this.loadVisits();
    }

}
