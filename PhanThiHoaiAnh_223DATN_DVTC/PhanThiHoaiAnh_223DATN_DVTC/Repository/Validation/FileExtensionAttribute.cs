using System.ComponentModel.DataAnnotations;

namespace PhanThiHoaiAnh_223DATN_DVTC.Repository.Validation
{
    public class FileExtensionAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validation)
        {
            if(value is IFormFile file)
            {
                var extension = Path.GetExtension(file.FileName); //anh.jpg
                string[] extensions = { "jpg", "jpeg", "png" };

                bool result = extensions.Any(x => extension.EndsWith(x));
                if(!result)
                {
                    return new ValidationResult("Cho phép tải file có đuôi jpg hoặc jpeg hoặc png");
                }
            }
            return ValidationResult.Success;
        }
    }
}
