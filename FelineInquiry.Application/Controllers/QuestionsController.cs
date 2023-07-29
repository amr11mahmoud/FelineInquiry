using AutoMapper;
using FelineInquiry.Application.Interfaces.Abstract;
using FelineInquiry.Core.DTOs.Questions;
using FelineInquiry.Core.Models.Entities.Questions;
using FelineInquiry.Core.Models.Entities.Users;
using FelineInquiry.Core.Models.Global;
using FelineInquiry.Domain.Services.Abstract;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FelineInquiry.Application.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        private readonly IQuestionsManager<Question> _questionsManager;
        private readonly IBaseController _baseController;
        private readonly IMapper _mapper;
        public QuestionsController(IQuestionsManager<Question> questionsManager,IBaseController baseController, IMapper mapper)
        {
            _questionsManager = questionsManager;
            _baseController = baseController;
            _mapper = mapper;
        } 

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Create(CreateQuestionDto request)
        {
            var (result, user) = await _baseController.GetCurrentUserAsync(User);
            
            result = await _questionsManager.ValidateCreateQuestionAsync(request);

            if (!result.Success)
            {
                return _baseController.MapErrorResultToValidHttpErrorResponse(result);
            }

            Question question = await _questionsManager.CreateQuestionAsync(request);

            return Ok(_mapper.Map<QuestionResponseDto>(question));
        }
    }
}
