//using Group2_3_Mission8.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Group2_3_Mission8.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Group2_3_Mission8.Controllers
{
    public class HomeController : Controller
    {
        // Get the info from our context file into our controller
        private TaskEntryContext taskEntryContext { get; set; }

        public HomeController(TaskEntryContext taskEntryContextObj)
        {
            // Set the context as a parameter
            taskEntryContext = taskEntryContextObj;
        }

        public IActionResult Index()
        {
            return View();
        }

        // The "AddTask" actions (Get and Post) are used to create a new task

        // The Get action will allow us to view the form and select a category
        [HttpGet]
        public IActionResult AddTask()
        {
            // This will allow us to see the categories in a dropdown on the form
            ViewBag.Categories = taskEntryContext.CategorySet.ToList();

            // This will open the "AddTask" view with a new TaskFormResponse object
            // which will allow us to add a hidden field in the form with a taskId
            return View(new TaskFormResponse());
        }

        // ThePost action will allow us to add the Task to the db.
        [HttpPost]
        public IActionResult AddTask(TaskFormResponse res)
        {
            if (ModelState.IsValid)
            {
                // Take the task context and add it to the db
                taskEntryContext.Add(res);
                // Save changes with the new task added
                taskEntryContext.SaveChanges();

                // Return the user to the quadrants to view all tasks
                return RedirectToAction("Quadrants");
            }
            else
            {
                // This will reload the form if it is not filled correctly
                ViewBag.Categories = taskEntryContext.CategorySet.ToList();
                return View(res);
            }
        }

        // This will allow the user to see all tasks in their respective Quadrants
        [HttpGet]
        public IActionResult Quadrants()
        {
            // This will allow us to return the tasks to the user
            // First, get all tasks from the db
            var allTasks = taskEntryContext.Task
                .Include(x => x.Category)
                .Where(x => x.Completed != true)
                .ToList();

            // Then, send this as a context variable to the user
            return View(allTasks);
        }

        // This will allow the user to get the information from a record so they can edit it
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // This will get us all the categories, again to use as a dropdown
            ViewBag.Categories = taskEntryContext.CategorySet.ToList();

            // This will get a single task with the corresponding ID from the one the user selected
            var task = taskEntryContext.Task.Single(x => x.TaskId == id);

            // This will send the user to the AddTask view with an associated task
            return View("AddTask", task);
        }

        /// This will allow the user to update the selected task
        [HttpPost]
        public IActionResult Edit(TaskFormResponse res)
        {
            // Make sure the model is valid before updating it
            if (ModelState.IsValid)
            {
                // Update the response to the db
                taskEntryContext.Update(res);

                // Save the changes to the db
                taskEntryContext.SaveChanges();

                // Return the user to the Quadrants view
                return RedirectToAction("Quadrants");
            }
            else
            {
                // This will reload the form if it is not filled correctly
                ViewBag.Categories = taskEntryContext.CategorySet.ToList();
                return View("AddTask", res);
            }
        }


        // This will allow the user to delete an item
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // This will get us all the categories, again to use as a dropdown
            ViewBag.Categories = taskEntryContext.CategorySet.ToList();

            // This will get a single task with the corresponding ID from the one the user selected
            var task = taskEntryContext.Task.Single(x => x.TaskId == id);
            return View(task);
        }

        [HttpPost]
        public IActionResult Delete(TaskFormResponse res)
        {
            // Remove selected item and save.
            taskEntryContext.Task.Remove(res);
            taskEntryContext.SaveChanges();

            // Return to the quadrants view
            return RedirectToAction("Quadrants");
        }
    }
}

