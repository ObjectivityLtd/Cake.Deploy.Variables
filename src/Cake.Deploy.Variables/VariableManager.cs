using System.Globalization;
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
            const string argumentName = "env";

            if (!ctx.Arguments.HasArgument(argumentName))
            {
                throw new InvalidOperationException($"Environment not defined (\"{argumentName}\" argument not present).");
            }

            return environments[ctx.Arguments.GetArgument(argumentName)];
        }

        [CakeMethodAlias]
        public static string ReleaseVariable(this ICakeContext ctx, string variableName)
        {
            const string argumentName = "env";

            if (!ctx.Arguments.HasArgument(argumentName))
            {
                throw new InvalidOperationException($"Environment not defined (\"{argumentName}\" argument not present).");
            }

            return environments[ctx.Arguments.GetArgument(argumentName)][variableName];
        }

        [CakeMethodAlias]
        public static T ReleaseVariable<T>(this ICakeContext ctx, string variableName) where T : IConvertible
        {
            var value = ctx.ReleaseVariable(variableName);

            var isDecimalType = typeof(T) == typeof(decimal) || typeof(T) == typeof(float) || typeof(T) == typeof(double);
            if (isDecimalType && value.Contains(","))
            {
                value = value.Replace(",", ".");
            }

            return (T) Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
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
