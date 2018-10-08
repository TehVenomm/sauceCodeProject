package com.google.android.gms.internal;

import android.content.Context;
import android.content.pm.PackageManager.NameNotFoundException;
import android.net.http.AndroidHttpClient;
import android.os.Build.VERSION;
import com.appsflyer.share.Constants;
import java.io.File;

public final class zzas {
    public static zzs zza(Context context, zzan zzan) {
        zzan zzao;
        File file = new File(context.getCacheDir(), "volley");
        String str = "volley/0";
        try {
            String packageName = context.getPackageName();
            str = new StringBuilder(String.valueOf(packageName).length() + 12).append(packageName).append(Constants.URL_PATH_DELIMITER).append(context.getPackageManager().getPackageInfo(packageName, 0).versionCode).toString();
        } catch (NameNotFoundException e) {
        }
        if (VERSION.SDK_INT >= 9) {
            zzao = new zzao();
        } else {
            Object zzak = new zzak(AndroidHttpClient.newInstance(str));
        }
        zzs zzs = new zzs(new zzag(file), new zzad(zzao));
        zzs.start();
        return zzs;
    }
}
