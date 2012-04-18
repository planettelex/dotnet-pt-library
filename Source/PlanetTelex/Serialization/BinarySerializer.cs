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
﻿using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using PlanetTelex.Common;
using PlanetTelex.Utilities;

namespace PlanetTelex.Serialization
{
    /// <summary>
    /// A binary serializer.
    /// </summary>
    public class BinarySerializer : ISerializer
    {
        private readonly StringUtility _stringUtility = new StringUtility();
        
        #region Implementation of ISerializer

        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <typeparam name="T">The type of object being serialized.</typeparam>
        /// <param name="instance">The object to serialize.</param>
        /// <returns>A string.</returns>
        public string Serialize<T>(T instance)
        {
            return _stringUtility.ConvertFromByteArray(Serialize((object)instance), TextEncoding.Utf16);
        }

        /// <summary>
        /// Deserializes a string into an object.
        /// </summary>
        /// <typeparam name="T">The type of object being deserialized.</typeparam>
        /// <param name="serializedInstance">A string representation of the object type specified.</param>
        /// <returns>A new object of the type specified.</returns>
        public T Deserialize<T>(string serializedInstance)
        {
            return (T)Deserialize(_stringUtility.ConvertToByteArray(serializedInstance));
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

            try
            {
                return RoundtripSerialize(instance) != null;
            }
            catch
            {
                return false;
            }
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
        /// Converts an object into a binary serialization.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public byte[] Serialize(object instance)
        {
            if (instance == null) return null;

            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, instance);
            memoryStream.Seek(0, 0);
            memoryStream.Close();

            return memoryStream.ToArray();
        }

        /// <summary>
        /// Converts a binary serialization of an object into an object instance.
        /// </summary>
        /// <param name="binarySerializedObject">A binary serialized object.</param>
        /// <returns>A new object.</returns>
        public object Deserialize(byte[] binarySerializedObject)
        {
            if (binarySerializedObject == null) return null;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            MemoryStream memoryStream = new MemoryStream();
            memoryStream.Write(binarySerializedObject, 0, binarySerializedObject.Length);
            memoryStream.Seek(0, 0);
            object returnVal = binaryFormatter.Deserialize(memoryStream);
            memoryStream.Close();

            return returnVal;
        }

        #endregion
    }
}
