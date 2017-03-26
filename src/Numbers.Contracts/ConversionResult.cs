using System.Runtime.Serialization;

namespace Numbers.Contracts
{
    [DataContract]
    public class ConversionResult
    {
        [DataMember]
        public string Words { get; set; }

        [DataMember]
        public string Error { get; set; }
    }
}