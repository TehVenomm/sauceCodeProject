using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class UIDamageNum : MonoBehaviour
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

	protected Color normalColor = Color.white;

	protected Color normalOutLineColor = new Color(0.03f, 0.08f, 0.14f);

	public bool enable;

	public int DamageLength => damageLength;

	protected void Awake()
	{
		normalColor = damadeNum.color;
		normalOutLineColor = damadeNum.effectColor;
	}

	protected void OnDisable()
	{
		int i = 0;
		for (int count = damageNumList.Count; i < count; i++)
		{
			damageNumList[i].GetComponent<UILabel>().alpha = 0.01f;
		}
		enable = false;
	}

	public bool Initialize(Vector3 pos, int damage, DAMAGE_COLOR color, int groupOffset)
	{
		worldPos = pos;
		worldPos.y += offsetY;
		float num = (float)Screen.height / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		float num2 = (float)Screen.width / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
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
				damageNumList[i].SetActive(value: true);
			}
			else
			{
				GameObject gameObject = null;
				if (i == 0)
				{
					gameObject = damadeNum.gameObject;
					uILabel = damadeNum;
				}
				else
				{
					gameObject = ResourceUtility.Instantiate(damadeNum.gameObject);
					Utility.Attach(base.gameObject.transform, gameObject.transform);
					uILabel = gameObject.GetComponent<UILabel>();
				}
				damageNumList.Add(gameObject);
			}
			uILabel.text = text[i].ToString();
			uILabel.alpha = 1f;
			ChangeColor(color, uILabel);
		}
		if (count > damageLength)
		{
			for (int j = text.Length; j < count; j++)
			{
				damageNumList[j].SetActive(value: false);
			}
		}
		grid.Reposition();
		StartCoroutine(DirectionNumber());
		return true;
	}

	private IEnumerator DirectionNumber()
	{
		int num_count = damageNumList.Count;
		for (int i = 0; i < num_count; i++)
		{
			GameObject obj = damageNumList[i];
			Vector3 v = obj.transform.localPosition;
			while (v.y < upHeight)
			{
				v.y += upSpeed * Time.deltaTime;
				obj.transform.localPosition = v;
				yield return null;
			}
			while (v.y >= 0f)
			{
				v.y -= upSpeed * Time.deltaTime;
				obj.transform.localPosition = v;
				yield return null;
			}
			v.y = 0f;
			obj.transform.localPosition = v;
			v = default(Vector3);
		}
		yield return new WaitForSeconds(showTime);
		for (int j = 0; j < num_count; j++)
		{
			damageNumList[j].GetComponent<UILabel>().alpha = 0.01f;
		}
		enable = false;
	}

	protected void LateUpdate()
	{
		if (enable && !SetPosFromWorld(worldPos))
		{
			enable = false;
			int i = 0;
			for (int count = damageNumList.Count; i < count; i++)
			{
				damageNumList[i].GetComponent<UILabel>().alpha = 0.01f;
			}
		}
	}

	public Vector3 GetUIPosFromWorld(Vector3 world_pos, int groupOffset)
	{
		if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			return Vector3.zero;
		}
		world_pos.y += offsetY;
		Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_pos);
		float num = (float)Screen.height / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		higthOffset_f = (float)(damadeNum.height * groupOffset) * heightOffsetRatio * num;
		position.y += higthOffset_f;
		if (position.z < 0f)
		{
			return Vector3.zero;
		}
		position.z = 0f;
		return MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
	}

	protected bool SetPosFromWorld(Vector3 world_pos)
	{
		if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			return false;
		}
		Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_pos);
		position.y += higthOffset_f;
		position.x += widthOffset;
		if (position.z < 0f)
		{
			return false;
		}
		position.z = 0f;
		Vector3 position2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
		base.gameObject.transform.position = position2;
		return true;
	}

	protected void ChangeColor(DAMAGE_COLOR color, UILabel label)
	{
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
