using System;

namespace Practice1
{
    class Shoe
    {

        private int _shoePrice;

        public Shoe(int shoePrice){
            _shoePrice = shoePrice;
        }

       public void ShowAllInfo(){
       Console.WriteLine($"This shoes costs - £ {_shoePrice}");

        }


        
    }
}