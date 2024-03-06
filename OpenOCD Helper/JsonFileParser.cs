using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Encodings.Web;


namespace OpenOCD_Helper
{
    public class JsonFileParser
    {
        public static void Serialize<T>(string filePath, T data) where T : class
        {
            try
            {
                var jsonString = JsonSerializer.Serialize<T>(data, new JsonSerializerOptions()
                {
                    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                    WriteIndented = true
                });
                File.WriteAllText(filePath, jsonString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error serialiaze: {ex.Message}");
            }

        }

        public static T Deserialize<T>(string filePath) where T : class
        {
            string fileString = "";
            T? result = null;
            try
            {
                fileString = File.ReadAllText(filePath);

                result = JsonSerializer.Deserialize<T>(fileString);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deserialiaze: {ex.Message}");
            }

            return result;
        }

    }
}
