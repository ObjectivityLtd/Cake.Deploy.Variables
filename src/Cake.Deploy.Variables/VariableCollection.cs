namespace Cake.Deploy.Variables
{
    using System;
    using System.Collections.Generic;

    public class VariableCollection
    {
        private readonly Dictionary<string, Func<VariableCollection, string>> variables = new Dictionary<string, Func<VariableCollection, string>>();

        private VariableCollection BaseCollection { get; set; }

        private Func<VariableCollection, string> GetVariableExpression(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if (this.variables.ContainsKey(name))
            {
                return this.variables[name];
            }

            if (this.BaseCollection != null)
            {
                return this.BaseCollection.GetVariableExpression(name);
            }

            throw new KeyNotFoundException($"Key with the given name not found: {name}");
        }

        public string this[string name]
        {
            get
            {
                if (string.IsNullOrWhiteSpace(name))
                {
                    throw new ArgumentNullException(nameof(name));
                }

                var expression = this.GetVariableExpression(name);

                return expression(this);
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

        public VariableCollection SetVariable(string name, string value)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }

            if(this.variables.ContainsKey(name))
            {
                this.variables.Remove(name);
            }

            return this.AddVariable(name, value);
        }
    }
}
