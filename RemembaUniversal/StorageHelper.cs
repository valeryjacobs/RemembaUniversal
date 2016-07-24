using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RemembaUniversal
{
    public class StorageHelper
    {
        //The actual file retrieval logic
        public async static Task<bool> DoesFileExistAsync(string fileName, StorageFolder folder)
        {
            try
            {
                var fileList = await folder.GetFilesAsync();

                return fileList.Count(x => x.Name == fileName) > 0;
            }
            catch
            {
                return false;
            }
        }

        public async static Task ClearCache(StorageFolder folder)
        {
            var files = await folder.GetFilesAsync();
            foreach (var file in files)
            {
                await file.DeleteAsync(StorageDeleteOption.PermanentDelete);
            }
        }
    }
}
