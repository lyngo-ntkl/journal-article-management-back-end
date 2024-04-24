using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;

namespace API.Configurations
{
    public class FirebaseConfiguration
    {
        private FirebaseApp? _firebaseApp = null;
        private static readonly object _lock = new object();
        private FirebaseApp FirebaseApp {get {
            if (_firebaseApp == null) {
                // TODO: how should I call an async method in getter setter, should I use signleton for this?
                new Task(async () =>
                {
                    _firebaseApp = await GetFirebaseAppAsync();
                }).Start();
                
            }
            return _firebaseApp;
        }}
        private static async Task<GoogleCredential> GetGoogleCredentialsAsync()
        {
            // FirebaseAuth
            try
            {
                CancellationTokenSource source = new CancellationTokenSource();
                CancellationToken cancellationToken = source.Token;
                // TODO: add service_account file & add this file to git ignore
                return await GoogleCredential.FromFileAsync("", cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return GoogleCredential.GetApplicationDefault();
            }
        }

        private static async Task<FirebaseApp?> GetFirebaseAppAsync()
        {
            try
            {
                return FirebaseApp.Create(new AppOptions
                {
                    Credential = await GetGoogleCredentialsAsync()
                });
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
                return null;
            }

        }
    }
}