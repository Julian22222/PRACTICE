using Project1.Models;

public interface ICarRepository
{
    List<Car> GetAllCars();
    Car GetCarById(int id);
    int AddCar(Car car);
}