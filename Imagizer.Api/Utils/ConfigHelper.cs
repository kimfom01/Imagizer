using Imagizer.Api.Exceptions;

namespace Imagizer.Api.Utils;

public static class ConfigHelper
{
    public static string GetVariable(string key, IConfiguration config)
    {
        var variable = config.GetValue<string>(key);

        if (string.IsNullOrWhiteSpace(variable))
        {
            variable = Environment.GetEnvironmentVariable(ConvertToEnvFormat(key));
        }

        if (string.IsNullOrWhiteSpace(variable))
        {
            throw new NotFoundException($"{key} not found");
        }

        return variable;
    }

    private static string ConvertToEnvFormat(string key)
    {
        var envKey = key.Replace(':', '_');

        return envKey;
    }
}