using AutoMapper;
using FelineInquiry.Core.DTOs.Questions;
using FelineInquiry.Core.Interfaces;
using FelineInquiry.Core.Models.Entities.Questions;
using FelineInquiry.Core.Models.Global;
using FelineInquiry.Domain.Services.Abstract;


namespace FelineInquiry.Domain.Services.Questions
{
    public class QuestionsManager:IQuestionsManager<Question>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public QuestionsManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
            
        public async Task<Question> CreateQuestionAsync(CreateQuestionDto questionDto)
        {
            Question question = _mapper.Map<Question>(questionDto);

            await _unitOfWork.Questions.AddAsync(question);
            await _unitOfWork.CompleteAsync();

            if (question.IsAnonymous)
            {
                question.Author = null;
                question.AuthorId = null;
            }

            return question;
        }

        public async Task<ResultMessage> ValidateCreateQuestionAsync(CreateQuestionDto questionDto)
        {
            ResultMessage result = new ResultMessage
            {
                Success = true,
            };

            //// Add Condition
            //if (true)
            //{
            //    result.Success = false;
            //    //Add Error
            //}

            return result;
        }
    }
}
