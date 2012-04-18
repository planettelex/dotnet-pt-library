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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Xml;
using PlanetTelex.Properties;

namespace PlanetTelex.Utilities
{
    /// <summary>
    /// Utility methods for file and assembly management.
    /// </summary>
    public class FileUtility
    {
        #region Assembly Methods

        /// <summary>
        /// Determines whether the provided assembly was compiled in 'Debug' mode.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <returns>
        ///   <c>true</c> if this is a debug assembly; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsDebugAssembly(string assemblyName)
        {
            Assembly assembly = Assembly.Load(assemblyName);
            return assembly.GetCustomAttributes(false).OfType<DebuggableAttribute>().Select(attr => (attr).IsJITTrackingEnabled).FirstOrDefault();
        }

        #endregion

        #region Embedded Resource Methods

        /// <summary>
        /// Gets an embedded file out of a given assembly.
        /// </summary>
        /// <param name="assemblyName">The namespace of you assembly.</param>
        /// <param name="fileName">The name of the file to extract.</param>
        /// <returns>A <see cref="Stream"/>.</returns>
        public virtual Stream GetEmbeddedFile(string assemblyName, string fileName)
        {
            try
            {
                Assembly assembly = Assembly.Load(assemblyName);
                Stream stream = assembly.GetManifestResourceStream(assemblyName + "." + fileName);

                if (stream == null)
                    throw new FileNotFoundException(string.Format(Resources.GetEmbeddedFileFileNotFoundException, fileName, assemblyName));
                    
                return stream;
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(assemblyName + ": " + e.Message, e);
            }
        }

        /// <summary>
        /// Gets the embedded resource out of a given assembly.
        /// </summary>
        /// <param name="assemblyName">Name of the assembly.</param>
        /// <param name="defaultNamespace">Default Namespace associated with the assembly</param>
        /// <param name="resourceFileName">Name of the resource file.</param>
        /// <param name="resourceKey">The resource key.</param>
        /// <returns>A string.</returns>
        public virtual string GetEmbeddedResource(string assemblyName, string defaultNamespace, string resourceFileName, string resourceKey)
        {
            try
            {
                Assembly assembly = Assembly.Load(assemblyName);
                ResourceManager resourceManager = new ResourceManager(defaultNamespace + "." + resourceFileName, assembly);
                return resourceManager.GetString(resourceKey, Thread.CurrentThread.CurrentUICulture);
            }
            catch (Exception e)
            {
                throw new TargetInvocationException(assemblyName + ": " + e.Message, e);
            }
        }

        /// <summary>
        /// Returns all resources in a given .resx file as a List of KeyValuePairs.
        /// </summary>
        /// <param name="filePath">full path to the resource file</param>
        /// <param name="language">in the format "[language code]-[country code]", example: "en-US"</param>
        /// <returns>An IList of string-string KeyValuePairs.</returns>
        public virtual IList<KeyValuePair<string, string>> GetResourceDataFromResourceFile(string filePath, string language)
        {
            List<KeyValuePair<string, string>> resourceList = new List<KeyValuePair<string, string>>();
            string file = GetResourceFileName(filePath, language);
            XmlDocument resourceFile = new XmlDocument();
            
            if(File.Exists(filePath))
                resourceFile.Load(file);
            else
                resourceFile.Load(GetResourceFileName(filePath, string.Empty));

            if (string.IsNullOrEmpty(resourceFile.InnerText))
                return null;
            
            XmlNodeList nodes = resourceFile.SelectNodes("//data");
            if (nodes != null)
            {
                foreach (XmlNode node in nodes)
                {
                    if (node != null && node.Attributes != null && node.Attributes["name"] != null)
                    {
                        XmlNode valueNode = node.SelectSingleNode("value");
                        if ((node.Attributes != null && valueNode != null && !string.IsNullOrEmpty(valueNode.InnerText)))
                        {
                            KeyValuePair<string, string> resource = new KeyValuePair<string, string>(node.Attributes["name"].Value, valueNode.InnerText);
                            resourceList.Add(resource);
                        }
                    }
                }
            }
            else
                return null;

            return resourceList;
        }
        
        /// <summary>
        /// Get appropriate resource file name using filepath and language provided.
        /// </summary>
        /// <param name="filePath">full filepath to the resx file, with or without file extension</param>
        /// <param name="language">CultureInfo.name in the format "[language code]-[country code]", example: "en-US"</param>
        private string GetResourceFileName(string filePath, string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                string newResourceFilePath = filePath + "." + language + ".resx";
                return File.Exists(newResourceFilePath) ? newResourceFilePath : filePath + ".resx";
            }
            return filePath + ".resx";
        }

        #endregion
    }
}