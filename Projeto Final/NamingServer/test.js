var grpc = require('grpc');
var services = require('./grpc/NamingServer_grpc_pb');
var messages = require('./grpc/NamingServer_pb');

var client = new services.NamingClient('127.0.0.1:7777', grpc.credentials.createInsecure());

function registerMessaging(portNumber, howMany){

    var request = new messages.RegistrationRequest();
    request.setName(messages.ServiceType.MESSAGING);
    request.setPort(portNumber);
    request.setHealth(100);

    client.registerService(request, (err, response) => {

        console.log(response.toObject());

        if(howMany > 1){
            setTimeout(() => {
                registerMessaging(portNumber+1, howMany-1);
            }, 1000);
        }

    });

}

registerMessaging(1000, 5);