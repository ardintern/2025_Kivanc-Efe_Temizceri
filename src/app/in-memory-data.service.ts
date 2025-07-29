import { Injectable } from '@angular/core';
import {InMemoryDbService} from 'angular-in-memory-web-api'

@Injectable({
  providedIn: 'root'
})
export class InMemoryData implements InMemoryDbService {
  
    createDb() {

         const movies = [
         
                 {id: 1, name:"movie 1",description:"güzel film",imageUrl:"1.jpg"},
                 {id: 2, name:"movie 2",description:"güzel film",imageUrl:"2.jpg"},
                 {id: 3, name:"movie 3",description:"güzel film",imageUrl:"3.jpg"},
                 {id: 4, name:"movie 4",description:"güzel film",imageUrl:"4.jpg"},
                 {id: 5, name:"movie 5",description:"güzel film",imageUrl:"5.jpg"},
                 {id: 6, name:"movie 6",description:"güzel film",imageUrl:"6.jpg"},
                 {id: 7, name:"movie 7",description:"güzel film",imageUrl:"7.jpg"},
                 {id: 8, name:"movie 8",description:"güzel film",imageUrl:"8.jpg"},
                 {id: 9, name:"movie 9",description:"güzel film",imageUrl:"9.jpg"}
         
         ];

        return {movies}; //Obje olarak geriye döndürdüğü için "{}" var

    }

}
