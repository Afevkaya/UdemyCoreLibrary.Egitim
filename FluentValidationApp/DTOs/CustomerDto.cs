namespace FluentValidationApp.DTOs;

/// <summary>
/// CustomerDto class
/// Entity classlar'ın kullanıcı ile etkileşime geçemesi istenilmez.
/// Bunun iki nedeni vardır
/// 1- güvenlik zaafiyeti olması
/// 2- bazı propertylerin kullanıcıya açılmasının istenilmememsi.
/// Kullanıcı ile etkileşime geçecek class
/// </summary>
public class CustomerDto
{
    public int Id { get; set; }
    public string Isim { get; set; }
    public string Eposta { get; set; }
    public int Yas { get; set; }
    public string FullName { get; set; }

    public string Number { get; set; }

    public DateTime ValidDate { get; set; }
    
    // public string CCNumber { get; set; }
    // public DateTime CCValidDate { get; set; }
    
    // public string CreditCardNumber { get; set; }
    // public DateTime CreditCardValidDate { get; set; }
    
    // public string Name { get; set; }
    // public string Email { get; set; }
    // public int Age { get; set; }
}