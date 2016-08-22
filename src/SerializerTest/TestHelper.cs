using System;

namespace SerializerTest
{
    public class TestHelper
    {
        public static Foo GetFoo() {
            return new Foo
            {
                Id = 1,
                Name = "John",
                Age = 30,
                Date = DateTime.UtcNow,
                Sex = true,
                Xuid = Guid.NewGuid(),
                UserStatus = UserStatus.Approved,
                Lastip = new byte[] { 192, 168, 0, 254 },
                Inner = new InnerFoo { InnerNumeric = 999.99912m }
            };
        }
    }
}