using System;
using System.DirectoryServices.AccountManagement;
using System.IO;
using System.Linq;

namespace ActiveDirectoryQuery
{
    public class CsvOutput : IOutput
    {
        private readonly DefaultOptions _options;
        private readonly StreamWriter _writer;
        private bool _headerWritten;

        public CsvOutput(DefaultOptions options)
        {
            _options = options;
            _writer = new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true };
        }

        public void WriteGroup(GroupPrincipal group)
        {
            if (!_headerWritten)
            {
                _headerWritten = true;

                _writer.Write("Name");

                if (_options.ShowMembership)
                {
                    _writer.Write(";Membership");
                }

                _writer.WriteLine();
            }

            _writer.Write("\"" + group.Name + "\"");

            if (_options.Detailed)
            {
                _writer.Write(";\"" + group.Name + "\";" +
                              "\"" + group.DisplayName + "\";" +
                              "\"" + group.Sid + "\";" +
                              "\"" + group.IsSecurityGroup + "\";" +
                              "\"" + group.Description + "\"");
            }

            if (_options.ShowMembership)
            {
                foreach(var member in group.GetMembers().Select(member => member.Name).ToList())
                {
                    _writer.Write(Environment.NewLine + "\"" + group.Name + "\";\"" + member + "\"");
                }
            }

            _writer.WriteLine();
        }

        public void WriteUser(UserPrincipal user)
        {
            if (!_headerWritten)
            {
                _headerWritten = true;

                _writer.Write("Name");

                if (_options.Detailed)
                {
                    _writer.Write(";DisplayName;Sid;UserPrincipalName;Description");
                }

                if (_options.ShowMembership)
                {
                    _writer.Write(";Membership");
                }

                _writer.WriteLine();
            }

            _writer.Write("\"" + user.Name + "\"");

            if (_options.Detailed)
            {
                _writer.Write(";\"" + user.Name + "\";" +
                              "\"" + user.DisplayName + "\";" +
                              "\"" + user.Sid + "\";" +
                              "\"" + user.UserPrincipalName + "\";" +
                              "\"" + user.Description + "\"");
            }

            if (_options.ShowMembership)
            {
                if (_options.ShowMembership)
                {
                    foreach (var member in user.GetAuthorizationGroups().Select(member => member.Name).ToList())
                    {
                        _writer.Write(Environment.NewLine + "\"" + user.Name + "\";\"" + member + "\"");
                    }
                }
            }

            _writer.WriteLine();
        }
    }
}