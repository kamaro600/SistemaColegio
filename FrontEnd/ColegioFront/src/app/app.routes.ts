import { Routes } from '@angular/router';

import { DirectorUsersComponent } from './component/director/director-users.component';
import { DirectorCoursesComponent } from './component/director/director-courses.component';
import { TeacherAttendanceComponent } from './component/teacher/teacher-attendance.component';
import { StudentCoursesComponent } from './component/student/student-courses.component';

export const routes: Routes = [
  { path: '', redirectTo: '/director-users', pathMatch: 'full' },
  { path: 'director-users', component: DirectorUsersComponent },
  { path: 'director-courses', component: DirectorCoursesComponent },
  { path: 'teacher-attendance', component: TeacherAttendanceComponent },
  { path: 'student-courses', component: StudentCoursesComponent }
];