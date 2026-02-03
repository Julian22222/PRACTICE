const express = require("express");
const app = express();

app.get("/",(req,res)=>{
res.status(200).send({msg:"Hello WORLD!"})
})

app.get("/about",(req,res)=>{
    res.status(200).send({msg:"This is about ........"})
})

app.listen(9090,(err)=>{
    if(err){
        console.log(err)
    }else{
        console.log("Server is listening on Port 9090...")
    }
})