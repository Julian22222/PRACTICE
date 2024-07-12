# It is essential that we can automate testing and delivery of our code - Thats where GitHub Actions comes in

## GitHub Actions

[GitHub Actions Docs](https://github.com/features/actions)

[Understanding GitHub Actions](https://docs.github.com/en/actions/learn-github-actions/understanding-github-actions)

- To Choose Workflow extensions
  [GitHub Actions Marketplace](https://github.com/marketplace?type=actions)

[GitHub Runners Docs](https://docs.github.com/en/actions/using-github-hosted-runners/about-github-hosted-runners/about-github-hosted-runners)

GitHub Actions - automates your build, test and deployment workflow with simple and secure CI/CD

- Allow to create a pipeline
- It is build-in CI/CD tool for GitHub
- CI/CD --> stands for continuos integration and continuos delivery ( it is allows us to automate the testing of our code to make sure it meets certain criteria, after all the test are passed you can enable actions to automate the delivery of our code this can significantly reduce the time it takes for you to deliver updates to your application which allows developers to focus more of their time on the code itself)

What is CI/CD --> Example:

- For example we are developing some app, and there are few stages of development, (implimenting many features to your app) and you want to see how it will display in real time app
  CI - it intigrates (new features) your code to your project
- after you have tested some features in your app you need to deploy it to production (it can be automated by --> CD). For example - we can build some docker container, expand it on particular server, make a LogIn and run the app. Or after you have tested your app you can publish your app to Production

# Terminology of the GitHub Actions Workflow YAML File

- One Repository can have few different workflows

Workflow contains:

1. Events - trigger for a workflow, (on: push) <-- when someone pushes new code to GitHub it will trigger GitHub Actions to work, --> will run the jobs within this workflow file,
   To see all the events options, after--> on: , we can press --> ctr + space
2. Jobs - when the event occurs it's going to run all the jobs within the workflow, that specify multiple steps and actions( super-lint: <-- we have a single job, it is name of the job)
3. Runners - we can specify our runner, this is container environment that will run our code, Default containers to choose from are: Ubuntu Linux, Microsoft Windows and Mac OS, (runs-on: ubuntu-latest) <-- which system the process will run the code. To see all the runners options, after--> runs-on: , we can press --> ctr + space
4. Steps - we have 2 steps
5. Actions ( in this sample we have 2 actions underneath the steps there's 2 actions)

```JS
name: Super-Linter    //<--name of the workflow
on: push              // <-- it is listenning for a push event, to start workflow, To see all the events after--> on: , we can press --> ctr + space
jobs:
  super-lint:   //<--job name, can have any name
    name: Lint code base               //<-- the name can have any name, description for this action
    runs-on: ubuntu-latest             //<--runs ubuntu latest version on virtual GitHub server
    steps:                               //<-- we have 2 steps underneath
      - name: Checkout code           //<-- the name can have any name, description for this action
        uses: actions/checkout@v4      //<-- will copy our code to GitHub server

      - name: Run Super-Linter
        uses: github/super-linter@v4
        env:
          DEFAULT_BRANCH: main
          GITHUB_TOKEK: ${{ secrets.GITHUB_TOKEN }}

```

- linter - it is just something that we use to check to make sure that our code is conforming to certain standards, super-linter is made up of multiple linters so it is doesn't matter which code you use in your repository, super-linter is going to understand it and make sure you conform to the standards of that language (Can check for correct spelling any Language -JS,Java, C# etc.)

# Create a Workflow (Directory structure)

- we need to create a new file in the root of the Project Folder otherwise GitHub can't find "yml" files. GitHub searches all workflows in --> .github/workflows/anyFileName.yml
- we need to be very specific with our naming here, so the proper directory structure for our workflow files needs to be first --> .github/workflows/nameOfTheFile.yml
- and then you can name your file whatever you want, following with 'yml' file format
- workflows could not be triggered if the path is wrong. Correct path --> .github/workflows/nameOfTheFile.yml

```JS
name: Node

on:
    push:
      branches: [ "main" ]
    pull_request:
      branches: [ "main" ]

jobs:
  build:       //<--name of the job
    runs-on: ubuntu-latest
      strategy:
        matrix:
          node-version: [14.x, 16.x, 18.x]
          # See supported Node.js release schedule at https://nodejs.org/en/about/releases/

      steps:
      - uses: actions/checkout@v4
      - name: Use Node.js ${{ matrix.node-version }}
        uses: actions/setup-node@v3
        with:
          node-version: ${{ matrix.node-version }}
          cache: 'npm'
      - run: npm ci                                 //<--dependency instalation, the same as --> npm install
      - run: npm run build --if-present
      - run: npm start
```

```JS
name: Demo Workflow
on: workflow_dispatch   //<--need to trigger this workflow manually, from GitHub
jobs:
  print_demo:         //<--name of the job
    runs-on: ubuntu-latest
    steps:
      - name: print to console
        run: echo Hello GH Actions!
```

```JS
name: Demo Workflow
on: workflow_dispatch
jobs:
  print_demo:                                  //<--job name can have any name
    runs-on: ubuntu-latest
    steps:
      - name: print to console                //<-- name of the step can have any name
        run: echo Hello GH Actions!             //<-- will write in console --> Hello GH Actions!
      - name: Print a few lines                //<-- name of the step can have any name
        run: |                                //<--add few lines in the console
          echo First line!
          echo Second line!
```

# All Workflow triggers (on:)

[Click Here](https://docs.github.com/en/actions/using-workflows/workflow-syntax-for-github-actions)

# All Workflow Actions

[Click Here](https://github.com/marketplace?type=actions)

- All workflows run in the GitHub Virtual machines, We need to folow the same pathway as we do on our local machines--> ( GitHub Actions should have the code of our project on their virual machines ) therefore we need to download Repository of our project to GitHub Actions + npm install (run on GitHub virual machine) + npm run someCommand ( run on GitHub virual machine)
- After we downloaded project Repository we need to install dependencies, --> npm ci (will install all dependencies in GitHub virtual machine)
- Jobs can run at the same time or can be performed in order that we allocate (one job can be dependant from previous job), depends how we adjust them. --> needs: []
  As example it is better to make jobs dependant, to do not run all the jobs on the virtual machines if Lint failed,( code syntax is wrong), in this case we can make other jobs dependant from Lint job

# YML syntaxis

- yml is compatible with JSON, it means we can use JSON inside "yml" file

- Example with node version 16

```JS
name: Check    //<--name of the Workflow, can be any name
on:  push     //<--when to trigger this workflow
jobs:         //<-- list of jobs that will be done after workflow triggering
  lint:   //<--name of the job, can have any name
    runs-on: ubuntu-latest   //<--virtual GitHub machine(GitHub resource), container where the code will run, (Free tarif 2000 min per month)
    steps:            //<-- contains actions in order to perform
      - name: Checkout  //<-- name is not mandatory field, can be skipped
            //then follows action(uses) or run command
        uses: actions/checkout@v4     //<--allow to download our Project Repository to GitHub virtual machine, @v4 <--version of checkout version
      - name: Install deps
        run: npm ci//<-- run will allow to make some command in our environment in ubuntu latest
            // npm ci --> (will install all dependencies in GitHub virtual machine), the same as npm install
      - name: Lint   //<-- Lint check correct code syntax
        run: npm run lint
  test: //<--next job, must be on the same level of the first job (lint in our case)
    needs: [lint]  //<--link of dependancies, not mandatory field, to perform test we need successful lint(good code syntax), this job will start running only after lint job finishes its execution, will not run at the same time but after lint successfully executed
    runs-on: ubuntu-latest
      steps:
        - name: Checkout
          uses: actions/checkout@v4
        - name: Node
          uses: actions/setup-node@v3    //<--will install node to virtual machine
            with:
                node-version: 16   // <--uses only node-version 16
        - name: Install deps
          run: npm ci           //<--dependency instalation, the same as --> npm install
        - name: Test
          run: npm run test
```

# Matrix (use for actions with few different parametrs)

- will allow to run the same action with different parammetrs
- Also, can run the same code on ubuntu, windows and MacOS
- etc. the same action for different needs

```JS
strategy:
    matrix:
        version: [14,16,18]    //<-- different node version
        os: [ub, w]  //<-- different virual machines (ubuntu, windows)
.......

//we dont indicate fixed node version, but we put variable that can change
uses: actions/setup-node@v3    //<--will install node to virtual machine
            with:
                node-version: ${{ matrix.version }}   //<-- will use 3 node versions
```

- Example with few node-versions, if app is deployed for few virual machines / few environments and it is important that package was supportted by few node versions

```JS
name: Check    //<--name of the Workflow, can be any name
on:  push     //<--when to trigger this workflow
jobs:         //<-- list of jobs that will be done after workflow triggering
    lint:   //<--name of the job, can have any name
        runs-on: ubuntu-latest   //<--virtual GitHub machine(GitHub resource), container where the code will run, (Free tarif 2000 min per month)
        steps:            //<-- contains actions in order to perform
            - name: Checkout  //<-- name is not mandatory field, can be skipped
            //then follows action(uses) or run command
            uses: actions/checkout@v4     //<--allow to download our Project Repository to GitHub virtual machine, @v4 <--version of checkout version
            -name: Install deps
            run: npm ci//<-- run will allow to make some command in our environment in ubuntu latest
            // npm ci --> (will install all dependencies in GitHub virtual machine), the same as npm install
            - name: Lint   //<-- Lint check correct code syntax
            run: npm run lint
    test: //<--next job, must be on the same level of the first job (lint in our case)
        needs: [lint]  //<--link of dependancies, not mandatory field, to perform test we need successful lint(good code syntax), this job will start running only after lint job finishes its execution, will not run at the same time but after lint successfully executed
        strategy:
          matrix:
            version: [14,16,18]
        runs-on: ubuntu-latest
        steps:
            - name: Checkout
            uses: actions/checkout@v4
            uses: actions/setup-node@v3    //<--will install node to virtual machine
            with:
                node-version: ${{ matrix.version }}   //<-- will use 3 node versions
            - name: Install deps
            run: npm ci                    //<--dependency instalation, the same as --> npm install
            - name: Test
            run: npm run test
```

# Work with variables in GitHub Actions

- most often used is --> Secret

To get an access of Secret -->

1. In GitHub project page --> go to Settings (Horizontal bar menu)
2. In Settings , go to Secrets and Variables ( left side bar menu) ,and choose Actions
3. Click --> New repository Secret , --> after we create a secret we won't see it any more, but we can use it in our GitHub Actions (Most often used Secret --> is a Tokken for something)
4. Secrets always should be written in Capital letters
5. To use that Secret in GitHub Actions workflow-->

```JS
run: ${{ secrets.nameOfSecret }}
```

# Self-hosted runners

- allow to do CI/CD not on GitHub servers but on our local machine, --> you will use less GitHub server minutes (GitHub Free limit 2000 free minutes per mounth), better to use for big projects and quicker
- uses your own local machine for CI/CD

### How to adjust Self-hosted runners

1. In GitHub Project page --> Go to Settings (Horizontal menu bar)
2. click --> Actions --> Runners ( Left side menu bar)
3. To Create a runner --> Click --> New self-hosted runner
4. Choose you local computer system --> macOS, Linux, Windows
5. Choose your architecture --> x64
6. use all the commands below to donwload and link self-hosted runners to your local machine and your GitHub

```JS
name: Check Self-hosted runners
on:  push
jobs:
    build:
        runs-on: self-hosted   //<--will run on our local machine
        steps:
        ......
```

### How to Check if the Runners waiting for a Workflow

1. In GitHub Project page --> Go to Settings (Horizontal menu bar)
2. click --> Actions --> Runners ( Left side menu bar)
3. If Status --> Offline, means the runner is not connected to GitHub
4. IF Status -->
