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
using System.Xml.Serialization;

/// <summary>
/// The Serializer namespace.
/// </summary>
namespace Es.Serializer
{
    /// <summary>
    /// Class XmlSerializer.
    /// </summary>
    public class XmlSerializer : ObjectSerializerBase
    {
#if DEBUG

        /// <summary>
        /// The xml format
        /// </summary>
        private readonly Formatting _format = Formatting.Indented;

#else
        private readonly Formatting _format = Formatting.None;
#endif

        /// <summary>
        /// Serializes the specified value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="output">The output.</param>
        public override void Serialize(object value, Stream output) {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(value.GetType());
            serializer.Serialize(output, value);
        }

        /// <summary>
        /// Deserializes the specified type.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="stream">The stream.</param>
        /// <returns>System.Object.</returns>
        public override object Deserialize(Type type, Stream stream) {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
            return serializer.Deserialize(stream);
        }

        /// <summary>
        /// Serializes the core.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="writer">The writer.</param>
        protected override void SerializeCore(object value, TextWriter writer) {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(value.GetType());
            XmlTextWriter xtw = new XmlTextWriter(writer);
            xtw.Formatting = _format;
            serializer.Serialize(xtw, value);
        }

        /// <summary>
        /// Deserializes the core.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="reader">The reader.</param>
        /// <returns>System.Object.</returns>
        protected override object DeserializeCore(Type type, TextReader reader) {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(type);
            return serializer.Deserialize(reader);
        }
    }
}