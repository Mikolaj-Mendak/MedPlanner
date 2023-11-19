import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, of } from 'rxjs';
import { CreateVisitDto } from 'src/app/DTOs/add-visit-dto';
import { Clinic } from 'src/app/models/clinic';
import { Doctor } from 'src/app/models/doctor';
import { DoctorAdmissionConditions } from 'src/app/models/doctor-admission';
import { ClinicOwnerServiceService } from 'src/app/services/clinic-owner/clinic-owner-service.service';
import { DoctorService } from 'src/app/services/doctor-service';
import { PatientService } from 'src/app/services/patient.service';
import { FullCalendarComponent } from '@fullcalendar/angular';
import dayGridPlugin from '@fullcalendar/daygrid';
import timeGridPlugin from '@fullcalendar/timegrid';
import interactionPlugin from '@fullcalendar/interaction';
import { CalendarOptions, EventClickArg } from '@fullcalendar/core';
import { ToastrService } from 'ngx-toastr';

@Component({
    selector: 'app-visit-appointment-details',
    templateUrl: './visit-appointment-details.component.html',
    styleUrls: ['./visit-appointment-details.component.scss']
})
export class VisitAppointmentDetailsComponent implements OnInit, AfterViewInit {

    @ViewChild(FullCalendarComponent) calendar!: FullCalendarComponent;



    calendarOptions: CalendarOptions = {
        plugins: [dayGridPlugin, timeGridPlugin, interactionPlugin],
        initialView: 'dayGridMonth',
        locale: 'pl',
        headerToolbar: {
            left: 'prev,next',
            center: 'title',
            right: null,
        },
        events: [],
        eventClick: (arg: EventClickArg) => {
            this.handleEventClick(arg);
        }
    };

    clinic$: Observable<Clinic>;
    doctor$: Observable<Doctor>;
    dates$: Observable<Date[]>;
    doctorAdmission$: Observable<DoctorAdmissionConditions>;
    visitForm: FormGroup;
    isDayAvailable: { [key: string]: boolean } = {};

    doctorParamsId: string;
    clinicParamsId: string;

    datePlaceholder: string;

    constructor(private clinicService: ClinicOwnerServiceService, private doctorService: DoctorService, private route: ActivatedRoute,
        private fb: FormBuilder, private patientService: PatientService, private toastr: ToastrService, private router: Router) {
        this.visitForm = this.fb.group({
            visitDate: [''],
            patientId: [''],
            doctorId: [''],
            clinicId: [''],
            description: [''],
        });
    }
    ngAfterViewInit(): void {
        this.loadCalendarEvents();

    }

    ngOnInit(): void {
        this.loadClinicData();
        this.loadDoctorData();
        this.loadDoctorAdmissionData();
        this.loadAvaliableVisitDatesForPatient();

    }


    public patchFormValue(controlName: string, value: any): void {
        this.visitForm.get(controlName)?.patchValue(value);
    }

    loadClinicData(): void {
        this.route.params.subscribe((params) => {
            this.clinicParamsId = params['clinicId'];
            this.clinicService.getSingleClinic(this.clinicParamsId).subscribe(
                (clinic) => {
                    this.clinic$ = of(clinic);
                    this.patchFormValue('clinicId', this.clinicParamsId);
                },
                (error) => {
                    console.error('Error loading clinic data', error);
                }
            );
        });
    }

    loadDoctorData(): void {
        this.route.params.subscribe((params) => {
            this.doctorParamsId = params['doctorId'];
            this.doctorService.getDoctorById(this.doctorParamsId).subscribe(
                (doctor) => {
                    this.doctor$ = of(doctor);
                    this.patchFormValue('doctorId', this.doctorParamsId);
                },
                (error) => {
                    console.error('Error loading clinic data', error);
                }
            );
        });
    }

    loadDoctorAdmissionData(): void {
        this.route.params.subscribe((params) => {
            this.doctorParamsId = params['doctorId'];
            this.clinicParamsId = params['clinicId'];
            this.doctorService.getAdmissionByClinicAndDoctor(this.doctorParamsId, this.clinicParamsId).subscribe(
                (doctorAdmission) => {
                    this.doctorAdmission$ = of(doctorAdmission);
                },
                (error) => {
                    console.error('Error loading clinic data', error);
                }
            );
        });
    }

    addVisit(): void {
        const visitDto = this.visitForm.value as CreateVisitDto;

        this.patientService.addVisit(visitDto).subscribe(
            () => {
                this.toastr.success('Wizyta została dodana pomyślnie.', 'Sukces');
                console.log("success");
                this.router.navigate(['/patient/incomingVisits']);
            },
            (error) => {
                this.toastr.error('Wystąpił błąd podczas dodawania wizyty.', 'Błąd');
                console.error('Error adding visit:', error);
            }
        );
    }

    loadAvaliableVisitDatesForPatient() {
        this.dates$ = this.patientService.GetAvaliableDatesForPatient(this.doctorParamsId, this.clinicParamsId)

    }

    loadCalendarEvents(): void {
        this.dates$.subscribe(availableDates => {
            const events = availableDates.map(date => ({
                title: 'Wolny termin',
                start: new Date(date),
                allDay: false,
            }));
            this.calendarOptions.events = events;
            this.calendar?.getApi()?.refetchEvents();
        });
    }

    handleEventClick(arg: EventClickArg): void {
        const selectedDate = arg.event.start;
        this.visitForm.patchValue({ visitDate: selectedDate });
        this.formatDate(selectedDate)
        this.toastr.success('Data została wybrana.', 'Sukces');
    }

    formatDate(dateTime: Date): void {
        const dateOptions: Intl.DateTimeFormatOptions = {
            year: 'numeric',
            month: '2-digit',
            day: '2-digit',
        };

        const timeOptions: Intl.DateTimeFormatOptions = {
            hour: '2-digit',
            minute: '2-digit',
            second: '2-digit',
            hour12: false,
        };

        const formattedDate = dateTime.toLocaleDateString('en-US', dateOptions);
        const formattedTime = dateTime.toLocaleTimeString('en-US', timeOptions);


        this.datePlaceholder = `${formattedDate} ${formattedTime}`;
    }
}
