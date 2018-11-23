var grpc = require('grpc');
var services = require('./grpc/NamingServer_grpc_pb');
var messages = require('./grpc/NamingServer_pb');
var process = require('process');

//=================================

var servicesNodes = {};
var servicesNextNode = {};

function log(fn, s){
    var now = new Date();
    console.log(`${now.toLocaleTimeString()} [${fn}] ${s}`);
}

function getIPFromPeer(peer){

    var result = /^ipv4:(\d+\.\d+\.\d+\.\d+):\d+$/.exec(peer);

    return result.length == 2 ? result[1] : null;

}

function buildNodeId(call){

    return call.request.getName() + ':' + getIPFromPeer(call.getPeer()) + ':' + call.request.getPort();

}

function checkNextNode(name, node){

    var nextNode = servicesNextNode[name];

    if(!nextNode || nextNode.health < node.health){

        servicesNextNode[name] = node;

    }

}

function registerService(call, callback){

    var response = new messages.RegistrationResponse();
    var request = call.request;

    var nodeId = buildNodeId(call);
    var node = servicesNodes[nodeId];

    if(!node){

        log(registerService.name, nodeId);

        servicesNodes[nodeId] = node = {
            ip: getIPFromPeer(call.getPeer()),
            port: request.getPort(),
            health: request.getHealth(),
            timestamp: process.hrtime()[0]
        };

        checkNextNode(request.getName(), node);

    }

    response.setSuccess(true);

    callback(null, response);

}

function ping(call, callback){

    var response = new messages.PingResponse();
    var node = servicesNodes[buildNodeId(call)];

    if(node){

        node.timestamp = process.hrtime()[0];

        checkNextNode(call.request.getName(), node);

    }

    response.setSuccess(!!node);

    callback(null, response);

}

function getServiceLocation(call, callback){

    var nextNode = servicesNextNode[call.request.getName()];

    if(nextNode){

        var response = new messages.ServiceResponse();

        response.setIp(nextNode.ip);
        response.setPort(nextNode.port);

        callback(null, response);

    }else{

        callback(new Error('Serviço indisponível.'));

    }

}

//=================================

var server = new grpc.Server();

server.addService(services.NamingService, {
    registerService: registerService,
    ping: ping,
    getServiceLocation: getServiceLocation
})

server.bind('0.0.0.0:7777', grpc.ServerCredentials.createInsecure());
server.start();