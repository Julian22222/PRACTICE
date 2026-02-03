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
docker build -t myImage_name .  //<-- we can pass some parammeters, -t <-- this allows to add a Name to your Image
docker build -t myImage_name:mytagname .  //<-- we can pass some parammeters, after colon --> :mytagname <-- this allow to put a tag to your Image. If we don't put tag name -> by default it is alwasys -> latest

docker images --help  //<--will show what this command does, and show description. use --> --help for any of the commands, don't need to memorise all the Docker commands
sudo docker images //<-- will show all images
sudo docker image ls //<-- the same command, will show all images

docker rmi imageId_or_imagename  //<-- delete unwanted image from the list (from -> docker images)
docker rmi f285d9d9c0b6 f267d8i9c0h9  ////<--also can delete few Images using their Id's
docker rmi imageId:imageTag  //<-- if we have Images with the same names we can specify which image to delete by adding imageTag --> :imageTag

docker image prune  //<--delete all unused Images from the list

//Every time when we run command  --> docker run ImageId, -It creates and runs new container from ImageId, and creates new ContainerId every time !!!!

docker run ImageID_or_ImageTag //<-- will create and run container from ImageId that we indicated in ImageID
docker run ImageName // the same command
docker run -it node //<-- run node with parametr, it -> means interactive, we get inside container and can insert and interact with Node how we want.
// To check what Node version is installed in container --> inside container we write -> process.version
// To exit from current container to local computer --> .exit

docker run -p 3000:3000 ImageID  //<-- we can pass some parammeters, -p <-- means PORT, which has special syntaxis where we indicate 2 ports -->
//first 3000 port tells which local port on local machine we need to use to run this Docker container, we use localhost:3000 in our browser to run this container, this localhost PORT --> MUST be the same as API PORT from our API server that we put in container--> app.listen(PORT)
//second 3000 port tells which PORT from Docker container we want to use, (second port indicates the PORT inside Docker container). This number MUST match Dockerfile !!!  --> EXPOSE 3000

docker run -d -p 80:3000 ImageId //<-- here we adding another parammetr -d <-- means Detached. We don't enter inside this container. We stay im main console and we can use other commands to check the docker processes, we can keep working with other containers or data.
//it helps to continue working with other data, we do not get inside current container. To get to main console we need to exit from current running container therefore we need to stop container.
//first PORT =80, we use localhost:80 in our browser to run this container, this localhost PORT --> MUST be the same as API PORT from our API server that we put in container--> app.listen(PORT)
//second PORT=3000, Docker container PORT, This PORT must be the same as --> EXPOSE 3000, in Dockerfile !!!

docker run -p 50:3000 -d --name any_container_name ImageId   //<-- we can pass extra parametrs when we start certain container, we can set a name for new container

docker run -d -p 3000:3000 --name myapp --rm imageID  //<-- another paramer -> rm. Means as soon as we stop this running container it will be deleted from container list, (docker ps -a) <-- this command won't show this container. myapp <-- is the name of our new container

docker run -d -p 3000:3000 --name myapp --rm imageID:imageTag  //<-- if we have Images with the same names we can specify which image to convert to container by adding imageTag --> :imageTag

docker run -d -p 3000:3000 -v myvolume:/app/data --name myapp --rm imageID:imageTag   //<-- another paramer -v . Using this code,docker create volume name: myvolume , AUTOMATICALLY !!!
// -v myvolume:/app/data
// -v <-- volumes parametr
// myvolume: <-- any name, we give a volume name here
//  /app/data  <-- the path to volume folder, should be the same what we indicated in Dockerfile -> VOLUMES [ "/app/data" ]
///<-- this command creates and runs current container

docker logs containerID_or_containerName //<-- will show all activity in current container, show what Docker container Port is used fro our running application etc.

docker ps --help //<-- will show what this command does, and show description. use --> --help for any of the commands, don't need to memorise all the Docker commands
docker ps //<-- show the list of all running/active containers, we can check if the container is running
docker ps -a //<-- will show all containers on our local machine (will show running and inactive containers)


docker attach containerID or container Name //<--if we need to connect to certain container, after using detached parametr. As example-> (docker run -d -p 80:3000 ImageId )


//You need to delete all inactive/unused containers, because they will build up and take more and more space and resources on your local machine
docker rm ContainerID  //<-- delete inactive containers from local machine, to don't waste memory and resources
docker rm f285d9d9c0b6 f267d8i9c0h9  ////<--also can delete few containerID's

docker container prune //<-- delete all inactive containers in the list

docker stop ContainerID_or_containerName //<-- to stop running Container

docker start ContainerID_or_containerName  //<-- if we have container in our list inactive containers (you can check that using this command--> docker ps -a)


docker volume --help    //<-- will show all commands related to volumes
docker volume ls   //<-- will show all volumes
docker volume inspect yourVolumeName  //<-- we can check what we have in this volume, (when it was created, drivers, path to folder, etc.)

docker volume rm volumeName  //<-- delete volume name
docker volume prune  //<-- delete all unused valumes

docker volume create volumeName  //<-- create manually volume name
```

### Give certain instructions for Docker

- Create Dockerfile (no extensions) File in the Root of your project, which is Docker indicator with its instruction
- The name should be --> Dockerfile, no any other names
- Inside of this Dockerfile we can write instructions for Docker, here we creating new Image
- When we build an Docker Image --> it building layer by layer and creating full Image

```JS
//Dockerfile instructions - EXAMPLE 1 (not fully correct example to use)
//Dockerfile helps to create an Image

FROM python //<-- we take python image, name of the Image that we want to use for our environment

WORKDIR /app //<-- identify Root directory/ working directory is -> /app (itis file app.js), app.js file already exist in the project, where we have all request methods (GET, POST, DELETE, PUT) and app.listen code

COPY . . //<-- copy all the files from working directory, fisrt dot means - copy files from current location, second dot - means paste all copied files to Root Docker Image folder
//<-- First of all we mention from where we want to copy our files and what we want to copy. First dot (COPY .) means we copy all folders and files from Root folder of our Project.
//Location of Project Root place is defined by --> Dockerfile folder (where you have Dockerfile document). After that we declare -> where we want to paste those files into new Docker Image. (COPY . .) --> Second dot means that we will paste all files to Root Docker Image Folder. Usually we create special folder, which will serve as Root folde for all our App. If we use --> WORKDIR /app before this, than we can write --> COPY . . - Because we will be located in /app folder after WORKDIR /app command.


CMD ["python", "index.py"] //<-- an array, with few elements. Each element represent one command. -> Locally to start our application in Python we write in terminal --> python index.py, Where index.py is file to run. Here is the same but all elements we put separately
//CMD command is running all the time when we convert our Image to Container
```

```JS
//Dockerfile instructions - EXAMPLE 2 (not fully correct example to use)

FROM node //<--we take node image, name of the Image that we want to use for our environment. We tell that our Image is based on Node JS. When Docker will read this line, Docker we check if we have installed this image localy and if we don't have this Image locally -> Docker will download it from Docker Hub.

//working Directory where we keep our application. WORKDIR /app <-- means in folder /app , we have our application, where we have all our folders and files
WORKDIR /app   ///identify Root directory/ working directory is -> /app (it is file app.js), app.js file already exist in the project, where we have all request methods (GET, POST, DELETE, PUT) and app.listen codelisten code

//Now we need to set our Image, we need to set what files we want to move to new Image and keep it there. Image is used for read only. When you create the Image, you can't edit it. COPY command copy some certains Folders and files from our project to new Docker Image
COPY . . //<-- First of all we mention from where we want to copy our files and what we want to copy. First dot (COPY .) means we copy all folders and files from Root folder of our Project. Project Root place is located, where you have Dockerfile document. After that we declare -> where we want to paste those files into new Image. (COPY . .) --> Second dot means that we will paste all files to Root Image Folder. If we use --> WORKDIR /app before this, than we can write --> COPY . . - Because we will be located in /app folder after WORKDIR /app command.

//usually you need to create special folder, which will serve as a Root folder for all our application. example below--> (we create app folder and paste all the files into this folder)
COPY . /app  //<-- we use COPY . . or use this line if we didn't use --> WORKDIR /app

//To run this Node app we need all dependencies from our app
RUN npm install  //RUN is used when we build an Image

EXPOSE 3000//<-- This command is not mandatory in Docker , but it is best practise. It tells what PORT will be used in Docker Container to run our application.
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

![pic3](https://github.com/Julian22222/PRACTICE/blob/main/Docker/IMG/pic3.jpg)

- (Step 1 on the pic above) Container ID
- (Step 2 on the pic) Container Name
- (Step 3 on the pic ) -> docker ps , will show all active containers
- (Step 4 on the pic ) -> docker stop loving_jennings, will stop running container with Name = loving_jennings

## Difference between: docker run ImageId and docker start containerId (Both commands start running container)

```JS
docker run ImageId //<-- this command works only with docker Images, NOT with docker containers !!!
```

```JS
docker start containeId  //<-- this command work only with docker containers !!!
```

## Images hold only that code which was in your application on the moment when you were creating current Image

- When you create an Image from Dockerfile (from your app) - it takes only current code (which we had on that moment) and makes an Image from it --> Image is fixed and we can't change it, it is read only
- After changing or adding something in your app --> you will not see these changes in your running container, to apply change to your container -> you need to create new Image and then run container

# Dockerfile correct syntaxis

Previous Dockerfile examples wasn't fully correct, because every time when we create new Image from our current application using Dockerfile instructions we do extra work.

- For example: If we adding or changing something in our code in the Application and we want to apply it to new container. Then we need to create new Image from our updated code and then create and run new container.

- If we going to use Dockerfile instruction examples 1 and 2 (used previously, above), every time we will run:

```JS
RUN npm install  //<-- it will takes a lot of traffic, time consuming, etc.
```

but for example we have changed only 1 line in our code, and we need to run -> npm install again (it is not good solution)

Docker can compare your previous code version and updated code version and take some code from cashe to do not do extra work. Therefore we can optimize our Dockerfile instruction process (which is related to node_modules folder)

```JS
//Dockerfile instructions - EXAMPLE 3 (Correct version of instructions)

FROM node //<--we take node image, name of the Image that we want to use for our environment. We tell that our Image is based on Node JS. When Docker will read this line, Docker we check if we have installed this image localy and if we don't have this Image locally -> Docker will download it from Docker Hub.

//working Directory where we keep our application. WORKDIR /app <-- means in folder /app , we have our application, where we have all our folders and files in the container
//# Set the working directory in the container
WORKDIR /app   ///identify Root directory/ working directory is -> /app (it is folder app), this folder already exist in the project, if not existing -> it will create app folder in the container, where we have all request methods (GET, POST, DELETE, PUT) and app.listen code.listen code

COPY package.json .  //<-- we copy package.json to work directory or to our folder --> app, where we keep all application.
//or
COPY package.json /app  //<-- This command to use if copy to our app.js file

//To run this Node app we need all dependencies from our app
RUN npm install  //RUN is used when we build an Image

//Now we need to set our Image, we need to set what files we want to move to new Image and keep it there. Image is used for read only. When you create the Image, you can't edit it. COPY command copy some certains Folders and files from our project to new Docker Image
COPY . . //<-- First of all we mention from where we want to copy our files and what we want to copy. First dot (COPY .) means we copy all folders and files from Root folder of our Project. Project Root place is located, where you have Dockerfile document. After that we declare -> where we want to paste those files into new Image. (COPY . .) --> Second dot means that we will paste all files to Root Image Folder. If we use --> WORKDIR /app before this, than we can write --> COPY . . - Because we will be located in /app folder after WORKDIR /app command.

EXPOSE 3000//<-- This command is not mandatory in Docker , but it is best practise. It tells what PORT will be used in Docker Container to run our application.
//This code means -> when we start this application, we will use PORT 3000

//To run our application
CMD ["node", "app.js"] //<-- an array, with few elements. Each element represent one command. -> Locally to start our application we write in terminal --> node app.js, Where app.js is file to run. Here is the same but all elements we put separately
//CMD command is running all the time when we convert our Image to Container
```

This command order is correct, when we want to recreate new Image because of the changes you made in the code. If we don't change node mudules -> Docker will take node modules from CASHE and will not make extra work every time when we add some changes to our app.

# How to Upload your Images to Docker Hub and the pull the Image to your computer

[--> Docker Hub <--](https://app.docker.com/)

1. In terminal write

```JS
docker login
```

2. give access to your docker (by confirming authorization)
3. Then use command -->

```JS
//your image name that you are pushing to Docker Hub must be the same as the command line name. This part -> ( julian22222/imageId_or_imageName ) from your command.
// docker push julian22222/imageId_or_imageName  <-- ( julian22222/imageId_or_imageName )

//to create new Image name from other Image use this command--> it not edeting the old image, it adds new image, and keeping both new Image and old image
docker tag oldImageName julian22222/newImageName


docker push julian22222/imageId_or_imageName   //<-- to push to your own Repository on Docker Hub, julian22222 <-- is my nickname on Docker Hub
//or
docker push julian22222/ImageName:imageTag     //<--
```

4. The go to your Docker Hub Repository and take the Image name and put this command in your terminal

```JS
docker pull image_name
```

# We can ignore to add some files to Image when we creating new Image

We don't need to COPY node_modules folder to our Docker Image ,because in Dockerfile we run --> RUN npm install (which will add node_modules folder on installation)
Comand COPY . . <-- From Dockerfile - copy all files to new Image.

We can ignore / do not allow to copy certain files or folders that don't need to copied to Docker Image

- We create file in the Root --> (works the same as .gitignore)

```bash
.dockerignore
```

```JS
// .dockerignore file
//here we put files and folders that we don't need to COPY to Docker Image

node_modules
.git   //<-- don't need git folder, it is hidden it has -> system info
Dockerfile  //<-- we deon't need this in the Image, because it is instruction for Docker Image
```

# Work with Envaironment variables

We can use env varibales in Dockerfile.

3 different options how to use ENV variables in Docker

1. Instead of writing --> EXPOSE 3000 in our Dockerfile , we can use env variable

```JS
//Dockerfile instructions - EXAMPLE 4 (Correct version of instructions)


//# Use official Node.js image as base
//FROM node:16

FROM node //<--we take node image, name of the Image that we want to use for our environment. We tell that our Image is based on Node JS. When Docker will read this line, Docker we check if we have installed this image localy and if we don't have this Image locally -> Docker will download it from Docker Hub.


//working Directory where we keep our application. WORKDIR /app <-- means in folder /app , we have our application, where we have all our folders and files in the container
//# Set the working directory in the container
WORKDIR /app   ///identify Root directory/ working directory is -> /app (it is folder app), this app folder already exist in the project, if not existing -> it will create app folder in the container, where we have all request methods (GET, POST, DELETE, PUT) and app.listen code.listen code

COPY package.json .  //<-- we copy package.json to work directory --> app folder (in this case) --> app, where we keep all application.
//or
COPY package.json /app  //<-- This command to use if copy to our app.js file

//To run this Node app we need all dependencies from our app
RUN npm install  //RUN is used when we build an Image

//Now we need to set our Image, we need to set what files we want to move to new Image and keep it there. Image is used for read only. When you create the Image, you can't edit it. COPY command copy some certains Folders and files from our project to new Docker Image
COPY . . //<-- First of all we mention from where we want to copy our files and what we want to copy. First dot (COPY .) means we copy all folders and files from Root folder of our Project. Project Root place is located, where you have Dockerfile document. After that we declare -> where we want to paste those files into new Image. (COPY . .) --> Second dot means that we will paste all files to Root Image Folder. If we use --> WORKDIR /app before this, than we can write --> COPY . . - Because we will be located in /app folder after WORKDIR /app command.

ENV PORT 3000  // PORT is env variable, 3000 is a value of ENV variable

EXPOSE $PORT //<-- we use this syntaxis to use -> PORT variable. It tells what PORT will be used in Docker Container to run our application.
//This code means -> when we start this application, we will use PORT 3000

VOLUME [ "/app/data" ]  //<-- it is array where we indicate the path to current VOLUME

//To run our application
CMD ["node", "app.js"] //<-- an array, with few elements. Each element represent one command. -> Locally to start our application we write in terminal --> node app.js, Where app.js is file to run. Here is the same but all elements we put separately
//CMD command is running all the time when we convert our Image to Container
```

2. Also, we can pass Environment variable in command, check syntaxis below

```JS
docker run -d -p 3000:80 -e PORT=80 --rm --name myContainerName imageId  //<--  -e <--allow to add variable to command when we create container from Docker Image

docker run -d -p 3000:4200 -e PORT=80 -e USER=jim --rm --name myContainerName imageId //<--  -e <--allow to add ENV variable to command when we create container from Docker Image, if we want to add to add 2 ENV variables -> we add 2 -e and following with variable and value of ENV variable.

//-e USER=Jim will pass variable USER= JIM to my application. Usually used for .ENV hidden files
//Using the -e flag in your docker run command will set the environment variable inside the container. So, -e USER=Jim will pass the USER environment variable with the value Jim to your application running inside the container. it can be accessed by your application code.
//When your backend application is running inside the container, it can access this environment variable. In most backend applications, you can retrieve it like this: In Node.js (JavaScript/TypeScript): process.env.USER
```

3. We can create .env file in project Root where we will keep all ENV variables

this is best options, if we use many environment variables we can keep all of them in .env file

```JS
// .env file

PORT = 4200
USER = "JIM"

```

and to use the value from .env file we use -->

```JS
docker run -d -p 80:4200 --env-file ./env --rm --name myContainerName imageId   // we use ->  --env-file  and full path to .env file
```

# Make your work with Docker in more convenient way

- Install "make" package on your computer if it is not installed, usually it is installed in VScode. How to Install, write in google --> make install linux
- create file in the project Root --> Makefile (no extensions)
- It works the same way as --> package.json (scripts), where you can write write commands and use shortcuts to invoke this command

```JS
//Makefile

run:    //<-- name of command
    docker run -d -p 3000:3000 --name myapp --rm imageID:imageTag
stop:   //<-- name of command
    docker stop containerID_or_containerName
run-dev:
    docker run -d -p 3000:3000 -v myvolume:/app/data --name myapp --rm imageID:imageTag
```

- write in terminal -->

```JS
make run  //<-- to invoke our command
```

# Volumes

- Volumes it is a folder which is located somewhere in the Docker, which is used by Docker to hold some data for different containers.
- Container can contact this Volumes folder and get needed data from there.
- Volumes allow keep the data even if we deleted current container, and rebuild new container from the same Docker Image
- There are 2 types of volumes:
  - anonymous (there is no name to this volume, will be deleted after container is deleted)
  - name (save data in the volumes)

Without giving Volume names for container it will create annonymous volumes (it creates volume folder without a name - it has some random letters and numbers).
In this case, if we have some data in this container with anonymous volume, --> then when we will delete this container --> all data will be deleted.
For example: To Do List, all saved todo's will be deleted after container will be deleted.

To give volume name, we need to add 1 more parametr (-v) when we creating Container from Docker Image. Syntaxis of volumes -->

```JS
docker run -d -p 3000:3000 -v myvolume:/app/data --name myapp --rm imageID:imageTag

// -v myvolume:/app/data
// -v <-- volumes parametr
// myvolume: <-- any name, we give a volume name here
//  /app/data  <-- the path to volume folder, should be the same what we indicated in Dockerfile -> VOLUMES [ "/app/data" ]
```

# Create your own VOLUMES from console

- for example: we used command:

```JS
docker run -d -p 3000:3000 -v myvolume:/app/data --name myapp --rm imageID:imageTag   //<-- using this code,docker create volume name: myvolume , AUTOMATICALLY !!!
```

We can manualy create name volumes by isng this code in terminal -->

```JS
docker volume create volumeName
```

# Docker additional parameters that can be added to your code when you run container from the Image

    ```C#
    //Standard code to run container from your Image:
    docker run -d -p 3000:3000 -v myvolume:/app/data --name myapp --rm imageID:imageTag
    ```

1. Environment Variables (-e)
   Set environment variables inside the container. This can be used to configure the container during runtime.

   ```C#
   docker run -d -p 3000:4200 -e PORT=80 -e USER=jim --rm --name myContainerName imageId //<--  -e <--allow to add ENV variable to command when we create container from Docker Image, if we want to add to add 2 ENV variables -> we add 2 -e and following with variable and value of ENV variable.

   -e MY_ENV_VAR=value

   //Example:
   -e NODE_ENV=production

   //This parameter it sets the NODE_ENV environment variable = production
   //-e USER=Jim will pass variable USER= JIM to my application. Usually used for .ENV hidden files
   //Using the -e flag in your docker run command will set the environment variable inside the container. So, -e USER=Jim will pass the USER environment variable with the value Jim to your application running inside the container. it can be accessed by your application code.
   //When your backend application is running inside the container, it can access this environment variable. In most backend applications, you can retrieve it like this: In Node.js (JavaScript/TypeScript): process.env.USER
   ```

2. Port Mapping (-p)
   You have used port mapping in example above (-p 3000:3000), but you can specify multiple port mappings adding: -p 8080:80

   Multiple port mappings in Docker, it means that a container can expose multiple ports to the host machine, allowing external services (like a web browser or another container) to interact with the container on different ports.

   Multiple ports needs if:

   1. The Container Runs Multiple Services
      A single container might run multiple services that each listen on different ports. For example, if you're running a web application and a background worker inside the same container:
      - The web server might run on port 80 or 3000.
      - The background worker might use port 5000 for some kind of inter-service communication or monitoring.
        In such a case, you need to expose both ports to be able to communicate with the different services.

   ```C#
   //Example:
   docker run -d -p 3000:3000 -p 5000:5000 --name myapp imageID:imageTag

   //Port 3000 on the host maps to port 3000 in the container (for the web application).
   //Port 5000 on the host maps to port 5000 in the container (for the background worker).
   ```

   2. Services Use Non-Standard Ports

   Some applications might use non-standard ports or need specific port mappings that don’t overlap with each other. For example:

   - A container might be running a database on port 5432 (default for PostgreSQL).
   - The same container might also run an API on port 8080.
     You might want to expose both ports to the host so that external services can connect to both, even if they’re different services.

   ```C#
   //Example:
   docker run -d -p 5432:5432 -p 8080:8080 --name myapp imageID:imageTag

   //Port 5432 on the host is used to access the database inside the container.
   //Port 8080 on the host is used to access the API inside the container.
   ```

   3. Multiple Containers in a Multi-Service Setup

   If you're running a multi-container setup (often used with Docker Compose), each container might expose different ports for different services, and you may need to map these to different ports on the host. For example, one container may run a web server on port 80, another container may run a database on port 3306, and a third container may run a cache on port 6379.

   ```C#
   //Example:

   docker run -d -p 80:80 --name webserver webimage
   docker run -d -p 3306:3306 --name db dbimage
   docker run -d -p 6379:6379 --name cache cacheimage

   //Each of these services runs in a separate container but exposes different ports to the host.
   ```

   4. Accessing Services for Debugging or Monitoring
      Sometimes, you might expose additional ports to access tools like debugging interfaces or monitoring services:

   - For example, a container might run a web app on port 3000, but you may also want to expose a debugging interface on port 9229 or metrics for monitoring purposes on port 8081.

   ```C#
   //Example:

   docker run -d -p 3000:3000 -p 9229:9229 --name myapp imageID:imageTag
   //Port 3000 for the main app.
   //Port 9229 for debugging or remote inspection.
   ```

   5. Handling Legacy or Custom Applications
      Some legacy applications or custom setups might need to run on specific ports. You may need to expose multiple ports to make the application work as expected.

   Example of Multiple Port Mappings:
   Here's an example of how you might run a container that exposes several ports to handle different types of traffic:

   ```C#
   docker run -d
   -p 80:80 //Port for Web server (HTTP)
   -p 443:443 //Port for Web server (HTTPS)
   -p 3306:3306 //Port for Database (MySQL)
   --name myapp imageID:imageTag
   ```

   In this example:

   - Port 80 on the host is mapped to port 80 in the container (HTTP web traffic).
   - Port 443 on the host is mapped to port 443 in the container (HTTPS web traffic).
   - Port 3306 on the host is mapped to port 3306 in the container (MySQL database).
     This setup allows you to run multiple services within the container and expose them to the outside world via different ports.

   Conclusion:
   You need multiple port mappings when your container hosts multiple services, uses non-standard ports, or requires specific access configurations. Each port mapping allows you to expose a different service running inside the container to the host system or external services, enabling flexible communication between your container and the outside world.

3. Mounting Volumes (-v or --mount)

   ```C#
   -v myvolume:/app/data
   //the -v flag is used to mount a volume inside the container


   myvolume:/app/data
   //myvolume: This is the name of the volume. It's a Docker-managed volume, which is stored in Docker's internal storage. Docker will automatically manage this volume and its data, and it will persist even if the container is stopped or removed. If the volume myvolume does not exist, Docker will create it automatically.
   // /app/data: This is the path inside the container where the volume will be mounted. Essentially, this means that the contents of myvolume will be accessible at /app/data inside the container.
   ```

   What Does This Do?

   Data Persistence: When you mount a volume like this, it ensures that any data written by processes running inside the container to /app/data will be stored in the Docker-managed volume (myvolume). This data is persistent and will not be lost if the container is stopped or removed. This is especially useful for databases, file uploads, logs, or other data that you want to keep between container runs.
   Volume Location: Docker manages the location of the volume. By default, it stores volumes in a specific location on the host machine (in /var/lib/docker/volumes/ on Linux systems), but you don’t need to worry about the specific file system location unless you need to manually manage it.

   Example Usage
   If you're running a database inside the container and it writes data to /app/data, that data will be stored in the volume myvolume.
   If you restart or remove the container, the data in myvolume will remain intact and can be reused by new containers.

   To Summarize:
   The code -v myvolume:/app/data does not specify a location on the host machine directly. Instead, it tells Docker to use a Docker-managed volume (myvolume) and mount it at /app/data inside the container. This allows data to be preserved across container restarts and provides a way to share data between containers.

4. Docker can read .env files and load them into the container's environment in two ways:

   1. Using --env-file

   ```C#
   //If you want to load environment variables from an .env file, you can use the --env-file option.
   //For example, if you have a .env file with the following content:

   NODE_ENV=production
   DB_HOST=localhost
   ```

   ```C#
   //You can run the container and load the variables like this:
   docker run --env-file .env -d -p 3000:3000 --name myapp imageID:imageTag
   ```

   This way, Docker will read the .env file and inject the environment variables into the container.

   Docker will take the environment variables specified in the .env file and make them available inside the running container. This allows the container to access those variables as part of its environment, just like how environment variables work in a regular operating system.

   When you run the docker run --env-file .env command, Docker will read the .env file, extract the environment variables, and pass them to the container during startup. The environment variables from the .env file will be available inside the container just like they would be in a regular environment.

   If your Node.js application uses process.env.NODE_ENV, it will now receive the value production because that’s what you defined in the .env file.

   These variables can be accessed in your application like this (in Node.js, for example):

   2. Manually setting environment variables from .env

   ```C#
   //You can also manually load each environment variable from the .env file by sourcing it before running the docker run command:

   console.log(process.env.NODE_ENV); // Outputs: production
   console.log(process.env.DB_HOST);  // Outputs: localhost


   export $(cat .env | xargs)
   docker run -d -p 3000:3000 -e NODE_ENV=$NODE_ENV --name myapp imageID:imageTag
   //This uses cat .env | xargs to load environment variables from the .env file into your shell environment and then pass them to the docker run command using -e.
   ```

5. Container Restart Policies (--restart)

   Set a restart policy to determine what happens when the container stops. Options are no, always, unless-stopped, and on-failure.

   --restart=always

6. Network Mode (--network)

   You can specify a network for the container, such as host, bridge, or a custom network.

   --network=host

7. Attach Terminal (-it)
   If you want to interact with the container directly, use -it (interactive + TTY).

   -it

8. AND OTHER PARAMETERS
