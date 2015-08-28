using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Es.Serializer
{
    public class ProtoBufSerializer : IBinarySerializer
    {
        private readonly MethodInfo _method;

        public ProtoBufSerializer() {
            _method = GetType().GetMethods()
                .FirstOrDefault(w => w.Name == "Deserialize" && w.IsGenericMethod);
        }

        public void Serialize<T>(T value, Stream output) {
            ProtoBuf.Serializer.Serialize(output, value);
        }

        public T Deserialize<T>(Stream stream) {
            return ProtoBuf.Serializer.Deserialize<T>(stream);
        }

        public object Deserialize(Stream stream, Type type) {
            var genericType = _method.MakeGenericMethod(type);

            return genericType.Invoke(this, new[] { stream }); ;
        }
    }
}