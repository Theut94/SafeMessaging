import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../../environments/environment';

@Injectable({
  providedIn: 'root',
})
export class MessageService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) {}

  getChat(Token: string, SelectedUserGuid: string): Observable<IChat> {
    return this.http.get<IChat>(this.apiUrl + '/chat/getChat', {
      headers: { Authorization: Token },
      params: { TargetUser: SelectedUserGuid },
    });
  }

  // we do not expect to get anything back from the server, a status code is enough.
  sendMessage(message: IMessage, GUID: string): Observable<any> {
    return this.http.post<any>(this.apiUrl + '/chat/sendMessage', {
      ...message,
      GUID,
    });
  }
}

export interface IMessage {
  text: string;
  IV: string;
  GUID?: string;
  sender: string;
}

export interface IChat {
  GUID: string;
  messages: IMessage[];
}
