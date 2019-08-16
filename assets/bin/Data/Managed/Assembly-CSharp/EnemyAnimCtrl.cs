using System;
using UnityEngine;

public class EnemyAnimCtrl : MonoBehaviour, IAnimEvent
{
	private Action onSignal;

	private AnimEventProcessor animEvent;

	private EnemyLoader loader;

	private Camera renderCamera;

	private bool isFieldQuest;

	protected CharacterStampCtrl stepCtrl;

	protected bool enableEventMove;

	protected Vector3 eventMoveVelocity = Vector3.get_zero();

	public Transform blurNode
	{
		get;
		protected set;
	}

	public EnemyAnimCtrl()
		: this()
	{
	}//IL_0001: Unknown result type (might be due to invalid IL or missing references)
	//IL_0006: Unknown result type (might be due to invalid IL or missing references)


	public void Init(EnemyLoader _loader, Camera render_camrea, bool is_field_quest = false)
	{
		loader = _loader;
		renderCamera = render_camrea;
		isFieldQuest = is_field_quest;
		animEvent = new AnimEventProcessor(_loader.animEventData, _loader.animator, this);
		EnemyAnimCtrlProxy enemyAnimCtrlProxy = loader.body.get_gameObject().AddComponent<EnemyAnimCtrlProxy>();
		enemyAnimCtrlProxy.enemyAnimCtrl = this;
		if (!isFieldQuest)
		{
			return;
		}
		EnemyParam componentInChildren = this.get_gameObject().GetComponentInChildren<EnemyParam>();
		if (componentInChildren != null)
		{
			if (componentInChildren.stampInfos != null && componentInChildren.stampInfos.Length > 0)
			{
				stepCtrl = this.get_gameObject().AddComponent<CharacterStampCtrl>();
				stepCtrl.Init(componentInChildren.stampInfos, null, is_direction: true);
				stepCtrl.stampDistance = 999f;
				stepCtrl.effectLayer = 18;
			}
			Object.DestroyImmediate(componentInChildren);
			componentInChildren = null;
		}
	}

	public void OnAnimatorMove()
	{
		//IL_0043: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		if (loader != null && loader.animator != null && loader.animator.get_applyRootMotion())
		{
			Transform transform = this.get_transform();
			transform.set_position(transform.get_position() + loader.animator.get_deltaPosition());
			Transform transform2 = this.get_transform();
			transform2.set_rotation(transform2.get_rotation() * loader.animator.get_deltaRotation());
		}
	}

	private void Update()
	{
		//IL_0028: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		if (animEvent != null)
		{
			animEvent.Update();
		}
		if (enableEventMove)
		{
			Transform transform = this.get_transform();
			transform.set_position(transform.get_position() + eventMoveVelocity * Time.get_deltaTime());
		}
	}

	public void OnAnimEvent(AnimEventData.EventData data)
	{
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0118: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0122: Unknown result type (might be due to invalid IL or missing references)
		//IL_0163: Unknown result type (might be due to invalid IL or missing references)
		//IL_0273: Unknown result type (might be due to invalid IL or missing references)
		//IL_027a: Unknown result type (might be due to invalid IL or missing references)
		//IL_027f: Unknown result type (might be due to invalid IL or missing references)
		//IL_028d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0292: Unknown result type (might be due to invalid IL or missing references)
		if (stepCtrl != null && stepCtrl.OnAnimEvent(data))
		{
			return;
		}
		switch (data.id)
		{
		case AnimEventFormat.ID.RADIAL_BLUR_START:
		{
			float time3 = data.floatArgs[0];
			float num4 = data.floatArgs[1];
			string name = data.stringArgs[0];
			bool flag = (data.intArgs[0] != 0) ? true : false;
			Transform val = Utility.Find(loader.body, name);
			if (val == null)
			{
				val = loader.body;
			}
			if (renderCamera == null && MonoBehaviourSingleton<InGameCameraManager>.IsValid())
			{
				if (flag)
				{
					MonoBehaviourSingleton<InGameCameraManager>.I.StartRadialBlurFilter(time3, num4, val);
				}
				else
				{
					MonoBehaviourSingleton<InGameCameraManager>.I.StartRadialBlurFilter(time3, num4, val.get_position());
				}
			}
			else
			{
				Vector2 center = Vector2.op_Implicit(renderCamera.WorldToScreenPoint(val.get_position()));
				center.x /= (float)Screen.get_width();
				center.y = Mathf.Lerp(0.5f, 1f, center.y / (float)Screen.get_height());
				MonoBehaviourSingleton<FilterManager>.I.StartTubulanceFilter(num4, center, null);
			}
			if (!isFieldQuest)
			{
				loader.animator.set_speed(0f);
				if (onSignal != null)
				{
					onSignal();
					onSignal = null;
				}
			}
			break;
		}
		case AnimEventFormat.ID.RADIAL_BLUR_CHANGE:
		{
			float time2 = data.floatArgs[0];
			float num2 = data.floatArgs[1];
			if (num2 <= 0f)
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(time2);
			}
			else
			{
				MonoBehaviourSingleton<InGameCameraManager>.I.ChangeRadialBlurFilter(time2, num2);
			}
			break;
		}
		case AnimEventFormat.ID.RADIAL_BLUR_END:
		{
			float time = data.floatArgs[0];
			MonoBehaviourSingleton<InGameCameraManager>.I.EndRadialBlurFilter(time);
			break;
		}
		case AnimEventFormat.ID.SE_ONESHOT:
		{
			int num3 = data.intArgs[0];
			if (num3 != 0)
			{
				SoundManager.PlayOneShotSE(num3, null, MonoBehaviourSingleton<AppMain>.I.mainCameraTransform);
			}
			break;
		}
		case AnimEventFormat.ID.FIELD_QUEST_UI_OPEN:
			if (isFieldQuest && onSignal != null)
			{
				onSignal();
				onSignal = null;
			}
			break;
		case AnimEventFormat.ID.MOVE_FORWARD_START:
		{
			float num = data.floatArgs[0];
			enableEventMove = true;
			eventMoveVelocity = Vector3.get_forward() * num;
			break;
		}
		case AnimEventFormat.ID.MOVE_END:
			enableEventMove = false;
			eventMoveVelocity = Vector3.get_zero();
			break;
		}
	}

	public void PlayQuestStartAnim(Action on_complete)
	{
		if (loader == null || loader.animEventData == null || !loader.animEventData.get_name().Contains("QENM"))
		{
			on_complete?.Invoke();
			return;
		}
		onSignal = on_complete;
		if (isFieldQuest)
		{
			loader.animator.CrossFade("ATTACK_FIELD", 0f);
		}
		else
		{
			loader.animator.CrossFade("ATTACK", 0.1f);
		}
	}
}
