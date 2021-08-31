namespace CMP332.Models
{
    public class Role : ModelBase
    {
        public string Name { get; set; }

        public Role()
        {
            
        }

        public Role(string _name)
        {
            Name = _name;
        }
    }
}