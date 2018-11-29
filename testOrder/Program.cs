using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace testOrder
{
	class MainClass
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			List<People> list = new List<People>();
			list.Add(new People { Id = 1, Name = "a1" });
			list.Add(new People { Id = 2, Name = "a2" });
			list.Add(new People { Id = 3, Name = "a3" });

            // 定义排序规则
			Action<IOrderable<People>> orderBy = o => o.Asc(j => j.Id)
			                                           .ThenDesc(j => j.Name);
            // 数据仓储
			Repository<People> repository = new Repository<People>(list.AsQueryable());
           
			// 获取结果 
			List<People> result=repository.GetModel(orderBy).ToList();

            // 输出
			result.ToList().ForEach(i => Console.WriteLine(i.Id));
		}

	}
   

}
