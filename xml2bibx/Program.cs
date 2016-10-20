/* For full copyright and license information view LICENSE file distributed with this source code. */

using System;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Reflection;

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
            Console.WriteLine("(c) 2013-2016 Rafał Toborek");
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

            try
            {
                byte[] xml = File.ReadAllBytes(args[0]);
                double srcSize = xml.Length;
                MemoryStream ms = new MemoryStream();
                GZipStream sw = new GZipStream(ms, CompressionMode.Compress);

                sw.Write(xml, 0, xml.Length);
                sw.Close();
                xml = ms.ToArray();

                StringBuilder sB = new StringBuilder(xml.Length);
                foreach (byte item in xml)
                {
                    sB.Append((char)item);
                }

                ms.Close();
                File.WriteAllBytes(args[1], xml);
                sw.Dispose();
                ms.Dispose();

                Console.WriteLine(string.Format(
                    "[OK]: {0} ({1}) => {2} ({3})",
                    args[0],
                    formatSize(srcSize),
                    args[1],
                    formatSize(xml.Length)
                    ));
            }
            catch (Exception e)
            {
                showError(string.Format("Exception: {0}", e.Message));
                Console.ForegroundColor = defaultForegroundColor;
            }
        }
    }
}
