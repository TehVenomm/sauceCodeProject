using UnityEngine;

internal static class PLACE_Extention
{
	public static Vector2 GetVector2(this PLACE place)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		switch (place)
		{
		case PLACE.FRONT:
			return Vector2.get_up();
		case PLACE.RIGHT:
			return Vector2.get_right();
		case PLACE.LEFT:
			return Vector2.get_left();
		case PLACE.BACK:
			return Vector2.get_down();
		default:
			return Vector2.get_zero();
		}
	}

	public static Vector3 GetVector3(this PLACE place)
	{
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0035: Unknown result type (might be due to invalid IL or missing references)
		switch (place)
		{
		case PLACE.FRONT:
			return Vector3.get_forward();
		case PLACE.RIGHT:
			return Vector3.get_right();
		case PLACE.LEFT:
			return Vector3.get_left();
		case PLACE.BACK:
			return Vector3.get_back();
		default:
			return Vector3.get_zero();
		}
	}

	public static PLACE Reverse(this PLACE place)
	{
		switch (place)
		{
		case PLACE.FRONT:
			return PLACE.BACK;
		case PLACE.RIGHT:
			return PLACE.LEFT;
		case PLACE.LEFT:
			return PLACE.RIGHT;
		case PLACE.BACK:
			return PLACE.FRONT;
		default:
			return place;
		}
	}
}
