using rhyme;
using System;
using System.Collections.Generic;
using UnityEngine;

public class Trail : MonoBehaviour
{
	private class Pool_List_Point : rymTPool<List<Point>>
	{
	}

	private class Pool_Point : rymTPool<Point>
	{
	}

	private class Point
	{
		public Vector3 beginPos;

		public Vector3 centerPos;

		public Vector3 endPos;

		public float time;
	}

	public enum AXIS
	{
		X,
		Y,
		Z
	}

	public static Func<Trail, bool> onQueryDestroy;

	public static bool settingFixedUpdate = true;

	public Shader shader;

	public Texture texture;

	public Transform targetCameraTransform;

	public bool emit = true;

	public bool fixedUpdate = true;

	public int polygonNum = 64;

	public float life = 0.25f;

	public float delayTime;

	public float checkMoveLength = 0.01f;

	public float divideLength = 1f;

	public int divideAngle = 10;

	public Vector3 offset = Vector3.get_zero();

	public AXIS axis = AXIS.Z;

	public bool billboard;

	public float width = 1f;

	public bool reverseV;

	public Color color = Color.get_white();

	public float timeForDelete = 0.25f;

	public Color colorForDelete = new Color(1f, 1f, 1f, 0f);

	public Bounds bounds = new Bounds(Vector3.get_zero(), new Vector3(20f, 20f, 20f));

	private Transform _transform;

	private Material material;

	private Mesh mesh;

	private Vector3[] vertices;

	private Color[] colors;

	private Vector2[] uvs;

	private int[] triangles;

	private List<Point> pointList;

	private Point prevPoint1;

	private Point prevPoint2;

	private float time;

	private float deleteTime;

	private bool autoDelete;

	private bool dirty;

	private Vector3 lastBeginPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

	private Vector3 lastEndPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);

	private int pauseStep;

	public bool pause
	{
		get
		{
			return pauseStep != 0;
		}
		set
		{
			if (!autoDelete)
			{
				if (value)
				{
					pauseStep = 1;
				}
				else
				{
					pauseStep = 0;
				}
			}
		}
	}

	public Trail()
		: this()
	{
	}//IL_0040: Unknown result type (might be due to invalid IL or missing references)
	//IL_0045: Unknown result type (might be due to invalid IL or missing references)
	//IL_005d: Unknown result type (might be due to invalid IL or missing references)
	//IL_0062: Unknown result type (might be due to invalid IL or missing references)
	//IL_0087: Unknown result type (might be due to invalid IL or missing references)
	//IL_008c: Unknown result type (might be due to invalid IL or missing references)
	//IL_0092: Unknown result type (might be due to invalid IL or missing references)
	//IL_00a6: Unknown result type (might be due to invalid IL or missing references)
	//IL_00ab: Unknown result type (might be due to invalid IL or missing references)
	//IL_00b0: Unknown result type (might be due to invalid IL or missing references)
	//IL_00c5: Unknown result type (might be due to invalid IL or missing references)
	//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
	//IL_00df: Unknown result type (might be due to invalid IL or missing references)
	//IL_00e4: Unknown result type (might be due to invalid IL or missing references)


	public static void ClearPoolObjects()
	{
		rymTPool<List<Point>>.Clear();
		rymTPool<Point>.Clear();
	}

	private void Awake()
	{
		fixedUpdate = settingFixedUpdate;
		pointList = rymTPool<List<Point>>.Get();
		if (pointList == null)
		{
			Debug.LogError((object)"Not found pointList (Pool_List_Point.Get())");
		}
	}

	private void Start()
	{
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Expected O, but got Unknown
		//IL_0056: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Expected O, but got Unknown
		//IL_00a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		if (!(shader == null))
		{
			if (targetCameraTransform == null)
			{
				targetCameraTransform = MonoBehaviourSingleton<AppMain>.I.mainCameraTransform;
			}
			material = new Material(shader);
			material.set_mainTexture(texture);
			mesh = new Mesh();
			int num = polygonNum * 2 + 2;
			int num2 = polygonNum * 6;
			vertices = (Vector3[])new Vector3[num];
			colors = (Color[])new Color[num];
			uvs = (Vector2[])new Vector2[num];
			triangles = new int[num2];
			Color val = color;
			Color[] array = colors;
			Vector2[] array2 = uvs;
			float num3 = reverseV ? 1f : 0f;
			float y = 1f - num3;
			for (int i = 0; i < num; i += 2)
			{
				array[i] = (array[i + 1] = val);
				array2[i].y = num3;
				array2[i + 1].y = y;
			}
			int num4 = 0;
			for (int j = 0; j < num2; j += 6)
			{
				triangles[j] = num4;
				triangles[j + 1] = 1 + num4;
				triangles[j + 2] = 2 + num4;
				triangles[j + 3] = 1 + num4;
				triangles[j + 4] = 3 + num4;
				triangles[j + 5] = 2 + num4;
				num4 += 2;
			}
			mesh.set_vertices(vertices);
			mesh.set_uv(uvs);
			mesh.set_colors(colors);
			mesh.set_triangles(triangles);
			mesh.MarkDynamic();
			Reset();
		}
	}

	private void ClearPointList()
	{
		if (pointList != null)
		{
			int i = 0;
			for (int count = pointList.Count; i < count; i++)
			{
				Point point = pointList[i];
				rymTPool<Point>.Release(ref point);
				pointList[i] = null;
			}
			pointList.Clear();
		}
	}

	private void OnDestroy()
	{
		if (pointList != null)
		{
			ClearPointList();
			rymTPool<List<Point>>.Release(ref pointList);
		}
		if (mesh != null)
		{
			Object.DestroyImmediate(mesh);
			mesh = null;
		}
		if (material != null)
		{
			Object.DestroyImmediate(material);
			material = null;
		}
	}

	public void Clear()
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0048: Unknown result type (might be due to invalid IL or missing references)
		//IL_004d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		if (deleteTime > 0f && colors != null)
		{
			Color val = color;
			Color[] array = colors;
			int i = 0;
			for (int num = array.Length; i < num; i += 2)
			{
				array[i] = (array[i + 1] = val);
			}
			mesh.set_colors(colors);
		}
		time = 0f;
		deleteTime = 0f;
		ClearPointList();
		prevPoint1 = null;
		prevPoint2 = null;
		lastBeginPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		lastEndPos = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
		autoDelete = false;
		pauseStep = 0;
	}

	public void Reset()
	{
		_transform = this.get_transform();
		Clear();
	}

	public void SetAutoDelete()
	{
		autoDelete = true;
		pauseStep = 0;
		StartDeleteFade();
	}

	public void StartDeleteFade()
	{
		emit = false;
		if (timeForDelete > 0f)
		{
			deleteTime = time;
		}
	}

	private void LateUpdate()
	{
		if (!(_transform == null))
		{
			if (!fixedUpdate)
			{
				UpdateTrail(Time.get_deltaTime());
			}
			if (pointList != null)
			{
				Draw();
			}
		}
	}

	private void FixedUpdate()
	{
		if (!(_transform == null) && fixedUpdate)
		{
			UpdateTrail(Time.get_fixedDeltaTime());
		}
	}

	private void UpdateTrail(float dt)
	{
		if (pauseStep > 0)
		{
			if (pauseStep == 2)
			{
				return;
			}
			pauseStep = 2;
		}
		time += dt;
		if (time < delayTime)
		{
			return;
		}
		while (pointList.Count != 0)
		{
			Point point = pointList[0];
			if (time < point.time + life)
			{
				break;
			}
			if (point == prevPoint2)
			{
				prevPoint2 = null;
			}
			else if (point == prevPoint1)
			{
				prevPoint1 = null;
			}
			pointList.RemoveAt(0);
			rymTPool<Point>.Release(ref point);
			dirty = true;
		}
		if (autoDelete)
		{
			if ((!(deleteTime > 0f) || !(time >= deleteTime + timeForDelete)) && pointList.Count != 0)
			{
				return;
			}
			_transform = null;
			if (onQueryDestroy != null)
			{
				if (onQueryDestroy(this))
				{
					Object.DestroyImmediate(this.get_gameObject());
				}
			}
			else
			{
				Object.DestroyImmediate(this.get_gameObject());
			}
		}
		else if (emit)
		{
			Emit();
		}
	}

	private void Emit()
	{
		//IL_0013: Unknown result type (might be due to invalid IL or missing references)
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0037: Unknown result type (might be due to invalid IL or missing references)
		//IL_003c: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_005c: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0060: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_009d: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_011d: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0133: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0155: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_015f: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0196: Unknown result type (might be due to invalid IL or missing references)
		//IL_019b: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ab: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d8: Unknown result type (might be due to invalid IL or missing references)
		//IL_0308: Unknown result type (might be due to invalid IL or missing references)
		//IL_030f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0314: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_03c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d5: Unknown result type (might be due to invalid IL or missing references)
		//IL_03df: Unknown result type (might be due to invalid IL or missing references)
		//IL_03e4: Unknown result type (might be due to invalid IL or missing references)
		Transform transform = _transform;
		Vector3 val = (axis == AXIS.X) ? transform.get_right() : ((axis != AXIS.Y) ? transform.get_forward() : transform.get_up());
		Vector3 val2 = transform.get_rotation() * offset;
		float num = width;
		Vector3 lossyScale = transform.get_lossyScale();
		if (lossyScale != Vector3.get_one())
		{
			float num2 = (lossyScale.x + lossyScale.y + lossyScale.z) * 0.333333343f;
			num *= num2;
			val2 *= num2;
		}
		Vector3 val3 = transform.get_position() + val2;
		Vector3 val4 = val * num + val3;
		if (checkMoveLength > 0f)
		{
			float num3 = checkMoveLength * checkMoveLength;
			Vector3 val5 = val3 - lastBeginPos;
			if (val5.get_sqrMagnitude() < num3)
			{
				Vector3 val6 = val4 - lastEndPos;
				if (val6.get_sqrMagnitude() < num3)
				{
					return;
				}
			}
		}
		Point point = rymTPool<Point>.Get();
		point.beginPos = val3;
		point.endPos = val4;
		point.centerPos = (val3 + val4) * 0.5f;
		point.time = time;
		lastBeginPos = val3;
		lastEndPos = val4;
		if (prevPoint1 != null && prevPoint2 != null)
		{
			Point point2 = prevPoint2;
			Point point3 = prevPoint1;
			int num4 = 0;
			Vector3 val7 = point3.centerPos - point2.centerPos;
			Vector3 val8 = point.centerPos - point3.centerPos;
			if ((float)divideAngle != 0f && val7 != val8)
			{
				num4 = (int)(Vector3.Angle(val7, val8) / (float)divideAngle);
			}
			if (divideLength != 0f)
			{
				int num5 = (int)(val8.get_magnitude() / divideLength);
				if (num4 < num5)
				{
					num4 = num5;
				}
			}
			if (num4 > 0)
			{
				Vector3 val9 = default(Vector3);
				rymUtil.CalcWayOfSpline(ref val9, ref point2.beginPos, ref point3.beginPos, ref point.beginPos);
				Vector3 val10 = default(Vector3);
				rymUtil.CalcWayOfSpline(ref val10, ref point2.endPos, ref point3.endPos, ref point.endPos);
				num4++;
				float num6 = 1f / (float)num4;
				float num7 = 0.5f / (float)num4;
				if (pointList.Count == 0)
				{
					AddPoint(point2);
					for (int i = 1; i < num4; i++)
					{
						Point point4 = rymTPool<Point>.Get();
						point4.time = (point3.time - point2.time) * ((float)i * num6) + point2.time;
						float num8 = (float)i * num7;
						rymUtil.CalcSpline(ref point4.beginPos, ref point2.beginPos, ref val9, ref point.beginPos, num8);
						rymUtil.CalcSpline(ref point4.endPos, ref point2.endPos, ref val10, ref point.endPos, num8);
						point4.centerPos = (point4.beginPos + point4.endPos) * 0.5f;
						AddPoint(point4);
					}
					AddPoint(point3);
				}
				for (int j = 1; j < num4; j++)
				{
					Point point5 = rymTPool<Point>.Get();
					point5.time = (point.time - point3.time) * ((float)j * num6) + point3.time;
					float num9 = (float)j * num7 + 0.5f;
					rymUtil.CalcSpline(ref point5.beginPos, ref point2.beginPos, ref val9, ref point.beginPos, num9);
					rymUtil.CalcSpline(ref point5.endPos, ref point2.endPos, ref val10, ref point.endPos, num9);
					point5.centerPos = (point5.beginPos + point5.endPos) * 0.5f;
					AddPoint(point5);
				}
			}
			else if (pointList.Count == 0)
			{
				AddPoint(point2);
				AddPoint(point3);
			}
			AddPoint(point);
			dirty = true;
		}
		prevPoint2 = prevPoint1;
		prevPoint1 = point;
	}

	private void AddPoint(Point point)
	{
		if (pointList.Count >= polygonNum)
		{
			Point point2 = pointList[0];
			if (point2 == prevPoint2)
			{
				prevPoint2 = null;
			}
			else if (point2 == prevPoint1)
			{
				prevPoint1 = null;
			}
			pointList.RemoveAt(0);
			rymTPool<Point>.Release(ref point2);
		}
		pointList.Add(point);
	}

	private void Draw()
	{
		//IL_00a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0129: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0176: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_017d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ea: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0205: Unknown result type (might be due to invalid IL or missing references)
		//IL_0209: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0277: Unknown result type (might be due to invalid IL or missing references)
		//IL_027c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0285: Unknown result type (might be due to invalid IL or missing references)
		//IL_028a: Unknown result type (might be due to invalid IL or missing references)
		//IL_029b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_02f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_02fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0319: Unknown result type (might be due to invalid IL or missing references)
		//IL_031b: Unknown result type (might be due to invalid IL or missing references)
		//IL_031c: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0323: Unknown result type (might be due to invalid IL or missing references)
		//IL_0325: Unknown result type (might be due to invalid IL or missing references)
		//IL_0392: Unknown result type (might be due to invalid IL or missing references)
		//IL_0398: Unknown result type (might be due to invalid IL or missing references)
		//IL_039f: Unknown result type (might be due to invalid IL or missing references)
		//IL_03a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_03d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_03db: Unknown result type (might be due to invalid IL or missing references)
		//IL_03fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0402: Unknown result type (might be due to invalid IL or missing references)
		//IL_040c: Unknown result type (might be due to invalid IL or missing references)
		//IL_041c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0429: Unknown result type (might be due to invalid IL or missing references)
		//IL_042e: Unknown result type (might be due to invalid IL or missing references)
		Vector3[] array = vertices;
		Vector2[] array2 = uvs;
		if (pointList.Count < 2)
		{
			return;
		}
		if (dirty || billboard)
		{
			int num = 0;
			float num2 = pointList[0].time;
			float num3 = pointList[pointList.Count - 1].time - num2;
			float num4 = 1f / num3;
			Point point = null;
			int i = 0;
			if (!billboard)
			{
				while (num < pointList.Count)
				{
					point = pointList[num];
					array[i] = point.beginPos;
					array[i + 1] = point.endPos;
					array2[i].x = (array2[i + 1].x = 1f - (point.time - num2) * num4);
					num++;
					i += 2;
				}
			}
			else
			{
				float num5 = width * 0.5f;
				Vector3 lossyScale = _transform.get_lossyScale();
				if (lossyScale != Vector3.get_one())
				{
					num5 *= (lossyScale.x + lossyScale.y + lossyScale.z) * 0.333333343f;
				}
				int num6 = num + 1;
				Point point2 = null;
				Vector3 position = targetCameraTransform.get_position();
				Vector3 val = Vector3.get_zero();
				while (num6 < pointList.Count)
				{
					point = pointList[num];
					point2 = pointList[num6];
					Vector3 val2 = Vector3.Cross(position - point.centerPos, point2.centerPos - point.centerPos);
					val = val2.get_normalized();
					array[i] = point.centerPos - val * num5;
					array[i + 1] = point.centerPos + val * num5;
					array2[i].x = (array2[i + 1].x = 1f - (point.time - num2) * num4);
					num++;
					num6++;
					i += 2;
				}
				array[i] = point2.centerPos - val * num5;
				array[i + 1] = point2.centerPos + val * num5;
				array2[i].x = (array2[i + 1].x = 1f - (point2.time - num2) * num4);
				i += 2;
			}
			Vector3 val3 = array[i - 2];
			for (int num7 = array.Length; i < num7; i += 2)
			{
				array[i] = (array[i + 1] = val3);
			}
			mesh.set_vertices(array);
			mesh.set_uv(array2);
			dirty = false;
		}
		if (deleteTime > 0f)
		{
			float num8 = (time - deleteTime) / timeForDelete;
			if (num8 > 1f)
			{
				num8 = 1f;
			}
			Color val4 = Color.Lerp(color, colorForDelete, num8);
			Color[] array3 = colors;
			int j = 0;
			for (int num9 = array.Length; j < num9; j += 2)
			{
				array3[j] = (array3[j + 1] = val4);
			}
			mesh.set_colors(array3);
		}
		Bounds val5 = bounds;
		val5.set_center(_transform.get_position());
		mesh.set_bounds(val5);
		Graphics.DrawMesh(mesh, Vector3.get_zero(), Quaternion.get_identity(), material, this.get_gameObject().get_layer());
	}
}
