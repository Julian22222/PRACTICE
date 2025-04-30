const myFetch = fetch("https://api.sampleapis.com/switch/games")

myFetch.then((data)=>console.log(data.json())) // <-- will be pending






const theFetch = fetch("https://api.sampleapis.com/switch/games")

theFetch.then((data)=>data.json()).then((games)=>{
    console.log(games)  // <-- wil give all the games from API
}).catch( err => console.log(err))