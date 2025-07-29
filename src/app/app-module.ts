import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import {MoviesComponent} from './movies/movies.component';
import { Movie } from './movie/movie'
import { FormsModule } from '@angular/forms';
import { MovieDetail } from './movie-detail/movie-detail';



@NgModule({
  declarations: [
    App,
    MoviesComponent,
    Movie,
    MovieDetail
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule { }
