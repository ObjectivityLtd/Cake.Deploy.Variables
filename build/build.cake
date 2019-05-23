#tool "nuget:?package=xunit.runner.console"

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");
var version = Argument<string>("FileVersion", "1.0.0");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var sourceDir = "..\\src";
var outputDir = "..\\bin";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
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

Teardown(context =>
{
    // Executed AFTER the last task.
    Information("Finished running tasks.");
});

///////////////////////////////////////////////////////////////////////////////
// TASK DEFINITIONS
///////////////////////////////////////////////////////////////////////////////

Task("BuildSolution")
    .IsDependentOn("Version")
    .Description("Builds Cake.Deploy.Variables solution")
    .Does(() =>
{
    var solution = sourceDir + "\\Cake.Deploy.Variables.sln";

    NuGetRestore(solution);

    var buildOutputDir = "\"" + MakeAbsolute(Directory(outputDir)).FullPath + "/dlls\"";

    Information(buildOutputDir);

     var settings = new DotNetCoreBuildSettings
     {
         Configuration = configuration
     };

    DotNetCoreBuild(solution, settings);
});

Task("Version")
    .Description("Set assembly Version")
    .Does(() => {
        var propsFile = sourceDir + "/Directory.build.props";
        var readedVersion = XmlPeek(propsFile, "//Version");
        var currentVersion = new Version(readedVersion);

        XmlPoke(propsFile, "//Version", version);
        
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

    var nuspecFile = sourceDir + "\\Cake.Deploy.Variables\\Cake.Deploy.Variables.csproj";

    var nuGetPackSettings = new DotNetCorePackSettings  {
        OutputDirectory = packagePath,
        Configuration = configuration
    };

    DotNetCorePack(nuspecFile, nuGetPackSettings);
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