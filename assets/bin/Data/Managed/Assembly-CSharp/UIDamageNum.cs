using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIDamageNum
{
	public enum DAMAGE_COLOR
	{
		NONE,
		BUFF,
		FIRE,
		WATER,
		THUNDER,
		SOIL,
		LIGHT,
		DARK,
		GOOD,
		BAD,
		REGION_ONLY_NORMAL,
		REGION_ONLY_ELEMENT,
		REGION_ONLY_BUFF
	}

	[SerializeField]
	protected UILabel damadeNum;

	[SerializeField]
	public UIGrid grid;

	[Tooltip("表示時間（秒）")]
	public float showTime = 1f;

	[Tooltip("表示高さオフセット")]
	public float offsetY;

	[Tooltip("跳ねる高さ")]
	public float upHeight = 10f;

	[Tooltip("上昇速度（距離/秒）")]
	public float upSpeed = 100f;

	[Tooltip("バフ時カラ\u30fc")]
	public Color buffColor;

	public Color buffOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("炎時カラ\u30fc")]
	public Color fireColor;

	public Color fireOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("水時カラ\u30fc")]
	[FormerlySerializedAs("iceColor")]
	public Color waterColor;

	public Color waterOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("雷時カラ\u30fc")]
	[FormerlySerializedAs("windColor")]
	public Color thunderColor;

	public Color thunderOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("土時カラ\u30fc")]
	public Color soilColor;

	public Color soilOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("光時カラ\u30fc")]
	public Color lightColor;

	public Color lightOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("闇時カラ\u30fc")]
	public Color darkColor;

	public Color darkOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("有効時カラ\u30fc")]
	public Color goodColor;

	public Color goodOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("無効時カラ\u30fc")]
	public Color badColor;

	public Color badOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("部位にしかダメ\u30fcジが通らないカラ\u30fc(無属性)")]
	public Color regionOnlyColor;

	public Color regionOnlyOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("部位にしかダメ\u30fcジが通らないカラ\u30fc(属性)")]
	public Color regionOnlyElementColor;

	public Color regionOnlyElementOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	[Tooltip("部位にしかダメ\u30fcジ通らないカラ\u30fc(弱点)")]
	public Color regionOnlyBuffColor;

	public Color regionOnlyBuffOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	private List<GameObject> damageNumList = new List<GameObject>();

	protected Vector3 worldPos;

	protected int higthOffset;

	protected int damageLength;

	protected float heightOffsetRatio = 1f;

	protected float higthOffset_f;

	protected float widthOffset;

	protected Color normalColor = Color.get_white();

	protected Color normalOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	public bool enable;

	public int DamageLength => damageLength;

	public UIDamageNum()
		: this()
	{
	}//IL_0031: Unknown result type (might be due to invalid IL or missing references)
	//IL_0036: Unknown result type (might be due to invalid IL or missing references)
	//IL_004b: Unknown result type (might be due to invalid IL or missing references)
	//IL_0050: Unknown result type (might be due to invalid IL or missing references)
	//IL_0065: Unknown result type (might be due to invalid IL or missing references)
	//IL_006a: Unknown result type (might be due to invalid IL or missing references)
	//IL_007f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0084: Unknown result type (might be due to invalid IL or missing references)
	//IL_0099: Unknown result type (might be due to invalid IL or missing references)
	//IL_009e: Unknown result type (might be due to invalid IL or missing references)
	//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
	//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
	//IL_00cd: Unknown result type (might be due to invalid IL or missing references)
	//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
	//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
	//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
	//IL_0101: Unknown result type (might be due to invalid IL or missing references)
	//IL_0106: Unknown result type (might be due to invalid IL or missing references)
	//IL_011b: Unknown result type (might be due to invalid IL or missing references)
	//IL_0120: Unknown result type (might be due to invalid IL or missing references)
	//IL_0135: Unknown result type (might be due to invalid IL or missing references)
	//IL_013a: Unknown result type (might be due to invalid IL or missing references)
	//IL_014f: Unknown result type (might be due to invalid IL or missing references)
	//IL_0154: Unknown result type (might be due to invalid IL or missing references)
	//IL_0170: Unknown result type (might be due to invalid IL or missing references)
	//IL_0175: Unknown result type (might be due to invalid IL or missing references)
	//IL_018a: Unknown result type (might be due to invalid IL or missing references)
	//IL_018f: Unknown result type (might be due to invalid IL or missing references)


	protected void Awake()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_000c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		normalColor = damadeNum.color;
		normalOutLineColor = damadeNum.effectColor;
	}

	protected void OnDisable()
	{
		int i = 0;
		for (int count = damageNumList.Count; i < count; i++)
		{
			UILabel component = damageNumList[i].GetComponent<UILabel>();
			component.alpha = 0.01f;
		}
		enable = false;
	}

	public bool Initialize(Vector3 pos, int damage, DAMAGE_COLOR color, int groupOffset)
	{
		//IL_0001: Unknown result type (might be due to invalid IL or missing references)
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Expected O, but got Unknown
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		//IL_013b: Expected O, but got Unknown
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Expected O, but got Unknown
		//IL_0154: Expected O, but got Unknown
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		worldPos = pos;
		worldPos.y += offsetY;
		float num = (float)Screen.get_height() / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		float num2 = (float)Screen.get_width() / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
		higthOffset_f = (float)(damadeNum.height * groupOffset) * heightOffsetRatio * num;
		widthOffset = (float)damadeNum.width * 0.2f * (float)groupOffset * num2;
		if (!SetPosFromWorld(worldPos))
		{
			return false;
		}
		enable = true;
		int count = damageNumList.Count;
		string text = damage.ToString();
		damageLength = text.Length;
		int i = 0;
		for (int num3 = damageLength; i < num3; i++)
		{
			UILabel uILabel = null;
			if (count > i)
			{
				uILabel = damageNumList[i].GetComponent<UILabel>();
				damageNumList[i].SetActive(true);
			}
			else
			{
				GameObject val = null;
				if (i == 0)
				{
					val = damadeNum.get_gameObject();
					uILabel = damadeNum;
				}
				else
				{
					val = ResourceUtility.Instantiate<GameObject>(damadeNum.get_gameObject());
					Utility.Attach(this.get_gameObject().get_transform(), val.get_transform());
					uILabel = val.GetComponent<UILabel>();
				}
				damageNumList.Add(val);
			}
			uILabel.text = text[i].ToString();
			uILabel.alpha = 1f;
			ChangeColor(color, uILabel);
		}
		if (count > damageLength)
		{
			for (int j = text.Length; j < count; j++)
			{
				damageNumList[j].SetActive(false);
			}
		}
		grid.Reposition();
		this.StartCoroutine(DirectionNumber());
		return true;
	}

	private IEnumerator DirectionNumber()
	{
		int num_count = damageNumList.Count;
		for (int j = 0; j < num_count; j++)
		{
			GameObject obj = damageNumList[j];
			Vector3 v = obj.get_transform().get_localPosition();
			while (v.y < upHeight)
			{
				v.y += upSpeed * Time.get_deltaTime();
				obj.get_transform().set_localPosition(v);
				yield return (object)null;
			}
			while (v.y >= 0f)
			{
				v.y -= upSpeed * Time.get_deltaTime();
				obj.get_transform().set_localPosition(v);
				yield return (object)null;
			}
			v.y = 0f;
			obj.get_transform().set_localPosition(v);
		}
		yield return (object)new WaitForSeconds(showTime);
		for (int i = 0; i < num_count; i++)
		{
			UILabel label = damageNumList[i].GetComponent<UILabel>();
			label.alpha = 0.01f;
		}
		enable = false;
	}

	protected void LateUpdate()
	{
		//IL_000e: Unknown result type (might be due to invalid IL or missing references)
		if (enable && !SetPosFromWorld(worldPos))
		{
			enable = false;
			int i = 0;
			for (int count = damageNumList.Count; i < count; i++)
			{
				UILabel component = damageNumList[i].GetComponent<UILabel>();
				component.alpha = 0.01f;
			}
		}
	}

	public Vector3 GetUIPosFromWorld(Vector3 world_pos, int groupOffset)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			return Vector3.get_zero();
		}
		world_pos.y += offsetY;
		Vector3 val = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_pos);
		float num = (float)Screen.get_height() / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		higthOffset_f = (float)(damadeNum.height * groupOffset) * heightOffsetRatio * num;
		val.y += higthOffset_f;
		if (val.z < 0f)
		{
			return Vector3.get_zero();
		}
		val.z = 0f;
		return MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val);
	}

	protected bool SetPosFromWorld(Vector3 world_pos)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			return false;
		}
		Vector3 val = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_pos);
		val.y += higthOffset_f;
		val.x += widthOffset;
		if (val.z < 0f)
		{
			return false;
		}
		val.z = 0f;
		Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val);
		this.get_gameObject().get_transform().set_position(position);
		return true;
	}

	protected void ChangeColor(DAMAGE_COLOR color, UILabel label)
	{
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_006a: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0087: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_010c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_0146: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_016f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0180: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a9: Unknown result type (might be due to invalid IL or missing references)
		switch (color)
		{
		case DAMAGE_COLOR.BUFF:
			label.color = buffColor;
			label.effectColor = buffOutLineColor;
			break;
		case DAMAGE_COLOR.FIRE:
			label.color = fireColor;
			label.effectColor = fireOutLineColor;
			break;
		case DAMAGE_COLOR.WATER:
			label.color = waterColor;
			label.effectColor = waterOutLineColor;
			break;
		case DAMAGE_COLOR.THUNDER:
			label.color = thunderColor;
			label.effectColor = thunderOutLineColor;
			break;
		case DAMAGE_COLOR.SOIL:
			label.color = soilColor;
			label.effectColor = soilOutLineColor;
			break;
		case DAMAGE_COLOR.LIGHT:
			label.color = lightColor;
			label.effectColor = lightOutLineColor;
			break;
		case DAMAGE_COLOR.DARK:
			label.color = darkColor;
			label.effectColor = darkOutLineColor;
			break;
		case DAMAGE_COLOR.GOOD:
			label.color = goodColor;
			label.effectColor = goodOutLineColor;
			break;
		case DAMAGE_COLOR.BAD:
			label.color = badColor;
			label.effectColor = badOutLineColor;
			break;
		case DAMAGE_COLOR.REGION_ONLY_NORMAL:
			label.color = regionOnlyColor;
			label.effectColor = regionOnlyOutLineColor;
			break;
		case DAMAGE_COLOR.REGION_ONLY_ELEMENT:
			label.color = regionOnlyElementColor;
			label.effectColor = regionOnlyElementOutLineColor;
			break;
		case DAMAGE_COLOR.REGION_ONLY_BUFF:
			label.color = regionOnlyBuffColor;
			label.effectColor = regionOnlyBuffOutLineColor;
			break;
		default:
			label.color = normalColor;
			label.effectColor = normalOutLineColor;
			break;
		}
	}
}
