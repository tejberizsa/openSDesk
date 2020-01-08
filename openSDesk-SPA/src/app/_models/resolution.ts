import { User } from './user';

export interface Resolution {
    id: number;
    note: string;
    createdAt: Date;
    refused: boolean;
    codeId: number;
    code: string;
    ownerId: number;
    owner: User;
}
