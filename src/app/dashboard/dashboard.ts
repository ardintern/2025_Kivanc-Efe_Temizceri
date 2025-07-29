import { Component } from '@angular/core';
import { MovieService } from '../movie.service';
import { Movie } from '../movie';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

  movies: Movie[] = [];
  movieLength!: number;

  constructor (private movieService: MovieService) {}

   
  ngOnInit() {

      this.getMovies();

  }  



  getMovies(): void {

    this.movieService.getMovies()
                  .subscribe(movies=> {
                    this.movies = movies.slice(0,5);
                    this.movieLength = movies.length;
                  })

  }


}
