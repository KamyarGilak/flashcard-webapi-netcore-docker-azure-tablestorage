using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace FlashCard.Services
{
    public class StudySetServices : IStudySetServices
    {

        private const string StorageAccountName = "webapptest001storage";
        private const string StorageAccountKey = "yj/8YhYrSs67VOhaSUcV/+DP4wMGQ689IgjVq+aD+w/GWn1CllB3Vfp3EcMXrfo7wWgcal2S795rrGMh9zpwXA==";

        private readonly List<StudySetItems> _studySetDicItems;

        private readonly CloudStorageAccount _storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), true);
        private CloudTable _studySetTable;

        public StudySetServices()
        {
            //create Azure Table (if not existed)
            var tableClient = _storageAccount.CreateCloudTableClient();
            _studySetTable = tableClient.GetTableReference("StudySet");
            _studySetTable.CreateIfNotExistsAsync();

            _studySetDicItems = new List<StudySetItems>();
        }

        public StudySetItems AddStudySetItems(StudySetItems items)
        {
            var studySet = new StudySetItems();
            _studySetDicItems.Add(studySet);

//            _studySetTable.ExecuteAsync(TableOperation.InsertOrReplace(new StudySetItems("Study Title 00", "00", "term 00", "term def 00", false)));


            return items;
        }

        public ActionResult<List<StudySetItems>> GetStudySetItems()
        {
            TableQuery<StudySetItems> studySetItemsQuery = new TableQuery<StudySetItems>()
             .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "username")); ;

            foreach (var studySetResult in _studySetTable.ExecuteQuerySegmentedAsync(studySetItemsQuery, null).Result)
            {
                var studySet = new StudySetItems(studySetResult.Title,
                                                    studySetResult.RowKey,
                                                    studySetResult.Term,
                                                    studySetResult.Definition, 
                                                    studySetResult.Stared, 
                                                    studySetResult.ETag);

                _studySetDicItems.Add(studySet);
            }
            return _studySetDicItems;
        }
    }
}
