using Microsoft.Extensions.Configuration;
using NewFileCopier;
using System.Runtime.CompilerServices;

NewFileCopierConfig _config;
List<FileSystemWatcher> _watchers;

Initialise();

JoinTheWatch();

void JoinTheWatch()
{
    foreach (var mapping in _config.Mappings)
    {
        FileSystemWatcher watcher = new FileSystemWatcher(mapping.SourcePath);
        watcher.Created += new FileSystemEventHandler((s, e) => OnCreated(s, e, mapping));
        watcher.EnableRaisingEvents = true;
        _watchers.Add(watcher);
    }

    Console.WriteLine("Always watching, Wazowski.");
    Console.ReadLine();
}

static void OnCreated(object sender, FileSystemEventArgs e, Mapping mapping)
{
    Console.WriteLine($"Created: {e.FullPath}. Waiting for {mapping.DelayBeforeCopy}s...");
    Thread.Sleep(mapping.DelayBeforeCopy * 1000);
    string destination = Path.Combine(mapping.DestinationPath, e.Name ?? Path.GetFileName(e.FullPath));
    Console.WriteLine($"Copying to {destination}...");
    File.Copy(e.FullPath, destination);
    Console.WriteLine("Done");
}

void Initialise()
{
    string configFilename = "appSettings.json";
    IConfigurationBuilder builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(configFilename);

    IConfiguration configRoot = builder.Build();

    _config = new();
    configRoot.Bind(_config);

    _watchers = new List<FileSystemWatcher>();
}