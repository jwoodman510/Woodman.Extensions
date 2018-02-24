using System.Linq;
using System.Collections.Generic;
using Xunit;
using System;

namespace Test.Woodman.Extensions
{
    public class IEnumerableExtensionsTests
    {
        [Theory(DisplayName = nameof(IEnumerableExtensions.ToListSafe))]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ToListSafe(int? count)
        {
            IEnumerable<string> enumerable = null;

            if(count.GetValueOrDefault() > 0)
            {
                enumerable = new string[count.Value];
            }

            var result = enumerable.ToListSafe();

            Assert.Equal(count.GetValueOrDefault(), result.Count);
        }

        [Theory(DisplayName = nameof(IEnumerableExtensions.ToDictionarySafe))]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ToDictionarySafe(int? count)
        {
            var enumerable = count.ToArray(idx => $"{idx}");

            var result = enumerable.ToDictionarySafe(key => key);
            var result1 = enumerable.ToDictionarySafe(key => key, value => value);

            Assert.Equal(count.GetValueOrDefault(), result.Count);
            Assert.Equal(count.GetValueOrDefault(), result1.Count);
        }

        [Theory(DisplayName = nameof(IEnumerableExtensions.ToHashSetSafe))]
        [InlineData(null)]
        [InlineData(0)]
        [InlineData(1)]
        [InlineData(2)]
        public void ToHashSetSafe(int? count)
        {
            var enumerable = count.ToArray(idx => $"{idx}");

            var result = enumerable.ToHashSetSafe(k => k);
            var result1 = enumerable.ToHashSetSafe();

            Assert.Equal(count.GetValueOrDefault(), result.Count);
            Assert.Equal(count.GetValueOrDefault(), result1.Count);
        }

        [Theory(DisplayName = nameof(IEnumerableExtensions.Compare))]
        [InlineData(null, null, "", "", "")]
        [InlineData(null, "", "", "", "")]
        [InlineData("", null, "", "", "")]
        [InlineData(null, "1,2,3", "", "1,2,3", "")]
        [InlineData("1,2,3", null, "1,2,3", "", "")]
        [InlineData("1,2,3,4", "3,4,5,6", "1,2", "5,6", "3,4")]
        public void Except(string a, string b, string expectedLeft, string expectedRight, string expectedIntersect)
        {
            var arrA = a?.Split(",", System.StringSplitOptions.RemoveEmptyEntries);
            var arrB = b?.Split(",", System.StringSplitOptions.RemoveEmptyEntries);

            var (Left, Right, Intersect) = arrA.Compare(arrB);

            var eLeft = expectedLeft?.Split(",", System.StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
            var eRight = expectedRight?.Split(",", System.StringSplitOptions.RemoveEmptyEntries) ?? new string[0];
            var eIntersect = expectedIntersect?.Split(",", System.StringSplitOptions.RemoveEmptyEntries) ?? new string[0];

            Assert.Equal(eLeft.Length, Left.Count);
            Assert.Equal(eRight.Length, Right.Count);
            Assert.Equal(eIntersect.Length, Intersect.Count);

            foreach(var val in eLeft)
            {
                Assert.Contains(val, Left);
            }

            foreach (var val in eRight)
            {
                Assert.Contains(val, Right);
            }

            foreach (var val in eIntersect)
            {
                Assert.Contains(val, Intersect);
            }
        }
    }
}
