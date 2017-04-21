namespace Cake.Deploy.Variables
{
    using System;
    using System.Collections.Generic;

    public class Environment 
    {
        public Environment(string name)
        {
            this.Name = name;
        }

        public string Name { get; }

        public Dictionary<string, string> Variables { get; } = new Dictionary<string, string>();

        public Environment AddVariable(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (this.Variables.ContainsKey(name))
            {
                throw new InvalidOperationException($"Duplicat. Variable can be added only once. {name}");
            }

            this.Variables.Add(name, value);

            return this;
        }

        public Environment AddVariables(Dictionary<string, string> variables)
        {
            if (variables == null)
            {
                throw new ArgumentNullException(nameof(variables));
            }

            foreach (var variable in variables)
            {
                this.AddVariable(variable.Key, variable.Value);
            }

            return this;
        }
    }
}
