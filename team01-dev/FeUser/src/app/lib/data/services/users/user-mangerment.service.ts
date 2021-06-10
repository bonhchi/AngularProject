import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { UserDataReturnDTOModel } from '../../models/users/user.model';

@Injectable({
  providedIn: 'root',
})
export class UserMangermentService {
  private subject: BehaviorSubject<UserDataReturnDTOModel>;
  constructor() {
    var user = JSON.parse(localStorage.getItem('user'));
    this.subject = new BehaviorSubject<UserDataReturnDTOModel>(
      user ? user : null
    );
  }

  get UserSubject() {
    return this.subject;
  }

  UpdateUser(user: UserDataReturnDTOModel) {
    this.subject.next(user);
  }
}
