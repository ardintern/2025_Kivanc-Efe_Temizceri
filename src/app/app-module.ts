import { NgModule, provideBrowserGlobalErrorListeners } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing-module';
import { App } from './app';
import {MoviesComponent} from './movies/movies.component';
import { Movie } from './movie/movie'
import { FormsModule } from '@angular/forms';
import { MovieDetail } from './movie-detail/movie-detail';
import { Logging } from './logging/logging';
import { Navbar } from './navbar/navbar';
import { Dashboard } from './dashboard/dashboard';
import { HttpClientModule } from '@angular/common/http';
import { HttpClientInMemoryWebApiModule } from 'angular-in-memory-web-api';
import { InMemoryData } from './in-memory-data.service';



@NgModule({
  declarations: [
    App,
    MoviesComponent,
    Movie,
    MovieDetail,
    Logging,
    Navbar,
    Dashboard
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    HttpClientInMemoryWebApiModule.forRoot(

      InMemoryData, {dataEncapsulation: false}

    )
    
  ],
  providers: [
    provideBrowserGlobalErrorListeners()
  ],
  bootstrap: [App]
})
export class AppModule { }
