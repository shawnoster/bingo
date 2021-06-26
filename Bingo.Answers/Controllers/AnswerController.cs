namespace Bingo.Answers.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Bingo.Answers.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;

    [ApiController]
    [Route("[controller]")]
    public class AnswerController : ControllerBase
    {
        private readonly ILogger<AnswerController> _logger;

        public AnswerController(ILogger<AnswerController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Answer> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Answer
            {
                Condition = "1",
                Category = "B",
            })
            .ToArray();
        }
    }
}
