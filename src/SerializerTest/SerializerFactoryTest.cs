using Es.Serializer;
using Xunit;

namespace SerializerTest
{
    public class SerializerFactoryTest
    {
        [Fact]
        public void Can_Set_Default() {
            var xml = SerializerFactory.Default;
            var dc = SerializerFactory.Get("dc");
            SerializerFactory.SetDefault(dc);

            var def = SerializerFactory.Default;

            Assert.NotEqual(xml, def);
            Assert.Equal(dc, def);

            SerializerFactory.SetDefault(xml);
            def = SerializerFactory.Default;

            Assert.NotEqual(dc, def);
            Assert.Equal(xml, def);
        }

        [Fact]
        public void Can_Add_Serializer() {
            SerializerFactory.AddSerializer(new ProtoBufSerializer(), "ProtoBuf");
            SerializerFactory.AddSerializer(new JsonNetSerializer(), new string[] { "b", "a" });

            foreach (var key in SerializerFactory.Alias) {
                Assert.True(SerializerFactory.Contains(key));
            }
        }
    }
}