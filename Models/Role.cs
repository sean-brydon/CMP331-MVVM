namespace CMP332.Models
{
    public class Role : ModelBase
    {
        public string Name { get; set; }

        public Role()
        {
            
        }

        public Role(int id, string _name)
        {
            Id = id;
            Name = _name;
        }
    }
}