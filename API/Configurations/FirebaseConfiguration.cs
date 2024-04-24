using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace API.Configurations
{
    public class FirebaseConfiguration
    {
        private FirebaseApp? _firebaseApp = null;
        private StorageClient? _storageClient;
        private FirebaseConfiguration firebaseConfiguration;
        private static readonly object _lock = new object();
        private static FirebaseConfiguration _firebaseConfiguration;
        private FirebaseConfiguration() {}
        public static FirebaseConfiguration Instance {get {
            if (_firebaseConfiguration == null) {
                lock (_lock) {
                    _firebaseConfiguration = new FirebaseConfiguration();
                }
            }
            return _firebaseConfiguration;
        }}
        public FirebaseApp FirebaseApp {get {
            if (_firebaseApp == null) {
                // TODO: how should I call an async method in getter setter, should I use signleton for this?
                new Task(async () =>
                {
                    _firebaseApp = await GetFirebaseAppAsync();
                }).Start();
                
            }
            return _firebaseApp;
        }}
        public StorageClient StorageClient {get {
            if (_storageClient == null) {
                new Thread(async () => {
                    _storageClient = await GetStorageClientAsync();
                }).Start();
            }
            return _storageClient;
        }}
        private static async Task<GoogleCredential> GetGoogleCredentialsAsync()
        {
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

        private static async Task<StorageClient?> GetStorageClientAsync() {
            try {
                return await StorageClient.CreateAsync(await GetGoogleCredentialsAsync());
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
                return null;
            }
        }
    }
}