using AutoMapper;
using viecLam24hBE.Models;
using viecLam24hBE.ViewModels;
using viecLam24hFE.Models;

namespace viecLam24hBE.Configs
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            CreateMap<User, TuyenDungLogin>().ReverseMap();
            CreateMap<JobPost, JobPostViewModel>().ReverseMap();
            CreateMap<JobType, JobTypeViewModel>();
            CreateMap<JobPostViewModel, JobPost>().ReverseMap();
            CreateMap<User, UngTuyenDetail>().ReverseMap();
            CreateMap<ApplicantProfile, ApplicantProfileViewModel>().ReverseMap();
            CreateMap<User, DangkiTuyenDung>().ReverseMap();
        }
    }
}
