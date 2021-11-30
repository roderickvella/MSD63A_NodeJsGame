const express = require("express");
const cors = require("cors");
const db = require("./app/models");

const app = express();


// parse requests of content-type - application/json
app.use(express.json());

// parse requests of content-type - application/x-www-form-urlencoded
app.use(express.urlencoded({ extended: true }));

//sync database
//db.sequelize.sync();
db.sequelize.sync({force: true}).then(() => {
  console.log('Drop and Resync Db');  
});


// simple route
app.get("/", (req, res) => {
  res.json({ message: "Welcome to backend application." });
});

//define other routes
require('./app/routes/auth.routes')(app);


// set port, listen for requests
const PORT = process.env.PORT || 8080;
app.listen(PORT, () => {
  console.log(`Server is running on port ${PORT}.`);
});