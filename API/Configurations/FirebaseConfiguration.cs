// using FirebaseAdmin;
// using FirebaseAdmin.Auth;
// using Google.Cloud.Storage.V1;

using Firebase.Auth;
using Firebase.Auth.Providers;
using Firebase.Storage;

namespace API.Configurations
{
    public class FirebaseConfiguration {
        private readonly IConfiguration _configuration;
        private FirebaseStorage? _firebaseStorage;
        private UserCredential? _credential;
        private FirebaseAuthConfig? _authConfig;

        public FirebaseConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private  FirebaseAuthConfig GetFirebaseAuthConfig() {
            if (_authConfig == null) {
                _authConfig = new FirebaseAuthConfig {
                    ApiKey = _configuration["firebase:auth:apiKey"],
                    AuthDomain = _configuration["firebase:auth:authDomain"],
                    Providers = new FirebaseAuthProvider[] {
                        new EmailProvider()
                    }
                };
            }
            return _authConfig;
        }

        private  async Task<UserCredential> GetUserCredential() {
            if (_credential == null) {
                FirebaseAuthClient client = new FirebaseAuthClient(GetFirebaseAuthConfig());
                _credential = await client.SignInWithEmailAndPasswordAsync(_configuration["firebase:auth:email"], _configuration["firebase:auth:password"]);
            }
            return _credential;
        }

        public  FirebaseStorage FirebaseStorage { get {
                if (_firebaseStorage == null) {
                    _firebaseStorage = new FirebaseStorage(_configuration["firebase:cloudStorage:storageBucket"], new FirebaseStorageOptions {
                        AuthTokenAsyncFactory = async () => {
                            var credential = await GetUserCredential();
                            return await credential.User.GetIdTokenAsync();
                        }
                    });
                }
                return _firebaseStorage;
            }
        }
    }
}