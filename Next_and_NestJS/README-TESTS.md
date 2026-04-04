# NestJS automatically creates test files if you use its CLI !!!

- Unit Tests: These are kept right next to the file they are testing. For example, users.service.ts will have its test in users.service.spec.ts
- End-to-End (E2E) Tests: These usually live in a dedicated test/ folder at the root of your project, with files ending in .e2e-spec.ts

Default Runner: NestJS comes pre-configured with Jest. In Express, you typically have to set up your own test runner (like Mocha or Jest) and configuration from scratch

# There are 2 test files in each folder

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

# How to run tests

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

# ✅ Key points for testing

1. Controller returns a Promise → test must await.

```JS
//users.controller.spec.ts

it('should return all users', async () => {
   expect(await controller.findAll()).toEqual(mockUsersService.findAll());
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

# 🔥 Pro tip

As your project grows, you might create a structure like:

```JS
test/
  mocks/
    users.service.mock.ts
```

# Optional for Unit testing, without DB connection (usually don't need)

- It is not mandatory to use real DB data for Unit test, but your API will not work. because there is no DB connection.
- You can use mock data for Unit tests. For unit testing, you DO NOT need a real database.

In Unit test in controller:

- Unit tests only check one small piece of your code in isolation.
- You don't need to connect to your real Database. You don’t care about the database.
- You don’t care about the real service implementation.
- You only care that the controller calls the service and returns what the service returns.
- Your tests shouldn’t fail because of database connections.
- Real database logic should only be tested in integration or e2e tests.

- You can just tell NestJS to use a mock without importing the real service at all.

```JS
//we keep this line, but we change constructor
import { UsersService } from './users.service'; // ❌ imports DB stuff

//then in your controller :
constructor(@Inject('UsersService') private usersService: any) {}
//✅ You never touch the database. Jest doesn’t try to load any DB files.
//with this option you will not connect ro your DB and your back-end API will not work !!!
```

See example below

```JS
//users.controller.spec.ts

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

   //here you compare mock data with hard coded block and you don't need connection to Databse
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

# More often used fo Testing

- I used this option in Bank app

```JS
//users.controller.ts line 17, This line allows to connect to my DB and get the data from it
constructor(private readonly usersService: UsersService) {}
```

and

```JS
//users.controller.specs.ts

//useValue: mockUsersService - this code allows to make UPDATE, PUT,PATCH,DELETE in unit test but it will not change my DB. The DB is not getting touched and any updates
providers: [{ provide: UsersService, useValue: mockUsersService }]


//change to this one to see all changes in DB what you do in Unit test
providers: [UsersService]
```
