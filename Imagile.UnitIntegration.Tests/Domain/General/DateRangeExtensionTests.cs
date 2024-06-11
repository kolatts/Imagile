using Imagile.Domain.General.DateTimes;

namespace Imagile.UnitIntegration.Tests.Domain.General;
public class DateRangeTests
{
    [Test]
    public void OverlapsWith_ShouldReturnTrue_WhenRangesOverlap()
    {
        // Arrange
        var range1 = new DateRange
        {
            Start = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero),
            End = new DateTimeOffset(2022, 1, 10, 0, 0, 0, TimeSpan.Zero)
        };

        var range2 = new DateRange
        {
            Start = new DateTimeOffset(2022, 1, 5, 0, 0, 0, TimeSpan.Zero),
            End = new DateTimeOffset(2022, 1, 15, 0, 0, 0, TimeSpan.Zero)
        };

        // Act
        var result = range1.OverlapsWith(range2);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void OverlapsWith_ShouldReturnFalse_WhenRangesDoNotOverlap()
    {
        // Arrange
        var range1 = new DateRange
        {
            Start = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero),
            End = new DateTimeOffset(2022, 1, 10, 0, 0, 0, TimeSpan.Zero)
        };

        var range2 = new DateRange
        {
            Start = new DateTimeOffset(2022, 1, 11, 0, 0, 0, TimeSpan.Zero),
            End = new DateTimeOffset(2022, 1, 15, 0, 0, 0, TimeSpan.Zero)
        };

        // Act
        var result = range1.OverlapsWith(range2);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void OverlapsWith_ShouldReturnTrue_WhenRangesAreEqual()
    {
        // Arrange
        var range1 = new DateRange
        {
            Start = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero),
            End = new DateTimeOffset(2022, 1, 10, 0, 0, 0, TimeSpan.Zero)
        };

        var range2 = new DateRange
        {
            Start = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero),
            End = new DateTimeOffset(2022, 1, 10, 0, 0, 0, TimeSpan.Zero)
        };

        // Act
        var result = range1.OverlapsWith(range2);

        // Assert
        Assert.IsTrue(result);
    }

    [Test]
    public void OverlapsWith_ShouldReturnTrue_WhenOneRangeIsInsideAnotherRange()
    {
        // Arrange
        var range1 = new DateRange
        {
            Start = new DateTimeOffset(2022, 1, 1, 0, 0, 0, TimeSpan.Zero),
            End = new DateTimeOffset(2022, 1, 15, 0, 0, 0, TimeSpan.Zero)
        };

        var range2 = new DateRange
        {
            Start = new DateTimeOffset(2022, 1, 5, 0, 0, 0, TimeSpan.Zero),
            End = new DateTimeOffset(2022, 1, 10, 0, 0, 0, TimeSpan.Zero)
        };

        // Act
        var result = range1.OverlapsWith(range2);

        // Assert
        Assert.IsTrue(result);
    }
}
