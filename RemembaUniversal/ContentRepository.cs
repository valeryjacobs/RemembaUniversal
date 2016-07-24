using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Storage;

namespace RemembaUniversal
{
    public class ContentRepository
    {
        public static bool IsConnected()
        {
            //return false;
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }

        public async Task ClearCache()
        {
            StorageFolder localFolder =
                ApplicationData.Current.LocalFolder;

            var files = await localFolder.GetFilesAsync();

            foreach (StorageFile file in files)
            {
                await file.DeleteAsync();
            }
        }


        public async Task<ObservableCollection<IContent>> GetContentItems()
        {
            var contentItems = new ObservableCollection<IContent>();

            contentItems.Add(new Content
            {
                Id = Guid.NewGuid().ToString(),
                Data = "#Imported markdown\nFancy, huh?\n\n* Milk\n* Eggs\n* Salmon\n* Butter"
            });

            return contentItems;
        }

        public async Task<IContent> GetContent(string contentId)
        {
            if (contentId == null || contentId == "") contentId = Guid.NewGuid().ToString();

            string data = null;

            StorageFolder localFolder =
                  ApplicationData.Current.LocalFolder;
            if (await StorageHelper.DoesFileExistAsync
                (contentId, localFolder))
            {
                //use cached version
                StorageFile file =
                    await localFolder.GetFileAsync(contentId);
                data = await FileIO.ReadTextAsync(file);

                return new Content
                {
                    Id = contentId,
                    Data = data
                };
            }
            else //download and store now
            {
                if (IsConnected())
                {
                    CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantContentContainerSaS));

                    bool exists = await container.GetBlockBlobReference(contentId).ExistsAsync();

                    if (exists)
                    {
                        CloudBlockBlob blob = container.GetBlockBlobReference(contentId);
                        data = await blob.DownloadTextAsync();
                    }

                    if (data == null) data = "";

                    StorageFile storageFile = await localFolder.CreateFileAsync(contentId, CreationCollisionOption.ReplaceExisting);
                    await FileIO.WriteTextAsync(storageFile, data);

                    return new Content
                    {
                        Id = contentId,
                        Data = data
                    };
                }
                else
                {
                    return new Content
                    {
                        Id = contentId,
                        Data = data
                    };
                }
            }
        }





        public async Task AddContent(IContent content)
        {
            if (content.Data == null) return;

            if (IsConnected())
            {

                CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantContentContainerSaS));
                CloudBlockBlob blob = container.GetBlockBlobReference(content.Id);

                await blob.UploadTextAsync(content.Data);
            }

            StorageFolder localFolder = ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.CreateFileAsync(content.Id, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, content.Data);
        }

        public async Task UpdateContent(IContent content)
        {
            if (content.Data == null) return;

            if (content.Id == "1" || content.Id == "2")
            {
                content.Id = Guid.NewGuid().ToString();
                await AddContent(content);
            }
            else
            {
                if (IsConnected())
                {
                    CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantContentContainerSaS));
                    CloudBlockBlob blob = container.GetBlockBlobReference(content.Id);

                    await blob.UploadTextAsync(content.Data);
                }

                StorageFolder localFolder = ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync(content.Id, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(storageFile, content.Data);
            }
        }

        public async Task DeleteContent(string id)
        {
            StorageFolder localFolder =
               ApplicationData.Current.LocalFolder;
            //use cached version
            StorageFile file =
                await localFolder.GetFileAsync(id);

            await file.DeleteAsync();

            if (IsConnected())
            {
                CloudBlobContainer backupContainer = new CloudBlobContainer(new Uri(Settings.TenantBackupContainerSaS));
                CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantContentContainerSaS));

                bool existsInCloud = await container.GetBlockBlobReference(id).ExistsAsync();
                if (existsInCloud)
                {

                    CloudBlockBlob sourceBlob = container.GetBlockBlobReference(id);
                    CloudBlockBlob targetBlob = backupContainer.GetBlockBlobReference(id);
                    await targetBlob.StartCopyFromBlobAsync(sourceBlob);

                    await sourceBlob.DeleteAsync();
                }
            }
        }

        public async Task<string> DownloadContent(string contentId)
        {
            string stemp = "";

            if (IsConnected())
            {
                CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantContentContainerSaS));
                CloudBlockBlob blob = container.GetBlockBlobReference(contentId);

                // Get reference to the file in blob storage
                CloudBlockBlob blobFromSASCredential = container.GetBlockBlobReference(contentId);

                stemp = await blobFromSASCredential.DownloadTextAsync();

                //store the JSON file from Blob storage to Windows local Storage
                StorageFolder folder = ApplicationData.Current.LocalFolder;
                StorageFile file = await folder.CreateFileAsync(contentId,
                    CreationCollisionOption.ReplaceExisting);
                Stream outStream = await file.OpenStreamForWriteAsync();

                await blobFromSASCredential.DownloadToStreamAsync(outStream.AsOutputStream());
            }


            return stemp;
        }
    }
}
