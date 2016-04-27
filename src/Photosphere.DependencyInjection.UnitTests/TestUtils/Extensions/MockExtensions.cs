using System;
using Moq;

namespace Photosphere.DependencyInjection.UnitTests.TestUtils.Extensions
{
    internal static class MockExtensions
    {
        public static T GetInstance<T>(this Mock<T> mock, Action<Mock<T>> setupAction) where T : class
        {
            setupAction(mock);
            return mock.Object;
        }
    }
}