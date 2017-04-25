using System.Runtime.CompilerServices;

namespace Cake.Deploy.Variables
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Annotations;

    [CakeAliasCategory("VariableManager")]
    public static class VariableManager
    {
        private static readonly Dictionary<string, VariableCollection> environments = new Dictionary<string, VariableCollection>();

        [CakeMethodAlias]
        public static VariableCollection ReleaseEnvironment(this ICakeContext ctx, string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!environments.ContainsKey(name))
            {
                environments.Add(name, new VariableCollection());
            }

            return environments[name];
        }

        [CakeMethodAlias]
        public static VariableCollection ReleaseVariable(this ICakeContext ctx)
        {
            var environmentVariableName = "env";

            if (!ctx.Environment.GetEnvironmentVariables().ContainsKey(environmentVariableName))
            {
                throw new InvalidOperationException("Environment variable \"env\" not defined.");
            }

            return environments[(ctx.Environment.GetEnvironmentVariable("env"))];
        }

        public static bool Exists(string name)
        {
            if (String.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return environments.ContainsKey(name);
        }

        public static VariableCollection GetEnvironment(string name)
        {
            if (!environments.ContainsKey(name))
            {
                throw new InvalidOperationException($"ReleaseEnvironment with the given name does not exist: {name}");
            }

            return environments[name];
        }

        public static void Clear()
        {
            environments.Clear();
        }
    }
}
