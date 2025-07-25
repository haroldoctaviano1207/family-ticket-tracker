import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, ActivatedRoute, Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { TicketService } from '../../services/ticket.service';
import { CommentService } from '../../services/comment.service';
import { AuthService } from '../../services/auth.service';
import { Ticket } from '../../models/ticket.model';
import { Comment } from '../../models/comment.model';
import { User } from '../../models/user.model';

@Component({
  selector: 'app-ticket-detail',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './ticket-detail.component.html',
  styleUrl: './ticket-detail.component.scss'
})
export class TicketDetailComponent implements OnInit {
  ticket: Ticket | null = null;
  comments: Comment[] = [];
  currentUser: User | null = null;
  loading = true;
  loadingComments = false;
  newComment = '';
  submittingComment = false;
  errorMessage = '';

  constructor(
    public router: Router,
    private route: ActivatedRoute,
    private ticketService: TicketService,
    private commentService: CommentService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    this.currentUser = this.authService.getCurrentUser();
    this.loadTicket();
  }

  loadTicket(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.loading = true;
      this.ticketService.getTicket(+id).subscribe({
        next: (ticket) => {
          this.ticket = ticket;
          this.loadComments();
          this.loading = false;
        },
        error: (error) => {
          console.error('Error loading ticket:', error);
          this.errorMessage = 'Failed to load ticket details';
          this.loading = false;
        }
      });
    }
  }

  loadComments(): void {
    if (this.ticket) {
      this.loadingComments = true;
      this.commentService.getComments(this.ticket.id).subscribe({
        next: (comments) => {
          this.comments = comments;
          this.loadingComments = false;
        },
        error: (error) => {
          console.error('Error loading comments:', error);
          this.loadingComments = false;
        }
      });
    }
  }

  addComment(): void {
    if (this.newComment.trim() && this.ticket && !this.submittingComment) {
      this.submittingComment = true;
      this.commentService.createComment({
        content: this.newComment.trim(),
        ticketId: this.ticket.id
      }).subscribe({
        next: (comment) => {
          this.comments.push(comment);
          this.newComment = '';
          this.submittingComment = false;
        },
        error: (error) => {
          console.error('Error adding comment:', error);
          this.submittingComment = false;
        }
      });
    }
  }

  completeTicket(): void {
    if (this.ticket) {
      this.ticketService.completeTicket(this.ticket.id).subscribe({
        next: (updatedTicket) => {
          this.ticket = updatedTicket;
        },
        error: (error) => {
          console.error('Error completing ticket:', error);
        }
      });
    }
  }

  approveTicket(): void {
    if (this.ticket) {
      this.ticketService.approveTicket(this.ticket.id).subscribe({
        next: (updatedTicket) => {
          this.ticket = updatedTicket;
        },
        error: (error) => {
          console.error('Error approving ticket:', error);
        }
      });
    }
  }

  rejectTicket(): void {
    if (this.ticket) {
      this.ticketService.rejectTicket(this.ticket.id).subscribe({
        next: (updatedTicket) => {
          this.ticket = updatedTicket;
        },
        error: (error) => {
          console.error('Error rejecting ticket:', error);
        }
      });
    }
  }

  deleteTicket(): void {
    if (this.ticket && confirm('Are you sure you want to delete this ticket?')) {
      this.ticketService.deleteTicket(this.ticket.id).subscribe({
        next: () => {
          this.router.navigate(['/tickets']);
        },
        error: (error) => {
          console.error('Error deleting ticket:', error);
        }
      });
    }
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
    return new Date(date).toLocaleDateString('en-US', {
      year: 'numeric',
      month: 'short',
      day: 'numeric',
      hour: '2-digit',
      minute: '2-digit'
    });
  }

  formatDateShort(date: Date): string {
    return new Date(date).toLocaleDateString();
  }

  isOverdue(): boolean {
    return this.ticket?.dueDate ? 
      new Date(this.ticket.dueDate) < new Date() && this.ticket.status !== 'Completed' : 
      false;
  }

  canEdit(): boolean {
    return this.authService.isParent() || 
           this.ticket?.createdById === this.currentUser?.id;
  }

  canComplete(): boolean {
    return this.ticket?.assigneeId === this.currentUser?.id && 
           (this.ticket?.status === 'Open' || this.ticket?.status === 'In Progress');
  }

  canApprove(): boolean {
    return this.authService.isParent() && this.ticket?.status === 'Pending Review';
  }

  canDelete(): boolean {
    return this.authService.isParent();
  }

  canAddComment(): boolean {
    return this.ticket?.status !== 'Completed' && this.ticket?.status !== 'Cancelled';
  }

  getProgressPercentage(): number {
    if (!this.ticket) return 0;
    
    switch (this.ticket.status) {
      case 'Open': return 0;
      case 'In Progress': return 50;
      case 'Pending Review': return 80;
      case 'Completed': return 100;
      default: return 0;
    }
  }

  getProgressColor(): string {
    const progress = this.getProgressPercentage();
    if (progress === 100) return 'success';
    if (progress >= 80) return 'info';
    if (progress >= 50) return 'warning';
    return 'primary';
  }
}
