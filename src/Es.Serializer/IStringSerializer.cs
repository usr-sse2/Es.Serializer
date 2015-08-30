// ==++==
//
//  Copyright (c) . All rights reserved.
//
// ==--==
/* ---------------------------------------------------------------------------
 *
 * Author			: v.la
 * Email			: v.la@live.cn
 * Created			: 2015-08-27
 * Class			: IStringSerializer.cs
 *
 * ---------------------------------------------------------------------------
 * */

using System;

/// <summary>
/// The Serializer namespace.
/// </summary>
namespace Es.Serializer
{
    /// <summary>
    /// Interface IStringSerializer
    /// </summary>
    public interface IStringSerializer
    {
        /// <summary>
        /// Deserializes from string.
        /// </summary>
        /// <param name="serializedText">The serialized text.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        object DeserializeFromString(string serializedText, Type type);

        /// <summary>
        /// Serializes to string.
        /// </summary>
        /// <param name="value">the value.</param>
        /// <returns>System.String.</returns>
        string SerializeToString(object value);
    }
}