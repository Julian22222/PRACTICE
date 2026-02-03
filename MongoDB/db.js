// const { MongoClient } = require("mongodb");
// // The MongoClient class is a class that allows for making Connections to MongoDB.

// const URL = "mongodb://localhost:27017/moviebox";
// // localhost :27017from mongodb compass / moviebox -our Database

// let dbConnection;

// module.exports.connectToDb = (cb) => {
//   // cb -callback function
//   // function that connects mongoDB database to our project
//   // inside connect method we pass the path to our Database -URL,
//   // this method is async and return a promise
//   MongoClient.connect(URL)
//     .then((client) => {
//       // in case of successful connection we put message in console log
//       console.log("Connected to MongoDB");
//       dbConnection = client.db();
//       // dbConnection - we assign our result of Database connection
//       //assign db method result in client object
//       return cb();
//       // return the result of callback function
//       // can skip this line
//     })
//     .catch((err) => {
//       return cb(err);
//       // return callback with an error
//     });
// };

// module.exports.getDb = () => {
//   return dbConnection;
//   // this function return
// };
