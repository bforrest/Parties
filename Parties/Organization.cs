using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parties
{
    public class Organization : IEquatable<Organization>
    {
        public Organization(string name, Guid id)
        {
            Name = name;
            Id = id;
            children = new HashSet<Organization>();
            parents = new HashSet<Organization>();
        }
        
        public string Name { get; protected set; }

        public Guid Id { get; protected set;}

        public IList<Organization> Parents
        {
            get
            {
                return parents.Cast<Organization>().ToList().AsReadOnly();
            }
        }

        public IList<Organization> Children
        {
            get
            {
                return children.Cast<Organization>().ToList().AsReadOnly();
            }
        }

        public Organization AddParent(Organization source)
        {
            parents.Add(source);
            if (!source.Children.Contains(this))
            {
                source.AddChild(this);
            }
            return source;
        }

        public Organization RemoveParent(Organization source)
        {
            parents.Remove(source);
            if (source.Children.Contains(this))
            {
                source.RemoveChild(this);
            }
            return source;
        }

        public Organization AddChild(Organization child)
        {
            children.Add(child);
            if(!child.Parents.Contains(this))
            {
                child.AddParent(this);
            }
            return child;
        }

        public Organization RemoveChild(Organization source)
        {
            children.Remove(source);
            if (source.Parents.Contains(this))
            {
                source.RemoveParent(this);
            }
            return source;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Organization);
        }

        public bool Equals(Organization other)
        {
            if (other == null)
            {
                return false;
            }

            return Id == other.Id;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 31 + (Name == null ? 0 : Name.GetHashCode());
            hash = hash * 31 + Id.GetHashCode();
            return hash;
        }

        protected HashSet<Organization> parents;

        protected HashSet<Organization> children;
    }
}
