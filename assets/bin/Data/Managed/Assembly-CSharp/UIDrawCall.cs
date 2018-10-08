using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Draw Call")]
public class UIDrawCall
{
	public enum Clipping
	{
		None = 0,
		TextureMask = 1,
		SoftClip = 3,
		ConstrainButDontClip = 4
	}

	public delegate void OnRenderCallback(Material mat);

	private const int maxIndexBufferCache = 10;

	private static BetterList<UIDrawCall> mActiveList = new BetterList<UIDrawCall>();

	private static BetterList<UIDrawCall> mInactiveList = new BetterList<UIDrawCall>();

	[NonSerialized]
	[HideInInspector]
	public int widgetCount;

	[NonSerialized]
	[HideInInspector]
	public int depthStart = 2147483647;

	[NonSerialized]
	[HideInInspector]
	public int depthEnd = -2147483648;

	[NonSerialized]
	[HideInInspector]
	public UIPanel manager;

	[NonSerialized]
	[HideInInspector]
	public UIPanel panel;

	[NonSerialized]
	[HideInInspector]
	public Texture2D clipTexture;

	[NonSerialized]
	[HideInInspector]
	public bool alwaysOnScreen;

	[NonSerialized]
	[HideInInspector]
	public BetterList<Vector3> verts = new BetterList<Vector3>();

	[NonSerialized]
	[HideInInspector]
	public BetterList<Vector3> norms = new BetterList<Vector3>();

	[NonSerialized]
	[HideInInspector]
	public BetterList<Vector4> tans = new BetterList<Vector4>();

	[NonSerialized]
	[HideInInspector]
	public BetterList<Vector2> uvs = new BetterList<Vector2>();

	[NonSerialized]
	[HideInInspector]
	public BetterList<Color32> cols = new BetterList<Color32>();

	private Material mMaterial;

	private Texture mTexture;

	private Shader mShader;

	private int mClipCount;

	private Transform mTrans;

	private Mesh mMesh;

	private MeshFilter mFilter;

	private MeshRenderer mRenderer;

	private Material mDynamicMat;

	private int[] mIndices;

	private bool mRebuildMat = true;

	private bool mLegacyShader;

	private int mRenderQueue = 3000;

	private int mTriangles;

	[NonSerialized]
	public bool isDirty;

	[NonSerialized]
	private bool mTextureClip;

	public OnRenderCallback onRender;

	private static List<int[]> mCache = new List<int[]>(10);

	private static int[] ClipRange = null;

	private static int[] ClipArgs = null;

	[Obsolete("Use UIDrawCall.activeList")]
	public static BetterList<UIDrawCall> list
	{
		get
		{
			return mActiveList;
		}
	}

	public static BetterList<UIDrawCall> activeList => mActiveList;

	public static BetterList<UIDrawCall> inactiveList => mInactiveList;

	public int renderQueue
	{
		get
		{
			return mRenderQueue;
		}
		set
		{
			if (mRenderQueue != value)
			{
				mRenderQueue = value;
				if (mDynamicMat != null)
				{
					mDynamicMat.set_renderQueue(value);
				}
			}
		}
	}

	public int sortingOrder
	{
		get
		{
			return (mRenderer != null) ? mRenderer.get_sortingOrder() : 0;
		}
		set
		{
			if (mRenderer != null && mRenderer.get_sortingOrder() != value)
			{
				mRenderer.set_sortingOrder(value);
			}
		}
	}

	public int finalRenderQueue => (!(mDynamicMat != null)) ? mRenderQueue : mDynamicMat.get_renderQueue();

	public Transform cachedTransform
	{
		get
		{
			//IL_0013: Unknown result type (might be due to invalid IL or missing references)
			//IL_0018: Expected O, but got Unknown
			if (mTrans == null)
			{
				mTrans = this.get_transform();
			}
			return mTrans;
		}
	}

	public Material baseMaterial
	{
		get
		{
			return mMaterial;
		}
		set
		{
			if (mMaterial != value)
			{
				mMaterial = value;
				mRebuildMat = true;
			}
		}
	}

	public Material dynamicMaterial => mDynamicMat;

	public Texture mainTexture
	{
		get
		{
			return mTexture;
		}
		set
		{
			mTexture = value;
			if (mDynamicMat != null)
			{
				mDynamicMat.set_mainTexture(value);
			}
		}
	}

	public Shader shader
	{
		get
		{
			return mShader;
		}
		set
		{
			if (mShader != value)
			{
				mShader = value;
				mRebuildMat = true;
			}
		}
	}

	public int triangles => (mMesh != null) ? mTriangles : 0;

	public bool isClipped => mClipCount != 0;

	public UIDrawCall()
		: this()
	{
	}

	public static Shader ShaderFind(string name)
	{
		return ResourceUtility.FindShader(name);
	}

	private void CreateMaterial()
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0237: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0252: Expected O, but got Unknown
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0340: Expected O, but got Unknown
		mTextureClip = false;
		mLegacyShader = false;
		mClipCount = panel.clipCount;
		string text = (mShader != null) ? mShader.get_name() : ((!(mMaterial != null)) ? "Unlit/Transparent Colored" : mMaterial.get_shader().get_name());
		text = text.Replace("GUI/Text Shader", "Unlit/Text");
		if (text.Length > 2 && text[text.Length - 2] == ' ')
		{
			int num = text[text.Length - 1];
			if (num > 48 && num <= 57)
			{
				text = text.Substring(0, text.Length - 2);
			}
		}
		if (text.StartsWith("Hidden/"))
		{
			text = text.Substring(7);
		}
		text = text.Replace(" (SoftClip)", string.Empty);
		text = text.Replace(" (TextureClip)", string.Empty);
		if (panel.clipping == Clipping.TextureMask)
		{
			mTextureClip = true;
			shader = ShaderFind("Hidden/" + text + " (TextureClip)");
		}
		else if (mClipCount != 0)
		{
			shader = ShaderFind("Hidden/" + text + " " + mClipCount);
			if (shader == null)
			{
				shader = ShaderFind(text + " " + mClipCount);
			}
			if (shader == null && mClipCount == 1)
			{
				mLegacyShader = true;
				shader = ShaderFind(text + " (SoftClip)");
			}
		}
		else
		{
			shader = ShaderFind(text);
		}
		if (shader == null)
		{
			shader = ShaderFind("Unlit/Transparent Colored");
		}
		if (mMaterial != null && mMaterial.get_shader().get_isSupported())
		{
			mDynamicMat = new Material(mMaterial);
			mDynamicMat.set_name("[NGUI] " + mMaterial.get_name());
			mDynamicMat.set_hideFlags(60);
			mDynamicMat.CopyPropertiesFromMaterial(mMaterial);
			string[] shaderKeywords = mMaterial.get_shaderKeywords();
			for (int i = 0; i < shaderKeywords.Length; i++)
			{
				mDynamicMat.EnableKeyword(shaderKeywords[i]);
			}
			if (shader != null)
			{
				mDynamicMat.set_shader(shader);
			}
			else if (mClipCount != 0)
			{
				Debug.LogError((object)(text + " shader doesn't have a clipped shader version for " + mClipCount + " clip regions"));
			}
		}
		else
		{
			mDynamicMat = new Material(shader);
			mDynamicMat.set_name("[NGUI] " + shader.get_name());
			mDynamicMat.set_hideFlags(60);
		}
	}

	private Material RebuildMaterial()
	{
		NGUITools.DestroyImmediate(mDynamicMat);
		CreateMaterial();
		mDynamicMat.set_renderQueue(mRenderQueue);
		if (mTexture != null)
		{
			mDynamicMat.set_mainTexture(mTexture);
		}
		if (mRenderer != null)
		{
			mRenderer.set_sharedMaterials((Material[])new Material[1]
			{
				mDynamicMat
			});
		}
		return mDynamicMat;
	}

	private void UpdateMaterials()
	{
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		if (mRebuildMat || mDynamicMat == null || mClipCount != panel.clipCount || mTextureClip != (panel.clipping == Clipping.TextureMask))
		{
			RebuildMaterial();
			mRebuildMat = false;
		}
		else if (mRenderer.get_sharedMaterial() != mDynamicMat)
		{
			mRenderer.set_sharedMaterials((Material[])new Material[1]
			{
				mDynamicMat
			});
		}
	}

	public void UpdateGeometry(int widgetCount)
	{
		//IL_0057: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Expected O, but got Unknown
		//IL_0415: Unknown result type (might be due to invalid IL or missing references)
		//IL_042b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0467: Unknown result type (might be due to invalid IL or missing references)
		//IL_0489: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_04bf: Unknown result type (might be due to invalid IL or missing references)
		this.widgetCount = widgetCount;
		int size = verts.size;
		if (size > 0 && size == uvs.size && size == cols.size && size % 4 == 0)
		{
			if (mFilter == null)
			{
				mFilter = this.get_gameObject().GetComponent<MeshFilter>();
			}
			if (mFilter == null)
			{
				mFilter = this.get_gameObject().AddComponent<MeshFilter>();
			}
			if (verts.size < 65000)
			{
				int num = (size >> 1) * 3;
				bool flag = mIndices == null || mIndices.Length != num;
				if (mMesh == null)
				{
					mMesh = new Mesh();
					mMesh.set_hideFlags(52);
					mMesh.set_name((!(mMaterial != null)) ? "[NGUI] Mesh" : ("[NGUI] " + mMaterial.get_name()));
					mMesh.MarkDynamic();
					flag = true;
				}
				bool flag2 = uvs.buffer.Length != verts.buffer.Length || cols.buffer.Length != verts.buffer.Length || (norms.buffer != null && norms.buffer.Length != verts.buffer.Length) || (tans.buffer != null && tans.buffer.Length != verts.buffer.Length);
				if (!flag2 && panel.renderQueue != 0)
				{
					flag2 = (mMesh == null || mMesh.get_vertexCount() != verts.buffer.Length);
				}
				mTriangles = verts.size >> 1;
				if (flag2 || verts.buffer.Length > 65000)
				{
					if (flag2 || mMesh.get_vertexCount() != verts.size)
					{
						mMesh.Clear();
						flag = true;
					}
					mMesh.set_vertices(verts.ToArray());
					mMesh.set_uv(uvs.ToArray());
					mMesh.set_colors32(cols.ToArray());
					if (norms != null)
					{
						mMesh.set_normals(norms.ToArray());
					}
					if (tans != null)
					{
						mMesh.set_tangents(tans.ToArray());
					}
				}
				else
				{
					if (mMesh.get_vertexCount() != verts.buffer.Length)
					{
						mMesh.Clear();
						flag = true;
					}
					mMesh.set_vertices(verts.buffer);
					mMesh.set_uv(uvs.buffer);
					mMesh.set_colors32(cols.buffer);
					if (norms != null)
					{
						mMesh.set_normals(norms.buffer);
					}
					if (tans != null)
					{
						mMesh.set_tangents(tans.buffer);
					}
				}
				if (flag)
				{
					mIndices = GenerateCachedIndexBuffer(size, num);
					mMesh.set_triangles(mIndices);
				}
				if (flag2 || !alwaysOnScreen)
				{
					mMesh.RecalculateBounds();
				}
				mFilter.set_mesh(mMesh);
			}
			else
			{
				mTriangles = 0;
				if (mFilter.get_mesh() != null)
				{
					mFilter.get_mesh().Clear();
				}
				Debug.LogError((object)("Too many vertices on one panel: " + verts.size));
			}
			if (mRenderer == null)
			{
				mRenderer = this.get_gameObject().GetComponent<MeshRenderer>();
			}
			if (mRenderer == null)
			{
				mRenderer = this.get_gameObject().AddComponent<MeshRenderer>();
			}
			UpdateMaterials();
		}
		else
		{
			if (mFilter.get_mesh() != null)
			{
				mFilter.get_mesh().Clear();
			}
			Debug.LogError((object)("UIWidgets must fill the buffer with 4 vertices per quad. Found " + size));
		}
		verts.Clear();
		uvs.Clear();
		cols.Clear();
		norms.Clear();
		tans.Clear();
	}

	private int[] GenerateCachedIndexBuffer(int vertexCount, int indexCount)
	{
		int i = 0;
		for (int count = mCache.Count; i < count; i++)
		{
			int[] array = mCache[i];
			if (array != null && array.Length == indexCount)
			{
				return array;
			}
		}
		int[] array2 = new int[indexCount];
		int num = 0;
		for (int j = 0; j < vertexCount; j += 4)
		{
			array2[num++] = j;
			array2[num++] = j + 1;
			array2[num++] = j + 2;
			array2[num++] = j + 2;
			array2[num++] = j + 3;
			array2[num++] = j;
		}
		if (mCache.Count > 10)
		{
			mCache.RemoveAt(0);
		}
		mCache.Add(array2);
		return array2;
	}

	private void OnWillRenderObject()
	{
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0062: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0190: Unknown result type (might be due to invalid IL or missing references)
		//IL_0195: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01da: Unknown result type (might be due to invalid IL or missing references)
		//IL_01df: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f7: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_028b: Unknown result type (might be due to invalid IL or missing references)
		//IL_02b5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_0376: Unknown result type (might be due to invalid IL or missing references)
		//IL_0383: Unknown result type (might be due to invalid IL or missing references)
		//IL_0395: Unknown result type (might be due to invalid IL or missing references)
		//IL_0397: Unknown result type (might be due to invalid IL or missing references)
		UpdateMaterials();
		if (onRender != null)
		{
			onRender(mDynamicMat ?? mMaterial);
		}
		if (!(mDynamicMat == null) && mClipCount != 0)
		{
			if (mTextureClip)
			{
				Vector4 drawCallClipRange = panel.drawCallClipRange;
				Vector2 clipSoftness = panel.clipSoftness;
				Vector2 val = default(Vector2);
				val._002Ector(1000f, 1000f);
				if (clipSoftness.x > 0f)
				{
					val.x = drawCallClipRange.z / clipSoftness.x;
				}
				if (clipSoftness.y > 0f)
				{
					val.y = drawCallClipRange.w / clipSoftness.y;
				}
				mDynamicMat.SetVector(ClipRange[0], new Vector4((0f - drawCallClipRange.x) / drawCallClipRange.z, (0f - drawCallClipRange.y) / drawCallClipRange.w, 1f / drawCallClipRange.z, 1f / drawCallClipRange.w));
				mDynamicMat.SetTexture("_ClipTex", clipTexture);
			}
			else if (!mLegacyShader)
			{
				UIPanel parentPanel = panel;
				int num = 0;
				while (parentPanel != null)
				{
					if (parentPanel.hasClipping)
					{
						float angle = 0f;
						Vector4 drawCallClipRange2 = parentPanel.drawCallClipRange;
						if (parentPanel != panel)
						{
							Vector3 val2 = parentPanel.cachedTransform.InverseTransformPoint(panel.cachedTransform.get_position());
							drawCallClipRange2.x -= val2.x;
							drawCallClipRange2.y -= val2.y;
							Quaternion rotation = panel.cachedTransform.get_rotation();
							Vector3 eulerAngles = rotation.get_eulerAngles();
							Quaternion rotation2 = parentPanel.cachedTransform.get_rotation();
							Vector3 eulerAngles2 = rotation2.get_eulerAngles();
							Vector3 val3 = eulerAngles2 - eulerAngles;
							val3.x = NGUIMath.WrapAngle(val3.x);
							val3.y = NGUIMath.WrapAngle(val3.y);
							val3.z = NGUIMath.WrapAngle(val3.z);
							if (Mathf.Abs(val3.x) > 0.001f || Mathf.Abs(val3.y) > 0.001f)
							{
								Debug.LogWarning((object)"Panel can only be clipped properly if X and Y rotation is left at 0", panel);
							}
							angle = val3.z;
						}
						SetClipping(num++, drawCallClipRange2, parentPanel.clipSoftness, angle);
					}
					parentPanel = parentPanel.parentPanel;
				}
			}
			else
			{
				Vector2 clipSoftness2 = panel.clipSoftness;
				Vector4 drawCallClipRange3 = panel.drawCallClipRange;
				Vector2 mainTextureOffset = default(Vector2);
				mainTextureOffset._002Ector((0f - drawCallClipRange3.x) / drawCallClipRange3.z, (0f - drawCallClipRange3.y) / drawCallClipRange3.w);
				Vector2 mainTextureScale = default(Vector2);
				mainTextureScale._002Ector(1f / drawCallClipRange3.z, 1f / drawCallClipRange3.w);
				Vector2 val4 = default(Vector2);
				val4._002Ector(1000f, 1000f);
				if (clipSoftness2.x > 0f)
				{
					val4.x = drawCallClipRange3.z / clipSoftness2.x;
				}
				if (clipSoftness2.y > 0f)
				{
					val4.y = drawCallClipRange3.w / clipSoftness2.y;
				}
				mDynamicMat.set_mainTextureOffset(mainTextureOffset);
				mDynamicMat.set_mainTextureScale(mainTextureScale);
				mDynamicMat.SetVector("_ClipSharpness", Vector4.op_Implicit(val4));
			}
		}
	}

	private void SetClipping(int index, Vector4 cr, Vector2 soft, float angle)
	{
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f0: Unknown result type (might be due to invalid IL or missing references)
		angle *= -0.0174532924f;
		Vector2 val = default(Vector2);
		val._002Ector(1000f, 1000f);
		if (soft.x > 0f)
		{
			val.x = cr.z / soft.x;
		}
		if (soft.y > 0f)
		{
			val.y = cr.w / soft.y;
		}
		if (index < ClipRange.Length)
		{
			mDynamicMat.SetVector(ClipRange[index], new Vector4((0f - cr.x) / cr.z, (0f - cr.y) / cr.w, 1f / cr.z, 1f / cr.w));
			mDynamicMat.SetVector(ClipArgs[index], new Vector4(val.x, val.y, Mathf.Sin(angle), Mathf.Cos(angle)));
		}
	}

	private void Awake()
	{
		if (ClipRange == null)
		{
			ClipRange = new int[4]
			{
				Shader.PropertyToID("_ClipRange0"),
				Shader.PropertyToID("_ClipRange1"),
				Shader.PropertyToID("_ClipRange2"),
				Shader.PropertyToID("_ClipRange4")
			};
		}
		if (ClipArgs == null)
		{
			ClipArgs = new int[4]
			{
				Shader.PropertyToID("_ClipArgs0"),
				Shader.PropertyToID("_ClipArgs1"),
				Shader.PropertyToID("_ClipArgs2"),
				Shader.PropertyToID("_ClipArgs3")
			};
		}
	}

	private void OnEnable()
	{
		mRebuildMat = true;
	}

	private void OnDisable()
	{
		depthStart = 2147483647;
		depthEnd = -2147483648;
		panel = null;
		manager = null;
		mMaterial = null;
		mTexture = null;
		clipTexture = null;
		if (mRenderer != null)
		{
			mRenderer.set_sharedMaterials((Material[])new Material[0]);
		}
		NGUITools.DestroyImmediate(mDynamicMat);
		mDynamicMat = null;
	}

	private void OnDestroy()
	{
		NGUITools.DestroyImmediate(mMesh);
		mMesh = null;
	}

	public static UIDrawCall Create(UIPanel panel, Material mat, Texture tex, Shader shader)
	{
		return Create(null, panel, mat, tex, shader);
	}

	private static UIDrawCall Create(string name, UIPanel pan, Material mat, Texture tex, Shader shader)
	{
		//IL_0008: Unknown result type (might be due to invalid IL or missing references)
		UIDrawCall uIDrawCall = Create(name);
		uIDrawCall.get_gameObject().set_layer(pan.cachedGameObject.get_layer());
		uIDrawCall.baseMaterial = mat;
		uIDrawCall.mainTexture = tex;
		uIDrawCall.shader = shader;
		uIDrawCall.renderQueue = pan.startingRenderQueue;
		uIDrawCall.sortingOrder = pan.sortingOrder;
		uIDrawCall.manager = pan;
		return uIDrawCall;
	}

	private static UIDrawCall Create(string name)
	{
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Expected O, but got Unknown
		//IL_0042: Unknown result type (might be due to invalid IL or missing references)
		//IL_0047: Expected O, but got Unknown
		if (mInactiveList.size > 0)
		{
			UIDrawCall uIDrawCall = mInactiveList.Pop();
			mActiveList.Add(uIDrawCall);
			if (name != null)
			{
				uIDrawCall.set_name(name);
			}
			NGUITools.SetActive(uIDrawCall.get_gameObject(), true);
			return uIDrawCall;
		}
		GameObject val = new GameObject(name);
		Object.DontDestroyOnLoad(val);
		UIDrawCall uIDrawCall2 = val.AddComponent<UIDrawCall>();
		mActiveList.Add(uIDrawCall2);
		return uIDrawCall2;
	}

	public static void ClearAll()
	{
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003e: Expected O, but got Unknown
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Expected O, but got Unknown
		bool isPlaying = Application.get_isPlaying();
		int num = mActiveList.size;
		while (num > 0)
		{
			UIDrawCall uIDrawCall = mActiveList[--num];
			if (Object.op_Implicit(uIDrawCall))
			{
				if (isPlaying)
				{
					NGUITools.SetActive(uIDrawCall.get_gameObject(), false);
				}
				else
				{
					NGUITools.DestroyImmediate(uIDrawCall.get_gameObject());
				}
			}
		}
		mActiveList.Clear();
	}

	public static void ReleaseAll()
	{
		ClearAll();
		ReleaseInactive();
	}

	public static void ReleaseInactive()
	{
		//IL_002c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Expected O, but got Unknown
		int num = mInactiveList.size;
		while (num > 0)
		{
			UIDrawCall uIDrawCall = mInactiveList[--num];
			if (Object.op_Implicit(uIDrawCall))
			{
				NGUITools.DestroyImmediate(uIDrawCall.get_gameObject());
			}
		}
		mInactiveList.Clear();
	}

	public static int Count(UIPanel panel)
	{
		int num = 0;
		for (int i = 0; i < mActiveList.size; i++)
		{
			if (mActiveList[i].manager == panel)
			{
				num++;
			}
		}
		return num;
	}

	public static void Destroy(UIDrawCall dc)
	{
		//IL_002d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Expected O, but got Unknown
		//IL_0055: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Expected O, but got Unknown
		if (Object.op_Implicit(dc))
		{
			dc.onRender = null;
			if (Application.get_isPlaying())
			{
				if (mActiveList.Remove(dc))
				{
					NGUITools.SetActive(dc.get_gameObject(), false);
					mInactiveList.Add(dc);
				}
			}
			else
			{
				mActiveList.Remove(dc);
				NGUITools.DestroyImmediate(dc.get_gameObject());
			}
		}
	}
}
