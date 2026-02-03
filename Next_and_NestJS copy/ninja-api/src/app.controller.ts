import { Controller, Get } from '@nestjs/common';
import { AppService } from './app.service';

@Controller() //Decorator that marks the class as a NestJS controller. Controllers are responsible for handling incoming HTTP requests and returning responses to the client. The empty parentheses indicate that this controller will handle requests to the root path of the application.
export class AppController {
  constructor(private readonly appService: AppService) {} //The constructor is using dependency injection to inject an instance of the AppService into the controller. The private readonly keywords indicate that this property is private to the class and cannot be modified after initialization. This allows the controller to use the methods defined in the AppService.

  @Get() //Decorator that marks the method as a handler for HTTP GET requests. When a GET request is made to the root path ("/"), this method will be invoked.
  getHello(): string {
    return this.appService.getHello(); //Calls the getHello method of the AppService to retrieve a string message. The result of this method will be sent back as the HTTP response.
    //use method -> getHello() from AppService to return "Hello World!" string when a GET request is made to the root path ("/").
  }
}
