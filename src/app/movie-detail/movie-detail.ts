import { Component,Input} from '@angular/core';
import { Movie } from '../movie';

@Component({
  selector: 'movie-detail',
  standalone: false,
  templateUrl: './movie-detail.html',
  styleUrl: './movie-detail.css'
})



export class MovieDetail {

   @Input() movie?:Movie  
 

}
