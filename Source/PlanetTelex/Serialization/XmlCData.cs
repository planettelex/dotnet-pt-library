using System;
using System.Xml;
using System.Xml.Serialization;
using PlanetTelex.Properties;

namespace PlanetTelex.Serialization
{
    /// <summary>
    /// A class that will be serialized as CData with the <see cref="XmlSerializer"/>.
    /// </summary>
    public sealed class XmlCData : IXmlSerializable
    {
        /// <summary>
        /// The string container of the CDATA contents.
        /// </summary>
        /// <value>
        /// The string value of this CDATA.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value ?? string.Empty;
        }

        #region Operator Overrides

        /// <summary>
        /// Implicitly converts from a <see cref="XmlCData"/> to a <see cref="string"/>.
        /// </summary>
        /// <param name="cdata">The <see cref="XmlCData"/> to be converted</param>
        /// <returns>
        /// The <see cref="string"/> representation
        /// </returns>
        /// <example>
        ///   <code>
        /// string text = new CDataString("Testing")
        ///   </code>
        ///   </example>
        public static implicit operator string(XmlCData cdata)
        {
            return cdata.Value;
        }

        /// <summary>
        /// Implicitly converts from a <see cref="string"/> to a <see cref="XmlCData"/>.
        /// </summary>
        /// <param name="text">The <see cref="string"/> to be converted.</param>
        /// <returns>
        /// A <see cref="XmlCData"/> object with its Text property initialized to <paramref name="text"/>
        /// </returns>
        /// <example>
        ///   <code>
        /// CDataString text = "Some string values";
        ///   </code>
        ///   </example>
        public static implicit operator XmlCData(string text)
        {
            return text == null ? null : new XmlCData { Value = text };
        }

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
        /// Writes this element out in a CDATA tag.
        /// </summary>
        /// <param name="writer">The <see cref="XmlWriter"/> to write to</param>
        /// <example>
        /// <code> Ouput XML examples
        /// "" => <Node/>
        /// "Foo" => <Node><![CDATA[Foo]]></Node>
        /// </code>
        /// </example>
        public void WriteXml(XmlWriter writer)
        {
            if (!string.IsNullOrEmpty(Value))
                writer.WriteCData(Value);
        }

        /// <summary>
        /// Reads this element in from the reader.
        /// </summary>
        /// <param name="reader">The reader to read from</param>
        /// <example> Input XML examples
        /// <code>
        /// <Node/> => ""
        /// <Node></Node> => ""
        /// <Node>Foo</Node> => "Foo"
        /// <Node><![CDATA[Foo]]></Node> => "Foo"
        /// </code>
        /// </example>
        public void ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                Value = string.Empty;
            else
            {
                reader.Read();
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        Value = string.Empty;
                        break;
                    case XmlNodeType.Text:
                    case XmlNodeType.CDATA:
                        Value = reader.ReadContentAsString();
                        break;
                    default:
                        throw new InvalidOperationException(Resources.NoTextOrCDataInXml);
                }
            }
        }

        #endregion
    }
}
