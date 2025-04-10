using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transversal.Common.Helper
{
    public static class StringUtil
    {
        public static string GetErrorMessage(this IEnumerable<ValidationFailure> errors) => string.Join(", ", errors);
    }
}
