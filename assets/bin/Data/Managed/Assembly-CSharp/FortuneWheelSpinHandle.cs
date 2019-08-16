using Network;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FortuneWheelSpinHandle : MonoBehaviour
{
	public float DEFAULT_ACTIVE_SCALE = 1.3f;

	public float DEFAULT_INACTIVE_SCALE = 0.8f;

	public float DEFAULT_SPIN_TIME_x1 = 5f;

	public float DEFAULT_SPIN_TIME_x10 = 1f;

	public float DEFAULT_SPIN_TIME_MULTI = 0.2f;

	public float DEFINE_REWARD_TIME_x1 = 1f;

	public float DEFINE_REWARD_TIME_x10 = 0.4f;

	public float DEFINE_REWARD_TIME_MULTI;

	public float MIN_AJUST_TIME_x1 = 0.8f;

	public float MIN_AJUST_TIME_x10 = 0.4f;

	public float MIN_AJUST_TIME_MULTI;

	public float SPIN_SPEED_MAX_x1 = 500f;

	public float SPIN_SPEED_MAX_x10 = 800f;

	public float SPIN_SPEED_MAX_MULTI = 1000f;

	public int SPIN_SPEED_MIN_X1 = 200;

	public int SPIN_SPEED_MIN_X10 = 200;

	public int SPIN_SPEED_MIN_MULTI = 1000;

	private float spinTimeDur;

	private float defineRewardTime;

	private float minAjustTime;

	private float spinMinSpeed;

	private float spinSpeed;

	public float DEFAULT_END_ACCELERATION = 100f;

	public int DEFAULT_WHEEL_ITEMS_LENGTH = 10;

	private Vector3[] wayPoints;

	private List<FortuneWheelSpinItem> initialSpinItem;

	private List<FortuneWheelSpinItem> pinItem;

	private List<FortuneWheelSpinItem> hiddenSpinItemList = new List<FortuneWheelSpinItem>();

	private List<FortuneWheelItem> itemList;

	private List<FortuneWheelItem> updatedItemList;

	private FortuneWheelReward currentReward;

	private float itemScaleMax = 1.2f;

	private float currentItemScale = 1f;

	private float currentDegree;

	private float spinTime;

	private float ajustTime;

	private float expectedDegree;

	private bool isStartSpin;

	private Vector3 hidePosition = new Vector3(1000f, 0f, 0f);

	public Transform _trans;

	private int currentIndexPos;

	private int itemDataLength;

	private bool isAjustSpeed;

	private Action<bool> spinEndAction;

	private GameObject spinPrefab;

	private float defaultDistaceScale;

	private FortuneWheelManager.SPIN_TYPE spinType;

	private int oppositeIndex;

	public FortuneWheelSpinHandle()
		: this()
	{
	}//IL_00e9: Unknown result type (might be due to invalid IL or missing references)
	//IL_00ee: Unknown result type (might be due to invalid IL or missing references)


	private void InitPosition()
	{
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_005b: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		wayPoints = (Vector3[])new Vector3[10];
		Vector3 zero = Vector3.get_zero();
		float num = 220f;
		Vector2 v = default(Vector2);
		v._002Ector(0f, 1f);
		for (int i = 0; i < 10; i++)
		{
			wayPoints[i] = zero;
			Vector2 val = Rotate(v, (float)i * 36f);
			val.Normalize();
			val *= num;
			wayPoints[i].x = wayPoints[i].x + val.x;
			wayPoints[i].y = wayPoints[i].y + val.y;
		}
	}

	public void IniSpin(List<FortuneWheelItem> _itemList, GameObject m_SpinItemPrefab)
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		defaultDistaceScale = DEFAULT_ACTIVE_SCALE - DEFAULT_INACTIVE_SCALE;
		_trans = this.get_transform();
		spinPrefab = m_SpinItemPrefab;
		InitPosition();
		itemList = _itemList;
		initialSpinItem = new List<FortuneWheelSpinItem>();
		pinItem = new List<FortuneWheelSpinItem>();
		itemDataLength = itemList.Count;
		for (int i = 0; i < DEFAULT_WHEEL_ITEMS_LENGTH; i++)
		{
			int index = i % itemDataLength;
			FortuneWheelSpinItem component = ResourceUtility.Realizes(m_SpinItemPrefab, this.get_transform(), 5).GetComponent<FortuneWheelSpinItem>();
			component.CreateItemIcon(itemList[index], i);
			component._trans.set_localPosition(wayPoints[i]);
			component.SetScale((i != 0) ? DEFAULT_INACTIVE_SCALE : DEFAULT_ACTIVE_SCALE);
			component.SetRotate((float)i * 36f);
			component.itemName = itemList[index].name;
			initialSpinItem.Add(component);
		}
		pinItem.AddRange(initialSpinItem);
	}

	public void Skip()
	{
		this.StartCoroutine(IESkip());
	}

	private IEnumerator IESkip()
	{
		while (!isAjustSpeed)
		{
			yield return null;
		}
		yield return (object)new WaitForEndOfFrame();
		if (isStartSpin)
		{
			_trans.set_localEulerAngles(new Vector3(0f, 0f, 0f - expectedDegree));
			ScaleItemFinal(expectedDegree);
			isStartSpin = false;
			if (spinEndAction != null)
			{
				spinEndAction(obj: false);
			}
		}
	}

	public void StartSpin(FortuneWheelData reward, FortuneWheelManager.SPIN_TYPE spinType, Action<bool> onEndAction, int rewardIndex = 0)
	{
		if (!isStartSpin && rewardIndex < reward.spinRewards.Count)
		{
			this.spinType = spinType;
			float num;
			switch (spinType)
			{
			case FortuneWheelManager.SPIN_TYPE.X1:
				num = DEFAULT_SPIN_TIME_x1;
				break;
			case FortuneWheelManager.SPIN_TYPE.X10:
				num = DEFAULT_SPIN_TIME_x10;
				break;
			default:
				num = DEFAULT_SPIN_TIME_MULTI;
				break;
			}
			spinTimeDur = num;
			float num2;
			switch (spinType)
			{
			case FortuneWheelManager.SPIN_TYPE.X1:
				num2 = DEFINE_REWARD_TIME_x1;
				break;
			case FortuneWheelManager.SPIN_TYPE.X10:
				num2 = DEFINE_REWARD_TIME_x10;
				break;
			default:
				num2 = DEFINE_REWARD_TIME_MULTI;
				break;
			}
			defineRewardTime = num2;
			float num3;
			switch (spinType)
			{
			case FortuneWheelManager.SPIN_TYPE.X1:
				num3 = MIN_AJUST_TIME_x1;
				break;
			case FortuneWheelManager.SPIN_TYPE.X10:
				num3 = MIN_AJUST_TIME_x10;
				break;
			default:
				num3 = MIN_AJUST_TIME_MULTI;
				break;
			}
			minAjustTime = num3;
			float num4;
			switch (spinType)
			{
			case FortuneWheelManager.SPIN_TYPE.X1:
				num4 = SPIN_SPEED_MAX_x1;
				break;
			case FortuneWheelManager.SPIN_TYPE.X10:
				num4 = SPIN_SPEED_MAX_x10;
				break;
			default:
				num4 = SPIN_SPEED_MAX_MULTI;
				break;
			}
			spinSpeed = num4;
			int num5;
			switch (spinType)
			{
			case FortuneWheelManager.SPIN_TYPE.X1:
				num5 = SPIN_SPEED_MIN_X1;
				break;
			case FortuneWheelManager.SPIN_TYPE.X10:
				num5 = SPIN_SPEED_MIN_X10;
				break;
			default:
				num5 = SPIN_SPEED_MIN_MULTI;
				break;
			}
			spinMinSpeed = num5;
			spinEndAction = onEndAction;
			spinEndAction(obj: true);
			currentReward = reward.spinRewards[rewardIndex];
			updatedItemList = reward.vaultInfo.itemList;
			spinTime = 0f;
			ajustTime = 0f;
			isStartSpin = true;
			isAjustSpeed = false;
		}
	}

	private void Update()
	{
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		if (!isStartSpin)
		{
			return;
		}
		currentDegree += Time.get_deltaTime() * spinSpeed;
		_trans.set_localEulerAngles(new Vector3(0f, 0f, 0f - currentDegree));
		if (currentDegree > 360f)
		{
			currentDegree -= 360f;
		}
		if (isAjustSpeed)
		{
			ajustTime += Time.get_deltaTime();
			float num = expectedDegree;
			float num2 = expectedDegree + 18f;
			if (spinTime < spinTimeDur)
			{
				spinSpeed -= Time.get_deltaTime() * DEFAULT_END_ACCELERATION;
			}
			else
			{
				if (spinType != FortuneWheelManager.SPIN_TYPE.X1 || (currentDegree >= num && currentDegree <= num2 && ajustTime >= minAjustTime))
				{
					_trans.set_localEulerAngles(new Vector3(0f, 0f, 0f - expectedDegree));
					ScaleItemFinal(expectedDegree);
					isStartSpin = false;
					spinEndAction(obj: false);
					return;
				}
				float num3 = spinSpeed - Time.get_deltaTime() * DEFAULT_END_ACCELERATION;
				if (num3 > 0f)
				{
					float num4 = spinSpeed;
					spinSpeed = num3;
					if (spinSpeed < spinMinSpeed)
					{
						spinSpeed = num4;
					}
				}
			}
		}
		else
		{
			spinSpeed -= Time.get_deltaTime() * DEFAULT_END_ACCELERATION;
		}
		ScaleItem();
		spinTime += Time.get_deltaTime();
		if (!isAjustSpeed)
		{
			isAjustSpeed = true;
			oppositeIndex = (currentIndexPos + 5) % 10;
			oppositeIndex = GetReplaceIndex(oppositeIndex);
			expectedDegree = (float)oppositeIndex * 36f;
			HandleResult(oppositeIndex);
		}
	}

	private void ScaleItem()
	{
		int num = GetCurrentIndexPos();
		FortuneWheelSpinItem fortuneWheelSpinItem = null;
		if (currentIndexPos != num)
		{
			FortuneWheelSpinItem fortuneWheelSpinItem2 = (from item in pinItem
			where item.indexPos == currentIndexPos
			select item).First();
			fortuneWheelSpinItem2.SetScale(DEFAULT_INACTIVE_SCALE);
			currentIndexPos = num;
		}
		fortuneWheelSpinItem = (from item in pinItem
		where item.indexPos == currentIndexPos
		select item).First();
		fortuneWheelSpinItem.SetScale(GetCurrentScale());
	}

	private void ScaleItemFinal(float deg)
	{
		int index = GetCurrentIndexFromDeg(deg);
		FortuneWheelSpinItem fortuneWheelSpinItem = (from item in pinItem
		where item.indexPos == index
		select item).First();
		fortuneWheelSpinItem.SetScale(DEFAULT_ACTIVE_SCALE);
		for (int i = 0; i < pinItem.Count; i++)
		{
			if (pinItem[i].indexPos != index)
			{
				pinItem[i].SetScale(DEFAULT_INACTIVE_SCALE);
			}
		}
	}

	private int GetReplaceIndex(int index)
	{
		index = ((index < DEFAULT_WHEEL_ITEMS_LENGTH) ? index : (index % DEFAULT_WHEEL_ITEMS_LENGTH));
		FortuneWheelSpinItem fortuneWheelSpinItem = (from i in pinItem
		where i.indexPos == index
		select i).First();
		if (fortuneWheelSpinItem.type != REWARD_TYPE.JACKPOT)
		{
			return index;
		}
		return GetReplaceIndex(++index);
	}

	private void HandleResult(int replaceIndex)
	{
		IEnumerable<FortuneWheelSpinItem> enumerable = from s in pinItem
		where s.id == currentReward.spinItemId
		select s;
		if (enumerable != null && enumerable.Count() > 0)
		{
			expectedDegree = (float)enumerable.First().indexPos * 36f;
		}
		else
		{
			ReplaceRewardIcon(replaceIndex);
		}
	}

	private void ReplaceRewardIcon(int replaceIndex)
	{
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0159: Unknown result type (might be due to invalid IL or missing references)
		if (currentReward == null)
		{
			return;
		}
		FortuneWheelSpinItem fortuneWheelSpinItem = null;
		IEnumerable<FortuneWheelSpinItem> enumerable = from s in initialSpinItem
		where s.id == currentReward.spinItemId
		select s;
		if (enumerable != null && enumerable.Count() > 0 && hiddenSpinItemList.IndexOf(enumerable.First()) > -1)
		{
			fortuneWheelSpinItem = enumerable.First();
		}
		else if (updatedItemList != null && updatedItemList.Count > 0)
		{
			IEnumerable<FortuneWheelItem> enumerable2 = from s in updatedItemList
			where s.id == currentReward.spinItemId
			select s;
			if (enumerable2 != null && enumerable2.Count() > 0)
			{
				FortuneWheelItem data = enumerable2.First();
				FortuneWheelSpinItem component = ResourceUtility.Realizes(spinPrefab, this.get_transform(), 5).GetComponent<FortuneWheelSpinItem>();
				component.CreateItemIcon(data, -1);
				component._trans.set_localPosition(hidePosition);
				fortuneWheelSpinItem = component;
				hiddenSpinItemList.Add(component);
				initialSpinItem.Add(component);
			}
		}
		FortuneWheelSpinItem fortuneWheelSpinItem2 = pinItem.First((FortuneWheelSpinItem s) => s.indexPos == replaceIndex);
		fortuneWheelSpinItem2._trans.set_localPosition(hidePosition);
		fortuneWheelSpinItem2.indexPos = -1;
		fortuneWheelSpinItem._trans.set_localPosition(wayPoints[replaceIndex]);
		fortuneWheelSpinItem.SetRotate((float)replaceIndex * 36f);
		fortuneWheelSpinItem.indexPos = replaceIndex;
		pinItem.Add(fortuneWheelSpinItem);
		pinItem.Remove(fortuneWheelSpinItem2);
		hiddenSpinItemList.Add(fortuneWheelSpinItem2);
		hiddenSpinItemList.Remove(fortuneWheelSpinItem);
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

	private int GetCurrentIndexFromDeg(float degree)
	{
		for (int i = 1; i < 10; i++)
		{
			float num = (float)i * 36f - 18f;
			float num2 = (float)i * 36f + 18f;
			if (degree > num && degree < num2)
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
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		float num = Mathf.Sin(degrees * ((float)Math.PI / 180f));
		float num2 = Mathf.Cos(degrees * ((float)Math.PI / 180f));
		float x = v.x;
		float y = v.y;
		v.x = num2 * x - num * y;
		v.y = num * x + num2 * y;
		return v;
	}

	private float GetCurrentScale()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		float num = (float)currentIndexPos * 36f;
		FortuneWheelSpinItem fortuneWheelSpinItem = (from item in pinItem
		where item.indexPos == currentIndexPos
		select item).First();
		Vector3 localEulerAngles = _trans.get_localEulerAngles();
		float z = localEulerAngles.z;
		Vector3 localEulerAngles2 = fortuneWheelSpinItem.get_transform().get_localEulerAngles();
		float num2 = z + localEulerAngles2.z;
		float num3 = (!(num2 >= 360f)) ? num2 : (num2 - 360f);
		float num4 = num;
		Vector3 localEulerAngles3 = _trans.get_localEulerAngles();
		float num5 = Mathf.Abs(num4 - localEulerAngles3.z);
		float num6 = num5 / 18f * defaultDistaceScale;
		Vector3 localScale = fortuneWheelSpinItem.get_transform().get_localScale();
		float num7 = num6 + localScale.z;
		return (!(num7 >= DEFAULT_ACTIVE_SCALE)) ? num7 : DEFAULT_ACTIVE_SCALE;
	}
}
