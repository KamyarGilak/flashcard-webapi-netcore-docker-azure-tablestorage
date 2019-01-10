using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;

namespace FlashCard.Services
{
    public class StudySetServices : IStudySetServices
    {

        private const string StorageAccountName = "webapptest001storage";
        private const string StorageAccountKey = "yj/8YhYrSs67VOhaSUcV/+DP4wMGQ689IgjVq+aD+w/GWn1CllB3Vfp3EcMXrfo7wWgcal2S795rrGMh9zpwXA==";

        private readonly CloudStorageAccount _storageAccount = new CloudStorageAccount(new StorageCredentials(StorageAccountName, StorageAccountKey), true);
        private CloudTable _studySetTable;

        public StudySetServices()
        {
            //create Azure Table (if not existed)
            var tableClient = _storageAccount.CreateCloudTableClient();
            _studySetTable = tableClient.GetTableReference("StudySet");
            _studySetTable.CreateIfNotExistsAsync();

            
        }

        public StudySetItems AddStudySetItems(StudySetItems items)
        {
            var studySetList = new List<StudySetItems>();
            var studySet = new StudySetItems(items.Title,
                items.RowKey,
                items.Term,
                items.Definition,
                items.Stared);
            
        //todo "add retrived message if item is existed"
            TableOperation tableOperation = TableOperation.Insert(studySet);
            _studySetTable.ExecuteAsync(tableOperation);
            Console.WriteLine("Record inserted");


            return items;
        }

        public ActionResult<List<StudySetItems>> GetStudySetList()
        {
            var studySetList = new List<StudySetItems>();

            TableQuery<StudySetItems> studySetItemsQuery = new TableQuery<StudySetItems>()
             .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "username"));
            var studySetResult = _studySetTable.ExecuteQuerySegmentedAsync(studySetItemsQuery, null).Result;

            foreach (var resultRow in studySetResult)
            {
                var studySet = new StudySetItems(resultRow.Title,
                                                resultRow.RowKey,
                                                resultRow.Term,
                                                resultRow.Definition,
                                                resultRow.Stared,
                                                resultRow.ETag);

                studySetList.Add(studySet);
            }
            return studySetList;
        }

        public ActionResult<StudySetItems> GetStudySetItem(string cardId)
        {
            

            TableQuery<StudySetItems> studySetItemsQuery = new TableQuery<StudySetItems>()
                .Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "username"))
                .Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, cardId));
            var studySetResult = _studySetTable.ExecuteQuerySegmentedAsync(studySetItemsQuery, null).Result.Results;

            if(studySetResult.Count() != 0)
            {
                var studySetItem = new StudySetItems(studySetResult[0].Title,
                    studySetResult[0].RowKey,
                    studySetResult[0].Term,
                    studySetResult[0].Definition,
                    studySetResult[0].Stared,
                    studySetResult[0].ETag);
                return studySetItem;
            }

            return null;

            
        }
    }
}
