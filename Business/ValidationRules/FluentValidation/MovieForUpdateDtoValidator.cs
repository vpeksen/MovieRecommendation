using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    public class MovieForUpdateDtoValidator : AbstractValidator<MovieForUpdateDto>
    {
        public MovieForUpdateDtoValidator()
        {
            RuleFor(m => m.Vote).InclusiveBetween(1, 10);
        }
    }
}
