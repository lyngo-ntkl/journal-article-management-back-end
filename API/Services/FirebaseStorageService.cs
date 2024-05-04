using API.Configurations;

namespace API.Services {
    public interface FirebaseStorageService {
        Task<string> UploadFileAsync(Stream stream, String contentType, String fileName);
    }

    public class FirebaseStorageServiceImplementation : FirebaseStorageService
    {
        private readonly FirebaseConfiguration _firebaseConfiguration;

        public FirebaseStorageServiceImplementation(FirebaseConfiguration firebaseConfiguration)
        {
            _firebaseConfiguration = firebaseConfiguration;
        }

        public async Task<string> UploadFileAsync(Stream stream, string contentType, string fileName)
        {
            // TODO: research about time out of provider token
            var storage = _firebaseConfiguration.FirebaseStorage;
            var cancellationTokenSource = new CancellationTokenSource();
            return await storage.Child(fileName).PutAsync(stream, cancellationTokenSource.Token, contentType);
        }
    }
}