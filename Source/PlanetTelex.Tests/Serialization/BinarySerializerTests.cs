using NUnit.Framework;
using PlanetTelex.Serialization;

namespace PlanetTelex.UnitTests.Serialization
{
    /// <summary>
    /// Unit tests for <see cref="BinarySerializer"/>.
    /// </summary>
    [TestFixture]
    public class SerializeTests
    {
        readonly DummyObject _dummy = new DummyObject("Dummy Object", 5, (decimal)3.14, new DummyObject("Nested dummy object"));
        readonly BinarySerializer _binarySerializer = new BinarySerializer();

        /// <summary>
        /// Test serializing an object.
        /// </summary>
        [Test]
        public void SerializeTest()
        {
            byte[] binarySerializedObject = _binarySerializer.Serialize((object)_dummy);
            DummyObject newDummyObject = (DummyObject)_binarySerializer.Deserialize(binarySerializedObject);
            DummyObject nestedOriginal = (DummyObject)_dummy.ObjectProperty;
            DummyObject nestedNew = (DummyObject)newDummyObject.ObjectProperty;

            Assert.That(_dummy.StringProperty == newDummyObject.StringProperty);
            Assert.That(_dummy.IntProperty == newDummyObject.IntProperty);
            Assert.That(_dummy.DecimalProperty == newDummyObject.DecimalProperty);
            Assert.That(nestedOriginal.StringProperty == nestedNew.StringProperty);
        }

        /// <summary>
        /// Test deserializing an object.
        /// </summary>
        [Test]
        public void DeserializeTest()
        {
            byte[] binaryDummy = _binarySerializer.Serialize((object)_dummy);
            DummyObject deserialized = (DummyObject)_binarySerializer.Deserialize(binaryDummy);

            Assert.That(deserialized.StringProperty, Is.EqualTo(_dummy.StringProperty));
            Assert.That(deserialized.DecimalProperty, Is.EqualTo(_dummy.DecimalProperty));
            Assert.That(((DummyObject)deserialized.ObjectProperty).StringProperty, Is.EqualTo(((DummyObject)_dummy.ObjectProperty).StringProperty));

            byte[] binaryTest = _binarySerializer.Serialize((object)"Test");
            string deserializedTest = (string)_binarySerializer.Deserialize(binaryTest);
            Assert.That(deserializedTest, Is.EqualTo("Test"));
        }
    }
}