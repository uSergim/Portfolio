#!/usr/bin/env nodejs

var express = require('express');
var https = require('https');
var http = require('http');
require("./global.js");

var app = express();

//decided to listen on both 3000 and 3443 ports for http and https request, although https requests probably
//wont work due to lack of digital certificate.
http.createServer(app).listen(3000, function(){console.log("listening on port 3000");});
var server = https.createServer({}, app).listen(3443, 
                                    function(){console.log("listening on port 3443");});

/*    ROUTES   */
app.use(require('./routes/linkedlist'));
/*              */