using FluentAssertions;
using Imagile.Domain.Attributes;
using Imagile.Domain.Extensions;

namespace Imagile.UnitIntegration.Tests;

public class EnumExtensionTests
{
    public enum TestEnumTypes
    {
        A,
        [Includes<TestEnumTypes>(A)]
        B,
        [Includes<TestEnumTypes>(B)]
        C,
        D,
        [DerivedIncludes(D)]
        E,
        [Requires<TestEnumTypes>(A)]
        F
    }

    public enum TestEnumAlternativeTypes
    {
        Z,
        Y,
        X,
        W,
        V,
        U
    }

    public class DerivedIncludesAttribute(params TestEnumTypes[] includes) : IncludesAttribute<TestEnumTypes>(includes)
    {
    }

    [Test]
    public void GetIncluded_Succeeds()
    {
        var included = TestEnumTypes.B.GetIncluded().ToList();

        included.Should().HaveCount(1);
        included.Should().Contain(TestEnumTypes.A);
    }

    [Test]
    public void GetIncluded_Succeeds_Empty()
    {
        var included = TestEnumTypes.A.GetIncluded().ToList();

        included.Should().BeEmpty();
    }

    [Test]
    public void GetIncluded_Succeeds_WithRecursion()
    {
        var included = TestEnumTypes.C.GetIncluded().ToList();

        included.Should().HaveCount(2);
        included.Should().Contain(TestEnumTypes.A);
        included.Should().Contain(TestEnumTypes.B);
    }

    [Test]
    public void GetIncluded_DerivedAttribute_Succeeds()
    {
        var included = TestEnumTypes.E.GetIncluded().ToList();

        included.Should().HaveCount(1);
        included.Should().Contain(TestEnumTypes.D);
    }

    [Test]
    public void GetRequired_Succeeds()
    {
        var required = TestEnumTypes.F.GetRequired().ToList();

        required.Should().HaveCount(1);
        required.Should().Contain(TestEnumTypes.A);
    }

    [Test]
    public void GetRequired_Succeeds_Empty()
    {
        var required = TestEnumTypes.A.GetRequired().ToList();

        required.Should().BeEmpty();
    }
}

