using System.ComponentModel.DataAnnotations;
using WebAPIMovies.Helpers;

namespace WebAPIMovies.Validations
{
    public class FileFormatValidation : ValidationAttribute
    {
        private readonly string[] validTypes;

        public FileFormatValidation(string[] validTypes)
        {
            this.validTypes = validTypes;
        }

        public FileFormatValidation(ValidFileTypes validFileTypes)
        {
            if (validFileTypes == ValidFileTypes.Image)
            {
                validTypes = new string[] { "image/jpg", "image/jpeg", "image/png", "image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            IFormFile file = value as IFormFile;

            if (file == null)
            {
                return ValidationResult.Success;
            }

            if (!validTypes.Contains(file.ContentType))
            {
                return new ValidationResult($"File format must be {string.Join(", ", validTypes)}");
            }

            return ValidationResult.Success;
        }
    }
}
