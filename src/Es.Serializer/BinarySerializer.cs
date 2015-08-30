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
    public class BinarySerializer : ObjectSerializerBase
    {
        /// <summary>
        /// The binary formatter
        /// </summary>
        private readonly IFormatter binaryFormatter = new BinaryFormatter();

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="output">The output.</param>
        public override void Serialize(object value, Stream output) {
            binaryFormatter.Serialize(output, value);
        }

        /// <summary>
        /// Deserializes the specified stream.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        public override object Deserialize(Stream stream, Type type) {
            return binaryFormatter.Deserialize(stream);
        }

        protected override void SerializeCore(object value, TextWriter writer) {
            throw new NotImplementedException();
        }

        protected override object DeserializeCore(TextReader reader, Type type) {
            throw new NotImplementedException();
        }
    }
}