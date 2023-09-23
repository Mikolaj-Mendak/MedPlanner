import { Doctor } from "./doctor";

export interface DoctorAdmissionConditions {
    id?: string;
    clinicId?: string;
    doctorId?: string;
    doctor?: Doctor;
    specialization?: string;
    isNFZ?: boolean;
    isPrivate?: boolean;
    consultationFee?: number;
    workingDays?: string[];
    workHoursStart?: Date;
    workHoursEnd?: Date;
}