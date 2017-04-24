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

        public VariableCollection Variables { get; } = new VariableCollection();

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

            if (this.Variables.Exists(name))
            {
                throw new InvalidOperationException($"Duplicat. Variable can be added only once. {name}");
            }

            this.Variables.Add(name, value);

            return this;
        }

        public Environment AddVariable(string name, Func<VariableCollection, string> expression)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (this.Variables.Exists(name))
            {
                throw new InvalidOperationException($"Duplicat. Variable can be added only once. {name}");
            }

            this.Variables.Add(name, expression);

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
                this.Variables.Add(variable.Key, variable.Value);
            }

            return this;
        }

        public Environment IsBasedOn(Environment baseEnvironment)
        {
            this.Variables.BaseCollection = baseEnvironment.Variables;

            return this;
        }
    }
}
