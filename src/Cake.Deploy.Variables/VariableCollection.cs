using System;
using System.Collections.Generic;

namespace Cake.Deploy.Variables
{
    public class VariableCollection
    {
        private readonly Dictionary<string, Func<VariableCollection, string>> variables = new Dictionary<string, Func<VariableCollection, string>>();

        public VariableCollection BaseCollection { get; set; }

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

        public void Add(string name, string value)
        {
            this.variables.Add(name, x => value);
        }

        public void Add(string name, Func<VariableCollection, string> expression)
        {
            this.variables.Add(name, expression);
        }

        public bool Exists(string name)
        {
            var keyInBaseCollection = this.BaseCollection?.Exists("name");

            return this.variables.ContainsKey(name) || (keyInBaseCollection == true);
        }
    }
}
