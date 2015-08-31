using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Es.Serializer;
using NUnit.Framework;

namespace SerializerTest
{
    public class ExtendTest
    {
        [TestFixtureSetUp]
        public void Init() {
            SerializerFactory.AddSerializer<JilSerializer>("jil");
            SerializerFactory.AddSerializer<JsonNetSerializer>("jsonNet");
            SerializerFactory.AddSerializer<ProtoBufSerializer>("ProtoBuf");
        }

        [Test]
        public void Can_deep_clone() {
            var foo = TestHelper.GetFoo();
            var foo2 = foo.DeepClone();
            Assert.AreNotEqual(foo, foo2);
            Assert.AreEqual(foo.ToString(), foo2.ToString());
        }

        [Test]
        public void Can_Jil_deep_clone() {
            Test_Deep_Clone(SerializerFactory.Get("jil"));
        }

        [Test]
        public void Can_jsonNet_deep_clone() {
            Test_Deep_Clone(SerializerFactory.Get("jsonNet"));
        }

        [Test]
        public void Can_ProtoBuf_deep_clone() {
            Test_Deep_Clone(SerializerFactory.Get("ProtoBuf"));
        }

        [Test]
        public void Can_dc_deep_clone() {
            Test_Deep_Clone(SerializerFactory.Get("dc"));
        }

        [Test]
        public void Can_soap_deep_clone() {
            Test_Deep_Clone(SerializerFactory.Get("soap"));
        }

        [Test]
        public void Can_xml_deep_clone() {
            Test_Deep_Clone(SerializerFactory.Get("xml"));
        }

        private void Test_Deep_Clone(ObjectSerializerBase sf) {
            var foo = TestHelper.GetFoo();
            var foo2 = sf.DeepClone(foo);
            Assert.AreNotEqual(foo, foo2);
            Assert.AreEqual(foo.ToString(), foo2.ToString());
        }
    }
}
