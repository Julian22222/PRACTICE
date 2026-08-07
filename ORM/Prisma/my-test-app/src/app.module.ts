import { Module } from '@nestjs/common';
import { AppController } from './app.controller.js';
import { AppService } from './app.service.js';
import { PrismaService } from './prisma.service.js';
import { ConfigModule } from '@nestjs/config'; //npm install @nestjs/config

@Module({
  //use ConfigModule.forRoot() to load environment variables from .env file, instead of using require('dotenv').config()
  imports: [
    ConfigModule.forRoot({
      isGlobal: true,
    }),
  ],
  controllers: [AppController],
  providers: [AppService, PrismaService],
})
export class AppModule {}
