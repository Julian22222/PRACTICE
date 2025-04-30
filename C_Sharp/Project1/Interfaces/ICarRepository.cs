using Project1.Models;

namespace Project1.Interfaces;
public interface ICarRepository
{
    List<Car> GetAllCars();
    Car GetCarById(int id);
    int AddCar(Car car);

    Car EditCar(int id, Car Newcar);
    void DeleteCar(int id);
}