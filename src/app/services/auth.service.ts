import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, tap, of, delay } from 'rxjs';
import { AuthResponse, LoginRequest, RegisterRequest, User } from '../models/user.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private apiUrl = '/api/auth';
  private currentUserSubject = new BehaviorSubject<User | null>(null);
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor(private http: HttpClient) {
    this.loadUserFromStorage();
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/login`, credentials).pipe(
      tap(response => {
        this.storeAuthData(response);
        this.setCurrentUser(response);
      })
    );
  }

  register(userData: RegisterRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.apiUrl}/register`, userData).pipe(
      tap(response => {
        this.storeAuthData(response);
        this.setCurrentUser(response);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('authToken');
  }

  isParent(): boolean {
    const user = this.currentUserSubject.value;
    return user?.role === 'Parent';
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  private storeAuthData(authResponse: AuthResponse): void {
    localStorage.setItem('authToken', authResponse.token);
    localStorage.setItem('currentUser', JSON.stringify({
      id: authResponse.userId,
      email: authResponse.email,
      firstName: authResponse.firstName,
      lastName: authResponse.lastName,
      avatar: authResponse.avatar,
      role: authResponse.role
    }));
  }

  private setCurrentUser(authResponse: AuthResponse): void {
    const user: User = {
      id: authResponse.userId,
      email: authResponse.email,
      firstName: authResponse.firstName,
      lastName: authResponse.lastName,
      avatar: authResponse.avatar,
      role: authResponse.role,
      completedTasks: 0,
      totalPoints: 0,
      createdAt: new Date()
    };
    this.currentUserSubject.next(user);
  }

  private loadUserFromStorage(): void {
    const userData = localStorage.getItem('currentUser');
    if (userData) {
      try {
        const user = JSON.parse(userData);
        this.currentUserSubject.next(user);
      } catch (error) {
        console.error('Error parsing user data from storage:', error);
        this.logout();
      }
    }
  }
}

// src/app/services/auth.service.mock.ts

@Injectable({
  providedIn: 'root'
})
export class MockAuthService {
  private sampleUsers: User[] = [
    {
      id: 'user-parent-1',
      email: 'parent@example.com',
      firstName: 'Alice',
      lastName: 'Smith',
      avatar: 'https://i.pravatar.cc/150?img=1',
      role: 'Parent',
      completedTasks: 10,
      totalPoints: 500,
      createdAt: new Date('2023-01-15T10:00:00Z')
    },
    {
      id: 'user-child-1',
      email: 'child1@example.com',
      firstName: 'Bob',
      lastName: 'Smith',
      avatar: 'https://i.pravatar.cc/150?img=2',
      role: 'Child',
      completedTasks: 5,
      totalPoints: 250,
      createdAt: new Date('2023-02-20T11:00:00Z')
    },
    {
      id: 'user-child-2',
      email: 'child2@example.com',
      firstName: 'Charlie',
      lastName: 'Brown',
      avatar: 'https://i.pravatar.cc/150?img=3',
      role: 'Child',
      completedTasks: 3,
      totalPoints: 150,
      createdAt: new Date('2023-03-01T12:00:00Z')
    }
  ];

  private currentUserSubject = new BehaviorSubject<User | null>(this.loadUserFromStorage());
  public currentUser$ = this.currentUserSubject.asObservable();

  constructor() {
    // Initialize current user from stored data on service creation
    this.currentUserSubject.next(this.loadUserFromStorage());
  }

  login(credentials: LoginRequest): Observable<AuthResponse> {
    const user = this.sampleUsers.find(
      u => u.email === credentials.email
      // In a real scenario, you'd also check the password
    );

    if (user) {
      const authResponse: AuthResponse = {
        token: 'mock-jwt-token-for-' + user.id,
        userId: user.id,
        email: user.email,
        firstName: user.firstName,
        lastName: user.lastName,
        avatar: user.avatar,
        role: user.role
      };
      return of(authResponse).pipe(
        delay(500), // Simulate network delay
        tap(response => {
          this.storeAuthData(response);
          this.setCurrentUser(response);
        })
      );
    } else {
      // Simulate login failure
      return new Observable<AuthResponse>(observer => {
        observer.error({ status: 401, message: 'Invalid credentials' });
        observer.complete();
      }).pipe(delay(500));
    }
  }

  register(userData: RegisterRequest): Observable<AuthResponse> {
    const newUser: User = {
      ...userData,
      id: `user-${Math.random().toString(36).substr(2, 9)}`, // Generate a unique ID
      completedTasks: 0,
      totalPoints: 0,
      createdAt: new Date()
    };
    this.sampleUsers.push(newUser); // Add new user to our mock data

    const authResponse: AuthResponse = {
      token: 'mock-jwt-token-for-' + newUser.id,
      userId: newUser.id,
      email: newUser.email,
      firstName: newUser.firstName,
      lastName: newUser.lastName,
      avatar: newUser.avatar,
      role: newUser.role
    };

    return of(authResponse).pipe(
      delay(500), // Simulate network delay
      tap(response => {
        this.storeAuthData(response);
        this.setCurrentUser(response);
      })
    );
  }

  logout(): void {
    localStorage.removeItem('authToken');
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
    console.log('Mock: User logged out.');
  }

  isAuthenticated(): boolean {
    return !!localStorage.getItem('authToken');
  }

  isParent(): boolean {
    const user = this.currentUserSubject.value;
    return user?.role === 'Parent';
  }

  getCurrentUser(): User | null {
    return this.currentUserSubject.value;
  }

  getToken(): string | null {
    return localStorage.getItem('authToken');
  }

  private storeAuthData(authResponse: AuthResponse): void {
    localStorage.setItem('authToken', authResponse.token);
    localStorage.setItem('currentUser', JSON.stringify({
      id: authResponse.userId,
      email: authResponse.email,
      firstName: authResponse.firstName,
      lastName: authResponse.lastName,
      avatar: authResponse.avatar,
      role: authResponse.role
    }));
  }

  private setCurrentUser(authResponse: AuthResponse): void {
    const user: User = {
      id: authResponse.userId,
      email: authResponse.email,
      firstName: authResponse.firstName,
      lastName: authResponse.lastName,
      avatar: authResponse.avatar,
      role: authResponse.role,
      completedTasks: 0, // These would typically come from the backend on login
      totalPoints: 0,
      createdAt: new Date()
    };
    this.currentUserSubject.next(user);
  }

  private loadUserFromStorage(): User | null {
    const userData = localStorage.getItem('currentUser');
    if (userData) {
      try {
        return JSON.parse(userData);
      } catch (error) {
        console.error('Mock Error parsing user data from storage:', error);
        this.logout();
        return null;
      }
    }
    return null;
  }
}
