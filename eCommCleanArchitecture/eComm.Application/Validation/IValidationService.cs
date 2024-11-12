using eComm.Application.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eComm.Application.Validation
{
    public interface IValidationService
    {
        Task<ServiceResponse> ValidateAsync<T>(T Model, IValidator<T> validator);
    }
}
