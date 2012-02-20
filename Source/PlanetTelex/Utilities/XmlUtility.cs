using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Xsl;
using PlanetTelex.Common;

namespace PlanetTelex.Utilities
{
    /// <summary>
    /// Utility methods that assists with xml and xml document manipulation.
    /// </summary>
    public class XmlUtility
    {
        #region Load Method

        /// <summary>
        /// Loads an XML file into a document, removing the XML version/encoding node and the xmlns information.
        /// </summary>
        /// <param name="xmlFilePath">The path and filename to the XML file on the server.</param>
        /// <returns>An <see cref="XmlDocument"/>.</returns>
        public virtual XmlDocument LoadXmlFile(string xmlFilePath)
        {
            StreamReader readFile = new StreamReader(xmlFilePath);
            string xmlString = readFile.ReadToEnd();
            Regex removeXmlDoc = new Regex(RegExPattern.XML_DOCTYPE_NODE);
            xmlString = removeXmlDoc.Replace(xmlString, string.Empty);
            Regex removeXmlns = new Regex(RegExPattern.XML_NAMESPACE_ATTRIBUTE);
            xmlString = removeXmlns.Replace(xmlString, string.Empty);
            XmlDocument sitemapXml = new XmlDocument();
            sitemapXml.LoadXml(xmlString);
            readFile.Close();
            return sitemapXml;
        }

        #endregion

        #region XSLT Methods

        /// <summary>
        /// Performs and XSLT transformation on a specified XmlDocument, and returns the result as a string, removing the XML DocType node from the resulting string.
        /// </summary>
        /// <param name="sourceXml">Source Xml Document</param>
        /// <param name="xsltResolver">XmlUrlResolver containing correct XSLT information</param>
        /// <param name="styleSheetUri">URI to a stylesheet for the transformation.</param>
        /// <returns>A string with the transform applied.</returns>
        /// <remarks>
        /// WARNING! Use of this method may result in issues with Medium Trust hosting environments.
        /// </remarks>
        public virtual string PerformXslt(XmlDocument sourceXml, XmlUrlResolver xsltResolver, string styleSheetUri)
        {
            return PerformXslt(sourceXml, xsltResolver, styleSheetUri, true);
        }

        /// <summary>
        /// Performs and XSLT transformation on a specified XmlDocument, and returns the result as a string.
        /// </summary>
        /// <param name="sourceXml">Source Xml Document</param>
        /// <param name="xsltResolver">XmlUrlResolver containing correct XSLT information</param>
        /// <param name="styleSheetUri">URI to a stylesheet for the transformation.</param>
        /// <param name="removeDoctypeNode">if set to <c>true</c> [remove doctype node].</param>
        /// <returns>A string with the transform applied.</returns>
        /// <remarks>
        /// WARNING! Use of this method may result in issues with Medium Trust hosting environments.
        /// </remarks>
        public virtual string PerformXslt(XmlDocument sourceXml, XmlUrlResolver xsltResolver, string styleSheetUri, bool removeDoctypeNode)
        {
            StringBuilder sBuild = new StringBuilder();
            TextReader tReader = new StringReader(sourceXml.InnerXml);
            using (XmlReader xRead = XmlReader.Create(tReader))
            {
                XslCompiledTransform xslTransform = new XslCompiledTransform();
                xslTransform.Load(styleSheetUri, XsltSettings.TrustedXslt, xsltResolver);

                TextWriter sWriter = new StringWriter(sBuild);
                xslTransform.Transform(xRead, null, sWriter);
            }
            // Remove XML doc type before returning
            if (removeDoctypeNode)
            {
                Regex removeXmlDoc = new Regex(RegExPattern.XML_DOCTYPE_NODE);
                return removeXmlDoc.Replace(sBuild.ToString(), string.Empty);
            }
            return sBuild.ToString();
        }

        #endregion

        #region Manipulation Methods

        /// <summary>
        /// Adds an attribute to a given XML node.
        /// </summary>
        /// <param name="xmlNode">The node to append the attribute to.</param>
        /// <param name="attributeName">The attribute name.</param>
        /// <param name="attributeValue">The attribute value.</param>
        /// <returns>An <see cref="XmlAttribute"/>.</returns>
        public virtual XmlAttribute AddAttributeToNode(XmlNode xmlNode, string attributeName, string attributeValue)
        {
            if (xmlNode.OwnerDocument != null)
            {
                XmlAttribute newAttribute = xmlNode.OwnerDocument.CreateAttribute(attributeName);
                newAttribute.Value = attributeValue;
                return newAttribute;
            }
            return null;
        }

        /// <summary>
        /// Removes the specified attribute from all nodes contained in the given XML node.
        /// </summary>
        /// <param name="xmlNode">The node to remove all specified attributes from.</param>
        /// <param name="rootNode">The root node to start the removal at.</param>
        /// <param name="attributeName">The attribute name.</param>
        public virtual void RemoveAttributeFromNodes(XmlNode xmlNode, string rootNode, string attributeName)
        {
            XmlNodeList nodes = xmlNode.SelectNodes("//" + rootNode);
            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    if (node.Attributes != null && node.Attributes[attributeName] != null)
                    {
                        XmlAttributeCollection attributes = node.Attributes;
                        attributes.Remove(attributes[attributeName]);
                    }
                }
            }
        }

        #endregion
    }
}