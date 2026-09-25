// Decompiled with JetBrains decompiler
// Type: QuestMapMeshGenerator
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
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
  public string outPath = "";
}
