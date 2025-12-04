# 🔥 API req, and export files notes

## API requests

```JS
//backend
//API and express


app.post  or put ("/:carId", async (req, res) => {
const {carId} = req.params    //Object destructuring

const {sort_by, order, topic} = req.query  // <--  request.query is another object that contains all the query parameters sent in the URL. For example, in a URL like ?sort_by=date&order=asc&topic=javascript, the query parameters are sort_by, order, and topic.

// This line uses destructuring to pull out the sort_by, order, and topic values directly from request.query and assign them to variables with the same names.

//OR -->

const {
  query: { sort_by, order, topic },
} = req;

const req.body
```

## Export and Import files

![pic01](https://github.com/Julian22222/PRACTICE/blob/main/Notes/IMG/export-import_files.JPG)

```JS
//export function

exports.fetchArticleId =()=>{
..//some code
}

//get exported functions from other files
const {
  fetchArticleId,
  updatedVote,
  listOfArticles,
} = require("../models/articleModels");
```

```JS
const  fetchArticleId =()=>{
..//some code
}

module.exports = fetchArticleId;
```

```JS
export const  fetchArticleId =()=>{
..//some code
}

import {fetchArticleId} from "./fileLocation";
```
