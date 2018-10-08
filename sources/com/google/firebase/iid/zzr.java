package com.google.firebase.iid;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.util.Base64;
import android.util.Log;
import com.google.android.gms.common.util.zzt;
import java.io.File;
import java.io.IOException;
import java.security.KeyFactory;
import java.security.KeyPair;
import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.PKCS8EncodedKeySpec;
import java.security.spec.X509EncodedKeySpec;

final class zzr {
    private Context zzaie;
    SharedPreferences zzhud;

    public zzr(Context context) {
        this(context, "com.google.android.gms.appid");
    }

    private zzr(Context context, String str) {
        this.zzaie = context;
        this.zzhud = context.getSharedPreferences(str, 0);
        String valueOf = String.valueOf(str);
        String valueOf2 = String.valueOf("-no-backup");
        File file = new File(zzt.getNoBackupFilesDir(this.zzaie), valueOf2.length() != 0 ? valueOf.concat(valueOf2) : new String(valueOf));
        if (!file.exists()) {
            try {
                if (file.createNewFile() && !isEmpty()) {
                    Log.i("InstanceID/Store", "App restored, clearing state");
                    FirebaseInstanceId.zza(this.zzaie, this);
                }
            } catch (IOException e) {
                if (Log.isLoggable("InstanceID/Store", 3)) {
                    valueOf2 = String.valueOf(e.getMessage());
                    Log.d("InstanceID/Store", valueOf2.length() != 0 ? "Error creating file in no backup dir: ".concat(valueOf2) : new String("Error creating file in no backup dir: "));
                }
            }
        }
    }

    private static String zzbl(String str, String str2) {
        return new StringBuilder((String.valueOf(str).length() + String.valueOf("|S|").length()) + String.valueOf(str2).length()).append(str).append("|S|").append(str2).toString();
    }

    private final void zzht(String str) {
        Editor edit = this.zzhud.edit();
        for (String str2 : this.zzhud.getAll().keySet()) {
            if (str2.startsWith(str)) {
                edit.remove(str2);
            }
        }
        edit.commit();
    }

    private static String zzn(String str, String str2, String str3) {
        return new StringBuilder((((String.valueOf(str).length() + 1) + String.valueOf("|T|").length()) + String.valueOf(str2).length()) + String.valueOf(str3).length()).append(str).append("|T|").append(str2).append("|").append(str3).toString();
    }

    public final boolean isEmpty() {
        boolean isEmpty;
        synchronized (this) {
            isEmpty = this.zzhud.getAll().isEmpty();
        }
        return isEmpty;
    }

    public final void zza(String str, String str2, String str3, String str4, String str5) {
        synchronized (this) {
            String zzc = zzs.zzc(str4, str5, System.currentTimeMillis());
            if (zzc != null) {
                Editor edit = this.zzhud.edit();
                edit.putString(zzn(str, str2, str3), zzc);
                edit.commit();
            }
        }
    }

    public final void zzasu() {
        synchronized (this) {
            this.zzhud.edit().clear().commit();
        }
    }

    public final void zzf(String str, String str2, String str3) {
        synchronized (this) {
            String zzn = zzn(str, str2, str3);
            Editor edit = this.zzhud.edit();
            edit.remove(zzn);
            edit.commit();
        }
    }

    public final void zzhu(String str) {
        synchronized (this) {
            zzht(String.valueOf(str).concat("|T|"));
        }
    }

    public final zzs zzo(String str, String str2, String str3) {
        zzs zzpz;
        synchronized (this) {
            zzpz = zzs.zzpz(this.zzhud.getString(zzn(str, str2, str3), null));
        }
        return zzpz;
    }

    public final long zzpv(String str) {
        long parseLong;
        synchronized (this) {
            String string = this.zzhud.getString(zzbl(str, "cre"), null);
            if (string != null) {
                try {
                    parseLong = Long.parseLong(string);
                } catch (NumberFormatException e) {
                }
            }
            parseLong = 0;
        }
        return parseLong;
    }

    final KeyPair zzpw(String str) {
        KeyPair zzaso;
        synchronized (this) {
            zzaso = zza.zzaso();
            long currentTimeMillis = System.currentTimeMillis();
            Editor edit = this.zzhud.edit();
            edit.putString(zzbl(str, "|P|"), FirebaseInstanceId.zzm(zzaso.getPublic().getEncoded()));
            edit.putString(zzbl(str, "|K|"), FirebaseInstanceId.zzm(zzaso.getPrivate().getEncoded()));
            edit.putString(zzbl(str, "cre"), Long.toString(currentTimeMillis));
            edit.commit();
        }
        return zzaso;
    }

    final void zzpx(String str) {
        synchronized (this) {
            zzht(String.valueOf(str).concat("|"));
        }
    }

    public final KeyPair zzpy(String str) {
        KeyPair keyPair;
        Object e;
        synchronized (this) {
            String string = this.zzhud.getString(zzbl(str, "|P|"), null);
            String string2 = this.zzhud.getString(zzbl(str, "|K|"), null);
            if (string == null || string2 == null) {
                keyPair = null;
            } else {
                try {
                    byte[] decode = Base64.decode(string, 8);
                    byte[] decode2 = Base64.decode(string2, 8);
                    KeyFactory instance = KeyFactory.getInstance("RSA");
                    keyPair = new KeyPair(instance.generatePublic(new X509EncodedKeySpec(decode)), instance.generatePrivate(new PKCS8EncodedKeySpec(decode2)));
                } catch (InvalidKeySpecException e2) {
                    e = e2;
                    string = String.valueOf(e);
                    Log.w("InstanceID/Store", new StringBuilder(String.valueOf(string).length() + 19).append("Invalid key stored ").append(string).toString());
                    FirebaseInstanceId.zza(this.zzaie, this);
                    keyPair = null;
                    return keyPair;
                } catch (NoSuchAlgorithmException e3) {
                    e = e3;
                    string = String.valueOf(e);
                    Log.w("InstanceID/Store", new StringBuilder(String.valueOf(string).length() + 19).append("Invalid key stored ").append(string).toString());
                    FirebaseInstanceId.zza(this.zzaie, this);
                    keyPair = null;
                    return keyPair;
                }
            }
        }
        return keyPair;
    }
}
