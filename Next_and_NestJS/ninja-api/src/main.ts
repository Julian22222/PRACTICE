import { NestFactory } from '@nestjs/core';
import { AppModule } from './app.module';

async function bootstrap() {
  const app = await NestFactory.create(AppModule); //create the NestJS application using the root module (AppModule). This sets up the application context and prepares it to handle incoming requests. AppModule is the root module of the application, which typically imports and configures other modules, controllers, and providers.
  await app.listen(process.env.PORT ?? 3001); //will run app on port 3001 or from environment variable PORT
}
bootstrap();
