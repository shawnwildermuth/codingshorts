using static System.Console;
using System.Data;
using System.Reflection;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

var assembly = typeof(DbContext).Assembly;
var simpleVersion = assembly.GetName().Version;
var name = assembly.ManifestModule.Name;
var attr = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
var version = attr?.InformationalVersion;

WriteLine($"EF Version: {simpleVersion} - {version}");