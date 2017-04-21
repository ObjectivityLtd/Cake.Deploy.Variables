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
        settings.WithProperty("OutputPath", buildOutputDir)
                .SetConfiguration("Release"));
});

Task("NuGet")
    .Description("Create nuget package")
    .Does(()=>
{
    var packagePath = outputDir + "\\nuget";

    if(!DirectoryExists(packagePath))
    {
        CreateDirectory(packagePath);
    }

    var nuspecFile = sourceDir + "\\Cake.Deploy.Variables.nuspec";

    var nuGetPackSettings   = new NuGetPackSettings {
        BasePath        = outputDir + "\\dlls",
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
    .IsDependentOn("NuGet");


///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);