﻿using System;
using System.IO;
using ProtoBuf.Meta;

namespace Es.Serializer
{
    public class ProtoBufSerializer : ObjectSerializerBase
    {
        /// <summary>
        /// ProtoBufSerializer Instance
        /// </summary>
        public static ProtoBufSerializer Instance = new ProtoBufSerializer();

        public override void Serialize(object value, Stream output) {
            RuntimeTypeModel.Default.Serialize(output, value);
        }

        public override object Deserialize(Stream stream, Type type) {
            return RuntimeTypeModel.Default.Deserialize(stream, null, type);
        }

        /// <summary>
        /// Serializes to string.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>System.String.</returns>
        public override string SerializeToString(object value) {
            using (MemoryStream mem = new MemoryStream()) {
                Serialize(value, mem);
                return ToHex(mem.ToArray());
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

    }
}