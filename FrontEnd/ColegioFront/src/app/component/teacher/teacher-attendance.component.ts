import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-teacher-attendance',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './teacher-attendance.component.html',
  styleUrls: ['./teacher-attendance.component.scss'] 
})
export class TeacherAttendanceComponent {
  courseId = '';
  rows: any[] = [{ studentId: '', present: true }];

  constructor(private courseService: CourseService) {}

  addRow() {
    this.rows.push({ studentId: '', present: true });
  }

  submit() {
    const payload = this.rows.map(r => ({
      studentId: r.studentId,
      date: new Date(),
      present: r.present
    }));

    this.courseService.registerAttendance(this.courseId, payload).subscribe(
      () => {
        alert('Asistencias registradas');
        this.rows = [{ studentId: '', present: true }];
      }
    );
  }
}