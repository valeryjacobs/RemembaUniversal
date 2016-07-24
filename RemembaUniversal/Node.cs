using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemembaUniversal
{
    public class Node : INotifyPropertyChanged, INode
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Node()
        {
            Children = new ObservableCollection<INode>();
        }


        private string _id;
        [JsonProperty("id")]
        public string Id
        {
            get { return _id; }
            set
            {
                _id = value;
                NotifyPropertyChanged("Id");
            }
        }

        private string _description;

        [JsonProperty("d")]
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                NotifyPropertyChanged("Description");
            }
        }

        private string _title;

        [JsonProperty("n")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                NotifyPropertyChanged("Title");
            }
        }

        private string _type;
        [JsonProperty("t")]
        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                NotifyPropertyChanged("Type");
            }
        }

        private string _contentId;

        [JsonProperty("cid")]
        public string ContentId
        {
            get { return _contentId; }
            set
            {
                _contentId = value;
                NotifyPropertyChanged("ContentId");
            }
        }

        private bool _edit;

        [JsonIgnoreAttribute]
        public bool Edit
        {
            get { return _edit; }
            set
            {
                _edit = value;
                NotifyPropertyChanged("Edit");
            }
        }

        private INode _parent;

        [JsonIgnoreAttribute]
        public INode Parent
        {
            get { return _parent; }
            set
            {
                _parent = value;
                NotifyPropertyChanged("Parent");
            }
        }

        private bool _markedForDeletion;

        public bool MarkedForDeletion
        {
            get { return _markedForDeletion; }
            set { _markedForDeletion = value; }
        }

        private ObservableCollection<INode> _children;

        [JsonProperty("c")]
        public ObservableCollection<INode> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                NotifyPropertyChanged("Children");
            }
        }

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this,
                    new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
