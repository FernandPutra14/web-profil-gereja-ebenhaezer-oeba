using PKMGerejaEbenhaezer.Domain.ValueObjects;

namespace PKMGerejaEbenhaezer.UnitTest
{
    public class AyatAlkitabTests
    {
        [ClassData(typeof(AyatAlkitabTestData))]
        [Theory]
        public void ToString_Should_EqualToValueString(AyatAlkitab value, string valueString)
        {
            //Act
            var s = value.ToString();

            //Assert
            Assert.Equal(valueString, s);
        }

        [ClassData(typeof(AyatAlkitabTestData))]
        [Theory]
        public void TryParse_Should_ReturnTrueAndResultEqualToValue(AyatAlkitab value, string valueString)
        {
            //Act
            var isValid = AyatAlkitab.TryParse(valueString, null, out AyatAlkitab? result);

            //Assert
            Assert.True(isValid);
            Assert.NotNull(result);
            Assert.Equal(value, result);
        }

        [ClassData(typeof(AyatAlkitabEqualTestData))]
        [Theory]
        public void Equals_Should_ReturnTrue(AyatAlkitab value1, AyatAlkitab value2)
        {
            //Act
            var result = value1.Equals(value2);

            //Assert
            Assert.Equal(value1, value2);
            Assert.True(result);
        }

        [ClassData(typeof(AyatAlkitabEqualTestData))]
        [Theory]
        public void EqualOperator_Should_ReturnTrue(AyatAlkitab value1, AyatAlkitab value2)
        {
            //Act
            var result = value1 == value2;

            //Assert
            Assert.Equal(value1, value2);
            Assert.True(result);
        }

        [ClassData(typeof(AyatAlkitabNotEqualTestData))]
        [Theory]
        public void Equals_Should_ReturnFalse(AyatAlkitab value1, AyatAlkitab value2)
        {
            //Act
            var result = value1.Equals(value2);

            //Assert
            Assert.NotEqual(value1, value2);
            Assert.False(result);
        }

        [ClassData(typeof(AyatAlkitabNotEqualTestData))]
        [Theory]
        public void EqualOperator_Should_ReturnFalse(AyatAlkitab value1, AyatAlkitab value2)
        {
            //Act
            var result = value1 == value2;

            //Assert
            Assert.NotEqual(value1, value2);
            Assert.False(result);
        }
    }

    public class AyatAlkitabTestData : TheoryData<AyatAlkitab, string>
    {
        public AyatAlkitabTestData()
        {
            Add(new AyatAlkitab(Kitab.Kejadian, 2, new int[] { 2 }), "Kejadian 2:2");
            Add(new AyatAlkitab(Kitab.Kejadian, 2, 2, 5), "Kejadian 2:2-5");
            Add(new AyatAlkitab(Kitab.Kejadian, 2, new int[] { 2, 5, 7 }), "Kejadian 2:2,5,7");
            Add(new AyatAlkitab(Kitab.Samuel1, 12, new int[] { 1 }), "1 Samuel 12:1");
            Add(new AyatAlkitab(Kitab.Samuel2, 12, Array.Empty<int>()), "2 Samuel 12");
        }
    }

    public class AyatAlkitabEqualTestData : TheoryData<AyatAlkitab, AyatAlkitab>
    {
        public AyatAlkitabEqualTestData()
        {
            foreach(Kitab item in Enum.GetValues(typeof(Kitab)))
            {
                var ayat1 = new AyatAlkitab(item, 1, Array.Empty<int>());
                var ayat2 = new AyatAlkitab(item, 12, new int[] { 1, 100 });
                var ayat3 = new AyatAlkitab(item, 123, new int[] { 2, 5, 6, 8 });

                Add(ayat1, ayat1);
                Add(ayat2, ayat2);
                Add(ayat3, ayat3);
            }
        }
    }

    public class AyatAlkitabNotEqualTestData : TheoryData<AyatAlkitab, AyatAlkitab>
    {
        public AyatAlkitabNotEqualTestData()
        {
            foreach (Kitab item in Enum.GetValues(typeof(Kitab)))
            {
                var ayat1 = new AyatAlkitab(item, 1, Array.Empty<int>());
                var ayat2 = new AyatAlkitab(item, 12, new int[] { 1, 100 });
                var ayat3 = new AyatAlkitab(item, 123, new int[] { 2, 5, 6, 8 });

                Add(ayat1, ayat2);
                Add(ayat1, ayat3);
                Add(ayat2, ayat3);
                Add(ayat3, ayat1);
            }
        }
    }
}
