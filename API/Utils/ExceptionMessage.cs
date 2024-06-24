namespace API.Utils
{
    public class ExceptionMessage
    {
        public const string EmailNotFound = "Email not found";
        public const string PasswordNotFound = "Password not found";
        public const string ArticleNotFound = "Article not found";
        public const string DraftArticleDeletionAllowance = "Only draft article can be delete";
        public const string SoftDeletion = "Need to soft delete before delete permanently";
        public const string FileNotExist = "File not exist";
        public const string UnsupportedFileType = "Unsupported file type. The system currently supports .pdf, .doc, .docx, .txt";
        public const string WrongFileFormat = "The accepted file format including the following section: Title, abstract, introduction, method, results, conclusion";
        public const string UnableToSubmit = "Only draft or revision article can be submitted";
        public const string UnqualifiedSubmission = "The submission is lack of information";
    }
}