using UnityEngine;

public class DeliveryDropRareTextColor : MonoBehaviour
{
	public Color NormalColor;

	public Color RareColor;

	public Color SuperRareColor;

	public Color GetRarityColor(DELIVERY_DROP_DIFFICULTY type)
	{
		switch (type)
		{
		default:
			return NormalColor;
		case DELIVERY_DROP_DIFFICULTY.RARE:
			return RareColor;
		case DELIVERY_DROP_DIFFICULTY.SUPER_RARE:
			return SuperRareColor;
		}
	}
}
