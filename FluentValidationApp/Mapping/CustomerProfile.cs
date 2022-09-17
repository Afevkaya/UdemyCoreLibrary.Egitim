using AutoMapper;
using FluentValidationApp.DTOs;
using FluentValidationApp.Models;

namespace FluentValidationApp.Mapping;

///  Bir class'ın mapleme sınıfı olabilmesi için AutoMapper kütüphanesi içinde bulunan Profile classından inherit edilmesi gereklidir.
/// Program.cs içinde eklediğimiz service, belirttiğimiz classın bulunduğu assembly içinde Profile classını inherit eden classları tarayacak.
/// Classı içinde yapmış olduğumuz maplemelere bakacak.
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        // Customer'ı CustomerDto classına mapleyecek.
        // Mapleme işlemini yaparken property namlerine göre yapacak.
        // Eğer propert nameleri aynı değilse AutoMapper mapleme işlemi yapamaz.
        // Bundan dolayı custom mapleme işlemi yapmamız gerekir.

        // CreateMap<Customer, CustomerDto>().ReverseMap();

        // Customer -> CustomerDto custom mapleme 
        // Entity class ile Dto class property nameleri aynı değilse AutoMapper kütüphanesi otomatik mapleme yapamaz.
        // Bundan dolayı bizim custom mapleme işlemi yapmamız gerekir.

        // Metodu property' e mapleme
        // Bir metodu bir property' e maplemek için isimleri aynı olmak şartı ile metodun başına Get koymak yeterlidir.
        // Eğer isimleri aynı değil ise custom bir mapleme işlemi yapılması gerekir.
        

        // Flattening
        // Flattening complex tipteki bir classı sade bir hale getirmeye denir.
        // Örnek olarak Customer classı içinde bir de CreditCard classına ait property'nin olması Customer classını complex tipte bir class yapar.
        // AutoMapper kütüphanesinin otomatik olarak mapleyebilmesi için;
        // Dto classının içinde property'nin tipi aynı olacak şekilde önce classın adını ve property name'ini bitişik olarak yazmak gerekir.
        // Eğer bu kurala uyulmaz ise custom bir mapleme yapılması gerekir.
        
        // IncludeMember metodu
        // IncludeMembers Dto classın daki property name ile complex classın da bulunan property clasının property nameleri ile aynı ise uygulanır.
        // Ama mutlaka property classını da  Profile classına eklenmesi gerekir yoksa hata alınır.

        CreateMap<CreditCard, CustomerDto>();

        CreateMap<Customer, CustomerDto>().IncludeMembers(x=>x.CreditCard)
            .ForMember(dest => dest.Isim, opt => opt.MapFrom(x => x.Name))
            .ForMember(dest => dest.Eposta, opt => opt.MapFrom(x => x.Email))
            .ForMember(dest => dest.Yas, opt => opt.MapFrom(x => x.Age))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(x => x.FullName2()));
        // .ForMember(dest => dest.CCNumber, opt => opt.MapFrom(x => x.CreditCard.Number))
        // .ForMember(dest => dest.CCValidDate, opt => opt.MapFrom(x => x.CreditCard.ValidDate));
    }
}