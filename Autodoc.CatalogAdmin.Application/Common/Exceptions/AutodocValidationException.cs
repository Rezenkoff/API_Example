using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autodoc.CatalogAdmin.Application.Common.Exceptions
{
    public class AutodocValidationException : Exception
    {
        public AutodocValidationException ()
            : base("One or more validation failures have occurred.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public AutodocValidationException (IEnumerable<ValidationFailure> failures)
            : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
                .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }

        public IDictionary<string, string[]> Errors { get; }
    }
}

