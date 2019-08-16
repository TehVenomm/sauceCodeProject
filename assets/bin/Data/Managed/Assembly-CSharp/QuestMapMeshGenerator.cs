using UnityEngine;

public class QuestMapMeshGenerator : MonoBehaviour
{
	public float heightMax = 10f;

	public Vector2 length = new Vector2(100f, 100f);

	public Vector2 center = new Vector2(0.5f, 0.5f);

	public Vector2 divide = new Vector2(80f, 80f);

	public bool fitHeightMapImage = true;

	public bool removeHeightZeroTriangle = true;

	public float vartexRandomMoveRate = 0.3f;

	public float vartexRandomMoveRateH0 = 0.1f;

	public Texture2D heightMap;

	public Texture2D colorMap;

	public bool enableBlend = true;

	public Texture2D blendMap1;

	public Texture2D blendMap2;

	public Texture2D blendMap3;

	public string outPath = string.Empty;

	public QuestMapMeshGenerator()
		: this()
	{
	}//IL_0016: Unknown result type (might be due to invalid IL or missing references)
	//IL_001b: Unknown result type (might be due to invalid IL or missing references)
	//IL_002b: Unknown result type (might be due to invalid IL or missing references)
	//IL_0030: Unknown result type (might be due to invalid IL or missing references)
	//IL_0040: Unknown result type (might be due to invalid IL or missing references)
	//IL_0045: Unknown result type (might be due to invalid IL or missing references)

}
