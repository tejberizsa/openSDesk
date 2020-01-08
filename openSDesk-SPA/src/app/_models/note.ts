import { User } from './user';

export interface Note {
    id: number;
    workNote: string;
    createdAt: Date;
    owner: User;
    ownerId: number;
}
