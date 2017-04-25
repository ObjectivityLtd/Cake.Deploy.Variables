using System;
using System.Collections.Generic;

namespace Cake.Deploy.Variables
{
    public class VariableCollection
    {
        private readonly Dictionary<string, Func<VariableCollection, string>> variables = new Dictionary<string, Func<VariableCollection, string>>();

        private VariableCollection BaseCollection { get; set; }

        public string this[string name]
        {
            get
            {
                if (this.variables.ContainsKey(name))
                {
                    return  this.variables[name](this);
                }

                var keyInBaseCollection = this.BaseCollection?.Exists(name);

                if (keyInBaseCollection == true)
                {
                    return this.BaseCollection[name];
                }

                throw new KeyNotFoundException($"The given key was not present in the dictionary: {name}");
            }
        }

        public VariableCollection AddVariable(string name, string value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (this.variables.ContainsKey(name))
            {
                throw new InvalidOperationException($"Duplicat. Variable can be added only once. {name}");
            }

            this.variables.Add(name, x => value);

            return this;
        }

        public VariableCollection AddVariable(string name, Func<VariableCollection, string> expression)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException(nameof(name));
            }

            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (this.variables.ContainsKey(name))
            {
                throw new InvalidOperationException($"Duplicat. Variable can be added only once. {name}");
            }

            this.variables.Add(name, expression);

            return this;
        }

        public VariableCollection AddVariables(Dictionary<string, string> variables)
        {
            if (variables == null)
            {
                throw new ArgumentNullException(nameof(variables));
            }

            foreach (var variable in variables)
            {
                this.variables.Add(variable.Key, x => variable.Value);
            }

            return this;
        }

        public bool Exists(string name)
        {
            var keyInBaseCollection = this.BaseCollection?.Exists("name");

            return this.variables.ContainsKey(name) || (keyInBaseCollection == true);
        }

        public VariableCollection IsBasedOn(string baseEnvironment)
        {
            if (!VariableManager.Exists(baseEnvironment))
            {
                throw new InvalidOperationException($"ReleaseEnvironment with the given name is not defined: {baseEnvironment}");
            }

            this.BaseCollection = VariableManager.GetEnvironment(baseEnvironment);

            return this;
        }
    }
}
