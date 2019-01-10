using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashCard.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Table;

namespace FlashCard.Services
{
    public interface IStudySetServices
    {
        StudySetItems AddStudySetItems(StudySetItems items);

        ActionResult<List<StudySetItems>> GetStudySetItems();

    }
}
