import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TicketService } from '../../services/ticket.service';
import { ApiService } from '../../services/api.service';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.model';
import { Ticket, TICKET_CATEGORIES, TICKET_PRIORITIES } from '../../models/ticket.model';

@Component({
  selector: 'app-ticket-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './ticket-form.component.html',
  styleUrl: './ticket-form.component.scss'
})
export class TicketFormComponent implements OnInit {
  ticketForm: FormGroup;
  isEditMode = false;
  ticketId: number | null = null;
  loading = false;
  saving = false;
  errorMessage = '';
  familyMembers: User[] = [];
  categories = TICKET_CATEGORIES;
  priorities = TICKET_PRIORITIES;

  constructor(
    private fb: FormBuilder,
    private ticketService: TicketService,
    private apiService: ApiService,
    private authService: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.ticketForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(200)]],
      description: ['', [Validators.maxLength(1000)]],
      assigneeId: ['', Validators.required],
      dueDate: [''],
      priority: ['Medium', Validators.required],
      category: ['General', Validators.required],
      photoUrl: ['', [Validators.pattern(/^https?:\/\/.+/)]]
    });
  }

  ngOnInit(): void {
    this.loadFamilyMembers();
    this.checkEditMode();
  }

  loadFamilyMembers(): void {
    this.apiService.getUsers().subscribe({
      next: (users) => {
        this.familyMembers = users;
      },
      error: (error) => {
        console.error('Error loading family members:', error);
      }
    });
  }

  checkEditMode(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.ticketId = +id;
      this.loadTicket();
    }
  }

  loadTicket(): void {
    if (this.ticketId) {
      this.loading = true;
      this.ticketService.getTicket(this.ticketId).subscribe({
        next: (ticket) => {
          this.populateForm(ticket);
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

  populateForm(ticket: Ticket): void {
    const dueDate = ticket.dueDate ? new Date(ticket.dueDate).toISOString().split('T')[0] : '';
    
    this.ticketForm.patchValue({
      title: ticket.title,
      description: ticket.description,
      assigneeId: ticket.assigneeId,
      dueDate: dueDate,
      priority: ticket.priority,
      category: ticket.category,
      photoUrl: ticket.photoUrl || ''
    });
  }

  onSubmit(): void {
    if (this.ticketForm.valid && !this.saving) {
      this.saving = true;
      this.errorMessage = '';

      const formData = { ...this.ticketForm.value };
      
      // Convert date string to Date object if provided
      if (formData.dueDate) {
        formData.dueDate = new Date(formData.dueDate);
      } else {
        formData.dueDate = null;
      }

      // Remove empty photoUrl
      if (!formData.photoUrl) {
        delete formData.photoUrl;
      }

      if (this.isEditMode && this.ticketId) {
        this.updateTicket(formData);
      } else {
        this.createTicket(formData);
      }
    }
  }

  createTicket(ticketData: any): void {
    this.ticketService.createTicket(ticketData).subscribe({
      next: (ticket) => {
        this.router.navigate(['/tickets', ticket.id]);
      },
      error: (error) => {
        console.error('Error creating ticket:', error);
        this.errorMessage = error.error?.message || 'Failed to create ticket';
        this.saving = false;
      }
    });
  }

  updateTicket(ticketData: any): void {
    if (this.ticketId) {
      this.ticketService.updateTicket(this.ticketId, ticketData).subscribe({
        next: (ticket) => {
          this.router.navigate(['/tickets', ticket.id]);
        },
        error: (error) => {
          console.error('Error updating ticket:', error);
          this.errorMessage = error.error?.message || 'Failed to update ticket';
          this.saving = false;
        }
      });
    }
  }

  onCancel(): void {
    if (this.isEditMode && this.ticketId) {
      this.router.navigate(['/tickets', this.ticketId]);
    } else {
      this.router.navigate(['/tickets']);
    }
  }

  getFieldError(fieldName: string): string {
    const field = this.ticketForm.get(fieldName);
    if (field && field.errors && field.touched) {
      if (field.errors['required']) return `${fieldName} is required`;
      if (field.errors['maxlength']) return `${fieldName} is too long`;
      if (field.errors['pattern']) return `Please enter a valid URL`;
    }
    return '';
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.ticketForm.get(fieldName);
    return !!(field && field.errors && field.touched);
  }

  canEdit(): boolean {
    return this.authService.isParent() || !this.isEditMode;
  }

  getAssigneeAvatar(assigneeId: string): string {
    const member = this.familyMembers.find(m => m.id === assigneeId);
    return member?.avatar || '👤';
  }

  getDateToday(): string {
    return new Date().toISOString().split('T')[0];
  }
}
