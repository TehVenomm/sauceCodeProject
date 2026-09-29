// Decompiled with JetBrains decompiler
// Type: Network.PromotionInfo
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;

#nullable disable
namespace Network;

[Serializable]
public class PromotionInfo
{
  public int promotionMaxCnt;
  public int promotionCnt;
  public int promotionReceivedCnt;
  public int promotionBannerId;
  public string newsUrl;
  public bool isPromotionEvent;
}
