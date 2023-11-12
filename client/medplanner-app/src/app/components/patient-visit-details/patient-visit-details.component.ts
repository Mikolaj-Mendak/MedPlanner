import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Visit } from 'src/app/models/visit';
import { DoctorService } from 'src/app/services/doctor-service';
import { PatientService } from 'src/app/services/patient.service';

@Component({
    selector: 'app-patient-visit-details',
    templateUrl: './patient-visit-details.component.html',
    styleUrls: ['./patient-visit-details.component.scss']
})
export class PatientVisitDetailsComponent implements OnInit {

    visit: Observable<Visit>;

    constructor(private patientService: PatientService, private router: Router) {

    }

    ngOnInit(): void {
        const url = this.router.url;
        const parts = url.split('/');
        const visitId = parts[parts.length - 1];
        this.visit = this.patientService.getVisitForPatient(visitId);
    }

}
