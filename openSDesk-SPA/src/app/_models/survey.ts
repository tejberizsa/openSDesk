import { User } from './user';

export interface Survey {
    id: number;
    comment: string;
    createdAt: Date;
    refusal: boolean;
    responseId: number;
    response: string;
}
