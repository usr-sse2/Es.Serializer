using System;
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
    }
}
