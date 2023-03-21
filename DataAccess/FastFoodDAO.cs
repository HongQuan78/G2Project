using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CinemaObject;

namespace DataAccess
{
    public class FastFoodDAO
    {
        private static FastFoodDAO instance = null;
        public static readonly object instanceLock = new object();
        public static FastFoodDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new FastFoodDAO();
                    }
                }
                return instance;
            }
        }

        public IEnumerable<FastFood> GetFastFoodList()
        {
            var fastFoods = new List<FastFood>();
            try
            {
                using var context = new CinemaProject_v4Context();
                fastFoods = context.FastFoods.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return fastFoods;
        }

        public FastFood GetFastFoodByID(int fastFoodID)
        {
            FastFood fastFood = null;
            try
            {
                using var context = new CinemaProject_v4Context();
                fastFood = context.FastFoods.SingleOrDefault(c => c.Id == fastFoodID);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return fastFood;
        }

        public void AddNewFastFood(FastFood fastFood)
        {
            try
            {
                FastFood _fastFood = GetFastFoodByID(fastFood.Id);
                if (_fastFood == null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.FastFoods.Add(fastFood);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The fast food is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void UpdateFastFood(FastFood fastFood)
        {
            try
            {
                FastFood _fastFood = GetFastFoodByID(fastFood.Id);
                if (_fastFood != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.FastFoods.Update(fastFood);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The fast food does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RemoveFastFood(int fastFoodID)
        {
            try
            {
                FastFood fastFood = GetFastFoodByID(fastFoodID);
                if (fastFood != null)
                {
                    using var context = new CinemaProject_v4Context();
                    context.FastFoods.Remove(fastFood);
                    context.SaveChanges();
                }
                else
                {
                    throw new Exception("The fast food does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
