using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerDamageNum
{
	[Serializable]
	public class LabelColor
	{
		public Color main;

		public Color effect;
	}

	public enum DAMAGE_COLOR
	{
		DAMAGE,
		HEAL
	}

	[SerializeField]
	protected UILabel damadeNum;

	[SerializeField]
	protected TweenPosition animPos;

	[SerializeField]
	protected TweenAlpha animAlpha;

	[SerializeField]
	protected TweenScale animScale;

	[Tooltip("表示高さオフセット")]
	public Vector3 offset = Vector3.get_zero();

	[Tooltip("ダメ\u30fcジカラ\u30fc")]
	public LabelColor damageColor;

	[Tooltip("回復カラ\u30fc")]
	public LabelColor healColor;

	[Tooltip("属性カラ\u30fc")]
	public List<LabelColor> elementColor;

	protected Character chara;

	protected int higthOffset;

	private bool isPlaying;

	private bool isAutoDelete;

	public bool enable;

	public float AlphaRate => (!(damadeNum != null)) ? 0f : damadeNum.alpha;

	public UIPlayerDamageNum()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)


	public void EnableAutoDelete()
	{
		isAutoDelete = true;
	}

	private void OnDisable()
	{
		OnFinishAnimation();
	}

	public bool Initialize(Character _chara, AttackedHitStatus status, bool isAutoPlay = true)
	{
		return Initialize(_chara, status.damage + status.shieldDamage, DAMAGE_COLOR.DAMAGE, status.damageDetails.GetElementType(), isAutoPlay);
	}

	public bool Initialize(Character _chara, int damage, DAMAGE_COLOR color, bool isAutoPlay = true)
	{
		return Initialize(_chara, damage, color, ELEMENT_TYPE.MAX, isAutoPlay);
	}

	public bool Initialize(Character _chara, int damage, DAMAGE_COLOR color, ELEMENT_TYPE element, bool isAutoPlay = true)
	{
		//IL_001f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0025: Unknown result type (might be due to invalid IL or missing references)
		//IL_002a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b7: Unknown result type (might be due to invalid IL or missing references)
		chara = _chara;
		higthOffset = damadeNum.height;
		if (!SetPosFromWorld(chara._position + offset, true))
		{
			return false;
		}
		enable = true;
		damadeNum.text = damage.ToString();
		switch (color)
		{
		case DAMAGE_COLOR.DAMAGE:
			if (elementColor.Count >= 0 && elementColor.Count > (int)element)
			{
				damadeNum.color = elementColor[(int)element].main;
				damadeNum.effectColor = elementColor[(int)element].effect;
			}
			else
			{
				damadeNum.color = elementColor[6].main;
				damadeNum.effectColor = elementColor[6].effect;
			}
			break;
		case DAMAGE_COLOR.HEAL:
			damadeNum.color = healColor.main;
			damadeNum.effectColor = healColor.effect;
			break;
		}
		if (animPos != null)
		{
			animPos.ResetToBeginning();
			animPos.set_enabled(false);
		}
		if (animAlpha != null)
		{
			animAlpha.ResetToBeginning();
			animAlpha.set_enabled(false);
		}
		if (animScale != null)
		{
			animScale.ResetToBeginning();
			animScale.set_enabled(false);
		}
		this.get_transform().set_localScale(Vector3.get_zero());
		if (isAutoPlay)
		{
			Play();
		}
		return true;
	}

	private IEnumerator DirectionNumber()
	{
		if (animPos != null)
		{
			animPos.PlayForward();
		}
		if (animAlpha != null)
		{
			animAlpha.PlayForward();
		}
		if (animScale != null)
		{
			animScale.PlayForward();
		}
		if (animPos != null)
		{
			while (animPos.get_enabled())
			{
				yield return (object)null;
			}
		}
		if (animAlpha != null)
		{
			while (animAlpha.get_enabled())
			{
				yield return (object)null;
			}
		}
		if (animScale != null)
		{
			while (animScale.get_enabled())
			{
				yield return (object)null;
			}
		}
		OnFinishAnimation();
	}

	private void LateUpdate()
	{
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		if (isPlaying && enable && !(chara == null) && !SetPosFromWorld(chara._transform.get_position() + offset, false))
		{
			OnFinishAnimation();
		}
	}

	private void OnFinishAnimation()
	{
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		enable = false;
		damadeNum.alpha = 0f;
		chara = null;
		isPlaying = false;
		if (isAutoDelete)
		{
			Object.Destroy(this.get_gameObject());
			isAutoDelete = false;
		}
	}

	private bool SetPosFromWorld(Vector3 world_pos, bool bUpdatePosY)
	{
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0070: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			return false;
		}
		Vector3 val = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_pos);
		val.y += (float)higthOffset;
		if (val.z < 0f)
		{
			return false;
		}
		val.z = 0f;
		Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(val);
		if (!bUpdatePosY)
		{
			Vector3 position2 = this.get_gameObject().get_transform().get_position();
			position.y = position2.y;
		}
		this.get_gameObject().get_transform().set_position(position);
		return true;
	}

	public void Play()
	{
		//IL_005f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0075: Unknown result type (might be due to invalid IL or missing references)
		isPlaying = true;
		if (animPos != null)
		{
			animPos.set_enabled(true);
		}
		if (animAlpha != null)
		{
			animAlpha.set_enabled(true);
		}
		if (animScale != null)
		{
			animScale.set_enabled(true);
		}
		this.get_transform().set_localScale(Vector3.get_one());
		this.StartCoroutine(DirectionNumber());
	}
}
