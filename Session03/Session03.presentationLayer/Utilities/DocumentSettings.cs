﻿namespace Session03.presentationLayer.Utilities
{
    public static class DocumentSettings
    {
        public static async Task <string> UploadFileAsync(IFormFile file, string folderName) 
        {
            //string folderPath = Directory.GetCurrentDirectory()+@"\wwwroot\Files";
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Files", folderName);
            string fileName = $"{Guid.NewGuid()}-{file.FileName}";
            string filePath = Path.Combine(folderPath, fileName);
            using var stream = new FileStream(filePath, FileMode.Create);
            await file.CopyToAsync(stream);
            return fileName;
        }

        public static void DeleteFile(string folderName, string fileName) 
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(),@"wwwroot\Files", folderName,fileName);
            if (File.Exists(filePath))
                File.Delete(filePath);
        }
    }
}
