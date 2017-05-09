// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile
open Fake.Testing

// Directories
let buildDir  = "./build/"
let levelDir = "./levels/"
let deployDir = "./deploy/"
// Targets
Target "Clean" (fun _ ->
    CleanDirs [buildDir]
)


Target "Build" (fun _ ->
    CreateFSharpAssemblyInfo "./Core/Properties/AssemblyInfo.fs"
      [Attribute.InternalsVisibleTo "Tests" ]
    !! "/**/*.fsproj"
    |> MSBuildDebug buildDir "Build"
    |> Log "AppBuild-Output:"
    DeleteFile (buildDir @@ "System.Runtime.InteropServices.RuntimeInformation.dll")
)

Target "Test" (fun _ ->
    !! "/**/build/Tests.dll"
    |> xUnit2 (fun p -> { p with HtmlOutputPath = Some (buildDir @@ "xunit.html") })
)

// Build order
Target "Deploy" (fun _ ->
    !! (buildDir + "/**/*.*")
    -- "*.zip"
    |> Zip "./" (deployDir + "CoffeeMachine.zip")
)

Target "Docker"(fun _ ->
    Unzip "./docker" (deployDir + "CoffeeMachine.zip")
    Rename "./docker/CoffeeMachine" "./docker/build" 
  )

// Build order
"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Deploy"
  ==> "Docker"

// start build
RunTargetOrDefault "Test"
