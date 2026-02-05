using ASPNETCoreWebAPI.Data;
using ASPNETCoreWebAPI.Model;
using AutoMapper;

namespace ASPNETCoreWebAPI.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //CreateMap<Student, StudentDTO>();
            //CreateMap<StudentDTO, Student>();

            //config for diff property names
            //CreateMap<StudentDTO, Student>().ForMember(n => n.StudentName, opt => opt.MapFrom(x => x.Name)).ReverseMap().ForMember(n => n.Name, opt => opt.MapFrom(x => x.StudentName));

            //ignore some property
            //CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n => n.StudentName, opt => opt.Ignore());
            //CreateMap<StudentDTO, Student>().ForMember(n => n.StudentName, opt => opt.Ignore()).ReverseMap();

            //transforming some property
            //CreateMap<StudentDTO, Student>().ReverseMap().AddTransform<string>(n => string.IsNullOrEmpty(n)?"No address found": n);
            //for perticular column
            //CreateMap<StudentDTO, Student>().ReverseMap().ForMember(n => n.Address, opt => opt.MapFrom(n => string.IsNullOrEmpty(n.Address)? "no address found!": n.Address));

            CreateMap<StudentDTO, Student>().ReverseMap();
        }
    }
}
