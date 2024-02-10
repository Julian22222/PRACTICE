using System.Collections.Generic;
using System.Threading.Tasks;
using Project_MVC_BookShop2.Models;

namespace Project_MVC_BookShop2.Repository
{

public interface ILanguageRepository
{
    //we put here in interface all action methods from the same Repository class name (LanguageRepository)
    Task<List<LanguageModel>> GetLanguages();
}



}

