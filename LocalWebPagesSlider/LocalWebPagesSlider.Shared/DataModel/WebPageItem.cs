using System.Runtime.Serialization;

namespace LocalWebPagesSlider.DataModel
{
    [DataContract]
    public class WebPageItem
    {
        [DataMember(Name = "Title")]
        public string Title { get; private set; }

        [DataMember(Name = "FileName")]
        public string FileName { get; private set; }
    }
}
