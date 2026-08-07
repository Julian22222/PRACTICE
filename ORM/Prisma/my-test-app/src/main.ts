import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module.js';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  // app.enableCors({
  //   origin: ['http://localhost:3000', 'http://localhost:3001'], // Next.js frontend
  //   methods: 'GET,POST,PUT,DELETE',
  //   credentials: true, //Without this → cookies will NEVER be sent
  // });

  try {
    await app.listen(process.env.PORT ?? 3002);
    console.log(`Server is running on port ${process.env.PORT ?? 3002}`);
  } catch (error) {
    console.error('Error starting server:', error);
    process.exit(1);
  }
}
bootstrap();
