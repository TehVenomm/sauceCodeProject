// Decompiled with JetBrains decompiler
// Type: gogame.GoWrap_Android
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace gogame;

public class GoWrap_Android : IGoWrap
{
  private IGoWrapDelegate goWrapDelegate;

  public void initGoWrap(string objName)
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.runOnUiThread(GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__1_0 ?? (GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__1_0 = new AndroidJavaRunnable((object) GoWrap_Android.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CinitGoWrap\u003Eb__1_0))));
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.unity.GoWrapUnityPlugin"))
      ((AndroidJavaObject) androidJavaClass).CallStatic("initialize", new object[1]
      {
        (object) objName
      });
  }

  public IGoWrapDelegate getDelegate() => this.goWrapDelegate;

  public void setDelegate(IGoWrapDelegate goWrapDelegate) => this.goWrapDelegate = goWrapDelegate;

  public void setGuid(string guid)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
      ((AndroidJavaObject) androidJavaClass).CallStatic(nameof (setGuid), new object[1]
      {
        (object) guid
      });
  }

  public void setVipStatus(VipStatus vipStatus)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
    {
      if (vipStatus != null)
      {
        using (AndroidJavaObject androidJavaObject = new AndroidJavaObject("net.gogame.gowrap.sdk.VipStatus", Array.Empty<object>()))
        {
          androidJavaObject.Call("setVip", new object[1]
          {
            (object) vipStatus.vip
          });
          androidJavaObject.Call("setSuspended", new object[1]
          {
            (object) vipStatus.suspended
          });
          androidJavaObject.Call("setSuspensionMessage", new object[1]
          {
            (object) vipStatus.suspensionMessage
          });
          ((AndroidJavaObject) androidJavaClass).CallStatic(nameof (setVipStatus), new object[1]
          {
            (object) androidJavaObject
          });
        }
      }
      else
        ((AndroidJavaObject) androidJavaClass).CallStatic(nameof (setVipStatus), (object[]) null);
    }
  }

  public bool hasOffers()
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
      return ((AndroidJavaObject) androidJavaClass).CallStatic<bool>(nameof (hasOffers), Array.Empty<object>());
  }

  public void showOffers()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.runOnUiThread(GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__7_0 ?? (GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__7_0 = new AndroidJavaRunnable((object) GoWrap_Android.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CshowOffers\u003Eb__7_0))));
  }

  public bool hasBannerAds()
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
      return ((AndroidJavaObject) androidJavaClass).CallStatic<bool>(nameof (hasBannerAds), Array.Empty<object>());
  }

  public bool hasBannerAds(BannerAdSize size)
  {
    using (AndroidJavaClass androidJavaClass1 = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
    {
      using (AndroidJavaClass androidJavaClass2 = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap$BannerAdSize"))
      {
        AndroidJavaObject androidJavaObject = ((AndroidJavaObject) androidJavaClass2).GetStatic<AndroidJavaObject>(size.ToString());
        return ((AndroidJavaObject) androidJavaClass1).CallStatic<bool>(nameof (hasBannerAds), new object[1]
        {
          (object) androidJavaObject
        });
      }
    }
  }

  public void showBannerAd()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.runOnUiThread(GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__10_0 ?? (GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__10_0 = new AndroidJavaRunnable((object) GoWrap_Android.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CshowBannerAd\u003Eb__10_0))));
  }

  public void showBannerAd(BannerAdSize size)
  {
    // ISSUE: object of a compiler-generated type is created
    // ISSUE: method pointer
    this.runOnUiThread(new AndroidJavaRunnable((object) new GoWrap_Android.\u003C\u003Ec__DisplayClass11_0()
    {
      size = size
    }, __methodptr(\u003CshowBannerAd\u003Eb__0)));
  }

  public void hideBannerAd()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.runOnUiThread(GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__12_0 ?? (GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__12_0 = new AndroidJavaRunnable((object) GoWrap_Android.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003ChideBannerAd\u003Eb__12_0))));
  }

  public bool hasInterstitialAds()
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
      return ((AndroidJavaObject) androidJavaClass).CallStatic<bool>(nameof (hasInterstitialAds), Array.Empty<object>());
  }

  public void showInterstitialAd()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.runOnUiThread(GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__14_0 ?? (GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__14_0 = new AndroidJavaRunnable((object) GoWrap_Android.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CshowInterstitialAd\u003Eb__14_0))));
  }

  public bool hasRewardedAds()
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
      return ((AndroidJavaObject) androidJavaClass).CallStatic<bool>(nameof (hasRewardedAds), Array.Empty<object>());
  }

  public void showRewardedAd()
  {
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: reference to a compiler-generated field
    // ISSUE: method pointer
    this.runOnUiThread(GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__16_0 ?? (GoWrap_Android.\u003C\u003Ec.\u003C\u003E9__16_0 = new AndroidJavaRunnable((object) GoWrap_Android.\u003C\u003Ec.\u003C\u003E9, __methodptr(\u003CshowRewardedAd\u003Eb__16_0))));
  }

  public void showMenu()
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
      ((AndroidJavaObject) androidJavaClass).CallStatic(nameof (showMenu), Array.Empty<object>());
  }

  public void trackEvent(string name, string category)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
      ((AndroidJavaObject) androidJavaClass).CallStatic(nameof (trackEvent), new object[2]
      {
        (object) category,
        (object) name
      });
  }

  public void trackEvent(string name, string category, long value)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
      ((AndroidJavaObject) androidJavaClass).CallStatic(nameof (trackEvent), new object[3]
      {
        (object) category,
        (object) name,
        (object) value
      });
  }

  public void trackEvent(string name, string category, Dictionary<string, object> values)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
    {
      using (AndroidJavaObject javaMap = this.toJavaMap(values))
      {
        IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(((AndroidJavaObject) androidJavaClass).GetRawClass(), nameof (trackEvent), "(Ljava/lang/String;Ljava/lang/String;Ljava/util/Map;)V");
        object[] objArray = new object[3];
        AndroidJavaObject androidJavaObject1 = this.toAndroidJavaObject((object) category);
        AndroidJavaObject androidJavaObject2 = this.toAndroidJavaObject((object) name);
        objArray[0] = (object) androidJavaObject1;
        objArray[1] = (object) androidJavaObject2;
        objArray[2] = (object) javaMap;
        try
        {
          AndroidJNI.CallStaticVoidMethod(((AndroidJavaObject) androidJavaClass).GetRawClass(), staticMethodId, AndroidJNIHelper.CreateJNIArgArray(objArray));
        }
        finally
        {
          androidJavaObject1?.Dispose();
          androidJavaObject2?.Dispose();
        }
      }
    }
  }

  public void trackPurchase(
    string productId,
    string currencyCode,
    double price,
    string purchaseData,
    string signature)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
      ((AndroidJavaObject) androidJavaClass).CallStatic(nameof (trackPurchase), new object[5]
      {
        (object) productId,
        (object) currencyCode,
        (object) price,
        (object) purchaseData,
        (object) signature
      });
  }

  public void setCustomUrlSchemes(List<string> schemes)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
    {
      AndroidJavaObject androidJavaObject = new AndroidJavaObject("java.util.ArrayList", Array.Empty<object>());
      IntPtr methodId = AndroidJNIHelper.GetMethodID(androidJavaObject.GetRawClass(), "add", "(Ljava/lang/Object;)Z");
      object[] objArray = new object[1];
      foreach (string scheme in schemes)
      {
        objArray[0] = (object) scheme;
        AndroidJNI.CallBooleanMethod(androidJavaObject.GetRawObject(), methodId, AndroidJNIHelper.CreateJNIArgArray(objArray));
      }
      ((AndroidJavaObject) androidJavaClass).CallStatic(nameof (setCustomUrlSchemes), new object[1]
      {
        (object) androidJavaObject
      });
    }
  }

  public void setIOSDeviceToken(byte[] deviceToken)
  {
  }

  private void runOnUiThread(AndroidJavaRunnable runnable)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
    {
      using (AndroidJavaObject androidJavaObject = ((AndroidJavaObject) androidJavaClass).GetStatic<AndroidJavaObject>("currentActivity"))
        androidJavaObject.Call(nameof (runOnUiThread), new object[1]
        {
          (object) runnable
        });
    }
  }

  private AndroidJavaObject toAndroidJavaObject(object value)
  {
    switch (value)
    {
      case null:
        return (AndroidJavaObject) null;
      case bool _:
        return new AndroidJavaObject("java.lang.Boolean", new object[1]
        {
          value
        });
      case Decimal num1:
        return new AndroidJavaObject("java.lang.Double", new object[1]
        {
          (object) (double) num1
        });
      case double _:
        return new AndroidJavaObject("java.lang.Double", new object[1]
        {
          value
        });
      case float _:
        return new AndroidJavaObject("java.lang.Float", new object[1]
        {
          value
        });
      case int _:
        return new AndroidJavaObject("java.lang.Integer", new object[1]
        {
          value
        });
      case uint num2:
        return new AndroidJavaObject("java.lang.Long", new object[1]
        {
          (object) (long) num2
        });
      case long _:
        return new AndroidJavaObject("java.lang.Long", new object[1]
        {
          value
        });
      case ulong num3:
        return new AndroidJavaObject("java.lang.Long", new object[1]
        {
          (object) (long) num3
        });
      case short _:
        return new AndroidJavaObject("java.lang.Short", new object[1]
        {
          value
        });
      case ushort num4:
        return new AndroidJavaObject("java.lang.Integer", new object[1]
        {
          (object) (int) num4
        });
      case byte num5:
        return new AndroidJavaObject("java.lang.Short", new object[1]
        {
          (object) (short) num5
        });
      case sbyte num6:
        return new AndroidJavaObject("java.lang.Short", new object[1]
        {
          (object) (short) num6
        });
      case string _:
        return new AndroidJavaObject("java.lang.String", new object[1]
        {
          value
        });
      default:
        return new AndroidJavaObject("java.lang.String", new object[1]
        {
          (object) value.ToString()
        });
    }
  }

  private AndroidJavaObject toJavaMap(Dictionary<string, object> values)
  {
    AndroidJavaObject javaMap = new AndroidJavaObject("java.util.HashMap", Array.Empty<object>());
    IntPtr methodId = AndroidJNIHelper.GetMethodID(javaMap.GetRawClass(), "put", "(Ljava/lang/Object;Ljava/lang/Object;)Ljava/lang/Object;");
    object[] objArray = new object[2];
    foreach (KeyValuePair<string, object> keyValuePair in values)
    {
      using (AndroidJavaObject androidJavaObject1 = new AndroidJavaObject("java.lang.String", new object[1]
      {
        (object) keyValuePair.Key
      }))
      {
        AndroidJavaObject androidJavaObject2 = this.toAndroidJavaObject(keyValuePair.Value);
        if (androidJavaObject2 != null)
        {
          using (androidJavaObject2)
          {
            objArray[0] = (object) androidJavaObject1;
            objArray[1] = (object) androidJavaObject2;
            AndroidJNI.CallObjectMethod(javaMap.GetRawObject(), methodId, AndroidJNIHelper.CreateJNIArgArray(objArray));
          }
        }
      }
    }
    return javaMap;
  }

  public void trackPurchase(string productId, string currencyCode, double price)
  {
    using (AndroidJavaClass androidJavaClass = new AndroidJavaClass("net.gogame.gowrap.sdk.GoWrap"))
      ((AndroidJavaObject) androidJavaClass).CallStatic(nameof (trackPurchase), new object[3]
      {
        (object) productId,
        (object) currencyCode,
        (object) price
      });
  }
}
