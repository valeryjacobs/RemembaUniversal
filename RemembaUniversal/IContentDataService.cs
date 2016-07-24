using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemembaUniversal
{
    public interface IContentDataService
    {
        Task<ObservableCollection<IContent>> GetContentItems();

        Task<IContent> GetContent(string id);

        Task AddContent(IContent content);

        Task UpdateContent(IContent content);

        Task DeleteContent(string id);
        Task ClearCache();
    }
}
