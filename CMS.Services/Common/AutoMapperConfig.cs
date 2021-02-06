using AutoMapper;
using CMS.Services.Course.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Services.Common
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<CMS.Data.AppModel.Course, CourseVM>().ReverseMap();
            //.ForMember(x => x.CreateDate, opt => opt.Ignore()).ForMember(x => x.UpdateDate, opt => opt.Ignore()).ForMember(x => x.DeleteDate, opt => opt.Ignore()).ForMember(x => x.Deleted, opt => opt.Ignore())
        }
    }
}