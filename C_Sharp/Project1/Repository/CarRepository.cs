using System;
using Project1.Models;


namespace Project1.Repository
{
    public class CarRepository 
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
        carList.Add(car);
        return car.Id;
    }

    private List <Car> carList = new List<Car>(){

        new Car(){Id=1,Name="Toyota",Price=10000,Year= "2020", FuelType="Petrol"},
        new Car(){Id=2,Name="Honda",Price=20000,Year="2023", FuelType="Diesel"},
        new Car(){Id=3,Name="Suzuki",Price=30000,Year="2020", FuelType="Petrol"},
        new Car(){Id=4,Name="BMW",Price=40000,Year="2022", FuelType="Diesel"},
        new Car(){Id=5,Name="Audi",Price=50000,Year="2023", FuelType="Petrol"},
        new Car(){Id=6,Name="Mercedes",Price=60000,Year="2023", FuelType="Diesel"},
        new Car(){Id=7,Name="Ford",Price=70000,Year="2022", FuelType="Petrol"},
        new Car(){Id=8,Name="Chevrolet",Price=80000,Year="2020", FuelType="Diesel"},
        new Car(){Id=9,Name="Ferrari",Price=90000,Year="2021", FuelType="Petrol"},
        new Car(){Id=10,Name="Lamborghini",Price=100000,Year="2019", FuelType="Diesel"}       
    };
       
    }
}
