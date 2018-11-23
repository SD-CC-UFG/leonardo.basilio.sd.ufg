var grpc = require('grpc');
var services = require('./grpc/NamingServer_grpc_pb');
var messages = require('./grpc/NamingServer_pb');

var client = new services.NamingClient('127.0.0.1:7777', grpc.credentials.createInsecure());

var request = new messages.RegistrationRequest();
request.setName(messages.ServiceType.AUTH);
request.setPort(50001);
request.setHealth(100);

client.registerService(request, (err, response) => {

    var request = new messages.ServiceRequest();
    request.setName(messages.ServiceType.AUTH);

    client.getServiceLocation(request, (err, response) => {
        console.log(response.toObject());
    });

 });
