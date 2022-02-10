using AutoMapper;
using DigitalStudio.InvoiceManagement.Domain.Models;
using DigitalStudio.InvoiceManagement.WebApi.Models.Views;

public sealed class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<InvoiceModel, InvoiceDataModel>()
            .ForMember(
                d => d.Id,
                o => o.MapFrom(s => s.Id ?? Guid.NewGuid())
            );
    }
}