using Microsoft.AspNetCore.StaticFiles;

namespace API.Services {
    public interface FileConverter {
        Task<string> ConvertFileToText(IFormFile? file);
        Task<string> ConvertPdfToText(Stream fileStream);
        Task<string> ConvertWordToText(Stream fileStream);
        Task<string> GetText(Stream fileStream);
    }

    public class FileConverterImplementation : FileConverter
    {
        public Task<string> ConvertFileToText(IFormFile? file)
        {
            if (file == null) {
                throw new Exception("File not exist");
            }
            var extension = GetExtension(file.FileName);
            var fileStream = file.OpenReadStream();
            switch (extension) {
                case ".pdf":
                    return ConvertPdfToText(fileStream);
                case ".doc":
                case ".docx":
                    return ConvertWordToText(fileStream);
                case ".txt":
                    return GetText(fileStream);
                default:
                    throw new Exception("Unsupport type");
            }
        }

        public Task<string> ConvertPdfToText(Stream fileStream)
        {
            
            throw new NotImplementedException();
        }

        public Task<string> ConvertWordToText(Stream fileStream)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GetText(Stream fileStream)
        {
            StreamReader streamReader = new StreamReader(fileStream);
            return await streamReader.ReadToEndAsync();
        }

        private string? GetExtension(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                return null;
            }

            int index = fileName.LastIndexOf('.');
            if (index < 0)
            {
                return null;
            }

            return fileName.Substring(index);
        }
    }
}