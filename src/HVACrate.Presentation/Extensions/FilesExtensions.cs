namespace HVACrate.Presentation.Extensions
{
    public static class FilesExtensions
    {
        private const string InvalidSize = "File size must be greater than 0.";

        private static string GetRelativePath(string name)
            => $"/{Path.Combine("images", "profile-pictures", name)}";

        private static string GetPath(this IWebHostEnvironment env, string fileName)
        {
            string rootPath = env.WebRootPath;

            if (string.IsNullOrEmpty(rootPath) || !Directory.Exists(rootPath))
            {
                rootPath = "/tmp";
            }

            return Path.Combine(rootPath, fileName);
        }

        public static string GetFileExtension(this IFormFile file)
            => Path.GetExtension(file.FileName);

        public static async Task<string> UploadImageAsync(this IWebHostEnvironment env, IFormFile image, string name)
        {
            ArgumentNullException.ThrowIfNull(image, nameof(image));
            if (image.Length == 0)
            {
                throw new ArgumentException(InvalidSize, nameof(image));
            }

            string fileName = $"{name}{image.GetFileExtension()}";
            string filePath = env.GetPath(Path.Combine("images", "profile-pictures", fileName));

            using FileStream stream = new(filePath, FileMode.Create);
            await image.CopyToAsync(stream).ConfigureAwait(false);

            return GetRelativePath(fileName);
        }

        public static void DeleteFile(this IWebHostEnvironment env, string name)
        {
            string path = env.GetPath(name);
            if (File.Exists(path)) File.Delete(path);
        }
    }
}
