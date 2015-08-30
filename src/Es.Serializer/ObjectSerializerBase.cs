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
 * Class			: ObjectSerializerBase.cs
 *
 * ---------------------------------------------------------------------------
 * */

using System;
using System.IO;
using System.Text;

/// <summary>
/// The Serializer namespace.
/// </summary>
namespace Es.Serializer
{
    /// <summary>
    /// Class ObjectSerializerBase.
    /// </summary>
    public abstract class ObjectSerializerBase : IObjectSerializer, IBinarySerializer, IStringSerializer
    {
        /// <summary>
        /// The buffer size
        /// </summary>
        private const int bufferSize = 1024;

        /// <summary>
        /// The encoding
        /// </summary>
        private readonly Encoding _encoding = Encoding.UTF8;

        /// <summary>
        /// Serializes the core.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="writer">The writer.</param>
        protected abstract void SerializeCore(object value, TextWriter writer);

        /// <summary>
        /// Deserializes the core.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        protected abstract object DeserializeCore(TextReader reader, Type type);

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        public object Deserialize(TextReader reader, Type type) {
            return DeserializeCore(reader, type);
        }

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        public virtual object Deserialize(Stream stream, Type type) {
            using (StreamReader reader = new StreamReader(stream, _encoding, true, bufferSize, true))
                return Deserialize(reader, type);
        }

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="data">The data.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        public virtual object Deserialize(byte[] data, Type type) {
            using (var mem = new MemoryStream(data)) {
                return Deserialize(mem, type);
            }
        }

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="writer">The writer.</param>
        public void Serialize(object value, TextWriter writer) {
            SerializeCore(value, writer);
        }

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="output">The output.</param>
        public virtual void Serialize(object value, Stream output) {
            using (StreamWriter sw = new StreamWriter(output, _encoding, bufferSize, true))
                Serialize(value, sw);
        }

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="output">The output.</param>
        public virtual void Serialize(object value, out byte[] output) {
            using (var mem = new MemoryStream()) {
                Serialize(value, mem);
                output = mem.ToArray();
            }
        }

        /// <summary>
        /// Deserializes from string.
        /// </summary>
        /// <typeparam name="To">The type of to.</typeparam>
        /// <param name="serializedText">The serialized text.</param>
        /// <returns>To.</returns>
        public To DeserializeFromString<To>(string serializedText) {
            return (To)DeserializeFromString(serializedText, typeof(To));
        }

        /// <summary>
        /// Deserializes from string.
        /// </summary>
        /// <param name="serializedText">The serialized text.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        public virtual object DeserializeFromString(string serializedText, Type type) {
            using (StringReader reader = new StringReader(serializedText)) {
                return DeserializeCore(reader, type);
            }
        }

        /// <summary>
        /// Serializes to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public virtual string SerializeToString(object value) {
            using (StringWriter writer = new StringWriter()) {
                SerializeCore(value, writer);
                return writer.ToString();
            }
        }
    }
}