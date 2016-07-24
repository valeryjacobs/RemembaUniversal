using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace RemembaUniversal
{
    public class MindMapDataService : IMindMapDataService
    {
        public async Task<IMindMap> GetMindMap(string mindMapName)
        {
            MindMapRepository rep = new MindMapRepository();

            var mindmap = await rep.GetMindMap(mindMapName);

            return mindmap;
        }

        public async Task Delete(string id)
        {
            MindMapRepository rep = new MindMapRepository();

            await rep.Delete(id);
        }

        public async Task<INode> GetRootNode(IMindMap mindMap)
        {
            if (mindMap.Content == null || mindMap.Content == "")
            {
                mindMap.Content = "{\"id\":null,\"n\":\"aap\",\"d\":null,\"cid\":null,\"c\":[{\"id\":null,\"n\":\"Azure Features\",\"d\":\"A to Z featureset\",\"cid\":\"ad5372b4-0602-4fbd-b0a6-4c576755a473\",\"c\":[{\"id\":null,\"n\":\"Compute & Networking\",\"d\":\"\",\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Virtual Machines\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Links and Documents\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Pricing\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Azure RemoteApp\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Cloud Services\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Virtual Networks\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"ExpressRoute\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Traffic Manager\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Web & Mobile\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Websites\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Best practices\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Techniques\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Mobile Services\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Techniques\",\"d\":\"\",\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Using Azure Storage with Mobile Services\",\"d\":\"http://chrisrisner.com/Mobile-Services-and-Windows-Azure-Storage\",\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Best practices\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"API Management\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Best practices\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Notification Hubs\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Event Hubs\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]}]}]},{\"id\":null,\"n\":\"Data & Analytics\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"SQL Database\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Performance tuning\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Comparison SQL Server\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Tooling\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"HDInsight\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Cache\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Machine Learning\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"DocumentDB\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Azure Search\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Storage & Backup\",\"d\":\"\",\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Storage\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Import/Export Service\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Backup\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Site Recovery\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Hybrid Integration\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"BizTalk Services\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Service Bus\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]}]}]},{\"id\":null,\"n\":\"Identity & Access Management\",\"d\":\"Accounts, Authentication, AAD etc.\",\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Azure Active Directory\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Multi-Factor Authentication\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Accounts\",\"d\":null,\"cid\":\"6344b2c4-16dc-4500-910c-d36ffe09f121\",\"c\":[]}]},{\"id\":null,\"n\":\"Media & CDN\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Media Services\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Demos\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"CDN\",\"d\":null,\"cid\":\"1\",\"c\":[]}]}]},{\"id\":null,\"n\":\"Network\",\"d\":\"MS, MVP & Other\",\"cid\":\"2\",\"c\":[{\"id\":null,\"n\":\"WAZUGNL\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Coming Event\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Speaker pipeline\",\"d\":null,\"cid\":\"1\",\"c\":[]}]}]},{\"id\":null,\"n\":\"Guidance\",\"d\":\"Decisions vs options\",\"cid\":\"3\",\"c\":[{\"id\":null,\"n\":\"CAT Practices\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Community Practices\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Tooling\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Azure Explorer\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"CloudBerry Explorer\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"ServiceBus Manager\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":null,\"d\":null,\"cid\":\"1\",\"c\":[]}]}]},{\"id\":null,\"n\":\"Development\",\"d\":\"Skills & reference\",\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\".NET\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Async\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"IOC\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Reactive\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Orleans\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Javascript / HTML5\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Other\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"R&D\",\"d\":\"Looking into...xx\",\"cid\":\"7fcbeddb-1064-48c3-8c36-db05eaf79656\",\"c\":[{\"id\":null,\"n\":\"ADFS\",\"d\":\"Details, best practices\",\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"HDInsight\",\"d\":\"Demo, overview talk\",\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"SQL Database\",\"d\":\"Performance tuning, best practices, comparison NoSQL and RDB alternatives\",\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Meteor\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Windows Ecosystem\",\"d\":\"Desktop,Slate & Phone\",\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Windows 8.1\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Windows Phone\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Other Microsoft Products\",\"d\":\"Servers & SaaS\",\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"CRM Dynamics\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Office 365\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Migration Plans\",\"d\":null,\"cid\":\"1\",\"c\":[]}]}]},{\"id\":null,\"n\":\"ToDo's\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"WAZUG Non-profit aanvraag\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"DemoButler\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"30-Day plan\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Trello integration\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Twitter feed\",\"d\":\"\",\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"O365 SDK REST\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Reminders\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Navragen aanbreng bonus Dennis\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"O365 Training\",\"d\":\"http://channel9.msdn.com/Series/Managing-Office-365-Identities-and-Services/10\",\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Notes\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":null,\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Quick Nodes\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Recordings\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Nodes\",\"d\":null,\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Blog\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"Posts\",\"d\":null,\"cid\":\"1\",\"c\":[]},{\"id\":null,\"n\":\"Statistics\",\"d\":\"Comments, views, IO\",\"cid\":\"1\",\"c\":[]}]},{\"id\":null,\"n\":\"Links\",\"d\":null,\"cid\":\"1\",\"c\":[{\"id\":null,\"n\":\"WAZUG OneNote\",\"d\":\"https://erwyn.sharepoint.com/teams/wazugnl/_layouts/15/WopiFrame.aspx?sourcedoc={6E45A30C-4AEC-4AA1-9B0D-AD97B39E15E9}&file=WAZUG%20NL%20Bestuur%20Notebook&action=default\",\"cid\":\"1\",\"c\":[]}]}]}";
            }

            dynamic json = await Task.Run(() => JValue.Parse(mindMap.Content));

            return await Task.Run(() => TreeHelper.BuildTree(new Node(), json));
        }

        public async Task<INode> CloneNode(INode node)
        {
            JsonNode jsonNode = new JsonNode();
            JsonNode jsonObject = BuildJSON(jsonNode, node);

            var serializedNode = JsonConvert.SerializeObject(jsonObject);

            dynamic json = await Task.Run(() => JValue.Parse(serializedNode));

            var clonedNode = await Task.Run(() => TreeHelper.BuildTree(new Node(), json));

            return clonedNode;
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

        public async Task Save(IMindMap mindMap, INode rootNode)
        {
            MindMapRepository rep = new MindMapRepository();
            await rep.SaveMindMap(rootNode, mindMap);
        }

        public async Task<IMindMap> Create(string name)
        {
            MindMapRepository rep = new MindMapRepository();
            return await rep.CreateGraph(name);
        }


        public async Task<List<IMindMap>> ListMindMaps()
        {
            MindMapRepository rep = new MindMapRepository();
            return await rep.ListMindMaps();
        }


        public async Task<List<INode>> Search(string searchQuery, INode rootNode, bool searchContent)
        {
            if (rootNode == null || rootNode.Children == null || rootNode.Children.Count == 0) return new List<INode>();

            //var boo = ContentContains(rootNode.ContentId, searchQuery);


            var results = new List<INode>();

            await SearchNode(rootNode, searchQuery, results, searchContent);

            //return await Task.Run(() =>
            //{
            //    var some = rootNode.Children.Where(x =>
            //        x.Title.Contains(searchQuery) ||
            //               (x.Description != null && x.Description.Contains(searchQuery))
            //         );

            //    return new List<INode>();
            //}); 

            return results;
        }

        private async Task SearchNode(INode node, string searchQuery, List<INode> results, bool searchContent)
        {
            if (node.Title.Contains(searchQuery) || (node.Description != null && node.Description.Contains(searchQuery)))
            {
                results.Add(node);
            }
            else
            {
                if (searchContent)
                {
                    if (await ContentContains(node.ContentId, searchQuery))
                    {
                        results.Add(node);
                    }
                }
            }

            foreach (INode n in node.Children)
            {
                await SearchNode(n, searchQuery, results, searchContent);
            }
        }

        private async Task<bool> ContentContains(string contentId, string searchQuery)
        {
            // return true;

            string data = null;

            StorageFolder localFolder =
                  ApplicationData.Current.LocalFolder;
            if (await StorageHelper.DoesFileExistAsync
                (contentId, localFolder))
            {
                //use cached version
                StorageFile file = await localFolder.GetFileAsync(contentId);
                data = await FileIO.ReadTextAsync(file);

                return data.Contains(searchQuery);
            }
            else
            { return false; }
        }
    }
}
