# Using axios to make http requests

- Axios it is HTTP client using based on Promise for Browser and node.js
- Instal Axios from npm library

```JS
npm install axios
```

- to use axios for GET request to particular URL address , we need import axios and call it's method --> .get with URL address

```JS
import axios from 'axios';

console.log(axios.get("http://......"));   //<-- this will show Promise {<pending>} in console.log because axios methods return promise when you invoke the method
```

- using axios we use --> .then() and catch() methods and getting the access to response and an error ( the same wasy as in fetch)
- difference between axios and fetch, that axios will reject the promise if its status code of the response falls outside of the 2XX range. (200-299)

Make a GET request to URL

```JS
const axios = require("axios");

axios.get("sampleapis.com")
.then((res)=>{
    console.log(res.data)
})
.catch((err)=>{
    console.log(err)
})
```

# Axios Params

- somethimes requst conatains some data that user inserted (query) --> user inout

- to send data (query, body) from user to the server , axios have 2nd argument --> config object

--> see axios.js file (line 20 and 70)

```JS
axios.get('/data',{
    params: {             //<-- inside this object under params key, these data will be added to the URL address
        id: 12345,
        all_data; true
    }
});
```

```JS
const filterTerm = undefined;

axios.get('/data',{    //result of these request will contain --> /data?id=12345
    params:{
        id: 12345,
        filterTerm
    }
});
```
