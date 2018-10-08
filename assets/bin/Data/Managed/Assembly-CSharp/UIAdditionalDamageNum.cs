using System.Collections;
using UnityEngine;

public class UIAdditionalDamageNum : UIDamageNum
{
	[SerializeField]
	private TweenPosition animPos;

	[SerializeField]
	private TweenScale animScale;

	[SerializeField]
	private TweenPosition animPosNormal;

	[SerializeField]
	private TweenScale animScaleNormal;

	[SerializeField]
	private TweenPosition animPosGood;

	[SerializeField]
	private TweenScale animScaleGood;

	[SerializeField]
	private TweenPosition animPosBad;

	[SerializeField]
	private TweenScale animScaleBad;

	[SerializeField]
	private GameObject damageNormal;

	[SerializeField]
	private GameObject damageGood;

	[SerializeField]
	private GameObject damageBad;

	[SerializeField]
	private UILabel damageNumNormal;

	[SerializeField]
	private UILabel damageNumGood;

	[SerializeField]
	private UILabel damageNumBad;

	public bool Initialize(Vector3 pos, int damage, DAMAGE_COLOR color, int groupOffset, UIDamageNum originalDamage, int effective)
	{
		worldPos = pos;
		worldPos.y += offsetY;
		float num = (float)Screen.height / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualHeight;
		float num2 = (float)Screen.width / (float)MonoBehaviourSingleton<UIManager>.I.uiRoot.manualWidth;
		higthOffset_f = (float)(damadeNum.height * groupOffset) * heightOffsetRatio * num;
		widthOffset = (float)damadeNum.width * 0.2f * (float)groupOffset * num2;
		if ((Object)null != (Object)damageNormal && (Object)null != (Object)damageGood && (Object)null != (Object)damageBad)
		{
			float num3 = 1f;
			if (effective == 0)
			{
				damageNormal.SetActive(true);
				damageGood.SetActive(false);
				damageBad.SetActive(false);
				animPos = animPosNormal;
				animScale = animScaleNormal;
				damadeNum = damageNumNormal;
			}
			else if (0 < effective)
			{
				damageNormal.SetActive(false);
				damageGood.SetActive(true);
				damageBad.SetActive(false);
				animPos = animPosGood;
				animScale = animScaleGood;
				damadeNum = damageNumGood;
				num3 = 1.2f;
			}
			else
			{
				damageNormal.SetActive(false);
				damageGood.SetActive(false);
				damageBad.SetActive(true);
				animPos = animPosBad;
				animScale = animScaleBad;
				damadeNum = damageNumBad;
			}
			higthOffset_f *= num3;
			widthOffset *= num3;
			color = calcColor(color, effective);
		}
		if (groupOffset == 0 && 0 >= effective)
		{
			animScale.from = new Vector3(1f, 1f, 1f);
			animScale.to = new Vector3(1f, 1f, 1f);
		}
		if (!SetPosFromWorld(worldPos))
		{
			return false;
		}
		enable = true;
		damadeNum.text = damage.ToString();
		damageLength = damadeNum.text.Length;
		ChangeColor(color, damadeNum);
		if ((Object)animPos != (Object)null)
		{
			animPos.ResetToBeginning();
		}
		if ((Object)animScale != (Object)null)
		{
			animScale.ResetToBeginning();
		}
		StartCoroutine(DirectionNumber());
		return true;
	}

	private IEnumerator DirectionNumber()
	{
		if ((Object)animPos != (Object)null)
		{
			animPos.PlayForward();
		}
		if ((Object)animScale != (Object)null)
		{
			animScale.PlayForward();
		}
		if ((Object)animPos != (Object)null)
		{
			while (animPos.enabled)
			{
				yield return (object)null;
			}
		}
		if ((Object)animScale != (Object)null)
		{
			while (animScale.enabled)
			{
				yield return (object)null;
			}
		}
		enable = false;
		damadeNum.alpha = 0.01f;
	}

	private DAMAGE_COLOR calcColor(DAMAGE_COLOR color, int effective)
	{
		switch (color)
		{
		case DAMAGE_COLOR.REGION_ONLY_NORMAL:
		case DAMAGE_COLOR.REGION_ONLY_ELEMENT:
		case DAMAGE_COLOR.REGION_ONLY_BUFF:
			return color;
		case DAMAGE_COLOR.FIRE:
		case DAMAGE_COLOR.WATER:
		case DAMAGE_COLOR.THUNDER:
		case DAMAGE_COLOR.SOIL:
		case DAMAGE_COLOR.LIGHT:
		case DAMAGE_COLOR.DARK:
			return color;
		default:
			if (effective == 0)
			{
				return color;
			}
			if (0 < effective)
			{
				return DAMAGE_COLOR.GOOD;
			}
			return DAMAGE_COLOR.BAD;
		}
	}
}
