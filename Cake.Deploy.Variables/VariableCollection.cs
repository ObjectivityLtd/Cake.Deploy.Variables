using System.Collections.Generic;

namespace Cake.Deploy.Variables
{
    public class VariableCollection
    {
        private readonly Dictionary<string, string> variables = new Dictionary<string, string>();

        private readonly VariableCollection baseVariables;

        public VariableCollection()
        {

        }

        public VariableCollection(VariableCollection baseCollection)
        {
            this.baseVariables = baseCollection;
        }
    }
}
