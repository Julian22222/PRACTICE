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

docker run -p 50:3000 -d --name any_container_name ImageId   //<-- we can pass extra parametrs when we start certain container, we can set a name for new container

docker run -d -p 3000:3000 --name myapp --rm imageID  //<-- another paramer -> rm. Means as soon as we stop this running container it will be deleted from container list, (docker ps -a) <-- this command won't show this container. myapp <-- is the name of our new container

docker run -d -p 3000:3000 --name myapp --rm imageID:imageTag  //<-- if we have Images with the same names we can specify which image to convert to container by adding imageTag --> :imageTag

docker run -d -p 3000:3000 -v myvolume:/app/data --name myapp --rm imageID:imageTag   //<-- another paramer -v . Using this code,docker create volume name: myvolume , AUTOMATICALLY !!!
// -v myvolume:/app/data
// -v <-- volumes parametr
// myvolume: <-- any name, we give a volume name here
//  /app/data  <-- the path to volume folder, should be the same what we indicated in Dockerfile -> VOLUMES [ "/app/data" ]


docker logs containerID_or_containerName //<-- will show all activity in current container, show what Docker container Port is used fro our running application etc.

docker ps --help //<-- will show what this command does, and show description. use --> --help for any of the commands, don't need to memorise all the Docker commands
docker ps //<-- show the list of all running/active containers
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

//working Directory where we keep our application. WORKDIR /app <-- means in folder /app , we have our application, where we have all our folders and files
WORKDIR /app   ///identify Root directory/ working directory is -> /app (it is file app.js), app.js file already exist in the project, where we have all request methods (GET, POST, DELETE, PUT) and app.listen code.listen code

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

FROM node //<--we take node image, name of the Image that we want to use for our environment. We tell that our Image is based on Node JS. When Docker will read this line, Docker we check if we have installed this image localy and if we don't have this Image locally -> Docker will download it from Docker Hub.

//working Directory where we keep our application. WORKDIR /app <-- means in folder /app , we have our application, where we have all our folders and files
WORKDIR /app   ///identify Root directory/ working directory is -> /app (it is file app.js), app.js file already exist in the project, where we have all request methods (GET, POST, DELETE, PUT) and app.listen code.listen code

COPY package.json .  //<-- we copy package.json to work directory or to our folder --> app, where we keep all application.
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
