﻿using System.IO;
using Es.Serializer;
using Xunit;

namespace SerializerTest
{
#if NETFULL || NETCOREAPP2_0
    public class BinarySerializerTest
    {

        [Fact]
        public void Can_Binary_Serializer_String() {
            var bs = SerializerFactory.Get("binary");

            var foo1 = TestHelper.GetFoo();

            var str = bs.SerializeToString(foo1);

            var foo2 = bs.DeserializeFromString<Foo>(str);

            Assert.Equal(foo1.ToString(), foo2.ToString());
        }

        [Fact]
        public void Can_Binary_Serializer_Stream() {
            var bs = SerializerFactory.Get("binary");

            var foo1 = TestHelper.GetFoo();
            Stream output = new MemoryStream();
            bs.Serialize(foo1, output);

            output.Position = 0;
            var foo2 = bs.Deserialize<Foo>(output);

            output.Dispose();

            Assert.Equal(foo1.ToString(), foo2.ToString());
        }

        [Fact]
        public void Can_Binary_Serializer_Bytes() {
            var bs = SerializerFactory.Get("binary");

            var foo1 = TestHelper.GetFoo();
            byte[] output;
            bs.Serialize(foo1, out output);

            var foo2 = bs.Deserialize<Foo>(output);
            Assert.Equal(foo1.ToString(), foo2.ToString());
        }

        [Fact]
        public void Can_Binary_Serializer_Writer_And_Reader() {
            var bs = SerializerFactory.Get("binary");
            var foo1 = TestHelper.GetFoo();

            StringWriter sw = new StringWriter();

            bs.Serialize(foo1, sw);

            StringReader sr = new StringReader(sw.ToString());

            var foo2 = bs.Deserialize<Foo>(sr);

            Assert.Equal(foo1.ToString(), foo2.ToString());
        }
    }
#endif
}