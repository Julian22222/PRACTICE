const http = require("http");


const server = http.createServer((request,response)=>{
// console.log(request.statusCode)
// console.log(request.url)
// console.log(request.method)
if(request.url === "/"){
response.setHeader("Content-Type", "application/json");
response.statusCode=200;
response.write(JSON.stringify({msg: "Hello I AM WORKING"}));
response.end()
}

if(request.url === "/users"){
response.setHeader("Content-Type", "application/json");
response.statusCode=200;
response.write(JSON.stringify({msg: "Hello I AM USER"}));
response.end()
}
})
// .catch(()=>{
//     console.log("Something went wrong")
// })

server.listen(9090, (err)=>{
    if (err){
        console.log(err)
    }else{
        console.log("Server is listening on Port 9090...")
    }
})