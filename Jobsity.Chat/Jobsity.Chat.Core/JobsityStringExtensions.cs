using System.Text.RegularExpressions;

namespace Jobsity.Chat.Core
{
    public static class JobsityStringExtensions
    {
        public static string ProcessTemplate(this string template, object data) 
        { 
            var typeOfData = data.GetType();
            var dataProps = typeOfData.GetProperties();
            var processed = template;

            foreach(var prop in dataProps ) 
            { 
                var value = prop.GetValue(data);

                processed = 
                    Regex.Replace(
                        processed, 
                        string.Concat(@"\{{1}", prop.Name, @"\}{1}"), 
                        (value?.ToString() ?? ""), 
                        RegexOptions.IgnoreCase
                    );
            }

            return processed;
        }
    }
}
