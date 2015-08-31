properties {

  $base_version = "0.0.2" 
  $base_dir = Split-Path $psake.build_script_file 

  $tools_dir = "$base_dir\tools"
  $artifacts_dir = "$base_dir\artifacts"
  $bin_dir = "$artifacts_dir\bin\"
  $nuspec_files = @("$base_dir\Es.Serializer.nuspec",`
                "$base_dir\Es.Serializer.Jil.nuspec",`
                "$base_dir\Es.Serializer.JsonNet.nuspec",`
                "$base_dir\Es.Serializer.ProtoBuf.nuspec",`
                "$base_dir\Es.Serializer.NetSerializer.nuspec")
  $nuget_tool = "$tools_dir\nuget\nuget.exe"

}

Framework('4.0')

function BuildHasBeenRun {
    $build_exists = (Test-Path $bin_dir)
    Assert $build_exists "Build task has not been run."
    $true
}

Task default -depends NugetPack

task NugetPack -precondition { (BuildHasBeenRun) } {
    Foreach($file in $nuspec_files){
        Exec { &$nuget_tool pack $file -o $artifacts_dir -Version $base_version -Symbols -BasePath $base_dir }
    }
}