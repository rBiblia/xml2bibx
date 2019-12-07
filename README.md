# rBiblia xml2bibx converter

With this simple console tool you can convert translations stored in XML format into fully functional *.bibx files used by [rBiblia](https://rbiblia.toborek.info) application. XML validation process is performed before the conversion.

## Download

Compiled version can be downloaded from the [official location](https://api.toborek.info/pobierz/xml2bibx.exe).

## Requirements

* any Windows version with .NET 3.5/4 framework installed

## Compilation

```bash
msbuild.exe "xml2bibx/xml2bibx.csproj" /p:OutputPath=output;Config=Release
```

## Usage

```bash
xml2bibx.exe input.xml output.bibx
```

## Changelog:

### 1.4.0.0 (2019-12-07):
* added `notice` tag to XSD schema
* small improvements

### 1.3.0.0 (2017-03-23):
* added book id validation in XSD schema

### 1.2.0.0 (2017-03-21):
* added `shortname` tag to XSD schema

### 1.1.0.0 (2017-03-20):
* added XML pre-validation process based on the included XSD schema
* XML is now truncated before compression (all white spaces will be removed)
* improved status messages when processing file

### 1.0.0.0 (2016-10-20):
* initial open source release
