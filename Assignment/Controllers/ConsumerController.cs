using Assignment.Data;
using Assignment.Models;
using Assignment.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.Controllers
{
    public class ConsumerController : Controller
    {
        private readonly AssignmentDemoDbcontext assignmentDemoDbcontext;

        public ConsumerController(AssignmentDemoDbcontext assignmentDemoDbcontext)
        {
            this.assignmentDemoDbcontext = assignmentDemoDbcontext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Index() 
        {
           var consumer = await assignmentDemoDbcontext.Consumers.ToListAsync();
            return View(consumer);
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddConsumerViewModel addConsumerRequest)
        {
            var consumer = new Consumer()
            {

                Id = Guid.NewGuid(),
                FirstName = addConsumerRequest.FirstName,
                LastName = addConsumerRequest.LastName,
                Address = addConsumerRequest.Address,
                Email = addConsumerRequest.Email
            };
            await assignmentDemoDbcontext.Consumers.AddAsync(consumer);
            await assignmentDemoDbcontext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var consumer = await assignmentDemoDbcontext.Consumers.FirstOrDefaultAsync(x => x.Id == id);

            if (consumer != null)
            {

                var viewModel = new UpdateViewModel()
                {

                    Id = consumer.Id,
                    FirstName = consumer.FirstName,
                    LastName = consumer.LastName,
                    Address = consumer.Address,
                    Email = consumer.Email
                };
                return await Task.Run(() => View("View", viewModel));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateViewModel model)
        {
            var consumer = await assignmentDemoDbcontext.Consumers.FindAsync(model.Id);
            if (consumer != null) 
            { 
                consumer.FirstName= model.FirstName;
                consumer.LastName= model.LastName;
                consumer.Address= model.Address;
                consumer.Email= model.Email;

                await assignmentDemoDbcontext.SaveChangesAsync();

                return RedirectToAction("Index");
            
            }
            return RedirectToAction("Index");
        }
        [HttpPost]

        public async Task<IActionResult> Delete(UpdateViewModel model)
        {
            var consumer = await assignmentDemoDbcontext.Consumers.FindAsync(model.Id);
            if (consumer != null) 
            {
                assignmentDemoDbcontext.Consumers.Remove(consumer);
                await assignmentDemoDbcontext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }

}
