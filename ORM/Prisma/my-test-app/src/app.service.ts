import { Injectable } from '@nestjs/common';
import { User } from 'generated/prisma/client.js';
import { PrismaService } from './prisma.service.js';
//Prisma can use data types from prisma/schema.prisma
//User is a data type from prisma/schema.prisma

@Injectable()
export class AppService {
  //use PrismaService in contsructor
  constructor(private readonly prisma: PrismaService) {}

  async getUsers(): Promise<User[]> {
    //work with imported prisma from constructor in this class-> this.prisma,
    // get data from table -> user and use method findMany()
    return this.prisma.user.findMany();
  }

  async getUser() {
    //for example we want to find all user which have Alice name in database, search is case insensitive, so it will find Alice, alice, ALICE, etc
    const name = 'Alice';
    return this.prisma.user.findMany({
      //you can use include to include related data, for example if you want to include all posts of this user, you can use include: { posts: true }
      include: {
        posts: true, //include all posts of this user
      },
      // orderBy: {
      //   createdAt: 'desc', //order by createdAt field in descending order, so the latest user will be first
      // },
      where: {
        name: { contains: name, mode: 'insensitive' }, //find all users which have Alice name in database,mode 'insensitive' means that it will find Alice, alice, ALICE, etc
      },
    });
  }

  async getUserTest() {
    const name = 'Alice';
    return this.prisma.user.findMany({
      include: {
        posts: {
          //use select to select only title of posts, instead of all data of posts, for example if you want to select only title of posts, you can use select: { title: true }
          select: {
            title: true,

            //or use - where to filter posts, for example if you want to filter posts that have title that contains 'Hello' in it, case insensitive, you can use where: { title: { contains: 'Hello', mode: 'insensitive' } }
            //it will return posts that have title that contains 'Hello' in it, case insensitive
            //posts:{
            //where: {
            //  title: { contains: 'Hello', mode: 'insensitive' },
            //}
            //},
          },
        },
      },
    });
  }

  async createUser(name: string, email: string) {
    return this.prisma.user.create({
      data: {
        name,
        email,
        //this will include posts data as well
        //post is a relation to Post model, so we can create a post for this user, for example we can create a post with title 'Hello World' and content 'This is my first post'
        posts: {
          create: {
            title: 'Hello World',
            content: 'This is my first post',
          },
        },
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

  async transactions() {
    //this is an example of how to use transactions in Prisma, you can use transactions to execute multiple queries in a single transaction, if one of the queries fails, all queries will be rolled back
    const [newUser, newPost] = await this.prisma.$transaction([
      //create a new user
      this.prisma.user.create({
        data: {
          name: 'Bob',
          email: 'bob@example.com',
        },
      }),
      //create a new post for the new user
      this.prisma.post.create({
        data: {
          title: 'My First Post',
          content: 'This is the content of my first post',
          authorId: 'user-id', // Replace with the actual user ID
        },
      }),
    ]);
  }

  sqlRequest() {
    //this is an example of how to use raw SQL queries in Prisma, you can use raw SQL queries to execute any SQL query, for example if you want to select all users from the database, you can use this.prisma.$queryRaw`SELECT * FROM users`
    return this.prisma.$queryRaw`SELECT * FROM users`; //in quotes you can use any SQL query, for example if you want to select all users from the database, you can use this.prisma.$queryRaw`SELECT * FROM users`
  }

  // //return this.prisma,user.findUnique({where: {id}}) to find user by id, if user is not found, it will return null
  //findUnique -is a method that will return a single user, if user is not found, it will return null
  //findFirst -is a method that will return the first user that matches the where condition, if user is not found, it will return null

  // async getUserById(id: string): Promise<User | null> {
  //   return this.prisma.user.findUnique({
  //     where: { email: 'user@example.com' }, //find user by email, if user is not found, it will return null
  //   });
}
