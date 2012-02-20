using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Reflection;
using NUnit.Framework;
using PlanetTelex.Utilities;

namespace PlanetTelex.UnitTests.Utilities
{
    /// <summary>
    /// Unit tests for ObjectUtility
    /// </summary>
    [TestFixture]
    public class ObjectUtilityTests
    {
        readonly DummyObject _dummyObject = new DummyObject("Dummy Object", 5, (decimal)3.14, new DummyObject("Nested dummy object"));
        private ObjectUtility _objectUtility;

        /// <summary>
        /// Setups this instance.
        /// </summary>
        [SetUp]
        public void Setup()
        {
            _objectUtility = new ObjectUtility();
        }

        /// <summary>
        /// Tests the cloning of an object.
        /// </summary>
        [Test]
        public void CloneTest()
        {
            DummyObject clone = (DummyObject)_objectUtility.Clone(_dummyObject);
            Assert.That(clone.StringProperty, Is.EqualTo(_dummyObject.StringProperty));
            Assert.That(((DummyObject)clone.ObjectProperty).IntProperty, Is.EqualTo(((DummyObject)_dummyObject.ObjectProperty).IntProperty));
        }

        /// <summary>
        /// Tests getting the properties of an object.
        /// </summary>
        [Test]
        public void GetPropertiesTest()
        {
            Collection<PropertyInfo> properties = _objectUtility.GetProperties(typeof(DummyObject));
            ArrayList propertyNames = new ArrayList();
            foreach (PropertyInfo property in properties)
                propertyNames.Add(property.Name);
            
            Assert.That(propertyNames.Contains("StringProperty"));
            Assert.That(propertyNames.Contains("IntProperty"));
            Assert.That(propertyNames.Contains("DecimalProperty"));
            Assert.That(propertyNames.Contains("ObjectProperty"));
        }

        /// <summary>
        /// Tests contains property.
        /// </summary>
        [Test]
        public void ContainsPropertyTest()
        {
            Assert.That(_objectUtility.ContainsProperty("StringProperty", _dummyObject));
            Assert.That(_objectUtility.ContainsProperty("IntProperty", _dummyObject));
            Assert.That(_objectUtility.ContainsProperty("DecimalProperty", _dummyObject));
            Assert.That(_objectUtility.ContainsProperty("ObjectProperty", _dummyObject));
        }

        /// <summary>
        /// Tests is comparable.
        /// </summary>
        [Test]
        public void IsComparableTest()
        {
            Assert.That(_objectUtility.IsComparable(typeof(DummyObject)) == false);
            Assert.That(_objectUtility.IsComparable(typeof(Int32)));
            Assert.That(_objectUtility.IsComparable(typeof(String)));
        }

        /// <summary>
        /// Tests mapping an object.
        /// </summary>
        [Test]
        public void MapObjectTest()
        {
            DummyObject dummyOne = new DummyObject();
            DummyObjectTwo dummyTwo = new DummyObjectTwo();

            _objectUtility.MapObject(_dummyObject, dummyOne);
            _objectUtility.MapObject(_dummyObject, dummyTwo);
            DummyObject nestedNew = (DummyObject)dummyOne.ObjectProperty;
            DummyObject nestedOriginal = (DummyObject) _dummyObject.ObjectProperty;
            
            Assert.That(dummyOne.StringProperty == _dummyObject.StringProperty);
            Assert.That(dummyOne.IntProperty == _dummyObject.IntProperty);
            Assert.That(dummyOne.DecimalProperty == _dummyObject.DecimalProperty);
            Assert.That(nestedNew.StringProperty == nestedOriginal.StringProperty);
            Assert.That(dummyTwo.StringProperty == _dummyObject.StringProperty);

            dummyOne.StringProperty = "New String Property";
            Assert.That(dummyOne.StringProperty != _dummyObject.StringProperty);
        }

        /// <summary>
        /// Test the GetProperty method
        /// </summary>
        [Test]
        public void GetPropertyTest()
        {
            PropertyInfo pinfo = _objectUtility.GetProperty(typeof(DummyObject), "StringProperty");
            Assert.That(pinfo.Name=="StringProperty");
        }
    }
}