using FluentValidation;
using FluentValidationApp.Models;

namespace FluentValidationApp.FluentValidators
{
    /// <summary>
    /// Customer model class için oluşturulan class
    /// CustomerValidator classı içinde Customer classı propetyleri için rulelar yazılabilir.
    /// Validator class olabilmesi için AbstractValidator abstract classını inherit etmesi gerekir.
    /// </summary>
    public class CustomerValidator:AbstractValidator<Customer>
    {
        public string NotEmptyMessage { get; } = "{PropertyName} alanı boş olamaz";

        // Kurallar constructor metdo içerisinde oluşturulur.
        // Custom bir validator yazılacak ise bu Must metodu içinde yazılabilir.
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage(NotEmptyMessage);
            RuleFor(x => x.Email).NotEmpty().WithMessage(NotEmptyMessage).EmailAddress()
                .WithMessage(NotEmptyMessage);
            RuleFor(x => x.Age).NotEmpty().WithMessage(NotEmptyMessage).InclusiveBetween(18, 60)
                .WithMessage("Age alanı 18 ile 60 arasında olmalıdır.");
            RuleFor(x => x.BirthDay).NotEmpty().WithMessage(NotEmptyMessage).Must(x =>
            {
                return DateTime.Now.AddYears(-18) >= x;
            }).WithMessage("{PropertyName} geçerli değildir.");

            // One-Many ilişkide RuleForEach metodu One classının validator classında kullanılır.
            // Bu sayede One classı için çalışan validasyon işlemi Many classı içinde çalışır.
            RuleForEach(x => x.Addresses).SetValidator(new AddressValidator());
        }
    }
}
