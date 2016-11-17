using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Es.Serializer;

namespace PerformanceTest
{
    public class Program
    {
        public static void Main(string[] args) {
#if NETFULL
            SerializerFactory.AddSerializer<JilSerializer>("jil");
            NetSerializer.Serializer instance = new NetSerializer.Serializer(new[] { typeof(Foo), typeof(SerializerWrapper) });
            NETSerializer ns = new NETSerializer(instance);
            SerializerFactory.AddSerializer(new NETSerializer(instance), "NET");
#endif

            SerializerFactory.AddSerializer<JsonNetSerializer>("jsonNet");
            SerializerFactory.AddSerializer<ProtoBufSerializer>("ProtoBuf");



            TestExcute.Excute(typeof(Program));
        }
    }
}
