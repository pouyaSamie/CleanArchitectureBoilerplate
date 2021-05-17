using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Common.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Failures { get; }

        public ValidationException() :
            base("لطفا خطاهای زیر را بررسی نمایید")
        {
            Failures = new Dictionary<string, string[]>();
        }

        public ValidationException(List<ValidationFailure> failures) :
            this()
        {
            var propertyNames = failures
                .Select(x => x.PropertyName)
                .Distinct()
                .ToList();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailure = failures
                    .Where(x => x.PropertyName == propertyName)
                    .Select(x => x.ErrorMessage)
                    .ToArray();

                Failures.Add(propertyName, propertyFailure);
            }

        }
    }
}
