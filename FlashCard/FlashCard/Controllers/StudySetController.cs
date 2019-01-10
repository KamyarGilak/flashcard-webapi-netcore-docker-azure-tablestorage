using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlashCard.Models;
using FlashCard.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Template;

namespace FlashCard.Controllers
{
    [Route("v1/")]
    [ApiController]
    public class StudySetController : ControllerBase
    {
        private readonly IStudySetServices _studySetServices;
        public StudySetController(IStudySetServices studySetServices)
        {
            _studySetServices = studySetServices;
        }

        // GET StudySets
        [HttpGet]
        [Route("studysets")]

        public ActionResult<List<StudySetItems>> GetStudySetItems()
        {
            var studySetItems = _studySetServices.GetStudySetList();
            if (studySetItems.Value.Count == 0)
            {
                return NotFound();
            }
            return studySetItems;
        }

        // POST StudySet
        [HttpPost]
        [Route("studysets")]
        public ActionResult<StudySetItems> AddStudySetItems(StudySetItems items)
        {
            var studySetItems = _studySetServices.AddStudySetItems(items);

            if (studySetItems == null)
            {
                return NotFound();
            }

            return studySetItems;

        }

        // GET api/values/5
        [HttpGet("studysets/{cardId}")]
        public ActionResult<StudySetItems> Get(string cardId)
        {
            var studySetItems = _studySetServices.GetStudySetItem(cardId);
            if (studySetItems == null)
            {
                return NotFound();
            }
            return studySetItems;
        }
        


        //        // PUT api/values/5
        //        [HttpPut("{id}")]
        //        public void Put(int id, [FromBody] string value)
        //        {
        //        }
        //
        //        // DELETE api/values/5
        //        [HttpDelete("{id}")]
        //        public void Delete(int id)
        //        {
        //        }
    }
}
