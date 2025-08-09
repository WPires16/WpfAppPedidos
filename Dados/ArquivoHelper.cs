using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace WpfAppPedidos.Dados
{
    public static class ArquivoHelper<T> where T : class
    {
        private static string GetFileName()
        {
            return $"{typeof(T).Name}.json";
        }

        public static List<T> Load()
        {
            var file = GetFileName();
            if (!File.Exists(file))
                return new List<T>();

            var json = File.ReadAllText(file);
            return JsonConvert.DeserializeObject<List<T>>(json) ?? new List<T>();
        }

        public static void Save(List<T> lista)
        {
            var file = GetFileName();
            var json = JsonConvert.SerializeObject(lista, Formatting.Indented);
            File.WriteAllText(file, json);
        }
    }
}