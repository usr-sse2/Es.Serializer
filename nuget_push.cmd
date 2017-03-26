set /p ver=<VERSION
set sourceUrl=-s https://www.nuget.org/api/v2/package

dotnet nuget push artifacts/Es.Serializer.%ver%.nupkg %sourceUrl%
dotnet nuget push artifacts/Es.Serializer.Jil.%ver%.nupkg %sourceUrl%
dotnet nuget push artifacts/Es.Serializer.JsonNet.%ver%.nupkg %sourceUrl%
dotnet nuget push artifacts/Es.Serializer.NetSerializer.%ver%.nupkg %sourceUrl%
dotnet nuget push artifacts/Es.Serializer.ProtoBuf.%ver%.nupkg %sourceUrl%
