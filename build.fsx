#r "packages/FAKE/tools/FakeLib.dll"
open Fake

let buildDir = @".\build"
let packages = @".\src\packages"
let solution = @".\src\RedisWindowsNewRelicPlugin.sln"
let package  = @"RedisWindowsNewRelicPlugin.zip"

Target "RestorePackages" (fun _ ->
    solution
    |> RestoreMSSolutionPackages (fun p ->
       { p with
           OutputPath = packages })
)

Target "Build" (fun _ ->
    !! solution
        |> MSBuildRelease buildDir "Build"
        |> Log "AppBuild-Output: "
)

Target "Package" (fun _ ->
    DeleteFile package
    !! (sprintf @"%s\**" buildDir)
        |> Zip buildDir package
)

"RestorePackages"
    ==> "Build"
    ==> "Package"

RunTargetOrDefault "Package"
