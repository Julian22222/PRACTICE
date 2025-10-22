import {
  Body,
  Controller,
  Delete,
  Get,
  NotFoundException,
  Param,
  Post,
  Put,
  Query,
} from '@nestjs/common';
import { CreateNinjaDto } from './dto/create-ninja.dto';
import { UpdateNinjaDto } from './dto/update-ninja.dto';
import { NinjasService } from './ninjas.service';

// The @Controller('ninjas') decorator marks this class as a NestJS controller that will handle requests to the 'ninjas' path. This means that any HTTP requests made to '/ninjas' will be routed to this controller for handling.
//Currently, the controller does not have any methods defined to handle specific HTTP requests (like GET, POST, etc.), but it serves as a placeholder for future implementations related to 'ninjas'.

//if you want to make a GET request to /ninjas, you would define a method in this controller with the @Get() decorator, like so:
// GET  /ninjas  -->  use this decorator -> @Get() and we will get all ninjas as an array []
// GET  /ninjas/:id  --> use this decorator -> @Get(':id') , it is taking in a id aparameter. and we will get a single ninja by id as an object {...}
// POST /ninjas  --> use this decorator -> @Post() and we will create a new ninja and return the created ninja as an object {...}
// PUT  /ninjas/:id  --> use this decorator -> @Put(':id') and we will update an existing ninja by id and return the updated ninja as an object {...}
// DELETE /ninjas/:id  --> use this decorator -> @Delete(':id') and we will delete an existing ninja by id and return a success message or the deleted ninja as an object {...}

@Controller('ninjas') // This decorator defines a controller that will handle requests to the 'ninjas' path.
export class NinjasController {
  //
  constructor(public readonly _ninjasServer: NinjasService) {}
  ////GET /ninjas --> []
  ////method GET for a current controller
  // @Get() // This decorator defines a method that will handle GET requests to the 'ninjas' path.
  // getNinjas() {
  //   return ['ninja1', 'ninja2', 'ninja3'];
  // }

  ////GET /ninjas?weapon=... --> [...]
  @Get() // This decorator defines a method that will handle GET requests to the 'ninjas' path.
  getNinjas(@Query('weapon') weapon: 'stars' | 'nunchucks' | 'sward') {
    //The @Query('weapon') decorator is used to extract the value of the 'weapon' query parameter from the incoming request URL and bind it to the weapon parameter in the getNinjas method.
    //If a client makes a GET request to /ninjas?weapon=stars, the value of weapon == 'stars'.

    return this._ninjasServer.getNinjas(weapon); //invoking getNinjas method from NinjasService to get ninjas, optionally filtered by weapon
  }

  //geting value from query parameters
  @Get('search') // This decorator defines a method that will handle GET requests to the 'ninjas/search' path.
  searchNinjas(@Query('type') type: string) {
    return [{ type }];
  }

  //GET /ninjas/:id --> {...}
  //method GET for a current controller
  @Get(':id') // This decorator defines a method that will handle GET requests to the 'ninjas/:id' path, where ':id' is a route parameter.
  getOneNinja(@Param('id') id: string) {
    // return { id: id, name: 'ninja' + id }; // Return an object with the id, No service used here

    //try-catch block to handle potential errors when getting a ninja by id
    try {
      return this._ninjasServer.getNinjaById(Number(id)); //invoking getNinjaById method from NinjasService to get a ninja by id
      //turn string to number use +id --> return this._ninjasServer.getNinjaById(+id);
    } catch (err) {
      //error is received from the ninjas.services method when ninja with the given id is not found
      //getNinjaById method in ninjas.service.ts file throws an error if the ninja is not found

      // throw new Error(error.message);
      throw new NotFoundException(err.message);
    }
  }

  //POST /ninjas --> {...}
  //method POST for a current controller
  @Post() // This decorator defines a method that will handle POST requests to the 'ninjas' path.
  createNinja(@Body() createNinjaDto1: CreateNinjaDto) {
    //The @Body() decorator is used to extract the body of the request and bind it to the createNinjaDto1 parameter.
    //createNinjaDto1 is the parameter that will hold the data sent in the request body.
    //CreateNinjaDto is a Data Transfer Object (DTO) class that defines the structure of the data type expected in the request body when creating a new ninja.
    //createNinjaDto1 is an instance of CreateNinjaDto class that contains the data sent in the request body.

    return this._ninjasServer.createNinja(createNinjaDto1); //invoking createNinja method from NinjasService to create a new ninja

    // return {
    //   name: createNinjaDto1.name,
    // };
  }

  //PUT  /ninjas/:id --> {...}
  //method PUT for a current controller
  @Put(':id') // This decorator defines a method that will handle PUT requests to the 'ninjas/:id' path, where ':id' is a route parameter.
  updateNinja(@Param('id') id: string, @Body() updateNinjaDto: UpdateNinjaDto) {
    // return this._ninjasServer.updateNinja(+id, updateNinjaDto);
    //the same as above
    return this._ninjasServer.updateNinja(Number(id), updateNinjaDto); //invoking updateNinja method from NinjasService to update an existing ninja by id

    // return {
    //   id,
    //   name: updateNinjaDto,
    // };
  }

  //DELETE /ninjas/:id
  //method DELETE for a current controller
  @Delete(':id') // This decorator defines a method that will handle DELETE requests to the 'ninjas/:id' path, where ':id' is a route parameter.
  deleteNinja(@Param('id') id: string) {
    //return this._ninjasServer.removeNinja(+id);
    //the same as above
    return this._ninjasServer.removeNinja(Number(id)); //invoking deleteNinja method from NinjasService to delete an existing ninja by id
  }
}
