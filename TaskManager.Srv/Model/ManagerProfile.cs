using AutoMapper;

using TaskManager.Srv.Model.DataModel;
using TaskManager.Srv.Model.ViewModel;

namespace TaskManager.Srv.Model;

public class ManagerProfile: Profile
{
	public ManagerProfile()
	{
		CreateMap<Project, ProjectViewModel>().ReverseMap();
		CreateMap<User, UserViewModel>().ReverseMap();
	}
}
