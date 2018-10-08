using System.Collections.Generic;
using UnityEngine;

public class TestRaycast : MonoBehaviour
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

	private void Update()
	{
		if (!((Object)checkObject == (Object)null) && !((Object)endObject == (Object)null))
		{
			checkObject.transform.position = CheckWarpPos();
		}
	}

	private Vector3 CheckWarpPos()
	{
		SphereCollider sphereCollider = GetComponent<Collider>() as SphereCollider;
		float radius = sphereCollider.radius;
		Vector3 lossyScale = base.transform.lossyScale;
		float num = radius * lossyScale.x;
		Vector3 position = base.transform.position;
		Vector3 position2 = endObject.transform.position;
		position.y = num;
		position2.y = num;
		Vector3 vector = position2 - position;
		float magnitude = vector.magnitude;
		float num2 = 5f;
		float num3 = 2f;
		if (num2 >= magnitude)
		{
			return base.transform.position;
		}
		List<CastHitInfo> list = new List<CastHitInfo>();
		RaycastHit[] array = Physics.SphereCastAll(position2, num, -vector, magnitude, 393728);
		int i = 0;
		for (int num4 = array.Length; i < num4; i++)
		{
			CastHitInfo castHitInfo = new CastHitInfo();
			castHitInfo.distance = array[i].distance;
			castHitInfo.faceToEnd = true;
			castHitInfo.collider = array[i].collider;
			list.Add(castHitInfo);
		}
		CastHitInfo castHitInfo2 = new CastHitInfo();
		castHitInfo2.distance = 0f;
		castHitInfo2.faceToEnd = false;
		castHitInfo2.collider = null;
		list.Add(castHitInfo2);
		array = Physics.SphereCastAll(position, num, vector, magnitude, 393728);
		int j = 0;
		for (int num5 = array.Length; j < num5; j++)
		{
			CastHitInfo castHitInfo3 = new CastHitInfo();
			castHitInfo3.distance = magnitude - array[j].distance;
			castHitInfo3.faceToEnd = false;
			castHitInfo3.collider = array[j].collider;
			list.Add(castHitInfo3);
		}
		CastHitInfo castHitInfo4 = new CastHitInfo();
		castHitInfo4.distance = magnitude;
		castHitInfo4.faceToEnd = true;
		castHitInfo4.collider = null;
		list.Add(castHitInfo4);
		list.Sort(delegate(CastHitInfo a, CastHitInfo b)
		{
			float num8 = a.distance - b.distance;
			if (num8 == 0f)
			{
				if (a.faceToEnd == b.faceToEnd)
				{
					return 0;
				}
				return a.faceToEnd ? 1 : (-1);
			}
			return (num8 > 0f) ? 1 : (-1);
		});
		int k = 0;
		for (int count = list.Count; k < count; k++)
		{
			CastHitInfo castHitInfo5 = list[k];
			if (!castHitInfo5.checkCollider && !((Object)castHitInfo5.collider == (Object)null))
			{
				int num6 = k;
				while (0 <= num6 && num6 < count)
				{
					CastHitInfo castHitInfo6 = list[num6];
					if (num6 != k)
					{
						if ((Object)castHitInfo6.collider == (Object)castHitInfo5.collider)
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
		float d = magnitude;
		for (int num7 = list.Count - 2; num7 >= 0; num7--)
		{
			CastHitInfo castHitInfo7 = list[num7];
			CastHitInfo castHitInfo8 = list[num7 + 1];
			if (castHitInfo7.enable && castHitInfo8.enable && !castHitInfo7.faceToEnd && castHitInfo8.faceToEnd)
			{
				if (castHitInfo7.distance <= num2 && castHitInfo8.distance >= num2)
				{
					d = num2;
					break;
				}
				if (castHitInfo7.distance >= num2)
				{
					d = castHitInfo7.distance;
				}
				else if (castHitInfo8.distance >= num2 - num3)
				{
					d = castHitInfo8.distance;
					break;
				}
			}
		}
		return endObject.transform.position - vector.normalized * d;
	}

	private Vector3 CheckWarpPos2()
	{
		SphereCollider sphereCollider = GetComponent<Collider>() as SphereCollider;
		float radius = sphereCollider.radius;
		Vector3 lossyScale = base.transform.lossyScale;
		float num = radius * lossyScale.x;
		Vector3 position = base.transform.position;
		Vector3 position2 = endObject.transform.position;
		position.y = num;
		position2.y = num;
		Vector3 vector = position2 - position;
		float magnitude = vector.magnitude;
		float num2 = 5f;
		if (num2 >= magnitude)
		{
			return base.transform.position;
		}
		List<CastHitInfo> list = new List<CastHitInfo>();
		RaycastHit[] array = Physics.SphereCastAll(position2, num, -vector, magnitude, 393728);
		int i = 0;
		for (int num3 = array.Length; i < num3; i++)
		{
			CastHitInfo castHitInfo = new CastHitInfo();
			castHitInfo.distance = array[i].distance;
			castHitInfo.faceToEnd = true;
			list.Add(castHitInfo);
		}
		CastHitInfo castHitInfo2 = new CastHitInfo();
		castHitInfo2.distance = 0f;
		castHitInfo2.faceToEnd = false;
		list.Add(castHitInfo2);
		array = Physics.SphereCastAll(position, num, vector, magnitude, 393728);
		int j = 0;
		for (int num4 = array.Length; j < num4; j++)
		{
			CastHitInfo castHitInfo3 = new CastHitInfo();
			castHitInfo3.distance = magnitude - array[j].distance;
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
		Debug.Log("----------------------------------------");
		int k = 0;
		for (int count = list.Count; k < count; k++)
		{
			Debug.Log("############ : " + list[k].distance + ", " + list[k].faceToEnd);
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
		Debug.Log("----------------------------------------");
		int l = 0;
		for (int count2 = list.Count; l < count2; l++)
		{
			Debug.Log("############ : " + list[l].distance + ", " + list[l].faceToEnd);
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
		Debug.Log("res : " + num6);
		return endObject.transform.position - vector.normalized * num6;
	}

	private Vector3 CheckWarpPos3()
	{
		float num = 5f;
		SphereCollider sphereCollider = GetComponent<Collider>() as SphereCollider;
		Vector3 direction = base.transform.position - endObject.transform.position;
		float magnitude = direction.magnitude;
		if (magnitude < num)
		{
			return endObject.transform.position;
		}
		bool flag = true;
		Vector3 position = endObject.transform.position;
		float radius = sphereCollider.radius;
		Vector3 lossyScale = base.transform.lossyScale;
		if (Physics.SphereCast(position, radius * lossyScale.x, direction, out RaycastHit hitInfo, magnitude, 393728) && hitInfo.distance < num)
		{
			flag = false;
		}
		if (flag)
		{
			return endObject.transform.position + direction.normalized * num;
		}
		Vector3 direction2 = endObject.transform.position - base.transform.position;
		Vector3 position2 = base.transform.position;
		float radius2 = sphereCollider.radius;
		Vector3 lossyScale2 = base.transform.lossyScale;
		RaycastHit[] array = Physics.SphereCastAll(position2, radius2 * lossyScale2.x, direction2, magnitude - num, 393728);
		if (array.Length > 0)
		{
			float num2 = 0f;
			int i = 0;
			for (int num3 = array.Length; i < num3; i++)
			{
				if (num2 < array[i].distance)
				{
					num2 = array[i].distance;
				}
			}
			return base.transform.position + direction2.normalized * num2;
		}
		return endObject.transform.position;
	}
}
