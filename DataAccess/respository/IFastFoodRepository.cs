using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public interface IFastFoodRepository
    {
        IEnumerable<FastFood> GetFastFoods();
        FastFood GetFastFoodByID(int fastFoodId);
        void DeleteFastFood(int fastFoodId);
        void InsertFastFood(FastFood fastFood);
        void UpdateFastFood(FastFood fastFood);
    }
}
