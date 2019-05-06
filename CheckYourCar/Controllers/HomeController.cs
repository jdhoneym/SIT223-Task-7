using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CheckYourCar.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;


namespace CheckYourCar.Controllers
{
    public class HomeController : Controller
    {


        public string constr { get; set; }

        public HomeController(IConfiguration configuration)
        {
            this.constr = configuration.GetConnectionString("DefaultConnection"); 
        }

        [HttpPost]
        public IActionResult AjaxMethod([FromBody] Car car)
        {

            var model = new ResultModel
            {
                Cars = new List<Car>(),
                Recalls = new List<Recall>(),
            };


            try
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string queryStudent = "SELECT * FROM `Recall` WHERE `CarID` =" + car.CarID + "  ";
                    using (MySqlCommand cmd = new MySqlCommand(queryStudent))
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {

                                model.Recalls.Add(new Recall()
                                {

                                    CarID = Convert.ToInt32(sdr["CarID"]),
                                    Description = sdr["Description"].ToString(),
                                    StartDate = sdr["StrtDateRecall"].ToString(),
                                    EndDate = sdr["EndDateRecall"].ToString(),
                                    Contact = sdr["Contact"].ToString()


                                });
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                var exxx = ex;
            }



            return Json(new { model});
        }


        public IActionResult Index()
        {

            var model = new ResultModel
            {
                Cars = new List<Car>(),
            };


            try
            {
                using (MySqlConnection con = new MySqlConnection(constr))
                {
                    string queryStudent = "SELECT * FROM `Car` ";
                    using (MySqlCommand cmd = new MySqlCommand(queryStudent))
                    {
                        cmd.Connection = con;
                        con.Open();
                        using (MySqlDataReader sdr = cmd.ExecuteReader())
                        {
                            while (sdr.Read())
                            {

                                model.Cars.Add(new Car()
                                {

                                    CarID = Convert.ToInt32(sdr["CarID"]),
                                    // Title = sdr["title"].ToString(),
                                    //Name = sdr["first name"].ToString(),
                                    //Surname = sdr["surname"].ToString()


                                    Make = sdr["Make"].ToString(),
                                    Model = sdr["Model"].ToString(),
                                    Year = sdr["Year"].ToString(),
                                    BodyType = sdr["BodyType"].ToString()


                                });
                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                var exxx = ex;
            }







            return View(model);


        }

        public IActionResult About()
        {
            // ViewData["Message"] = "Your application description page.";

            //MusicStoreContext context = HttpContext.RequestServices.GetService(typeof(CheckYourCar.Models.MusicStoreContext)) as MusicStoreContext;
            return View();
           // return View(context.GetAllAlbums());
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
