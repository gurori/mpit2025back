using AutoMapper;
using mpit.Core.Models;
using mpit.DataAccess.Entities;

namespace mpit.Mapping;

public sealed class ApplicationAutoMapper : Profile {
    public ApplicationAutoMapper() {
        
        CreateMap<UserEntity, User>();
    }
}