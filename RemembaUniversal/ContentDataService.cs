using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemembaUniversal
{
    public class ContentDataService : IContentDataService
    {
        public async Task<ObservableCollection<IContent>> GetContentItems()
        {
            ContentRepository rep = new ContentRepository();

            var contentItems = await rep.GetContentItems();

            return contentItems;
        }

        public async Task<IContent> GetContent(string contentId)
        {
            ContentRepository rep = new ContentRepository();

            var contentItem = await rep.GetContent(contentId);

            return contentItem;
        }

        public async Task AddContent(IContent content)
        {
            ContentRepository rep = new ContentRepository();

            await rep.AddContent(content);
        }

        public async Task UpdateContent(IContent content)
        {
            ContentRepository rep = new ContentRepository();

            await rep.UpdateContent(content);
        }

        public async Task DeleteContent(string id)
        {
            ContentRepository rep = new ContentRepository();

            await rep.DeleteContent(id);
        }

        public async Task ClearCache()
        {
            ContentRepository rep = new ContentRepository();

            await rep.ClearCache();
        }
    }
}
