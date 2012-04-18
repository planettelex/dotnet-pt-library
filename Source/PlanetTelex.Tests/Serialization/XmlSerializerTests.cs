/**
 * Copyright (c) 2012 Planet Telex Inc. all rights reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *         http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
﻿using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;
using PlanetTelex.Serialization;

namespace PlanetTelex.UnitTests.Serialization
{
    /// <summary>
    /// Unit tests for <see cref="XmlSerializer"/>.
    /// </summary>
    [TestFixture]
    public class XmlSerializerTests
    {
        private const string DUMMY_OBJECT_XML_SERIALIZED = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<DummyObject xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\">\r\n  <StringProperty>Dummy Object</StringProperty>\r\n  <IntProperty>5</IntProperty>\r\n  <DecimalProperty>3.14</DecimalProperty>\r\n  <ObjectProperty xsi:type=\"DummyObject\">\r\n    <StringProperty>Nested dummy object</StringProperty>\r\n    <IntProperty>0</IntProperty>\r\n    <DecimalProperty>0</DecimalProperty>\r\n  </ObjectProperty>\r\n</DummyObject>";
        private const string DUMMY_OBJECT_XML_DOCUMENT_SERIALIZED = "<?xml version=\"1.0\" encoding=\"utf-16\"?>\r\n<DummyObject>\r\n  <StringProperty>Dummy Object</StringProperty>\r\n  <IntProperty>5</IntProperty>\r\n  <DecimalProperty>3.14</DecimalProperty>\r\n  <ObjectProperty>\r\n    <StringProperty>Nested dummy object</StringProperty>\r\n    <IntProperty>0</IntProperty>\r\n    <DecimalProperty>0</DecimalProperty>\r\n  </ObjectProperty>\r\n</DummyObject>";
        
        readonly DummyObject _dummy = new DummyObject("Dummy Object", 5, (decimal)3.14, new DummyObject("Nested dummy object"));
        readonly XmlSerializer _xmlSerializer = new XmlSerializer();

        /// <summary>
        /// Test serializing an object.
        /// </summary>
        [Test]
        public void SerializeTest()
        {
            string xmlSerializedObject = _xmlSerializer.Serialize(_dummy);
            DummyObject newDummyObject = _xmlSerializer.Deserialize<DummyObject>(xmlSerializedObject);
            DummyObject nestedOriginal = (DummyObject)_dummy.ObjectProperty;
            DummyObject nestedNew = (DummyObject)newDummyObject.ObjectProperty;

            Assert.That(_dummy.StringProperty == newDummyObject.StringProperty);
            Assert.That(_dummy.IntProperty == newDummyObject.IntProperty);
            Assert.That(_dummy.DecimalProperty == newDummyObject.DecimalProperty);
            Assert.That(nestedOriginal.StringProperty == nestedNew.StringProperty);
        }

        /// <summary>
        /// Test XML document serializing an object.
        /// </summary>
        [Test]
        public void XmlDocumentSerializeTest()
        {
            XmlDocument xmlDocument = _xmlSerializer.SerializeAsXmlDocument(_dummy);
            StringBuilder stringBuilder = new StringBuilder();
            TextWriter textWriter = new StringWriter(stringBuilder);
            xmlDocument.Save(textWriter);
            string document = stringBuilder.ToString();
            textWriter.Close();
            Assert.That(System.String.Compare(document, DUMMY_OBJECT_XML_DOCUMENT_SERIALIZED, System.StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <summary>
        /// Test deserializing an object.
        /// </summary>
        [Test]
        public void DeserializeTest()
        {
            DummyObject deserialized = _xmlSerializer.Deserialize<DummyObject>(DUMMY_OBJECT_XML_SERIALIZED);

            Assert.That(deserialized.StringProperty, Is.EqualTo(_dummy.StringProperty));
            Assert.That(deserialized.DecimalProperty, Is.EqualTo(_dummy.DecimalProperty));
            Assert.That(((DummyObject)deserialized.ObjectProperty).StringProperty, Is.EqualTo(((DummyObject)_dummy.ObjectProperty).StringProperty));
        }
    }
}