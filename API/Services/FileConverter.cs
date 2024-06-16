using System.Text;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Spire.Doc;

namespace API.Services {
    public interface FileConverter {
        Task<string> ConvertFileToText(IFormFile? file);
        string ConvertPdfToText(Stream fileStream);
        Task<string> ConvertWordToText(Stream fileStream, FileFormat fileFormat);
        Task<string> GetText(Stream fileStream);
    }

    public class FileConverterImplementation : FileConverter
    {
        public async Task<string> ConvertFileToText(IFormFile? file)
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
                    return await ConvertWordToText(fileStream, FileFormat.Doc);
                case ".docx":
                    return await ConvertWordToText(fileStream, FileFormat.Docx);
                case ".txt":
                    return await GetText(fileStream);
                default:
                    throw new Exception("Unsupport type");
            }
        }

        public string ConvertPdfToText(Stream fileStream)
        {
            var pdfReader = new PdfReader(fileStream);
            var pdfDocument = new PdfDocument(pdfReader);
            var pages = pdfDocument.GetNumberOfPages();
            StringBuilder stringBuilder = new StringBuilder();
            for (int page = 1; page < pages; page++) {
                stringBuilder.Append(PdfTextExtractor.GetTextFromPage(pdfDocument.GetPage(page)));
                stringBuilder.Append("\n");
            }
            return stringBuilder.ToString();
        }

        public async Task<string> ConvertWordToText(Stream fileStream, FileFormat fileFormat)
        {
            Document document = new Document();
            document.LoadFromStream(fileStream, fileFormat);
            return document.GetText();
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