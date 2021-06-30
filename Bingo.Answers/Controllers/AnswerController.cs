namespace Bingo.Answers.Controllers
{
    using Bingo.Answers.Exceptions;
    using Bingo.Answers.Interfaces;
    using Bingo.Answers.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
