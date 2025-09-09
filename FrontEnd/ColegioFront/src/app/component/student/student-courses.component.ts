import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-student-courses',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './student-courses.component.html',
  styleUrls: ['./student-courses.component.scss']
})
export class StudentCoursesComponent {
  studentId = '';
  courses: any[] = [];

  constructor(private courseService: CourseService) {}

  load() {
    this.courseService.getCourses().subscribe(all => {
      this.courses = all.filter((c: any) =>
        c.studentIds.includes(this.studentId)
      );
    });
  }
}