namespace Cake.Deploy.Variables
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Cake.Core;
    using Cake.Core.Annotations;

    [CakeAliasCategory("VariableManager")]
    public static class VariableManager
    {
        private static readonly Dictionary<string, VariableCollection> Environments = new Dictionary<string, VariableCollection>();

        [CakeMethodAlias]
        public static VariableCollection ReleaseEnvironment(this ICakeContext ctx, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (!Environments.ContainsKey(name))
            {
                Environments.Add(name, new VariableCollection());
            }

            return Environments[name];
        }

        [CakeMethodAlias]
        public static VariableCollection ReleaseVariable(this ICakeContext ctx)
        {
            const string argumentName = "env";

            if (!ctx.Arguments.HasArgument(argumentName))
            {
                throw new InvalidOperationException($"Environment not defined (\"{argumentName}\" argument not present).");
            }

            return Environments[ctx.Arguments.GetArgument(argumentName)];
        }

        [CakeMethodAlias]
        public static string ReleaseVariable(this ICakeContext ctx, string variableName)
        {
            const string argumentName = "env";

            if (!ctx.Arguments.HasArgument(argumentName))
            {
                throw new InvalidOperationException($"Environment not defined (\"{argumentName}\" argument not present).");
            }

            return Environments[ctx.Arguments.GetArgument(argumentName)][variableName];
        }

        [CakeMethodAlias]
        public static T ReleaseVariable<T>(this ICakeContext ctx, string variableName)
            where T : IConvertible
        {
            var value = ctx.ReleaseVariable(variableName);

            var isDecimalType = typeof(T) == typeof(decimal) || typeof(T) == typeof(float) || typeof(T) == typeof(double);
            if (isDecimalType && value.Contains(","))
            {
                value = value.Replace(",", ".");
            }

            if (typeof(Enum).IsAssignableFrom(typeof(T)))
            {
                var enumValue = (T)Enum.Parse(typeof(T), value);
                if (Enum.IsDefined(typeof(T), enumValue))
                {
                    return enumValue;
                }

                throw new InvalidOperationException($"Requested value '{enumValue}' was not found.");
            }

            return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
        }

        public static bool Exists(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            return Environments.ContainsKey(name);
        }

        public static VariableCollection GetEnvironment(string name)
        {
            if (!Exists(name))
            {
                throw new InvalidOperationException($"ReleaseEnvironment with the given name does not exist: {name}");
            }

            return Environments[name];
        }

        public static void Clear()
        {
            Environments.Clear();
        }

        public static void Clear(string name)
        {
            if (!Exists(name))
            {
                throw new InvalidOperationException($"ReleaseEnvironment with the given name does not exist: {name}");
            }

            Environments.Remove(name);
        }
    }
}
