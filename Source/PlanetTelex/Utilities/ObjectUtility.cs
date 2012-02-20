using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using PlanetTelex.Properties;
using PlanetTelex.Serialization;

namespace PlanetTelex.Utilities
{
    /// <summary>
    /// Utility methods that assist in object manipulation.
    /// </summary>
    public class ObjectUtility
    {
        // Keep instance dictionary to avoid excessive reflection method calls.
        private readonly Dictionary<string, Collection<PropertyInfo>> _propertyLookup = new Dictionary<string, Collection<PropertyInfo>>();
        private readonly BinarySerializer _binarySerializer = new BinarySerializer();

        #region Clone Method

        /// <summary>
        /// Performs a "deep copy" of a given object, resulting in a new copy of the object in memory.
        /// </summary>
        /// <param name="toClone">The object to clone.</param>
        /// <returns>A new object with the same type and property values as the provided object.</returns>
        public virtual object Clone<T>(T toClone) where T : class, new()
        {
            return _binarySerializer.RoundtripSerialize(toClone);
        }

        #endregion

        #region Conversion Methods

        /// <summary>
        /// Converts an object instance into a dictionary.
        /// </summary>
        /// <remarks>This method is useful when starting with an anonymous type.</remarks>
        /// <param name="toConvert">The object to convert.</param>
        /// <returns>A dictionary representation of the provided object.</returns>
        public virtual Dictionary<object, object> ToDictionary(object toConvert)
        {
            Dictionary<object, object> returnVal = new Dictionary<object, object>();
            if (toConvert != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(toConvert);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(toConvert);
                    if (val != null)
                        returnVal.Add(prop.Name, val);
                }
            }
            return returnVal;
        }

        /// <summary>
        /// Converts an object instance into a dictionary.
        /// </summary>
        /// <remarks>This method is useful when starting with an anonymous type.</remarks>
        /// <typeparam name="TVal">The type of the value in the dictionary.</typeparam>
        /// <param name="toConvert">The object to convert.</param>
        /// <returns>A dictionary representation of the provided object.</returns>
        public virtual Dictionary<string, TVal> ToDictionary<TVal>(object toConvert)
        {
            Dictionary<string, TVal> returnVal = new Dictionary<string, TVal>();
            if (toConvert != null)
            {
                PropertyDescriptorCollection props = TypeDescriptor.GetProperties(toConvert);
                foreach (PropertyDescriptor prop in props)
                {
                    object val = prop.GetValue(toConvert);
                    if (val != null)
                        returnVal.Add(prop.Name, (TVal)val);
                }
            }
            return returnVal;
        }

        #endregion

        #region Get Property Methods

        /// <summary>
        /// Gets all the properties of a specified type.
        /// </summary>
        /// <param name="toGetPropertiesFor">The class to get properties for.</param>
        /// <returns>A collection of <see cref="PropertyInfo"/>.</returns>
        public virtual Collection<PropertyInfo> GetProperties(Type toGetPropertiesFor)
        {
            if (toGetPropertiesFor == null || toGetPropertiesFor.FullName == null) return null;

            if (_propertyLookup.ContainsKey(toGetPropertiesFor.FullName))
                return _propertyLookup[toGetPropertiesFor.FullName];
            
            Collection<PropertyInfo> properties = new Collection<PropertyInfo>();
            foreach (PropertyInfo property in toGetPropertiesFor.GetProperties())
                properties.Add(property);

            if (toGetPropertiesFor.FullName == null) return null;

            if (!_propertyLookup.ContainsKey(toGetPropertiesFor.FullName))
                _propertyLookup.Add(toGetPropertiesFor.FullName, properties);
                
            return properties;
        }

        /// <summary>
        /// Gets the <see cref="PropertyInfo"/> of the given property in a specified type.
        /// </summary>
        /// <param name="toGetPropertyFor">The class containing the property.</param>
        /// <param name="propertyName">The property name to get the <see cref="PropertyInfo"/> for.</param>
        /// <returns>A <see cref="PropertyInfo"/>, or null if the property is not found.</returns>
        public virtual PropertyInfo GetProperty(Type toGetPropertyFor, string propertyName)
        {
            if (toGetPropertyFor == null) return null;

            PropertyInfo returnVal = null;
            Collection<PropertyInfo> allProperties = GetProperties(toGetPropertyFor);

            if(allProperties != null)
                foreach (PropertyInfo property in allProperties.Where(property => property.Name == propertyName))
                    returnVal = property;

            return returnVal;   
        }

        #endregion

        #region Testing Methods

        /// <summary>
        /// Determines if the given object has the specified property.
        /// </summary>
        /// <param name="property">The property to check for.</param>
        /// <param name="toCheck">The object to check.</param>
        /// <returns><c>true</c> if the specified property contains property; otherwise, <c>false</c>.</returns>
        public virtual bool ContainsProperty(string property, object toCheck)
        {
            bool returnVal = false;
            foreach (PropertyInfo propertyInfo in GetProperties(toCheck.GetType()))
            {
                if (String.Compare(propertyInfo.Name, property, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    returnVal = true;
                    break;
                }
            }
            return returnVal;
        }

        /// <summary>
        /// Determines if a given object type can be compared.
        /// </summary>
        /// <param name="toTest">The object to test.</param>
        /// <returns><c>true</c> if the specified object is comparable; otherwise, <c>false</c>.</returns>
        public virtual bool IsComparable(Type toTest)
        {
            if (toTest == null)
                return false;

            Type[] objectInterfaces = toTest.GetInterfaces();
            bool hasIComparable = false;

            foreach (Type objectInterface in objectInterfaces)
            {
                if (String.Compare(objectInterface.Name, "icomparable", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    hasIComparable = true;
                    break;
                }
            }
            return (toTest.IsPrimitive || hasIComparable);
        }

        #endregion

        #region Mapping Methods

        /// <summary>
        /// Maps the properties of a given 'from' object into a given 'to' object. Properties are matched up by name. Classes do not have to be of the same type.
        /// </summary>
        /// <param name="fromObject">The object to map from.</param>
        /// <param name="fromType">The 'from' object's type.</param>
        /// <param name="toObject">The object to map to.</param>
        /// <param name="toType">The 'to' object's type.</param>
        /// <returns>The 'to' object with values from the 'from' object applied.</returns>
        public virtual object MapObject(object fromObject, Type fromType, object toObject, Type toType)
        {
            if (toObject == null)
                throw new ArgumentNullException("toObject", Resources.MapObjectArgumentNullException1);
            if (fromType == null)
                throw new ArgumentNullException("fromType", Resources.MapObjectArgumentNullException2);
            if (toType == null)
                throw new ArgumentNullException("fromType", Resources.MapObjectArgumentNullException2);

            foreach (PropertyInfo property in GetProperties(fromType))
            {
                try
                {
                    if (fromType.GetProperty(property.Name) != null && (toType.GetProperty(property.Name)).CanWrite)
                        (toType.GetProperty(property.Name)).SetValue(toObject, (fromType.GetProperty(property.Name)).GetValue(fromObject, null), null);
                }
                // ReSharper disable EmptyGeneralCatchClause
                catch { } // Do nothing. An exception in this mapping simply causes the field to not be mapped.
                // ReSharper restore EmptyGeneralCatchClause
            }
            return toObject;
        }

        /// <summary>
        /// Maps the properties of a given 'from' object into a given 'to' object. Properties are matched up by name. Classes do not have to be of the same type.
        /// </summary>
        /// <param name="fromObject">The object to map from.</param>
        /// <param name="toObject">The object to map to.</param>
        /// <returns>The 'to' object with values from the 'from' object applied.</returns>
        public virtual object MapObject(object fromObject, object toObject)
        {
            if (fromObject == null) return null;

            if(toObject == null)
                throw new ArgumentNullException("toObject", Resources.MapObjectArgumentNullException1);

            return MapObject(fromObject, fromObject.GetType(), toObject, toObject.GetType());
        }

        #endregion
    }
}