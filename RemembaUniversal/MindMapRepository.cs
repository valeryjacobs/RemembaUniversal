using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
using Windows.Storage;

namespace RemembaUniversal
{
    public class MindMapRepository
    {
        private const string CacheFile = "MindMapsListCache.dat";

        public static bool IsConnected()
        {
            //return false;
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
        public async Task<List<IMindMap>> ListMindMaps()
        {
            var mindMaps = new List<IMindMap>();

            if (IsConnected())
            {

                CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantGraphContainerSaS));

                BlobContinuationToken token = null;
                do
                {
                    BlobResultSegment results = await container.ListBlobsSegmentedAsync(null, true, BlobListingDetails.None, 1, token, null, null);

                    foreach (IListBlobItem blobItem in results.Results)
                    {
                        var blob = blobItem.Container.GetBlockBlobReference(blobItem.Uri.Segments[2]);
                        await blob.FetchAttributesAsync();
                        if (blob.Metadata.ContainsKey("Name"))
                        {
                            mindMaps.Add(new MindMap()
                            {
                                Name = blob.Metadata["Name"],
                                ContentUri = blobItem.Uri.AbsoluteUri,
                                Id = blob.Metadata["Id"]
                            });
                        }
                    }

                    token = results.ContinuationToken;
                }
                while (token != null);



                StorageFolder localFolder =
                    ApplicationData.Current.LocalFolder;
                StorageFile storageFile = await localFolder.CreateFileAsync(CacheFile, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(storageFile, JsonConvert.SerializeObject(mindMaps));
            }
            else
            {
                StorageFolder localFolder =
                 ApplicationData.Current.LocalFolder;
                StorageFile file =
                        await localFolder.GetFileAsync(CacheFile);
                var graphListSerialized = await FileIO.ReadTextAsync(file);

                var list = JsonConvert.DeserializeObject<List<MindMap>>(graphListSerialized);
                return list.ToList<IMindMap>();
            }

            return mindMaps;
        }



        public async Task SaveMindMap(INode rootNode, IMindMap mindMap)
        {
            if (rootNode == null || mindMap == null) return;

            JsonNode jsonNode = new JsonNode();
            JsonNode jsonObject = BuildJSON(jsonNode, rootNode);

            mindMap.Content = JsonConvert.SerializeObject(jsonObject);
            mindMap.Touched = DateTime.Now.Ticks.ToString();
            var serializedGraph = JsonConvert.SerializeObject(mindMap);

            if (IsConnected())
            {
                CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantGraphContainerSaS));
                CloudBlockBlob blob = container.GetBlockBlobReference(mindMap.Id);
                await blob.FetchAttributesAsync();
                blob.Metadata["Name"] = mindMap.Name;
                blob.Metadata["Id"] = mindMap.Id;

                await blob.UploadTextAsync(serializedGraph);
                await blob.SetMetadataAsync();
            }

            StorageFolder localFolder =
                      ApplicationData.Current.LocalFolder;
            StorageFile storageFile = await localFolder.CreateFileAsync(mindMap.Id, CreationCollisionOption.ReplaceExisting);
            await FileIO.WriteTextAsync(storageFile, serializedGraph);
        }

        private JsonNode BuildJSON(JsonNode jsonNode, INode source)
        {
            if (source == null)
            {
                return null;
            }
            JsonNode node = new JsonNode
            {
                id = source.Id,
                cid = source.ContentId,
                d = source.Description,
                n = source.Title
            };
            if (source.Children != null)
            {
                foreach (INode node2 in source.Children)
                {
                    node.c.Add(BuildJSON(node, node2));
                }
            }
            return node;
        }


        public async Task<IMindMap> CreateGraph(string graphName)
        {
            StorageFolder localFolder =
                    ApplicationData.Current.LocalFolder;

            Node rootNode = new Node();
            rootNode.Id = Guid.NewGuid().ToString();
            rootNode.Title = graphName;
            rootNode.Description = "[Description]";

            JsonNode jsonNode = new JsonNode();
            JsonNode jsonObject = BuildJSON(jsonNode, rootNode);

            var newGraph = new MindMap
            {
                Content = JsonConvert.SerializeObject(jsonObject),
                Id = Guid.NewGuid().ToString(),
                Name = graphName
            };

            StorageFile storageFile = await localFolder.CreateFileAsync(newGraph.Id, CreationCollisionOption.ReplaceExisting);

            string serializedGraph = JsonConvert.SerializeObject(newGraph);

            await FileIO.WriteTextAsync(storageFile, serializedGraph);

            if (IsConnected())
            {
                CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantGraphContainerSaS));
                CloudBlockBlob blob = container.GetBlockBlobReference(newGraph.Id);
                blob.Metadata.Add("Name", graphName);
                blob.Metadata.Add("Id", newGraph.Id);

                await blob.UploadTextAsync(serializedGraph);
                await blob.SetMetadataAsync();
            }

            return newGraph;
        }

        public async Task Delete(string id)
        {
            StorageFolder localFolder =
                   ApplicationData.Current.LocalFolder;

            if (await StorageHelper.DoesFileExistAsync(id, localFolder))
            {
                StorageFile file =
              await localFolder.GetFileAsync(id);

                await file.DeleteAsync();
            }

            if (IsConnected())
            {
                CloudBlobContainer backupContainer = new CloudBlobContainer(new Uri(Settings.TenantBackupContainerSaS));
                CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantGraphContainerSaS));

                bool existsInCloud = await container.GetBlockBlobReference(id).ExistsAsync();
                if (existsInCloud)
                {

                    CloudBlockBlob sourceBlob = container.GetBlockBlobReference(id);
                    CloudBlockBlob targetBlob = backupContainer.GetBlockBlobReference(id);
                    await targetBlob.StartCopyFromBlobAsync(sourceBlob);

                    //TODO: Why does it say 404 when is also says it still exists and it DOES still exist.
                    //await sourceBlob.DeleteAsync();
                }
            }
        }


        public async Task<IMindMap> GetMindMap(string id)
        {
            string retrievedJson = null;

            try
            {
                //check cache first
                StorageFolder localFolder =
                    ApplicationData.Current.LocalFolder;
                if (await StorageHelper.DoesFileExistAsync
                    (id, localFolder))
                {
                    //use cached version
                    StorageFile file =
                        await localFolder.GetFileAsync(id);

                    if (IsConnected())
                    {
                        // BasicProperties props = await file.GetBasicPropertiesAsync();
                        string dateAccessedProperty = "System.DateModified";

                        List<string> propertiesName = new List<string>();
                        propertiesName.Add(dateAccessedProperty);

                        var dateFileChanged = await file.Properties.RetrievePropertiesAsync(propertiesName);


                        DateTime dt = DateTime.Parse(dateFileChanged.First().Value.ToString());

                        CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantGraphContainerSaS));
                        bool existsInCloud = await container.GetBlockBlobReference(id).ExistsAsync();
                        if (existsInCloud)
                        {
                            CloudBlockBlob blob = container.GetBlockBlobReference(id);
                            await blob.FetchAttributesAsync();
                            if (dt.ToUniversalTime() < blob.Properties.LastModified)
                            {
                                //TODO:We depend on client time accuracy, that is not acceptible for sync conflict handling, by any standard!

                                //We have a newer file in the cloud. Handle it!
                                blob = container.GetBlockBlobReference(id);
                                retrievedJson = await blob.DownloadTextAsync();

                                //overwrite local
                                StorageFile storageFile = await localFolder.CreateFileAsync(id, CreationCollisionOption.ReplaceExisting);
                                await FileIO.WriteTextAsync(storageFile, retrievedJson);
                            }
                            else
                            {
                                //local file is newer
                                retrievedJson = await FileIO.ReadTextAsync(file);
                                await blob.UploadTextAsync(retrievedJson);
                            }
                        }
                        else
                        {
                            retrievedJson = await FileIO.ReadTextAsync(file);
                        }

                    }
                    else
                    {
                        retrievedJson = await FileIO.ReadTextAsync(file);
                    }
                }
                else //download and store now
                {
                    if (IsConnected())
                    {
                        CloudBlobContainer container = new CloudBlobContainer(new Uri(Settings.TenantGraphContainerSaS));
                        bool exists = await container.GetBlockBlobReference(id).ExistsAsync();


                        if (exists)
                        {
                            CloudBlockBlob blob = container.GetBlockBlobReference(id);
                            retrievedJson = await blob.DownloadTextAsync();
                        }

                        //store the response now
                        if (retrievedJson != null && retrievedJson != "")
                        {
                            StorageFile storageFile = await localFolder.CreateFileAsync(id, CreationCollisionOption.ReplaceExisting);
                            await FileIO.WriteTextAsync(storageFile, retrievedJson);
                        }
                    }
                    else
                    {
                        return new MindMap
                        {
                            Content = null,
                            Id = id,
                            Name = "Empty"
                        };
                    }
                }

                dynamic json = await Task.Run(() => JValue.Parse(retrievedJson));

                //return json;

                return new MindMap
                {
                    Content = json.content,
                    Id = id,
                    Name = json.name
                };
            }
            catch (Exception ex)
            {
                var jsonObject = (JObject)JsonConvert.DeserializeObject(retrievedJson);
                return new MindMap
                {
                    Content = jsonObject["content"].ToString(),
                    Id = id,
                    Name = jsonObject["name"].ToString()
                };
                return null;
            }
        }
    }
}
