import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { Observable, forkJoin, map } from 'rxjs';
import { AuthService } from '../../services/auth.service';
import { TicketService } from '../../services/ticket.service';
import { ApiService } from '../../services/api.service';
import { Ticket } from '../../models/ticket.model';
import { User } from '../../models/user.model';

interface DashboardStats {
  totalTickets: number;
  completedTickets: number;
  overdueTickets: number;
  pendingReview: number;
  myTickets: number;
  completionRate: number;
}

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss'
})
export class DashboardComponent implements OnInit {
  currentUser: User | null = null;
  myTickets: Ticket[] = [];
  recentTickets: Ticket[] = [];
  familyMembers: User[] = [];
  stats: DashboardStats = {
    totalTickets: 0,
    completedTickets: 0,
    overdueTickets: 0,
    pendingReview: 0,
    myTickets: 0,
    completionRate: 0
  };
  loading = true;

  constructor(
    private authService: AuthService,
    private ticketService: TicketService,
    private apiService: ApiService
  ) {}

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
    this.loadDashboardData();
  }

  loadDashboardData(): void {
    this.loading = true;
    
    const requests = [
      this.ticketService.getTickets(),
      this.apiService.getUsers()
    ];

    // forkJoin(requests).subscribe({
    //   next: ([tickets, users]) => {
    //     this.calculateStats(tickets);
    //     this.familyMembers = users;
    //     this.myTickets = tickets.filter(t => t.assigneeId === this.currentUser?.id);
    //     this.recentTickets = tickets.slice(0, 5);
    //     this.loading = false;
    //   },
    //   error: (error) => {
    //     console.error('Error loading dashboard data:', error);
    //     this.loading = false;
    //   }
    // }); TBD
  }

  calculateStats(tickets: Ticket[]): void {
    const myTickets = tickets.filter(t => t.assigneeId === this.currentUser?.id);
    const completed = tickets.filter(t => t.status === 'Completed');
    const overdue = tickets.filter(t => t.isOverdue);
    const pendingReview = tickets.filter(t => t.status === 'Pending Review');

    this.stats = {
      totalTickets: tickets.length,
      completedTickets: completed.length,
      overdueTickets: overdue.length,
      pendingReview: pendingReview.length,
      myTickets: myTickets.length,
      completionRate: tickets.length > 0 ? Math.round((completed.length / tickets.length) * 100) : 0
    };
  }

  getStatusClass(status: string): string {
    switch (status) {
      case 'Open': return 'status-open';
      case 'In Progress': return 'status-in-progress';
      case 'Pending Review': return 'status-pending-review';
      case 'Completed': return 'status-completed';
      case 'Cancelled': return 'status-cancelled';
      default: return 'status-open';
    }
  }

  getPriorityClass(priority: string): string {
    switch (priority) {
      case 'High': return 'priority-high';
      case 'Medium': return 'priority-medium';
      case 'Low': return 'priority-low';
      default: return 'priority-medium';
    }
  }

  formatDate(date: Date): string {
    return new Date(date).toLocaleDateString();
  }

  isOverdue(ticket: Ticket): boolean {
    return ticket.dueDate ? new Date(ticket.dueDate) < new Date() && ticket.status !== 'Completed' : false;
  }

  completeTicket(ticket: Ticket): void {
    this.ticketService.completeTicket(ticket.id).subscribe({
      next: () => {
        this.loadDashboardData();
      },
      error: (error) => {
        console.error('Error completing ticket:', error);
      }
    });
  }

  approveTicket(ticket: Ticket): void {
    this.ticketService.approveTicket(ticket.id).subscribe({
      next: () => {
        this.loadDashboardData();
      },
      error: (error) => {
        console.error('Error approving ticket:', error);
      }
    });
  }

  rejectTicket(ticket: Ticket): void {
    this.ticketService.rejectTicket(ticket.id).subscribe({
      next: () => {
        this.loadDashboardData();
      },
      error: (error) => {
        console.error('Error rejecting ticket:', error);
      }
    });
  }

  isParent(): boolean {
    return this.authService.isParent();
  }
}
