﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Es.Serializer
{
    public static class SerializerExtensions
    {
        public static T Deserialize<T>(this ObjectSerializerBase self, Stream stream) {
            return (T)self.Deserialize(stream, typeof(T));
        }

        public static To DeserializeFromString<To>(this ObjectSerializerBase self, string serializedText) {
            return (To)self.DeserializeFromString(serializedText, typeof(To));
        }

        public static T DeepClone<T>(this ObjectSerializerBase self, T obj) {
            using (MemoryStream ms = new MemoryStream()) {
                self.Serialize(obj, ms);
                ms.Seek(0, SeekOrigin.Begin);
                return (T)self.Deserialize(ms, obj.GetType());
            }
        }


        public static T DeepClone<T>(this T obj) {
            var serializer = SerializerFactory.Get("binary");
            return (T)serializer.DeepClone(obj);
        }

    }
}
