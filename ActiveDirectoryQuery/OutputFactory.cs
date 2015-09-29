using System;

namespace ActiveDirectoryQuery
{
    public class OutputFactory
    {
        public static IOutput FromOptions(DefaultOptions options)
        {
            if (string.Equals(options.Output, "console", StringComparison.InvariantCultureIgnoreCase))
            {
                return new ConsoleOutput(options.ShowMembership, options.Detailed);
            }

            if (string.Equals(options.Output, "csv", StringComparison.InvariantCultureIgnoreCase))
            {
                return new CsvOutput(options);
            }

            var exception = new Exception("Invalid output type");
            exception.Data.Add("type", options.Output);
            throw exception;
        }
    }
}