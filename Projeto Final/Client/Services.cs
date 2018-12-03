using System;
using System.Threading.Tasks;
using Chat.Grpc;
using Grpc.Core;

namespace Client {

	internal static class Services {

		private async static Task<ServiceResponse> GetServiceLocation(ServiceType serviceType, Action<Exception> exceptionHandler = null) {

			var naming = new Naming.NamingClient(
				new Channel("127.0.0.1", 7777, ChannelCredentials.Insecure)
			);

			return await naming.GetServiceLocationAsync(new ServiceRequest() {
				Name = serviceType
			});

		}

		public async static Task<Auth.AuthClient> GetAuthentication() {

			var location = await GetServiceLocation(ServiceType.Auth);

			return new Auth.AuthClient(
				new Channel(location.Ip, location.Port, ChannelCredentials.Insecure)
			);

		}

		public async static Task<Messaging.MessagingClient> GetMessaging() {

			var location = await GetServiceLocation(ServiceType.Messaging);

			return new Messaging.MessagingClient(
				new Channel(location.Ip, location.Port, ChannelCredentials.Insecure)
			);

		}

	}

}
