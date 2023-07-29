using FelineInquiry.Core.DTOs.Questions;
using FelineInquiry.Core.Models.Entities.Questions;
using FelineInquiry.Core.Models.Global;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FelineInquiry.Domain.Services.Abstract
{
    public interface IQuestionsManager<T>:IBaseManager<T> where T : class
    {
        Task<Question> CreateQuestionAsync(CreateQuestionDto questionDto);
        Task<ResultMessage> ValidateCreateQuestionAsync(CreateQuestionDto questionDto);
    }
}
