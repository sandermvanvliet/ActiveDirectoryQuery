using System;
using System.DirectoryServices.AccountManagement;
using System.Linq;

namespace ActiveDirectoryQuery
{
    public class ConsoleOutput : IOutput
    {
        private readonly bool _showMembership;
        private readonly bool _showPrincipalDetails;

        public ConsoleOutput(bool showMembership, bool showPrincipalDetails)
        {
            _showMembership = showMembership;
            _showPrincipalDetails = showPrincipalDetails;
        }

        public void WriteGroup(GroupPrincipal group)
        {
            Console.WriteLine(group.Name);

            if (_showPrincipalDetails)
            {
                Console.WriteLine("\tDisplayName:     "+ $"{group.DisplayName}");
                Console.WriteLine("\tSid:             " + $"{group.Sid}");
                Console.WriteLine("\tIsSecurityGroup: " + $"{group.IsSecurityGroup}");
                Console.WriteLine("\tDescription:     " + $"{group.Description}");
            }

            if (_showMembership)
            {
                Console.WriteLine("\tMembers:");
                foreach (var member in group.GetMembers().ToList())
                {
                    Console.WriteLine("\t\t" + $"{member.Name} ({member.StructuralObjectClass})");
                }
            }
        }

        public void WriteUser(UserPrincipal user)
        {
            Console.WriteLine(user.Name);

            if (_showPrincipalDetails)
            {
                Console.WriteLine("\tDisplayName:     " + $"{user.DisplayName}");
                Console.WriteLine("\tSid:             " + $"{user.Sid}");
                Console.WriteLine("\tIsSecurityGroup: " + $"{user.UserPrincipalName}");
                Console.WriteLine("\tDescription:     " + $"{user.Description}");
            }

            if (_showMembership)
            {
                Console.WriteLine("\tMembers:");
                foreach (var member in user.GetAuthorizationGroups().ToList())
                {
                    Console.WriteLine("\t\t" + $"{member.Name} ({member.StructuralObjectClass})");
                }
            }
        }
    }
}