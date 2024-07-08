# It is essential that we can automate testing and delivery of our code - Thats where GitHub Actions comes in

- It is build-in CI/CD tool for GitHub
- CI/CD --> stands for continuos integration and continuos delivery ( it is allows us to automate the testing of our code to make sure it meets certain criteria, after all the test are passed you can enable actions to automate the delivery of our code this can significantly reduce the time it takes for you to deliver updates to your application which allows developers to focus more of their time on the code itself)

# Terminology of the GitHub Actions Workflow YAML File

-yml file specifies:

1. Events - trigger for a workflow, (on: push) <-- when someone pushes new code to GitHub it will trigger GitHub Actions to work, --> will run the jobs within this workflow file
2. Jobs - when the event occurs it's going to run all the jobs within the workflow, that specify multiple steps and actions( super-lint: <-- we have a single job, it is name of the job)
3. Runners - we can specify our runner, this is container environment that will run our code, Default containers to choose from are: Ubuntu Linux, Microsoft Windows and Mac OS, (runs-on: ubuntu-latest) <-- which system the process will run the code
4. Steps - we have 2 steps
5. Actions ( in this sample we have 2 actions underneath the steps there's 2 actions)

```JS
name: Super-Linter

on: push                           // <-- it is listenning for a push event, to start workflow

jobs:
    super-lint:
        name: Lint code base               //<-- the name can have any name, description for this action
        runs-on: ubuntu-latest             //<--runs ubuntu latest version
        steps:                               //<-- we have 2 steps underneath
            -name: Checkout code           //<-- the name can have any name, description for this action
            uses: actions/checkout@v3      //<-- will copy our code to GitHub server

            -name: Run Super-Linter
            uses: github/super-linter@v3
            env:
                DEFAULT_BRANCH: main
                GITHUB_TOKEK: ${{ secrets.GITHUB_TOKEN }}

```

- linter - it is just something that we use to check to make sure that our code is conforming to certain standards, super-linter is made up of multiple linters so it is doesn't matter which code you use in your repository, super-linter is going to understand it and make sure you conform to the standards of that language

# Create a Workflow (Directory structure)

- we need to create a new file in the root of the Project Folder
- we need to be very specific with our naming here, so the proper directory structure for our workflow files needs to be first --> .github/workflows
- and then you can name your file whatever you want, following with 'yml' file format
- workflows could not be triggered if the path is wrong. Correct path --> .github/workflows/nameOfTheFile.yml
