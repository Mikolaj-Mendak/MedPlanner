import { UserRolesEnum } from "../enums/user-roles-enum";

export interface User {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    pesel: string;
    role: UserRolesEnum;
}