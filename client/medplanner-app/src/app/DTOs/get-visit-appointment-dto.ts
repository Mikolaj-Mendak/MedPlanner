export interface GetVisitAppointmentDto {
    fee: number | null;
    closestDate: Date | null;
    doctorFirstName: string | null;
    doctorLastName: string | null;
    clinicName: string | null;
    clinicAddress: string | null;
    specialization: string | null;
    doctorId: string | null;
    clinicId: string | null;
    doctorAdmissionId: string | null;
}