using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSpec;
using Parties;
namespace PartiesTests
{
    class describe_organization : nspec
    {
         Organization child;
        Organization parent;

        void before_each()
        {
            child = new Organization("child", Guid.NewGuid());
            parent = new Organization("parent", Guid.NewGuid());
        }

        void when_adding_a_child()
        {
            context["when adding a child organization"] = () =>
            {
                before = () =>
                {
                    parent.AddChild(child);
                };

                it["should contain a child"] = () =>
                {
                    parent.Children.should_contain(child);
                };

                it["should add the parent to the child's parent collection"] = () =>
                {
                    child.Parents.should_contain(parent);
                };
            };
        }

        void when_adding_a_parent()
        {
            context["when adding a parent organization"] = () =>
            {
                before = () =>
                {
                    child.AddParent(parent);
                };

                it["should add parent to the parents collection"] = () =>
                {
                    child.Parents.should_contain(parent);
                };

                it["should add the child to the parent's child collection"] = () =>
                {
                    parent.Children.should_contain(child);
                };
            };
        }

        void when_removing_a_child()
        {
            context["when an organization has a child"] = () =>
            {
                before = () =>
                {
                    parent.AddChild(child);
                };

                context["when removing the child organization"] = () =>
                {
                    before = () =>
                    {
                        parent.RemoveChild(child);
                    };

                    it["should be removed for the child collection"] = () =>
                    {
                        parent.Children.should_not_contain(child);
                    };

                    it["the child's parent should be removed as well"] = () =>
                    {
                        child.Parents.should_not_contain(parent);
                    };
                };                
            };
        }

        void when_removing_a_parent_organization()
        {
            context["when an organization has a parent"] = () =>
            {
                before = () =>
                {
                    child.AddParent(parent);
                };

                context["when removing the parent"] = () =>
                {
                    before = () =>
                    {
                        child.RemoveParent(parent);
                    };

                    it["should no longer have that parent organization"] = () =>
                    {
                        child.Parents.should_not_contain(parent);
                        child.Parents.should_be_empty();
                    };

                    it["parent organization shouldn't contain that child"] = () =>
                    {
                        parent.Children.should_be_empty();
                    };
                };
            };
        }

        void when_comparing_organizations()
        {
            Guid id = Guid.NewGuid();
            Organization first = new Organization("first", id);
            Organization second = new Organization("second", id);

            context["organizations with the same id"] = () =>
            {    
                //before = () =>
                //{
                //    first = new Organization("first");
                //    //second = new Organization("second");
                //};

                it["should be equal when the id values are equal"] = () =>
                {
                    first.Is(second);
                };
            };
        }
    }
}