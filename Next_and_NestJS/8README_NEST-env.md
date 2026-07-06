# Manage Secrets from .env in Nest.js

### In Nest.js you can use basic approach ->

```JS
//to get access to .env secrets,
// you use on the top of each file ->
require('dotenv').config() //in each file where you want to use process.env

//and then you can use
process.env.DB //for example

//This approach works the same way as in express.js. But try to avoid this approach.
```

# There is Better way to get access to .env secrets in NEST.JS

## ConfigModule is the recommended NestJS approach.

```JS
//Now you don't need to use ->  require('dotenv').config(); on the top of the each file to use process.env.

//Bank/bank-api/src/app.module.ts

import { ConfigModule } from '@nestjs/config';

@Module({
  imports: [
    //add ConfigModule
    ConfigModule.forRoot({
      isGlobal: true,
    }),

    UsersModule,
   //other Modules
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
```

# What ConfigModule.forRoot() does

When your application starts, NestJS loads your .env file and populates process.env.

- Don't need to userequire('dotenv').config()
- ConfigModule.forRoot() is imported in AppModule
- AppModule imports DatabaseModule
- the .env file is in the project root (or you've configured envFilePath)

# To use process.env is acceptable, but NestJS best practice is to inject ConfigService instead of reading process.env directly.

```JS
//Instead of getting the value of .env secret key this way:
process.env.DB_HOST

------------------------------------

//do this:

import { ConfigService } from '@nestjs/config';

{
  provide: PG_POOL,
  inject: [ConfigService],
  useFactory: (configService: ConfigService) => {
    return new Pool({
      host: configService.get<string>('DB_HOST'),
      user: configService.get<string>('DB_USER'),
      password: configService.get<string>('DB_PASSWORD'),
      database: configService.get<string>('DB_DATABASE'),
      port: configService.get<number>('DB_PORT'),
    });
  },
}

// This approach has several advantages:

// ✅ Easier to test (you can mock ConfigService)
// ✅ Centralized configuration
// ✅ Supports validation
// ✅ Better integration with NestJS dependency injection
// ✅ Doesn't rely on global state (process.env)
```

```JS
//Even better: validate your environment variables
//A common production setup is:

ConfigModule.forRoot({
  isGlobal: true,
  validationSchema: Joi.object({
    DB_HOST: Joi.string().required(),
    DB_PORT: Joi.number().required(),
    DB_USER: Joi.string().required(),
    DB_PASSWORD: Joi.string().required(),
    DB_DATABASE: Joi.string().required(),
  }),
});
```

### Recommendation

I would rank the approaches like this:

- ✅ Best practice: ConfigModule + ConfigService (recommended)
- ✅ ConfigModule + process.env (works fine, but less idiomatic in NestJS)
- ❌ require('dotenv').config() scattered throughout modules (avoid)

---

# The Flow how Nest.js app is loading and working with secrets

How Nest.js loading secrets from .env file in development environment and loading secrets from "AWS Parameter Store" in Production environment

The app start running from main file in Nest.js -> Bank/bank-api/src/main.ts

- Local development uses .env
- production uses AWS Parameter Store

### main.ts file has a logic that is switching between these 2 options.

# 🫩 Development Flow

```JS
//When the application starts in Development:

node dist/src/main.js //dist folder is used in production, dist folder is created when - npm run build(transcribe TS to JS)
        │
        ▼
AppModule is created  //Fist Nest.js module
        │
        ▼
ConfigModule.forRoot() //in app.module.ts - in Development, helps to get process.env values from .env -> process.env is filled by ConfigModule
        │
        ▼
    Reads .env  //Get values from .env
        │
        ▼
Copies variables into process.env  //Now process.env has all values from .env file

// So before your DatabaseModule is created:
process.env.DB_HOST  //is assigned before DatabaseModule is created

// Then your database pool is created:
new Pool({
host: process.env.DB_HOST,
})
// which connects to your local PostgreSQL - that is written in .env file
```

```JS
//✅ Development
//.env file

DB_HOST=localhost
DB_PORT=5432
DB_DATABASE=bank
DB_USER=postgres
DB_PASSWORD=password
JWT_SECRET=my-local-secret

USE_AWS_PARAMETER_STORE=false     //<- this is the main factor that uses main.ts file to understand from where get the secrets – from .env or AWS Parameter store
NODE_ENV //<--or it can be any other KEY in .env file
```

```JS
//Bank/bank-api/src/app.module.ts contains->

// ConfigModule.forRoot({
// isGlobal: true,
// });


import { ConfigModule } from '@nestjs/config';

@Module({
  imports: [
    ConfigModule.forRoot({  //in Development - process.env is filled by ConfigModule block
      isGlobal: true,
    }),

    UsersModule,
    //oter modules
  ],
  controllers: [AppController],
  providers: [AppService],
})
export class AppModule {}
```

# 🫩 Production Flow

```JS
////When the application starts in Production:

dist/src/main.js //main file where application starts
        │
        ▼
AWS Parameter Store //secrets are sored in AWS arameter Store
        │
        ▼
loadParameters()  //Get these secrets from AWS Parameter Store
        │
        ▼
    process.env  //assign these secrets to process.env
        │
        ▼
NestFactory.create(AppModule)  //start building first App module
        │
        ▼
DatabaseModule
```

```JS
//✅ Production

//your application starts from -> (in main Bank folder)
ecosystem.config.js file  //which has USE_AWS_PARAMETER_STORE: "true", OR other KEY //<- this is the main factor that uses main.ts file to understand from where get the secrets – from .env or AWS Parameter store

pm2 start ecosystem.config.js  //<- to start our Production server in AWS EC2

//ecosystem.config.js contains these variables
env: {
  NODE_ENV: "production",
  USE_AWS_PARAMETER_STORE: "true",   //this is the main factor that uses main.ts file to understand from where get the secrets – from .env or AWS Parameter store
  PORT: 3005,
}
```

1. Step 1

- You use ecosystem.config.js which start executing -> Main.js file (dist/src/main.js)

2. STEP 2

- in main.js file you have a logic

```JS
const useAWS = process.env.USE_AWS_PARAMETER_STORE === 'true';

if (useAWS) {
    //in Production, helps to get process.env values from "AWS Parameter Store"
await loadParameters(); //load environment variables from AWS Parameter Store
}
```

3. STEP 3

```JS
//if USE_AWS_PARAMETER_STORE === 'true' -> use await loadParameters();-> and  load secrets from AWS Parameter store

//then you call AWS ->
GetParameterCommand({
Name: 'DB_HOST'
})

//AWS returns->  bank-production.xxxxx.eu-west-1.rds.amazonaws.com
// Then you do ->
process.env.DB_HOST =
response.Parameter.Value;

// You repeat this for
DB_USER
DB_PASSWORD
DB_DATABASE
JWT_SECRET

// After the loop finishes, process.env now contains all values
DB_HOST=bank-production...
DB_USER=admin
DB_PASSWORD=*******
JWT_SECRET=*******

// Exactly as if they came from .env.
```

4. STEP 4

This STEP very important.

```JS
//After you have all process.env assigned
// Only after that you can create Nest

const app = await NestFactory.create(AppModule);


// At this moment
process.env
// already contains:
DB_HOST
DB_USER
DB_PASSWORD

// because you loaded them from AWS first.
// Then Nest builds your modules.
```

5. STEP 5

```JS
// Your DatabaseModule

useFactory: () => {
    console.log(process.env.DB_HOST);

    return new Pool({
        host: process.env.DB_HOST,
    });
}

// now sees
process.env.DB_HOST

// because loadParameters() already populated it.
// So your code doesn't care where the value came from
// It simply reads: process.env.DB_HOST
```

## Why await loadParameters() is before NestFactory.create()

```JS
//main.ts
////This is the critical part.

const useAWS = process.env.NODE_ENV === 'production';
if (useAWS) {
await loadParameters(); //we load environment variables from AWS Parameter Store First ->load process.env before create Nest.js project
}

const app = await NestFactory.create(AppModule); //Then we start running app

------------------------------

//This guarantees:
 AWS
  ↓
process.env
  ↓
AppModule
  ↓
DatabaseModule
```

```JS
//❌ If you reversed them
const app = await NestFactory.create(AppModule);
await loadParameters();

//then DatabaseModule would execute first:
DatabaseModule

//tries
process.env.DB_HOST === undefined
//and PostgreSQL would fail to connect
```

# 🫩 Full Flow - all together (Development + Production)

```JS
dist/src/main.js
    │
    ▼
await loadParameters();  //in main.js file IF USE_AWS_PARAMETER_STORE === 'true' -> get process.env from AWS Parameter Store (in Production),
// IF USE_AWS_PARAMETER_STORE === 'false' no process.env assigned but flow is further
    │
    ▼
AppModule is created
    │
    ▼
ConfigModule.forRoot()  //in app.module.ts file, IF USE_AWS_PARAMETER_STORE === 'false' -> process.env are assigned from from .env file (in Development)
    │
    ▼
Reads .env
    │
    ▼
Copies variables into process.env
```

# why in app.module.ts the .process.env are not reassigned in Production?

```JS
//In Production
IF USE_AWS_PARAMETER_STORE === 'true' //then await loadParameters(); -> // process.env are assigned from AWS Parameter Store

//the flow going down and why process.env are not reassigning with -> ConfigModule.forRoot()
```

##### what ConfigModule.forRoot() actually does and whether a .env file exists in production?

```JS
await loadParameters();
    │
    ▼
const app = await NestFactory.create(AppModule);
//When NestFactory.create(AppModule) runs, Nest creates AppModule, which imports:
// ConfigModule.forRoot({
//   isGlobal: true,
// });

// At that point, process.env already contains the values you loaded from AWS:
// DB_HOST = production-db.amazonaws.com
// DB_USER = admin
```

##### What does ConfigModule.forRoot() do?

```JS
//ConfigModule.forRoot() uses dotenv internally.
//dotenv loads variables from a .env file (->ConfigModule.forRoot()) only if they are not already defined in process.env.

//For example, suppose your production server has:
process.env.DB_HOST = production-db.amazonaws.com

//and your .env file contains:
DB_HOST=localhost

//dotenv does not overwrite the existing value by default.
//So after loading:
//Before dotenv:
DB_HOST = production-db.amazonaws.com

//After dotenv:
DB_HOST = production-db.amazonaws.com

//The value stays the same.
```

#### Why?

```JS
//Internally, dotenv behaves like this:

if (process.env.DB_HOST === undefined) {
    process.env.DB_HOST = valueFromDotEnv;  //process.env assigned from .env local file
}

//It does not do:
process.env.DB_HOST = valueFromDotEnv;

//unless you explicitly enable overriding.
```

###### Could .env overwrite AWS values?

```JS
//Yes, but only if you explicitly enable it:

ConfigModule.forRoot({
  isGlobal: true,
  expandVariables: true,
  // or through dotenv:
  // override: true
});

//or by using dotenv.config({ override: true }).
```
