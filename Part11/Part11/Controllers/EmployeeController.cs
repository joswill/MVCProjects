 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessLayer;

namespace Part11.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Employee
        public ActionResult Index()
        {
            EmployeeBusLayer employeeBus = new EmployeeBusLayer();
            List<Employee> employees = employeeBus.Employees.ToList();
            return View(employees);
        }
        //this method will only respond to the get request
        [HttpGet]
        [ActionName ("Create")]
        public ActionResult Create_Get()
        {
           
            return View();
        }
        
        //public ActionResult Create(FormCollection formcollection)
        //{
        //    //foreach(string key in formcoll.AllKeys)
        //    //{
        //    //    Response.Write("key = " + key + " ");
        //    //    Response.Write(formcoll[key]);
        //    //    Response.Write("<br/>");
        //    //}

        //    Employee employee = new Employee();
        //    employee.Name = formcollection["Name"];
        //    employee.Gender = formcollection["Gender"];
        //    employee.City = formcollection["City"];
        //    employee.DepartmentId = Convert.ToInt32(formcollection["DepartmentId"]);
        //    EmployeeBusLayer bus = new EmployeeBusLayer();
        //    bus.AddEmployee(employee);
        //    return RedirectToAction("Index");
        //}
        // 2) i can use model binders to accomplish the same task as in the 
        // method below
            // the name of the parameters has the match with the name of the controls
            // but the order of the parameters does not matter
        //public ActionResult Create(string name, string gender, string city, string departmentid)
        //{
        //    Employee employee = new Employee();
        //    employee.Name = name;
        //    employee.Gender = gender;
        //    employee.City = city;
        //    employee.DepartmentId = Convert.ToInt32(departmentid);
        //    EmployeeBusLayer bus = new EmployeeBusLayer();
        //    bus.AddEmployee(employee);
        //    return RedirectToAction("Index");
        //}
        // 3) the third method is to pass form data as an Employee object parameter
        //
       /* public ActionResult Create(Employee employee)
        {
            // check whether there are model validation errors
            if(ModelState.IsValid)
            {
                EmployeeBusLayer bus = new EmployeeBusLayer();
                bus.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }*/


        //4 we can achieve the same thing without passing anyting to the controller
        // now since the two methods are the same "Create"
        // we now need to rename the one method 
        // but it will no longer respond to the post action
        // this is fixed by actionname 
        [HttpPost]
        [ActionName("Create")]
        public ActionResult Create_Post()
        {
            
            // check whether there are model validation errors
            if (ModelState.IsValid)
            {
                
                /*the updatemodel fn will inspect all the HttpRequest input such as
                 posted form data, wuesry strings cookies and server variables and 
                 * populate the employee object*/
                Employee employee = new Employee();
                UpdateModel(employee);
                
                EmployeeBusLayer bus = new EmployeeBusLayer();
                bus.AddEmployee(employee);
                return RedirectToAction("Index");
            }
            return View();
        }
        
        [HttpGet]
        public ActionResult Edit(int id)
        {
            EmployeeBusLayer busLayer = new EmployeeBusLayer();
            Employee employee = busLayer.Employees.Single(emp => emp.EmployeeId == id);
            return View(employee);

        }
        [HttpPost]
        public ActionResult Edit(Employee myEmployee)
        {
            if(ModelState.IsValid)
            {
                EmployeeBusLayer busLayer = new EmployeeBusLayer();
                busLayer.saveEmployee(myEmployee);
                return RedirectToAction("Index");
            }
            
            // Employee employee = busLayer.Employees.Single(emp => emp.EmployeeId == id);
            return View(myEmployee);

        }
        [HttpPost]
        public ActionResult Save()
        {
            if(ModelState.IsValid)
            {
                Employee employee = new Employee();
                UpdateModel(employee);

                EmployeeBusLayer bus = new EmployeeBusLayer();
                bus.saveEmployee(employee);
                return RedirectToAction("Index");

            }
            return View();   
        }

        // fiddler to force changes 
    }
}