### ✅ What is a Module in NestJS?

- A Module is a class annotated with @Module() decorator.
- It organizes your application into logical units.
- Think of a Module as a container that groups related code (like controllers, providers, services) together.

### ✅ Why do we need Modules?

- To keep your app organized.
- To encapsulate features — each module handles a specific domain or feature.
- To manage dependencies between different parts of your app.
- To make the app scalable and maintainable.

### ✅ The basics: What’s inside a Module?

A Module is defined with the @Module() decorator, which takes an object that can have:

1. providers
2. controllers
3. imports
4. exports

### ✅ Module Dependencies (in simple words)

When Module A needs something from Module B, Module A imports Module B.
Module B exports what it wants to share.
This creates a dependency relationship.
NestJS uses this info to inject the right instances where needed.

# ⭐ Modules

A module is a class that is annotated with the @Module() decorator. This decorator provides metadata that Nest uses to organize and manage the application structure efficiently.

Modules keep your app organized. To encapsulate features — each module handles a specific domain or feature. To manage dependencies between different parts of your app. To make the app scalable and maintainable.

Every Nest application has at least one module, the root module - app.module.ts, which serves as the starting point for Nest to build the application graph. This graph is an internal structure that Nest uses to resolve relationships and dependencies between modules and providers. While small applications might only have a root module, this is generally not the case. Modules are highly recommended as an effective way to organize your components. For most applications, you'll likely have multiple modules, each encapsulating a closely related set of capabilities.

✔️ The @Module() decorator takes a single object with properties that describe the module:

➡️ Providers-

the providers that will be instantiated by the Nest injector and that may be shared at least across this module, Here you put “Services” that will share data with this module after that we can make injection.  These are the services or classes that provide functionality. Usually, these are things like services, repositories, or helpers. Providers are injectable using NestJS’s Dependency Injection system. They handle business logic.

➡️ Controllers-

the set of controllers defined in this module which have to be instantiated. Current Module will have access to “Controllers” that you put here

➡️ imports-

the list of imported modules that export the providers which are required in this module, make relationship between modules.  This is where you bring in other modules that you need to use inside this module. By importing, you can use the exported providers from those modules.

➡️ exports-

the subset of providers that are provided by this module and should be available in other modules which import this module. You can use either the provider itself or just its token (provide value).  Modules can make some of their providers available to other modules by exporting them. This means other modules that import this module can use the exported providers.

#### Shared modules

In Nest, modules are singletons by default, and thus you can share the same instance of any provider between multiple modules effortlessly

[ modules ](https://docs.nestjs.com/modules)

Now any module that imports the CatsModule has access to the CatsService and will share the same instance with all other modules that import it as well.

If we were to directly register the CatsService in every module that requires it, it would indeed work, but it would result in each module getting its own separate instance of the CatsService. This can lead to increased memory usage since multiple instances of the same service are created, and it could also cause unexpected behavior, such as state inconsistency if the service maintains any internal state.

## Here is small working example of a NestJS module with a provider and a controller

Example: Cats Module

This module will:

- Have a CatsService provider that returns a list of cats.
- Have a CatsController that defines a GET route to return cats.

```JS
🔥 //Step 0: Setup a new NestJS project (if you don’t have one)

npm i -g @nestjs/cli    //in terminal write, to install Nest JS CLI globally or locally into your project
//or -> sudo npm install -g @nestjs/cli@latest

🔥 //Run this command in your terminal to create a new NestJS project:
nest new my-app --skip-git  //this option with NO hidden .git folder. <--with NEST CLI build in command helps to create your project,
//This creates a new folder called "my-app" with all the basic files

//NestJS CLI --> it is the way to help you to generate new projects and also has a couple other extra commands and create new files for you and it will make it easier. Nest CLI contains many build in commands that helps building Back-end in Nest.js

----------------------------------------------

// There is also a CLI command that allows you to generate an entire resource along with controllers and providers all in one command

🔥 //from src folder in terminal -> if you run this command
nest g resource ninjas   //<-- will create all needed files and folders AUTOMATICALLY for ninjas -> ninjas folder, and ninjas.module.ts, ninjas.controller.ts and ninjas.service.ts files in correct locations. Name of resource = ninjas,
nest generate resource ninjas  //<-- the same command



/////////////////////////
// 👀 Separate module creation - using Nest CLI

📍 nest g module ninjas //we create new module, with the name ninjas
//or
nest generate module ninjas  //<-- the same command
//This comand will create new file in this direction (will create ninjas folder automatically)--> src/ninjas/ninjas.module.ts
//Also, will update file automatically--> src/app.module.ts our new module will be added to --> imports. starting to build that dependency tree.
//When you are creating new modules make sure it is being added to -> app.module.ts file - to imports array of another module. This is how things are registred in NEST.JS.


📍 nest g service ninjas  //<--//we create new service, with name ninjas,
// this command will create -> src/ninjas/ninjas.service.spec.ts,
// //will create -> src/ninjas/ninjas.service.ts,
// //will update -> src/ninjas/ninjas.module.ts (this will add [NinjasService] to providers), to register that service

📍 nest g controller ninjas  //<-- we create new controller, with name ninjas,
// //this command will create -> src/ninjas/ninjas.controller.spec.ts,
// //wil create -> src/ninjas/ninjas.controller.ts
//will update -> src/ninjas/ninjas.module.ts --> (this will add [NinjasController] to controllers), to register that controller
```

```JS
//Step 1: Create cats.service.ts inside src/cats/: (🔥 OR USE NEST CLI - IT WILL CREATE all needed files and folders AUTOMATICALLY)

// src/cats/cats.service.ts
import { Injectable } from '@nestjs/common';

@Injectable()  //@Injectable() means this class can be injected as a provider.
export class CatsService {
  private cats = ['Tom', 'Garfield', 'Felix'];

  findAll(): string[] {  //method findAll returning an array of cat names
    return this.cats;
  }
}
```

```JS
//Step 2: Create cats.controller.ts inside src/cats/: (🔥 OR USE NEST CLI - IT WILL CREATE all needed files and folders AUTOMATICALLY)

// src/cats/cats.controller.ts
import { Controller, Get } from '@nestjs/common';
import { CatsService } from './cats.service';

//@Controller('cats') means this controller handles routes starting with /cats.
@Controller('cats')  // Base route: /cats
export class CatsController {
  constructor(private readonly catsService: CatsService) {}

  @Get()  // GET /cats
  findAll(): string[] {   //findAll method handles GET requests to /cats.
    return this.catsService.findAll();  //It uses CatsService to get the list.
  }
}
```

```JS
//Step 3: Create cats.module.ts inside src/cats/: (🔥 OR USE NEST CLI - IT WILL CREATE all needed files and folders AUTOMATICALLY)

// src/cats/cats.module.ts
import { Module } from '@nestjs/common';
import { CatsService } from './cats.service';
import { CatsController } from './cats.controller';

@Module({   //The @Module() decorator defines this class as a NestJS module.
  providers: [CatsService],   //Declares CatsService as a provider.
  controllers: [CatsController],  //Declares CatsController as a controller.
})

export class CatsModule {}
```

```JS
//Step 4: Use CatsModule in the root module (app.module.ts)

// src/app.module.ts
import { Module } from '@nestjs/common';
import { CatsModule } from './cats/cats.module';

@Module({
  imports: [CatsModule],
})

export class AppModule {}

//The root module AppModule imports CatsModule.This tells NestJS to include everything inside CatsModule when the app runs.
```

➡️ What happens when you run this?

When you start the NestJS app, the CatsModule is loaded.
If you make a GET request to /cats, the CatsController calls CatsService.findAll().
You get a response: ["Tom", "Garfield", "Felix"].

# Summary:

- Service (CatsService): contains logic (list of cats).
- Controller (CatsController): defines routes and handles requests.
- Module (CatsModule): groups service + controller.
- Root module (AppModule): imports the CatsModule to load it.
