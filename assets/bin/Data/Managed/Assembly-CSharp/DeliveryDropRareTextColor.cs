using UnityEngine;

public class DeliveryDropRareTextColor
{
	public Color NormalColor;

	public Color RareColor;

	public Color SuperRareColor;

	public DeliveryDropRareTextColor()
		: this()
	{
	}

	public Color GetRarityColor(DELIVERY_DROP_DIFFICULTY type)
	{
		//IL_001a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
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
