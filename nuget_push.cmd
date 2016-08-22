set /p ver=<VERSION
set sourceUrl=-Source https://www.nuget.org/api/v2/package

nuget push artifacts/Es.Serializer.%ver%.nupkg %sourceUrl%
nuget push artifacts/Es.Serializer.Jil.%ver%.nupkg %sourceUrl%
nuget push artifacts/Es.Serializer.JsonNet.%ver%.nupkg %sourceUrl%
nuget push artifacts/Es.Serializer.NetSerializer.%ver%.nupkg %sourceUrl%
nuget push artifacts/Es.Serializer.ProtoBuf.%ver%.nupkg %sourceUrl%