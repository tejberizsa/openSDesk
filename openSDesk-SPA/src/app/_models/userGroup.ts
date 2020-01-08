import { User } from './user';

export interface UserGroup {
    id: number;
    name: string;
    users?: User[];
}
