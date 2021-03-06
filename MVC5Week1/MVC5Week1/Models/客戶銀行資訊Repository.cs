using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Week1.Models
{   
	public  class 客戶銀行資訊Repository : EFRepository<客戶銀行資訊>, I客戶銀行資訊Repository
	{
        public override IQueryable<客戶銀行資訊> All()
        {
            return base.All().Include("客戶資料").Where(p => !p.是否已刪除).OrderBy(p => p.Id);  
        }

        public IQueryable<客戶銀行資訊> All(string QueryName)
        {
            if(!string.IsNullOrEmpty(QueryName))
            {
                return this.All().Where(p => p.銀行名稱.Contains(QueryName));
            } else
            {
                return this.All();
            }
        }

        public 客戶銀行資訊 Find(int id)
        {
            return this.All().FirstOrDefault(p => p.Id == id);
        }
    }

	public  interface I客戶銀行資訊Repository : IRepository<客戶銀行資訊>
	{

	}
}