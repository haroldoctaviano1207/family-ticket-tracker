import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TicketService } from '../../services/ticket.service';
import { AuthService } from '../../services/auth.service';
import { Ticket, TICKET_CATEGORIES, TICKET_STATUSES } from '../../models/ticket.model';

@Component({
  selector: 'app-ticket-list',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './ticket-list.component.html',
  styleUrl: './ticket-list.component.scss'
})
export class TicketListComponent implements OnInit {
  tickets: Ticket[] = [];
  filteredTickets: Ticket[] = [];
  loading = true;
  searchTerm = '';
  selectedStatus = '';
  selectedCategory = '';
  selectedAssignee = '';
  showMyTicketsOnly = false;

  categories = TICKET_CATEGORIES;
  statuses = TICKET_STATUSES;

  constructor(
    private ticketService: TicketService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.loadTickets();
  }

  loadTickets(): void {
    this.loading = true;
    this.ticketService.getTickets().subscribe({
      next: (tickets) => {
        this.tickets = tickets;
        this.applyFilters();
        this.loading = false;
      },
      error: (error) => {
        console.error('Error loading tickets:', error);
        this.loading = false;
      }
    });
  }

  applyFilters(): void {
    let filtered = [...this.tickets];

    // Search filter
    if (this.searchTerm) {
      const term = this.searchTerm.toLowerCase();
      filtered = filtered.filter(ticket => 
        ticket.title.toLowerCase().includes(term) ||
        ticket.description.toLowerCase().includes(term) ||
        ticket.assigneeName.toLowerCase().includes(term) ||
        ticket.createdByName.toLowerCase().includes(term)
      );
    }

    // Status filter
    if (this.selectedStatus) {
      filtered = filtered.filter(ticket => ticket.status === this.selectedStatus);
    }

    // Category filter
    if (this.selectedCategory) {
      filtered = filtered.filter(ticket => ticket.category === this.selectedCategory);
    }

    // Assignee filter
    if (this.selectedAssignee) {
      filtered = filtered.filter(ticket => ticket.assigneeId === this.selectedAssignee);
    }

    // My tickets only filter
    if (this.showMyTicketsOnly) {
      const currentUser = this.authService.getCurrentUser();
      filtered = filtered.filter(ticket => ticket.assigneeId === currentUser?.id);
    }

    this.filteredTickets = filtered;
  }

  onFilterChange(): void {
    this.applyFilters();
  }

  clearFilters(): void {
    this.searchTerm = '';
    this.selectedStatus = '';
    this.selectedCategory = '';
    this.selectedAssignee = '';
    this.showMyTicketsOnly = false;
    this.applyFilters();
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

  deleteTicket(ticket: Ticket): void {
    if (confirm('Are you sure you want to delete this ticket?')) {
      this.ticketService.deleteTicket(ticket.id).subscribe({
        next: () => {
          this.loadTickets();
        },
        error: (error) => {
          console.error('Error deleting ticket:', error);
        }
      });
    }
  }

  completeTicket(ticket: Ticket): void {
    this.ticketService.completeTicket(ticket.id).subscribe({
      next: () => {
        this.loadTickets();
      },
      error: (error) => {
        console.error('Error completing ticket:', error);
      }
    });
  }

  approveTicket(ticket: Ticket): void {
    this.ticketService.approveTicket(ticket.id).subscribe({
      next: () => {
        this.loadTickets();
      },
      error: (error) => {
        console.error('Error approving ticket:', error);
      }
    });
  }

  rejectTicket(ticket: Ticket): void {
    this.ticketService.rejectTicket(ticket.id).subscribe({
      next: () => {
        this.loadTickets();
      },
      error: (error) => {
        console.error('Error rejecting ticket:', error);
      }
    });
  }

  canEdit(ticket: Ticket): boolean {
    const currentUser = this.authService.getCurrentUser();
    return this.authService.isParent() || ticket.createdById === currentUser?.id;
  }

  canComplete(ticket: Ticket): boolean {
    const currentUser = this.authService.getCurrentUser();
    return ticket.assigneeId === currentUser?.id && 
           (ticket.status === 'Open' || ticket.status === 'In Progress');
  }

  canApprove(ticket: Ticket): boolean {
    return this.authService.isParent() && ticket.status === 'Pending Review';
  }

  canDelete(ticket: Ticket): boolean {
    return this.authService.isParent();
  }

  getUniqueAssignees(): { id: string, name: string }[] {
    const assignees = this.tickets.reduce((acc, ticket) => {
      if (!acc.find(a => a.id === ticket.assigneeId)) {
        acc.push({ id: ticket.assigneeId, name: ticket.assigneeName });
      }
      return acc;
    }, [] as { id: string, name: string }[]);
    
    return assignees.sort((a, b) => a.name.localeCompare(b.name));
  }
}
