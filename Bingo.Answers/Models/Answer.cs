// Copyright (c) Shawn Oster. All rights reserved.
// Licensed under the MIT license.

namespace Bingo.Answers.Models
{
    using System.ComponentModel.DataAnnotations;

    public class Answer : Entity
    {
        [Required]
        [StringLength(
            60,
            MinimumLength = 1,
            ErrorMessage = "Condition should be between 1 and 60 characters"
            )
        ]
        public string Condition { get; set; }

        [Required]
        [StringLength(
            60,
            MinimumLength = 1,
            ErrorMessage = "Category should be between 1 and 60 characters"
            )
        ]
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$")]
        public string Category { get; set; }
    }
}
