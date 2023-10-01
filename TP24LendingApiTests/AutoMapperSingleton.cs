using AutoMapper;
using TP24LendingApi;

namespace TP24LendingApiTests
{
    public class AutoMapperSingleton
    {
        private static IMapper _mapper;
        public static IMapper Mapper
        {
            get
            {
                if (_mapper == null)
                {
                    // Auto Mapper Configurations
                    var mappingConfig = new MapperConfiguration(mc =>
                    {
                        mc.AddProfile(new MappingProfile());
                    });

                    IMapper mapper = mappingConfig.CreateMapper();
                    _mapper = mapper;
                }

                return _mapper;
            }
        }
    }
}
