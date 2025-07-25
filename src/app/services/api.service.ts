import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { delay, Observable, of } from 'rxjs';
import { User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = '/api';

  constructor(private http: HttpClient) {}

  // User-related API calls
  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(`${this.apiUrl}/user`);
  }

  getUser(id: string): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/user/${id}`);
  }

  updateUser(id: string, userData: Partial<User>): Observable<User> {
    return this.http.put<User>(`${this.apiUrl}/user/${id}`, userData);
  }

  // Dashboard statistics
  getDashboardStats(): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/dashboard/stats`);
  }

  // Notification-related API calls
  getNotifications(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/notifications`);
  }

  markNotificationAsRead(id: number): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/notifications/${id}/read`, {});
  }
}

// src/app/services/api.service.mock.ts

@Injectable({
  providedIn: 'root'
})
export class MockApiService {

  private sampleUsers: User[] = [
    {
      id: 'user-101',
      email: 'john.doe@example.com',
      firstName: 'John',
      lastName: 'Doe',
      avatar: 'https://i.pravatar.cc/150?img=4',
      role: 'Child',
      completedTasks: 12,
      totalPoints: 600,
      createdAt: new Date('2023-01-01T09:00:00Z')
    },
    {
      id: 'user-102',
      email: 'jane.smith@example.com',
      firstName: 'Jane',
      lastName: 'Smith',
      avatar: 'https://i.pravatar.cc/150?img=5',
      role: 'Parent',
      completedTasks: 0,
      totalPoints: 0,
      createdAt: new Date('2023-01-10T10:30:00Z')
    },
    {
      id: 'user-103',
      email: 'peter.jones@example.com',
      firstName: 'Peter',
      lastName: 'Jones',
      avatar: 'https://i.pravatar.cc/150?img=6',
      role: 'Child',
      completedTasks: 8,
      totalPoints: 400,
      createdAt: new Date('2023-02-05T14:15:00Z')
    },
    {
      id: 'user-104',
      email: 'sara.williams@example.com',
      firstName: 'Sara',
      lastName: 'Williams',
      avatar: 'https://i.pravatar.cc/150?img=7',
      role: 'Child',
      completedTasks: 15,
      totalPoints: 750,
      createdAt: new Date('2023-03-01T11:00:00Z')
    }
  ];

  private sampleNotifications: any[] = [
    { id: 1, message: 'New task assigned: "Take out the trash"', read: false, createdAt: new Date('2024-07-20T10:00:00Z') },
    { id: 2, message: 'Your "Clean your room" task was approved', read: true, createdAt: new Date('2024-07-19T15:30:00Z') },
    { id: 3, message: 'Parent approved your "Homework Help" request', read: false, createdAt: new Date('2024-07-22T08:45:00Z') },
    { id: 4, message: 'New comment on "Wash the car" task', read: false, createdAt: new Date('2024-07-23T11:20:00Z') }
  ];

  constructor() { }

  // User-related API calls
  getUsers(): Observable<User[]> {
    console.log('Mock: Fetching all users.');
    return of(this.sampleUsers).pipe(delay(300));
  }

  getUser(id: string): Observable<User> {
    console.log(`Mock: Fetching user with ID: ${id}`);
    const user = this.sampleUsers.find(u => u.id === id);
    if (user) {
      return of(user).pipe(delay(300));
    } else {
      return new Observable<User>(observer => {
        observer.error({ status: 404, message: 'User not found' });
        observer.complete();
      }).pipe(delay(300));
    }
  }

  updateUser(id: string, userData: Partial<User>): Observable<User> {
    console.log(`Mock: Updating user with ID: ${id}`, userData);
    const index = this.sampleUsers.findIndex(u => u.id === id);
    if (index > -1) {
      this.sampleUsers[index] = { ...this.sampleUsers[index], ...userData };
      return of(this.sampleUsers[index]).pipe(delay(300));
    } else {
      return new Observable<User>(observer => {
        observer.error({ status: 404, message: 'User not found for update' });
        observer.complete();
      }).pipe(delay(300));
    }
  }

  // Dashboard statistics
  getDashboardStats(): Observable<any> {
    console.log('Mock: Fetching dashboard statistics.');
    const stats = {
      totalUsers: this.sampleUsers.length,
      activeTasks: 7,
      completedTasksToday: 3,
      pendingApprovals: 2,
      totalPointsEarned: 1500
    };
    return of(stats).pipe(delay(400));
  }

  // Notification-related API calls
  getNotifications(): Observable<any[]> {
    console.log('Mock: Fetching notifications.');
    return of(this.sampleNotifications).pipe(delay(300));
  }

  markNotificationAsRead(id: number): Observable<void> {
    console.log(`Mock: Marking notification ID ${id} as read.`);
    const notification = this.sampleNotifications.find(n => n.id === id);
    if (notification) {
      notification.read = true;
      return of(void 0).pipe(delay(200)); // Return void for success
    } else {
      return new Observable<void>(observer => {
        observer.error({ status: 404, message: 'Notification not found' });
        observer.complete();
      }).pipe(delay(200));
    }
  }
}
