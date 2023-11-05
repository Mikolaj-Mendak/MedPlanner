import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Visit } from 'src/app/models/visit';
import { DoctorService } from 'src/app/services/doctor-service';

@Component({
    selector: 'app-doctor-inocoming-visits',
    templateUrl: './doctor-inocoming-visits.component.html',
    styleUrls: ['./doctor-inocoming-visits.component.scss']
})
export class DoctorInocomingVisitsComponent implements OnInit {

    visits$: Observable<Visit[]>;

    constructor(private doctorService: DoctorService) {

    }

    ngOnInit(): void {
        this.visits$ = this.doctorService.getIncominVisits();
    }

}
