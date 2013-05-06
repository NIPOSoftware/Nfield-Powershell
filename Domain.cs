using Nfield.Infrastructure;

namespace Nfield.PowerShell
{
    public class Domain
    {
        public string Name { get; set; }

        internal INfieldConnection Connection { get; set; }
    }
}
