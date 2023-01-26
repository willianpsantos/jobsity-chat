const express = require('express');
const app = express();
const http = require('http').createServer(app);
const { Server }  =  require('socket.io');
const cors = require('cors');
const bodyParser = require('body-parser');

let ioConnected = false;
const host = 'localhost';
const port = 3003;
const whitelist = [ 'https://localhost:44478', 'http://localhost:44478', 'http://localhost:61199' ];

const jsonParser = bodyParser.json();

app.use((request, response, next) => {  
    if(whitelist.includes(request.baseUrl)) {  
        response.header('Access-Control-Allow-Origin', whitelist.join(','));
        response.header("Access-Control-Allow-Methods", 'GET,PUT,POST,DELETE');
    }

    app.use(cors());
    next();
});

const io = new Server({
    path: '/jobisty-messages',
    connectTimeout: 600000,
    allowEIO3: true,    
    allowRequest: (req, callback) => {
        callback(null, true);
    },
   
    cors: {        
        transports: ['websocket', 'polling'],
        credentials: true,    
        origin: whitelist,        
        methods: ["GET", "POST", "PUT", "GET"]
    }
});

io.on('connection', (socket) => {
    console.log('WebSocket server is online. Client ID: ' + socket.client.id);
    socket.emit('serverOnline');
    ioConnected = true;
});

app.post('/message/send', jsonParser, (request, response) => {
    if (!ioConnected) {
        response.end();
        return;
    }

    const body = request.body;

    console.log('Connection received.');    
    console.log(body);

    io.emit(body.emitEventName, body);

    response.json({  
        message: "Websocket event emitted",
        eventName: body.emitEventName,
        body
    });
    
    response.end();
});

http.listen(port, host, () => {    
    console.log(`App running on http://${host}:${port}`);
});

io.listen(http);
