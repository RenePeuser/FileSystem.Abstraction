# FileSystem.Abstraction

A simple library to get an abstraction to FileInfo and DirectoryInfo. As an addition you had for each object a service which you can use for better coding and handle accessing to the IO in a better way.

```
!! This poject is still in development !!
```

## Prerequisites
* .NET Standard 2.0 compatible projects

## Install package

```bash
dotnet add package FileSystem.Abstraction
```

```bash
dotnet add package FileSystem.Abstraction --version x.y.z
```

![](./assets/pack-manager.png)

## DependencyInjection
If you use any DI mechanism, you can register the File- and DirectoryService at your DI Container
```csharp
services.AddSingleton<IFileService, FileService>();
services.AddSingleton<IDirectoryService, DirectoryService>();
```

## Usage for a file

The file service returns an IFileInfo which wrapps the FileInfo away.
### Build a instance of the file service
```csharp
var fileService = new FileService();
```

### Get a file from a string path
```csharp
var fileInfo = fileService.GetFileInfo();
```

### File exists
```csharp
var fileInfo = fileService.GetFileInfo();
if(fileInfo.Exists)
{
    ...
}

if(fileInfo.NoExists)
{
    ...
}
```

## Usage for a folder

The file service returns an IFileInfo which wrapps the FileInfo away.
### Build a instance of the file service
```csharp
var fileService = new FolderService();
```

### Get a file from a string path
```csharp
var fileInfo = fileService.GetFileInfo();
```

### File exists
```csharp
var fileInfo = fileService.GetFileInfo();
if(fileInfo.Exists)
{
    ...
}

if(fileInfo.NotExists)
{
    ...
}
```

## Usage for a directory

The directory service returns an IDirectoryInfo which wrapps the DirectoryInfo away.
### Build a instance of the directory service
```csharp
var directoryService = new DirectoryService();
```

### Get a directory from a string path
```csharp
var directoryInfo = directoryService.GetDirectoryInfo();
```

### Directory exists
```csharp
var directoryInfo = directoryService.GetDirectoryInfo();
if(directoryInfo.Exists)
{
    ...
}

if(directoryInfo.NotExists)
{
    ...
}
```