using System;
using System.Collections.Generic;
using System.Linq;
using NSpec;
using Parties;

namespace PartiesTests
{
    class describe_root_organization : nspec
    {
        void when_adding_a_parent()
        {
            Organization root = new OrganizationRoot("I'm a root", Guid.NewGuid());
            context["to a root organization"] = () =>
            {
                it["should throw invalid operation"] = 
                    expect<InvalidOperationException>(() => root.AddParent(new Organization("child", Guid.NewGuid())));
            };
        }
    }
}