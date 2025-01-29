[--> Docker Docs <--](https://docs.docker.com/get-started/docker-overview/)

[--> Docker Hub <--](hub.docker.com)

# What is Docker

- Docker it is a programm that can be installed on your phisical machine/ computer. This programm allows to create and control containers.
- Docker is an open-source software platform that allows developers to build, test, run and deploy applications quickly.
- Docker enables you to separate your applications from your infrastructure so you can deliver software quickly. With Docker, you can manage your infrastructure in the same ways you manage your applications. By taking advantage of Docker's methodologies for shipping, testing, and deploying code, you can significantly reduce the delay between writing code and running it in production.
- Docker provides the ability to package and run an application in a loosely isolated environment called a container. The isolation and security lets you run many containers simultaneously on a given host.
- Docker allow to develop your application and its supporting components using containers.
- Docker is written on --> Go language
- The Docker is based on Images and Containers. Images and Containers are called -> Entities. Images - are templates/patterns which are designed to create containers. Images exist only for Read only (We can't edit them). Container always creating from the Image (we read the Image and run the container, our application or service runs in the Container). Containers are launched on the basis of Image.
- Docker has whole chain of image inheritances which allow us to run different applications in different operation systems.

# Container

- Container it is isolated environment where we can put different programms or services (For example- NodeJS, Mongo DB, Python,Nginx,PHP, etc.)
- Containers are lightweight and contain everything needed to run the application, so you don't need to rely on what's installed on the host. You can share containers while you work, and be sure that everyone you share with gets the same container that works in the same way.

# Why We need Docker?

You develop some application/service and to host it somewhere we wrap our app into container/containers and host on some cloud server.
Then Docker guarantee that on any other physical machine where Docker is installed we can run the same container with that application or service.

Docker allows you to ship, test, and deploy your applications in any environment without worrying about incompatible issues regardless of the machine's configuration settings

#### Consider the following example scenario:

For example - You use macOS operation system with Node JS (version 12) and develop some application or service. Then you need to host your application somewhere (for example on some cloud server, but that cloud server might not support macOS operating system, and cloud server should have the same Node JS version that we used in our development, if it will be different it make show an error). Threfore we need to make it work on different operating systems (Windows, linux, macOS, etc.) and with different Node JS versions.

#### Consider the following example scenario:

Your developers write code locally and share their work with their colleagues using Docker containers.
They use Docker to push their applications into a test environment and run automated and manual tests.
When developers find bugs, they can fix them in the development environment and redeploy them to the test environment for testing and validation.
When testing is complete, getting the fix to the customer is as simple as pushing the updated image to the production environment.

# Work with Docker

### Docker commands

```JS
sudo docker images  //This command will open this table, check picture below
```

![pic2](https://github.com/Julian22222/PRACTICE/blob/main/Docker/IMG/pic2.jpg)

write in terminal

```JS
clear //<-- clear the console

docker -v //will show Docker version, if it is installed on your computer

docker //<-- will give different commands

docker build .  //<-- will create new Image from Dockerfile instructions / you need to be in correct folder

docker images --help  //<--will show what this command does, and show description. use --> --help for any of the commands, don't need to memorise all the Docker commands
sudo docker images //<-- will show all images
sudo docker image ls //<-- the same command, will show all images


//Every time when we run command  --> docker run ImageId, -It creates and runs new container from ImageId, and creates new ContainerId every time !!!!

docker run ImageID //<-- will create and run container from ImageId that we indicated in ImageID
docker run ImageName // the same command
docker run -it node //<-- run node with parametr, it -> means interactive, we get inside container and can insert and interact with Node how we want.
// To check what Node version is installed in container --> inside container we write -> process.version
// To exit from current container to local computer --> .exit

docker run -p 3000:3000 ImageID  //<-- we can pass some parammeters, -p <-- means PORT, which has special syntaxis where we indicate 2 ports -->
//first 3000 port tells which local port on local machine we need to use to run this Docker container, we use localhost:3000 in our browser to run this container
//second 3000 port tells which PORT from Docker container we want to use, (second port indicates the PORT inside Docker container). This number MUST match Dockerfile !!!  --> EXPOSE 3000

docker run -d -p 80:3000 ImageId //<-- here we adding another parammetr -d <-- means Detached. We don't enter inside this container. We stay im main console and we can use other commands to check the docker processes, we can keep working with other containers or data.
//it helps to continue working with other data, we do not get inside current container. To get to main console we need to exit from current running container therefore we need to stop container.
//first PORT =80, we use localhost:80 in our browser to run this container
//second PORT=3000, Docker container PORT, This PORT must be the same as --> EXPOSE 3000, in Dockerfile !!!


docker ps --help //<-- will show what this command does, and show description. use --> --help for any of the commands, don't need to memorise all the Docker commands
docker ps //<-- show the list of all running/active containers
docker ps -a //<-- will show all containers on our local machine (will show running and inactive containers)

//You need to delete all inactive/unused containers, because they will build up and take more and more space and resources on your local machine
docker rm ContainerID  //<-- delete inactive containers from local machine, to don't waste memory and resources
//also can delete few containerID's, example --> docker rm f285d9d9c0b6 f267d8i9c0h9

docker container prune //<-- delete all inactive containers in the list

docker stop ContainerID //<-- to stop running Container

docker start ContainerID  //<-- if we have container in our list inactive containers (you can check that using this command--> docker ps -a)
```

### Give certain instructions for Docker

- Create Dockerfile (no extensions) File in the Root of your project, which is Docker indicator with its instruction
- The name should be --> Dockerfile, no any other names
- Inside of this Dockerfile we can write instructions for Docker, here we creating new Image

```JS
//Dockerfile instructions example 1
//Dockerfile helps to create an Image

FROM python //<-- we take python image, name of the Image that we want to use for our environment

WORKDIR /app //<-- identify Root directory/ working directory is -> /app (folder), app folder already exist in the project, where we have all request methods (GET, POST, DELETE, PUT) and app.listen code

COPY . . //<-- copy all the files from working directory, fisrt dot means - copy files from current location, second dot - means paste all copied files to Root Docker Image folder
//<-- First of all we mention from where we want to copy our files and what we want to copy. First dot (COPY .) means we copy all folders and files from Root folder of our Project.
//Location of Project Root place is defined by --> Dockerfile folder (where you have Dockerfile document). After that we declare -> where we want to paste those files into new Docker Image. (COPY . .) --> Second dot means that we will paste all files to Root Docker Image Folder. Usually we create special folder, which will serve as Root folde for all our App. If we use --> WORKDIR /app before this, than we can write --> COPY . . - Because we will be located in /app folder after WORKDIR /app command.


CMD ["python", "index.py"] //<-- an array, with few elements. Each element represent one command. -> Locally to start our application in Python we write in terminal --> python index.py, Where index.py is file to run. Here is the same but all elements we put separately
//CMD command is running all the time when we convert our Image to Container
```

```JS
//Dockerfile instructions example 2

FROM node //<--we take node image, name of the Image that we want to use for our environment. We tell that our Image is based on Node JS. When Docker will read this line, Docker we check if we have installed this image localy and if we don't have this Image locally -> Docker will download it from Docker Hub.

//working Directory where we keep our application. WORKDIR /app <-- means in folder /app , we have our application, where we have all our folders and files
WORKDIR /app   ///app folder already exist in the project, where we have all request methods (GET, POST, DELETE, PUT) and app.listen code

//Now we need to set our Image, we need to set what files we want to move to new Image and keep it there. Image is used for read only. When you create the Image, you can't edit it. COPY command copy some certains Folders and files from our project to new Docker Image
COPY . . //<-- First of all we mention from where we want to copy our files and what we want to copy. First dot (COPY .) means we copy all folders and files from Root folder of our Project. Project Root place is located, where you have Dockerfile document. After that we declare -> where we want to paste those files into new Image. (COPY . .) --> Second dot means that we will paste all files to Root Image Folder. If we use --> WORKDIR /app before this, than we can write --> COPY . . - Because we will be located in /app folder after WORKDIR /app command.

//usually you need to create special folder, which will serve as a Root folder for all our application. example below--> (we create app folder and paste all the files into this folder)
COPY . /app

//To run this Node app we need all dependencies from our app
RUN npm install  //RUN is used when we build an Image

EXPOSE 3000//<-- This command is not mandatory in Docker , but it is best practise. It tells what PORT will run our application
//This code means -> when we start this application, we will use PORT 3000

//To run our application
CMD ["node", "app.js"] //<-- an array, with few elements. Each element represent one command. -> Locally to start our application we write in terminal --> node app.js, Where app.js is file to run. Here is the same but all elements we put separately
//CMD command is running all the time when we convert our Image to Container

```

- To run Docker instructions (create new Image) we put in terminal ->

```JS
docker build .  //this command creates image based on our Docker instructions, In terminal be in corect project folder -> You need to be in Folder where you have -> Dockerfile file, therefor dot means - current directory
```

- Then we can find created image

```JS
sudo docker images  //<-- here you can copy image ID to create Container
```

- Then to create Container from this image we put in terminal

```JS
docker run ImageID

docker stop ContainerID //<-- to stop running Container, if you want to exit
//or use new terminal to continue working
```

This environment Images allow us to write code or run application even without instaling current language package.
From example above, if we use Python language we don't need to set environment for Python (or Node, or PHP, etc.) because these settings are already prepared and we can use them. We can find already prepared settings.

write in google -> docker hub Node, which will give a link to Docker Hub website for Node environment -> hub.docker.com

From hub.docker.com we can pull needed Images.

![pic1](https://github.com/Julian22222/PRACTICE/blob/main/Docker/IMG/pic1.jpg)

- Then we copy command and paste to Terminal, will install latest Image by default, if we don't specify Image version
- Then this Image will be on your local computer, to check that you can write -->

```JS
sudo docker images  //This command will open this table, check picture below
```

![pic2](https://github.com/Julian22222/PRACTICE/blob/main/Docker/IMG/pic2.jpg)

To interact with certain Docker Container we can use ContainerID or Container Name, they are unique for each created container (The same with Images)

- (Step 1 on the pic below) Container ID
- (Step 2 on the pic) Container Name
- (Step 3 on the pic ) -> docker ps , will show all active containers
- (Step 4 on the pic ) -> docker stop loving_jennings, will stop running container with Name = loving_jennings

![pic3](https://github.com/Julian22222/PRACTICE/blob/main/Docker/IMG/pic3.jpg)

## Difference between docker run ImageId and docker start containerId (Both commands start running container)

- run <-- this command works only with docker Images, NOT with docker containers !!!
- start <-- this command work only with docker containers !!!
