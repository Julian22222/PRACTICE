import {
  Body,
  Controller,
  Delete,
  Get,
  Param,
  Post,
  Put,
} from '@nestjs/common';
import { AppService } from './app.service.js';
import { User } from 'generated/prisma/client.js';
import { get } from 'http';

@Controller()
export class AppController {
  constructor(private readonly appService: AppService) {}

  @Get()
  test() {
    return 'Server is working';
  }

  @Get('users')
  getUsers(): Promise<User[]> {
    return this.appService.getUsers();
  }

  @Get('test')
  async getUser(): Promise<User[]> {
    return this.appService.getUser();
  }

  @Post('users')
  async createUser(@Body() { name, email }: { name: string; email: string }) {
    return this.appService.createUser(name, email);
  }

  @Put('users/:id')
  async updateUser(
    @Param('id') id: string,
    @Body() { name, email }: { name: string; email: string },
  ) {
    return this.appService.updateUser(id, name, email);
  }

  @Delete('users/:id')
  async deleteUser(@Param('id') id: string) {
    return this.appService.deleteUser(id);
  }
}
