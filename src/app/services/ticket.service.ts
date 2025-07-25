import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { delay, map, Observable, of } from 'rxjs';
import { Ticket, CreateTicketRequest, UpdateTicketRequest } from '../models/ticket.model';

@Injectable({
  providedIn: 'root'
})
export class TicketService {
  private apiUrl = '/api/ticket';

  constructor(private http: HttpClient) {}

  getTickets(userId?: string, status?: string, category?: string): Observable<Ticket[]> {
    let params = new HttpParams();
    if (userId) params = params.set('userId', userId);
    if (status) params = params.set('status', status);
    if (category) params = params.set('category', category);

    return this.http.get<Ticket[]>(this.apiUrl, { params });
  }

  getTicket(id: number): Observable<Ticket> {
    return this.http.get<Ticket>(`${this.apiUrl}/${id}`);
  }

  createTicket(ticket: CreateTicketRequest): Observable<Ticket> {
    return this.http.post<Ticket>(this.apiUrl, ticket);
  }

  updateTicket(id: number, ticket: UpdateTicketRequest): Observable<Ticket> {
    return this.http.put<Ticket>(`${this.apiUrl}/${id}`, ticket);
  }

  deleteTicket(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }

  completeTicket(id: number): Observable<Ticket> {
    return this.http.post<Ticket>(`${this.apiUrl}/${id}/complete`, {});
  }

  approveTicket(id: number): Observable<Ticket> {
    return this.http.post<Ticket>(`${this.apiUrl}/${id}/approve`, {});
  }

  rejectTicket(id: number): Observable<Ticket> {
    return this.http.post<Ticket>(`${this.apiUrl}/${id}/reject`, {});
  }

  // Helper methods for filtering
  getMyTickets(userId: string): Observable<Ticket[]> {
    return this.getTickets(userId);
  }

  getOverdueTickets(): Observable<Ticket[]> {
    return this.getTickets();
  }

  getTicketsByStatus(status: string): Observable<Ticket[]> {
    return this.getTickets(undefined, status);
  }

  getTicketsByCategory(category: string): Observable<Ticket[]> {
    return this.getTickets(undefined, undefined, category);
  }
}


@Injectable({
  providedIn: 'root'
})
export class MockTicketService {
  private sampleTickets: Ticket[] = [
    {
      id: 1,
      title: 'Clean my room',
      description: 'Make sure to vacuum and dust all surfaces.',
      assigneeId: 'user-101',
      assigneeName: 'John Doe',
      assigneeAvatar: 'https://i.pravatar.cc/150?img=4',
      createdById: 'user-102',
      createdByName: 'Jane Smith',
      createdAt: new Date('2024-07-15T09:00:00Z'),
      dueDate: new Date('2024-07-25T17:00:00Z'),
      priority: 'High',
      category: 'Chores',
      status: 'In Progress',
      photoUrl: 'https://picsum.photos/id/10/200/300',
      isOverdue: false,
      commentsCount: 2
    },
    {
      id: 2,
      title: 'Take out the trash',
      description: 'Recycling and regular trash to the curb.',
      assigneeId: 'user-103',
      assigneeName: 'Peter Jones',
      assigneeAvatar: 'https://i.pravatar.cc/150?img=6',
      createdById: 'user-102',
      createdByName: 'Jane Smith',
      createdAt: new Date('2024-07-16T10:00:00Z'),
      dueDate: new Date('2024-07-23T08:00:00Z'),
      priority: 'Medium',
      category: 'Chores',
      status: 'Pending Review',
      photoUrl: undefined,
      isOverdue: true, // Marked as overdue for sample
      commentsCount: 1
    },
    {
      id: 3,
      title: 'Help with Math Homework',
      description: 'Need help with algebra equations, specifically chapter 5.',
      assigneeId: 'user-104',
      assigneeName: 'Sara Williams',
      assigneeAvatar: 'https://i.pravatar.cc/150?img=7',
      createdById: 'user-102',
      createdByName: 'Jane Smith',
      createdAt: new Date('2024-07-18T14:00:00Z'),
      dueDate: new Date('2024-07-26T20:00:00Z'),
      priority: 'High',
      category: 'Homework Help',
      status: 'Open',
      photoUrl: 'https://picsum.photos/id/20/200/300',
      isOverdue: false,
      commentsCount: 2
    },
    {
      id: 4,
      title: 'Wash the car',
      description: 'Exterior wash and interior vacuum.',
      assigneeId: 'user-101',
      assigneeName: 'John Doe',
      assigneeAvatar: 'https://i.pravatar.cc/150?img=4',
      createdById: 'user-102',
      createdByName: 'Jane Smith',
      createdAt: new Date('2024-07-19T08:00:00Z'),
      dueDate: new Date('2024-07-24T12:00:00Z'),
      priority: 'Low',
      category: 'Maintenance',
      status: 'Completed',
      photoUrl: undefined,
      completedAt: new Date('2024-07-24T11:00:00Z'),
      approvedAt: new Date('2024-07-24T13:00:00Z'),
      isOverdue: false,
      commentsCount: 0
    },
    {
      id: 5,
      title: 'Grocery Shopping',
      description: 'Buy milk, eggs, bread, and fruits. See the attached list.',
      assigneeId: 'user-102',
      assigneeName: 'Jane Smith',
      assigneeAvatar: 'https://i.pravatar.cc/150?img=5',
      createdById: 'user-102',
      createdByName: 'Jane Smith',
      createdAt: new Date('2024-07-20T16:00:00Z'),
      dueDate: new Date('2024-07-27T10:00:00Z'),
      priority: 'Medium',
      category: 'Shopping',
      status: 'Open',
      photoUrl: undefined,
      isOverdue: false,
      commentsCount: 0
    }
  ];

  constructor() { }

  getTickets(userId?: string, status?: string, category?: string): Observable<Ticket[]> {
    console.log(`Mock: Fetching tickets - userId: ${userId}, status: ${status}, category: ${category}`);
    let filteredTickets = [...this.sampleTickets]; // Create a copy to avoid direct mutation

    if (userId) {
      filteredTickets = filteredTickets.filter(ticket => ticket.assigneeId === userId || ticket.createdById === userId);
    }
    if (status) {
      filteredTickets = filteredTickets.filter(ticket => ticket.status === status);
    }
    if (category) {
      filteredTickets = filteredTickets.filter(ticket => ticket.category === category);
    }

    return of(filteredTickets).pipe(delay(400));
  }

  getTicket(id: number): Observable<Ticket> {
    console.log(`Mock: Fetching ticket with ID: ${id}`);
    const ticket = this.sampleTickets.find(t => t.id === id);
    if (ticket) {
      return of(ticket).pipe(delay(300));
    } else {
      return new Observable<Ticket>(observer => {
        observer.error({ status: 404, message: 'Ticket not found' });
        observer.complete();
      }).pipe(delay(300));
    }
  }

  createTicket(ticket: CreateTicketRequest): Observable<Ticket> {
    console.log('Mock: Creating ticket:', ticket);
    const newTicket: Ticket = {
      id: Math.max(...this.sampleTickets.map(t => t.id)) + 1, // Simple ID generation
      ...ticket,
      createdById: 'user-mock-current', // Placeholder for current user
      createdByName: 'Mock Current User', // Placeholder
      assigneeName: 'Mock Assignee Name', // Placeholder
      assigneeAvatar: 'https://i.pravatar.cc/150?img=9', // Placeholder
      createdAt: new Date(),
      status: 'Open',
      isOverdue: false,
      commentsCount: 0
    };
    this.sampleTickets.push(newTicket);
    return of(newTicket).pipe(delay(300));
  }

  updateTicket(id: number, ticket: UpdateTicketRequest): Observable<Ticket> {
    console.log(`Mock: Updating ticket ID ${id}:`, ticket);
    const index = this.sampleTickets.findIndex(t => t.id === id);
    if (index > -1) {
      this.sampleTickets[index] = { ...this.sampleTickets[index], ...ticket };
      return of(this.sampleTickets[index]).pipe(delay(300));
    } else {
      return new Observable<Ticket>(observer => {
        observer.error({ status: 404, message: 'Ticket not found for update' });
        observer.complete();
      }).pipe(delay(300));
    }
  }

  deleteTicket(id: number): Observable<void> {
    console.log(`Mock: Deleting ticket with ID: ${id}`);
    const initialLength = this.sampleTickets.length;
    this.sampleTickets = this.sampleTickets.filter(ticket => ticket.id !== id);
    if (this.sampleTickets.length < initialLength) {
      return of(void 0).pipe(delay(200));
    } else {
      return new Observable<void>(observer => {
        observer.error({ status: 404, message: 'Ticket not found for deletion' });
        observer.complete();
      }).pipe(delay(200));
    }
  }

  completeTicket(id: number): Observable<Ticket> {
    console.log(`Mock: Completing ticket ID: ${id}`);
    const index = this.sampleTickets.findIndex(t => t.id === id);
    if (index > -1) {
      this.sampleTickets[index].status = 'Pending Review'; // Or 'Completed' directly depending on flow
      this.sampleTickets[index].completedAt = new Date();
      return of(this.sampleTickets[index]).pipe(delay(300));
    } else {
      return new Observable<Ticket>(observer => {
        observer.error({ status: 404, message: 'Ticket not found for completion' });
        observer.complete();
      }).pipe(delay(300));
    }
  }

  approveTicket(id: number): Observable<Ticket> {
    console.log(`Mock: Approving ticket ID: ${id}`);
    const index = this.sampleTickets.findIndex(t => t.id === id);
    if (index > -1) {
      this.sampleTickets[index].status = 'Completed';
      this.sampleTickets[index].approvedAt = new Date();
      // Logic to update user points/completed tasks would go here
      return of(this.sampleTickets[index]).pipe(delay(300));
    } else {
      return new Observable<Ticket>(observer => {
        observer.error({ status: 404, message: 'Ticket not found for approval' });
        observer.complete();
      }).pipe(delay(300));
    }
  }

  rejectTicket(id: number): Observable<Ticket> {
    console.log(`Mock: Rejecting ticket ID: ${id}`);
    const index = this.sampleTickets.findIndex(t => t.id === id);
    if (index > -1) {
      this.sampleTickets[index].status = 'In Progress'; // Or revert to previous status
      this.sampleTickets[index].completedAt = undefined; // Reset completion date
      this.sampleTickets[index].approvedAt = undefined; // Reset approval date
      return of(this.sampleTickets[index]).pipe(delay(300));
    } else {
      return new Observable<Ticket>(observer => {
        observer.error({ status: 404, message: 'Ticket not found for rejection' });
        observer.complete();
      }).pipe(delay(300));
    }
  }

  // Helper methods for filtering (these will just call the main getTickets with specific params)
  getMyTickets(userId: string): Observable<Ticket[]> {
    return this.getTickets(userId);
  }

  getOverdueTickets(): Observable<Ticket[]> {
    return this.getTickets().pipe(
      delay(400),
      map(tickets => tickets.filter(t => t.isOverdue === true))
    );
  }

  getTicketsByStatus(status: string): Observable<Ticket[]> {
    return this.getTickets(undefined, status);
  }

  getTicketsByCategory(category: string): Observable<Ticket[]> {
    return this.getTickets(undefined, undefined, category);
  }
}