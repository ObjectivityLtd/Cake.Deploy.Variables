using System.Runtime.CompilerServices;

namespace Cake.Deploy.Variables
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Annotations;

    [CakeAliasCategory("EnvironmentManager")]
    public static class EnvironmentManager
    {
        private static readonly Dictionary<string, Environment> environments = new Dictionary<string, Environment>();

        [CakeMethodAlias]
        public static Environment Environment(this ICakeContext ctx, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!environments.ContainsKey(name))
            {
                environments.Add(name, new Environment(name));
            }

            return environments[name];
        }

        [CakeMethodAlias]
        public static Environment Environment(this ICakeContext ctx)
        {
            var environmentVariableName = "env";

            if (ctx.Environment.GetEnvironmentVariables().ContainsKey(environmentVariableName))
            {
                throw new InvalidOperationException("Can not use DeploymentVariables. Environment variable \"env\" not defined.");
            }

            return environments[(ctx.Environment.GetEnvironmentVariable("env"))];
        }

        public static bool Exists(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return environments.ContainsKey(name);
        }

        public static Environment GetEnvironment(string name)
        {
            if (!environments.ContainsKey(name))
            {
                throw new InvalidOperationException($"Environment with the given name does not exist: {name}");
            }

            return environments[name];
        }
    }
}
