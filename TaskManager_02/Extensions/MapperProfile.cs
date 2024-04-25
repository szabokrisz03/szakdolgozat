using AutoMapper;

using TaskManager_02.Data.Entity;
using TaskManager_02.Data.ViewModels;

namespace TaskManager_02.Extensions;

/// <summary>
/// 
///     AutoMapper Konfiguráció
/// 
/// </summary>
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<User, UserViewModel>().ReverseMap();
        CreateMap<Project, ProjectViewModel>().ReverseMap();
    }
}
