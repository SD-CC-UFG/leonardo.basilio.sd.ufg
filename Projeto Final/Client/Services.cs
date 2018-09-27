using System;
using Chat.Grpc;
using Grpc.Core;

namespace Client {
	
	internal static class Services {

		public static ServiceResponse GetServiceLocation(ServiceRequest.Types.ServiceType serviceType){

			var naming = new NamingServer.NamingServerClient(
                new Channel("127.0.0.1", 7777, ChannelCredentials.Insecure)
            );

			return naming.GetServiceLocation(new ServiceRequest() {
                Name = serviceType
            });

		}

		public static AuthServer.AuthServerClient GetAuthentication() {
            
			var location = GetServiceLocation(ServiceRequest.Types.ServiceType.Auth);

            return new AuthServer.AuthServerClient(
                new Channel(location.Ip, location.Port, ChannelCredentials.Insecure)
            );
            
        }

		public static MessagingServer.MessagingServerClient GetMessaging(){

			var location = GetServiceLocation(ServiceRequest.Types.ServiceType.Messaging);

            return new MessagingServer.MessagingServerClient(
                new Channel(location.Ip, location.Port, ChannelCredentials.Insecure)
            );

		}

    }

}
