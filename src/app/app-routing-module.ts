import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MoviesComponent } from './movies/movies.component';
import { Dashboard } from './dashboard/dashboard';
import { MovieDetail } from './movie-detail/movie-detail';

const routes: Routes = [

  
  {path: '', redirectTo:'/dashboard', pathMatch:'full'},
  {path:'dashboard', component:Dashboard},
  {path:'movies', component:MoviesComponent},
  {path:'detail/:id', component:MovieDetail}


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
