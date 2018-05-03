using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System;
using System.Globalization;

namespace IO
{
    /// <summary>
    /// CSV File reader.
    /// Source: https://bravenewmethod.com/2014/09/13/lightweight-csv-reader-for-unity/
    /// </summary>
    public class CSVFileReader
    {
        private static string split_regex = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))";
        private static string line_split_regex = @"\r\n|\n\r|\n|\r";
        private static char[] trim_characters = { '\"' };
        private static string commentCharacter = "#";

        public CSVFileReader() { }

        /// <summary>
        /// Read the csv file specified by path into a list of dictionaries.
        /// </summary>
        /// <returns>The read data as a list of dictionaries.</returns>
        /// <param name="path">The path of the csv file.</param>
        public List<Dictionary<string, object>> Read(string path)
        {
            string csv_string = File.ReadAllText(path);
            return ParseCSV(csv_string);
        }

        private List<Dictionary<string, object>> ParseCSV(string fileContent)
        {
            var data = new List<Dictionary<string, object>>();

            string[] lines = ReadLines(fileContent);

            lines = RemoveCommentLines(lines);

            if (IsEmptyFile(lines)) return data;

            string[] header = ReadHeader(lines);

            Dictionary<string, object> entry;
            for (var i = 1; i < lines.Length; i++)
            {
                entry = ParseLine(lines[i], header);

                //If the line was empty contine
                if (entry.Count == 0) continue;

                data.Add(entry);
            }
            return data;
        }

        private string[] RemoveCommentLines(string[] lines)
        {
            List<string> content = new List<string>(lines.Length);

            foreach (string line in lines)
            {
                if (!IsCommentLine(line)) content.Add(line);
            }

            return content.ToArray();
        }

        private bool IsCommentLine(string line)
        {
            return line.StartsWith(commentCharacter, StringComparison.Ordinal);
        }

        private bool IsEmptyFile(string[] lines)
        {
            return lines.Length <= 1;
        }

        private string[] ReadLines(string fileContent)
        {
            return Regex.Split(fileContent, line_split_regex);
        }

        private string[] ReadHeader(string[] lines)
        {
            return Regex.Split(lines[0], split_regex);
        }

        private Dictionary<string, object> ParseLine(string line, string[] header)
        {
            Dictionary<string, object> entry = new Dictionary<string, object>();

            string[] values = Regex.Split(line, split_regex);
            if (IsEmptyLine(values)) return entry;

            string value;
            for (int j = 0; j < header.Length && j < values.Length; j++)
            {
                value = TrimValue(values[j]);

                entry[header[j]] = TryeParseValue(value);
            }
            return entry;
        }

        private bool IsEmptyLine(string[] values)
        {
            return (values.Length == 0 || values[0] == "");
        }

        private string TrimValue(string value)
        {
            return value.TrimStart(trim_characters).TrimEnd(trim_characters).Replace("\\", "");
        }

        private object TryeParseValue(string value)
        {
            int n; float f;

            if (int.TryParse(value, out n)) return (object)n;

            if (float.TryParse(value, out f)) return (object)f;

            return (object)value;
        }

    }
}