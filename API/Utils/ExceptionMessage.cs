namespace API.Utils {
    public class ExceptionMessage {
        public const string ArticleNotFound = "Article not found";
        public const string DraftArticleDeletionAllowance = "Only draft article can be delete";
        public const string SoftDeletion = "Need to soft delete before delete permanently";
        public const string FileNotExist = "File not exist";
        public const string UnsupportedFileType = "Unsupported file type. The system currently supports .pdf, .doc, .docx, .txt";
    }
}