## 大叔关于动态排序的说明
1. 排序所需要的接口
`IOrderable`提供了排序规则，升序与降序
2. 对排序的实现
`Orderable`对排序的实现，主要对`可查询结果集IQueryable`的OrderBy方法和OrderByDescending方法进行封装
3. 简单的使用排序接口
```
// 定义排序条件
Action<IOrderable<People>> orderByDesc = o => o.Desc(j => j.Id);
var linq = new Orderable<People>(list.AsQueryable());
orderByDesc(linq);
linq.Queryable.ToList().ForEach(i => Console.WriteLine(i.Id));
```
4. 在方法中使用这个排序接口，它是方法的参数，接收一个排序的结构
我们在定义方法时，将排序做为方法参数进行传递，例如
```
public IQueryable<TEntity> GetModel(Action<IOrderable<TEntity>> orderBy)
{

}
```
> 而这个方法在使用`排序`参数时，格式基本是一至的，都是将需要排序的集合传给`orderBy`委托，然后返回值就是被排序的结果。
```
var linq = new Orderable<TEntity>(GetModel());
orderBy(linq);
return linq.Queryable;
```
5. 程序外部的调用
我们在外部主要动态构建排序条件即可，`orderBy`这个条件会做为参数传到方法里，方法的返回值为一个排序后的结果
```
// 定义排序规则
Action<IOrderable<People>> orderBy = o => o.Asc(j => j.Id)
			                               .ThenDesc(j => j.Name);
// 数据仓储
Repository<People> repository = new Repository<People>(list.AsQueryable());

// 获取结果 
List<People> result=repository.GetModel(orderBy).ToList();
```
