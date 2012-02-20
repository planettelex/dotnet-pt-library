using System;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace PlanetTelex.Serialization
{
    /// <summary>
    /// An XML serializer.
    /// </summary>
    public class XmlSerializer : ISerializer
    {
        #region Implementation of ISerializer

        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <typeparam name="T">The type of object being serialized.</typeparam>
        /// <param name="instance">The object to serialize.</param>
        /// <returns>A string.</returns>
        public string Serialize<T>(T instance)
        {
            StringBuilder builder = new StringBuilder();

            XmlWriterSettings writerSettings = new XmlWriterSettings();
            writerSettings.OmitXmlDeclaration = true;

            using (XmlWriter xmlWriter = XmlWriter.Create(builder, writerSettings))
            using (XmlDictionaryWriter writer = XmlDictionaryWriter.CreateDictionaryWriter(xmlWriter))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                namespaces.Add(string.Empty, string.Empty);
                serializer.Serialize(writer, instance, namespaces);
            }

            return builder.ToString();
        }

        /// <summary>
        /// Deserializes a string into an object.
        /// </summary>
        /// <typeparam name="T">The type of object being deserialized.</typeparam>
        /// <param name="serializedInstance">A string representation of the object type specified.</param>
        /// <returns>A new object of the type specified.</returns>
        public T Deserialize<T>(string serializedInstance)
        {
            if (string.IsNullOrEmpty(serializedInstance)) return default(T);

            T instance;

            using (XmlReader xmlReader = XmlReader.Create(new StringReader(serializedInstance)))
            using (XmlDictionaryReader reader = XmlDictionaryReader.CreateDictionaryReader(xmlReader))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
                instance = (T)serializer.Deserialize(reader);
            }

            return instance;
        }

        /// <summary>
        /// Determines if the instance is serializable.
        /// </summary>
        /// <typeparam name="T">The type of object being checked.</typeparam>
        /// <param name="instance">Object instance to be checked.</param>
        /// <returns><c>true</c> if this instance is serializable; otherwise, <c>false</c>.</returns>
        public bool IsSerializable<T>(T instance) where T : class, new()
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            return instance.GetType().IsSerializable;
        }

        /// <summary>
        /// Serializes and deserializes an object instance to test that the class is correctly configured for serialization.
        /// </summary>
        /// <typeparam name="T">The type of object being serialized.</typeparam>
        /// <param name="instance">Object instance to be checked.</param>
        /// <returns>A new object instance.</returns>
        public T RoundtripSerialize<T>(T instance) where T : class, new()
        {
            if (instance == null)
                throw new ArgumentNullException("instance");

            return Deserialize<T>(Serialize(instance));
        }

        #endregion

        #region Additional Methods

        /// <summary>
        /// Converts an object into an <see cref="XmlDocument"/>.
        /// </summary>
        /// <param name="toSerialize">The object to serialize.</param>
        /// <returns>An <see cref="XmlDocument"/></returns>
        public XmlDocument SerializeAsXmlDocument<T>(T toSerialize)
        {
            DataSet dataSet = new DataSet();
            XmlDocument returnVal = new XmlDocument();
            StringBuilder stringBuilder = new StringBuilder();
            System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            TextWriter textWriter = new StringWriter(stringBuilder);
            xmlSerializer.Serialize(textWriter, toSerialize);
            StringReader stringReader = new StringReader(textWriter.ToString());
            dataSet.ReadXml(stringReader);
            returnVal.LoadXml(dataSet.GetXml());
            return returnVal;
        }

        #endregion
    }
}
