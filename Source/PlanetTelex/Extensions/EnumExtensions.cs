using System;
using PlanetTelex.Common;
using PlanetTelex.Utilities;

namespace PlanetTelex.Extensions
{
    /// <summary>
    /// Extension methods to various enum types.
    /// </summary>
    public static class EnumExtensions
    {
        private static readonly EnumUtility EnumUtility = new EnumUtility();

        #region Type Methods

        /// <summary>
        /// Determines whether this type is a nullable enum.
        /// </summary>
        /// <param name="t">The type.</param>
        /// <returns>
        ///   <c>true</c> if the type is a nullable enum; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullableEnum(this Type t)
        {
            Type u = Nullable.GetUnderlyingType(t);
            return (u != null) && u.IsEnum;
        }

        /// <summary>
        /// Determines whether this type is an enum.
        /// </summary>
        /// <param name="t">The type.</param>
        /// <returns>
        ///   <c>true</c> if the type is an enum; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEnum(this Type t)
        {
            return t.IsEnum;
        }

        #endregion

        #region Specific Enum Methods

        /// <summary>
        /// Gets the MIME type string of this file extension.
        /// </summary>
        /// <param name="fileExtension">A file extension.</param>
        /// <returns>A MIME type string.</returns>
        public static string MimeType(this FileExtension fileExtension)
        {
            return EnumUtility.GetMimeType(fileExtension);
        }

        #endregion
    }
}
