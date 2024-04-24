using API.Configurations;
using Google.Cloud.Storage.V1;

namespace API.Services {
    public interface FirebaseStorageService {
        Task UploadFileAsync(Stream stream, String contentType, String fileName);
    }
    public class FirebaseStorageServiceImplementation : FirebaseStorageService
    {
        public async Task UploadFileAsync(Stream file, String contentType, String fileName)
        {
            try {
                await FirebaseConfiguration.Instance.StorageClient.UploadObjectAsync("", GenerateNewFileName(fileName), contentType, file, new UploadObjectOptions {
                    ChunkSize = 20
                });
            } catch (Exception e) {
                Console.WriteLine(e.StackTrace);
            }
        }

        private String GenerateNewFileName(String fileName) {
            String result = Guid.NewGuid().ToString() + GetFileExtension(fileName);
            return result;
        }

        private String GetFileExtension(String fileName) {
            return "." + fileName.Split('.', 2)[1];
        }
    }
}