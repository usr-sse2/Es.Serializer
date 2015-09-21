

### Es.Serializer

A serialized factory provider, support a variety of serialization.

## Usage


### Serializing

```C#

StringWriter sw = new StringWriter();

SerializerFactory.Default.Serialize(obj,sw);

StringReader sr = new StringReader(sw.ToString());

```

### Serializing Stream

```C#

Stream output = new MemoryStream();

SerializerFactory.Default.Serialize(obj,outbut);

output.Position = 0;

var foo = bs.Deserialize(output, typeof(Foo));

```

### Serializing Stream

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