import { Injectable } from '@angular/core';
import {InMemoryDbService} from 'angular-in-memory-web-api'

@Injectable({
  providedIn: 'root'
})
export class InMemoryData implements InMemoryDbService {
  
    createDb() {

         const movies = [
         
                 {id: 1, name:"Shawshank Redemption",description:"A banker convicted of uxoricide forms a friendship over a quarter century with a hardened convict, while maintaining his innocence and trying to remain hopeful through simple compassion.",imageUrl:"1.jpg"},
                 {id: 2, name:"Fight Club",description:"An insomniac office worker and a devil-may-care soap maker form an underground fight club that evolves into much more.",imageUrl:"2.jpg"},
                 {id: 3, name:"Gladiator",description:"A former Roman General sets out to exact vengeance against the corrupt emperor who murdered his family and sent him into slavery.",imageUrl:"3.jpg"},
                 {id: 4, name:"Green Mile",description:"A death row guard learns that a gentle giant in his charge possesses a mysterious gift.",imageUrl:"4.jpg"},
                 {id: 5, name:"Inception",description:"A troubled thief who steals secrets from people's dreams will take on a dangerous mission in his latest job, planting an idea in his target's subconscious.",imageUrl:"5.jpg"},
                 {id: 6, name:"Interstellar",description:"When Earth becomes uninhabitable in the future, a farmer and ex-NASA pilot, Joseph Cooper, is tasked to pilot a spacecraft, along with a team of researchers, to find a new planet for humans.",imageUrl:"6.jpg"},
                 {id: 7, name:"The Dark Knight",description:"When a menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman, James Gordon and Harvey Dent must work together to put an end to the madness.",imageUrl:"7.jpg"},
                 {id: 8, name:"V For Vendetta",description:"In a future British dystopian society, a shadowy freedom fighter, known only by the alias of 'V', plots to overthrow the tyrannical government - with the help of a young woman.",imageUrl:"8.jpg"},
                 {id: 9, name:"The Prestige",description:"Rival 19th-century magicians engage in a bitter battle for trade secrets.",imageUrl:"9.jpg"}
         
         ];

        return {movies}; //Obje olarak geriye döndürdüğü için "{}" var

    }

}
