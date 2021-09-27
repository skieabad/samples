using Shiny.Nfc;
using System.Text;


namespace Sample
{
    public class NDefItemViewModel
    {
        readonly NDefRecord record;
        public NDefItemViewModel(NDefRecord record) => this.record = record;


        public string Type => this.record.PayloadType.ToString();
        public string Payload => this.record.PayloadType switch
        {
            NfcPayloadType.Uri => this.record.Uri,
            _ => Encoding.UTF8.GetString(this.record.Payload)
        };
    }
}
