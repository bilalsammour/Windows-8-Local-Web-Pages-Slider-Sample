using LocalWebPagesSlider.Common;
using LocalWebPagesSlider.DataModel;
using System;
using System.Threading.Tasks;
using Windows.Storage;

namespace LocalWebPagesSlider
{
    public static class DataLoader
    {
        private const string DEFAULT_VIEWMODEL_KEY = "WebPages";
        private const string FOLDER_NAME = "Html";

        public static StreamUriWinRTResolver WebStream { get; set; }

        public static async Task Start(ObservableDictionary defaultViewModel)
        {
            SampleDataSource sampleDataSource = await LoadData();
            await InitWebStream();

            defaultViewModel[DEFAULT_VIEWMODEL_KEY] = sampleDataSource;
        }

        private static async Task<SampleDataSource> LoadData()
        {
            SampleDataSource sampleDataSource = await JsonManager.DeserializeFromFile<SampleDataSource>("ms-appx:///DataModel/SampleData.json");

            return sampleDataSource;
        }

        private static async Task InitWebStream()
        {
            var folder = await GetHtnmlFolder();
            WebStream = new StreamUriWinRTResolver(folder);
        }

        private static async Task<StorageFolder> GetHtnmlFolder()
        {
            StorageFolder htmlFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync(FOLDER_NAME);

            return htmlFolder;
        }
    }
}
