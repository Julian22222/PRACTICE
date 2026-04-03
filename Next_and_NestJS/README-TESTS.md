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
