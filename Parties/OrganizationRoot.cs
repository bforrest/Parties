using System;
using System.Collections.Generic;
using System.Linq;

namespace Parties
{
    public class OrganizationRoot : Organization
    {
        public OrganizationRoot(string name, Guid Id):base(name, Id)
        {
        }

        public override Organization AddParent(Organization source)
        {            
            throw new InvalidOperationException("Root Organization can not have a parent node.");
            /*
             * Code to prove the rule works
             * return base.AddParent(source);
             */
        }
    }
}