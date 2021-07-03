namespace Bingo.Answers.Controllers
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Bingo.Answers.Exceptions;
    using Bingo.Answers.Interfaces;
    using Bingo.Answers.Models;

    [ApiController]
    [Route("[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly ILogger<AnswerController> _logger;
        private readonly IAnswerRepository _repository;

        public AnswerController(ILogger<AnswerController> logger, IAnswerRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        [HttpPost]
        public async Task<ActionResult> CreateAnswer([FromBody] Answer newAnswer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            await _repository.AddItemAsync(newAnswer);

            _logger.LogInformation("Created Answer");

            return Ok();
        }

        [HttpGet("{answerId}")]
        public async Task<ActionResult> GetAnswer(string answerId)
        {
            try
            {
                var answer = await _repository.GetItemAsync(answerId);
                return Ok(answer);
            }
            catch (EntityNotFoundException)
            {
                return NotFound(answerId);
            }
        }
    }
}
