using AutoMapper;
using TP24Entities.Models;
using TP24LendingApi.Models;

namespace TP24LendingApi
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReceivableForCreationDto, Receivable>();
        }
    }
}
