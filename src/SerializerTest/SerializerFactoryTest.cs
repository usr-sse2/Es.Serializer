using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Serializer;
using NUnit.Framework;

namespace SerializerTest
{
    public class SerializerFactoryTest
    {
        [Test]
        public void Can_Set_Default() {
            var binary = SerializerFactory.Default;
            var xml = SerializerFactory.Get("xml");
            SerializerFactory.SetDefault(xml);

            var def = SerializerFactory.Default;

            Assert.AreNotEqual(binary, def);
            Assert.AreEqual(xml, def);

            SerializerFactory.SetDefault(binary);
            def = SerializerFactory.Default;

            Assert.AreNotEqual(xml, def);
            Assert.AreEqual(binary, def);
        }

        [Test]
        public void Can_Add_Serializer() {
            SerializerFactory.AddSerializer(new JilSerializer(), "jil");
            SerializerFactory.AddSerializer(new JsonNetSerializer(), new string[] { "b", "a" });

            foreach (var key in SerializerFactory.Alias) {
                Assert.True(SerializerFactory.Contains(key));
            }
        }
    }
}
