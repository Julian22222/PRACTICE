const posts =[
    {titel:"Post 1", body: "This is post 1"},
    {titel:"Post 2", body: "This is post 2"},
    {titel:"Post 3", body: "This is post 3"},
]

function addPost(newPost){
    return new Promise((resolve,reject)=>{
setTimeout(()=>{
    if(newPost){
        resolve(posts.push(newPost))
        // posts.push(newPost)
    }else{
        reject("error")
    }

},3000)
    })
}

addPost({titel:"Post 4", body: "This is post 4"}).then((data)=>{console.log(data)})
.catch(err=>console.log(err))

console.log(posts)