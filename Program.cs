using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Microsoft.VisualBasic.FileIO;
 
namespace CSV_Converter
{
   internal class Program
   {
      static void Main(string[] args)
      {
         while (true)
            updateFile(userPrompt());
      }

      private static void updateFile(string fileName)
      {
         TextFieldParser parser = new TextFieldParser($"{Directory.GetCurrentDirectory()}{@"\CSV_Files"}" + "\\" + fileName + ".csv");
         parser.HasFieldsEnclosedInQuotes = true;
         parser.SetDelimiters(",");
         string[] properties;
         var listObjResult = new List<Dictionary<string, string>>();
         while (!parser.EndOfData)
         {
            properties = parser.ReadFields();
            properties[1] = properties[1].Trim('~');
            // create dictionary object & add to list
            var objResult = new Dictionary<string, string>();
            objResult.Add("Id", properties[0]);
            objResult.Add("Name", properties[1]);
            listObjResult.Add(objResult);
         }
         parser.Close();
         var json = JsonConvert.SerializeObject(listObjResult, Formatting.Indented);
         System.IO.File.WriteAllText($"{Directory.GetCurrentDirectory()}{@"\JSON_Files"}" + "\\" + fileName + ".json", json);
      }

      private static string userPrompt()
      {
         Console.WriteLine("Are we updating Agent or Station IDs? (Please type 'agent' or 'station')");
         return Console.ReadLine();
      }
   }
}
