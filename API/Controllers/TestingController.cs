using API.Services;
using API.Utils;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers {
    [ApiController]
    [Route(ApiPath.Version1 + ApiPath.Api + ApiPath.Test)]
    public class TestingController: ControllerBase {
        private readonly FirebaseStorageService _firebaseStorageService;
        private readonly FileConverter _fileConverter;

        public TestingController(FirebaseStorageService firebaseStorageService, FileConverter fileConverter)
        {
            _firebaseStorageService = firebaseStorageService;
            this._fileConverter = fileConverter;
        }

        [HttpPost("/firebase-file-uploading")]
        public async Task<string> UploadFile(IFormFile file) {
            return await _firebaseStorageService.UploadFileAsync(file.OpenReadStream(), file.ContentType, file.FileName);
        }

        [HttpPost("/file-to-text-converter")]
        public async Task<string> ConvertFile(IFormFile file) {
            return await _fileConverter.ConvertFileToText(file);
        }
    }
}