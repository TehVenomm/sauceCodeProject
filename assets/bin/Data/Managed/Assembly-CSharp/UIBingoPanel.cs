using System.Collections.Generic;
using UnityEngine;

public class UIBingoPanel : MonoBehaviourSingleton<UIBingoPanel>
{
	[SerializeField]
	private GameObject noticeObject;

	[SerializeField]
	protected UITweenCtrl animCtrl;

	[SerializeField]
	protected TweenAlpha noticeTween;

	[SerializeField]
	protected Vector3 offset;

	protected IFieldGimmickObject bingo;

	protected IFieldGimmickObject bingoReq;

	protected override void Awake()
	{
		base.Awake();
		noticeObject.SetActive(false);
		base.gameObject.SetActive(FieldManager.IsValidInGameNoQuest());
	}

	private void Update()
	{
		if (MonoBehaviourSingleton<StageObjectManager>.IsValid() && MonoBehaviourSingleton<InGameProgress>.IsValid())
		{
			Self self = MonoBehaviourSingleton<StageObjectManager>.I.self;
			if (!((Object)self == (Object)null))
			{
				int num = 4;
				List<IFieldGimmickObject> list = MonoBehaviourSingleton<InGameProgress>.I.fieldGimmickList[num];
				if (!list.IsNullOrEmpty())
				{
					bingoReq = null;
					float num2 = 3.40282347E+38f;
					for (int i = 0; i < list.Count; i++)
					{
						IFieldGimmickObject fieldGimmickObject = list[i];
						if (fieldGimmickObject != null)
						{
							float num3 = Vector3.Distance(fieldGimmickObject.GetTransform().position, self._position);
							if (num3 < 10f && num3 < num2)
							{
								bingoReq = fieldGimmickObject;
								num2 = num3;
							}
						}
					}
					if (bingo != bingoReq && !noticeTween.isActiveAndEnabled)
					{
						if (noticeTween.value == 0f)
						{
							if (bingoReq != null)
							{
								noticeObject.SetActive(true);
								noticeTween.PlayForward();
							}
							else
							{
								noticeObject.SetActive(false);
							}
							bingo = bingoReq;
						}
						else
						{
							noticeTween.PlayReverse();
						}
					}
				}
			}
		}
	}

	private void LateUpdate()
	{
		if (bingo != null)
		{
			Vector3 position = MonoBehaviourSingleton<UIManager>.I.uiCamera.ScreenToWorldPoint(MonoBehaviourSingleton<AppMain>.I.mainCamera.WorldToScreenPoint(bingo.GetTransform().position + offset));
			if (position.z >= 0f)
			{
				position.z = 0f;
			}
			else
			{
				position.z = -100f;
			}
			base._transform.position = position;
		}
	}
}
