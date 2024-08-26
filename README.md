# rBiblia xml2bibx converter

With this straightforward console tool, you can convert translations stored in XML format into fully functional *.bibx files used by the [rBiblia](https://rbiblia.toborek.info) application. The XML validation process occurs before the conversion.

## Download

Compiled version can be downloaded from the [GitHub Releases page](https://github.com/rBiblia/xml2bibx/releases).

## Requirements

Any Windows version with the .NET 3.5 or 4 framework installed.

## Compilation

```bash
msbuild.exe "xml2bibx/xml2bibx.csproj" /p:OutputPath=output /p:Config=Release
```

## Usage

```bash
xml2bibx.exe input_translation_file.xml output_translation_file.bibx
```

## Contact details and donation

If you have any questions, please don't hesitate to contact the owner of this repository using the dedicated [contact form](https://kontakt.toborek.info). Please note that I can understand only Polish and English languages.

Please [support my work](https://rbiblia.toborek.info/donation/).

You can follow rBiblia on [Facebook](https://www.facebook.com/rBiblia) and [Telegram](https://t.me/rBiblia).

## Changelog:

### 1.8.0.0 (2021-05-20):
* verse data type changed to `Int32` from `byte`

### 1.7.0.0 (2021-05-14):
* fixed chapter index validation

### 1.6.0.0 (2021-05-04):
* added support for new apocrypha books: `3es`, `sn3`

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
