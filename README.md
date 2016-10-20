# rBiblia xml2bibx converter

With this simple console tool you are able to convert translations stored in XML format into fully functional *.bibx files used by [rBiblia](http://rbiblia.toborek.info) application.

## Download

Compiled version can be downloaded from the [official homepage](http://api.toborek.info/pobierz/xml2bibx.exe).

## Requirements

* any Windows version with .NET 3.5/4 framework

## Compilation

```bash
msbuild.exe "xml2bibx/xml2bibx.csproj" /p:OutputPath=output;Config=Release
```

## Usage

```bash
xml2bibx.exe input.xml output.bibx
```

## Changelog:

### 1.0.0.0 (2016-10-20):
* initial open source release
