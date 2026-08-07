import { Injectable } from '@nestjs/common';
import { User } from 'generated/prisma/client.js';
import { PrismaService } from './prisma.service.js';
//Prisma can use data types from prisma/schema.prisma
//User is a data type from prisma/schema.prisma

@Injectable()
export class AppService {
  //use PrismaService in contsructor
  constructor(private prisma: PrismaService) {}

  async getUsers(): Promise<User[]> {
    //work with imported prisma from constructor in this class-> this.prisma,
    // take model -> user and take method findMany()
    return this.prisma.user.findMany();
  }

  async createUser(name: string, email: string) {
    return this.prisma.user.create({
      data: {
        name,
        email,
      },
    });
  }

  async updateUser(id: string, name: string, email: string) {
    return this.prisma.user.update({
      where: { id }, //update user whith this id
      data: { name, email }, //update name and email
    });
  }

  deleteUser(id: string) {
    return this.prisma.user.delete({
      where: { id },
    });
  }
}
