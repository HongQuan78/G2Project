using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess.respository
{
    public class FastFoodRepository: IFastFoodRepository
    {
        public FastFood GetFastFoodByID(int fastFoodId) => FastFoodDAO.Instance.GetFastFoodByID(fastFoodId);
        public IEnumerable<FastFood> GetFastFoods() => FastFoodDAO.Instance.GetFastFoodList();
        public void InsertFastFood(FastFood fastFood) => FastFoodDAO.Instance.AddNewFastFood(fastFood);
        public void DeleteFastFood(int fastFoodId) => FastFoodDAO.Instance.RemoveFastFood(fastFoodId);
        public void UpdateFastFood(FastFood fastFood) => FastFoodDAO.Instance.UpdateFastFood(fastFood);
    }
}
