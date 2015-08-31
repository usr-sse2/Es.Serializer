using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Serializer;
using NUnit.Framework;

namespace SerializerTest
{
    public class CompressionTest
    {
        [Test]
        public void Can_Compression() {
            var jil = new JilSerializer();

            var foo = TestHelper.GetFoo();

            var output1 = new MemoryStream();
            var output2 = new MemoryStream();

            jil.Serialize(foo, output1);

            var rtl1 = output1.ToArray();

            //compress
            var gzip = new GZipStream(output2, CompressionLevel.Fastest);
            StreamWriter sw2 = new StreamWriter(gzip);
            jil.Serialize(foo, sw2);

            sw2.Dispose();
            var rtl2 = output2.ToArray();

            //comparing the size
            Assert.Less(rtl2.Length, rtl1.Length);

            //decompress
            var gzip2 = new GZipStream(new MemoryStream(rtl2), CompressionMode.Decompress);
            StreamReader sr = new StreamReader(gzip2);
            var foo2 = jil.Deserialize<Foo>(sr);

            Assert.AreEqual(foo.ToString(), foo2.ToString());

        }
    }
}
