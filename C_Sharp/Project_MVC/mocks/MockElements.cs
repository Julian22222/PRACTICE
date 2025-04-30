using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Project_MVC.Interfaces;   //connect IAllElements interface
using Project_MVC.Models;   //connect Element class

namespace Project_MVC.mocks
{

     // this class will implement interface IAllElements
    public class MockElements : IAllElements
    {

        private readonly IElementsCategory _categoryElements = new MockCategory();
        
           // IEnumerable - is a list type
        public IEnumerable<Element> Elements {
            get{
                return new List<Element>{
                    new Element {name="Fighting game", shortDesc="Card fighting game", longDesc="Fighting game where you pick up your a card ", img="https://insider-gaming.com/wp-content/uploads/2023/04/mortal-kombat-12-1.jpg", price = 4500, isFavourite = true, available=true, category = _categoryElements.AllCategories.First()},
                    new Element {name="Machine", shortDesc="Vending machine", longDesc="Vending machine app - where you have pocket money and possibility to buy groceries", img="https://www.servicevend.co.uk/image/cache/catalog/merchant-touch-6-550x550h.png", price = 4500, isFavourite = true, available=true, category = _categoryElements.AllCategories.First()},
                    new Element {name="Calculator", shortDesc="Calculator app", longDesc="Possibility to make calculation of different numbers", img="https://m.media-amazon.com/images/I/61yotlsfV3L._AC_UF1000,1000_QL80_.jpg", price = 4500, isFavourite = true, available=true, category = _categoryElements.AllCategories.Last()},
                    new Element {name="IT blog", shortDesc="Blog about different IT technologies", longDesc="Possibility to read and learn new technologies in IT.", img="https://clickfirstmarketing.com/wp-content/uploads/Purpose-of-Blogging.jpeg", price = 4500, isFavourite = true, available=true, category = _categoryElements.AllCategories.Last()},
                };
            }
        }


        public IEnumerable<Element> getFavElements {get;set;}


        // function
        public Element getObjectElement(int elementId){
            throw new NotImplementedException();
        }

    }
}