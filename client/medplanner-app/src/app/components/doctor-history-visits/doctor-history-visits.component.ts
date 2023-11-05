import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Visit } from 'src/app/models/visit';
import { DoctorService } from 'src/app/services/doctor-service';

@Component({
    selector: 'app-doctor-history-visits',
    templateUrl: './doctor-history-visits.component.html',
    styleUrls: ['./doctor-history-visits.component.scss']
})
export class DoctorHistoryVisitsComponent {

    visits$: Observable<Visit[]>;

    constructor(private doctorService: DoctorService, private router: Router) {

    }

    ngOnInit(): void {
        this.visits$ = this.doctorService.getHistoryVisits();
    }

    formatDateTime(dateTimeString: string): string {
        const date = new Date(dateTimeString);
        const formattedDate = date.toLocaleDateString();
        const formattedTime = date.toLocaleTimeString();
        return `${formattedDate} ${formattedTime}`;
    }

    openDetails(visitId: string) {
        this.router.navigate(['/doctor/historyVisit', visitId]);
    }

    cancelVisit(visitId: string) {
        this.doctorService.setInactiveVisit(visitId).subscribe(() => {
            console.log('Wizyta została anulowana.');
        }, error => {
            console.error('Błąd anulowania wizyty:', error);
        });
    }


}
