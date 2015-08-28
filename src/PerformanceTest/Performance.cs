using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            const int runs = 10000;

            Console.WriteLine("\tJil\t\tJsonNet\t\tProtobuf\tXml\t\tBinary\t\tSoap\t\tDataContract");

            var foo = Helper.GetFoo();

            double[] speeds = CompareSerializers(foo, runs);

            Console.WriteLine("single\t" + string.Join("\t\t", Enumerable.Range(0, serializer.Count).Select(s => "{" + s + "}")),
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

            serializer["Xml"] = () =>
              {
                  GC.Collect(2, GCCollectionMode.Forced, blocking: true);
                  ret[keys.IndexOf("Xml")] = Helper.AverageRuntime(() =>
                  {
                      xmlserializer.SerializeToString(obj);
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

            //var allRuns = new List<double[]>();

            //foreach (var perm in Helper.Permutate(ret.Length)) {
            //    ret = new double[ret.Length];
            //    foreach (var index in perm) {
            //        serializer[keys[index]]();
            //    }
            //    allRuns.Add(ret);
            //}

            //var medians = new double[ret.Length];

            //for (var i = 0; i < ret.Length; i++) {
            //    var allForIndex = allRuns.Select(run => run[i])
            //        .OrderBy(_ => _).ToArray();

            //    if (allForIndex.Length % 2 == 1) {
            //        medians[i] = allForIndex[allForIndex.Length / 2];
            //    }
            //    else {
            //        medians[i] = (allForIndex[allForIndex.Length / 2 - 1] + allForIndex[allForIndex.Length / 2]) / 2.0;
            //    }
            //}

            //return medians;
        }
    }
}
