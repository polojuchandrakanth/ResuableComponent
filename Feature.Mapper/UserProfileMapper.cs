using AutoMapper;
using Feature.BusinessModel.ViewModel;
using Feature.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Feature.Mapper
{
    public class UserProfileMapper : Profile
    {
        public UserProfileMapper()
            {
                CreateMap<UserProfile, UserProfileViewModel>();
                CreateMap<UserProfileViewModel, UserProfile>();

            }         
    }
}
