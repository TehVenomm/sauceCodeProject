using System.Collections.Generic;
using UnityEngine;

public class TestRaycast
{
	private class CastHitInfo
	{
		public float distance;

		public bool faceToEnd;

		public Collider collider;

		public bool enable = true;

		public bool checkCollider;
	}

	public GameObject checkObject;

	public GameObject endObject;

	public TestRaycast()
		: this()
	{
	}

	private void Update()
	{
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		if (!(checkObject == null) && !(endObject == null))
		{
			checkObject.get_transform().set_position(CheckWarpPos());
		}
	}

	private Vector3 CheckWarpPos()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0084: Unknown result type (might be due to invalid IL or missing references)
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ea: Expected O, but got Unknown
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0185: Unknown result type (might be due to invalid IL or missing references)
		//IL_018a: Expected O, but got Unknown
		//IL_038f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0394: Unknown result type (might be due to invalid IL or missing references)
		//IL_039b: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a7: Unknown result type (might be due to invalid IL or missing references)
		SphereCollider val = this.GetComponent<Collider>() as SphereCollider;
		float radius = val.get_radius();
		Vector3 lossyScale = this.get_transform().get_lossyScale();
		float num = radius * lossyScale.x;
		Vector3 position = this.get_transform().get_position();
		Vector3 position2 = endObject.get_transform().get_position();
		position.y = num;
		position2.y = num;
		Vector3 val2 = position2 - position;
		float magnitude = val2.get_magnitude();
		float num2 = 5f;
		float num3 = 2f;
		if (num2 >= magnitude)
		{
			return this.get_transform().get_position();
		}
		List<CastHitInfo> list = new List<CastHitInfo>();
		RaycastHit[] array = Physics.SphereCastAll(position2, num, -val2, magnitude, 393728);
		int i = 0;
		for (int num4 = array.Length; i < num4; i++)
		{
			CastHitInfo castHitInfo = new CastHitInfo();
			castHitInfo.distance = array[i].get_distance();
			castHitInfo.faceToEnd = true;
			castHitInfo.collider = array[i].get_collider();
			list.Add(castHitInfo);
		}
		CastHitInfo castHitInfo2 = new CastHitInfo();
		castHitInfo2.distance = 0f;
		castHitInfo2.faceToEnd = false;
		castHitInfo2.collider = null;
		list.Add(castHitInfo2);
		array = Physics.SphereCastAll(position, num, val2, magnitude, 393728);
		int j = 0;
		for (int num5 = array.Length; j < num5; j++)
		{
			CastHitInfo castHitInfo3 = new CastHitInfo();
			castHitInfo3.distance = magnitude - array[j].get_distance();
			castHitInfo3.faceToEnd = false;
			castHitInfo3.collider = array[j].get_collider();
			list.Add(castHitInfo3);
		}
		CastHitInfo castHitInfo4 = new CastHitInfo();
		castHitInfo4.distance = magnitude;
		castHitInfo4.faceToEnd = true;
		castHitInfo4.collider = null;
		list.Add(castHitInfo4);
		list.Sort(delegate(CastHitInfo a, CastHitInfo b)
		{
			float num9 = a.distance - b.distance;
			if (num9 == 0f)
			{
				if (a.faceToEnd == b.faceToEnd)
				{
					return 0;
				}
				return a.faceToEnd ? 1 : (-1);
			}
			return (num9 > 0f) ? 1 : (-1);
		});
		int k = 0;
		for (int count = list.Count; k < count; k++)
		{
			CastHitInfo castHitInfo5 = list[k];
			if (!castHitInfo5.checkCollider && !(castHitInfo5.collider == null))
			{
				int num6 = k;
				while (0 <= num6 && num6 < count)
				{
					CastHitInfo castHitInfo6 = list[num6];
					if (num6 != k)
					{
						if (castHitInfo6.collider == castHitInfo5.collider)
						{
							castHitInfo6.checkCollider = true;
							break;
						}
						castHitInfo6.enable = false;
					}
					num6 = ((!castHitInfo5.faceToEnd) ? (num6 - 1) : (num6 + 1));
				}
			}
		}
		float num7 = magnitude;
		for (int num8 = list.Count - 2; num8 >= 0; num8--)
		{
			CastHitInfo castHitInfo7 = list[num8];
			CastHitInfo castHitInfo8 = list[num8 + 1];
			if (castHitInfo7.enable && castHitInfo8.enable && !castHitInfo7.faceToEnd && castHitInfo8.faceToEnd)
			{
				if (castHitInfo7.distance <= num2 && castHitInfo8.distance >= num2)
				{
					num7 = num2;
					break;
				}
				if (castHitInfo7.distance >= num2)
				{
					num7 = castHitInfo7.distance;
				}
				else if (castHitInfo8.distance >= num2 - num3)
				{
					num7 = castHitInfo8.distance;
					break;
				}
			}
		}
		return endObject.get_transform().get_position() - val2.get_normalized() * num7;
	}

	private Vector3 CheckWarpPos2()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_003f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0078: Unknown result type (might be due to invalid IL or missing references)
		//IL_007d: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008c: Unknown result type (might be due to invalid IL or missing references)
		//IL_008e: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0111: Unknown result type (might be due to invalid IL or missing references)
		//IL_0442: Unknown result type (might be due to invalid IL or missing references)
		//IL_0447: Unknown result type (might be due to invalid IL or missing references)
		//IL_044e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0455: Unknown result type (might be due to invalid IL or missing references)
		//IL_045a: Unknown result type (might be due to invalid IL or missing references)
		SphereCollider val = this.GetComponent<Collider>() as SphereCollider;
		float radius = val.get_radius();
		Vector3 lossyScale = this.get_transform().get_lossyScale();
		float num = radius * lossyScale.x;
		Vector3 position = this.get_transform().get_position();
		Vector3 position2 = endObject.get_transform().get_position();
		position.y = num;
		position2.y = num;
		Vector3 val2 = position2 - position;
		float magnitude = val2.get_magnitude();
		float num2 = 5f;
		if (num2 >= magnitude)
		{
			return this.get_transform().get_position();
		}
		List<CastHitInfo> list = new List<CastHitInfo>();
		RaycastHit[] array = Physics.SphereCastAll(position2, num, -val2, magnitude, 393728);
		int i = 0;
		for (int num3 = array.Length; i < num3; i++)
		{
			CastHitInfo castHitInfo = new CastHitInfo();
			castHitInfo.distance = array[i].get_distance();
			castHitInfo.faceToEnd = true;
			list.Add(castHitInfo);
		}
		CastHitInfo castHitInfo2 = new CastHitInfo();
		castHitInfo2.distance = 0f;
		castHitInfo2.faceToEnd = false;
		list.Add(castHitInfo2);
		array = Physics.SphereCastAll(position, num, val2, magnitude, 393728);
		int j = 0;
		for (int num4 = array.Length; j < num4; j++)
		{
			CastHitInfo castHitInfo3 = new CastHitInfo();
			castHitInfo3.distance = magnitude - array[j].get_distance();
			castHitInfo3.faceToEnd = false;
			list.Add(castHitInfo3);
		}
		CastHitInfo castHitInfo4 = new CastHitInfo();
		castHitInfo4.distance = magnitude;
		castHitInfo4.faceToEnd = true;
		list.Add(castHitInfo4);
		list.Sort(delegate(CastHitInfo a, CastHitInfo b)
		{
			float num7 = a.distance - b.distance;
			if (num7 == 0f)
			{
				if (a.faceToEnd == b.faceToEnd)
				{
					return 0;
				}
				return a.faceToEnd ? 1 : (-1);
			}
			return (num7 > 0f) ? 1 : (-1);
		});
		Debug.Log((object)"----------------------------------------");
		int k = 0;
		for (int count = list.Count; k < count; k++)
		{
			Debug.Log((object)("############ : " + list[k].distance + ", " + list[k].faceToEnd));
		}
		int num5 = 0;
		CastHitInfo castHitInfo5 = null;
		while (num5 < list.Count)
		{
			CastHitInfo castHitInfo6 = list[num5];
			if (castHitInfo5 == null)
			{
				castHitInfo5 = castHitInfo6;
				num5++;
			}
			else if (castHitInfo6.faceToEnd == castHitInfo5.faceToEnd)
			{
				if (castHitInfo6.faceToEnd)
				{
					list.RemoveAt(num5);
				}
				else
				{
					list.RemoveAt(num5 - 1);
					castHitInfo5 = castHitInfo6;
				}
			}
			else
			{
				castHitInfo5 = castHitInfo6;
				num5++;
			}
		}
		num5 = 0;
		while (num5 + 1 < list.Count)
		{
			CastHitInfo castHitInfo7 = list[num5];
			CastHitInfo castHitInfo8 = list[num5 + 1];
			if (castHitInfo7.distance == castHitInfo8.distance && castHitInfo7.faceToEnd != castHitInfo8.faceToEnd)
			{
				list.RemoveRange(num5, 2);
			}
			else
			{
				num5++;
			}
		}
		Debug.Log((object)"----------------------------------------");
		int l = 0;
		for (int count2 = list.Count; l < count2; l++)
		{
			Debug.Log((object)("############ : " + list[l].distance + ", " + list[l].faceToEnd));
		}
		float num6 = magnitude;
		int m = 0;
		for (int count3 = list.Count; m + 1 < count3; m++)
		{
			CastHitInfo castHitInfo9 = list[m];
			CastHitInfo castHitInfo10 = list[m + 1];
			if (!castHitInfo9.faceToEnd && castHitInfo10.faceToEnd)
			{
				if (castHitInfo9.distance >= num2)
				{
					num6 = castHitInfo9.distance;
					break;
				}
				if (castHitInfo9.distance <= num2 && castHitInfo10.distance >= num2)
				{
					num6 = num2;
					break;
				}
			}
		}
		Debug.Log((object)("res : " + num6));
		return endObject.get_transform().get_position() - val2.get_normalized() * num6;
	}

	private Vector3 CheckWarpPos3()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00df: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_017b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_0187: Unknown result type (might be due to invalid IL or missing references)
		//IL_018e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0193: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		float num = 5f;
		SphereCollider val = this.GetComponent<Collider>() as SphereCollider;
		Vector3 val2 = this.get_transform().get_position() - endObject.get_transform().get_position();
		float magnitude = val2.get_magnitude();
		if (magnitude < num)
		{
			return endObject.get_transform().get_position();
		}
		bool flag = true;
		Vector3 position = endObject.get_transform().get_position();
		float radius = val.get_radius();
		Vector3 lossyScale = this.get_transform().get_lossyScale();
		RaycastHit val3 = default(RaycastHit);
		if (Physics.SphereCast(position, radius * lossyScale.x, val2, ref val3, magnitude, 393728) && val3.get_distance() < num)
		{
			flag = false;
		}
		if (flag)
		{
			return endObject.get_transform().get_position() + val2.get_normalized() * num;
		}
		Vector3 val4 = endObject.get_transform().get_position() - this.get_transform().get_position();
		Vector3 position2 = this.get_transform().get_position();
		float radius2 = val.get_radius();
		Vector3 lossyScale2 = this.get_transform().get_lossyScale();
		RaycastHit[] array = Physics.SphereCastAll(position2, radius2 * lossyScale2.x, val4, magnitude - num, 393728);
		if (array.Length > 0)
		{
			float num2 = 0f;
			int i = 0;
			for (int num3 = array.Length; i < num3; i++)
			{
				if (num2 < array[i].get_distance())
				{
					num2 = array[i].get_distance();
				}
			}
			return this.get_transform().get_position() + val4.get_normalized() * num2;
		}
		return endObject.get_transform().get_position();
	}
}
