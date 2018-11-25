var grpc = require('grpc');
var services = require('./grpc/NamingServer_grpc_pb');
var messages = require('./grpc/NamingServer_pb');

var client = new services.NamingClient('127.0.0.1:7777', grpc.credentials.createInsecure());

for(let i = 10 ; i < 16 ; i++){

    var request = new messages.RegistrationRequest();
    request.setName(messages.ServiceType.MESSAGING);
    request.setPort(i);
    request.setHealth(100);

    client.registerService(request, (err, response) => {

        var request = new messages.ServiceRequest();
        request.setName(messages.ServiceType.MESSAGING);

        console.log(response.toObject());

        /*client.getServiceLocation(request, (err, response) => {
            console.log(response.toObject());
        });*/

    });

}