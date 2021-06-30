// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Interfaces
{
    using System.Collections.Generic;

    public interface IAnswers
    {
        List<Answer> GetAllAnswers();
        Answer GetAnswer(int id);
        Answer GetRandomAnswer();
        Answer GetRandomAnswerInCategory(string category);
    }
}
