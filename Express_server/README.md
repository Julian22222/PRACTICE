# install dependencies (in terminal --> npm i express, as example)

- express
- cors
- nodemon //restart,update the server anytime you change something and save it in the app, (works as --watch), install as Developer dependency
- dotenv //allow to use .ENV file and hide passwords, connection strings and etc. (get access through process.env. )
- pg //postgreSQL database
- jest // to make TDD, install as Developer dependency
- supertest // install as Developer dependency

.................................................................................................

# Ways to handle asynchronious requests:

1. Promises
2. Async / await
3. Callbacks

..................................................................................................

1. MVC pattern ( Promises )

## app.js

```JS
const express = require("express");  // assign express to variale, to use it in our app
const app = express();  //

const cors = require("cors");  // assign cors to variale, to use it in our app
app.use(cors());  // in order for your server to be accessible by other origins (domains)

app.use(express.json());  // to access --> req.body (middleware function),
require('ditenv').config();  // to access .ENV file through  --> process.env.

//if we use --> .ENV.test or .ENV.Development we put ->
//require('ditenv').config({path:'${--dirname}/../.env.development'});  <--the path to the correct file


const {getTopics} = require(../controllers/topicsController.js); // destructuring getTopics, request function from controllers


app.get("api/topics", getTopics);  //requst to "api/topics" and invoking getTopics function in the controller
app.get("api/topics/:topicId", getTopicId);  // parametric end-points,
app.get("api/book?name=Simba&favorite=true");  //queries


app.patch("/api/articles/:article_id", patchArticleId);
app.post("/api/articles/:article_id/comments", postComments);
app.delete("/api/comments/:comment_id", deleteComment);



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
if(err.status && err.msg){  //custome errors comming from models, where we put --> Promis.reject({status: ..., msg: " some message here"})
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

## topicsController.js

```JS
const {fetchTopics} = require("../models/categoriesModels");  //require function from models

exports.getTopics = (req,res,next)=>{
const {topicId} = req.params;  //will get Id from URL, is used to make a request into database
const {name} = req.query;  //will get the name from URL --> Simba
const {dataFromUser} = req.body;  //will get the data from front-end, UI that user used with POST, PATCH or PUT method


//here in fetchTopics(topicId, name, dataFromUser).then({category}=>{.......}) we can pass any variables to the Model
fetchTopics().then((category)=>{
res.status(200).send({category});
//or ({category:category})  , or re.send(category[0]);
}).catch(next);  //if there is any error ,it will pass it to app.js app.use(err,req,res,next) block
};

```

## categoriesModels.js

```JS

const db = require("../db/connection.js");  //assign data from database from separate file


// if we passed some data in controllers , then we need to receive it here -->exports.fetchTopics =(topicId, name, dataFromUser)=>{....}
exports.fetchTopics =()=>{

//GROUPBY and/or ORDERBY not supported Parammetrs in PSQL requests (User can pass any data to database using this parammers -> GROUPBY, ORDERBY), therefore to avoid SQL injection we need to write some logic-->
//we create array with the the same options from filter or any dropdown menu if you have on the Front-End
cons validOrder = ["asc", "desc"];  //<--example of how to filter the list order from Front-End,  we put here only options from the Fron-End --> "asc" and "desc"



// if(!validOrder.includes(sort_by)){ <-- As example, sort_by will come from Front-End and it will have value asc or desc
if(!validOrder.includes(someDataThatAreCommingFromFrontEnd)){
    return Promise.reject({status:404, msg: "this is Error, Bad request"});
}

    return db.query("SELECT * FROM topics").then({rows})=>{ //<-- topics here is name of the Table

    // if(rows.length ===0){
    //     return Promise.reject({status:400, msg;"Not Found"});
    // }
        return rows;
    }
}
```

..............................................................................................................

2. Async / Await (with Try Catch )

## server.js

```JS
const express = require("express");  // assign express to variale, to use it in our app
const app = express();  //

const cors = require("cors");  // assign cors to variale, to use it in our app
app.use(cors());  // in order for your server to be accessible by other origins (domains)

app.use(express.json());  // to access --> req.body (middleware function),

const pool = require("../db/....");  //connction to the data from database

const PORT = process.env.PORT || 9090  //if process.env.PORT not existing then will use PORT=9090
//this is the same --> const PORT = process.env.PORT ?? 9090


app.get("api/topics", async (req,res)=>{

    try{

    const topics = await pool.query(`SELECT * FROM topics`)
    res.json(topics.rows)

    catch(err){
        console.error(err)
    }}

});


app.listen(PORT,(err)=>{
    err ? console.log(err) : console.log(`Server is listening on PORT ${PORT}`)
})
```

.....................................................................................................

3. Callbacks (See--> MySQL-database project)

## app.js

```JS
const express = require("express");  // assign express to variale, to use it in our app
const app = express();  //

const cors = require("cors");  // assign cors to variale, to use it in our app
app.use(cors());  // in order for your server to be accessible by other origins (domains)

app.use(express.json());  // to access --> req.body (middleware function),
require('ditenv').config();  // to access .ENV file through  --> process.env.

const {getCatById} = require(../controllers/cats.controllers.js); // destructuring getCatById, request function from controllers


app.get("api/topics/:CatId", getCatById);  //requst to "api/topics/:CatId" and invoking getCatById function in the controller

```

## cats.controllers.js

```JS
const {selectCatById} = require('../models/cats.models.js');

exports.getCatById = (req,res)=>{
    const {CatId} = req.params;

    //using callback here
    selectCatById(catId, (err,cat)=>{  //err --if there is no catId it will invoke --> err (catId ===banana,atId==99999), if there is CatId it will invoke cat (cat -> can be any name here)
        res.status(200).send({cat}); //cat <--the same name as we gave it for data , if CatId existing
    });
};
```

## cats.models.js

```JS
exports.selectCatById = (catId, callback)=>{
    const data = db.query("SELECT * FROM topics WHERE cat = $1, [catId]");

    if(data.length ===0){
     callback(err);  //if there is no data for CatId will send an error
    }
else{
    const parsedCat = JSON.parse(data);
    callback(null, parsedCat);

}

    }

```
