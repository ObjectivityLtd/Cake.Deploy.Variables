namespace Cake.Deploy.Variables
{
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Annotations;

    [CakeAliasCategory("VariableManager")]
    public static class VariableManager
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
    }
}
