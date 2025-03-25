using System.Reflection;

[assembly: AssemblyVersion("1.0.0")]
[assembly: AssemblyCompany("Lleoliad")]
[assembly: AssemblyCopyright("Copyright © 2025 Lleoliad")]
[assembly: AssemblyDescription("GhysX Framework Plugin Generator is a tool to extend the Unity Editor, capable of automatically generating a development environment for a Unity plugin. This tool generates the necessary file structure and configuration files based on the plugin's basic information, helping developers quickly set up a plugin development environment.")]

#if BESTHTTP_WITH_BURST
[assembly: Unity.Burst.BurstCompile(CompileSynchronously = true, OptimizeFor = Unity.Burst.OptimizeFor.Performance)]
#endif
