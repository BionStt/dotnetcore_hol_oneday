﻿#region copyright

// Copyright Information
// ==================================
// SpyStore.Hol - SpyStore.Hol.Dal.Tests - XUnitExamples.cs
// All samples copyright Philip Japikse
// http://www.skimedic.com 2019/10/04
// See License.txt for more information
// ==================================

#endregion

using System;
using Xunit;

namespace SpyStore.Hol.Dal.Tests
{
    public class XUnitExamples : IDisposable
    {
        public XUnitExamples()
        {
            //Runs before every test
        }

        public void Dispose()
        {
            //runs after every test
        }

        [Fact]
        public void Foo()
        {
            Assert.Equal(1, 1);
        }

        [Theory]
        [InlineData(4, 8, 12)]
        [InlineData(-4, 8, 4)]
        //[InlineData(4,int.MaxValue,4)]
        public void ShouldAddTwoNumbers(int addend1, int addend2, int result)
        {
            Assert.Equal(result, Add(addend1, addend2));
        }

        private int Add(int addend1, int addend2)
        {
            return addend1 + addend2;
        }
    }
}