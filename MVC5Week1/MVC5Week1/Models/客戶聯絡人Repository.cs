using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Week1.Models
{   
	public  class 客戶聯絡人Repository : EFRepository<客戶聯絡人>, I客戶聯絡人Repository
	{
        public override IQueryable<客戶聯絡人> All()
        {
            return base.All().Include("客戶資料").Where(p => !p.是否已刪除);
        }

        public IQueryable<客戶聯絡人> All(string queryname)
        {
            if(!string.IsNullOrEmpty(queryname))
            {
                return this.All().Where(p => p.職稱.Contains(queryname));
            } else
            {
                return this.All();
            }
            
        }

        public 客戶聯絡人 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
    }

	public  interface I客戶聯絡人Repository : IRepository<客戶聯絡人>
	{

	}
}