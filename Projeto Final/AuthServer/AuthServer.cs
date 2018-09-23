using System;
using System.Threading.Tasks;
using Chat.Grpc;
using Grpc.Core;
using MongoDB.Driver;
using MongoDB.Bson.Serialization;

namespace AuthServer {
	
	public class AuthServer : Chat.Grpc.AuthServer.AuthServerBase {

		private static MongoClient mongoClient;
		private static IMongoCollection<UserLogin> usersCollection;

		private static readonly DateTime UNIX_EPOCH = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

		private static readonly byte[] signingKey = new byte[] { 0x59, 0x7c, 0x0d, 0x7c, 0xe5, 0x02, 0xca, 0x0f, 0x2e, 0xe7, 0x57, 0x79, 0x93, 0xee, 0x80, 0xf4, 0x51, 0x92, 0x30, 0x58, 0x2b, 0x28, 0x33, 0x1b, 0x12, 0x31, 0xdf, 0x41, 0x37, 0x6e, 0x32, 0x0d }; // 597c0d7ce502ca0f2ee7577993ee80f4519230582b28331b1231df41376e320d

		static AuthServer(){
			
			BsonClassMap.RegisterClassMap<UserLogin>(classMap => {
                classMap.MapProperty(nameof(UserLogin.UserName)).SetElementName("username");
                classMap.MapProperty(nameof(UserLogin.Password)).SetElementName("password");            
            });
   
		}

		public AuthServer(string mongoHostName) {

			mongoClient = new MongoClient($"mongodb://{mongoHostName}:27017");

			usersCollection = mongoClient.GetDatabase("authserver").GetCollection<UserLogin>("users");

			usersCollection.Indexes.CreateOne(
				new CreateIndexModel<UserLogin>(
					Builders<UserLogin>.IndexKeys.Ascending(u => u.UserName),
					new CreateIndexOptions(){
						Collation = new Collation("pt", strength : CollationStrength.Primary),
                        Unique = true
					}
			    )
			);

		}

		public override Task<UserCredential> SignUp(UserLogin request, ServerCallContext context) {

			return new Task<UserCredential>(() => {
				
				request.UserName = request.UserName.Trim();

				try {

					if(request.UserName == ""){
						throw new ArgumentException("Informe um nome de usuário.");
					}

					if (request.Password.Length < 4) {
                        throw new ArgumentException("Informe uma senha de pelo menos 4 caracteres.");
                    }

					usersCollection.InsertOne(request);

				}catch(MongoDuplicateKeyException){

					return new UserCredential() {
						Error = "O usuário escolhido já existe."
					};

				}

				return CreateCredential(request.UserName);

			});

		}

		public override Task<UserCredential> Authenticate(UserLogin request, ServerCallContext context) {
			
			return new Task<UserCredential>(() => {
                
				var user = usersCollection.Find(u => u.UserName == request.UserName &&
                                                     u.Password == request.Password).FirstOrDefault();

				if (user != null) {

					return CreateCredential(user.UserName);

				}else {

                    return new UserCredential() {
                        Error = "Usuário e/ou senha incorretos."
                    };

                }
                    
			});

		}

		public override Task<UserCredential> RenewCredential(UserCredential request, ServerCallContext context) {

			return new Task<UserCredential>(() => {

				if(this.CheckSignature(request)){

					var expiration = UNIX_EPOCH.AddSeconds(request.Expiration);

					if (expiration > DateTime.Now) {

						return CreateCredential(request.UserName);

					}else{

                        return new UserCredential() {
                            Error = "Credencial expirada."
                        };

					}

				}else{

					return new UserCredential() {
						Error = "Credencial inválida."
					};

				}
			
			});

		}

		private UserCredential CreateCredential(string username){
                         
			var expiration = DateTime.Now.AddHours(1).ToUniversalTime();
			var timestamp = expiration.Subtract(UNIX_EPOCH).Seconds;
            
			return this.SignCredential(new UserCredential() {
				UserName = username,
				Expiration = timestamp
			});
         
		}

		private UserCredential SignCredential(UserCredential credential){
			         
			credential.Signature = this.ComputeSignature(credential);

			return credential;

		}

		private bool CheckSignature(UserCredential credential){

			return credential.Signature == this.ComputeSignature(credential);

		}

		private string ComputeSignature(UserCredential credential){

			var hmac = new System.Security.Cryptography.HMACSHA256(signingKey);

            var data = System.Text.Encoding.UTF8.GetBytes(credential.UserName + "\n" +
                                                          credential.Expiration.ToString());
                                                          
			return Convert.ToBase64String(hmac.ComputeHash(data));

		}

	}

}
