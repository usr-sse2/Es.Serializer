using System;
using System.Runtime.Serialization;
using Es.Serializer;
using Newtonsoft.Json;
using ProtoBuf;

namespace PerformanceTest
{
    [Serializable, ProtoContract]
    public class SerializerWrapper
    {
        [NonSerialized, IgnoreDataMember, JsonIgnore, ProtoIgnore]
        private object _value;

        public SerializerWrapper() {
        }

        public SerializerWrapper(object value) {
            ValueType = value.GetType().AssemblyQualifiedName;
            _value = value;
            JsonString = SerializerFactory.Get("jil").SerializeToString(value);
        }

        [ProtoMember(1)]
        public string ValueType { get; set; }

        [ProtoMember(2)]
        public string JsonString { get; set; }

        [IgnoreDataMember, JsonIgnore, ProtoIgnore]
        public object Value {
            get {
                if (_value == null && !string.IsNullOrEmpty(JsonString))
                    _value = SerializerFactory.Get("jil")
                        .DeserializeFromString(JsonString,
                         Type.GetType(ValueType, true, true));
                return _value;
            }
        }
    }
}