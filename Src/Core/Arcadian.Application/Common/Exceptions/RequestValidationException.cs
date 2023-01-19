using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using FluentValidation.Results;

namespace Arcadian.Application.Common.Exceptions
{
    public class RequestValidationException : Exception
    {
        public Dictionary<string, string[]> Errors { get; set; }

        public RequestValidationException() : base("One or more validation failures occured")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public RequestValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            var failureGroups = failures.GroupBy(f => f.PropertyName, f => f.ErrorMessage);

            foreach(var fg in failureGroups)
            {
                Errors.Add(fg.Key, fg.ToArray());
            }
        }

    }
}
