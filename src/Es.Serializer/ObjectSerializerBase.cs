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
    public abstract class ObjectSerializerBase : IObjectSerializer, IStringSerializer
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
        /// <param name="type">The type.</param>
        /// <param name="reader">The reader.</param>
        /// <returns>System.Object.</returns>
        protected abstract object DeserializeCore(Type type, TextReader reader);

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="reader">The reader.</param>
        /// <returns>System.Object.</returns>
        public object Deserialize(Type type, TextReader reader) {
            return DeserializeCore(type, reader);
        }

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>System.Object.</returns>
        public virtual object Deserialize(Type type, Stream stream) {
            using (StreamReader reader = new StreamReader(stream, _encoding, true, bufferSize, true))
                return Deserialize(type, reader);
        }

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="data">The data.</param>
        /// <returns>System.Object.</returns>
        public virtual object Deserialize(Type type, byte[] data) {
            using (var mem = new MemoryStream(data)) {
                return Deserialize(type, mem);
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
            return (To)DeserializeFromString(typeof(To), serializedText);
        }

        /// <summary>
        /// Deserializes from string.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="serializedText">The serialized text.</param>
        /// <returns>System.Object.</returns>
        public virtual object DeserializeFromString(Type type, string serializedText) {
            using (StringReader reader = new StringReader(serializedText)) {
                return DeserializeCore(type, reader);
            }
        }

        /// <summary>
        /// Serializes to string.
        /// </summary>
        /// <typeparam name="TFrom">The type of the TFrom.</typeparam>
        /// <param name="from">From.</param>
        /// <returns>System.String.</returns>
        public virtual string SerializeToString<TFrom>(TFrom from) {
            using (StringWriter writer = new StringWriter()) {
                SerializeCore(from, writer);
                return writer.ToString();
            }
        }
    }
}