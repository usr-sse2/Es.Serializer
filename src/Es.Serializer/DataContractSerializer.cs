using System;
using System.IO;

namespace Es.Serializer
{
    public class DataContractSerializer : IBinarySerializer
    {
        public void Serialize<T>(T value, Stream output) {
            System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(value.GetType());
            serializer.WriteObject(output, value);
        }

        public T Deserialize<T>(Stream stream) {
            System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(typeof(T));
            return (T)serializer.ReadObject(stream);
        }

        public object Deserialize(Stream stream, Type type) {
            System.Runtime.Serialization.DataContractSerializer serializer = new System.Runtime.Serialization.DataContractSerializer(type);
            return serializer.ReadObject(stream);
        }
    }
}