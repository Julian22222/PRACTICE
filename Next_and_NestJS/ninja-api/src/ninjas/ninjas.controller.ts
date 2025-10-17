import { Controller, Delete, Get, Param, Post, Put } from '@nestjs/common';

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

  //GET /ninjas --> []
  @Get() // This decorator defines a method that will handle GET requests to the 'ninjas' path.
  getNinjas() {
    //method for a current controller
    return ['ninja1', 'ninja2', 'ninja3'];
  }

  //GET /ninjas/:id --> {...}
  @Get(':id') // This decorator defines a method that will handle GET requests to the 'ninjas/:id' path, where ':id' is a route parameter.
  getNinjaById(@Param('id') id: string) {
    //method for a current controller
    return { id: id, name: 'ninja' + id };
  }

  //POST /ninjas --> {...}
  @Post() // This decorator defines a method that will handle POST requests to the 'ninjas' path.
  createNinja() {
    //method for a current controller
    return {};
  }

  //PUT  /ninjas/:id --> {...}
  @Put(':id') // This decorator defines a method that will handle PUT requests to the 'ninjas/:id' path, where ':id' is a route parameter.
  updateNinja() {
    //method for a current controller
    return {};
  }

  //DELETE /ninjas/:id
  @Delete(':id') // This decorator defines a method that will handle DELETE requests to the 'ninjas/:id' path, where ':id' is a route parameter.
  deleteNinja() {
    //method for a current controller
    return {};
  }
}
