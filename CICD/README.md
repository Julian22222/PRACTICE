# GitHub Actions

- It is essential that we can automate testing and delivery of our code - Thats where GitHub Actions comes in

[GitHub Actions Docs](https://github.com/features/actions)

[Understanding GitHub Actions](https://docs.github.com/en/actions/learn-github-actions/understanding-github-actions)

# GitHub Actions - automates your build, test and deployment workflow with simple and secure CI/CD

- Allow to create a pipeline
- It is build-in CI/CD tool for GitHub
- CI/CD --> stands for continuos integration and continuos delivery ( it is allows us to automate the testing of our code to make sure it meets certain criteria, after all the test are passed you can enable actions to automate the delivery of our code this can significantly reduce the time it takes for you to deliver updates to your application which allows developers to focus more of their time on the code itself)
- Workflow it is a process of GitHub Actions. Workflow it is an instruction in yml format. workflow reacts on different types of events.
- GitHub Actions have 2000 Free minutes per month, after that if you use more you need to pay to use GitHub virtual servers to run GitHub Actions, or you can run GitHub Actions using your computer.
- GitHub Actions can have only one terminal, (if we need to check some tests -> we need start server in one terminal and run the tests from another terminal). In this case you need to install npm package to current repository --> pm2. This package allow to use containers for separate tasks.

```JS
//npx <-- download and run the pm2 package
// npx pm2 <-- will create new container for separate task (command), to run our server -> start server.js
npx pm2 start server.js

npx pm2 stop 0 //to stop container with id = 0, from table

npx pm2 monit 0 //show CPU usage, memory usage, loop delay or request/min for each container with id = 0

npx pm2 list  //will show table with all container tasks

npx pm2 logs

//Example on the pic below

```

![pic10](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic10.jpg)

![pic11](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic11.jpg)

```JS
//Ci/CD code FROM Root Project folder --> Practice --> .github/workflows -->mySQL_database.yml
//example of using pm2 package

   - name: start server
        working-directory: MySQL-database
        run: |
          npx pm2 start server.js
          sleep 10  // <-- wait 10 seconds after start server, and then do --> npm run test
          npm run test
```

#### What is CI/CD --> Example:

- CI --> For example we are developing some app, and there are few stages of development, (implimenting many features to your app) and you want to see how it will display in real time app.

  CI - it intigrates (new features) your code to your project.

- CD --> after you have tested some features in your app, you need to deploy it to production (it can be automated by --> CD). For example - we can build some docker container, expand it on particular server, make a LogIn and run the app. Or after you have tested your app you can publish your app to Production

# Terminology of the GitHub Actions Workflow YAML File

- One Repository can have few different workflows

Workflow contains:

1. Events - trigger for a workflow, (on: push) <-- when someone pushes new code to GitHub it will trigger GitHub Actions to work, --> will run the jobs within this workflow file,
   To see all the events options, after--> on: , we can press --> ctr + space

[--> All events that trigger workflows <--](https://docs.github.com/en/actions/writing-workflows/choosing-when-your-workflow-runs/events-that-trigger-workflows)

2. Jobs - when the event occurs it's going to run all the jobs within the workflow, that specify multiple steps and actions.You can have many jobs in 1 workflow

If we have few jobs in our workflow, by default they are not dependant from other jobs then they will be running in parallel, at the same time. All the jobs will start running separately, not dependent from each other, they run parallel to each other.
We can make jobs dependant from other jobs --> For example: second job will start running only first job is successfully completed. (Example: ckeck2.yml)

```JS
//some code..
jobName:
  needs: [name of the dependant job]
  runs-on: ubuntu-latest
  steps:
    - name: nameOfTheStep
```

3. Runners - we can specify our runner, this is container environment that will run our code, Default containers to choose from are: Ubuntu Linux, Microsoft Windows and Mac OS, (runs-on: ubuntu-latest) <-- which system the process will run the code. To see all the runners options, after--> runs-on: , we can press --> ctr + space

[--> GitHub Runners Docs <--](https://docs.github.com/en/actions/using-github-hosted-runners/about-github-hosted-runners/about-github-hosted-runners)

4. Steps. you can have as many steps as you want
5. Actions

[--> GitHub Actions Marketplace <--](https://github.com/marketplace?type=actions)

In Marketplace we can find --> Bot, extensions, apps, filter, etc. Actions in Marketplace are workflow code pieces that have already written by other developers. That we can use in our workflows

- To use action from GitHub Actions Marketplace in workflow we put --> uses: ....

You can find alredy written actions in GitHub Actions ecosystem--> From GitHub.com click:

- Left menu button (step 1 on the picture)

![pic1](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic1.jpg)

- Click Marketplace (step 2 on the picture)

![pic2](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic2.jpg)

- In Marketplace we can find many additional extensions, bots, etc. that we can use in GitHub Actions

- Click on Actions, and then --> all actions ( step 3 on the picture), these are already written pieces of code that we can use

![pic3](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic3.jpg)

here you can find what code to write in --> steps. It helps to performe certain actions, For example to copy our code to GitHub server -->
package is called - Setup Node.js environment, where yu can click on that package and you will see that we need to use -->
uses: actions/setup-node@v3 ///<- this code download specific Node version on GitHub virtual machine. By default it is already build-in on the GitHub servers. But this way we can install different Node versions

[--> Most used Workflow Actions <--](https://about.codecov.io/blog/discovering-the-most-popular-and-most-used-github-actions/)

basically you add additional packages from Actions if you use AWS, python, Azure,Docker, C# etc.

# How to Create a Workflow (Directory structure)

- we need to create a new file in the root of the Project Folder otherwise GitHub can't find "yml" files. GitHub searches all workflows in --> .github/workflows/anyFileName.yml
- we need to be very specific with our naming here, so the proper directory structure for our workflow files needs to be first --> .github/workflows/nameOfTheFile.yml
- and then you can name your file whatever you want, following with 'yml' file format
- workflows could not be triggered if the path is wrong. Correct path --> .github/workflows/nameOfTheFile.yml
- we can make many workflows in Repository, each workflow can be responsible for different things

# How to find GitHub actions for your created Repo in GitHub.com

- Go to your Repository (step 1, our Repository in GitHub to check or run GitHub Actions)
- Then click Actions (step 2 on the picture)

![pic4](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic4.jpg)

- then click --> All workflows (step 3 on the picture)

![pic5](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic5.jpg)

- then you can click on specific workflow that you need

# All Workflow triggers (on:)

This is called Events. It triggers for a workflow, (on: push) <-- when someone pushes new code to GitHub it will trigger GitHub Actions to work, --> will run the jobs within this workflow file, To see all the events options, after--> on: , we can press --> ctr + space

[--> Workflow syntax <--](https://docs.github.com/en/actions/using-workflows/workflow-syntax-for-github-actions)

[--> All events that trigger workflows <--](https://docs.github.com/en/actions/writing-workflows/choosing-when-your-workflow-runs/events-that-trigger-workflows)

- it can be one trigger or many triggers

```JS
name: nameOfTheWorkflow
on: workflow_dispatch   //<-- one trigger, triggering manually in GitHub
....
////////
name: nameOfTheWorkflow
on: [workflow_dispatch, push, pull_request ]   //<-- can put many triggers in the array, workflow will react on each of these triggers from array
.....
/////
name: nameOfTheWorkflow
on:
  workflow_dispatch:
  pull_request:                    //<-- pull request can have many activity types, if we don't use --> types: ..., it will run workflow with all the types
    types: [opened, edited, reopened]  //<--to be specific with pull request activity type, on which types we want to react and run workflow
///////
name: nameOfTheWorkflow
on:
  workflow_dispatch:
  schedule:
    -cron: '*/15****'  //<-- cron will run workflow automatically every 15 min

```

[All GitHub actions events triggers](https://docs.github.com/en/actions/using-workflows/events-that-trigger-workflows)

# All Workflow Actions

[Click Here](https://github.com/marketplace?type=actions)

[Marketplace](https://github.com/marketplace)

- All workflows run in the GitHub Virtual machines, We need to folow the same pathway as we do on our local machines--> ( GitHub Actions should have the code of our project on their virual machines ) therefore we need to download Repository of our project to GitHub Actions + npm install (run on GitHub virual machine) + npm run someCommand ( run on GitHub virual machine)
- After we downloaded project Repository we need to install dependencies, --> npm ci (will install all dependencies in GitHub virtual machine)
- Jobs can run at the same time or can be performed in order that we allocate (one job can be dependant from previous job), depends how we adjust them. --> needs: []
  As example it is better to make jobs dependant, to do not run all the jobs on the virtual machines if Lint failed,( code syntax is wrong), in this case we can make other jobs dependant from Lint job

### Checkout (actions/checkout@v4)

### Setup node (actions/setup-node@v4)

### Super-linter (github/super-linter@v4)

### actions/checkout@v4

### Cache (It is an another Action from GitHub marketPlace) (actions/cache@v4)

if you make the same commands on each job you can put it in cache and then use it.

- often used action
- make workflow run faster,
- you don't need to run the same command on each job (such as --> npm ci, etc.). It keeps this in cache
- we can cache some files and folders

To use Cache

- add Cache before a step which we want to put in Cache --> See cache\_&_context.yml file

```JS
....
      - name: Cache deps
        uses: actions/cache@v4
        with:                       //<-- adding some parametrs to Cache
          path: ~/.npm   //<--always the same path for ubuntu machines, on this path all cache will be stored (if we want to cache - npm ci )
          key: node-modules-deps   //<-- the name of created folder for cache.

          //(the same line but with dynamic data). It is better to make this key dynamic which will be dependant from the current dependancy list, because if during development we will add some dependency to this package, then our cache will not be valid. We must use dynamic key-->
          key: node-modules-${{ hashFiles('**/package-lock.json') }}   //<-- expressions allow to add dynamic to workflow.

          // hashFiles(**/package-lock.json') <-- it is special method / function, and here we are passing the path

          //Then we can copy this cache block and put it in each job before command that we want to put in the cache. it is going to be the same cache code for each job. If we install all dependencies on each job, then we can cache that and insert the cache code block before each dependency installation
```

### Artifact (It is an another Action from GitHub marketPlace)

it allows to get acces to build folder from our repository, or to any static files/test reports from our repository. We can download any static file
We use 2 different GitHub actions to upload artifact (from your repository) and then to download artifact --> to us it

[upload artifact docs](https://github.com/marketplace/actions/upload-a-build-artifact)
[download artifact docs](https://github.com/marketplace/actions/download-a-build-artifact)

```JS
name: Build & Deploy
on: [push, work_dispatch]
jobs:
  build:
    runs-on: ubuntu-latest
    steps:
      - name: Get code
        uses: actions/checkout@v4
      - name: Install deps
        run: npm ci
      - name: Upload Artifacts
        uses: actions/upload-artifact@v3
        with:
          path: build   //searching file or folder using this path to add to our artifact, in this case it is folder--> build, (other example- path/to/artifac/word.txt)
          name: build-files  //we name the artifact that we upload
  deploy:
    needs: build   //depends from build job, to get downloaded artifact
    runs-on: ubuntu-latest
    steps:
      - name: Get build project
        uses: actions/download-artifact@v4  //this will download artifact to GitHub actions, where we can see that file and download.
        with:
          name: build-files //name artifacts that we uploaded previously
```

After running this workflow the artifact will appear on GitHub Actions page. Go to current repo GitHub actions ,click on current workflow

![pic6](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic6.jpg)

# YML syntaxis ( file can be written --> nameOfTheWorkFlow.yml or nameOfTheWorkFlow.yaml)

- yml is compatible with JSON, it means we can use JSON inside "yml" file

- Example with node version 16

```JS
name: Check                            //<--name of the Workflow, can be any name
on:  push                              //<--when to trigger this workflow
jobs:                                  //<-- list of jobs that will be done after workflow triggering
  lint:                                //<--name of the job, can have any name
    runs-on: ubuntu-latest             //<--virtual GitHub machine(GitHub resource), container where the code will run, (Free tarif 2000 min per month)
    steps:                             //<-- contains actions in order to perform
      - name: Checkout                 //<-- name is not mandatory field, can be skipped
                                       //then follows action(uses) or run command
        uses: actions/checkout@v4      //<--allow to download our Project Repository to GitHub virtual machine, @v4 <--version of checkout version, we getting acces to Repository code
      - name: Install deps
        run: npm ci                    //<-- run will allow to make some command in our environment in ubuntu latest. npm ci --> (will install all dependencies in GitHub virtual machine), the same as npm install. To run this command you should have --> package-lock.json and package.json files in the Repository root
      - name: Lint                     //<-- Lint check correct code syntax
        run: npm run lint
  test:                                //<--next job, must be on the same level of the first job (lint in our case)
    runs-on: ubuntu-latest
    needs: lint                      //<--link of dependancies, not mandatory field, to perform test we need successful lint(good code syntax), this job will start running only after lint job finishes its execution, will not run at the same time but after lint successfully executed. If lint fails then other job will not be executed, won't run (test job is dependant from lint job)
    steps:
      - name: Checkout
        uses: actions/checkout@v4      //<-- we getting acces to Repository code
      - name: Node
        uses: actions/setup-node@v3    ///<- download specific Node version on GitHub virtual machine. By default it is already build-in on the GitHub servers. But this way we can install different Node versions
            with:
                node-version: 16        // <--uses only node-version 16
      - name: Install deps
        run: npm ci                    //<--dependency instalation, the same as --> npm install, to run this command you shold have --> package-lock.json and package.json files in the Repository root
      - name: Test
        run: npm run test
```

# GitHub Action code examples

```JS
name: Super-Linter                      //<--name of the workflow
on: push                                //<--it is listenning for a push event, to start workflow, To see all the events after--> on: , we can press --> ctr + space
jobs:
  super-lint:                          //<--job name, can have any name
    name: Lint code base               //<--the name can have any name, description for this action
    runs-on: ubuntu-latest             //<--runs ubuntu latest version on virtual GitHub server
    steps:                             //<--we have 2 steps underneath
      - name: Checkout code            //<-- the name can have any name, description for this action
        uses: actions/checkout@v4      //<-- will copy our code to GitHub server, give access to our code for GitHub virtual server

      - name: Run Super-Linter
        uses: github/super-linter@v4
        env:
          DEFAULT_BRANCH: main
          GITHUB_TOKEK: ${{ secrets.GITHUB_TOKEN }}  //<-- use secrets from GitHub secrets section

```

- linter - it is just something that we use to check to make sure that our code is conforming to certain standards, We check code quality. super-linter is made up of multiple linters so it is doesn't matter which code you use in your repository, super-linter is going to understand it and make sure you conform to the standards of that language (Can check for correct spelling any Language -JS,Java, C# etc.)

```JS
//if we have another Folder in the Root folder where we want to run our GitHub Actions, You need to specify what folder/directory you are working in

 - name: Install dependencies
        working-directory: MySQL-database
        run: npm ci
```

```JS
name: Node

on:
    push:
      branches: [ "main" ]
    pull_request:
      branches: [ "main" ]

    //another option how to indicate that workflow will run when we push to main branch-->
    // push:
    //   branches:
    //     - main

    //GitHub will ignore all the changes that was done from the path (in workflow in this case), it won't react in the GitHub
    // push:
    //   branches:
    //     - main
      //  paths-ignore:
      //   - '.github/workflows/*'

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
        uses: actions/setup-node@v4    //<- download specific Node version on GitHub virtual machine, By default it is already build-in on the GitHub servers. But this way we can install different Node versions
        with:
          node-version: ${{ matrix.node-version }}
          cache: 'npm'
      - run: npm ci                                 //<--dependency instalation, the same as --> npm install(npm ci is better to use and safer), to run this command you shold have --> package-lock.json and package.json files in the Repository root,
      - run: npm run build --if-present
      - run: npm start
```

```JS
name: Demo Workflow
on: workflow_dispatch                    //<--need to trigger this workflow manually, from GitHub
jobs:
  print_demo:                           //<--name of the job
    runs-on: ubuntu-latest              //<--first parametr of the job is -> runs-on
    steps:
      - name: print to console
        run: echo Hello GH Actions!
```

```JS
name: Demo Workflow
on: workflow_dispatch                   //<--trigger for a workflow
jobs:
  print_demo:                           //<--job name can have any name
    runs-on: ubuntu-latest              //<-- choose runners (ubuntu, windows, macOS), chooseing what operatin system to use for this workflow
    steps:
      - name: print to console          //<-- name of the step, can have any name, we give a name to identify what is going on on this step
        run: echo Hello GH Actions!     //<-- will write in console --> Hello GH Actions!
      - name: Print a few lines         //<-- name of the step can have any name
        run: |                          //<--add few lines in the console
          echo First line!
          echo Second line!
```

# Context Object

- Displaying in the console --> special context object, which allow to add some data or use some data inside our workflow

- Context object has detailed info about current workflow

- context object --> "${{ github }}"

[GitHub Actions Expressions](https://docs.github.com/en/actions/learn-github-actions/expressions)

```JS
name: Context demo
on: workflow_dispatch
jobs:
    print:
        runs-on: ubuntu-latest
        steps:
            - name: Print context
              run: echo "${{ github }}"   //<-- won't show an object context, can't see what is inside context
```

You can search in Google for GitHub Action expressions to see what else we can use in GitHub Actions, check link below

[--> Here <--](https://docs.github.com/en/actions/writing-workflows/choosing-what-your-workflow-does/evaluate-expressions-in-workflows-and-actions)

- To convert the value from context object to JSON , to display what is inside context, we need -->

```JS
name: Context demo
on: workflow_dispatch
jobs:
    print:
        runs-on: ubuntu-latest
        steps:
            - name: Print context
              run: echo "${{ toJSON(github) }}"  //<-- Context object will display detailed info about this current workflow.

              //It will show detailed info from github object, from this forkflow (info such as: from what branch we pushed the code, name of repository, repository owner, workflow id, etc. ) <-- info from github related to current repository will be here
```

```JS
//if we have different jobs, steps and actions in one yml file --> this example will not work to get context (-->See cache_&_context.yml)
  print:
     runs-on: ubuntu-latest
     steps:
       - name: Print context
         run: echo "${{ toJSON(github) }}"
       - name: Print context event
         run: echo "${{ toJSON(github.event) }}"


//exporting this toJSON(github.event.inputs) into an environment variable and then use this variable

//Use this example --> (See cache_&_context.yml)
- name: Parámetros de entrada
   env:
      PARAMS_ENTERED: ${{ toJSON(github.event.inputs) }}
      run: echo $PARAMS_ENTERED
```

### Context Object contains Detailed data about current workflow:

- what branch we have used to push our code
- repository name
- owner of the repository
- url of our repository
- etc. (all info in GitHub related to current Repository )

- We use context to adjust and automate CI/CD

# Work with variables in GitHub Actions (allow to work with environment variables) --> See env.yml file

```JS
//env.yml file

name: Environment   // Under this name will be workflow in GitHub Actions
on:
  push:                //this is a triiger
    branches:
      - main
  workflow_dispatch:  //this is a triiger
env:                 // define Environment variables here
  NODE_ENV: production
  PORT: 8080
  DB_HOST: localhost
  GH_SECRET: 42
  GH2_SECRET: ${{ secrets.DEPLOY_SECRET}}  // secret from GitHub Action Secrets, GitHub won't show secret, it will show --> ***, we can use secrests for passwords, connection strings, etc. But GitHub Won't show the secrets
jobs:
  build:                     // name of the job
    runs-on: ubuntu-latest
    steps:
      - name: Print Env Build
        run: |
          echo "${{ env.DB_HOST }}"  // to show environment variables from line 441, here we are using env object and DB_HOST key to get needed value
          echo "${{ env.GH_SECRET }}"
          echo "${{ env.PORT }}"
          echo "${{ env.NODE_ENV }}"
  deploy:                   // name of the job
    runs-on: ubuntu-latest
    env:                     //declearing other environment varibles, this show environment variable scope
        NODE_ENV: staging
        PORT: 8081
    steps:
      - name: Print Env Deploy
        run: |
          echo "${{ env.PORT }}"
          echo "${{ env.NODE_ENV }}"
```

- most often used with --> Secret (from GitHub page)

## How to write Secret variables on GitHub page and then use them in CI/CD-->

1. In GitHub project Repository --> go to Settings (Horizontal bar menu)

![pic7](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic7.jpg)

2. In Settings , go to Secrets and Variables ( left side bar menu) ,and choose Actions

![pic8](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic8.jpg)

3. Click --> New repository Secret --> after we create a secret we won't see it any more, but we can use it in our GitHub Actions (Most often used Secret --> is a Tokken for something)

![pic9](https://github.com/Julian22222/PRACTICE/blob/main/CICD/IMG/pic9.jpg)

4. Secrets always should be written in Capital letters
5. To use that Secret in GitHub Actions workflow we write -->

```JS
run: ${{ secrets.nameOfSecret }}
```

# Matrix (use for actions with few different parametrs)

- will allow to run the same action with different parammetrs
- Also, can run the same code on ubuntu, windows and MacOS
- etc. the same action for different needs

```JS
strategy:
    matrix:
        version: [14,16,18]    //<-- different node version, word --> version (can have any name, it is a key that we will use)
        os: [ubuntu-latest, windows-latest]  //<-- different virual machines (ubuntu, windows), word --> os (can have any name, it is a key that we will use)
.......

runs-on: ${{ matrix.os }}  //<-- will run 2 parametrs

//we dont indicate fixed node version, but we put variable that can change
uses: actions/setup-node@v4    //<--will install node to virtual machine, By default it is already build-in on the GitHub servers. But this way we can install different Node versions
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
        uses: actions/checkout@v4     //<--allow to download our Project Repository to GitHub virtual machine, @v4 <--version of checkout version, we getting acces to Repository code
      -name: Install deps
        run: npm ci//<-- run will allow to make some command in our environment in ubuntu latest
            // npm ci --> (will install all dependencies in GitHub virtual machine), the same as npm install.  //<--dependency instalation, the same as --> npm install, to run this command you shold have --> package-lock.json and package.json files in the Repository root
      - name: Lint   //<-- Lint check correct code syntax
        run: npm run lint
    test: //<--next job, must be on the same level of the first job (lint in our case)
      needs: [lint, otherNameOfJob]  //<--link of dependancies, not mandatory field, to perform test we need successful lint(good code syntax), this job will start running only after lint job finishes its execution, will not run at the same time but after lint successfully executed. If we don't put --> needs: --> Jobs are not dependant from each other and running at the same time. Each Job was invoked separatelly, See --> check.yml file line 24
      continue-on-error: true // will continue to run CI/CD if some threads had errors, and some threads have passed
      strategy:
        matrix:
          version: [14,16,18]   //version key <-- can be any name
          os: [ubuntu-latest, windows-latest]
        runs-on: ${{ matrix.os }}
        steps:
            - name: Checkout
            uses: actions/checkout@v4       //<-- we getting acces to Repository code
            uses: actions/setup-node@v4    //<--will install node to virtual machine, By default it is already build-in on the GitHub servers. But this way we can install different Node versions
            with:
                node-version: ${{ matrix.version }}   //<-- will use 3 node versions. we use matrix and a key that we created in the matrix
            - name: Install deps
            run: npm ci                     //<--dependency instalation, the same as --> npm install, to run this command you shold have --> package-lock.json and package.json files in the Repository root
            - name: Test
            run: npm run test
```

# Self-hosted runners

- allow to run CI/CD not on GitHub servers but on your local machine, --> you will use less GitHub server minutes (GitHub Free limit 2000 free minutes per mounth), better to use for big projects and quicker. When you often need to run CI/CD on your project, check different tests.
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

As example: if we want to use GitHub Actions with React app (Front-End)
To test this app we need:

- get access to Repository code --> (uses: actions/checkout@v4). GitHub server don't have access to our code by default.
- get Node to GitHub virtual machine --> (uses: actions/setup-node@v4 ) --> it downloads specific Node version on GitHub virtual machine. By default it is already build-in on the GitHub servers. But this way we can install different Node versions
- make a build or run some tests

# Most often used Actions from Marketplace

[--> Marketplace <--](https://github.com/marketplace?type=actions)

- Test Reporter (to check JEST / Mocha test in JS)(.NET / dotnet test ( xUnit / NUnit / MSTest ))

- Build and push docker images

- etc.

# NPM package

- eslint-config-react-app (check code quality in React app)
- install packages
- in package.json -->in scripts -->

```JS
"lint" : "npx eslint ./src"
```
