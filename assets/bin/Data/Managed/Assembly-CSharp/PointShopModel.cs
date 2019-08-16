using Network;
using System.Collections.Generic;

public class PointShopModel : BaseModel
{
	public const string URL = "ajax/pointshop/list";

	public readonly List<PointShop> result = new List<PointShop>();
}
