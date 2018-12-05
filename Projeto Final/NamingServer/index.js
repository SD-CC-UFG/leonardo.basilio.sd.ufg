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

function getMonotonicClock(){
    return process.hrtime()[0];
}

function getIPFromPeer(peer){

    var result = /^ipv4:(\d+\.\d+\.\d+\.\d+):\d+$/.exec(peer);

    return result.length == 2 ? result[1] : null;

}

function buildNodeId(call){

    return call.request.getName() + ':' + getIPFromPeer(call.getPeer()) + ':' + call.request.getPort();

}

function checkNextNode(node){

    var nextNode = servicesNextNode[node.type];

    if(!nextNode || nextNode.health < node.health){

        servicesNextNode[node.type] = node;

    }

}

function registerServiceSetPeers(newNode){

    var min1 = null;
    var min2 = null;

    if(newNode.peers.length >= 2) return;

    for(let id in servicesNodes){

        let node = servicesNodes[id];

        if(node.type == newNode.type && node.id != newNode.id){

            if(!min1 || min1.peers.length > node.peers.length){

                min1 = node;

            }else if(!min2 || min2.peers.length > node.peers.length){

                min2 = node;

            }

        }

    }

    if(newNode.peers.length == 0){
        if(min1){
            newNode.peers.push(min1);
            min1.peers.push(newNode);
        }
        if(min2){
            newNode.peers.push(min2);
            min2.peers.push(newNode);
        }
    }else{
        if(min1 && min1 != newNode.peers[0]){
            newNode.peers.push(min1);
            min1.peers.push(newNode);
        }else if(min2){
            newNode.peers.push(min2);
            min2.peers.push(newNode);
        }
    }

}

function registerServiceBuildResponse(node){

    var response = new messages.RegistrationResponse();

    response.setSuccess(true);
    response.setIp(node.ip);

    for(let peer of node.peers){

        let item = new messages.ServiceResponse();

        item.setIp(peer.ip);
        item.setPort(peer.port);

        response.addPeers(item);

    }

    return response;

}

function registerService(call, callback){

    var request = call.request;
    var nodeId = buildNodeId(call);
    var node = servicesNodes[nodeId];

    if(!node){

        log(registerService.name, nodeId);

        servicesNodes[nodeId] = node = {
            id: nodeId,
            ip: getIPFromPeer(call.getPeer()),
            port: request.getPort(),
            type: request.getName(),
            peers: []
        };

        if(!servicesNextNode[node.type]) {
            servicesNextNode[node.type] = node;
        }

    }

    node.health = request.getHealth();
    node.timestamp = getMonotonicClock();

    if(node.type == messages.ServiceType.MESSAGING){
        registerServiceSetPeers(node);
    }

    callback(null, registerServiceBuildResponse(node));

}

function unregisterService(node){

    if(servicesNextNode[node.type] == node){
        servicesNextNode[node.type] = null;
    }

    for(let myPeer of node.peers){
        let i = myPeer.peers.indexOf(node);
        if(i >= 0) myPeer.peers.splice(i, 1);
    }

    delete servicesNodes[node.id];

    log(unregisterService.name, node.id);

}

function ping(call, callback){

    var request = call.request;
    var response = new messages.PingResponse();
    var node = servicesNodes[buildNodeId(call)];

    if(node){

        if(node.health != request.getHealth()){

            node.health = request.getHealth();

            log(ping.name, node.id + ' Health = ' + node.health);

        }

        node.timestamp = getMonotonicClock();

        checkNextNode(node);

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

function runPingMonitor(){

    setInterval(function(){

        var servicesNodesKeys = Object.keys(servicesNodes);
        var limitTimestamp = getMonotonicClock()-5;

        for(let nodeId of servicesNodesKeys){
            let node = servicesNodes[nodeId];
            if(node.timestamp < limitTimestamp){
                unregisterService(node);
            }
        }

        for(let nodeId in servicesNodes){
            checkNextNode(servicesNodes[nodeId]);
        }

    }, 1000);

}

function runServer(){

    var server = new grpc.Server();

    server.addService(services.NamingService, {
        registerService: registerService,
        ping: ping,
        getServiceLocation: getServiceLocation
    })

    server.bind('0.0.0.0:7777', grpc.ServerCredentials.createInsecure());
    server.start();

}

runPingMonitor();
runServer();