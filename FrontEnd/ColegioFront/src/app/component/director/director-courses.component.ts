import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CourseService } from '../../services/course.service';
import { UserService } from '../../services/user.service';

@Component({
  selector: 'app-director-courses',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './director-courses.component.html',
  styleUrls: ['./director-courses.component.scss']
})
export class DirectorCoursesComponent implements OnInit {
  name = '';
  schedule = '';
  teacherId = '';
  students: any[] = [];
  teachers: any[] = [];
  courses: any[] = [];
  selectedStudents = new Set<string>();

  constructor(
    private courseService: CourseService,
    private userService: UserService
  ) {}

  ngOnInit() {
    this.load();
  }

  load() {
    this.courseService.getCourses().subscribe(x => (this.courses = x));
    this.userService.getUsers().subscribe(u => {
      this.students = u.filter((z: any) => z.role === 'student');
      this.teachers = u.filter((z: any) => z.role === 'teacher');
    });
  }

  toggleStudent(id: string, ev: any) {
    if (ev.target.checked) this.selectedStudents.add(id);
    else this.selectedStudents.delete(id);
  }

  create() {
    const dto = {
      name: this.name,
      schedule: this.schedule,
      teacherId: this.teacherId || null,
      studentIds: Array.from(this.selectedStudents)
    };

    this.courseService.createCourse(dto).subscribe(() => {
      this.load();
      this.name = '';
      this.schedule = '';
      this.selectedStudents.clear();
    });
  }
}