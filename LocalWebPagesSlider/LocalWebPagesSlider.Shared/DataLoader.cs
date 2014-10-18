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

        public async Task Start(ObservableDictionary defaultViewModel)
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

        private async Task InitWebStream()
        {
            var folder = await GetHtnmlFolder();
            WebStream = new StreamUriWinRTResolver(folder);
        }

        public async Task<StorageFolder> GetHtnmlFolder()
        {
            StorageFolder local = Windows.Storage.ApplicationData.Current.LocalFolder;

            StorageFolder htmlFolder = await local.CreateFolderAsync(FOLDER_NAME, CreationCollisionOption.OpenIfExists);

            return htmlFolder;
        }
    }
}
