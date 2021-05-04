# rBiblia xml2bibx converter

With this simple console tool you can convert translations stored in XML format into fully functional *.bibx files used by [rBiblia](https://rbiblia.toborek.info) application. XML validation process is done before the conversion.

## Download

Compiled version can be downloaded from the [official location](https://api.toborek.info/download/xml2bibx.exe).

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

## Contact details and donation

If you have a question, please do not hesitate to contact owner of this repo using dedicated [contact form](https://kontakt.toborek.info) (please, keep in mind that I can understand only Polish and English language).

Please [support my work](https://rbiblia.toborek.info/donation/).

You can follow rBiblia on [Facebook](https://www.facebook.com/rBiblia) and [Telegram](https://t.me/rBiblia).

## Changelog:

### 1.6.0.0 (2021-05-04):
* added support for new apocrypha books: 3es, sn3

### 1.5.0.0 (2021-03-13):
* added XML attribute identifier validation

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
