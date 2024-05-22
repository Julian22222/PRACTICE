# install dependencies

- express
- cors
- nodemon //restart,update the server anytime you change something and save it in the app, (works as --watch), install as Developer dependency
- dotenv //allow to use .ENV file and hide passwords, connection strings and etc.
- pg //postgreSQL database
- jest // to make TDD, install as Developer dependency
- supertest // install as Developer dependency

.................................................................................................

# app.js

```JS
const express = require("express");  // assign express to variale, to use it in our app
const app = express();  //

const cors = require("cors");  // assign cors to variale, to use it in our app
app.use(cors());  // in order for your server to be accessible by other origins (domains)

app.use(express.json());  // to access --> req.body (middleware function),


const {getTopics} = require(../controllers/topicsController.js); // destructuring getTopics, request function from controllers


app.get("api/topics", getTopics);  //requst to "api/topics" and invoking getTopics function in the controller
app.get("api/topics/:topicId", getTopicId);  // parametric end-points,
app.get("api/book?name=Simba&favorite=true");  //queries




app.use((err,req,res,next)=>{
if(err.code === "P2202"){
    res.status(400).send({"msg: Bad request, invalid topic Id"})
}else if(err.code === "D33333"){
res.status(400).send({"msg: some message here"})
}else{
    next(err);  //move to another next middleware function in the chain
}
});


app.use((err,req,res,next)=>{
if(err.status && err.msg){
    res.status(err.status).send({err.msg})
}else{
    next(err);
}
});


app.use((err,req,res,next)=>{
res.status(500).send({"msg: Internal server error"})
});



module.exports = app;


app.listen(3000, (err) => {
  err ? console.log(err) : console.log("Server is listening on Port 3000");
});
```

# topicsController.js

```JS
const {fetchTopics} = require("../models/categoriesModels");  //require function from models

exports.getTopics = (req,res,next)=>{
const {topicId} = req.params;  //will get Id from URL, is used to make a request into database
const {name} = req.query;  //will get the name from URL --> Simba
const {dataFromUser} = req.body;  //will get the data from front-end, UI that user used with POST, PATCH or PUT method


//here in fetchTopics(topicId, name, dataFromUser).then({category}=>{.......}) we can pass any variables to the Model
fetchTopics().then((category)=>{
res.status(200).send({category});
//or ({category:category})
}).catch(next);  //if there is any error ,it will pass it to app.js app.use(err,req,res,next) block
};

```

# categoriesModels.js

```JS

const db = require("../db/connection.js");  //assign data from database from separate file


// if we passed some data in controllers , then we need to receive it here -->exports.fetchTopics =(topicId, name, dataFromUser)=>{....}
exports.fetchTopics =()=>{


if(!articleSort.includes(someData)){
    return Promise.reject({status:400, msg: "this is Error"});
}

    return db.query("SELECT * FROM topics").then({rows})=>{ //<-- topics here is name of the Table
        return rows;
    }
}
```
