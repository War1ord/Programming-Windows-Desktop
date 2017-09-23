using System.Collections.Generic;

namespace Models
{
    public class CommandsGroup
    {
        public CommandsGroup(){}
        public CommandsGroup(string name)
        {
            Name = name;
        }

        public CommandsGroup(string name, List<CommandParameter> list)
        {
            Name = name;
            List = list;
        }

        public string Name { get; set; }
        public List<CommandParameter> List { get; set; }
        public string SelectedItem { get; set; }
    }
}
