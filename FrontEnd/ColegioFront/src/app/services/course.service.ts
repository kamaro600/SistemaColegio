import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Course } from '../models/course.model';

@Injectable({
  providedIn: 'root'
})
export class CourseService {
  private baseUrl = 'https://localhost:5086/api';

  constructor(private http: HttpClient) {}

  // Crear curso
  createCourse(payload: any): Observable<Course> {
    return this.http.post<Course>(`${this.baseUrl}/director/courses`, payload);
  }

  // Obtener todos los cursos
  getCourses(): Observable<Course[]> {
    return this.http.get<Course[]>(`${this.baseUrl}/director/courses`);
  }

  // Obtener todos los cursos por studiante
  getCoursesByStudent(studentId: string): Observable<Course[]> {
    return this.http.get<Course[]>(`${this.baseUrl}/student/${studentId}/courses`);
  }
  //obtener los registros de asistencia por curso
  getAttendancesByCourse(courseId: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/teacher/${courseId}/attendances`);
  }
  // Registrar asistencia (para docentes)
  registerAttendance(courseId: string, attendances: any[]): Observable<any> {
    return this.http.post(
      `${this.baseUrl}/teacher/${courseId}/attendance`,
      attendances
    );
  }

  deleteCourse(id: string): Observable<any> {
    return this.http.delete(`${this.baseUrl}/director/courses/${id}`);
  }
}