using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Es.Serializer;

namespace PerformanceTest
{
    public class Performance
    {

        Dictionary<string, Action> serializer = new Dictionary<string, Action>(StringComparer.OrdinalIgnoreCase)
        {
            {"Jil",()=> { } },
            {"Json",()=> { }},
            {"Protobuf",()=> { }},
            {"Xml",()=> { }},
            {"Binary",()=> { }},
            {"Soap",()=> { }},
            {"DataContract",()=> { }}
        };

        JilSerializer jilserializer = new JilSerializer();
        JsonNetSerializer jsonnetserializer = new JsonNetSerializer();
        ProtoBufSerializer protobufserializer = new ProtoBufSerializer();
        XmlSerializer xmlserializer = new XmlSerializer();
        BinarySerializer binaryserializer = new BinarySerializer();
        SoapSerializer soapserializer = new SoapSerializer();
        DataContractSerializer datacontractserializer = new DataContractSerializer();


        [Test("Serializers Performance Test")]
        public void SerializersTest() {

            Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)1;

            const int runs = 10000;

            Console.WriteLine("\tJil\t\tJsonNet\t\tProtobuf\tXml\t\tBinary\t\tSoap\t\tDataContract");

            var foo = Helper.GetFoo();

            double[] speeds = CompareSerializers(foo, runs);

            Console.WriteLine("single\t" + string.Join("\t\t", Enumerable.Range(0, serializer.Count).Select(s => "{" + s + "}")),
                speeds.Select(s => s.ToString()).ToArray());

            var wrapper = new SerializerWrapper(foo);

            speeds = CompareSerializers(wrapper, runs);

            Console.WriteLine("wrapper\t" + string.Join("\t\t", Enumerable.Range(0, serializer.Count).Select(s => "{" + s + "}")),
                speeds.Select(s => s.ToString()).ToArray());
        }


        [Test("Deserialize Performance Test")]
        public void CompareDeserializes() {

            Process.GetCurrentProcess().ProcessorAffinity = (IntPtr)1;

            const int runs = 10000;

            Console.WriteLine("\tJil\t\tJsonNet\t\tProtobuf\tXml\t\tBinary\t\tSoap\t\tDataContract");

            var foo = Helper.GetFoo();

            double[] speeds = CompareDeserializes(foo, runs);

            Console.WriteLine("single\t" + string.Join("\t\t", Enumerable.Range(0, serializer.Count).Select(s => "{" + s + "}")),
                speeds.Select(s => s.ToString()).ToArray());

            var wrapper = new SerializerWrapper(foo);

            speeds = CompareDeserializes(wrapper, runs);

            Console.WriteLine("wrapper\t" + string.Join("\t\t", Enumerable.Range(0, serializer.Count).Select(s => "{" + s + "}")),
                speeds.Select(s => s.ToString()).ToArray());

        }

        double[] CompareSerializers<T>(T obj, int runs) {
            double[] ret = new double[serializer.Count];


            //warm-up
            jilserializer.SerializeToString(obj);
            jsonnetserializer.SerializeToString(obj);
            using (MemoryStream mem = new MemoryStream()) {
                protobufserializer.Serialize(obj, mem);
            }
            xmlserializer.SerializeToString(obj);
            using (MemoryStream mem = new MemoryStream()) {
                binaryserializer.Serialize(obj, mem);
            }
            using (MemoryStream mem = new MemoryStream()) {
                datacontractserializer.Serialize(obj, mem);
            }
            using (MemoryStream mem = new MemoryStream()) {
                soapserializer.Serialize(obj, mem);
            }

            var keys = serializer.Keys.ToList();

            serializer["Jil"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Jil")] = Helper.AverageRuntime(() =>
                {
                    jilserializer.SerializeToString(obj);
                }, runs);
            };

            serializer["Json"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Json")] = Helper.AverageRuntime(() =>
                {
                    jsonnetserializer.SerializeToString(obj);
                }, runs);
            };

            serializer["Xml"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Xml")] = Helper.AverageRuntime(() =>
                {
                    xmlserializer.SerializeToString(obj);
                }, runs);
            };

            serializer["Protobuf"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Protobuf")] = Helper.AverageRuntime(() =>
                {
                    using (MemoryStream mem = new MemoryStream()) {
                        protobufserializer.Serialize(obj, mem);
                    }
                }, runs);
            };

            serializer["Binary"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Binary")] = Helper.AverageRuntime(() =>
                {
                    using (MemoryStream mem = new MemoryStream()) {
                        binaryserializer.Serialize(obj, mem);
                    }
                }, runs);
            };

            serializer["Soap"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Soap")] = Helper.AverageRuntime(() =>
                {
                    using (MemoryStream mem = new MemoryStream()) {
                        soapserializer.Serialize(obj, mem);
                    }
                }, runs);
            };

            serializer["DataContract"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("DataContract")] = Helper.AverageRuntime(() =>
                {
                    using (MemoryStream mem = new MemoryStream()) {
                        datacontractserializer.Serialize(obj, mem);
                    }
                }, runs);
            };

            keys.ForEach(k =>
            {
                serializer[k]();
            });

            return ret;
        }

        double[] CompareDeserializes<T>(T obj, int runs) {
            double[] ret = new double[serializer.Count];

            var objType = obj.GetType();

            //warm-up
            var jilSerializedText = jilserializer.SerializeToString(obj);
            var jsonnetSerializedText = jsonnetserializer.SerializeToString(obj);
            var xmlSerializedText = xmlserializer.SerializeToString(obj);
            byte[] protobufData, binaryData, dataContractData, soapData;
            using (MemoryStream mem = new MemoryStream()) {
                protobufserializer.Serialize(obj, mem);
                protobufData = mem.ToArray();
            }
            using (MemoryStream mem = new MemoryStream()) {
                binaryserializer.Serialize(obj, mem);
                binaryData = mem.ToArray();
            }
            using (MemoryStream mem = new MemoryStream()) {
                datacontractserializer.Serialize(obj, mem);
                dataContractData = mem.ToArray();
            }
            using (MemoryStream mem = new MemoryStream()) {
                soapserializer.Serialize(obj, mem);
                soapData = mem.ToArray();
            }

            var keys = serializer.Keys.ToList();

            serializer["Jil"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Jil")] = Helper.AverageRuntime(() =>
                {
                    jilserializer.DeserializeFromString(jilSerializedText, objType);
                }, runs);
            };

            serializer["Json"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Json")] = Helper.AverageRuntime(() =>
                {
                    jsonnetserializer.DeserializeFromString(jsonnetSerializedText, objType);
                }, runs);
            };

            serializer["Xml"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Xml")] = Helper.AverageRuntime(() =>
                {
                    xmlserializer.DeserializeFromString(xmlSerializedText, objType);
                }, runs);
            };

            serializer["Protobuf"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Protobuf")] = Helper.AverageRuntime(() =>
                {
                    using (MemoryStream mem = new MemoryStream(protobufData)) {
                        protobufserializer.Deserialize(mem, objType);
                    }
                }, runs);
            };

            serializer["Binary"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Binary")] = Helper.AverageRuntime(() =>
                {
                    using (MemoryStream mem = new MemoryStream(binaryData)) {
                        binaryserializer.Deserialize(mem, objType);
                    }
                }, runs);
            };

            serializer["Soap"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("Soap")] = Helper.AverageRuntime(() =>
                {
                    using (MemoryStream mem = new MemoryStream(soapData)) {
                        soapserializer.Deserialize(mem, objType);
                    }
                }, runs);
            };

            serializer["DataContract"] = () =>
            {
                GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                ret[keys.IndexOf("DataContract")] = Helper.AverageRuntime(() =>
                {
                    using (MemoryStream mem = new MemoryStream(dataContractData)) {
                        datacontractserializer.Deserialize(mem, objType);
                    }
                }, runs);
            };

            keys.ForEach(k =>
            {
                serializer[k]();
            });

            return ret;
        }
    }
}
