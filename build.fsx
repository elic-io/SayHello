// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open System.IO

// Directories
let getPath localPath = 
    (Directory.GetCurrentDirectory(),localPath)
    |> Path.Combine
    |> Path.GetFullPath

let buildDir  = getPath "./build/"
let deployDir = getPath "./deploy/"


// Filesets
let appReferences  =
    !! "/**/*.csproj"
    ++ "/**/*.fsproj"

// version info
let version = "0.1"  // or retrieve from CI server

// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir; deployDir]
)

Target "Build" (fun _ ->
    DotNetCli.Build
      (fun p -> 
           { p with
                Output = buildDir
                Configuration = "Release" })
    // compile all projects below src/app/
    //MSBuildDebug buildDir "Build" appReferences
    //|> Log "AppBuild-Output: "
)

Target "Deploy" (fun _ ->
    !! (buildDir + "/**/*.*")
    -- "*.zip"
    |> Zip buildDir (deployDir + "ApplicationName." + version + ".zip")
)

// Build order
"Clean"
  ==> "Build"
  ==> "Deploy"

// start build
RunTargetOrDefault "Build"
