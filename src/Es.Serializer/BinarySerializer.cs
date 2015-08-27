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
 * Class			: BinarySerializer.cs
 *
 * ---------------------------------------------------------------------------
 * */

using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

/// <summary>
/// The Serializer namespace.
/// </summary>
namespace Es.Serializer
{
    /// <summary>
    /// Class BinarySerializer.
    /// </summary>
    public class BinarySerializer : IBinarySerializer
    {
        /// <summary>
        /// The binary formatter
        /// </summary>
        private readonly IFormatter binaryFormatter = new BinaryFormatter();

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">The value.</param>
        /// <param name="output">The output.</param>
        public void Serialize<T>(T value, Stream output) {
            binaryFormatter.Serialize(output, value);
        }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="stream">The stream.</param>
        /// <returns>T.</returns>
        public T Deserialize<T>(Stream stream) {
            return (T)Deserialize(stream, typeof(T));
        }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        public object Deserialize(Stream stream, Type type) {
            return binaryFormatter.Deserialize(stream);
        }
    }
}