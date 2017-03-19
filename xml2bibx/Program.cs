﻿/* For full copyright and license information view LICENSE file distributed with this source code. */

using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Reflection;
using System.Xml;
using System.Xml.Schema;

namespace xml2bibx
{
    class Program
    {
        /// <summary>
        /// Displays error message.
        /// </summary>
        /// <param name="errorMsg"></param>
        private static void showError(string errorMsg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(string.Format("[ERROR]: {0}", errorMsg));
            Console.ReadLine();
        }

        /// <summary>
        /// Formats input size.
        /// </summary>
        /// <param name="size"></param>
        /// <returns></returns>
        private static string formatSize(double size)
        {
            string[] units = { "B", "KB", "MB", "GB" };
            int order = 0;
            while (size >= 1024 & order + 1 < units.Length)
            {
                order++;
                size = size / 1024;
            }
            return String.Format("{0:0.##} {1}", size, units[order]);
        }

        static void Main(string[] args)
        {
            ConsoleColor defaultForegroundColor = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(String.Format(
                "rBiblia xml2bibx converter, ver. {0}",
                Assembly.GetExecutingAssembly().GetName().Version.ToString()
            ));
            Console.WriteLine("http://rbiblia.toborek.info");
            Console.WriteLine("(c) 2013-2017 Rafał Toborek");
            Console.ForegroundColor = defaultForegroundColor;

            Console.WriteLine();
            Console.WriteLine("Sources are available at https://github.com/rBiblia/xml2bibx");
            Console.WriteLine();
            Console.WriteLine("Usage: xml2bibx.exe input.xml output.bibx");
            Console.WriteLine();

            if (args.Length < 2)
            {
                showError("not enought arguments provided");

                return;
            }

            if (!File.Exists(args[0]))
            {
                showError(string.Format("file {0} not found", args[0]));

                return;
            }

            if (!isValid(args[0]))
            {
                showError("XML validation failed");

                return;
            }

            try
            {
                byte[] xmlByteData = File.ReadAllBytes(args[0]);
                double srcSize = xmlByteData.Length;
                MemoryStream ms = new MemoryStream();
                GZipStream sw = new GZipStream(ms, CompressionMode.Compress);

                sw.Write(xmlByteData, 0, xmlByteData.Length);
                sw.Close();
                xmlByteData = ms.ToArray();

                StringBuilder sB = new StringBuilder(xmlByteData.Length);
                foreach (byte item in xmlByteData)
                {
                    sB.Append((char)item);
                }

                ms.Close();
                File.WriteAllBytes(args[1], xmlByteData);
                sw.Dispose();
                ms.Dispose();

                Console.WriteLine(string.Format(
                    "[OK]: {0} ({1}) => {2} ({3})",
                    args[0],
                    formatSize(srcSize),
                    args[1],
                    formatSize(xmlByteData.Length)
                    ));
            }
            catch (Exception e)
            {
                showError(string.Format("Exception: {0}", e.Message));
                Console.ForegroundColor = defaultForegroundColor;
            }
        }

        /// <summary>
        /// Perform XML validation using XSD schema.
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        private static bool isValid(string fileName)
        {
            XmlReaderSettings settings = new XmlReaderSettings();

            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;

            Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream("xml2bibx.Resources.bibx.xsd");
            XmlReader xsdReader = XmlReader.Create(stream);

            try
            {
                settings.Schemas.Add(null, xsdReader);
            }
            catch (XmlSchemaException e)
            {
                Console.WriteLine(
                    "Schema validation error ({0}:{1}): {2}",
                    e.LineNumber,
                    e.LinePosition,
                    e.Message
                );
                Console.WriteLine();

                return false;
            }

            bool result = true;

            settings.ValidationEventHandler += new ValidationEventHandler(delegate(Object sender, ValidationEventArgs args)
            {
                if (args.Severity == XmlSeverityType.Warning)
                {
                    Console.WriteLine(
                        "Warning ({0}:{1}): Matching schema not found, no validation occurred. {2}",
                        args.Exception.LineNumber,
                        args.Exception.LinePosition,
                        args.Message
                    );
                }
                else
                {
                    Console.WriteLine(
                        "XML validation error ({0}:{1}): {2}",
                        args.Exception.LineNumber,
                        args.Exception.LinePosition,
                        args.Message
                    );
                }

                result = false;
            });

            XmlReader reader = XmlReader.Create(fileName, settings);

            try
            {
                while (reader.Read()) ;
            }
            catch (XmlException e)
            {
                Console.WriteLine(
                    "XML validation error ({0}:{1}): {2}",
                    e.LineNumber,
                    e.LinePosition,
                    e.Message
                );

                result = false;
            }

            if (!result)
            {
                Console.WriteLine();
            }

            return result;
        }
    }
}