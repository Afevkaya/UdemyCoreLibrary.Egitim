using AutoMapper;
using FluentValidationApp.DTOs;
using FluentValidationApp.Models;

namespace FluentValidationApp.Mapping;

public class EventDateProfile: Profile
{
    public EventDateProfile()
    {
        // Projection
        // Projection complex bir property'i ayırma veya birleştirmeye denir.
        // Örnek olarak DateTime property'sini gün ay yıl olarak ayrıma veya birleştimek.
        // Bu gibi işlemlerde(ayırma veya birleştime gibi complex işlemer) ReverseMap metodu çalışmaz.
        // Bunu yerine custom bir mapleme işlemi yapılmalıdır.
        CreateMap<EventDateDto, EventDate>()
            .ForMember(x => x.Date, opt => opt.MapFrom(x => new DateTime(x.Day, x.Month, x.Year)));
        
        CreateMap<EventDate, EventDateDto>()
            .ForMember(x => x.Day, opt => opt.MapFrom(x => x.Date.Day))
            .ForMember(x => x.Month, opt => opt.MapFrom(x => x.Date.Month))
            .ForMember(x => x.Year, opt => opt.MapFrom(x => x.Date.Year));
    }
}