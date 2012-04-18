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
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace PlanetTelex.Serialization
{
    /// <summary>
    /// A generic dictionary that can be XML serialized, a limitation of IDictionary.
    /// </summary>
    [Serializable]
    [XmlRoot("dictionary")]
    public class XmlSerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, IXmlSerializable
    {
        private readonly XmlSerializer _xmlSerializer = new XmlSerializer();

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSerializableDictionary&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        public XmlSerializableDictionary() {}

        /// <summary>
        /// Initializes a new instance of the <see cref="XmlSerializableDictionary&lt;TKey, TValue&gt;"/> class.
        /// </summary>
        /// <param name="info">A <see cref="T:System.Runtime.Serialization.SerializationInfo"/> object containing the information required to serialize the <see cref="T:System.Collections.Generic.Dictionary`2"/>.</param>
        /// <param name="context">A <see cref="T:System.Runtime.Serialization.StreamingContext"/> structure containing the source and destination of the serialized stream associated with the <see cref="T:System.Collections.Generic.Dictionary`2"/>.</param>
        protected XmlSerializableDictionary(SerializationInfo info, StreamingContext context) : base(info, context) { }

        #endregion

        #region IXmlSerializable Members

        /// <summary>
        /// This method is reserved and should not be used. When implementing the IXmlSerializable interface, you should return null (Nothing in Visual Basic) from this method, and instead, if specifying a custom schema is required, apply the <see cref="T:System.Xml.Serialization.XmlSchemaProviderAttribute"/> to the class.
        /// </summary>
        /// <returns>
        /// An <see cref="T:System.Xml.Schema.XmlSchema"/> that describes the XML representation of the object that is produced by the <see cref="M:System.Xml.Serialization.IXmlSerializable.WriteXml(System.Xml.XmlWriter)"/> method and consumed by the <see cref="M:System.Xml.Serialization.IXmlSerializable.ReadXml(System.Xml.XmlReader)"/> method.
        /// </returns>
        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        /// <summary>
        /// Generates an object from its XML representation.
        /// </summary>
        /// <param name="reader">The <see cref="T:System.Xml.XmlReader"/> stream from which the object is deserialized.</param>
        public void ReadXml(System.Xml.XmlReader reader)
        {
            bool wasEmpty = reader.IsEmptyElement;
            reader.Read();

            if (wasEmpty)
                return;

            while (reader.NodeType != System.Xml.XmlNodeType.EndElement)
            {
                reader.ReadStartElement("item");
                
                reader.ReadStartElement("key");
                TKey key = _xmlSerializer.Deserialize<TKey>(reader.ReadContentAsString());
                reader.ReadEndElement();

                reader.ReadStartElement("value");
                TValue value = _xmlSerializer.Deserialize<TValue>(reader.ReadContentAsString());
                reader.ReadEndElement();

                Add(key, value);

                reader.ReadEndElement();
                reader.MoveToContent();
            }
            reader.ReadEndElement();
        }

        /// <summary>
        /// Converts an object into its XML representation.
        /// </summary>
        /// <param name="writer">The <see cref="T:System.Xml.XmlWriter"/> stream to which the object is serialized.</param>
        public void WriteXml(System.Xml.XmlWriter writer)
        {
            foreach (TKey key in Keys)
            {
                writer.WriteStartElement("item");

                writer.WriteStartElement("key");
                writer.WriteString(_xmlSerializer.Serialize(key));
                writer.WriteEndElement(); // End key

                writer.WriteStartElement("value");
                TValue value = this[key];
                writer.WriteString(_xmlSerializer.Serialize(value));
                writer.WriteEndElement(); // End value

                writer.WriteEndElement(); // End item
            }
        }

        #endregion
    }
}