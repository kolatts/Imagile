using FluentAssertions;
using Imagile.Domain.Attributes;

namespace Imagile.UnitIntegration.Tests;

public class EnumExtensionTests
{
    public enum TestEnumTypes
    {
        A,
        [Includes<TestEnumTypes>(A)]
        B,
        [Includes<TestEnumTypes>(B)]
        C
    }

    [Test]
    public void GetIncluded_Succeeds()
    {
        var included = TestEnumTypes.B.GetIncluded().ToList();

        included.Should().HaveCount(1);
        included.Should().Contain(TestEnumTypes.A);
    }

    [Test]
    public void GetIncluded_Succeeds_WithRecursion()
    {
        var included = TestEnumTypes.C.GetIncluded().ToList();

        included.Should().HaveCount(2);
        included.Should().Contain(TestEnumTypes.A);
        included.Should().Contain(TestEnumTypes.B);
    }
}