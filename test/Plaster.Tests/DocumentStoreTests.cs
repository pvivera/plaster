using System;
using Xunit;

namespace Plaster.Tests
{
    public class DocumentStoreTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        public void path_should_not_be_null(string path)
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentStore(path));
        }
    }
}