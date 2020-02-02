import { PriorityType } from './priorityType.enum';
import { TicketType } from './ticketType.enum';
import { User } from './user';
import { Status } from './status';
import { SubStatus } from './subStatus';
import { Category } from './category';
import { Note } from './note';
import { Resolution } from './resolution';
import { Survey } from './survey';
import { UserGroup } from './userGroup';

export interface Ticket {
    id: number;
    location: string;
    summary: string;
    description: string;
    priority: PriorityType;
    type: TicketType;
    createdAt: Date;
    resolvedAt?: Date;
    closedAt?: Date;
    invoicedAt?: Date;
    modifiedAt: Date;
    sourceId: number;
    source: string;
    statusId: number;
    status: Status;
    subStatus?: SubStatus;
    category: Category;
    requesterId: number;
    requester: User;
    assignedToId?: number;
    assignedTo?: User;
    assignmentGroup?: UserGroup;
    notes?: Note[];
    resolutions?: Resolution[];
    surveys?: Survey[];
}
