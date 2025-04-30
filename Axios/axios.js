const axios = require("axios");

///The same request or post outcomes with different methods

// GET request <<<<<<<<<

axios
  .get("https://fortnite-api.com/v1/map")
  .then((res) => {
    console.log(res.data);
  })
  .catch((error) => {
    console.error(error);
  });

////////////////////////////////////////////////////////////////////////////////////////////////////////
// GET request <<<<<<<<<

// axios.get("https://fortnite-api.com/v1/map",{
//     params:{
//         lang: "en"
//     },
//     headers:{
//         "Content-Type": "application/json",
//         // Authorization: "api -key need to insert",
//     }
// })
// .then(res=>{
//     console.log(res.data)
// }).catch(error => {
//     console.error(error)
// })

///////////////////////////////////////////////////////////////////////////////////////////////////
// POST request <<<<<<<<<

// axios.post("https://jsonplaceholder.typicode.com/posts",{
// userId : 1,
// title: "My title",
// body : "This is my body...."
// })
// .then(res=>{
//     console.log(res.data)
// }).catch(error => {
//     console.error(error)
// })

////////////////////////////////
// POST request <<<<<<<<<

// axios({
//     method: "POST",
//     url: "https://jsonplaceholder.typicode.com/posts",
//     data: {
//         userId : 1,
//         title: "My title",
//         body : "This is my body...."}
// })
// .then(res=>{
//         console.log(res.data)
//     }).catch(error => {
//         console.error(error)
//     })

/////////////////////////////////////////////////////////////////////////////////////////////////////
// GET request <<<<<<<<<

// axios({
//     url: "https://jsonplaceholder.typicode.com/posts",
//     params: {
//         lang: "en"
//     },
//     headers: {
//         "Content-Type": "application/json"
//     }
// })
// .then(res=>{
//         console.log(res.data)
//     }).catch(error => {
//         console.error(error)
//     })

//////////////////////////////////////////////////////////////////////////////////////////////////
// GET request <<<<<<<<<

// fetch("https://fortnite-api.com/v1/map")
// .then(res=>res.json())
// .then((data)=>{
//     console.log(data)
// }).catch(error => {
//         console.error(error)
//     })
