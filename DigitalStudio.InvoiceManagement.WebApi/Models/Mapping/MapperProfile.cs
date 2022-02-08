using AutoMapper;
using DigitalStudio.InvoiceManagement.WebApi.Models;

public sealed class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<InvoiceModel, InvoiceDataModel>();
    }
}