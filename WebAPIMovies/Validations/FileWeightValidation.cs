using System.ComponentModel.DataAnnotations;

namespace WebAPIMovies.Validations
{
    public class FileWeightValidation : ValidationAttribute
    {
        private readonly int maxWeightInMegaBytes;

        public FileWeightValidation(int maxWeightInMegaBytes)
        {
            this.maxWeightInMegaBytes = maxWeightInMegaBytes;
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

            if (file.Length > maxWeightInMegaBytes * 1024 * 1024)
            {
                return new ValidationResult($"File weight must be less than {maxWeightInMegaBytes} MB");
            }

            return ValidationResult.Success;
        }
    }
}
