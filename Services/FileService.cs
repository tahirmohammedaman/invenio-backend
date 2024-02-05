namespace invenio.Services;

public class FileService
{
    public static string UploadFile(IFormFile file)
    {
        if (file is null)
            return null;

        var fileName = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", fileName);

        using (var stream = new FileStream(path, FileMode.Create))
        {
            file.CopyTo(stream);
        }
        return fileName;
    }
}