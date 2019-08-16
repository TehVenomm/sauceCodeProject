package com.google.firebase.iid;

import android.content.Context;
import android.content.SharedPreferences;
import android.content.SharedPreferences.Editor;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.support.p000v4.content.ContextCompat;
import android.text.TextUtils;
import android.util.Base64;
import android.util.Log;
import com.google.android.gms.internal.firebase_messaging.zzn;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.io.UnsupportedEncodingException;
import java.nio.channels.Channels;
import java.nio.channels.FileChannel;
import java.security.KeyFactory;
import java.security.KeyPair;
import java.security.NoSuchAlgorithmException;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.PKCS8EncodedKeySpec;
import java.security.spec.X509EncodedKeySpec;
import java.util.Properties;

final class zzz {
    zzz() {
    }

    @Nullable
    private final zzy zza(Context context, String str, zzy zzy, boolean z) {
        Throwable th;
        Throwable th2;
        Throwable th3;
        Throwable th4;
        if (Log.isLoggable("FirebaseInstanceId", 3)) {
            Log.d("FirebaseInstanceId", "Writing key to properties file");
        }
        Properties properties = new Properties();
        properties.setProperty("pub", zzy.zzv());
        properties.setProperty("pri", zzy.zzw());
        properties.setProperty("cre", String.valueOf(zzy.zzbx));
        File zzf = zzf(context, str);
        try {
            zzf.createNewFile();
            RandomAccessFile randomAccessFile = new RandomAccessFile(zzf, "rw");
            try {
                FileChannel channel = randomAccessFile.getChannel();
                try {
                    channel.lock();
                    if (z && channel.size() > 0) {
                        try {
                            channel.position(0);
                            zzy zza = zza(channel);
                            if (channel != null) {
                                zza((Throwable) null, channel);
                            }
                            zza((Throwable) null, randomAccessFile);
                            return zza;
                        } catch (zzaa | IOException e) {
                            if (Log.isLoggable("FirebaseInstanceId", 3)) {
                                String valueOf = String.valueOf(e);
                                Log.d("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf).length() + 64).append("Tried reading key pair before writing new one, but failed with: ").append(valueOf).toString());
                            }
                        }
                    }
                    channel.position(0);
                    properties.store(Channels.newOutputStream(channel), null);
                    if (channel != null) {
                        zza((Throwable) null, channel);
                    }
                    zza((Throwable) null, randomAccessFile);
                    return zzy;
                } catch (Throwable th5) {
                    th4 = th5;
                    th3 = r1;
                }
                zza(th, randomAccessFile);
                throw th2;
                if (channel != null) {
                    zza(th3, channel);
                }
                throw th4;
            } catch (Throwable th6) {
                th2 = th6;
                th = r1;
            }
        } catch (IOException e2) {
            String valueOf2 = String.valueOf(e2);
            Log.w("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf2).length() + 21).append("Failed to write key: ").append(valueOf2).toString());
            return null;
        }
    }

    @Nullable
    private static zzy zza(SharedPreferences sharedPreferences, String str) throws zzaa {
        String string = sharedPreferences.getString(zzav.zzd(str, "|P|"), null);
        String string2 = sharedPreferences.getString(zzav.zzd(str, "|K|"), null);
        if (string == null || string2 == null) {
            return null;
        }
        return new zzy(zzc(string, string2), zzb(sharedPreferences, str));
    }

    private final zzy zza(File file) throws zzaa, IOException {
        Throwable th;
        Throwable th2;
        Throwable th3;
        FileInputStream fileInputStream = new FileInputStream(file);
        try {
            FileChannel channel = fileInputStream.getChannel();
            try {
                channel.lock(0, Long.MAX_VALUE, true);
                zzy zza = zza(channel);
                if (channel != null) {
                    zza((Throwable) null, channel);
                }
                zza((Throwable) null, fileInputStream);
                return zza;
            } catch (Throwable th4) {
                th3 = th4;
                th2 = r0;
            }
            zza(th, fileInputStream);
            throw th;
            if (channel != null) {
                zza(th2, channel);
            }
            throw th3;
        } catch (Throwable th5) {
            th = th5;
        }
    }

    private static zzy zza(FileChannel fileChannel) throws zzaa, IOException {
        Properties properties = new Properties();
        properties.load(Channels.newInputStream(fileChannel));
        String property = properties.getProperty("pub");
        String property2 = properties.getProperty("pri");
        if (property == null || property2 == null) {
            throw new zzaa("Invalid properties file");
        }
        try {
            return new zzy(zzc(property, property2), Long.parseLong(properties.getProperty("cre")));
        } catch (NumberFormatException e) {
            throw new zzaa((Exception) e);
        }
    }

    static void zza(Context context) {
        File[] listFiles;
        for (File file : zzb(context).listFiles()) {
            if (file.getName().startsWith("com.google.InstanceId")) {
                file.delete();
            }
        }
    }

    private final void zza(Context context, String str, zzy zzy) {
        SharedPreferences sharedPreferences = context.getSharedPreferences("com.google.android.gms.appid", 0);
        try {
            if (zzy.equals(zza(sharedPreferences, str))) {
                return;
            }
        } catch (zzaa e) {
        }
        if (Log.isLoggable("FirebaseInstanceId", 3)) {
            Log.d("FirebaseInstanceId", "Writing key to shared preferences");
        }
        Editor edit = sharedPreferences.edit();
        edit.putString(zzav.zzd(str, "|P|"), zzy.zzv());
        edit.putString(zzav.zzd(str, "|K|"), zzy.zzw());
        edit.putString(zzav.zzd(str, "cre"), String.valueOf(zzy.zzbx));
        edit.commit();
    }

    private static /* synthetic */ void zza(Throwable th, FileInputStream fileInputStream) {
        if (th != null) {
            try {
                fileInputStream.close();
            } catch (Throwable th2) {
                zzn.zza(th, th2);
            }
        } else {
            fileInputStream.close();
        }
    }

    private static /* synthetic */ void zza(Throwable th, RandomAccessFile randomAccessFile) {
        if (th != null) {
            try {
                randomAccessFile.close();
            } catch (Throwable th2) {
                zzn.zza(th, th2);
            }
        } else {
            randomAccessFile.close();
        }
    }

    private static /* synthetic */ void zza(Throwable th, FileChannel fileChannel) {
        if (th != null) {
            try {
                fileChannel.close();
            } catch (Throwable th2) {
                zzn.zza(th, th2);
            }
        } else {
            fileChannel.close();
        }
    }

    private static long zzb(SharedPreferences sharedPreferences, String str) {
        String string = sharedPreferences.getString(zzav.zzd(str, "cre"), null);
        if (string != null) {
            try {
                return Long.parseLong(string);
            } catch (NumberFormatException e) {
            }
        }
        return 0;
    }

    private static File zzb(Context context) {
        File noBackupFilesDir = ContextCompat.getNoBackupFilesDir(context);
        if (noBackupFilesDir != null && noBackupFilesDir.isDirectory()) {
            return noBackupFilesDir;
        }
        Log.w("FirebaseInstanceId", "noBackupFilesDir doesn't exist, using regular files directory instead");
        return context.getFilesDir();
    }

    private static KeyPair zzc(String str, String str2) throws zzaa {
        try {
            byte[] decode = Base64.decode(str, 8);
            byte[] decode2 = Base64.decode(str2, 8);
            try {
                KeyFactory instance = KeyFactory.getInstance("RSA");
                return new KeyPair(instance.generatePublic(new X509EncodedKeySpec(decode)), instance.generatePrivate(new PKCS8EncodedKeySpec(decode2)));
            } catch (NoSuchAlgorithmException | InvalidKeySpecException e) {
                String valueOf = String.valueOf(e);
                Log.w("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf).length() + 19).append("Invalid key stored ").append(valueOf).toString());
                throw new zzaa((Exception) e);
            }
        } catch (IllegalArgumentException e2) {
            throw new zzaa((Exception) e2);
        }
    }

    @Nullable
    private final zzy zzd(Context context, String str) throws zzaa {
        zzaa zzaa;
        try {
            zzy zze = zze(context, str);
            if (zze != null) {
                zza(context, str, zze);
                return zze;
            }
            zzaa = null;
            try {
                zzy zza = zza(context.getSharedPreferences("com.google.android.gms.appid", 0), str);
                if (zza != null) {
                    zza(context, str, zza, false);
                    return zza;
                }
                e = zzaa;
                if (e == null) {
                    return null;
                }
                throw e;
            } catch (zzaa e) {
                e = e;
            }
        } catch (zzaa e2) {
            zzaa = e2;
        }
    }

    @Nullable
    private final zzy zze(Context context, String str) throws zzaa {
        File zzf = zzf(context, str);
        if (!zzf.exists()) {
            return null;
        }
        try {
            return zza(zzf);
        } catch (zzaa | IOException e) {
            if (Log.isLoggable("FirebaseInstanceId", 3)) {
                String valueOf = String.valueOf(e);
                Log.d("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf).length() + 40).append("Failed to read key from file, retrying: ").append(valueOf).toString());
            }
            try {
                return zza(zzf);
            } catch (IOException e2) {
                String valueOf2 = String.valueOf(e2);
                Log.w("FirebaseInstanceId", new StringBuilder(String.valueOf(valueOf2).length() + 45).append("IID file exists, but failed to read from it: ").append(valueOf2).toString());
                throw new zzaa((Exception) e2);
            }
        }
    }

    private static File zzf(Context context, String str) {
        String sb;
        if (TextUtils.isEmpty(str)) {
            sb = "com.google.InstanceId.properties";
        } else {
            try {
                String encodeToString = Base64.encodeToString(str.getBytes("UTF-8"), 11);
                sb = new StringBuilder(String.valueOf(encodeToString).length() + 33).append("com.google.InstanceId_").append(encodeToString).append(".properties").toString();
            } catch (UnsupportedEncodingException e) {
                throw new AssertionError(e);
            }
        }
        return new File(zzb(context), sb);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final zzy zzb(Context context, String str) throws zzaa {
        zzy zzd = zzd(context, str);
        return zzd != null ? zzd : zzc(context, str);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final zzy zzc(Context context, String str) {
        zzy zzy = new zzy(zza.zzc(), System.currentTimeMillis());
        zzy zza = zza(context, str, zzy, true);
        if (zza == null || zza.equals(zzy)) {
            if (Log.isLoggable("FirebaseInstanceId", 3)) {
                Log.d("FirebaseInstanceId", "Generated new key");
            }
            zza(context, str, zzy);
            return zzy;
        } else if (!Log.isLoggable("FirebaseInstanceId", 3)) {
            return zza;
        } else {
            Log.d("FirebaseInstanceId", "Loaded key after generating new one, using loaded one");
            return zza;
        }
    }
}
