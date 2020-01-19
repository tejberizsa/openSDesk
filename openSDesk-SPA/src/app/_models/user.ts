import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    registrationDate?: Date;
    lastActive?: Date;
    introduction?: string;
    photoUrl?: string;
    photos?: Photo[];
}
