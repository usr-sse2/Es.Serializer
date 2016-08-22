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
 * Class			: XmlSerializer.cs
 *
 * ---------------------------------------------------------------------------
 * */

using System;
using System.IO;
using System.Xml;
namespace Es.Serializer
{
    /// <summary>
    /// Class XmlSerializer.
    /// </summary>
    public class XmlSerializer : ObjectSerializerBase
    {
        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="output">The output.</param>
        public override void Serialize(object value, Stream output) {
            var serializer = new System.Xml.Serialization.XmlSerializer(value.GetType());
            serializer.Serialize(output, value);
        }

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        public override object Deserialize(Stream stream, Type type) {
            var serializer = new System.Xml.Serialization.XmlSerializer(type);
            return serializer.Deserialize(stream);
        }

        /// <summary>
        /// Serializes the core.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="writer">The writer.</param>
        protected override void SerializeCore(object value, TextWriter writer) {
            var serializer = new System.Xml.Serialization.XmlSerializer(value.GetType());
            var xtw = XmlWriter.Create(writer);
            serializer.Serialize(xtw, value);
        }

        /// <summary>
        /// Deserializes the core.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <param name="type">The type.</param>
        /// <returns>System.Object.</returns>
        protected override object DeserializeCore(TextReader reader, Type type) {
            var serializer = new System.Xml.Serialization.XmlSerializer(type);
            return serializer.Deserialize(reader);
        }
    }
}