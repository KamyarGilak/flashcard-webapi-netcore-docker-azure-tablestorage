using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Table;

namespace FlashCard.Models
{
    public class StudySetItems : TableEntity
    {
        private string eTag;
        private string cardId;

        public string CardId
        {
            get => cardId;
            set => cardId  = RowKey = value;
        }
        
        
        public void AssignRowKey()
        {
            RowKey = cardId;
        }
        public void AssignPartitionKey()
        {
            PartitionKey = "username";
        }

        public string Title { get; set; }
        public string Term { get; set; }
        public string Definition { get; set; }
        public Boolean Stared { get; set; }

        public StudySetItems() {}

        public StudySetItems(string title, string cardId, string term, string definition, bool isStared, string eTag)
        {
            CardId = cardId;
            Title = title;
            Term = term;
            Definition = definition;
            Stared = isStared;
            ETag = eTag;
            AssignRowKey();
            AssignPartitionKey();
        }

        public StudySetItems(string title, string cardId, string term, string definition, bool isStared)
        {
            CardId = cardId;
            Title = title;
            Term = term;
            Definition = definition;
            Stared = isStared;
            AssignRowKey();
            AssignPartitionKey();
        }
    }
}
