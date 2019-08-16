package com.yasirkula.unity;

import android.content.Context;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.ProviderInfo;
import android.util.Log;
import android.webkit.MimeTypeMap;
import com.appsflyer.share.Constants;
import java.io.File;
import java.util.ArrayList;
import java.util.List;
import java.util.Locale;
import java.util.regex.Pattern;

public class NativeShare {
    private static String authority = null;

    public static String FindMatchingTarget(Context context, String str, String str2) {
        List<PackageInfo> installedPackages = context.getPackageManager().getInstalledPackages(1);
        if (installedPackages != null) {
            Pattern compile = Pattern.compile(str);
            Pattern pattern = str2.length() > 0 ? Pattern.compile(str2) : null;
            for (PackageInfo packageInfo : installedPackages) {
                if (compile.matcher(packageInfo.packageName).find()) {
                    ActivityInfo[] activityInfoArr = packageInfo.activities;
                    if (activityInfoArr != null) {
                        for (ActivityInfo activityInfo : activityInfoArr) {
                            if (pattern == null || pattern.matcher(activityInfo.name).find()) {
                                return packageInfo.packageName + ">" + activityInfo.name;
                            }
                        }
                        continue;
                    } else {
                        continue;
                    }
                }
            }
        }
        return "";
    }

    private static String GetAuthority(Context context) {
        if (authority == null) {
            try {
                ProviderInfo[] providerInfoArr = context.getPackageManager().getPackageInfo(context.getPackageName(), 8).providers;
                if (providerInfoArr != null) {
                    int length = providerInfoArr.length;
                    int i = 0;
                    while (true) {
                        if (i >= length) {
                            break;
                        }
                        ProviderInfo providerInfo = providerInfoArr[i];
                        if (providerInfo.name.equals(UnitySSContentProvider.class.getName()) && providerInfo.packageName.equals(context.getPackageName()) && providerInfo.authority.length() > 0) {
                            authority = providerInfo.authority;
                            break;
                        }
                        i++;
                    }
                }
            } catch (Exception e) {
                Log.e("Unity", "Exception:", e);
            }
        }
        return authority;
    }

    public static void Share(Context context, String str, String str2, String[] strArr, String[] strArr2, String str3, String str4, String str5) {
        String str6;
        String mimeTypeFromExtension;
        if (strArr.length <= 0 || GetAuthority(context) != null) {
            Intent intent = new Intent();
            if (str3.length() > 0) {
                intent.putExtra("android.intent.extra.SUBJECT", str3);
            }
            if (str4.length() > 0) {
                intent.putExtra("android.intent.extra.TEXT", str4);
            }
            if (strArr.length > 0) {
                String str7 = null;
                String str8 = null;
                int i = 0;
                while (true) {
                    if (i >= strArr.length) {
                        break;
                    }
                    if (strArr2[i].length() > 0) {
                        mimeTypeFromExtension = strArr2[i];
                    } else {
                        int lastIndexOf = strArr[i].lastIndexOf(46);
                        if (lastIndexOf < 0 || lastIndexOf == strArr.length - 1) {
                            str8 = "*";
                            str7 = "*";
                        } else {
                            mimeTypeFromExtension = MimeTypeMap.getSingleton().getMimeTypeFromExtension(strArr[i].substring(lastIndexOf + 1).toLowerCase(Locale.ENGLISH));
                        }
                    }
                    if (mimeTypeFromExtension == null || mimeTypeFromExtension.length() == 0) {
                        str8 = "*";
                        str7 = "*";
                    } else {
                        int indexOf = mimeTypeFromExtension.indexOf(47);
                        if (indexOf <= 0 || indexOf == mimeTypeFromExtension.length() - 1) {
                            str8 = "*";
                            str7 = "*";
                        } else {
                            String substring = mimeTypeFromExtension.substring(0, indexOf);
                            String substring2 = mimeTypeFromExtension.substring(indexOf + 1);
                            if (str7 != null) {
                                if (!str7.equals(substring)) {
                                    str8 = "*";
                                    str7 = "*";
                                    break;
                                }
                            } else {
                                str7 = substring;
                            }
                            if (str8 == null) {
                                str8 = substring2;
                            } else if (!str8.equals(substring2)) {
                                str8 = "*";
                            }
                            i++;
                        }
                    }
                }
                str8 = "*";
                str7 = "*";
                String str9 = str7 + Constants.URL_PATH_DELIMITER + str8;
                if (strArr.length == 1) {
                    intent.setAction("android.intent.action.SEND");
                    intent.putExtra("android.intent.extra.STREAM", UnitySSContentProvider.getUriForFile(context, authority, new File(strArr[0])));
                    str6 = str9;
                } else {
                    intent.setAction("android.intent.action.SEND_MULTIPLE");
                    ArrayList arrayList = new ArrayList(strArr.length);
                    for (String file : strArr) {
                        arrayList.add(UnitySSContentProvider.getUriForFile(context, authority, new File(file)));
                    }
                    intent.putParcelableArrayListExtra("android.intent.extra.STREAM", arrayList);
                    str6 = str9;
                }
            } else {
                str6 = "text/plain";
                intent.setAction("android.intent.action.SEND");
            }
            if (str5.length() > 0) {
                intent.putExtra("android.intent.extra.TITLE", str5);
            }
            intent.setType(str6);
            intent.setFlags(1);
            if (str.length() > 0) {
                intent.setPackage(str);
                if (str2.length() > 0) {
                    intent.setClassName(str, str2);
                }
            }
            if (context.getPackageManager().queryIntentActivities(intent, 65536).size() == 1) {
                context.startActivity(intent);
            } else {
                context.startActivity(Intent.createChooser(intent, str5));
            }
        } else {
            Log.e("Unity", "Can't find ContentProvider, share not possible!");
        }
    }

    public static boolean TargetExists(Context context, String str, String str2) {
        try {
            if (str2.length() == 0) {
                context.getPackageManager().getPackageInfo(str, 0);
                return true;
            }
            ActivityInfo[] activityInfoArr = context.getPackageManager().getPackageInfo(str, 1).activities;
            if (activityInfoArr != null) {
                for (ActivityInfo activityInfo : activityInfoArr) {
                    if (activityInfo.name.equals(str2)) {
                        return true;
                    }
                }
            }
            return false;
        } catch (NameNotFoundException e) {
            return false;
        }
    }
}
