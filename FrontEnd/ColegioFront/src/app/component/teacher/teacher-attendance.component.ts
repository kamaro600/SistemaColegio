import { Component , OnInit} from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CourseService } from '../../services/course.service';
import { UserService } from '../../services/user.service';
import { Course } from '../../models/course.model';
import { User } from '../../models/user.model';
import { Attendance } from '../../models/attendance.model'; 

@Component({
  selector: 'app-teacher-attendance',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './teacher-attendance.component.html',
  styleUrls: ['./teacher-attendance.component.scss'] 
})
export class TeacherAttendanceComponent implements OnInit{
  
 // Datos para los dropdown y la tabla de asistencia
  allCourses: Course[] = [];
  allStudents: User[] = [];
  
  // Datos del curso seleccionado
  selectedCourseId: string = '';
  studentsInCourse: User[] = [];

  // Datos para el formulario de asistencia
  attendanceRows: { studentId: string; present: boolean }[] = [];
  registeredAttendances: Attendance[] = [];
  constructor(private courseService: CourseService,
              private userService: UserService
  ) {}

  ngOnInit() {
    this.loadData();
  }

   loadData() {
    // Primero, carga todos los cursos
    this.courseService.getCourses().subscribe(courses => {
      this.allCourses = courses;
    });

    // Luego, carga todos los usuarios para filtrar los estudiantes
    this.userService.getUsers().subscribe(users => {
      this.allStudents = users.filter(user => user.role === 'student');
    });
  }
  onCourseSelected() {
    // Busca el curso seleccionado por su ID
    const course = this.allCourses.find(c => c.id === this.selectedCourseId);
    
    // Si se encuentra el curso, filtra los estudiantes
    if (course) {
      this.studentsInCourse = this.allStudents.filter(student =>
        course.studentIds.includes(student.id)
      );
    } else {
      this.studentsInCourse = [];
    }

    // Carga las asistencias registradas para el curso seleccionado
    if (this.selectedCourseId) {
      this.courseService.getAttendancesByCourse(this.selectedCourseId).subscribe(attendances => {
        this.registeredAttendances = attendances;
    });
    } else {
      this.registeredAttendances = [];
    }
    // Inicializa el arreglo de asistencias con los estudiantes del curso
    this.attendanceRows = this.studentsInCourse.map(student => ({
      studentId: student.id,
      present: true // Por defecto, todos están presentes
    }));
  }


  submit() {
    if (!this.selectedCourseId) {
      alert('Por favor, selecciona un curso.');
      return;
    }

    const payload = this.attendanceRows.map(row => ({
      studentId: row.studentId,
      date: new Date(),
      present: row.present
    }));

    this.courseService.registerAttendance(this.selectedCourseId, payload).subscribe(() => {
      alert('Asistencias registradas');
      this.attendanceRows = [];
      this.selectedCourseId = '';
      this.studentsInCourse = [];
    });
  }
}