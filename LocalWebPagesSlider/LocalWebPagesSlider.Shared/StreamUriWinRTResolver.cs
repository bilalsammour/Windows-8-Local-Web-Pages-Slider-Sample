using System;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.Web;

namespace LocalWebPagesSlider
{
    public class StreamUriWinRTResolver : IUriToStreamResolver
    {
        public static string WEBVIEW_TAG = "WEBVIEW_TAG";

        private StorageFolder baseFolder;

        public StreamUriWinRTResolver(StorageFolder baseFolder)
        {
            this.baseFolder = baseFolder;
        }

        /// <summary>
        /// The entry point for resolving a Uri to a stream.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        public IAsyncOperation<IInputStream> UriToStreamAsync(Uri uri)
        {
            if (uri == null)
            {
                throw new Exception();
            }
            string path = uri.AbsolutePath;

            if (System.Diagnostics.Debugger.IsAttached)
            {
                System.Diagnostics.Debug.WriteLine(string.Format("Stream Requested: {0}", uri.AbsoluteUri));
            }

            // Because of the signature of the this method, it can't use await, so we 
            // call into a seperate helper method that can use the C# await pattern.
            return getContent(path).AsAsyncOperation();
        }

        /// <summary>
        /// Helper that cracks the path and resolves the Uri
        /// Uses the C# await pattern to coordinate async operations
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        private async Task<IInputStream> getContent(string path)
        {
            StorageFolder current = baseFolder;

            // Trim the initial '/' if applicable
            if (path.StartsWith("/")) path = path.Remove(0, 1);
            // Split the path into an array of nodes
            string[] nodes = path.Split('/');

            // Walk the nodes of the path checking against the filesystem along the way
            for (int i = 0; i < nodes.Length; i++)
            {
                try
                {
                    // Try and get the node from the file system
                    IStorageItem item = await current.GetItemAsync(nodes[i]);

                    if (item.IsOfType(StorageItemTypes.Folder) && i < nodes.Length - 1)
                    {
                        // If the item is a folder and isn't the leaf node
                        current = item as StorageFolder;
                    }
                    else if (item.IsOfType(StorageItemTypes.File) && i == nodes.Length - 1)
                    {
                        // If the item is a file and is the leaf node
                        StorageFile f = item as StorageFile;

                        IRandomAccessStream stream = await f.OpenAsync(FileAccessMode.Read);
                        return stream;
                    }
                    else
                    {
                        return null;
                        //Leaf is not a file, or the file isn't the leaf node in the path
                        throw new Exception("Invalid path");
                    }
                }
                catch (Exception) { throw new Exception("Invalid path"); }
            }
            return null;
        }
    }
}
