set ng="%~dp0Tools\NuGet\NuGet.exe"
%ng% push %~dp0artifacts\Es.Serializer.0.0.2.nupkg
%ng% push %~dp0artifacts\Es.Serializer.Jil.0.0.2.nupkg
%ng% push %~dp0artifacts\Es.Serializer.JsonNet.0.0.2.nupkg
%ng% push %~dp0artifacts\Es.Serializer.ProtoBuf.0.0.2.nupkg