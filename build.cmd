if exist %~dp0artifacts  rd /q /s %~dp0artifacts

call dotnet restore src/Es.Serializer
call dotnet restore src/Es.Serializer.Jil
call dotnet restore src/Es.Serializer.JsonNet
call dotnet restore src/Es.Serializer.NetSerializer
call dotnet restore src/Es.Serializer.ProtoBuf

call dotnet build -f netstandard1.6 -c release src/Es.Serializer  -o artifacts/netstandard1.6
call dotnet build -f netstandard1.6 -c release src/Es.Serializer.Jil  -o artifacts/netstandard1.6
call dotnet build -f netstandard1.6 -c release src/Es.Serializer.JsonNet  -o artifacts/netstandard1.6
call dotnet build -f netstandard1.6 -c release src/Es.Serializer.NetSerializer  -o artifacts/netstandard1.6
call dotnet build -f netstandard1.6 -c release src/Es.Serializer.ProtoBuf  -o artifacts/netstandard1.6

call dotnet build -f net45 -c release src/Es.Serializer  -o artifacts/net45
call dotnet build -f net45 -c release src/Es.Serializer.Jil  -o artifacts/net45
call dotnet build -f net45 -c release src/Es.Serializer.JsonNet  -o artifacts/net45
call dotnet build -f net45 -c release src/Es.Serializer.NetSerializer  -o artifacts/net45
call dotnet build -f net45 -c release src/Es.Serializer.ProtoBuf  -o artifacts/net45

call dotnet pack -c release src/Es.Serializer  -o artifacts
call dotnet pack -c release src/Es.Serializer.Jil  -o artifacts
call dotnet pack -c release src/Es.Serializer.JsonNet  -o artifacts
call dotnet pack -c release src/Es.Serializer.NetSerializer  -o artifacts
call dotnet pack -c release src/Es.Serializer.ProtoBuf  -o artifacts