# FileSystem.Abstraction

A simple library to access System.IO.FileInfo and System.IO.DirectoryInfo objects via an abstraction layer. File service and Directory service can be used for better coding and testing, they provide access to the .Net System.IO via dependency injection.
For convenience, some System.IO.File methods like ReadAllLines, WriteAllLines are added to the IFileInfo interface.

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

Using any DI mechanism, you can register the File- and DirectoryService at your DI Container
```csharp
services.AddSingleton<IFileService, FileService>();
services.AddSingleton<IDirectoryService, DirectoryService>();
```

## Accessing a file

The file service returns an IFileInfo instance that wrapps the System.IO.FileInfo object.

### Obtain an instance of the file service via dependency injection
```csharp
public MyClass(IFileService fileService);
```

### Get a file info object from a string path
```csharp
var fileInfo = fileService.GetFileInfo("C:\\file.txt");
```

### Check if file exists and access the content
```csharp
if (fileInfo.Exists)
{
    var content = fileInfo.ReadAllLines();
    ...
}

// or

if(fileInfo.NotExists)
{
    ...
}
```

## Accessing a directory

The directory service returns an IDirectoryInfo which wrapps the DirectoryInfo away.

### ### Obtain an instance of the directory service via dependency injection
```csharp
public MyClass(IDirectoryService directoryService);
```

### Get a directory info object from a string path
```csharp
var directoryInfo = directoryService.GetDirectoryInfo("C:\\MyFolder");
```

### Check if directory exists
```csharp
if(directoryInfo.Exists)
{
    ...
}

// or

if(directoryInfo.NotExists)
{
    ...
}
```