import { Component } from '@angular/core';
import { LoggingService } from '../logging.service';

@Component({
  selector: 'logging',
  standalone: false,
  templateUrl: './logging.html',
  styleUrl: './logging.css'
})
export class Logging {

   constructor(public loggingService: LoggingService) {}

   ngOnInit() {


   }

}
