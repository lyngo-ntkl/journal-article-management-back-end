using API.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [ApiController]
    [Route(ApiPath.Version1 + ApiPath.Api + ApiPath.Test)]
    public class TestingController: ControllerBase {
        private readonly FirebaseStorageService _firebaseStorageService;

        public TestingController(FirebaseStorageService firebaseStorageService)
        {
            _firebaseStorageService = firebaseStorageService;
        }

        [HttpPost("/firebase-file-uploading")]
        public async Task<string> UploadFile(IFormFile file) {
            return await _firebaseStorageService.UploadFileAsync(file.OpenReadStream(), file.ContentType, file.FileName);
        }
    }
}