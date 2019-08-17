#tool paket:?package=OpenCover
#tool paket:?package=codecov
#tool paket:?package=GitVersion.CommandLine
#addin paket:?package=Cake.Paket
#addin paket:?package=Cake.Codecov

var target = Argument("target", "Build");
var configuration = Argument("Configuration", "Debug");

GitVersion version;

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

Task("GitVersion")
    .Does(() => {
        version = GitVersion(new GitVersionSettings {
            UpdateAssemblyInfo = true,
        });

        if (BuildSystem.IsLocalBuild == false)
        {
            GitVersion(new GitVersionSettings {
                OutputType = GitVersionOutput.BuildServer
            });
        }
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
    .IsDependentOn("GitVersion")
    .Does(() => {
       var buildVersion = string.Format("{0}.build.{1}",
            version.FullSemVer,
            BuildSystem.AppVeyor.Environment.Build.Number
        );
        var settings = new CodecovSettings {
            Files = new[] { "./coverage/cobertura.xml" },
            EnvironmentVariables = new Dictionary<string,string> { { "APPVEYOR_BUILD_VERSION", buildVersion } }
        };
        Codecov(settings);
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
