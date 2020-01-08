import { Photo } from './photo';

export interface User {
    id: number;
    username: string;
    gender?: string;
    birth?: Date;
    registrationDate?: Date;
    lastActive?: Date;
    age?: number;
    introduction?: string;
    photoUrl?: string;
    photos?: Photo[];
    isFollowedByCurrentUser?: boolean;
}
