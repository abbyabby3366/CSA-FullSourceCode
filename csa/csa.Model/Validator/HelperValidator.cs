using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace csa.Model.Validator
{
    public static class HelperValidator
    {
        public static bool MustNotNullAndZero(int? value)
        {
            return value.HasValue && value != 0;
        }

        public static bool MustNotZero(int? value)
        {
            return value != 0;
        }

        public static bool MustNotNullAndEmpty(string value)
        {
            return value != null && value.Trim() != "";
        }

        public static bool MustNumberPositiveNotNull(int? value)
        {
            return value != null && value > -1;
        }
    }
}
