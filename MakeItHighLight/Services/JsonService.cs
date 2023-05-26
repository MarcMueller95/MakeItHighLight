using MakeItHighLight.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MakeItHighLight.Services
{
    public class JsonService
    {
        public static async Task SaveJsonFile(Settings settings)
        {
            string fileName = "Settings.json";
            using FileStream createStream = File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, settings);
            await createStream.DisposeAsync();
        }
        public static Settings ReadJsonFile()
        {
            string fileName = "Settings.json";
            try
            {
                using FileStream openStream = File.OpenRead(fileName);
                Settings? settings =
                    JsonSerializer.Deserialize<Settings>(openStream);
                return settings;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
