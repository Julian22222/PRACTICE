# JEST AND SUPERTEST

[CLICK HERE](https://create-react-app.dev/docs/running-tests/#:~:text=Jest%20will%20look%20for%20test,in%20__tests__%20folders)

- Jest is a Node-based runner. This means that the tests always run in a Node environment and not in a real browser.

Jest will look for test files with any of the following popular naming conventions:

- Files with .js suffix in **tests** folders.
- Files with .test.js suffix.
- Files with .spec.js suffix.
- The .test.js / .spec.js files (or the **tests** folders) can be located at any depth under the src top level folder. --> In REACT JS

When you run npm test, Jest will launch in watch mode\*. Every time you save a file, it will re-run the tests, like how npm start recompiles the code.

jest --watch

- To have the same data before eaach test we can implement --> beforeEach or afterEach function

[-->afterEach example<--](https://www.youtube.com/watch?app=desktop&v=UFjtOmvmAU0)

### Jest has many advantages, some of which are listed below.

- It requires minimal configuration to run the tests
- It can be integrated with popular NodJS front-end frameworks such as VueJS, React, Angular, etc.
- It also supports popular end-to-end testing frameworks like WebdriverIO, Playwright, etc.
- Jest is an open-source framework and free to use
- It provides many built-in capabilities, such as skipping, running specific, and running tests based on regular expression patterns.
- A large set of matches to validate the test results
- Supports Asynchronous Testing
- Debugging capability, Mocking, and faster execution are a few key features of Jest.

### Jest provides different ways to run the tests. You can run jest tests on a specific file(s), specific folder(s), or with a regular expression, and you can even skip specific tests.

```JS
npx jest <filename>


//Example
npx jest vowels.test.js
```

### To run Jest tests for a specific folder, the syntax remains the same as running tests for a specific file; instead of the file, you must pass the folder name. In the above project, you have created a folder called string-operations, which has two files namely extractnumber.test.js and stringconcat.test.js. If you execute the string-operations folder, then both tests should be executed.

```JS
npx jest <folder_name>

//Example
npx jest string-operations
```

## Running a Single Test Using ‘only’ or ‘f’

Jest supports executing single tests within the same file. To execute a single test in a Jest you can use the “only” postfix to your tests such as it.only(), test.only(), describe.only() etc.

The f prefix also performs the same operations but it is available for only it() and describe() functions. Example fit(), fdescribe() etc.

## Skipping Tests Using ‘skip’ or ‘x’

There are two ways to skip a test in Jest: skip postfix or use the x prefix. Note that the x prefix is only available for it() and describe() functions. For example: you can skip the test using xit(), xdescribe(), etc.

However, the skip postfix is available for all different functions for example test.skip(), it.skip(), etc. Let us understand with an example

## Unit testing allows us to test the individual functionality of our React components.

[HERE](https://dev.to/knowicki024/setting-up-test-files-in-react-with-jest-26la#:~:text=The%20best%20way%20to%20set,will%20keep%20your%20test%20files)

```JS
//example block

request('https://dog.ceo')
.get('/api/breeds/image/random')
.expect(200)
.expect('Content-Type', 'application/json')
.expect(function(res) {
if (!res.body.hasOwnProperty('status')) throw new Error("Expected 'status' key!");

```
