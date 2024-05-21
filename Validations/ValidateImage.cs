

using System.ComponentModel.DataAnnotations;

namespace BooksApp.Validations;

public class ValidateImage : ValidationAttribute
{
    private readonly string[] _validFiles;

    public ValidateImage(string[] validFiles)
    {
        _validFiles = validFiles;
    }

    public ValidateImage(ValidFileTypes ValidFileTypes)
    {
        if (ValidFileTypes == ValidFileTypes.Image)
        {
            _validFiles = new string[] { "image/jpeg", "image/jpg", "image/png", "image/gif" };
        }
    }

    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return ValidationResult.Success;
        }

        IFormFile formFile = value as IFormFile;

        if (formFile == null)
        {
            return ValidationResult.Success;
        }

        if (!_validFiles.Contains(formFile.ContentType))
        {
            return new ValidationResult($"La imagen debe ser: {string.Join(", ", _validFiles)}");
        }

        return ValidationResult.Success;
    }
}


