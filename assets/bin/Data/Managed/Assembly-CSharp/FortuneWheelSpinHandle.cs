using Network;
using System.Collections.Generic;
using UnityEngine;

public class FortuneWheelSpinHandle : MonoBehaviour
{
	private Vector3[] wayPoints;

	private List<FortuneWheelSpinItem> spinItemList;

	private List<FortuneWheelItem> itemList;

	private int winId;

	private float itemScaleMax = 1.2f;

	private float currentItemScale = 1f;

	private float spinSpdMax = 300f;

	private float spinSpd;

	private float currentDegree;

	private float spinTime;

	private bool isStartSpin;

	private Vector3 hidePosition = new Vector3(1000f, 0f, 0f);

	public Transform _trans;

	private int currentIndexPos;

	private void InitPosition()
	{
		wayPoints = new Vector3[10];
		Vector3 zero = Vector3.zero;
		float d = 220f;
		Vector2 v = new Vector2(0f, 1f);
		for (int i = 0; i < 10; i++)
		{
			wayPoints[i] = zero;
			Vector2 a = Rotate(v, (float)i * 36f);
			a.Normalize();
			a *= d;
			wayPoints[i].x = wayPoints[i].x + a.x;
			wayPoints[i].y = wayPoints[i].y + a.y;
		}
	}

	public void IniSpin(List<FortuneWheelItem> _itemList, GameObject m_SpinItemPrefab)
	{
		_trans = base.transform;
		InitPosition();
		itemList = _itemList;
		spinItemList = new List<FortuneWheelSpinItem>();
		int count = itemList.Count;
		if (count < 10)
		{
			for (int i = 0; i < 10; i++)
			{
				int index = i % count;
				FortuneWheelSpinItem component = ResourceUtility.Realizes(m_SpinItemPrefab, base.transform, 5).GetComponent<FortuneWheelSpinItem>();
				component.CreateItemIcon(itemList[index].id, (REWARD_TYPE)itemList[index].rewardType, (uint)itemList[index].rewardId);
				component._trans.localPosition = wayPoints[i];
				component.SetRotate((float)i * 36f);
				spinItemList.Add(component);
			}
		}
		else
		{
			for (int j = 0; j < count; j++)
			{
				FortuneWheelSpinItem component2 = ResourceUtility.Realizes(m_SpinItemPrefab, base.transform, 5).GetComponent<FortuneWheelSpinItem>();
				component2.CreateItemIcon(itemList[j].id, (REWARD_TYPE)itemList[j].rewardType, (uint)itemList[j].rewardId);
				if (j < 10)
				{
					int num = j % 10;
					component2._trans.localPosition = wayPoints[num];
					component2.SetRotate((float)num * 36f);
				}
				else
				{
					component2._trans.localPosition = hidePosition;
				}
				spinItemList.Add(component2);
			}
		}
	}

	public void StartSpin(int _win)
	{
		winId = _win;
		spinSpd = spinSpdMax;
		spinTime = 0f;
		isStartSpin = true;
	}

	private void Update()
	{
		if (isStartSpin)
		{
			currentDegree += Time.deltaTime * spinSpd;
			_trans.localEulerAngles = new Vector3(0f, 0f, 0f - currentDegree);
			if (currentDegree > 360f)
			{
				currentDegree -= 360f;
			}
			if (spinTime < 1f)
			{
				spinSpd -= Time.deltaTime * 200f;
			}
			else if (spinSpd < 10f)
			{
				isStartSpin = false;
			}
			else
			{
				spinSpd -= Time.deltaTime * 50f;
			}
			int num = GetCurrentIndexPos();
			if (currentIndexPos != num)
			{
				currentItemScale = 1f;
				spinItemList[currentIndexPos].SetScale(1f);
				currentIndexPos = num;
			}
			spinItemList[currentIndexPos].SetScale(currentItemScale);
			if (CheckLocalScale(currentIndexPos))
			{
				currentItemScale += Time.deltaTime;
			}
			else
			{
				currentItemScale -= Time.deltaTime;
			}
			if (currentItemScale > itemScaleMax)
			{
				currentItemScale = itemScaleMax;
			}
			else if (currentItemScale < 1f)
			{
				currentItemScale = 1f;
			}
			spinTime += Time.deltaTime;
		}
	}

	private int GetCurrentIndexPos()
	{
		for (int i = 1; i < 10; i++)
		{
			float num = (float)i * 36f - 18f;
			float num2 = (float)i * 36f + 18f;
			if (currentDegree > num && currentDegree < num2)
			{
				return i;
			}
		}
		return 0;
	}

	private bool CheckLocalScale(int indexPos)
	{
		float num = (float)indexPos * 36f;
		if (num == 0f)
		{
			float num2 = Mathf.Abs(currentDegree - num);
			if (currentDegree > 340f)
			{
				num2 = 360f - currentDegree;
			}
			if (currentDegree > num)
			{
				return true;
			}
			return false;
		}
		float num3 = Mathf.Abs(currentDegree - num);
		if (currentDegree > num)
		{
			return true;
		}
		return false;
	}

	public Vector2 Rotate(Vector2 v, float degrees)
	{
		float num = Mathf.Sin(degrees * 0.0174532924f);
		float num2 = Mathf.Cos(degrees * 0.0174532924f);
		float x = v.x;
		float y = v.y;
		v.x = num2 * x - num * y;
		v.y = num * x + num2 * y;
		return v;
	}
}
