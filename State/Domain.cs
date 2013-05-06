using Nfield.Infrastructure;

namespace Nfield.PowerShell.State
{
    public class Domain
    {
        public string Name { get; set; }

        internal INfieldConnection Connection { get; set; }
    }
}
