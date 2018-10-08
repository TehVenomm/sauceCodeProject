using System;
using UnityEngine;

[RequireComponent(typeof(UISprite))]
public class ResultExpGaugeCtrl
{
	public AnimationCurve lvUpCurve;

	public AnimationCurve lastDirectionCurve;

	private float time = -1f;

	private float totalExp;

	private int startLevel;

	private bool isProgressAnim;

	[HideInInspector]
	public UISprite progress;

	[HideInInspector]
	public float getExp;

	[HideInInspector]
	public float addCountExpValue;

	[HideInInspector]
	public float startExp;

	[HideInInspector]
	public int nowLevel;

	[HideInInspector]
	public int remainLevelUpCnt;

	[HideInInspector]
	public int beforeUpdateLevel;

	[HideInInspector]
	public UserLevelTable.UserLevelData levelTable;

	[HideInInspector]
	public UserLevelTable.UserLevelData nowLevelTable;

	private bool skip;

	public Action callBack;

	public Action<bool, int, ResultExpGaugeCtrl> OnUpdate;

	public bool isEnd
	{
		get;
		private set;
	}

	public ResultExpGaugeCtrl()
		: this()
	{
	}

	public void InitDirection(Action<ResultExpGaugeCtrl> initialize_call = null)
	{
		progress = this.GetComponent<UISprite>();
		initialize_call?.Invoke(this);
		totalExp = getExp + startExp;
		startLevel = nowLevel;
		if (nowLevel < Singleton<UserLevelTable>.I.GetMaxLevel())
		{
			time = -1f;
			UpdateAddCountExp(nowLevel);
			SetFillAmount((float)((int)startExp - (int)nowLevelTable.needExp) / (float)((int)levelTable.needExp - (int)nowLevelTable.needExp));
		}
		else
		{
			SetFillAmount(0f);
			getExp = 0f;
		}
	}

	public void SetFillAmount(float amount)
	{
		progress.fillAmount = amount;
	}

	public void StartAnim()
	{
		isProgressAnim = true;
	}

	public void Skip()
	{
		skip = true;
	}

	private void UpdateAddCountExp(int now_level)
	{
		levelTable = Singleton<UserLevelTable>.I.GetLevelTable(now_level + 1);
		nowLevelTable = Singleton<UserLevelTable>.I.GetLevelTable(now_level);
		if (time >= 0f)
		{
			UpdateCurveEvaluate();
		}
		else
		{
			time = 0f;
			addCountExpValue = 0f;
		}
	}

	public void UpdateCurveEvaluate()
	{
		bool flag = remainLevelUpCnt > 0;
		float num = (float)((int)levelTable.needExp - (int)nowLevelTable.needExp);
		float num2 = flag ? num : ((nowLevel != startLevel) ? (totalExp - (float)(int)nowLevelTable.needExp) : getExp);
		float num3 = (float)(int)nowLevelTable.needExp;
		if (nowLevel == startLevel)
		{
			num3 = startExp;
		}
		float deltaTime = Time.get_deltaTime();
		time += deltaTime;
		if (flag)
		{
			float num4 = lvUpCurve.get_keys()[lvUpCurve.get_length() - 1].get_time();
			addCountExpValue = lvUpCurve.Evaluate(Mathf.Clamp(time, 0f, num4)) * num2 + num3;
		}
		else
		{
			float num5 = lastDirectionCurve.get_keys()[lastDirectionCurve.get_length() - 1].get_time();
			addCountExpValue = lastDirectionCurve.Evaluate(Mathf.Clamp(time, 0f, num5)) * num2 + num3;
		}
	}

	public void Update()
	{
		if (isProgressAnim)
		{
			if (getExp > 0f)
			{
				bool flag = false;
				bool flag2 = false;
				beforeUpdateLevel = nowLevel;
				UpdateCurveEvaluate();
				float num = addCountExpValue;
				if (num >= totalExp || skip)
				{
					flag = skip;
					skip = false;
					num = totalExp;
					isProgressAnim = false;
					isEnd = true;
					if (callBack != null)
					{
						callBack.Invoke();
					}
				}
				float num2 = 0f;
				do
				{
					num2 = (num - (float)(int)nowLevelTable.needExp) / (float)((int)levelTable.needExp - (int)nowLevelTable.needExp);
					if (num2 >= 1f)
					{
						flag2 = true;
						progress.fillAmount = 0f;
						nowLevel++;
						remainLevelUpCnt--;
						time = -1f;
						UpdateAddCountExp(nowLevel);
						if ((int)nowLevelTable.lv >= Singleton<UserLevelTable>.I.GetMaxLevel())
						{
							num2 = 0f;
							progress.fillAmount = num2;
						}
						if (!flag)
						{
							num = (float)(int)nowLevelTable.needExp;
						}
					}
					else
					{
						progress.fillAmount = num2;
					}
				}
				while (num2 >= 1f);
				if (OnUpdate != null)
				{
					OnUpdate.Invoke(flag2, (int)(num - startExp), this);
				}
			}
			else
			{
				isProgressAnim = false;
				isEnd = true;
				if (callBack != null)
				{
					callBack.Invoke();
				}
			}
		}
	}
}
