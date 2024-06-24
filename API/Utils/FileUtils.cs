namespace API.Utils {
    public class FileUtils {
        public static string? GetExtension(string fileName)
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

        public static string GenerateFileName(string oldName) {
            var extension = GetExtension(oldName);
            var guid = Guid.NewGuid();
            return guid.ToString() + extension;
        }
    }
}