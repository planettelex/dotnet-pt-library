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
﻿namespace PlanetTelex.Serialization
{
    /// <summary>
    /// Defines a contract for a serializer.
    /// </summary>
    public interface ISerializer
    {
        /// <summary>
        /// Serializes an object to a string.
        /// </summary>
        /// <typeparam name="T">The type of object being serialized.</typeparam>
        /// <param name="instance">The object to serialize.</param>
        /// <returns>A string.</returns>
        string Serialize<T>(T instance);

        /// <summary>
        /// Deserializes a string into an object.
        /// </summary>
        /// <typeparam name="T">The type of object being deserialized.</typeparam>
        /// <param name="serializedInstance">A string representation of the object type specified.</param>
        /// <returns>A new object of the type specified.</returns>
        T Deserialize<T>(string serializedInstance);

        /// <summary>
        /// Determines if the instance is serializable.
        /// </summary>
        /// <typeparam name="T">The type of object being checked.</typeparam>
        /// <param name="instance">Object instance to be checked.</param>
        /// <returns><c>true</c> if this instance is serializable; otherwise, <c>false</c>.</returns>
        bool IsSerializable<T>(T instance) where T : class, new();

        /// <summary>
        /// Serializes and deserializes an object instance to test that the class is correctly configured for serialization.
        /// </summary>
        /// <typeparam name="T">The type of object being serialized.</typeparam>
        /// <param name="instance">Object instance to be checked.</param>
        /// <returns>A new object instance.</returns>
        T RoundtripSerialize<T>(T instance) where T : class, new();
    }
}
