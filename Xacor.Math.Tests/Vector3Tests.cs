using FluentAssertions;
using Xunit;

namespace Xacor.Math.Tests
{
    public class Vector3Tests
    {
        [Fact]
        public void Add_Vector3_Vector3()
        {
            var a = new Vector3(1, 1, 1);
            var b = new Vector3(1, 1, 1);

            var result = a + b;

            result.Should().BeEquivalentTo(new Vector3(2, 2, 2));
        }

        [Fact]
        public void Add_Vector3_float()
        {
            var a = new Vector3(1, 1, 1);
            var b = 1.0f;

            var result = a + b;

            result.Should().BeEquivalentTo(new Vector3(2, 2, 2));
        }

        [Fact]
        public void Add_float_Vector3()
        {
            var a = 1.0f;
            var b = new Vector3(1, 1, 1);

            var result = a + b;

            result.Should().BeEquivalentTo(new Vector3(2, 2, 2));
        }

        [Fact]
        public void Subtract_Vector3_Vector3()
        {
            var a = new Vector3(1, 1, 1);
            var b = new Vector3(1, 1, 1);

            var result = a - b;

            result.Should().BeEquivalentTo(new Vector3(0, 0, 0));
        }

        [Fact]
        public void Subtract_Vector3_float()
        {
            var a = new Vector3(1, 1, 1);
            var b = 1.0f;

            var result = a - b;

            result.Should().BeEquivalentTo(new Vector3(0, 0, 0));
        }

        [Fact]
        public void Subtract_float_Vector3()
        {
            var a = 1.0f;
            var b = new Vector3(1, 1, 1);

            var result = a - b;

            result.Should().BeEquivalentTo(new Vector3(0, 0, 0));
        }

        [Fact]
        public void Multiply_Vector3_Vector3()
        {
            var a = new Vector3(1, 1, 1);
            var b = new Vector3(10, 10, 10);

            var result = a * b;

            result.Should().BeEquivalentTo(new Vector3(10, 10, 10));
        }

        [Fact]
        public void Multiply_Vector3_float()
        {
            var a = new Vector3(1, 1, 1);
            var b = 10.0f;

            var result = a * b;

            result.Should().BeEquivalentTo(new Vector3(10, 10, 10));
        }

        [Fact]
        public void Multiply_float_Vector3()
        {
            var a = 10.0f;
            var b = new Vector3(1, 1, 1);

            var result = a * b;

            result.Should().BeEquivalentTo(new Vector3(10, 10, 10));
        }

        [Fact]
        public void Normalized_10_0_0_MustBe_1_0_0()
        {
            var v = new Vector3(10, 0, 0);
            var normalized = v.Normalized();

            normalized.Should().BeEquivalentTo(Vector3.UnitX);
        }

        [Fact]
        public void Normalized_0_10_0_MustBe_0_1_0()
        {
            var v = new Vector3(0, 10, 0);
            var normalized = v.Normalized();

            normalized.Should().BeEquivalentTo(Vector3.UnitY);
        }

        [Fact]
        public void Normalized_0_0_10_MustBe_0_0_1()
        {
            var v = new Vector3(0, 0, 10);
            var normalized = v.Normalized();

            normalized.Should().BeEquivalentTo(Vector3.UnitZ);
        }

        [Fact]
        public void Normalized_Negative10_0_0_MustBe_Negative1_0_0()
        {
            var v = new Vector3(-10, 0, 0);
            var normalized = v.Normalized();

            normalized.Should().BeEquivalentTo(-Vector3.UnitX);
        }

        [Fact]
        public void Normalized_0_Negative10_0_MustBe_0_Negative1_0()
        {
            var v = new Vector3(0, -10, 0);
            var normalized = v.Normalized();

            normalized.Should().BeEquivalentTo(-Vector3.UnitY);
        }

        [Fact]
        public void Normalized_0_0_Negative10_MustBe_0_0_Negative1()
        {
            var v = new Vector3(0, 0, -10);
            var normalized = v.Normalized();

            normalized.Should().BeEquivalentTo(-Vector3.UnitZ);
        }
    }
}
