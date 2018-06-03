
var express = require('express');
var app = express();
var mysql = require('mysql');
var util = require('util');

var connection = mysql.createConnection({
    host: '',
    user: '',
    password: '',
    database: 'Redrunner'
});

connection.connect(function (err) {
    if (err) {
        console.log('Error Connecting', err.stack);
        return;
    }
    console.log('Connected as id', connection.threadId);

});

app.get('/register', function (req, res) {
    
    var name = req.query.name;
    var password = req.query.pass;
    var email = req.query.email;

    var user = [[name,password,email,0]];

    Register(user,function(err,result){
        res.end(result);
    }); 
});

app.get('/user/update', function (req, res) {
    
    var name = req.query.name;
    var score = parseInt(req.query.score);

    UpdateScore(name,score,function(err,result){
        res.end(result);
    }); 
});

app.get('/top10users', function (req, res) {
    Top10Users(function(err,result){
        res.end(result);
    });
});

app.get('/login/:name/:password', function (req, res) {

    var name = req.params.name;
    var password = req.params.password;

    Login(function(err,result){
        res.end(result);
    }, name, password);
    
});

var server = app.listen(8081, function () {
    console.log('Server: Running');
});

function Login(callback, name, password) {

    var json = '';
    var sql = util.format('SELECT username,score FROM user WHERE username = "%s" AND password = "%s"', name, password);
    connection.query(sql,
        function (err, rows, fields) {
            if (err) throw err;

            json = JSON.stringify(rows);

            callback(null, json);
        });
}

function Register(user,callback) {

    var sql = 'INSERT INTO user(username, password, email, score) values ?';

    connection.query(sql,[user],
        function (err) {

            var result = '[{"success":"true"}]'

            if (err){
                result = '[{"success":"false"}]'
                throw err;

            }

            callback(null, result);
        });
}

function UpdateScore(name, score, callback){
    var sql = util.format('UPDATE user SET score = %s WHERE username = "%s" AND score < %s', score, name, score);

    connection.query(sql,
        function (err) {

            var result = '[{"success":"true"}]'

            if (err){
                result = '[{"success":"false"}]'
                throw err;

            }

            callback(null, result);
        });
}

function Top10Users(callback){
    var json = '';
    connection.query("SELECT username, score FROM user ORDER BY score DESC LIMIT 10;",
        function (err, rows, fields) {
            if (err) throw err;

            json = JSON.stringify(rows);

            callback(null, json);
        });
}
