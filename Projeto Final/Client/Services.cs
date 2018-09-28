using System;
using System.Threading.Tasks;
using Chat.Grpc;
using Grpc.Core;

namespace Client {

	internal static class Services {

		private async static Task<ServiceResponse> GetServiceLocation(ServiceRequest.Types.ServiceType serviceType, Action<Exception> exceptionHandler = null) {

			var naming = new NamingServer.NamingServerClient(
				new Channel("127.0.0.1", 7777, ChannelCredentials.Insecure)
			);

			return await naming.GetServiceLocationAsync(new ServiceRequest() {
				Name = serviceType
			});

		}

		public async static Task<AuthServer.AuthServerClient> GetAuthentication() {

			var location = await GetServiceLocation(ServiceRequest.Types.ServiceType.Auth);

			return new AuthServer.AuthServerClient(
				new Channel(location.Ip, location.Port, ChannelCredentials.Insecure)
			);

		}

		public async static Task<MessagingServer.MessagingServerClient> GetMessaging() {

			var location = await GetServiceLocation(ServiceRequest.Types.ServiceType.Messaging);

			return new MessagingServer.MessagingServerClient(
				new Channel(location.Ip, location.Port, ChannelCredentials.Insecure)
			);

		}

	}

}
