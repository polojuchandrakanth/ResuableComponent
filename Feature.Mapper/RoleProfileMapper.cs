using AutoMapper;
using Feature.BusinessModel.ViewModel;
using Feature.Entity.Entities;

namespace Feature.Mapper
{
    public class RoleProfileMapper : Profile
    {


        public RoleProfileMapper()
        {
            CreateMap<Role, RoleModel>();
            CreateMap<RoleModel, Role>();

        }

    }
}