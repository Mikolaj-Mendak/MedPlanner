import { Clinic } from "./clinic";
import { Doctor } from "./doctor";
import { DoctorAdmissionConditions } from "./doctor-admission";
import { User } from "./user";

export interface Visit {
    id?: string;
    visitDate?: Date;
    patientId?: string;
    patient?: User | null;
    doctorId?: string;
    clinicId?: string;
    doctorAdmissionId?: string;
    clinic: Clinic;
    doctorAdmisison: DoctorAdmissionConditions;
    doctor?: Doctor | null;
    isActive?: boolean;
    description?: string;
    reccomendations?: string;
    fee?: number | null;
}