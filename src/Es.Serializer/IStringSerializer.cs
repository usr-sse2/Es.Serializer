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
        /// <typeparam name="To">The type of to.</typeparam>
        /// <param name="serializedText">The serialized text.</param>
        /// <returns>To.</returns>
        To DeserializeFromString<To>(string serializedText);

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
        /// <typeparam name="TFrom">The type of the TFrom.</typeparam>
        /// <param name="from">From.</param>
        /// <returns>System.String.</returns>
        string SerializeToString<TFrom>(TFrom from);
    }
}