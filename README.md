

### Es.Serializer

A serialized factory provider, support a variety of serialization.

Packages & Status
---

Package  | NuGet         |
-------- | :------------ |
|**Es.Serializer**|[![NuGet package](https://buildstats.info/nuget/Es.Serializer)](https://www.nuget.org/packages/Es.Serializer)
|**Es.Serializer.Jil**|[![NuGet package](https://buildstats.info/nuget/Es.Serializer.Jil)](https://www.nuget.org/packages/Es.Serializer.Jil)
|**Es.Serializer.JsonNet**|[![NuGNuGet packageet](https://buildstats.info/nuget/Es.Serializer.JsonNet)](https://www.nuget.org/packages/Es.Serializer.JsonNet)
|**Es.Serializer.NetSerializer**|[![NuGet package](https://buildstats.info/nuget/Es.Serializer.NetSerializer)](https://www.nuget.org/packages/Es.Serializer.NetSerializer)
|**Es.Serializer.ProtoBuf**|[![NuGet package](https://buildstats.info/nuget/Es.Serializer.ProtoBuf)](https://www.nuget.org/packages/Es.Serializer.ProtoBuf)

## Usage


### Serializing

```C#

StringWriter sw = new StringWriter();

SerializerFactory.Default.Serialize(obj,sw);

StringReader sr = new StringReader(sw.ToString());

var foo = bs.Deserialize(sr, typeof(Foo));

```

### Serializing Stream

```C#

Stream output = new MemoryStream();

SerializerFactory.Default.Serialize(obj,outbut);

output.Position = 0;

var foo = bs.Deserialize(output, typeof(Foo));

```

### Serializing String

```C#

var output = SerializerFactory.Default.SerializeToString(obj,outbut);

var foo = bs.DeserializeFromString<Foo>(output);

```

### Add Serialized Provider

```C#

SerializerFactory.AddSerializer(new JilSerializer(), "jil");

SerializerFactory.AddSerializer(new JsonNetSerializer(), new string[] { "jsonNet", "json" });

SerializerFactory.AddSerializer<NETSerializer>("NET");

--Set Default
SerializerFactory.SetDefault(new JilSerializer());

```