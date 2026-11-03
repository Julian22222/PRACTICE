# NestJS automatically creates test files if you use its CLI !!!

1. For Unit Tests: These are kept right next to the file they are testing. For example, users.service.ts will have its test in users.service.spec.ts

2. For End-to-End (E2E) Tests - Supertest(to check your real API): These usually live in a dedicated test/ folder at the root of your project, with files ending in .e2e-spec.ts

```JS
//loacted in
test/app.e2e-spec.ts
```

Default Runner: NestJS comes pre-configured with Jest. In Express, you typically have to set up your own test runner (like Mocha or Jest) and configuration from scratch

## There are 2 test files in each folder

```JS
For example:

//in main app folder will be
app.controller.specs.ts

//users folder will be
users.controller.spec.ts //here you can test what you sending back if user use some API/URL request
//and
users.service.spec.ts  //here you can test what you get from your DB
```

1. Use users.service.spec.ts for Business Logic
   This is where the "heavy lifting" happens. You use this file to test:

- Data transformations: Does the service correctly format data before saving it?
- Calculations: Are your math or logic rules working?
- Database interactions: Does it call the repository correctly?
- Error handling: Does it throw a NotFoundException if a user doesn't exist?

2. Use users.controller.spec.ts for API Routing & Guarding
   The controller is just a "traffic cop." You use this file to test:

- Request handling: Does the controller call the right service method?
- Response codes: Does it return a 201 Created or a 200 OK?
- Input validation: (Though often handled by DTOs/Pipes, you can test if the controller rejects bad data).
- Parameters: Does it correctly extract the @Param('id')?

Which one should you start with?

- Start with the Service test if you want to make sure your app actually works (the logic is sound).
- Start with the Controller test if you want to make sure your API endpoints are reachable and return the correct status codes.

## How to run Unit tests

1. Run only that specific file

If you want to run only the users.controller.spec.ts file without running every other test in your project, use this command in your terminal

```JS
npm test -- users.controller

//The -- tells npm to pass the following arguments directly to the underlying Jest command.

npm test users.controller
npm test -- users.controller.spec.ts
npx jest users.controller.spec.ts

```

2. Run in "Watch Mode" (Recommended for development)

If you are actively writing code, use watch mode. It will re-run the tests every time you save a file:

```JS
npm run test:watch
```

Once it starts, you can press p and type users.controller to filter and only run tests matching that filename

3. Run all tests

If you want to ensure your controller changes didn't break anything else, run the entire suite:

```JS
npm run test
```

4. Check for Coverage

If you want to see which lines of your controller are actually being tested, run:

```JS
npm run test:cov
```

This generates a report (usually in a /coverage folder) showing exactly which parts of your code were executed during the test

Common Issue: If you see an error like "Nest can't resolve dependencies of the UsersController", it means you need to mock the UsersService inside your beforeEach block in the spec file.

## ✅ Key points for testing

1. Controller returns a Promise → test must await.

```JS
//users.controller.spec.ts

it('should return all users', async () => {
   expect(await controller.findAll()).toEqual(
    [
      {
        customer_id: 1,
        first_name: 'Julian',
        last_name: 'Golovens',
        email: 'julian@test.com',
        password: '123',
        phone: '123-456-7890',
        customer_address: '123 Main St, Springfield, IL',
        dob: new Date('1995-06-15'),
      },
      {
        customer_id: 2,
        first_name: 'Tom',
        last_name: 'Simpsons',
        email: 'tomSimpson@gmail.com',
        password: '01234',
        phone: '123-456-7890',
        customer_address: '456 Main St, Springfield, IL',
        dob: new Date('1985-06-15'),
      }
    ]
   );
});
```

2. Service/mocks that controller returns a Promise → use "mockResolvedValue", otherwise use "mockReturnValue"

```JS
//users.controller.mock.ts file

export const mockUsersService = {
  findAll: jest.fn().mockReturnValue([
    {
      first_name: 'Julian',
      last_name: 'Golovens',
      email: 'julian@test.com',
      password: '123',
      phone: '123-456-7890',
      customer_address: '123 Main St, Springfield, IL',
      dob: new Date('1995-06-15'),
    },
    {
      first_name: 'Tom',
      last_name: 'Simpsons',
      email: 'tomSimpson@gmail.com',
      password: '01234',
      phone: '123-456-7890',
      customer_address: '456 Main St, Springfield, IL',
      dob: new Date('1985-06-15'),
    }
  ]),
};
```

3. Use "toEqual" -for arrays, use "toBe" for strings.

4. Jest only recognizes .ts, .js, .json by default, Therefore you need to stick with those file extentions and don't use "example.tsx"

## 🔥 Pro tip

As your project grows, you might create a structure like:

```JS
test/
  mocks/
    users.service.mock.ts
```

## ❌ BAD Practices for Unit testing, to use DB connection and real DB data (usually don't need)

- It is BAD Practice to use real DB data for Unit tests.
- Use mock data for Unit tests. For unit testing, you DO NOT need a real database.
- If you use

```JS
//I take as example my Bank/bank-api

//users.controller.ts
//This line will disconnect your API from your DataBase- bad practice. won't be able to use your API for your app. APIs will not work.
constructor(@Inject('UsersService') private usersService: any) {}
//instead of this line below
constructor(private readonly usersService: UsersService) {}
//line 17, This line allows to connect to my DB
```

API will not work. because there is no DB connection.

In Unit test in controller:

- Unit tests only check one small piece of your code in isolation.
- You don't need to connect to your real Database. You don’t care about the database.
- You don’t care about the real service implementation.
- You only care that the controller calls the service and returns what the service returns.
- Your tests shouldn’t fail because of database connections.
- Real database logic should only be tested in integration or e2e tests.

- You can just tell NestJS to use a mock without importing the real service at all.

```JS
//we keep this line, but we change constructor - I take as example my Bank/bank-api
import { UsersService } from './users.service';

//then in your controller : I take as example my Bank/bank-api
constructor(@Inject('UsersService') private usersService: any) {}
//In this case - You never touch the database. Jest doesn’t try to load any DB files.
//with this option you will not connect to your DB and your back-end API will not work !!! ❌ Bad Practice
```

See example below

```JS
//users.controller.spec.ts  - I take as example my Bank/bank-api

import { Test, TestingModule } from '@nestjs/testing';
import { UsersController } from './users.controller';
import { UsersService } from './users.service';
import { mockUsersService } from './users.controller.mock';

describe('UsersController', () => {
  let controller: UsersController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [UsersController],
      providers: [
         { provide: UsersService,
         useValue: mockUsersService } // use a token instead of importing the real service
         ],
    }).compile();

    controller = module.get<UsersController>(UsersController);
  });

  it('should return all users', async () => {

   //here you compare ...controller.spec.ts file with data from mock file data and you don't need connection to Databse
    expect(await controller.findAll()).toEqual([
      {
        first_name: 'Julian',
        last_name: 'Golovens',
        email: 'julian@test.com',
        password: '123',
        phone: '123-456-7890',
        customer_address: '123 Main St, Springfield, IL',
        dob: new Date('1995-06-15'),
      },
      {
        first_name: 'Tom',
        last_name: 'Simpsons',
        email: 'tomSimpson@gmail.com',
        password: '01234',
        phone: '123-456-7890',
        customer_address: '456 Main St, Springfield, IL',
        dob: new Date('1985-06-15'),
      },
      {
        first_name: 'Den',
        last_name: 'Jason',
        email: 'superben@gmail.com',
        password: '56789',
        phone: '123-999-7890',
        customer_address: '456 Market St, Salford, IL',
        dob: new Date('1982-08-15'),
      },
    ]
    );
  });

//if you use Data from Database - you need to close connection, if you use mock Data you don't need this afterAll block
   afterAll(async () => {
    await pool.end(); // or close(), depending on your DB
  });
});
```

```JS
//UsersService -contains -> const pool = require('../../data/dbconnection');
Jest tries to resolve this dbconnection file.
-If it doesn’t exist or Jest can’t find it, the test fails before it even runs.

-So the problem is not the mock itself, it’s that Jest loads the real service file.
```

```JS
//users.controller.ts file

import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  Put,
  Inject,
} from '@nestjs/common';
import { UsersService } from './users.service';
import { CreateUserDto } from './dto/create-user.dto';
import { UpdateUserDto } from './dto/update-user.dto';
// import { UpdateUserDto } from './dto/update-user.dto';

@Controller('users')
export class UsersController {
  // constructor(private readonly usersService: UsersService) {}  //<- this code to connect with real database

  constructor(
    @Inject('UsersService') private readonly usersService: any, // using the mock token only, without connecting to real DB
  ) {}

  @Get()
  async findAll() {
    return await this.usersService.findAll();
  }

////other code
}
```

## 🧠 Best practice (important)

You don't need to use Real Database data, use mock data

- I used this option in Bank app

```JS
//users.controller.ts line 17, This line allows to connect to my DB, you need to have this line
constructor(private readonly usersService: UsersService) {}
```

and

```JS
//users.controller.specs.ts

// 🔥 useValue: mockUsersService - this code allows to make UPDATE, PUT,PATCH,DELETE in unit test but it will not change my DB. The DB is not getting touched and any updates
providers: [{ provide: UsersService, useValue: mockUsersService }]


//change to this one to see all changes in DB what you do in Unit test
providers: [UsersService]
//❌ but this is BAD practice
```

🔥 In Unit Tests without DB connection you compare mock data file with hard coded code in spec.ts file

```JS
//users.controller.spec.ts  - will compare users.controller.spec.ts data with mock data from users.controller.mock.ts file that is hard coded code. No data will be used from Database.

//await controller.findAll() - is bringing mock data from "users.controller.mock.ts". If you have -> added - providers: [{ provide: UsersService, useValue: mockUsersService }]

//otherwise it will get data from your Database - if you use -> providers: [UsersService]

 it('should return all users', async () => {
    expect(await controller.findAll()).toEqual([   //<-- methods (such as findAll() in here and others ) in controller.spec.ts files must match methods from controller.ts file
  {
    first_name: 'Julian',
    last_name: 'Golovens',
    email: 'julian@test.com',
    password: '123',
    phone: '123-456-7890',
    customer_address: '123 Main St, Springfield, IL',
    dob: new Date('1995-06-15'),
  },
  {
    first_name: 'Tom',
    last_name: 'Simpsons',
    email: 'tomSimpson@gmail.com',
    password: '01234',
    phone: '123-456-7890',
    customer_address: '456 Main St, Springfield, IL',
    dob: new Date('1985-06-15'),
  },
  {
    first_name: 'Den',
    last_name: 'Jason',
    email: 'superben@gmail.com',
    password: '56789',
    phone: '123-999-7890',
    customer_address: '456 Market St, Salford, IL',
    dob: new Date('1982-08-15'),
  },
]);
  });


  //methods in users.controller.mock.ts file must match user.service.ts file methods

```

# End-to-End (E2E) Tests - Supertest(to check your real API)

1. code

```JS
//in test/app.e2e-spec.ts

import * as request from 'supertest';
import { Test } from '@nestjs/testing';
import { INestApplication, ValidationPipe, NotFoundException } from '@nestjs/common';
import { AppModule } from '../src/app.module';
import { UsersService } from '../src/users/users.service';

describe('UsersController (e2e)', () => {
  let app: INestApplication;

  const mockUsersService = {
    findAll: jest.fn(),
    findOne: jest.fn(),
    create: jest.fn(),
  };

  beforeAll(async () => {
    const moduleRef = await Test.createTestingModule({
      imports: [AppModule],
    })
      .overrideProvider(UsersService)
      .useValue(mockUsersService)
      .compile();

    app = moduleRef.createNestApplication();

    // enable validation (IMPORTANT for 400 tests)
    app.useGlobalPipes(new ValidationPipe());  //ValidationPipe must be enabled !!!! for class validations
    //without this line - .expect(400) will FAIL - Because validation never runs

    await app.init();
  });

  afterAll(async () => {
    await app.close();
  });

  // =========================
  // ✅ GET ALL USERS → 200
  // =========================
  it('GET /users → 200', async () => {
    mockUsersService.findAll.mockResolvedValue([
      {
        customer_id: 1,
        first_name: 'Julian',
      },
    ]);

    await request(app.getHttpServer())
      .get('/users')
      .expect(200)
      .expect([
        {
          customer_id: 1,
          first_name: 'Julian',
        },
      ]);
  });

  // =========================
  // ✅ GET ONE USER → 200
  // =========================
  it('GET /users/1 → 200', async () => {
    mockUsersService.findOne.mockResolvedValue({
      customer_id: 1,
      first_name: 'Julian',
    });

    await request(app.getHttpServer())
      .get('/users/1')
      .expect(200)
      .expect({
        customer_id: 1,
        first_name: 'Julian',
      });
  });

  // =========================
  // ❌ GET ONE USER → 404
  // =========================
  it('GET /users/999 → 404', async () => {
    mockUsersService.findOne.mockRejectedValue(
      new NotFoundException('User not found'),
    );

    await request(app.getHttpServer())
      .get('/users/999')
      .expect(404);
  });

  // =========================
  // ✅ CREATE USER → 201
  // =========================
  it('POST /users → 201', async () => {
    const newUser = {
      first_name: 'Bill',
      last_name: 'Doe',
      email: 'john.doe@example.com',
      password: 'mypassword',
      phone: '123456789',
      customer_address: 'Manchester, UK',
      dob: '1990-01-01',
    };

    mockUsersService.create.mockResolvedValue({
      customer_id: 3,
      ...newUser,
    });

    await request(app.getHttpServer())
      .post('/users')
      .send(newUser)
      .expect(201)
      .expect({
        customer_id: 3,
        ...newUser,
      });
  });

  // =========================
  // ❌ CREATE USER → 400
  // =========================
  it('POST /users → 400 (validation error)', async () => {
    await request(app.getHttpServer())
      .post('/users')
      .send({
        first_name: '', // invalid
        email: 'not-an-email', // invalid
      })
      .expect(400);
  });
});
```

2. class validations

- first install -> class-validator and class-transformer

```JS
npm install class-validator class-transformer
```

- then use it in your classes

```JS
//in your DTO classes put validations

import {
  IsDate,
  IsEmail,
  IsMobilePhone,
  IsNotEmpty,
  IsPhoneNumber,
  isPhoneNumber,
  IsStrongPassword,
} from 'class-validator';

//give validations for DTO class
export class CreateUserDto {
  @IsNotEmpty()
  first_name: string;

  @IsNotEmpty()
  last_name: string;

  @IsNotEmpty()
  @IsEmail()
  email: string;

  @IsNotEmpty()
  @IsStrongPassword()
  password: string;

  @IsNotEmpty()
  @IsPhoneNumber()
  phone: string;

  @IsNotEmpty()
  customer_address: string;

  @IsNotEmpty()
  @IsDate()
  dob: Date;
}
```

# How to run tests -supertests

🧪 1. Run ALL tests (unit + e2e)

```JS
npm test

//👉 This runs everything:
// *.spec.ts (unit tests)
// *.e2e-spec.ts (e2e tests)
```

🧪 2. Run ONLY e2e tests (recommended)

```JS
npm run test:e2e
```

🧪 4. Run a SINGLE e2e file

```JS
npx jest --config ./test/jest-e2e.json test/users.e2e-spec.ts
//users.e2e-spec.ts <-- name of the file to test
```

🧪 5. Run a specific test inside file

```JS
npx jest users.e2e-spec.ts -t "GET /users → 200"
```

🔍 6. Debug failing tests

```JS
npx jest --detectOpenHandles
```

### Optional: watch mode

```JS
//While developing:
npm run test:e2e -- --watch
```

💡 Recommended workflow

```JS
//While developing:
npm run test:e2e
```

For unit tests only:

```JS
npx jest src/users
```

# 🔄 Two types of e2e tests

🟡 Your current setup (mocked e2e):

- No DB
- Very fast ⚡
- Safe (No risk of breaking real data)
- Tests HTTP layer only
- Easy to control responses

🔵 Real e2e (advanced):

- Requires setup (test DB)
- Uses real DB, it is slower than mock e2e tests
- Inserts real data
- Slower
- More complex
- Tests full system (controller + service + DB)
- Catches real bugs 🐛
- Must clean data after tests
- Test real queries, Test real data flow

# Best practice to use BOTH - mock and real BD e2e test, for different purposes.

## 🧪 Example of real DB test

⚠️ Use test database !!! for real DB tests, NOT your real one !!!!

```JS
it('should create user in real DB', async () => {
  const newUser = { ... };

  const response = await request(app.getHttpServer())
    .post('/users')
    .send(newUser)
    .expect(201);

  expect(response.body.email).toBe(newUser.email);
});

//Clean DB after tests
afterEach(async () => {
  await pool.query('DELETE FROM users');
});
```

## 🧪 Code explanation - Supertest (mock example)

- In example below - No real Database data is used, We use mock data

```JS
//from Bank/bank-api/test/users.e2e-spec.ts file - test users API

import request from 'supertest';
import { Test, TestingModule } from '@nestjs/testing';
import {
  INestApplication,
  ValidationPipe,
  NotFoundException,
} from '@nestjs/common';
import { AppModule } from '../src/app.module';
import { UsersService } from '../src/users/users.service';


//describe what you are going to perform, All tests for the users API will go inside this block
describe('UsersController (e2e)', () => {


  //app will hold our NestJS application instance.
  // INestApplication is the type provided by NestJS.
  //We use app to simulate real HTTP requests (like hitting /users).
  let app: INestApplication;


  //We create a mock service to replace the real UsersService.
  //jest.fn() creates a mock function.
  //This ensures our e2e test does not hit the real database.
  const mockUsersService = {
    findAll: jest.fn(),
    findOne: jest.fn(),
    create: jest.fn(),
  };


  //This code runs once before all tests in this suite.
  //Good place to set up the app and mock dependencies.
  beforeAll(async () => {

    //Creates a NestJS testing module (like a mini app for testing).
    //AppModule is imported, so all routes/controllers/services are available.
    const moduleRef = await Test.createTestingModule({
      imports: [AppModule],
    })
      .overrideProvider(UsersService)
      .useValue(mockUsersService)
      .compile();
      //Override the real UsersService with our mock version.
      //useValue(mockUsersService) tells NestJS to use our fake service instead of the real one.
      //.compile() builds the testing module so it’s ready to use.


    //Converts the testing module into a real NestJS app instance.
    //We can now send HTTP requests to this instance in our tests.
    app = moduleRef.createNestApplication();

    // enable validation - that we used in DTO in users folder (IMPORTANT for 400 tests)
    // app.useGlobalPipes(new ValidationPipe());
    //the same as code above but it is better to use this one
    app.useGlobalPipes(
      new ValidationPipe({
        whitelist: true, // removes unknown fields
        forbidNonWhitelisted: true, // throws error for extra fields
      }),
    );


    //Initializes the NestJS application.
    //Must be done before sending any HTTP requests.
    //Think of it like starting the server, but in memory for testing.
    await app.init();
  });

  beforeEach(() => {
    jest.clearAllMocks();
  });


  //Runs once after all tests.
  //Closes the app and frees resources.
  //Prevents Jest from hanging or leaving open handles.
  afterAll(async () => {
    await app.close();
  });

  // =========================
  // ✅ GET ALL USERS → 200
  // =========================
  //Defines a single test case.
  it('GET /users → 200', async () => {
    const mockData = [
      {
        customer_id: 1,
        first_name: 'Julian',
        last_name: 'Golovens',
        email: 'julian@test.com',
        password: '123',
        phone: '123-456-7890',
        customer_address: '123 Main St, Springfield, IL',
        dob: '1995-06-15',
      },
      {
        customer_id: 2,
        first_name: 'Tom',
        last_name: 'Simpsons',
        email: 'tomSimpson@gmail.com',
        password: '01234',
        phone: '123-456-7890',
        customer_address: '456 Main St, Springfield, IL',
        dob: '1985-06-15',
      },
    ];


    //Sets up the mock service to return a value when findAll() is called.
    //mockResolvedValue is used because findAll() is async.
    //This avoids hitting the real database.
    mockUsersService.findAll.mockResolvedValue(mockData);


    //request(app.getHttpServer()) comes from supertest.
    //Simulates an HTTP GET request to /users.
    //.expect(200) checks that the HTTP response status code is 200.
    await request(app.getHttpServer())
      .get('/users')
      .expect(200)
      .expect(mockData);
  });

  //All together it ensures that your controller + route + DTOs return the correct data format.
  });
```

```JS
//there is 2 the same block, from example above:
//difference between them is:

//OPTION 1
app.useGlobalPipes(new ValidationPipe());

👉 What it does:

Validates DTO using class-validator
If data is invalid → returns 400 Bad Request

❗ What it DOES NOT do
❌ Does NOT remove extra fields
❌ Does NOT block unknown fields
❌ Does NOT convert types

//Example of this code block:
{
  "first_name": "Julian",
  "email": "test@test.com",
  "hacker_field": "I should not be here"
}

👉 This will PASS ❌
👉 hacker_field stays in the request

/////////////////////////////////////////
//OPTION 2
//Always use this option
app.useGlobalPipes(
  new ValidationPipe({
    whitelist: true,
    forbidNonWhitelisted: true,
    transform: true,
    }),
    );

------

✅ whitelist: true
👉 Removes properties NOT in DTO

//For example:
//DTO:
first_name: string;
email: string;

//Request:
{
  "first_name": "Julian",
  "email": "test@test.com",
  "hacker_field": "bad"
}

//👉 Result:
{
  "first_name": "Julian",
  "email": "test@test.com"
}
//👉 hacker_field is removed automatically

------

//🚫 forbidNonWhitelisted: true
//👉 Instead of removing, it throws error

//Same request:
{
  "first_name": "Julian",
  "email": "test@test.com",
  "hacker_field": "bad"
}

//👉 Result:
400 Bad Request
"hacker_field should not exist"

-----

//🔄 transform: true
//👉 Automatically converts types

//Example WITHOUT transform:
@Get(':id')
findOne(@Param('id') id: number)

//Request:
GET /users/1

👉 id is:
"1" // string ❌

----
//WITH transform:
id === 1 // number ✅
```

#### 🧠 Side-by-side comparison of these 2 options above

```JS
| Feature             | `ValidationPipe()` | Advanced config            |
| ------------------- | ------------------ | -------------------------- |
| Validate DTO        | ✅                  | ✅                          |
| Remove extra fields | ❌                  | ✅ (`whitelist`)            |
| Block extra fields  | ❌                  | ✅ (`forbidNonWhitelisted`) |
| Convert types       | ❌                  | ✅ (`transform`)            |
| Security            | ❌ weak             | ✅ strong                   |
```

🔥 Real-world impact.
❌ Without options

- Users can send extra data
- Possible security risks
- Manual type conversion needed

✅ With options

- Clean input ✔
- Safe API ✔
- Automatic type handling ✔
