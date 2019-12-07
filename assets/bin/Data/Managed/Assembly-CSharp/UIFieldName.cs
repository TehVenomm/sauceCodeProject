using UnityEngine;

public class UIFieldName : MonoBehaviour
{
	private enum Phase
	{
		StartWait,
		FadeIn,
		Display,
		FadeOut,
		None
	}

	private Phase phase = Phase.FadeIn;

	private float waitSeconds;

	private bool initialized;

	private const float baseWidth = 120f;

	private float bgLineWidth;

	private float bgLineWidthMax;

	private float bgLineWidthStep;

	public UILabel nameLabelU;

	public UILabel nameLabelD;

	public Transform labelRoot;

	public UITweenCtrl tweenCtrl;

	public UIWidget bgLight;

	public UIWidget bgBlack;

	public UIWidget bgLine;

	public TweenAlpha tweenTop;

	private void Start()
	{
		if (!(null == nameLabelU) && !(null == nameLabelD) && !(null == labelRoot) && !(null == tweenCtrl) && !(null == bgLight) && !(null == bgBlack) && !(null == bgLine) && !(null == tweenTop))
		{
			FieldMapTable.FieldMapTableData fieldMapData = Singleton<FieldMapTable>.I.GetFieldMapData(MonoBehaviourSingleton<FieldManager>.I.currentMapID);
			if (fieldMapData == null)
			{
				base.gameObject.SetActive(value: false);
				return;
			}
			if (string.Compare(fieldMapData.stageName, 0, "FI", 0, 2) != 0)
			{
				base.gameObject.SetActive(value: false);
				return;
			}
			if (QuestManager.IsValidInGameWaveMatch())
			{
				base.gameObject.SetActive(value: false);
				return;
			}
			nameLabelU.text = fieldMapData.mapName;
			nameLabelD.text = fieldMapData.mapName;
			float num = nameLabelU.width;
			Vector3 localPosition = labelRoot.localPosition;
			localPosition.x += num * 0.5f;
			labelRoot.localPosition = localPosition;
			float num2 = num - 120f;
			float num3 = (float)bgLight.width + num2;
			bgLight.width = (int)num3;
			num3 = (float)bgBlack.width + num2;
			bgBlack.width = (int)num3;
			bgLineWidthMax = (float)bgLine.width + num2;
			bgLineWidth = 0f;
			bgLine.width = (int)bgLineWidth;
			bgLineWidthStep = bgLineWidthMax / 12f;
			nameLabelU.fontStyle = FontStyle.Italic;
			nameLabelD.fontStyle = FontStyle.Italic;
			phase = Phase.StartWait;
			waitSeconds = 0.8f;
			initialized = true;
		}
	}

	private void Phase_StartWait()
	{
		if (0f >= waitSeconds)
		{
			phase = Phase.FadeIn;
			waitSeconds = 1.2f;
			UITweenCtrl.Reset(tweenCtrl.transform);
			UITweenCtrl.Play(tweenCtrl.transform, forward: true, null, is_input_block: false);
		}
	}

	private void Phase_FadeIn()
	{
		bgLineWidth += bgLineWidthStep;
		if (bgLineWidthMax <= bgLineWidth)
		{
			bgLineWidth = bgLineWidthMax;
		}
		bgLine.width = (int)bgLineWidth;
		if (0f >= waitSeconds)
		{
			phase = Phase.Display;
			waitSeconds = 2f;
		}
	}

	private void Phase_Display()
	{
		if (0f >= waitSeconds)
		{
			phase = Phase.FadeOut;
			waitSeconds = 1f;
			tweenTop.ResetToBeginning();
			tweenTop.PlayForward();
		}
	}

	private void Phase_FadeOut()
	{
		if (0f >= waitSeconds)
		{
			base.gameObject.SetActive(value: false);
			phase = Phase.None;
		}
	}

	private void Update()
	{
		if (initialized)
		{
			waitSeconds -= Time.deltaTime;
			switch (phase)
			{
			case Phase.None:
				break;
			case Phase.StartWait:
				Phase_StartWait();
				break;
			case Phase.FadeIn:
				Phase_FadeIn();
				break;
			case Phase.Display:
				Phase_Display();
				break;
			case Phase.FadeOut:
				Phase_FadeOut();
				break;
			}
		}
	}
}
