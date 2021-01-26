#addin "nuget:?package=Cake.Coverlet&version=2.3.4"

#tool "nuget:?package=ReportGenerator&version=4.1.8"
#tool "nuget:?package=NUnit.Runners&version=2.6.4"
#tool "dotnet:?package=coverlet.console&version=1.5.1"

#module "nuget:?package=Cake.DotNetTool.Module&version=0.3.0"

var target = Argument("Target", "Default");

public class CurrentDirectory : System.IDisposable
{
    readonly string _oldPath;

    public CurrentDirectory(string path)
    {
        _oldPath = Environment.CurrentDirectory;
        Environment.CurrentDirectory = path;
    }

    public void Dispose() => Environment.CurrentDirectory = _oldPath;

}

Task("NugetRestore")
  .Does(() =>
  {
    NuGetRestore("./Xamarin.Forms.TestingLibrary.sln");
  });

Task("BuildTest")
  .IsDependentOn("NugetRestore")
    .Does(() =>
    {
        DotNetCoreBuild("./test/Xamarin.Forms.TestingLibrary.Tests/Xamarin.Forms.TestingLibrary.Tests.csproj",
        new DotNetCoreBuildSettings
        {
            Verbosity = DotNetCoreVerbosity.Minimal,
            Configuration = "Debug",
            NoRestore = true
        });
    });

Task("Test")
  .IsDependentOn("BuildTest")
  .Does(() =>
  {
        if (DirectoryExists(@".\coverage-results"))
        {
            DeleteDirectory(@".\coverage-results\", new DeleteDirectorySettings()
            {
                Recursive = true
            });
        }

        var coverletSettings = new CoverletSettings {
            CollectCoverage = true,
            CoverletOutputFormat = CoverletOutputFormat.cobertura,
            CoverletOutputDirectory = Directory(@".\coverage-results\"),
            CoverletOutputName = $"results-{DateTime.UtcNow:dd-MM-yyyy-HH-mm-ss-FFF}",
            Exclude = new List<string>
                            {
                                "[Xamarin.Forms.TestingLibrary.SampleApp]*"
                            }
        };

        Coverlet(
            new FilePath("./test/Xamarin.Forms.TestingLibrary.Tests/Xamarin.Forms.TestingLibrary.Tests.csproj"),
            coverletSettings);
  });

Task("ReportGenerator")
    .IsDependentOn("Test")
    .Does(() => {
        var reportGeneratorSettings = new ReportGeneratorSettings();

        ReportGenerator("./coverage-results/*.xml", "./coverage-results/ReportGeneratorOutput", reportGeneratorSettings);

        if (IsRunningOnUnix())
            StartProcess("open", "./coverage-results/ReportGeneratorOutput/index.htm");
        else
            StartProcess("explorer", ".\\coverage-results\\ReportGeneratorOutput\\index.htm");
    });

Task("Default")
  .Does(() =>
  {

  });

Task("Clean")
    .Does(() =>
    {
        var objFolders = GetDirectories("./src/**/obj*");

        foreach (var objFolder in objFolders)
        {
            if (DirectoryExists(objFolder))
            {
                DeleteDirectory(objFolder, true);
            }
        }

        var binFolders = GetDirectories("./src/**/bin*");


        foreach (var binFolder in binFolders)
        {
            if (DirectoryExists(binFolder))
            {
                DeleteDirectory(binFolder, new DeleteDirectorySettings
                {
                    Recursive = true,
                    Force = true
                });
            }
        }
    });

RunTarget(target);
