// Decompiled with JetBrains decompiler
// Type: AlmostEngine.Screenshot.AndroidUtils
// Assembly: Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 6956D195-24FE-45FD-BE54-16E1761063F1
// Assembly location: K:\Project\Dragon Project\ReverseEngineering\DumbServer\dragon1.8.9apk_decoded\assets\bin\Data\Managed\Assembly-CSharp.dll

using System;
using System.Collections.Generic;
using UnityEngine;

#nullable disable
namespace AlmostEngine.Screenshot;

public class AndroidUtils
{
  public static string GetExternalPictureDirectory()
  {
    return AndroidUtils.IsPrimaryStorageAvailable() && AndroidUtils.HasPermissionToAccessExternalStorage() ? $"{AndroidUtils.GetPrimaryStorage()}/{AndroidUtils.GetPictureFolder()}" : AndroidUtils.GetFirstAvailableMediaStorage();
  }

  public static bool IsPrimaryStorageAvailable()
  {
    try
    {
      if (((AndroidJavaObject) new AndroidJavaClass("android.os.Environment")).CallStatic<string>("getExternalStorageState", Array.Empty<object>()) == "mounted")
        return true;
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ("AndroidUtils: Error getting external storage state: " + ex.Message));
    }
    return false;
  }

  public static string GetPrimaryStorage()
  {
    try
    {
      return ((AndroidJavaObject) new AndroidJavaClass("android.os.Environment")).CallStatic<AndroidJavaObject>("getExternalStorageDirectory", Array.Empty<object>()).Call<string>("getPath", Array.Empty<object>());
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ("AndroidUtils: Error getting primary external storage directory: " + ex.Message));
    }
    return "";
  }

  public static string GetFirstAvailableMediaStorage()
  {
    List<string> secondaryStorages = AndroidUtils.GetAvailableSecondaryStorages();
    if (secondaryStorages.Count > 0)
      return secondaryStorages[0];
    Debug.LogWarning((object) "No media storage available, using persistentDataPath as fallback");
    return Application.persistentDataPath;
  }

  public static List<string> GetAvailableSecondaryStorages()
  {
    List<string> secondaryStorages = new List<string>();
    try
    {
      AndroidJavaObject androidJavaObject = ((AndroidJavaObject) new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity");
      IntPtr methodId1 = AndroidJNI.GetMethodID(androidJavaObject.GetRawClass(), "getExternalMediaDirs", "()[Ljava/io/File;");
      IntPtr[] numArray = AndroidJNI.FromObjectArray(AndroidJNI.CallObjectMethod(androidJavaObject.GetRawObject(), methodId1, new jvalue[0]));
      for (int index = 0; index < numArray.Length; ++index)
      {
        IntPtr methodId2 = AndroidJNI.GetMethodID(AndroidJNI.GetObjectClass(numArray[index]), "getPath", "()Ljava/lang/String;");
        IntPtr num1 = AndroidJNI.CallObjectMethod(numArray[index], methodId2, new jvalue[0]);
        string stringUtfChars1 = AndroidJNI.GetStringUTFChars(num1);
        AndroidJNI.DeleteLocalRef(num1);
        AndroidJavaClass androidJavaClass = new AndroidJavaClass("android.os.Environment");
        IntPtr staticMethodId = AndroidJNI.GetStaticMethodID(((AndroidJavaObject) androidJavaClass).GetRawClass(), "getExternalStorageState", "(Ljava/io/File;)Ljava/lang/String;");
        jvalue[] jvalueArray = new jvalue[1];
        jvalueArray[0].l = numArray[index];
        IntPtr num2 = AndroidJNI.CallStaticObjectMethod(((AndroidJavaObject) androidJavaClass).GetRawClass(), staticMethodId, jvalueArray);
        string stringUtfChars2 = AndroidJNI.GetStringUTFChars(num2);
        AndroidJNI.DeleteLocalRef(num2);
        if (stringUtfChars2 == "mounted")
          secondaryStorages.Add(stringUtfChars1);
      }
    }
    catch (Exception ex)
    {
      Debug.LogError((object) ("AndroidUtils: Error getting secondary external storage directory: " + ex.Message));
    }
    return secondaryStorages;
  }

  public static string GetPictureFolder() => AndroidUtils.GetDirectoryName();

  public static string GetDirectoryName(string directoryType = "DIRECTORY_PICTURES")
  {
    return ((AndroidJavaObject) new AndroidJavaClass("android.os.Environment")).GetStatic<string>(directoryType);
  }

  public static bool HasPermissionToAccessExternalStorage()
  {
    return ((AndroidJavaObject) new AndroidJavaClass("android.os.Build$VERSION")).GetStatic<int>("SDK_INT") < 23 || AndroidUtils.HasPermission("android.permission.WRITE_EXTERNAL_STORAGE");
  }

  public static bool HasPermission(string permissionName)
  {
    return ((AndroidJavaObject) new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity").Call<int>("checkSelfPermission", new object[1]
    {
      (object) permissionName
    }) == 0;
  }

  public static void AddImageToGallery(string file)
  {
    ((AndroidJavaObject) new AndroidJavaClass("android.media.MediaScannerConnection")).CallStatic("scanFile", new object[4]
    {
      (object) ((AndroidJavaObject) new AndroidJavaClass("com.unity3d.player.UnityPlayer")).GetStatic<AndroidJavaObject>("currentActivity"),
      (object) new string[1]{ file },
      null,
      null
    });
  }
}
