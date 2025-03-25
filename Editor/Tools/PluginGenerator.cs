using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

// ReSharper disable once CheckNamespace
namespace Unity.GhysX.Framework.Plugin.Generator.Tools.Editors
{
    public class PluginGenerator
    {
        private readonly PluginConfig _config;
        private readonly string _rootPath;

        public PluginGenerator(PluginConfig config)
        {
            this._config = config;
            // this.rootPath = Path.Combine(Application.dataPath, config.name.SanitizeFileName());
            this._rootPath = Path.Combine(Application.dataPath, config.name);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public bool Generate()
        {
            try
            {
                CreateDirectoryStructure();
                GeneratePackageJson();
                GenerateChangelog();
                GenerateMITLicense();
                GenerateLinkXml();
                GenerateReadme();
                GenerateThirdPartyNotices();
                GenerateAssemblyInfoFiles();
                GenerateAssemblyDefinitionFiles();
                AssetDatabase.Refresh();
                return true;
            }
            catch (Exception e)
            {
                Debug.LogError($"Generation failed: {e}");
            }

            return false;
        }

        private void CreateDirectoryStructure()
        {
            var directories = new[]
            {
                "Documentation~/images",
                "Editor",
                "PackageResources",
                "Plugins",
                "Resources",
                "Runtime",
                "Samples~/Examples",
                "Samples~/Tutorials",
                "Tests/Editor",
                "Tests/Runtime"
            };

            foreach (var dir in directories)
            {
                Directory.CreateDirectory(Path.Combine(_rootPath, dir));
            }
        }

        private void GeneratePackageJson()
        {
            var template = @"{
  ""name"": ""com.{author}.{pname}"",
  ""displayName"": ""{name}"",
  ""version"": ""{version}"",
  ""unity"": ""{unity}"",
  ""description"": ""{description}"",
  ""license"": ""MIT"",
  ""scopedRegistries"": [
    {
      ""name"": ""package.openupm.com"",
      ""url"": ""https://package.openupm.com"",
      ""scopes"": [
        ""com.openupm"",
        ""com.cysharp.unitask"",
        ""org.icsharpcode.sharpziplib""
      ]
    }
  ],
  ""dependencies"": {
    ""com.cysharp.unitask"": ""2.5.10"",
    ""com.unity.nuget.newtonsoft-json"": ""3.2.1"",
    ""org.icsharpcode.sharpziplib"": ""1.4.1""
  },
  ""keywords"": [{keywords}],
  ""author"": {
    ""name"": ""{author}"",
    ""email"": ""{email}"",
    ""url"": ""{url}""
  },
  ""category"": ""{category}"",
  ""relatedPackages"": {},
  ""repository"": {
    ""type"": ""git"",
    ""url"": ""{url}""
  },
  ""samples"": [
    {
      ""displayName"": ""Examples"",
      ""description"": ""Examples"",
      ""path"": ""Samples~/Examples""
    },
    {
      ""displayName"": ""Tutorials"",
      ""description"": ""Tutorials"",
      ""path"": ""Samples~/Tutorials""
    }
  ],
  ""publishConfig"": {
    ""registry"": ""https://package.openupm.com""
  }
}";

            var content = template
                .Replace("{author}", _config.author)
                .Replace("{pname}", _config.name.ToLower().Replace(" ", "-").Replace(".", "-"))
                .Replace("{name}", _config.name)
                .Replace("{version}", _config.version)
                .Replace("{unity}", _config.unity)
                .Replace("{description}", _config.description)
                .Replace("{keywords}", $"\n    {FormatKeywords(_config.keywords)}\n  ")
                .Replace("{email}", _config.email)
                .Replace("{url}", _config.url.Replace("{author}", _config.author).Replace("{projectName}", _config.name.ToLower().Replace(" ", "-").Replace(".", "-")))
                .Replace("{category}", _config.category);

            WriteFile("package.json", content);
        }

        private void GenerateChangelog()
        {
            var template = @"# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [{version}] - {date}

### Added

- Initial release
";
            // Get year-month-day
            var date = DateTime.Now.ToString("yyyy-MM-dd");

            template = template.Replace("{version}", _config.version)
                .Replace("{date}", date);
            WriteFile("CHANGELOG.md", template);
        }

        // ReSharper disable once InconsistentNaming
        private void GenerateMITLicense()
        {
            var template = @"MIT License

Copyright (c) {year} {author}

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the ""Software""), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and thispermission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
";

            // acquisition year
            var year = DateTime.Now.ToString("yyyy");

            template = template.Replace("{year}", year)
                .Replace("{author}", _config.author);
            WriteFile("LICENSE", template);
        }

        private void GenerateLinkXml()
        {
            var template = @"<linker>
    <assembly fullname=""{assemblyName}.Runtime"" preserve=""all""/>
</linker>
";
            var assemblyName = _config.name.SanitizeFileName();
            var content = template.Replace("{assemblyName}", assemblyName);
            WriteFile("Link.xml", content);
        }

        private void GenerateReadme()
        {
            var template = @"# {name}

{description}

## Installation

1. Open the Package Manager window (Window > Package Manager).
2. Click the '+' button in the upper left corner and select 'Add package from git URL...'.
3. Enter the following URL: {url}.git
4. Click 'Add'.

## Usage

1. Follow the installation steps above.
2. Use the package in your project as needed.

## Contributing

Contributions are welcome! If you find a bug or have a feature request, please open an issueon the GitHub repository. If you'd like to contribute code, please  open an issue on the [GitHub repository]({url}/issues).  

**Want to contribute code?**  
1. **Fork the repository** and create your branch from `main`.  
2. **Commit changes** with clear descriptions.  
3. **Test thoroughly** and ensure existing tests pass.  
4. **Update documentation** if needed.  
5. **Push** to your fork and **open a Pull Request** against the `main` branch.  

**Tips**:  
- Follow the project’s code style and conventions.  
- Keep PRs focused and reference related issues.  

We appreciate your help! ✨  
";

            template = template.Replace("{name}", _config.name)
                .Replace("{description}", _config.description)
                .Replace("{url}", _config.url.Replace("{author}", _config.author).Replace("{projectName}", _config.name.ToLower().Replace(" ", "-").Replace(".", "-")));
            WriteFile("README.md", template);
        }

        private void GenerateThirdPartyNotices()
        {
            var template = @"# Third Party Notices

This project depends on the following third party libraries:

- [Newtonsoft.Json](https://www.newtonsoft.com/json) - MIT License
- [Unitask](https://github.com/Cysharp/UniTask) - MIT License
- [SharpZipLib](https://icsharpcode.github.io/SharpZipLib/) - BSD 2-Clause License

Please refer to the respective licenses for more information.
";
            WriteFile("Third Party Notices.md", template);
        }

        private string FormatKeywords(List<string> keywords)
        {
            if (keywords == null || keywords.Count == 0)
                return "\"Tools\""; // default value

            // Filter null values and add quotes
            var validKeywords = keywords
                .Where(k => !string.IsNullOrWhiteSpace(k))
                .Select(k => $"\"{k.Replace("\"", "\\\"")}\"")
                .ToList();

            return validKeywords.Count > 0 ? string.Join(",\n    ", validKeywords) : "\"Tools\"";
        }

        private void GenerateAssemblyInfoFiles()
        {
            var template = @"using System.Reflection;

[assembly: AssemblyVersion(""{version}"")]
[assembly: AssemblyCompany(""{author}"")]
[assembly: AssemblyCopyright(""Copyright © {year} {author}"")]
[assembly: AssemblyDescription(""{description}"")]

#if BESTHTTP_WITH_BURST
[assembly: Unity.Burst.BurstCompile(CompileSynchronously = true, OptimizeFor = Unity.Burst.OptimizeFor.Performance)]
#endif
";

            var year = DateTime.Now.ToString("yyyy");
            var content = template
                .Replace("{author}", _config.author)
                .Replace("{version}", _config.version)
                .Replace("{description}", _config.description)
                .Replace("{year}", year);

            WriteFile("Editor/AssemblyInfo.cs", content);
            WriteFile("Runtime/AssemblyInfo.cs", content);
            WriteFile("Tests/Editor/AssemblyInfo.cs", content);
            WriteFile("Tests/Runtime/AssemblyInfo.cs", content);
        }

        private void GenerateAssemblyDefinitionFiles()
        {
            var templateEditor = @"{
    ""name"": ""{assemblyName}"",
    ""rootNamespace"": ""Unity.{assemblyName}"",
    ""references"": [],
    ""includePlatforms"": [
        ""Editor""
    ],
    ""excludePlatforms"": [],
    ""allowUnsafeCode"": false,
    ""overrideReferences"": false,
    ""precompiledReferences"": [],
    ""autoReferenced"": true,
    ""defineConstraints"": [],
    ""versionDefines"": [],
    ""noEngineReferences"": false
}";

            var templateRuntime = @"{
    ""name"": ""{assemblyName}"",
    ""rootNamespace"": ""Unity.{assemblyName}"",
    ""references"": [],
    ""optionalUnityReferences"": [],
    ""includePlatforms"": [],
    ""excludePlatforms"": [],
    ""allowUnsafeCode"": true,
    ""overrideReferences"": false,
    ""precompiledReferences"": [],
    ""autoReferenced"": true,
    ""defineConstraints"": []
}";

            var assemblyName = _config.name.SanitizeFileName();
            WriteFile($"Editor/{assemblyName}.Editor.asmdef", templateEditor.Replace("{assemblyName}", $"{assemblyName}.Editor"));
            WriteFile($"Runtime/{assemblyName}.Runtime.asmdef", templateRuntime.Replace("{assemblyName}", $"{assemblyName}.Runtime"));
            WriteFile($"Tests/Editor/{assemblyName}.Editor.Tests.asmdef", templateEditor.Replace("{assemblyName}", $"{assemblyName}.Editor.Tests"));
            WriteFile($"Tests/Runtime/{assemblyName}.Runtime.Tests.asmdef", templateRuntime.Replace("{assemblyName}", $"{assemblyName}.Runtime.Tests"));
        }

        private void WriteFile(string relativePath, string content)
        {
            var fullPath = Path.Combine(_rootPath, relativePath);
            File.WriteAllText(fullPath, content);
        }
    }

    public static class StringExtensions
    {
        public static string SanitizeFileName(this string name)
        {
            var invalidChars = Path.GetInvalidFileNameChars();
            return new string(name
                .Where(c => !invalidChars.Contains(c))
                .ToArray()).Replace(" ", ".");
        }
    }
}