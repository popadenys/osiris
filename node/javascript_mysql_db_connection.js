var mysql = require('mysql');
var express = require('express')
var app = express()
var url = require('url');
var https = require('https')
var fs = require('fs')


var pool  = mysql.createPool({
  connectionLimit : 10,
  host            : 'localhost',
  user            : 'root',
  password        : 'rootpassword',
  database        : 'mysqlcsharp'
});

https.createServer({
  key: fs.readFileSync('C:/Users/Admin/Desktop/node/security/server.key'),
  cert: fs.readFileSync('C:/Users/Admin/Desktop/node/security/server.crt')
}, app).listen(8080, () => {
  console.log('Listening...')
})

app.get('/', function(req, res) {
    res.send('Hello World');
})



app.get('/searchu', function(req, res) {

    pool.getConnection(function(err,connection) {
		if (err) {
            res.write("Baza de date offline");
			res.end()
        } else {

        var q = url.parse(req.url, true);
        var qdata = q.query;

        console.log(qdata.username);

        var searchquerry = "SELECT * FROM mysqlcsharp WHERE `username`= '" + qdata.username + "'";
        console.log(searchquerry);
        connection.query(searchquerry, function(err, result) {
            if (err) throw err;
            console.log("searched")
            console.log(result);
            res.setHeader('Content-Type', 'application/json');
            res.write(JSON.stringify(result))
            res.end()
			connection.release();

        });
		}

    })

})

app.get('/users', function(req,res){
	
	pool.getConnection(function(err,connection) {
		
		if (err) {
            res.write("Baza de date offline");
			res.end()
        } else {

        var querry = "SELECT * FROM mysqlcsharp "
        connection.query(querry, function(err, result) {
            if (err) throw err;
            console.log("searched")
            console.log(result);
            res.setHeader('Content-Type', 'application/json');
            res.write(JSON.stringify(result))
            res.end()
			connection.release();

        });
		}

    })
	
	
})





app.get('/updategriduser', function(req, res) {


    pool.getConnection(function(err,connection) {
        if (err) {
            res.write("Baza de date offline");
			res.end()
        } else {
            console.log("Connected!");

            var qu = url.parse(req.url, true)
            var qdatau = qu.query;
			
			var sql= "UPDATE `mysqlcsharp` SET `password`='" +qdatau.password+ "',`admin`='"+qdatau.admin+"' WHERE `username`='" + qdatau.username + "' "
            connection.query(sql, function(err, result) {
                if (err) {
                    res.write("Neexecutat")
                    res.end()
                }
                console.log(result.affectedRows + " record(s) updated");
                res.write("Executat")
                res.end()
				connection.release();
            });

        }

    })

})

