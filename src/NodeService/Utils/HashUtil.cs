using System;
using System.Text;
using System.Text.Json;

namespace NodeService
{
    public static class HashUtil
    {
        public static string GetHash(object obj)
        {
            var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(obj);
            var builder = new StringBuilder("0x");
            foreach (var item in jsonBytes) builder.Append(item.ToString("X2"));
            return builder.ToString();
        }
    }
}