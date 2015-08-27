using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Soap;

namespace Es.Serializer
{
    public class SoapSerializer : IBinarySerializer
    {
        private readonly IFormatter binaryFormatter = new SoapFormatter();

        public void Serialize<T>(T value, Stream output) {
            binaryFormatter.Serialize(output, value);
        }

        public T Deserialize<T>(Stream stream) {
            return (T)Deserialize(stream, typeof(T));
        }

        public object Deserialize(Stream stream, Type type) {
            return binaryFormatter.Deserialize(stream);
        }
    }
}