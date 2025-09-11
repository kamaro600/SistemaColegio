import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { UserService } from '../../services/user.service';




@Component({
  selector: 'app-director-users',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './director-users.component.html',
  styleUrls: ['./director-users.component.scss']
})
export class DirectorUsersComponent implements OnInit {
  users: any[] = [];
  firstName = '';
  lastName = '';
  email = '';
  role = 'student';
  id = '';

  constructor(private userService: UserService) {}

  ngOnInit() { this.load(); }

  load() {
    this.userService.getUsers().subscribe(x => this.users = x);
  }

  create() {
    const dto = {
      firstName: this.firstName,
      lastName: this.lastName,
      email: this.email,
      role: this.role,
      id : this.id
    };

    this.userService.createUser(dto).subscribe(() => {
      this.load();
      this.firstName = '';
      this.lastName = '';
      this.email = '';
      this.id = '';
    });
  }

  delete(id: string) {
    if (confirm('¿Estás seguro de que quieres eliminar este usuario?')) {
      this.userService.deleteUser(id).subscribe(() => {
        this.load();
      });
    }
  }
}