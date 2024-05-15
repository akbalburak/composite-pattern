using System.Text.RegularExpressions;

namespace Wave.Engine.Composite.Extends
{
    public static class NamingExtension
    {
        public static string SpaceBeforeUpper(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            string pattern = @"(?<=[A-Z])(?=[A-Z][a-z])|(?<=[a-z])(?=[A-Z])";
            string result = Regex.Replace(input, pattern, " ");

            return result;
        }
    }
}
