#tool "nuget:?package=xunit.runner.console"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var sourceDir = "..\\src";
var outputDir = "..\\bin";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(() =>
{
    // Executed BEFORE the first task.
    Information("Running tasks...");

    if(!DirectoryExists(outputDir))
    {
        Information("Output directory does not exist.");
        CreateDirectory(outputDir);
    }
    else
    {
        CleanDirectory(outputDir);
    }
});

Teardown(() =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("BuildSolution")
    .Description("Builds Cake.Deploy.Variables solution")
    .Does(() =>
{
    var solution = sourceDir + "\\Cake.Deploy.Variables.sln";

    NuGetRestore(solution);

    var buildOutputDir = "\"" + MakeAbsolute(Directory(outputDir)).FullPath + "/dlls\"";

    Information(buildOutputDir);

    MSBuild(solution, settings => 
        settings.SetConfiguration("Release"));
});

Task("TestRun")
    .Description("Run Unit Tests")
    .Does(() =>
{
    var testAssemblies = GetFiles("..\\src\\**\\bin\\Release\\*.Test.dll");

    XUnit2(testAssemblies);
});

Task("NuGet")
    .Description("Create nuget package")
    .Does(()=>
{
    var packagePath = outputDir;

    if(!DirectoryExists(packagePath))
    {
        CreateDirectory(packagePath);
    }

    var nuspecFile = sourceDir + "\\Cake.Deploy.Variables.nuspec";

    var nuGetPackSettings   = new NuGetPackSettings {
        BasePath        = sourceDir + "\\Cake.Deploy.Variables\\bin\\Release\\",
        OutputDirectory = packagePath
    };

    NuGetPack(nuspecFile, nuGetPackSettings);
});

///////////////////////////////////////////////////////////////////////////////
// TARGETS
///////////////////////////////////////////////////////////////////////////////

Task("Default")
    .Description("This is the default task which will be ran if no specific target is passed in.")
    .IsDependentOn("BuildSolution")
    .IsDependentOn("TestRun")
    .IsDependentOn("NuGet");


///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);