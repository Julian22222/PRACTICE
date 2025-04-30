let p = new Promise((resolve,reject)=>{
    let a = 2;
    if(a===2){
        resolve("Success")
    }else{
        reject("Failed")
    }
})

p.then((res)=>{
    console.log(res) // Success
}).catch((res)=>{
    console.log(res)
})