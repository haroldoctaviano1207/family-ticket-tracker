export interface Ticket {
  id: number;
  title: string;
  description: string;
  assigneeId: string;
  assigneeName: string;
  assigneeAvatar: string;
  createdById: string;
  createdByName: string;
  createdAt: Date;
  dueDate?: Date;
  priority: 'Low' | 'Medium' | 'High';
  category: string;
  status: 'Open' | 'In Progress' | 'Pending Review' | 'Completed' | 'Cancelled';
  photoUrl?: string;
  completedAt?: Date;
  approvedAt?: Date;
  isOverdue: boolean;
  commentsCount: number;
}

export interface CreateTicketRequest {
  title: string;
  description: string;
  assigneeId: string;
  dueDate?: Date;
  priority: 'Low' | 'Medium' | 'High';
  category: string;
  photoUrl?: string;
}

export interface UpdateTicketRequest {
  title?: string;
  description?: string;
  assigneeId?: string;
  dueDate?: Date;
  priority?: 'Low' | 'Medium' | 'High';
  category?: string;
  status?: 'Open' | 'In Progress' | 'Pending Review' | 'Completed' | 'Cancelled';
  photoUrl?: string;
}

export const TICKET_CATEGORIES = [
  'General',
  'Chores',
  'Maintenance',
  'Homework Help',
  'Shopping',
  'Cleaning',
  'Cooking',
  'Yard Work',
  'Pet Care',
  'Other'
];

export const TICKET_PRIORITIES = ['Low', 'Medium', 'High'] as const;
export const TICKET_STATUSES = ['Open', 'In Progress', 'Pending Review', 'Completed', 'Cancelled'] as const;
