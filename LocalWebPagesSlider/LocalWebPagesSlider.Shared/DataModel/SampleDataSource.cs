using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace LocalWebPagesSlider.DataModel
{
    [DataContract]
    public class SampleDataSource
    {
        public SampleDataSource()
        {
            WebPages = new ObservableCollection<WebPageItem>();
        }

        [DataMember(Name = "WebPages")]
        public ObservableCollection<WebPageItem> WebPages { get; private set; }
    }
}
