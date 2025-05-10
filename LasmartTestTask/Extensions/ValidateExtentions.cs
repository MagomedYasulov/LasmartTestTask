using FluentValidation;
using System.Text.RegularExpressions;

namespace LasmartTestTask.Extensions
{
    public static partial class ValidateExtentions
    {
        public static IRuleBuilderOptions<T, string> HEX<T>(this IRuleBuilderOptions<T, string> ruleBuilder)
        {
            var regex = HEXRegex();
            return ruleBuilder.Matches(regex);
        }

        [GeneratedRegex(@"^#(?:[0-9a-fA-F]{3}){1,2}$")]
        private static partial Regex HEXRegex();
    }
}
