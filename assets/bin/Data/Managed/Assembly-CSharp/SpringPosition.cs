using UnityEngine;

[AddComponentMenu("NGUI/Tween/Spring Position")]
public class SpringPosition
{
	public delegate void OnFinished();

	public static SpringPosition current;

	public Vector3 target = Vector3.get_zero();

	public float strength = 10f;

	public bool worldSpace;

	public bool ignoreTimeScale;

	public bool updateScrollView;

	public OnFinished onFinished;

	[SerializeField]
	[HideInInspector]
	private GameObject eventReceiver;

	[HideInInspector]
	[SerializeField]
	public string callWhenFinished;

	private Transform mTrans;

	private float mThreshold;

	private UIScrollView mSv;

	public SpringPosition()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)


	private void Start()
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_0007: Expected O, but got Unknown
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Expected O, but got Unknown
		mTrans = this.get_transform();
		if (updateScrollView)
		{
			mSv = NGUITools.FindInParents<UIScrollView>(this.get_gameObject());
		}
	}

	private void Update()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_006c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		float deltaTime = (!ignoreTimeScale) ? Time.get_deltaTime() : RealTime.deltaTime;
		if (worldSpace)
		{
			if (mThreshold == 0f)
			{
				Vector3 val = target - mTrans.get_position();
				mThreshold = val.get_sqrMagnitude() * 0.001f;
			}
			mTrans.set_position(NGUIMath.SpringLerp(mTrans.get_position(), target, strength, deltaTime));
			float num = mThreshold;
			Vector3 val2 = target - mTrans.get_position();
			if (num >= val2.get_sqrMagnitude())
			{
				mTrans.set_position(target);
				NotifyListeners();
				this.set_enabled(false);
			}
		}
		else
		{
			if (mThreshold == 0f)
			{
				Vector3 val3 = target - mTrans.get_localPosition();
				mThreshold = val3.get_sqrMagnitude() * 1E-05f;
			}
			mTrans.set_localPosition(NGUIMath.SpringLerp(mTrans.get_localPosition(), target, strength, deltaTime));
			float num2 = mThreshold;
			Vector3 val4 = target - mTrans.get_localPosition();
			if (num2 >= val4.get_sqrMagnitude())
			{
				mTrans.set_localPosition(target);
				NotifyListeners();
				this.set_enabled(false);
			}
		}
		if (mSv != null)
		{
			mSv.UpdateScrollbars(true);
		}
	}

	private void NotifyListeners()
	{
		current = this;
		if (onFinished != null)
		{
			onFinished();
		}
		if (eventReceiver != null && !string.IsNullOrEmpty(callWhenFinished))
		{
			eventReceiver.SendMessage(callWhenFinished, (object)this, 1);
		}
		current = null;
	}

	public static SpringPosition Begin(GameObject go, Vector3 pos, float strength)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		SpringPosition springPosition = go.GetComponent<SpringPosition>();
		if (springPosition == null)
		{
			springPosition = go.AddComponent<SpringPosition>();
		}
		springPosition.target = pos;
		springPosition.strength = strength;
		springPosition.onFinished = null;
		if (!springPosition.get_enabled())
		{
			springPosition.mThreshold = 0f;
			springPosition.set_enabled(true);
		}
		return springPosition;
	}
}
