// Decompiled with JetBrains decompiler
// Type: UIDrawCall
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
[ExecuteInEditMode]
[AddComponentMenu("NGUI/Internal/Draw Call")]
public class UIDrawCall : MonoBehaviour
{
  private static BetterList<UIDrawCall> mActiveList = new BetterList<UIDrawCall>();
  private static BetterList<UIDrawCall> mInactiveList = new BetterList<UIDrawCall>();
  [HideInInspector]
  [NonSerialized]
  public int widgetCount;
  [HideInInspector]
  [NonSerialized]
  public int depthStart = int.MaxValue;
  [HideInInspector]
  [NonSerialized]
  public int depthEnd = int.MinValue;
  [HideInInspector]
  [NonSerialized]
  public UIPanel manager;
  [HideInInspector]
  [NonSerialized]
  public UIPanel panel;
  [HideInInspector]
  [NonSerialized]
  public Texture2D clipTexture;
  [HideInInspector]
  [NonSerialized]
  public bool alwaysOnScreen;
  [HideInInspector]
  [NonSerialized]
  public BetterList<Vector3> verts = new BetterList<Vector3>();
  [HideInInspector]
  [NonSerialized]
  public BetterList<Vector3> norms = new BetterList<Vector3>();
  [HideInInspector]
  [NonSerialized]
  public BetterList<Vector4> tans = new BetterList<Vector4>();
  [HideInInspector]
  [NonSerialized]
  public BetterList<Vector2> uvs = new BetterList<Vector2>();
  [HideInInspector]
  [NonSerialized]
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
  public UIDrawCall.OnRenderCallback onRender;
  private const int maxIndexBufferCache = 10;
  private static List<int[]> mCache = new List<int[]>(10);
  private static int[] ClipRange = (int[]) null;
  private static int[] ClipArgs = (int[]) null;

  public static Shader ShaderFind(string name) => ResourceUtility.FindShader(name);

  [Obsolete("Use UIDrawCall.activeList")]
  public static BetterList<UIDrawCall> list => UIDrawCall.mActiveList;

  public static BetterList<UIDrawCall> activeList => UIDrawCall.mActiveList;

  public static BetterList<UIDrawCall> inactiveList => UIDrawCall.mInactiveList;

  public int renderQueue
  {
    get => this.mRenderQueue;
    set
    {
      if (this.mRenderQueue == value)
        return;
      this.mRenderQueue = value;
      if (!Object.op_Inequality((Object) this.mDynamicMat, (Object) null))
        return;
      this.mDynamicMat.renderQueue = value;
    }
  }

  public int sortingOrder
  {
    get
    {
      return !Object.op_Inequality((Object) this.mRenderer, (Object) null) ? 0 : ((Renderer) this.mRenderer).sortingOrder;
    }
    set
    {
      if (!Object.op_Inequality((Object) this.mRenderer, (Object) null) || ((Renderer) this.mRenderer).sortingOrder == value)
        return;
      ((Renderer) this.mRenderer).sortingOrder = value;
    }
  }

  public int finalRenderQueue
  {
    get
    {
      return !Object.op_Inequality((Object) this.mDynamicMat, (Object) null) ? this.mRenderQueue : this.mDynamicMat.renderQueue;
    }
  }

  public Transform cachedTransform
  {
    get
    {
      if (Object.op_Equality((Object) this.mTrans, (Object) null))
        this.mTrans = ((Component) this).transform;
      return this.mTrans;
    }
  }

  public Material baseMaterial
  {
    get => this.mMaterial;
    set
    {
      if (!Object.op_Inequality((Object) this.mMaterial, (Object) value))
        return;
      this.mMaterial = value;
      this.mRebuildMat = true;
    }
  }

  public Material dynamicMaterial => this.mDynamicMat;

  public Texture mainTexture
  {
    get => this.mTexture;
    set
    {
      this.mTexture = value;
      if (!Object.op_Inequality((Object) this.mDynamicMat, (Object) null))
        return;
      this.mDynamicMat.mainTexture = value;
    }
  }

  public Shader shader
  {
    get => this.mShader;
    set
    {
      if (!Object.op_Inequality((Object) this.mShader, (Object) value))
        return;
      this.mShader = value;
      this.mRebuildMat = true;
    }
  }

  public int triangles
  {
    get => !Object.op_Inequality((Object) this.mMesh, (Object) null) ? 0 : this.mTriangles;
  }

  public bool isClipped => this.mClipCount != 0;

  private void CreateMaterial()
  {
    this.mTextureClip = false;
    this.mLegacyShader = false;
    this.mClipCount = this.panel.clipCount;
    string str = (Object.op_Inequality((Object) this.mShader, (Object) null) ? ((Object) this.mShader).name : (Object.op_Inequality((Object) this.mMaterial, (Object) null) ? ((Object) this.mMaterial.shader).name : "Unlit/Transparent Colored")).Replace("GUI/Text Shader", "Unlit/Text");
    if (str.Length > 2 && str[str.Length - 2] == ' ')
    {
      int num = (int) str[str.Length - 1];
      if (num > 48 /*0x30*/ && num <= 57)
        str = str.Substring(0, str.Length - 2);
    }
    if (str.StartsWith("Hidden/"))
      str = str.Substring(7);
    string name = str.Replace(" (SoftClip)", "").Replace(" (TextureClip)", "");
    if (this.panel.clipping == UIDrawCall.Clipping.TextureMask)
    {
      this.mTextureClip = true;
      this.shader = UIDrawCall.ShaderFind($"Hidden/{name} (TextureClip)");
    }
    else if (this.mClipCount != 0)
    {
      this.shader = UIDrawCall.ShaderFind($"Hidden/{name} {(object) this.mClipCount}");
      if (Object.op_Equality((Object) this.shader, (Object) null))
        this.shader = UIDrawCall.ShaderFind($"{name} {(object) this.mClipCount}");
      if (Object.op_Equality((Object) this.shader, (Object) null) && this.mClipCount == 1)
      {
        this.mLegacyShader = true;
        this.shader = UIDrawCall.ShaderFind(name + " (SoftClip)");
      }
    }
    else
      this.shader = UIDrawCall.ShaderFind(name);
    if (Object.op_Equality((Object) this.shader, (Object) null))
      this.shader = UIDrawCall.ShaderFind("Unlit/Transparent Colored");
    if (Object.op_Inequality((Object) this.mMaterial, (Object) null) && this.mMaterial.shader.isSupported)
    {
      this.mDynamicMat = new Material(this.mMaterial);
      ((Object) this.mDynamicMat).name = "[NGUI] " + ((Object) this.mMaterial).name;
      ((Object) this.mDynamicMat).hideFlags = (HideFlags) 60;
      this.mDynamicMat.CopyPropertiesFromMaterial(this.mMaterial);
      foreach (string shaderKeyword in this.mMaterial.shaderKeywords)
        this.mDynamicMat.EnableKeyword(shaderKeyword);
      if (Object.op_Inequality((Object) this.shader, (Object) null))
      {
        this.mDynamicMat.shader = this.shader;
      }
      else
      {
        if (this.mClipCount == 0)
          return;
        Debug.LogError((object) $"{name} shader doesn't have a clipped shader version for {(object) this.mClipCount} clip regions");
      }
    }
    else
    {
      this.mDynamicMat = new Material(this.shader);
      ((Object) this.mDynamicMat).name = "[NGUI] " + ((Object) this.shader).name;
      ((Object) this.mDynamicMat).hideFlags = (HideFlags) 60;
    }
  }

  private Material RebuildMaterial()
  {
    NGUITools.DestroyImmediate((Object) this.mDynamicMat);
    this.CreateMaterial();
    this.mDynamicMat.renderQueue = this.mRenderQueue;
    if (Object.op_Inequality((Object) this.mTexture, (Object) null))
      this.mDynamicMat.mainTexture = this.mTexture;
    if (Object.op_Inequality((Object) this.mRenderer, (Object) null))
      ((Renderer) this.mRenderer).sharedMaterials = new Material[1]
      {
        this.mDynamicMat
      };
    return this.mDynamicMat;
  }

  private void UpdateMaterials()
  {
    if (this.mRebuildMat || Object.op_Equality((Object) this.mDynamicMat, (Object) null) || this.mClipCount != this.panel.clipCount || this.mTextureClip != (this.panel.clipping == UIDrawCall.Clipping.TextureMask))
    {
      this.RebuildMaterial();
      this.mRebuildMat = false;
    }
    else
    {
      if (!Object.op_Inequality((Object) ((Renderer) this.mRenderer).sharedMaterial, (Object) this.mDynamicMat))
        return;
      ((Renderer) this.mRenderer).sharedMaterials = new Material[1]
      {
        this.mDynamicMat
      };
    }
  }

  public void UpdateGeometry(int widgetCount)
  {
    this.widgetCount = widgetCount;
    int size = this.verts.size;
    if (size > 0 && size == this.uvs.size && size == this.cols.size && size % 4 == 0)
    {
      if (Object.op_Equality((Object) this.mFilter, (Object) null))
        this.mFilter = ((Component) this).gameObject.GetComponent<MeshFilter>();
      if (Object.op_Equality((Object) this.mFilter, (Object) null))
        this.mFilter = ((Component) this).gameObject.AddComponent<MeshFilter>();
      if (this.verts.size < 65000)
      {
        int indexCount = (size >> 1) * 3;
        bool flag1 = this.mIndices == null || this.mIndices.Length != indexCount;
        if (Object.op_Equality((Object) this.mMesh, (Object) null))
        {
          this.mMesh = new Mesh();
          ((Object) this.mMesh).hideFlags = (HideFlags) 52;
          ((Object) this.mMesh).name = Object.op_Inequality((Object) this.mMaterial, (Object) null) ? "[NGUI] " + ((Object) this.mMaterial).name : "[NGUI] Mesh";
          this.mMesh.MarkDynamic();
          flag1 = true;
        }
        bool flag2 = this.uvs.buffer.Length != this.verts.buffer.Length || this.cols.buffer.Length != this.verts.buffer.Length || this.norms.buffer != null && this.norms.buffer.Length != this.verts.buffer.Length || this.tans.buffer != null && this.tans.buffer.Length != this.verts.buffer.Length;
        if (!flag2 && this.panel.renderQueue != UIPanel.RenderQueue.Automatic)
          flag2 = Object.op_Equality((Object) this.mMesh, (Object) null) || this.mMesh.vertexCount != this.verts.buffer.Length;
        this.mTriangles = this.verts.size >> 1;
        if (flag2 || this.verts.buffer.Length > 65000)
        {
          if (flag2 || this.mMesh.vertexCount != this.verts.size)
          {
            this.mMesh.Clear();
            flag1 = true;
          }
          this.mMesh.vertices = this.verts.ToArray();
          this.mMesh.uv = this.uvs.ToArray();
          this.mMesh.colors32 = this.cols.ToArray();
          if (this.norms != null)
            this.mMesh.normals = this.norms.ToArray();
          if (this.tans != null)
            this.mMesh.tangents = this.tans.ToArray();
        }
        else
        {
          if (this.mMesh.vertexCount != this.verts.buffer.Length)
          {
            this.mMesh.Clear();
            flag1 = true;
          }
          this.mMesh.vertices = this.verts.buffer;
          this.mMesh.uv = this.uvs.buffer;
          this.mMesh.colors32 = this.cols.buffer;
          if (this.norms != null)
            this.mMesh.normals = this.norms.buffer;
          if (this.tans != null)
            this.mMesh.tangents = this.tans.buffer;
        }
        if (flag1)
        {
          this.mIndices = this.GenerateCachedIndexBuffer(size, indexCount);
          this.mMesh.triangles = this.mIndices;
        }
        if (flag2 || !this.alwaysOnScreen)
          this.mMesh.RecalculateBounds();
        this.mFilter.mesh = this.mMesh;
      }
      else
      {
        this.mTriangles = 0;
        if (Object.op_Inequality((Object) this.mFilter.mesh, (Object) null))
          this.mFilter.mesh.Clear();
        Debug.LogError((object) ("Too many vertices on one panel: " + (object) this.verts.size));
      }
      if (Object.op_Equality((Object) this.mRenderer, (Object) null))
        this.mRenderer = ((Component) this).gameObject.GetComponent<MeshRenderer>();
      if (Object.op_Equality((Object) this.mRenderer, (Object) null))
        this.mRenderer = ((Component) this).gameObject.AddComponent<MeshRenderer>();
      this.UpdateMaterials();
    }
    else
    {
      if (Object.op_Inequality((Object) this.mFilter.mesh, (Object) null))
        this.mFilter.mesh.Clear();
      Debug.LogError((object) ("UIWidgets must fill the buffer with 4 vertices per quad. Found " + (object) size));
    }
    this.verts.Clear();
    this.uvs.Clear();
    this.cols.Clear();
    this.norms.Clear();
    this.tans.Clear();
  }

  private int[] GenerateCachedIndexBuffer(int vertexCount, int indexCount)
  {
    int index1 = 0;
    for (int count = UIDrawCall.mCache.Count; index1 < count; ++index1)
    {
      int[] cachedIndexBuffer = UIDrawCall.mCache[index1];
      if (cachedIndexBuffer != null && cachedIndexBuffer.Length == indexCount)
        return cachedIndexBuffer;
    }
    int[] cachedIndexBuffer1 = new int[indexCount];
    int num1 = 0;
    for (int index2 = 0; index2 < vertexCount; index2 += 4)
    {
      int[] numArray1 = cachedIndexBuffer1;
      int index3 = num1;
      int num2 = index3 + 1;
      int num3 = index2;
      numArray1[index3] = num3;
      int[] numArray2 = cachedIndexBuffer1;
      int index4 = num2;
      int num4 = index4 + 1;
      int num5 = index2 + 1;
      numArray2[index4] = num5;
      int[] numArray3 = cachedIndexBuffer1;
      int index5 = num4;
      int num6 = index5 + 1;
      int num7 = index2 + 2;
      numArray3[index5] = num7;
      int[] numArray4 = cachedIndexBuffer1;
      int index6 = num6;
      int num8 = index6 + 1;
      int num9 = index2 + 2;
      numArray4[index6] = num9;
      int[] numArray5 = cachedIndexBuffer1;
      int index7 = num8;
      int num10 = index7 + 1;
      int num11 = index2 + 3;
      numArray5[index7] = num11;
      int[] numArray6 = cachedIndexBuffer1;
      int index8 = num10;
      num1 = index8 + 1;
      int num12 = index2;
      numArray6[index8] = num12;
    }
    if (UIDrawCall.mCache.Count > 10)
      UIDrawCall.mCache.RemoveAt(0);
    UIDrawCall.mCache.Add(cachedIndexBuffer1);
    return cachedIndexBuffer1;
  }

  private void OnWillRenderObject()
  {
    this.UpdateMaterials();
    if (this.onRender != null)
      this.onRender(this.mDynamicMat ?? this.mMaterial);
    if (Object.op_Equality((Object) this.mDynamicMat, (Object) null) || this.mClipCount == 0)
      return;
    if (this.mTextureClip)
    {
      Vector4 drawCallClipRange = this.panel.drawCallClipRange;
      Vector2 clipSoftness = this.panel.clipSoftness;
      Vector2 vector2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2).\u002Ector(1000f, 1000f);
      if ((double) clipSoftness.x > 0.0)
        vector2.x = drawCallClipRange.z / clipSoftness.x;
      if ((double) clipSoftness.y > 0.0)
        vector2.y = drawCallClipRange.w / clipSoftness.y;
      this.mDynamicMat.SetVector(UIDrawCall.ClipRange[0], new Vector4(-drawCallClipRange.x / drawCallClipRange.z, -drawCallClipRange.y / drawCallClipRange.w, 1f / drawCallClipRange.z, 1f / drawCallClipRange.w));
      this.mDynamicMat.SetTexture("_ClipTex", (Texture) this.clipTexture);
    }
    else if (!this.mLegacyShader)
    {
      UIPanel uiPanel = this.panel;
      int num = 0;
      for (; Object.op_Inequality((Object) uiPanel, (Object) null); uiPanel = uiPanel.parentPanel)
      {
        if (uiPanel.hasClipping)
        {
          float angle = 0.0f;
          Vector4 drawCallClipRange = uiPanel.drawCallClipRange;
          if (Object.op_Inequality((Object) uiPanel, (Object) this.panel))
          {
            Vector3 vector3_1 = uiPanel.cachedTransform.InverseTransformPoint(this.panel.cachedTransform.position);
            drawCallClipRange.x -= vector3_1.x;
            drawCallClipRange.y -= vector3_1.y;
            Quaternion rotation1 = this.panel.cachedTransform.rotation;
            Vector3 eulerAngles = ((Quaternion) ref rotation1).eulerAngles;
            Quaternion rotation2 = uiPanel.cachedTransform.rotation;
            Vector3 vector3_2 = Vector3.op_Subtraction(((Quaternion) ref rotation2).eulerAngles, eulerAngles);
            vector3_2.x = NGUIMath.WrapAngle(vector3_2.x);
            vector3_2.y = NGUIMath.WrapAngle(vector3_2.y);
            vector3_2.z = NGUIMath.WrapAngle(vector3_2.z);
            if ((double) Mathf.Abs(vector3_2.x) > 1.0 / 1000.0 || (double) Mathf.Abs(vector3_2.y) > 1.0 / 1000.0)
              Debug.LogWarning((object) "Panel can only be clipped properly if X and Y rotation is left at 0", (Object) this.panel);
            angle = vector3_2.z;
          }
          this.SetClipping(num++, drawCallClipRange, uiPanel.clipSoftness, angle);
        }
      }
    }
    else
    {
      Vector2 clipSoftness = this.panel.clipSoftness;
      Vector4 drawCallClipRange = this.panel.drawCallClipRange;
      Vector2 vector2_1;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_1).\u002Ector(-drawCallClipRange.x / drawCallClipRange.z, -drawCallClipRange.y / drawCallClipRange.w);
      Vector2 vector2_2;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_2).\u002Ector(1f / drawCallClipRange.z, 1f / drawCallClipRange.w);
      Vector2 vector2_3;
      // ISSUE: explicit constructor call
      ((Vector2) ref vector2_3).\u002Ector(1000f, 1000f);
      if ((double) clipSoftness.x > 0.0)
        vector2_3.x = drawCallClipRange.z / clipSoftness.x;
      if ((double) clipSoftness.y > 0.0)
        vector2_3.y = drawCallClipRange.w / clipSoftness.y;
      this.mDynamicMat.mainTextureOffset = vector2_1;
      this.mDynamicMat.mainTextureScale = vector2_2;
      this.mDynamicMat.SetVector("_ClipSharpness", Vector4.op_Implicit(vector2_3));
    }
  }

  private void SetClipping(int index, Vector4 cr, Vector2 soft, float angle)
  {
    angle *= -1f * (float) Math.PI / 180f;
    Vector2 vector2;
    // ISSUE: explicit constructor call
    ((Vector2) ref vector2).\u002Ector(1000f, 1000f);
    if ((double) soft.x > 0.0)
      vector2.x = cr.z / soft.x;
    if ((double) soft.y > 0.0)
      vector2.y = cr.w / soft.y;
    if (index >= UIDrawCall.ClipRange.Length)
      return;
    this.mDynamicMat.SetVector(UIDrawCall.ClipRange[index], new Vector4(-cr.x / cr.z, -cr.y / cr.w, 1f / cr.z, 1f / cr.w));
    this.mDynamicMat.SetVector(UIDrawCall.ClipArgs[index], new Vector4(vector2.x, vector2.y, Mathf.Sin(angle), Mathf.Cos(angle)));
  }

  private void Awake()
  {
    if (UIDrawCall.ClipRange == null)
      UIDrawCall.ClipRange = new int[4]
      {
        Shader.PropertyToID("_ClipRange0"),
        Shader.PropertyToID("_ClipRange1"),
        Shader.PropertyToID("_ClipRange2"),
        Shader.PropertyToID("_ClipRange4")
      };
    if (UIDrawCall.ClipArgs != null)
      return;
    UIDrawCall.ClipArgs = new int[4]
    {
      Shader.PropertyToID("_ClipArgs0"),
      Shader.PropertyToID("_ClipArgs1"),
      Shader.PropertyToID("_ClipArgs2"),
      Shader.PropertyToID("_ClipArgs3")
    };
  }

  private void OnEnable() => this.mRebuildMat = true;

  private void OnDisable()
  {
    this.depthStart = int.MaxValue;
    this.depthEnd = int.MinValue;
    this.panel = (UIPanel) null;
    this.manager = (UIPanel) null;
    this.mMaterial = (Material) null;
    this.mTexture = (Texture) null;
    this.clipTexture = (Texture2D) null;
    if (Object.op_Inequality((Object) this.mRenderer, (Object) null))
      ((Renderer) this.mRenderer).sharedMaterials = new Material[0];
    NGUITools.DestroyImmediate((Object) this.mDynamicMat);
    this.mDynamicMat = (Material) null;
  }

  private void OnDestroy()
  {
    NGUITools.DestroyImmediate((Object) this.mMesh);
    this.mMesh = (Mesh) null;
  }

  public static UIDrawCall Create(UIPanel panel, Material mat, Texture tex, Shader shader)
  {
    return UIDrawCall.Create((string) null, panel, mat, tex, shader);
  }

  private static UIDrawCall Create(
    string name,
    UIPanel pan,
    Material mat,
    Texture tex,
    Shader shader)
  {
    UIDrawCall uiDrawCall = UIDrawCall.Create(name);
    ((Component) uiDrawCall).gameObject.layer = pan.cachedGameObject.layer;
    uiDrawCall.baseMaterial = mat;
    uiDrawCall.mainTexture = tex;
    uiDrawCall.shader = shader;
    uiDrawCall.renderQueue = pan.startingRenderQueue;
    uiDrawCall.sortingOrder = pan.sortingOrder;
    uiDrawCall.manager = pan;
    return uiDrawCall;
  }

  private static UIDrawCall Create(string name)
  {
    if (UIDrawCall.mInactiveList.size > 0)
    {
      UIDrawCall uiDrawCall = UIDrawCall.mInactiveList.Pop();
      UIDrawCall.mActiveList.Add(uiDrawCall);
      if (name != null)
        ((Object) uiDrawCall).name = name;
      NGUITools.SetActive(((Component) uiDrawCall).gameObject, true);
      return uiDrawCall;
    }
    GameObject gameObject = new GameObject(name);
    Object.DontDestroyOnLoad((Object) gameObject);
    UIDrawCall uiDrawCall1 = gameObject.AddComponent<UIDrawCall>();
    UIDrawCall.mActiveList.Add(uiDrawCall1);
    return uiDrawCall1;
  }

  public static void ClearAll()
  {
    bool isPlaying = Application.isPlaying;
    int size = UIDrawCall.mActiveList.size;
    while (size > 0)
    {
      UIDrawCall mActive = UIDrawCall.mActiveList[--size];
      if (Object.op_Implicit((Object) mActive))
      {
        if (isPlaying)
          NGUITools.SetActive(((Component) mActive).gameObject, false);
        else
          NGUITools.DestroyImmediate((Object) ((Component) mActive).gameObject);
      }
    }
    UIDrawCall.mActiveList.Clear();
  }

  public static void ReleaseAll()
  {
    UIDrawCall.ClearAll();
    UIDrawCall.ReleaseInactive();
  }

  public static void ReleaseInactive()
  {
    int size = UIDrawCall.mInactiveList.size;
    while (size > 0)
    {
      UIDrawCall mInactive = UIDrawCall.mInactiveList[--size];
      if (Object.op_Implicit((Object) mInactive))
        NGUITools.DestroyImmediate((Object) ((Component) mInactive).gameObject);
    }
    UIDrawCall.mInactiveList.Clear();
  }

  public static int Count(UIPanel panel)
  {
    int num = 0;
    for (int i = 0; i < UIDrawCall.mActiveList.size; ++i)
    {
      if (Object.op_Equality((Object) UIDrawCall.mActiveList[i].manager, (Object) panel))
        ++num;
    }
    return num;
  }

  public static void Destroy(UIDrawCall dc)
  {
    if (!Object.op_Implicit((Object) dc))
      return;
    dc.onRender = (UIDrawCall.OnRenderCallback) null;
    if (Application.isPlaying)
    {
      if (!UIDrawCall.mActiveList.Remove(dc))
        return;
      NGUITools.SetActive(((Component) dc).gameObject, false);
      UIDrawCall.mInactiveList.Add(dc);
    }
    else
    {
      UIDrawCall.mActiveList.Remove(dc);
      NGUITools.DestroyImmediate((Object) ((Component) dc).gameObject);
    }
  }

  public enum Clipping
  {
    None = 0,
    TextureMask = 1,
    SoftClip = 3,
    ConstrainButDontClip = 4,
  }

  public delegate void OnRenderCallback(Material mat);
}
