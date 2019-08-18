#tool paket:?package=OpenCover
#tool paket:?package=codecov
#addin paket:?package=Cake.Paket
#addin paket:?package=Cake.Codecov

var target = Argument("target", "Build");
var configuration = Argument("Configuration", "Debug");

Task("CI")
    .IsDependentOn("Pack")
    .IsDependentOn("Codecov").Does(() => {});

Task("Pack")
    .IsDependentOn("Build")
    .DoesForEach(GetFiles("./src/**/*.csproj"), path => {
        var settings = new DotNetCorePackSettings {
            Configuration = configuration,
            OutputDirectory = "./nugets/",
            NoBuild = true,
            MSBuildSettings = new DotNetCoreMSBuildSettings()
        };

        DotNetCorePack(path.FullPath, settings);
    });

Task("Restore")
    .Does(() => {
        DotNetCoreRestore(new DotNetCoreRestoreSettings {
            Sources = new[] {
                "https://api.nuget.org/v3/index.json",
                "https://www.myget.org/F/tpluscode/api/v3/index.json"
            },
        });
    });

Task("Build")
    .IsDependentOn("Restore")
    .Does(() => {
        DotNetCoreBuild("Argolis.sln", new DotNetCoreBuildSettings {
            Configuration = configuration
        });
    });

Task("Codecov")
    .IsDependentOn("Test")
    .Does(() => {
        Codecov("coverage\\cobertura.xml");
    });

Task("Test")
    .IsDependentOn("Build")
    .Does(RunTests("Argolis.tests"))
    .Does(RunTests("Argolis.tests.Integration"))
    .Does(() => {
        DotCoverMerge(GetFiles("coverage\\*.dcvr"), "coverage\\merged.dcvr");
    })
    .Does(() => {
        DotCoverReport(
          "./coverage/merged.dcvr",
          "./coverage/dotcover.xml",
          new DotCoverReportSettings {
            ReportType = DotCoverReportType.DetailedXML,
          });
    })
    .Does(() => {
       ReportGenerator("./coverage/dotcover.xml", "./coverage", new ReportGeneratorSettings() {
            ReportTypes = new [] { ReportGeneratorReportType.Cobertura }
        });
    })
    .DeferOnError();

public Action<ICakeContext> RunTests(string project)
{
    var testSettings = new DotNetCoreTestSettings
    {
            Configuration = configuration,
            NoBuild = true
    };

    return (ICakeContext c) =>
        DotCoverCover(
            (ICakeContext ctx) => ctx.DotNetCoreTest(GetFiles($"**\\{project}.csproj").Single().FullPath, testSettings),
            $"./coverage/{project}.dcvr",
            new DotCoverCoverSettings());
}

RunTarget(target);
