export interface Comment {
  id: number;
  content: string;
  userId: string;
  userName: string;
  userAvatar: string;
  ticketId: number;
  createdAt: Date;
}

export interface CreateCommentRequest {
  content: string;
  ticketId: number;
}
