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

        /// <summary>
        /// Serializes to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public override string SerializeToString(object value) {
            using (MemoryStream mem = new MemoryStream()) {
                Serialize(value, mem);
                return BitConverter.ToString(mem.ToArray()).Replace("-", string.Empty);
            }
        }

        /// <summary>
        /// Deserializes from string.
        /// </summary>
        /// <param name="serializedText">The serialized text.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        public override object DeserializeFromString(string serializedText, Type type) {
            var data = FromHex(serializedText);
            using (MemoryStream mem = new MemoryStream(data)) {
                return Deserialize(mem, type);
            }
        }

        /// <summary>
        /// Serializes the core.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="writer">The writer.</param>
        protected override void SerializeCore(object value, TextWriter writer) {
            writer.Write(SerializeToString(value));
        }

        /// <summary>
        /// Deserializes the core.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        protected override object DeserializeCore(TextReader reader, Type type) {
            var hex = reader.ReadToEnd();
            return DeserializeFromString(hex, type);
        }

        public static byte[] FromHex(string hex) {
            if (string.IsNullOrEmpty(hex)) return new byte[0];
            if ((hex.Length % 2) != 0)
                hex += " ";
            byte[] returnBytes = new byte[hex.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
            return returnBytes;
        }
    }
}