@echo off
set artifacts=%~dp0artifacts

if exist %artifacts%  rd /q /s %artifacts%

call dotnet restore src/Es.Serializer
call dotnet restore src/Es.Serializer.Jil
call dotnet restore src/Es.Serializer.JsonNet
call dotnet restore src/Es.Serializer.NetSerializer
call dotnet restore src/Es.Serializer.ProtoBuf

call dotnet build src/Es.Serializer -f netstandard2.0 -c release -o %artifacts%\netstandard2.0
call dotnet build src/Es.Serializer.JsonNet -f netstandard2.0 -c release -o %artifacts%\netstandard2.0
call dotnet build src/Es.Serializer.ProtoBuf -f netstandard2.0 -c release -o %artifacts%\netstandard2.0
call dotnet build src/Es.Serializer.Jil -f netstandard2.0 -c release -o %artifacts%\netstandard2.0

call dotnet build src/Es.Serializer -f net45 -c release -o %artifacts%\net45
call dotnet build src/Es.Serializer.Jil -f net45 -c release -o %artifacts%\net45
call dotnet build src/Es.Serializer.JsonNet -f net45 -c release -o %artifacts%\net45
call dotnet build src/Es.Serializer.NetSerializer -o %artifacts%\net45
call dotnet build src/Es.Serializer.ProtoBuf -f net45 -c release -o %artifacts%\net45

call dotnet pack -c release src/Es.Serializer  -o %artifacts%
call dotnet pack -c release src/Es.Serializer.Jil  -o %artifacts%
call dotnet pack -c release src/Es.Serializer.JsonNet  -o %artifacts%
call dotnet pack -c release src/Es.Serializer.NetSerializer  -o %artifacts%
call dotnet pack -c release src/Es.Serializer.ProtoBuf  -o %artifacts%
