// include Fake libs
#r "./packages/FAKE/tools/FakeLib.dll"

open Fake
open Fake.AssemblyInfoFile
open Fake.Testing

// Directories
let buildDir  = "./build/"
let levelDir = "./levels/"
let deployDir = "./deploy/"
let dockerDir = "./docker/"
let dockerDeploy = "CoffeeMachine"
let zipFile = "CoffeeMachine.zip"
let configFile = "CoffeeMachine.WebApi.exe.config"

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
    |> Zip "./" (deployDir + zipFile)
)

Target "Docker"(fun _ ->
    DeleteDir (dockerDir + dockerDeploy)
    Unzip dockerDir (deployDir + zipFile)
    Rename (dockerDir + dockerDeploy) (dockerDir + buildDir)
    DeleteFile (dockerDir + dockerDeploy + configFile)
    Copy  (dockerDir + dockerDeploy) [(dockerDir + configFile)]
  )

// Build order
"Clean"
  ==> "Build"
  ==> "Test"
  ==> "Deploy"
  ==> "Docker"

// start build
RunTargetOrDefault "Test"
