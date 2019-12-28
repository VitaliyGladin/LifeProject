using Xunit;
using Life.Model;
using System;
namespace ModelTests
{
    public class FieldTests
    {
        [Fact]
        public void Instance_IsNotInit_AutoInit()
        {
            Assert.NotNull(Field.Instance);
        }
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void Init_InvalidLength_ThrowArgumentException(int length)
        {
            Assert.Throws<ArgumentException>(() => Field.Init(length));
        }

        [Fact]
        public void Init_NotChangeInstance()
        {
            Field field = Field.Instance;
            Field.Init();
            Assert.Equal(field.ToString(), Field.Instance.ToString());
        }
    }
}