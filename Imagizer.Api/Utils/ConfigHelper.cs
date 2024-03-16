using Imagizer.Api.Exceptions;

namespace Imagizer.Api.Utils;

public static class ConfigHelper
{
    public static string GetVariable(string key, IConfiguration config)
    {
        return config.GetValue<string>(key) ??
               Environment.GetEnvironmentVariable(ConvertToEnvFormat(key)) ??
               throw new NotFoundException($"{key} not found");
    }

    private static string ConvertToEnvFormat(string key)
    {
        var keyArray = key.Split(':').ToList();

        var output = string.Join("_", keyArray);

        return output;
    }
}