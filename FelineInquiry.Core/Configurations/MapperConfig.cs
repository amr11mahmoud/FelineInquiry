using AutoMapper;
using FelineInquiry.Core.DTOs.Questions;
using FelineInquiry.Core.DTOs.Users;
using FelineInquiry.Core.Models.Entities.Questions;
using FelineInquiry.Core.Models.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Core.Configurations
{
    public class MapperConfig:Profile
    {
        public MapperConfig()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, RegisterDto>().ReverseMap();
            CreateMap<User, UserResultDto>().ReverseMap();
            CreateMap<Question, CreateQuestionDto>().ReverseMap();
            CreateMap<Question, QuestionResponseDto>().ReverseMap();
        }
    }
}
