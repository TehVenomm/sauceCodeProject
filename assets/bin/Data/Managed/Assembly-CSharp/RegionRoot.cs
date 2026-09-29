// Decompiled with JetBrains decompiler
// Type: RegionRoot
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using UnityEngine;

#nullable disable
public class RegionRoot : MonoBehaviour
{
  [Tooltip("部位ID")]
  public int regionID = 1;
  [Tooltip("サブ部位ID（ターゲット時しかヒットしない")]
  public int[] subRegionIDs;
  [Tooltip("RegionRootとして登録した後に非表示にする")]
  public bool isDeactive;

  public int[] regionIDArray { get; protected set; }

  private void Awake()
  {
    if (this.subRegionIDs == null)
    {
      this.regionIDArray = new int[1];
      this.regionIDArray[0] = this.regionID;
    }
    else
    {
      this.regionIDArray = new int[this.subRegionIDs.Length + 1];
      this.regionIDArray[0] = this.regionID;
      int index = 0;
      for (int length = this.subRegionIDs.Length; index < length; ++index)
        this.regionIDArray[index + 1] = this.subRegionIDs[index];
    }
  }
}
