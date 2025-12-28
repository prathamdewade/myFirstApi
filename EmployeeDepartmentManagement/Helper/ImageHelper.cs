
namespace EmployeeDepartmentManagement.Helper
{
    public class ImageHelper
    {
        private const string ImageFolderPath = "wwwroot/Images/";

        public static string SaveImage(IFormFile image)
        {
            if (image == null && image.Length == 0)
            {
                return null;
            }
            if (!Directory.Exists(ImageFolderPath))
            {
                Directory.CreateDirectory(ImageFolderPath);
            }
            string actualPath = Path.Combine(ImageFolderPath, image.FileName);
            if (!File.Exists(actualPath))
            {
                FileStream fs = new FileStream(actualPath, FileMode.OpenOrCreate);
                image.CopyTo(fs);
            }
            return image.FileName;

        }
        public static void DeleteImage(string imageName)
        {
            string actualPath = Path.Combine(ImageFolderPath, imageName);
            if (File.Exists(actualPath))
            {
                File.Delete(actualPath);
            }
        }
    }
}
