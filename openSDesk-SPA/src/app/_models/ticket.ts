import { PriorityType } from './priorityType.enum';
import { TicketType } from './ticketType.enum';

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
    status: string;
}
