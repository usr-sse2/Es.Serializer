﻿using System.IO;
using Es.Serializer;
using NUnit.Framework;

namespace SerializerTest
{
    public class BinarySerializerTest
    {

        [Test]
        public void Can_Binary_Serializer_String() {
            var bs = SerializerFactory.Get("binary");

            var foo1 = TestHelper.GetFoo();

            var str = bs.SerializeToString(foo1);

            var foo2 = bs.DeserializeFromString<Foo>(str);

            Assert.AreEqual(foo1.ToString(), foo2.ToString());
        }

        [Test]
        public void Can_Binary_Serializer_Stream() {
            var bs = SerializerFactory.Get("binary");

            var foo1 = TestHelper.GetFoo();
            Stream output = new MemoryStream();
            bs.Serialize(foo1, output);

            output.Position = 0;
            var foo2 = bs.Deserialize(output, typeof(Foo));

            output.Dispose();

            Assert.AreEqual(foo1.ToString(), foo2.ToString());
        }

        [Test]
        public void Can_Binary_Serializer_Bytes() {
            var bs = SerializerFactory.Get("binary");

            var foo1 = TestHelper.GetFoo();
            byte[] output;
            bs.Serialize(foo1, out output);

            var foo2 = bs.Deserialize(output, typeof(Foo));
            Assert.AreEqual(foo1.ToString(), foo2.ToString());
        }

        [Test]
        public void Can_Binary_Serializer_Writer_And_Reader() {
            var bs = SerializerFactory.Get("binary");
            var foo1 = TestHelper.GetFoo();

            StringWriter sw = new StringWriter();

            bs.Serialize(foo1, sw);

            StringReader sr = new StringReader(sw.ToString());

            var foo2 = bs.Deserialize(sr, typeof(Foo));

            Assert.AreEqual(foo1.ToString(), foo2.ToString());
        }
    }
}