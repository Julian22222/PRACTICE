import { Injectable } from '@nestjs/common';
import { CreateNinjaDto } from './dto/create-ninja.dto';
import { UpdateNinjaDto } from './dto/update-ninja.dto';

@Injectable() //Injectable decorator, NEST behind the scenes allow us to inject this service into other parts of the application, such as controllers or other services.
export class NinjasService {
  //This is our local data storage for ninjas
  private ninjas = [
    { id: 1, name: 'Naruto Uzumaki', rank: 'Genin', weapon: 'stars' },
    { id: 2, name: 'Sasuke Uchiha', rank: 'Genin', weapon: 'nunchucks' },
    { id: 3, name: 'Sakura Haruno', rank: 'Chunin', weapon: 'sward' },
  ];

  //method to return ninjas, allowing optionally filter by weapon
  getNinjas(weapon?: 'stars' | 'nunchucks' | 'sward') {
    if (weapon) {
      //if weapon is provided, filter ninjas by weapon
      return this.ninjas.filter((ninja) => ninja.weapon === weapon);
    }

    return this.ninjas; //if no weapon is provided, return all ninjas
  }

  getNinjaById(id: number) {
    const ninja = this.ninjas.find((ninja) => ninja.id === id);

    //if ninja with the given id is not found, throw an error, handling error in controller
    if (!ninja) {
      // return { message: `Ninja with id ${id} not found` };
      throw new Error(`Ninja with id ${id} not found`); //will return to controller - this message, "error": "Not Found","statusCode": 404
    }

    //if found, return the ninja
    return ninja;
  }

  // createNinja(ninja: { name: string; rank: string; weapon: string }) {
  // ///some code
  // }
  createNinja(ninja: CreateNinjaDto) {
    const newNinja = {
      //create a new ninja object
      id: this.ninjas.length + 1,
      ...ninja,
    };

    this.ninjas.push(newNinja);

    return newNinja;
  }

  //updateNinjaDto variable has optional properties from UpdateNinjaDto
  updateNinja(id: number, updateNinjaDto: UpdateNinjaDto) {
    //assign new values to the ninja with the given id
    //assign the updated properties from updateNinjaDto to the existing ninja object
    this.ninjas = this.ninjas.map((ninja) => {
      //we map original ninjas array to create a new array with updated ninja
      //we are mapping through the ninjas array to find the ninja with the given id
      if (ninja.id === id) {
        return { ...ninja, ...updateNinjaDto }; //overwriting existing ninja properties with the new values from updateNinjaDto
      }

      return ninja; //return the original ninja if id does not match
    });

    return this.getNinjaById(id); //return the updated ninja
  }

  removeNinja(id: number) {
    const toBeRemoved = this.getNinjaById(id); //check if ninja exists

    this.ninjas = this.ninjas.filter((ninja) => ninja.id !== id); //remove ninja by filtering. assign a new array without the removed ninja

    return toBeRemoved; //return the removed ninja
  }
}
