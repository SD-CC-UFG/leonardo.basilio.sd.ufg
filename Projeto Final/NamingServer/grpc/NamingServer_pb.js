/**
 * @fileoverview
 * @enhanceable
 * @suppress {messageConventions} JS Compiler reports an error if a variable or
 *     field starts with 'MSG_' and isn't a translatable message.
 * @public
 */
// GENERATED CODE -- DO NOT EDIT!

var jspb = require('google-protobuf');
var goog = jspb;
var global = Function('return this')();

goog.exportSymbol('proto.Chat.Grpc.PingRequest', null, global);
goog.exportSymbol('proto.Chat.Grpc.PingResponse', null, global);
goog.exportSymbol('proto.Chat.Grpc.RegistrationRequest', null, global);
goog.exportSymbol('proto.Chat.Grpc.RegistrationResponse', null, global);
goog.exportSymbol('proto.Chat.Grpc.ServiceRequest', null, global);
goog.exportSymbol('proto.Chat.Grpc.ServiceResponse', null, global);
goog.exportSymbol('proto.Chat.Grpc.ServiceType', null, global);

/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.Chat.Grpc.ServiceRequest = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.Chat.Grpc.ServiceRequest, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  proto.Chat.Grpc.ServiceRequest.displayName = 'proto.Chat.Grpc.ServiceRequest';
}


if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto suitable for use in Soy templates.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     com.google.apps.jspb.JsClassTemplate.JS_RESERVED_WORDS.
 * @param {boolean=} opt_includeInstance Whether to include the JSPB instance
 *     for transitional soy proto support: http://goto/soy-param-migration
 * @return {!Object}
 */
proto.Chat.Grpc.ServiceRequest.prototype.toObject = function(opt_includeInstance) {
  return proto.Chat.Grpc.ServiceRequest.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Whether to include the JSPB
 *     instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.Chat.Grpc.ServiceRequest} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.ServiceRequest.toObject = function(includeInstance, msg) {
  var f, obj = {
    name: jspb.Message.getFieldWithDefault(msg, 1, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.Chat.Grpc.ServiceRequest}
 */
proto.Chat.Grpc.ServiceRequest.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.Chat.Grpc.ServiceRequest;
  return proto.Chat.Grpc.ServiceRequest.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.Chat.Grpc.ServiceRequest} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.Chat.Grpc.ServiceRequest}
 */
proto.Chat.Grpc.ServiceRequest.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {!proto.Chat.Grpc.ServiceType} */ (reader.readEnum());
      msg.setName(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.Chat.Grpc.ServiceRequest.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.Chat.Grpc.ServiceRequest.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.Chat.Grpc.ServiceRequest} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.ServiceRequest.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getName();
  if (f !== 0.0) {
    writer.writeEnum(
      1,
      f
    );
  }
};


/**
 * optional ServiceType name = 1;
 * @return {!proto.Chat.Grpc.ServiceType}
 */
proto.Chat.Grpc.ServiceRequest.prototype.getName = function() {
  return /** @type {!proto.Chat.Grpc.ServiceType} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/** @param {!proto.Chat.Grpc.ServiceType} value */
proto.Chat.Grpc.ServiceRequest.prototype.setName = function(value) {
  jspb.Message.setField(this, 1, value);
};



/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.Chat.Grpc.ServiceResponse = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.Chat.Grpc.ServiceResponse, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  proto.Chat.Grpc.ServiceResponse.displayName = 'proto.Chat.Grpc.ServiceResponse';
}


if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto suitable for use in Soy templates.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     com.google.apps.jspb.JsClassTemplate.JS_RESERVED_WORDS.
 * @param {boolean=} opt_includeInstance Whether to include the JSPB instance
 *     for transitional soy proto support: http://goto/soy-param-migration
 * @return {!Object}
 */
proto.Chat.Grpc.ServiceResponse.prototype.toObject = function(opt_includeInstance) {
  return proto.Chat.Grpc.ServiceResponse.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Whether to include the JSPB
 *     instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.Chat.Grpc.ServiceResponse} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.ServiceResponse.toObject = function(includeInstance, msg) {
  var f, obj = {
    ip: jspb.Message.getFieldWithDefault(msg, 1, ""),
    port: jspb.Message.getFieldWithDefault(msg, 2, 0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.Chat.Grpc.ServiceResponse}
 */
proto.Chat.Grpc.ServiceResponse.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.Chat.Grpc.ServiceResponse;
  return proto.Chat.Grpc.ServiceResponse.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.Chat.Grpc.ServiceResponse} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.Chat.Grpc.ServiceResponse}
 */
proto.Chat.Grpc.ServiceResponse.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {string} */ (reader.readString());
      msg.setIp(value);
      break;
    case 2:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setPort(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.Chat.Grpc.ServiceResponse.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.Chat.Grpc.ServiceResponse.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.Chat.Grpc.ServiceResponse} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.ServiceResponse.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getIp();
  if (f.length > 0) {
    writer.writeString(
      1,
      f
    );
  }
  f = message.getPort();
  if (f !== 0) {
    writer.writeInt32(
      2,
      f
    );
  }
};


/**
 * optional string ip = 1;
 * @return {string}
 */
proto.Chat.Grpc.ServiceResponse.prototype.getIp = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 1, ""));
};


/** @param {string} value */
proto.Chat.Grpc.ServiceResponse.prototype.setIp = function(value) {
  jspb.Message.setField(this, 1, value);
};


/**
 * optional int32 port = 2;
 * @return {number}
 */
proto.Chat.Grpc.ServiceResponse.prototype.getPort = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 2, 0));
};


/** @param {number} value */
proto.Chat.Grpc.ServiceResponse.prototype.setPort = function(value) {
  jspb.Message.setField(this, 2, value);
};



/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.Chat.Grpc.RegistrationRequest = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.Chat.Grpc.RegistrationRequest, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  proto.Chat.Grpc.RegistrationRequest.displayName = 'proto.Chat.Grpc.RegistrationRequest';
}


if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto suitable for use in Soy templates.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     com.google.apps.jspb.JsClassTemplate.JS_RESERVED_WORDS.
 * @param {boolean=} opt_includeInstance Whether to include the JSPB instance
 *     for transitional soy proto support: http://goto/soy-param-migration
 * @return {!Object}
 */
proto.Chat.Grpc.RegistrationRequest.prototype.toObject = function(opt_includeInstance) {
  return proto.Chat.Grpc.RegistrationRequest.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Whether to include the JSPB
 *     instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.Chat.Grpc.RegistrationRequest} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.RegistrationRequest.toObject = function(includeInstance, msg) {
  var f, obj = {
    name: jspb.Message.getFieldWithDefault(msg, 1, 0),
    port: jspb.Message.getFieldWithDefault(msg, 3, 0),
    health: +jspb.Message.getFieldWithDefault(msg, 2, 0.0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.Chat.Grpc.RegistrationRequest}
 */
proto.Chat.Grpc.RegistrationRequest.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.Chat.Grpc.RegistrationRequest;
  return proto.Chat.Grpc.RegistrationRequest.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.Chat.Grpc.RegistrationRequest} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.Chat.Grpc.RegistrationRequest}
 */
proto.Chat.Grpc.RegistrationRequest.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {!proto.Chat.Grpc.ServiceType} */ (reader.readEnum());
      msg.setName(value);
      break;
    case 3:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setPort(value);
      break;
    case 2:
      var value = /** @type {number} */ (reader.readFloat());
      msg.setHealth(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.Chat.Grpc.RegistrationRequest.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.Chat.Grpc.RegistrationRequest.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.Chat.Grpc.RegistrationRequest} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.RegistrationRequest.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getName();
  if (f !== 0.0) {
    writer.writeEnum(
      1,
      f
    );
  }
  f = message.getPort();
  if (f !== 0) {
    writer.writeInt32(
      3,
      f
    );
  }
  f = message.getHealth();
  if (f !== 0.0) {
    writer.writeFloat(
      2,
      f
    );
  }
};


/**
 * optional ServiceType name = 1;
 * @return {!proto.Chat.Grpc.ServiceType}
 */
proto.Chat.Grpc.RegistrationRequest.prototype.getName = function() {
  return /** @type {!proto.Chat.Grpc.ServiceType} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/** @param {!proto.Chat.Grpc.ServiceType} value */
proto.Chat.Grpc.RegistrationRequest.prototype.setName = function(value) {
  jspb.Message.setField(this, 1, value);
};


/**
 * optional int32 port = 3;
 * @return {number}
 */
proto.Chat.Grpc.RegistrationRequest.prototype.getPort = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 3, 0));
};


/** @param {number} value */
proto.Chat.Grpc.RegistrationRequest.prototype.setPort = function(value) {
  jspb.Message.setField(this, 3, value);
};


/**
 * optional float health = 2;
 * @return {number}
 */
proto.Chat.Grpc.RegistrationRequest.prototype.getHealth = function() {
  return /** @type {number} */ (+jspb.Message.getFieldWithDefault(this, 2, 0.0));
};


/** @param {number} value */
proto.Chat.Grpc.RegistrationRequest.prototype.setHealth = function(value) {
  jspb.Message.setField(this, 2, value);
};



/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.Chat.Grpc.RegistrationResponse = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, proto.Chat.Grpc.RegistrationResponse.repeatedFields_, null);
};
goog.inherits(proto.Chat.Grpc.RegistrationResponse, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  proto.Chat.Grpc.RegistrationResponse.displayName = 'proto.Chat.Grpc.RegistrationResponse';
}
/**
 * List of repeated fields within this message type.
 * @private {!Array<number>}
 * @const
 */
proto.Chat.Grpc.RegistrationResponse.repeatedFields_ = [3];



if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto suitable for use in Soy templates.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     com.google.apps.jspb.JsClassTemplate.JS_RESERVED_WORDS.
 * @param {boolean=} opt_includeInstance Whether to include the JSPB instance
 *     for transitional soy proto support: http://goto/soy-param-migration
 * @return {!Object}
 */
proto.Chat.Grpc.RegistrationResponse.prototype.toObject = function(opt_includeInstance) {
  return proto.Chat.Grpc.RegistrationResponse.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Whether to include the JSPB
 *     instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.Chat.Grpc.RegistrationResponse} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.RegistrationResponse.toObject = function(includeInstance, msg) {
  var f, obj = {
    success: jspb.Message.getFieldWithDefault(msg, 1, false),
    ip: jspb.Message.getFieldWithDefault(msg, 2, ""),
    peersList: jspb.Message.toObjectList(msg.getPeersList(),
    proto.Chat.Grpc.ServiceResponse.toObject, includeInstance)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.Chat.Grpc.RegistrationResponse}
 */
proto.Chat.Grpc.RegistrationResponse.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.Chat.Grpc.RegistrationResponse;
  return proto.Chat.Grpc.RegistrationResponse.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.Chat.Grpc.RegistrationResponse} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.Chat.Grpc.RegistrationResponse}
 */
proto.Chat.Grpc.RegistrationResponse.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setSuccess(value);
      break;
    case 2:
      var value = /** @type {string} */ (reader.readString());
      msg.setIp(value);
      break;
    case 3:
      var value = new proto.Chat.Grpc.ServiceResponse;
      reader.readMessage(value,proto.Chat.Grpc.ServiceResponse.deserializeBinaryFromReader);
      msg.addPeers(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.Chat.Grpc.RegistrationResponse.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.Chat.Grpc.RegistrationResponse.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.Chat.Grpc.RegistrationResponse} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.RegistrationResponse.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getSuccess();
  if (f) {
    writer.writeBool(
      1,
      f
    );
  }
  f = message.getIp();
  if (f.length > 0) {
    writer.writeString(
      2,
      f
    );
  }
  f = message.getPeersList();
  if (f.length > 0) {
    writer.writeRepeatedMessage(
      3,
      f,
      proto.Chat.Grpc.ServiceResponse.serializeBinaryToWriter
    );
  }
};


/**
 * optional bool success = 1;
 * Note that Boolean fields may be set to 0/1 when serialized from a Java server.
 * You should avoid comparisons like {@code val === true/false} in those cases.
 * @return {boolean}
 */
proto.Chat.Grpc.RegistrationResponse.prototype.getSuccess = function() {
  return /** @type {boolean} */ (jspb.Message.getFieldWithDefault(this, 1, false));
};


/** @param {boolean} value */
proto.Chat.Grpc.RegistrationResponse.prototype.setSuccess = function(value) {
  jspb.Message.setField(this, 1, value);
};


/**
 * optional string ip = 2;
 * @return {string}
 */
proto.Chat.Grpc.RegistrationResponse.prototype.getIp = function() {
  return /** @type {string} */ (jspb.Message.getFieldWithDefault(this, 2, ""));
};


/** @param {string} value */
proto.Chat.Grpc.RegistrationResponse.prototype.setIp = function(value) {
  jspb.Message.setField(this, 2, value);
};


/**
 * repeated ServiceResponse peers = 3;
 * @return {!Array.<!proto.Chat.Grpc.ServiceResponse>}
 */
proto.Chat.Grpc.RegistrationResponse.prototype.getPeersList = function() {
  return /** @type{!Array.<!proto.Chat.Grpc.ServiceResponse>} */ (
    jspb.Message.getRepeatedWrapperField(this, proto.Chat.Grpc.ServiceResponse, 3));
};


/** @param {!Array.<!proto.Chat.Grpc.ServiceResponse>} value */
proto.Chat.Grpc.RegistrationResponse.prototype.setPeersList = function(value) {
  jspb.Message.setRepeatedWrapperField(this, 3, value);
};


/**
 * @param {!proto.Chat.Grpc.ServiceResponse=} opt_value
 * @param {number=} opt_index
 * @return {!proto.Chat.Grpc.ServiceResponse}
 */
proto.Chat.Grpc.RegistrationResponse.prototype.addPeers = function(opt_value, opt_index) {
  return jspb.Message.addToRepeatedWrapperField(this, 3, opt_value, proto.Chat.Grpc.ServiceResponse, opt_index);
};


proto.Chat.Grpc.RegistrationResponse.prototype.clearPeersList = function() {
  this.setPeersList([]);
};



/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.Chat.Grpc.PingRequest = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.Chat.Grpc.PingRequest, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  proto.Chat.Grpc.PingRequest.displayName = 'proto.Chat.Grpc.PingRequest';
}


if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto suitable for use in Soy templates.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     com.google.apps.jspb.JsClassTemplate.JS_RESERVED_WORDS.
 * @param {boolean=} opt_includeInstance Whether to include the JSPB instance
 *     for transitional soy proto support: http://goto/soy-param-migration
 * @return {!Object}
 */
proto.Chat.Grpc.PingRequest.prototype.toObject = function(opt_includeInstance) {
  return proto.Chat.Grpc.PingRequest.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Whether to include the JSPB
 *     instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.Chat.Grpc.PingRequest} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.PingRequest.toObject = function(includeInstance, msg) {
  var f, obj = {
    name: jspb.Message.getFieldWithDefault(msg, 1, 0),
    port: jspb.Message.getFieldWithDefault(msg, 3, 0),
    health: +jspb.Message.getFieldWithDefault(msg, 2, 0.0)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.Chat.Grpc.PingRequest}
 */
proto.Chat.Grpc.PingRequest.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.Chat.Grpc.PingRequest;
  return proto.Chat.Grpc.PingRequest.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.Chat.Grpc.PingRequest} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.Chat.Grpc.PingRequest}
 */
proto.Chat.Grpc.PingRequest.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {!proto.Chat.Grpc.ServiceType} */ (reader.readEnum());
      msg.setName(value);
      break;
    case 3:
      var value = /** @type {number} */ (reader.readInt32());
      msg.setPort(value);
      break;
    case 2:
      var value = /** @type {number} */ (reader.readFloat());
      msg.setHealth(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.Chat.Grpc.PingRequest.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.Chat.Grpc.PingRequest.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.Chat.Grpc.PingRequest} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.PingRequest.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getName();
  if (f !== 0.0) {
    writer.writeEnum(
      1,
      f
    );
  }
  f = message.getPort();
  if (f !== 0) {
    writer.writeInt32(
      3,
      f
    );
  }
  f = message.getHealth();
  if (f !== 0.0) {
    writer.writeFloat(
      2,
      f
    );
  }
};


/**
 * optional ServiceType name = 1;
 * @return {!proto.Chat.Grpc.ServiceType}
 */
proto.Chat.Grpc.PingRequest.prototype.getName = function() {
  return /** @type {!proto.Chat.Grpc.ServiceType} */ (jspb.Message.getFieldWithDefault(this, 1, 0));
};


/** @param {!proto.Chat.Grpc.ServiceType} value */
proto.Chat.Grpc.PingRequest.prototype.setName = function(value) {
  jspb.Message.setField(this, 1, value);
};


/**
 * optional int32 port = 3;
 * @return {number}
 */
proto.Chat.Grpc.PingRequest.prototype.getPort = function() {
  return /** @type {number} */ (jspb.Message.getFieldWithDefault(this, 3, 0));
};


/** @param {number} value */
proto.Chat.Grpc.PingRequest.prototype.setPort = function(value) {
  jspb.Message.setField(this, 3, value);
};


/**
 * optional float health = 2;
 * @return {number}
 */
proto.Chat.Grpc.PingRequest.prototype.getHealth = function() {
  return /** @type {number} */ (+jspb.Message.getFieldWithDefault(this, 2, 0.0));
};


/** @param {number} value */
proto.Chat.Grpc.PingRequest.prototype.setHealth = function(value) {
  jspb.Message.setField(this, 2, value);
};



/**
 * Generated by JsPbCodeGenerator.
 * @param {Array=} opt_data Optional initial data array, typically from a
 * server response, or constructed directly in Javascript. The array is used
 * in place and becomes part of the constructed object. It is not cloned.
 * If no data is provided, the constructed object will be empty, but still
 * valid.
 * @extends {jspb.Message}
 * @constructor
 */
proto.Chat.Grpc.PingResponse = function(opt_data) {
  jspb.Message.initialize(this, opt_data, 0, -1, null, null);
};
goog.inherits(proto.Chat.Grpc.PingResponse, jspb.Message);
if (goog.DEBUG && !COMPILED) {
  proto.Chat.Grpc.PingResponse.displayName = 'proto.Chat.Grpc.PingResponse';
}


if (jspb.Message.GENERATE_TO_OBJECT) {
/**
 * Creates an object representation of this proto suitable for use in Soy templates.
 * Field names that are reserved in JavaScript and will be renamed to pb_name.
 * To access a reserved field use, foo.pb_<name>, eg, foo.pb_default.
 * For the list of reserved names please see:
 *     com.google.apps.jspb.JsClassTemplate.JS_RESERVED_WORDS.
 * @param {boolean=} opt_includeInstance Whether to include the JSPB instance
 *     for transitional soy proto support: http://goto/soy-param-migration
 * @return {!Object}
 */
proto.Chat.Grpc.PingResponse.prototype.toObject = function(opt_includeInstance) {
  return proto.Chat.Grpc.PingResponse.toObject(opt_includeInstance, this);
};


/**
 * Static version of the {@see toObject} method.
 * @param {boolean|undefined} includeInstance Whether to include the JSPB
 *     instance for transitional soy proto support:
 *     http://goto/soy-param-migration
 * @param {!proto.Chat.Grpc.PingResponse} msg The msg instance to transform.
 * @return {!Object}
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.PingResponse.toObject = function(includeInstance, msg) {
  var f, obj = {
    success: jspb.Message.getFieldWithDefault(msg, 1, false)
  };

  if (includeInstance) {
    obj.$jspbMessageInstance = msg;
  }
  return obj;
};
}


/**
 * Deserializes binary data (in protobuf wire format).
 * @param {jspb.ByteSource} bytes The bytes to deserialize.
 * @return {!proto.Chat.Grpc.PingResponse}
 */
proto.Chat.Grpc.PingResponse.deserializeBinary = function(bytes) {
  var reader = new jspb.BinaryReader(bytes);
  var msg = new proto.Chat.Grpc.PingResponse;
  return proto.Chat.Grpc.PingResponse.deserializeBinaryFromReader(msg, reader);
};


/**
 * Deserializes binary data (in protobuf wire format) from the
 * given reader into the given message object.
 * @param {!proto.Chat.Grpc.PingResponse} msg The message object to deserialize into.
 * @param {!jspb.BinaryReader} reader The BinaryReader to use.
 * @return {!proto.Chat.Grpc.PingResponse}
 */
proto.Chat.Grpc.PingResponse.deserializeBinaryFromReader = function(msg, reader) {
  while (reader.nextField()) {
    if (reader.isEndGroup()) {
      break;
    }
    var field = reader.getFieldNumber();
    switch (field) {
    case 1:
      var value = /** @type {boolean} */ (reader.readBool());
      msg.setSuccess(value);
      break;
    default:
      reader.skipField();
      break;
    }
  }
  return msg;
};


/**
 * Serializes the message to binary data (in protobuf wire format).
 * @return {!Uint8Array}
 */
proto.Chat.Grpc.PingResponse.prototype.serializeBinary = function() {
  var writer = new jspb.BinaryWriter();
  proto.Chat.Grpc.PingResponse.serializeBinaryToWriter(this, writer);
  return writer.getResultBuffer();
};


/**
 * Serializes the given message to binary data (in protobuf wire
 * format), writing to the given BinaryWriter.
 * @param {!proto.Chat.Grpc.PingResponse} message
 * @param {!jspb.BinaryWriter} writer
 * @suppress {unusedLocalVariables} f is only used for nested messages
 */
proto.Chat.Grpc.PingResponse.serializeBinaryToWriter = function(message, writer) {
  var f = undefined;
  f = message.getSuccess();
  if (f) {
    writer.writeBool(
      1,
      f
    );
  }
};


/**
 * optional bool success = 1;
 * Note that Boolean fields may be set to 0/1 when serialized from a Java server.
 * You should avoid comparisons like {@code val === true/false} in those cases.
 * @return {boolean}
 */
proto.Chat.Grpc.PingResponse.prototype.getSuccess = function() {
  return /** @type {boolean} */ (jspb.Message.getFieldWithDefault(this, 1, false));
};


/** @param {boolean} value */
proto.Chat.Grpc.PingResponse.prototype.setSuccess = function(value) {
  jspb.Message.setField(this, 1, value);
};


/**
 * @enum {number}
 */
proto.Chat.Grpc.ServiceType = {
  AUTH: 0,
  MESSAGING: 1
};

goog.object.extend(exports, proto.Chat.Grpc);