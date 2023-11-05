import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { Visit } from 'src/app/models/visit';
import { DoctorService } from 'src/app/services/doctor-service';

@Component({
    selector: 'app-visit-doctor-details',
    templateUrl: './visit-doctor-details.component.html',
    styleUrls: ['./visit-doctor-details.component.scss']
})
export class VisitDoctorDetailsComponent implements OnInit {
    visit: Observable<Visit>;

    constructor(private doctorService: DoctorService, private router: Router) {

    }

    ngOnInit(): void {
        const url = this.router.url;
        const parts = url.split('/');
        const visitId = parts[parts.length - 1];
        this.visit = this.doctorService.getVisitForDoctor(visitId);
    }

}
