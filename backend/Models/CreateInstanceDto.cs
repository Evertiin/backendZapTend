using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend.Models
{
    public class CreateInstanceDto
    {
        public string instanceName { get; set; }
        public bool qrCode { get; set; }
        public string integration { get; set; }
    }
    

}
