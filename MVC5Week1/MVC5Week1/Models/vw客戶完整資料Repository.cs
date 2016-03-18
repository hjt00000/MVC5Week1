using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Week1.Models
{   
	public  class vw客戶完整資料Repository : EFRepository<vw客戶完整資料>, Ivw客戶完整資料Repository
	{

	}

	public  interface Ivw客戶完整資料Repository : IRepository<vw客戶完整資料>
	{

	}
}