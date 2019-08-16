package com.google.firebase.iid;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.support.annotation.GuardedBy;
import android.support.p000v4.content.ContextCompat;
import android.support.p000v4.util.ArrayMap;
import android.util.Log;
import java.io.File;
import java.io.IOException;
import java.util.Map;

final class zzav {
    private final Context zzag;
    private final SharedPreferences zzdc;
    private final zzz zzdd;
    @GuardedBy("this")
    private final Map<String, zzy> zzde;

    public zzav(Context context) {
        this(context, new zzz());
    }

    private zzav(Context context, zzz zzz) {
        this.zzde = new ArrayMap();
        this.zzag = context;
        this.zzdc = context.getSharedPreferences("com.google.android.gms.appid", 0);
        this.zzdd = zzz;
        File file = new File(ContextCompat.getNoBackupFilesDir(this.zzag), "com.google.android.gms.appid-no-backup");
        if (!file.exists()) {
            try {
                if (file.createNewFile() && !isEmpty()) {
                    Log.i("FirebaseInstanceId", "App restored, clearing state");
                    zzaj();
                    FirebaseInstanceId.getInstance().zzn();
                }
            } catch (IOException e) {
                if (Log.isLoggable("FirebaseInstanceId", 3)) {
                    String valueOf = String.valueOf(e.getMessage());
                    Log.d("FirebaseInstanceId", valueOf.length() != 0 ? "Error creating file in no backup dir: ".concat(valueOf) : new String("Error creating file in no backup dir: "));
                }
            }
        }
    }

    private final boolean isEmpty() {
        boolean isEmpty;
        synchronized (this) {
            isEmpty = this.zzdc.getAll().isEmpty();
        }
        return isEmpty;
    }

    private static String zza(String str, String str2, String str3) {
        return new StringBuilder(String.valueOf(str).length() + 4 + String.valueOf(str2).length() + String.valueOf(str3).length()).append(str).append("|T|").append(str2).append("|").append(str3).toString();
    }

    static String zzd(String str, String str2) {
        return new StringBuilder(String.valueOf(str).length() + 3 + String.valueOf(str2).length()).append(str).append("|S|").append(str2).toString();
    }

    public final void zza(String str, String str2, String str3, String str4, String str5) {
        synchronized (this) {
            String zza = zzay.zza(str4, str5, System.currentTimeMillis());
            if (zza != null) {
                Editor edit = this.zzdc.edit();
                edit.putString(zza(str, str2, str3), zza);
                edit.commit();
            }
        }
    }

    public final String zzai() {
        String string;
        synchronized (this) {
            string = this.zzdc.getString("topic_operation_queue", "");
        }
        return string;
    }

    public final void zzaj() {
        synchronized (this) {
            this.zzde.clear();
            zzz.zza(this.zzag);
            this.zzdc.edit().clear().commit();
        }
    }

    public final zzay zzb(String str, String str2, String str3) {
        zzay zzi;
        synchronized (this) {
            zzi = zzay.zzi(this.zzdc.getString(zza(str, str2, str3), null));
        }
        return zzi;
    }

    public final void zzc(String str, String str2, String str3) {
        synchronized (this) {
            String zza = zza(str, str2, str3);
            Editor edit = this.zzdc.edit();
            edit.remove(zza);
            edit.commit();
        }
    }

    public final void zzf(String str) {
        synchronized (this) {
            this.zzdc.edit().putString("topic_operation_queue", str).apply();
        }
    }

    public final zzy zzg(String str) {
        zzy zzy;
        synchronized (this) {
            zzy = (zzy) this.zzde.get(str);
            if (zzy == null) {
                try {
                    zzy = this.zzdd.zzb(this.zzag, str);
                } catch (zzaa e) {
                    Log.w("FirebaseInstanceId", "Stored data is corrupt, generating new identity");
                    FirebaseInstanceId.getInstance().zzn();
                    zzy = this.zzdd.zzc(this.zzag, str);
                }
                this.zzde.put(str, zzy);
            }
        }
        return zzy;
    }

    public final void zzh(String str) {
        synchronized (this) {
            String concat = String.valueOf(str).concat("|T|");
            Editor edit = this.zzdc.edit();
            for (String str2 : this.zzdc.getAll().keySet()) {
                if (str2.startsWith(concat)) {
                    edit.remove(str2);
                }
            }
            edit.commit();
        }
    }
}
