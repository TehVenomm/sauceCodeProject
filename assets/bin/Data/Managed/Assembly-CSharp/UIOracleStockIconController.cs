using UnityEngine;

public class UIOracleStockIconController : MonoBehaviour
{
	[SerializeField]
	private UISprite baseSprite;

	[SerializeField]
	private UISprite stockSprite;

	public bool Stocked => stockSprite.gameObject.activeSelf;

	public void Initialize(int index, int depth, bool stocked = false)
	{
		baseSprite.depth = depth;
		stockSprite.depth = depth + 1;
		Stock(stocked);
	}

	public void Stock(bool enable)
	{
		if (enable != stockSprite.gameObject.activeSelf)
		{
			stockSprite.gameObject.SetActive(enable);
		}
	}
}
