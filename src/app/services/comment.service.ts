import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { delay, Observable, of } from 'rxjs';
import { Comment, CreateCommentRequest } from '../models/comment.model';

@Injectable({
  providedIn: 'root'
})
export class CommentService {
  private apiUrl = '/api/comment';

  constructor(private http: HttpClient) {}

  getComments(ticketId: number): Observable<Comment[]> {
    return this.http.get<Comment[]>(`${this.apiUrl}/ticket/${ticketId}`);
  }

  createComment(commentRequest: CreateCommentRequest): Observable<Comment> {
    return this.http.post<Comment>(this.apiUrl, commentRequest);
  }

  deleteComment(commentId: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${commentId}`);
  }
}

// src/app/services/comment.service.mock.ts

@Injectable({
  providedIn: 'root'
})
export class MockCommentService {
  private sampleComments: Comment[] = [
    {
      id: 1,
      content: 'Great job on this task!',
      userId: 'user-101',
      userName: 'John Doe',
      userAvatar: 'https://i.pravatar.cc/150?img=4',
      ticketId: 1,
      createdAt: new Date('2024-07-20T11:00:00Z')
    },
    {
      id: 2,
      content: 'I need some help with step 3.',
      userId: 'user-103',
      userName: 'Peter Jones',
      userAvatar: 'https://i.pravatar.cc/150?img=6',
      ticketId: 1,
      createdAt: new Date('2024-07-20T14:30:00Z')
    },
    {
      id: 3,
      content: 'Looks good! Ready for review.',
      userId: 'user-102',
      userName: 'Jane Smith',
      userAvatar: 'https://i.pravatar.cc/150?img=5',
      ticketId: 2,
      createdAt: new Date('2024-07-21T09:00:00Z')
    },
    {
      id: 4,
      content: 'Dont forget to sweep under the rug!',
      userId: 'user-102',
      userName: 'Jane Smith',
      userAvatar: 'https://i.pravatar.cc/150?img=5',
      ticketId: 3,
      createdAt: new Date('2024-07-22T16:00:00Z')
    },
    {
      id: 5,
      content: 'On it!',
      userId: 'user-104',
      userName: 'Sara Williams',
      userAvatar: 'https://i.pravatar.cc/150?img=7',
      ticketId: 3,
      createdAt: new Date('2024-07-22T16:05:00Z')
    }
  ];

  constructor() { }

  getComments(ticketId: number): Observable<Comment[]> {
    console.log(`Mock: Fetching comments for ticket ID: ${ticketId}`);
    const commentsForTicket = this.sampleComments.filter(comment => comment.ticketId === ticketId);
    return of(commentsForTicket).pipe(delay(300));
  }

  createComment(commentRequest: CreateCommentRequest): Observable<Comment> {
    console.log('Mock: Creating comment:', commentRequest);
    const newComment: Comment = {
      id: Math.max(...this.sampleComments.map(c => c.id)) + 1, // Simple ID generation
      content: commentRequest.content,
      ticketId: commentRequest.ticketId,
      userId: 'user-mock-current', // Replace with a dynamic mock user ID if needed
      userName: 'Mock User',
      userAvatar: 'https://i.pravatar.cc/150?img=8',
      createdAt: new Date()
    };
    this.sampleComments.push(newComment);
    return of(newComment).pipe(delay(300));
  }

  deleteComment(commentId: number): Observable<void> {
    console.log(`Mock: Deleting comment with ID: ${commentId}`);
    const initialLength = this.sampleComments.length;
    this.sampleComments = this.sampleComments.filter(comment => comment.id !== commentId);
    if (this.sampleComments.length < initialLength) {
      return of(void 0).pipe(delay(200)); // Return void for success
    } else {
      return new Observable<void>(observer => {
        observer.error({ status: 404, message: 'Comment not found for deletion' });
        observer.complete();
      }).pipe(delay(200));
    }
  }
}