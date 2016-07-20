using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Xunit;

namespace FluentAssertionsIssue
{
    public class UsingTest
    {
        [Fact]
        public void FluentAssertionShouldPassWhenValueCodeIsUsed()
        {
            var expectedParent = new Parent(new Child {Name = "TestName", AValueCode = "100"});
            var simulatedResult = new Parent(new Child { Name = "TestName", AValueCode = "100", AValue = "100"});

            simulatedResult.ShouldBeEquivalentTo(
                expectedParent,
                options => options.Using<Child>(info => info.Subject.AValue.Should().Be(info.Expectation.AValueCode ?? info.Expectation.AValue))
                               .When(subject => subject.RuntimeType == typeof(Child) && subject.SelectedMemberInfo.Name == "AValue"));
        }

    }

    public class Parent
    {
        public Parent(params Child[] children)
        {
            Children = new List<Child>(children);
        }

        public List<Child> Children { get; set; }
    }

    public class Child
    {
        public string Name { get; set; }
        public string AValueCode { get; set; }
        public string AValue { get; set; }
    }
}
