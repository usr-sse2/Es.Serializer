using System.IO;
using Es.Serializer;
using NUnit.Framework;

namespace SerializerTest
{
    public class BinarySerializerTest
    {
        [Test]
        public void Can_Binary_Serializer() {
            var bs = new BinarySerializer();

            var foo1 = TestHelper.GetFoo();
            Stream output = new MemoryStream();
            bs.Serialize(foo1, output);

            output.Position = 0;
            var foo2 = bs.Deserialize<Foo>(output);
            Assert.AreEqual(foo1.ToString(), foo2.ToString());

            output.Position = 0;
            var foo3 = bs.Deserialize(output, typeof(Foo));
            Assert.AreEqual(foo2.ToString(), foo3.ToString());

            output.Dispose();
        }

        [Test]
        public void Can_DataContract_Serializer() {
            var bs = new DataContractSerializer();

            var foo1 = TestHelper.GetFoo();
            Stream output = new MemoryStream();
            bs.Serialize(foo1, output);

            output.Position = 0;
            var foo2 = bs.Deserialize<Foo>(output);
            Assert.AreEqual(foo1.ToString(), foo2.ToString());

            output.Position = 0;
            var foo3 = bs.Deserialize(output, typeof(Foo));
            Assert.AreEqual(foo2.ToString(), foo3.ToString());

            output.Dispose();
        }

        [Test]
        public void Can_Soap_Serializer() {
            var bs = new SoapSerializer();

            var foo1 = TestHelper.GetFoo();
            Stream output = new MemoryStream();
            bs.Serialize(foo1, output);

            output.Position = 0;
            var foo2 = bs.Deserialize<Foo>(output);
            Assert.AreEqual(foo1.ToString(), foo2.ToString());

            output.Position = 0;
            var foo3 = bs.Deserialize(output, typeof(Foo));
            Assert.AreEqual(foo2.ToString(), foo3.ToString());

            output.Dispose();
        }

        [Test]
        public void Can_ProtoBuf_Serializer() {
            var bs = new ProtoBufSerializer();

            var foo1 = TestHelper.GetFoo();
            Stream output = new MemoryStream();
            bs.Serialize(foo1, output);

            output.Position = 0;
            var foo2 = bs.Deserialize<Foo>(output);
            Assert.AreEqual(foo1.ToString(), foo2.ToString());

            output.Position = 0;
            var foo3 = bs.Deserialize(output, typeof(Foo));
            Assert.AreEqual(foo2.ToString(), foo3.ToString());

            output.Dispose();
        }
    }
}