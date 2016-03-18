using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Week1.Models
{   
	public  class 客戶資料Repository : EFRepository<客戶資料>, I客戶資料Repository
	{
        public override IQueryable<客戶資料> All()
        {
            return base.All().Where(p => !p.是否已刪除).OrderBy(p => p.Id);
        }

        public IQueryable<客戶資料> All(string queryname,string type)
        {
            var data = this.All().AsQueryable();
            if (!string.IsNullOrEmpty(queryname))
            {
                data = data.Where(p => p.客戶名稱.Contains(queryname));
            }
            if (!string.IsNullOrEmpty(type))
            {
                data = data.Where(p => p.客戶分類 == type);
            }
            return data;
        }

        public 客戶資料 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }

        public IQueryable<string> Get客戶分類()
        {
            var data = this.All().Where(p => !p.是否已刪除).Select(b => b.客戶分類).Distinct();
            return data;
        }
    }

	public  interface I客戶資料Repository : IRepository<客戶資料>
	{

	}
}