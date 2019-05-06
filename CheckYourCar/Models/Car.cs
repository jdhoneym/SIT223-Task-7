using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using CheckYourCar.Models;


namespace CheckYourCar.Models
{



    public class Car
    {


        public int CarID { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string BodyType { get; set; }

    }

    public class Recall
    {


        public int CarID { get; set; }

        public string Description { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

        public string Contact { get; set; }

    }

  

    public class ResultModel
    {

        public List<Car> Cars { get; set; }
        public List<Recall> Recalls { get; set; }

        // public List<StudentUnitsModel> StudentUnits { get; set; }

    }



}
