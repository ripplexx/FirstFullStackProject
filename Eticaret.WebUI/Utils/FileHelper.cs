namespace Eticaret.WebUI.Utils
{
    public class FileHelper
    {
        public static async Task<string> FileLoaderAsync(IFormFile formFile,string filePath="/Img/")
        {
            string fileName = "";
            if(formFile!=null && formFile.Length > 0)
            {
                fileName = formFile.FileName.ToLower();
                string directory=Directory.GetCurrentDirectory()+"/wwwroot" +filePath+fileName;// yukarda /Img/ 'nin sonundaki slashı silip /wwwroot/ yapabiliriz yani rootun sonuna slash koyarız yada /Img/ yapıp, /wwwroot sonunda slash koymayız. filePath eklediğimiz için
                // bir dosya akış başlatacağımızı aşağıdaki gibi belirtelim
                // bir handle bir access vericez
                using var stream = new FileStream(directory, FileMode.Create);
                await formFile.CopyToAsync(stream); 
            }
            return fileName;
        }
        public static bool FileRemover(string fileName,string filePath="/Img/")
        {
            string directory = Directory.GetCurrentDirectory()+"/wwwroot" + filePath + fileName;
            // uygulamanın çalıştığı dizini bul, sonra wwwroot klasörüne git, bunun için default olarak Img'ye gideceksin, ekstra bir path gönderilirse; ozaman ona gideceksin
            if (File.Exists(directory))
            {
                File.Delete(directory);
                return true;
            }
            return false;
        }
    }

}
