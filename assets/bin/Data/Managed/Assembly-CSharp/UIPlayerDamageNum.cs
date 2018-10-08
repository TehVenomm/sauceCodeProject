using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayerDamageNum : MonoBehaviour
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
	public Vector3 offset = Vector3.zero;

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

	public float AlphaRate => (!((UnityEngine.Object)damadeNum != (UnityEngine.Object)null)) ? 0f : damadeNum.alpha;

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
		if ((UnityEngine.Object)animPos != (UnityEngine.Object)null)
		{
			animPos.ResetToBeginning();
			animPos.enabled = false;
		}
		if ((UnityEngine.Object)animAlpha != (UnityEngine.Object)null)
		{
			animAlpha.ResetToBeginning();
			animAlpha.enabled = false;
		}
		if ((UnityEngine.Object)animScale != (UnityEngine.Object)null)
		{
			animScale.ResetToBeginning();
			animScale.enabled = false;
		}
		base.transform.localScale = Vector3.zero;
		if (isAutoPlay)
		{
			Play();
		}
		return true;
	}

	private IEnumerator DirectionNumber()
	{
		if ((UnityEngine.Object)animPos != (UnityEngine.Object)null)
		{
			animPos.PlayForward();
		}
		if ((UnityEngine.Object)animAlpha != (UnityEngine.Object)null)
		{
			animAlpha.PlayForward();
		}
		if ((UnityEngine.Object)animScale != (UnityEngine.Object)null)
		{
			animScale.PlayForward();
		}
		if ((UnityEngine.Object)animPos != (UnityEngine.Object)null)
		{
			while (animPos.enabled)
			{
				yield return (object)null;
			}
		}
		if ((UnityEngine.Object)animAlpha != (UnityEngine.Object)null)
		{
			while (animAlpha.enabled)
			{
				yield return (object)null;
			}
		}
		if ((UnityEngine.Object)animScale != (UnityEngine.Object)null)
		{
			while (animScale.enabled)
			{
				yield return (object)null;
			}
		}
		OnFinishAnimation();
	}

	private void LateUpdate()
	{
		if (isPlaying && enable && !((UnityEngine.Object)chara == (UnityEngine.Object)null) && !SetPosFromWorld(chara._transform.position + offset, false))
		{
			OnFinishAnimation();
		}
	}

	private void OnFinishAnimation()
	{
		enable = false;
		damadeNum.alpha = 0f;
		chara = null;
		isPlaying = false;
		if (isAutoDelete)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			isAutoDelete = false;
		}
	}

	private bool SetPosFromWorld(Vector3 world_pos, bool bUpdatePosY)
	{
		if (!MonoBehaviourSingleton<InGameCameraManager>.IsValid())
		{
			return false;
		}
		Vector3 position = MonoBehaviourSingleton<InGameCameraManager>.I.WorldToScreenPoint(world_pos);
		position.y += (float)higthOffset;
		if (position.z < 0f)
		{
			return false;
		}
		position.z = 0f;
		Vector3 position2 = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(position);
		if (!bUpdatePosY)
		{
			Vector3 position3 = base.gameObject.transform.position;
			position2.y = position3.y;
		}
		base.gameObject.transform.position = position2;
		return true;
	}

	public void Play()
	{
		isPlaying = true;
		if ((UnityEngine.Object)animPos != (UnityEngine.Object)null)
		{
			animPos.enabled = true;
		}
		if ((UnityEngine.Object)animAlpha != (UnityEngine.Object)null)
		{
			animAlpha.enabled = true;
		}
		if ((UnityEngine.Object)animScale != (UnityEngine.Object)null)
		{
			animScale.enabled = true;
		}
		base.transform.localScale = Vector3.one;
		StartCoroutine(DirectionNumber());
	}
}
