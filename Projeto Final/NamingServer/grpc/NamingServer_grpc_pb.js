// GENERATED CODE -- DO NOT EDIT!

'use strict';
var grpc = require('grpc');
var NamingServer_pb = require('./NamingServer_pb.js');

function serialize_Chat_Grpc_PingRequest(arg) {
  if (!(arg instanceof NamingServer_pb.PingRequest)) {
    throw new Error('Expected argument of type Chat.Grpc.PingRequest');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_Chat_Grpc_PingRequest(buffer_arg) {
  return NamingServer_pb.PingRequest.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_Chat_Grpc_PingResponse(arg) {
  if (!(arg instanceof NamingServer_pb.PingResponse)) {
    throw new Error('Expected argument of type Chat.Grpc.PingResponse');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_Chat_Grpc_PingResponse(buffer_arg) {
  return NamingServer_pb.PingResponse.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_Chat_Grpc_RegistrationRequest(arg) {
  if (!(arg instanceof NamingServer_pb.RegistrationRequest)) {
    throw new Error('Expected argument of type Chat.Grpc.RegistrationRequest');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_Chat_Grpc_RegistrationRequest(buffer_arg) {
  return NamingServer_pb.RegistrationRequest.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_Chat_Grpc_RegistrationResponse(arg) {
  if (!(arg instanceof NamingServer_pb.RegistrationResponse)) {
    throw new Error('Expected argument of type Chat.Grpc.RegistrationResponse');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_Chat_Grpc_RegistrationResponse(buffer_arg) {
  return NamingServer_pb.RegistrationResponse.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_Chat_Grpc_ServiceRequest(arg) {
  if (!(arg instanceof NamingServer_pb.ServiceRequest)) {
    throw new Error('Expected argument of type Chat.Grpc.ServiceRequest');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_Chat_Grpc_ServiceRequest(buffer_arg) {
  return NamingServer_pb.ServiceRequest.deserializeBinary(new Uint8Array(buffer_arg));
}

function serialize_Chat_Grpc_ServiceResponse(arg) {
  if (!(arg instanceof NamingServer_pb.ServiceResponse)) {
    throw new Error('Expected argument of type Chat.Grpc.ServiceResponse');
  }
  return new Buffer(arg.serializeBinary());
}

function deserialize_Chat_Grpc_ServiceResponse(buffer_arg) {
  return NamingServer_pb.ServiceResponse.deserializeBinary(new Uint8Array(buffer_arg));
}


var NamingService = exports.NamingService = {
  registerService: {
    path: '/Chat.Grpc.Naming/RegisterService',
    requestStream: false,
    responseStream: false,
    requestType: NamingServer_pb.RegistrationRequest,
    responseType: NamingServer_pb.RegistrationResponse,
    requestSerialize: serialize_Chat_Grpc_RegistrationRequest,
    requestDeserialize: deserialize_Chat_Grpc_RegistrationRequest,
    responseSerialize: serialize_Chat_Grpc_RegistrationResponse,
    responseDeserialize: deserialize_Chat_Grpc_RegistrationResponse,
  },
  getServiceLocation: {
    path: '/Chat.Grpc.Naming/GetServiceLocation',
    requestStream: false,
    responseStream: false,
    requestType: NamingServer_pb.ServiceRequest,
    responseType: NamingServer_pb.ServiceResponse,
    requestSerialize: serialize_Chat_Grpc_ServiceRequest,
    requestDeserialize: deserialize_Chat_Grpc_ServiceRequest,
    responseSerialize: serialize_Chat_Grpc_ServiceResponse,
    responseDeserialize: deserialize_Chat_Grpc_ServiceResponse,
  },
  ping: {
    path: '/Chat.Grpc.Naming/Ping',
    requestStream: false,
    responseStream: false,
    requestType: NamingServer_pb.PingRequest,
    responseType: NamingServer_pb.PingResponse,
    requestSerialize: serialize_Chat_Grpc_PingRequest,
    requestDeserialize: deserialize_Chat_Grpc_PingRequest,
    responseSerialize: serialize_Chat_Grpc_PingResponse,
    responseDeserialize: deserialize_Chat_Grpc_PingResponse,
  },
};

exports.NamingClient = grpc.makeGenericClientConstructor(NamingService);
