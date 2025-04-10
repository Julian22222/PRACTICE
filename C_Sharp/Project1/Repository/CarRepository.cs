using System;
using Project1.Models;


namespace Project1.Repository
{
    public class CarRepository  : ICarRepository  //inherit from ICarRepository (interface)
    {
        
        // public async Task<List<Car>> GetAllCars(){
        //     return Allcars();
        // //Local application data storage, dont use DB here 

        // }


        // public async Task<Car> GetCarById(int id){
        //     return Allcars().Where(x=>x.Id==id).FirstOrDefault();
        // }

        

        //Local application data storage, dont use DB here 
        // private List<Car> Allcars(){
        //     return new List<Car>(){
        //     new Car(){Id=1,Name="Toyota",Price=10000,Year=DateTime.Now,FuelType="Petrol"},
        //     new Car(){Id=2,Name="Honda",Price=20000,Year=DateTime.Now,FuelType="Diesel"},
        //     new Car(){Id=3,Name="Suzuki",Price=30000,Year=DateTime.Now,FuelType="Petrol"},
        //     new Car(){Id=4,Name="BMW",Price=40000,Year=DateTime.Now,FuelType="Diesel"},
        //     new Car(){Id=5,Name="Audi",Price=50000,Year=DateTime.Now,FuelType="Petrol"},
        //     new Car(){Id=6,Name="Mercedes",Price=60000,Year=DateTime.Now,FuelType="Diesel"},
        //     new Car(){Id=7,Name="Ford",Price=70000,Year=DateTime.Now,FuelType="Petrol"},
        //     new Car(){Id=8,Name="Chevrolet",Price=80000,Year=DateTime.Now,FuelType="Diesel"},
        //     new Car(){Id=9,Name="Ferrari",Price=90000,Year=DateTime.Now,FuelType="Petrol"},
        //     new Car(){Id=10,Name="Lamborghini",Price=100000,Year=DateTime.Now,FuelType="Diesel"}       
        //     };
        // }

    public List<Car> GetAllCars()
    // public IEnumerable<Car> GetAllCars()
    {
        return carList;
    }

    public Car GetCarById(int id)
    {
        return carList.Where(x => x.Id == id).FirstOrDefault();
    }

    // public void AddCar(Car car)
    // {
    //     carList.Add(car);
    // }

    public int AddCar(Car car)
    {
        // Generate a new ID for the car,  we assign the Id value by finding the highest Id in the list and adding 1
        car.Id = carList.Count > 0 ? carList.Max(c => c.Id) + 1 : 1;

        carList.Add(car);
        return car.Id;
    }


    //If you're using an in-memory list (carList), it's important to inject the CarRepository properly so that the list persists across requests
    //Notice that carList is a static field in this case. This ensures that the data persists throughout the application's lifetime, meaning that the cars added in one request will be available in subsequent requests.
    // Use a static list so the data persists across requests.
     //static list (not recommended for production but useful for testing)
     //If you're using an in-memory list for testing, try making carList static in your repository temporarily to make sure the data persists across requests:
    private static List <Car> carList = new List<Car>(){

        new Car(){Id=1,Name="Toyota",Price=10000,Year= new DateTime(2020,5,20), FuelType="Petrol"},
        new Car(){Id=2,Name="Honda",Price=20000,Year=new DateTime(2021,7,20), FuelType="Diesel"},
        new Car(){Id=3,Name="Suzuki",Price=30000,Year=new DateTime(2023,2,11), FuelType="Petrol"},
        new Car(){Id=4,Name="BMW",Price=40000,Year=new DateTime(2024,9,24), FuelType="Diesel"},
        new Car(){Id=5,Name="Audi",Price=50000,Year=new DateTime(2029,11,14), FuelType="Petrol"},
        new Car(){Id=6,Name="Mercedes",Price=60000,Year=new DateTime(2023,10,25), FuelType="Diesel"},
        new Car(){Id=7,Name="Ford",Price=70000,Year=new DateTime(2020,8,23), FuelType="Petrol"},
        new Car(){Id=8,Name="Chevrolet",Price=80000,Year=new DateTime(2022,5,26), FuelType="Diesel"},
        new Car(){Id=9,Name="Ferrari",Price=90000,Year=new DateTime(2020,7,20), FuelType="Petrol"},
        new Car(){Id=10,Name="Lamborghini",Price=100000,Year=new DateTime(2023,4,12), FuelType="Diesel"}       
    };
       
    }
}
