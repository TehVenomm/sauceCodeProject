package com.google.android.gms.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ActivityInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.ServiceInfo;
import android.net.Uri;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.Parcelable.Creator;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import com.facebook.appevents.AppEventsConstants;
import com.google.android.gms.common.internal.safeparcel.zzc;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.measurement.AppMeasurement.Event;
import com.google.android.gms.measurement.AppMeasurement.UserProperty;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import io.fabric.sdk.android.services.common.CommonUtils;
import io.fabric.sdk.android.services.events.EventsFilesManager;
import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.ObjectInputStream;
import java.io.ObjectOutputStream;
import java.io.OutputStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.security.cert.CertificateException;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.BitSet;
import java.util.Collections;
import java.util.List;
import java.util.Random;
import java.util.concurrent.atomic.AtomicLong;
import java.util.zip.GZIPInputStream;
import java.util.zip.GZIPOutputStream;
import javax.security.auth.x500.X500Principal;

public final class zzcfo extends zzcdm {
    private static String[] zziwz = new String[]{"firebase_"};
    private SecureRandom zzixa;
    private final AtomicLong zzixb = new AtomicLong(0);
    private int zzixc;

    zzcfo(zzcco zzcco) {
        super(zzcco);
    }

    private final int zza(String str, Object obj, boolean z) {
        if (z) {
            int length;
            Object obj2;
            zzcap.zzavr();
            if (obj instanceof Parcelable[]) {
                length = ((Parcelable[]) obj).length;
            } else if (obj instanceof ArrayList) {
                length = ((ArrayList) obj).size();
            } else {
                length = 1;
                if (obj2 == null) {
                    return 17;
                }
            }
            if (length > 1000) {
                zzauk().zzaye().zzd("Parameter array is too long; discarded. Value kind, name, array length", "param", str, Integer.valueOf(length));
                obj2 = null;
            } else {
                length = 1;
            }
            if (obj2 == null) {
                return 17;
            }
        }
        return zzkd(str) ? zza("param", str, zzcap.zzavq(), obj, z) : zza("param", str, zzcap.zzavp(), obj, z) ? 0 : 4;
    }

    private static Object zza(int i, Object obj, boolean z) {
        if (obj == null) {
            return null;
        }
        if ((obj instanceof Long) || (obj instanceof Double)) {
            return obj;
        }
        if (obj instanceof Integer) {
            return Long.valueOf((long) ((Integer) obj).intValue());
        }
        if (obj instanceof Byte) {
            return Long.valueOf((long) ((Byte) obj).byteValue());
        }
        if (obj instanceof Short) {
            return Long.valueOf((long) ((Short) obj).shortValue());
        }
        if (!(obj instanceof Boolean)) {
            return obj instanceof Float ? Double.valueOf(((Float) obj).doubleValue()) : ((obj instanceof String) || (obj instanceof Character) || (obj instanceof CharSequence)) ? zza(String.valueOf(obj), i, z) : null;
        } else {
            return Long.valueOf(((Boolean) obj).booleanValue() ? 1 : 0);
        }
    }

    public static String zza(String str, int i, boolean z) {
        return str.codePointCount(0, str.length()) > i ? z ? String.valueOf(str.substring(0, str.offsetByCodePoints(0, i))).concat("...") : null : str;
    }

    @Nullable
    public static String zza(String str, String[] strArr, String[] strArr2) {
        zzbp.zzu(strArr);
        zzbp.zzu(strArr2);
        int min = Math.min(strArr.length, strArr2.length);
        for (int i = 0; i < min; i++) {
            if (zzau(str, strArr[i])) {
                return strArr2[i];
            }
        }
        return null;
    }

    public static boolean zza(Context context, String str, boolean z) {
        try {
            PackageManager packageManager = context.getPackageManager();
            if (packageManager == null) {
                return false;
            }
            ActivityInfo receiverInfo = packageManager.getReceiverInfo(new ComponentName(context, str), 2);
            return receiverInfo != null && receiverInfo.enabled;
        } catch (NameNotFoundException e) {
            return false;
        }
    }

    private final boolean zza(String str, String str2, int i, Object obj, boolean z) {
        if (obj == null || (obj instanceof Long) || (obj instanceof Float) || (obj instanceof Integer) || (obj instanceof Byte) || (obj instanceof Short) || (obj instanceof Boolean) || (obj instanceof Double)) {
            return true;
        }
        if ((obj instanceof String) || (obj instanceof Character) || (obj instanceof CharSequence)) {
            String valueOf = String.valueOf(obj);
            if (valueOf.codePointCount(0, valueOf.length()) <= i) {
                return true;
            }
            zzauk().zzaye().zzd("Value is too long; discarded. Value kind, name, value length", str, str2, Integer.valueOf(valueOf.length()));
            return false;
        } else if ((obj instanceof Bundle) && z) {
            return true;
        } else {
            int length;
            int i2;
            Object obj2;
            if ((obj instanceof Parcelable[]) && z) {
                Parcelable[] parcelableArr = (Parcelable[]) obj;
                length = parcelableArr.length;
                i2 = 0;
                while (i2 < length) {
                    obj2 = parcelableArr[i2];
                    if (obj2 instanceof Bundle) {
                        i2++;
                    } else {
                        zzauk().zzaye().zze("All Parcelable[] elements must be of type Bundle. Value type, name", obj2.getClass(), str2);
                        return false;
                    }
                }
                return true;
            } else if (!(obj instanceof ArrayList) || !z) {
                return false;
            } else {
                ArrayList arrayList = (ArrayList) obj;
                length = arrayList.size();
                i2 = 0;
                while (i2 < length) {
                    obj2 = arrayList.get(i2);
                    i2++;
                    if (!(obj2 instanceof Bundle)) {
                        zzauk().zzaye().zze("All ArrayList elements must be of type Bundle. Value type, name", obj2.getClass(), str2);
                        return false;
                    }
                }
                return true;
            }
        }
    }

    private final boolean zza(String str, String[] strArr, String str2) {
        if (str2 == null) {
            zzauk().zzayc().zzj("Name is required and can't be null. Type", str);
            return false;
        }
        boolean z;
        zzbp.zzu(str2);
        for (String startsWith : zziwz) {
            if (str2.startsWith(startsWith)) {
                z = true;
                break;
            }
        }
        z = false;
        if (z) {
            zzauk().zzayc().zze("Name starts with reserved prefix. Type, name", str, str2);
            return false;
        }
        if (strArr != null) {
            zzbp.zzu(strArr);
            for (String startsWith2 : strArr) {
                if (zzau(str2, startsWith2)) {
                    z = true;
                    break;
                }
            }
            z = false;
            if (z) {
                zzauk().zzayc().zze("Name is reserved. Type, name", str, str2);
                return false;
            }
        }
        return true;
    }

    public static boolean zza(long[] jArr, int i) {
        return i < (jArr.length << 6) && (jArr[i / 64] & (1 << (i % 64))) != 0;
    }

    static byte[] zza(Parcelable parcelable) {
        if (parcelable == null) {
            return null;
        }
        Parcel obtain = Parcel.obtain();
        try {
            parcelable.writeToParcel(obtain, 0);
            byte[] marshall = obtain.marshall();
            return marshall;
        } finally {
            obtain.recycle();
        }
    }

    public static long[] zza(BitSet bitSet) {
        int length = (bitSet.length() + 63) / 64;
        long[] jArr = new long[length];
        int i = 0;
        while (i < length) {
            jArr[i] = 0;
            int i2 = 0;
            while (i2 < 64 && (i << 6) + i2 < bitSet.length()) {
                if (bitSet.get((i << 6) + i2)) {
                    jArr[i] = jArr[i] | (1 << i2);
                }
                i2++;
            }
            i++;
        }
        return jArr;
    }

    public static Bundle[] zzac(Object obj) {
        if (obj instanceof Bundle) {
            return new Bundle[]{(Bundle) obj};
        } else if (obj instanceof Parcelable[]) {
            return (Bundle[]) Arrays.copyOf((Parcelable[]) obj, ((Parcelable[]) obj).length, Bundle[].class);
        } else {
            if (!(obj instanceof ArrayList)) {
                return null;
            }
            ArrayList arrayList = (ArrayList) obj;
            return (Bundle[]) arrayList.toArray(new Bundle[arrayList.size()]);
        }
    }

    public static Object zzad(Object obj) {
        ObjectInputStream objectInputStream;
        Throwable th;
        ObjectOutputStream objectOutputStream;
        ObjectInputStream objectInputStream2;
        if (obj == null) {
            return null;
        }
        try {
            OutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            ObjectOutputStream objectOutputStream2 = new ObjectOutputStream(byteArrayOutputStream);
            try {
                objectOutputStream2.writeObject(obj);
                objectOutputStream2.flush();
                objectInputStream = new ObjectInputStream(new ByteArrayInputStream(byteArrayOutputStream.toByteArray()));
            } catch (Throwable th2) {
                th = th2;
                objectOutputStream = objectOutputStream2;
                objectInputStream2 = null;
                if (objectOutputStream != null) {
                    objectOutputStream.close();
                }
                if (objectInputStream2 != null) {
                    objectInputStream2.close();
                }
                throw th;
            }
            try {
                Object readObject = objectInputStream.readObject();
                try {
                    objectOutputStream2.close();
                    objectInputStream.close();
                    return readObject;
                } catch (IOException e) {
                    return null;
                } catch (ClassNotFoundException e2) {
                    return null;
                }
            } catch (Throwable th22) {
                ObjectOutputStream objectOutputStream3 = objectOutputStream2;
                objectInputStream2 = objectInputStream;
                th = th22;
                objectOutputStream = objectOutputStream3;
                if (objectOutputStream != null) {
                    objectOutputStream.close();
                }
                if (objectInputStream2 != null) {
                    objectInputStream2.close();
                }
                throw th;
            }
        } catch (Throwable th222) {
            objectInputStream2 = null;
            th = th222;
            objectOutputStream = null;
            if (objectOutputStream != null) {
                objectOutputStream.close();
            }
            if (objectInputStream2 != null) {
                objectInputStream2.close();
            }
            throw th;
        }
    }

    private final boolean zzaj(Context context, String str) {
        X500Principal x500Principal = new X500Principal("CN=Android Debug,O=Android,C=US");
        try {
            PackageInfo packageInfo = zzbdp.zzcs(context).getPackageInfo(str, 64);
            if (!(packageInfo == null || packageInfo.signatures == null || packageInfo.signatures.length <= 0)) {
                return ((X509Certificate) CertificateFactory.getInstance("X.509").generateCertificate(new ByteArrayInputStream(packageInfo.signatures[0].toByteArray()))).getSubjectX500Principal().equals(x500Principal);
            }
        } catch (CertificateException e) {
            zzauk().zzayc().zzj("Error obtaining certificate", e);
        } catch (NameNotFoundException e2) {
            zzauk().zzayc().zzj("Package name not found", e2);
        }
        return true;
    }

    private final boolean zzas(String str, String str2) {
        if (str2 == null) {
            zzauk().zzayc().zzj("Name is required and can't be null. Type", str);
            return false;
        } else if (str2.length() == 0) {
            zzauk().zzayc().zzj("Name is required and can't be empty. Type", str);
            return false;
        } else {
            int codePointAt = str2.codePointAt(0);
            if (Character.isLetter(codePointAt)) {
                int length = str2.length();
                codePointAt = Character.charCount(codePointAt);
                while (codePointAt < length) {
                    int codePointAt2 = str2.codePointAt(codePointAt);
                    if (codePointAt2 == 95 || Character.isLetterOrDigit(codePointAt2)) {
                        codePointAt += Character.charCount(codePointAt2);
                    } else {
                        zzauk().zzayc().zze("Name must consist of letters, digits or _ (underscores). Type, name", str, str2);
                        return false;
                    }
                }
                return true;
            }
            zzauk().zzayc().zze("Name must start with a letter. Type, name", str, str2);
            return false;
        }
    }

    private final boolean zzat(String str, String str2) {
        if (str2 == null) {
            zzauk().zzayc().zzj("Name is required and can't be null. Type", str);
            return false;
        } else if (str2.length() == 0) {
            zzauk().zzayc().zzj("Name is required and can't be empty. Type", str);
            return false;
        } else {
            int codePointAt = str2.codePointAt(0);
            if (Character.isLetter(codePointAt) || codePointAt == 95) {
                int length = str2.length();
                codePointAt = Character.charCount(codePointAt);
                while (codePointAt < length) {
                    int codePointAt2 = str2.codePointAt(codePointAt);
                    if (codePointAt2 == 95 || Character.isLetterOrDigit(codePointAt2)) {
                        codePointAt += Character.charCount(codePointAt2);
                    } else {
                        zzauk().zzayc().zze("Name must consist of letters, digits or _ (underscores). Type, name", str, str2);
                        return false;
                    }
                }
                return true;
            }
            zzauk().zzayc().zze("Name must start with a letter or _ (underscore). Type, name", str, str2);
            return false;
        }
    }

    public static boolean zzau(String str, String str2) {
        return (str == null && str2 == null) ? true : str == null ? false : str.equals(str2);
    }

    private static void zzb(Bundle bundle, Object obj) {
        zzbp.zzu(bundle);
        if (obj == null) {
            return;
        }
        if ((obj instanceof String) || (obj instanceof CharSequence)) {
            bundle.putLong("_el", (long) String.valueOf(obj).length());
        }
    }

    private final boolean zzb(String str, int i, String str2) {
        if (str2 == null) {
            zzauk().zzayc().zzj("Name is required and can't be null. Type", str);
            return false;
        } else if (str2.codePointCount(0, str2.length()) <= i) {
            return true;
        } else {
            zzauk().zzayc().zzd("Name is too long. Type, maximum supported length, name", str, Integer.valueOf(i), str2);
            return false;
        }
    }

    private static boolean zzd(Bundle bundle, int i) {
        if (bundle.getLong("_err") != 0) {
            return false;
        }
        bundle.putLong("_err", (long) i);
        return true;
    }

    @WorkerThread
    static boolean zzd(zzcbc zzcbc, zzcak zzcak) {
        zzbp.zzu(zzcbc);
        zzbp.zzu(zzcak);
        if (!TextUtils.isEmpty(zzcak.zziln)) {
            return true;
        }
        zzcap.zzawj();
        return false;
    }

    static MessageDigest zzed(String str) {
        int i = 0;
        while (i < 2) {
            try {
                MessageDigest instance = MessageDigest.getInstance(str);
                if (instance != null) {
                    return instance;
                }
                i++;
            } catch (NoSuchAlgorithmException e) {
            }
        }
        return null;
    }

    static boolean zzju(String str) {
        zzbp.zzgf(str);
        return str.charAt(0) != '_' || str.equals("_ep");
    }

    private final int zzjz(String str) {
        return !zzas("event param", str) ? 3 : !zza("event param", null, str) ? 14 : zzb("event param", zzcap.zzavo(), str) ? 0 : 3;
    }

    private final int zzka(String str) {
        return !zzat("event param", str) ? 3 : !zza("event param", null, str) ? 14 : zzb("event param", zzcap.zzavo(), str) ? 0 : 3;
    }

    private static int zzkc(String str) {
        return "_ldl".equals(str) ? zzcap.zzavt() : zzcap.zzavs();
    }

    public static boolean zzkd(String str) {
        return !TextUtils.isEmpty(str) && str.startsWith(EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR);
    }

    static boolean zzkf(String str) {
        return str != null && str.matches("(\\+|-)?([0-9]+\\.?[0-9]*|[0-9]*\\.?[0-9]+)") && str.length() <= 310;
    }

    @WorkerThread
    static boolean zzki(String str) {
        zzbp.zzgf(str);
        boolean z = true;
        switch (str.hashCode()) {
            case 94660:
                if (str.equals("_in")) {
                    z = false;
                    break;
                }
                break;
            case 95025:
                if (str.equals("_ug")) {
                    z = true;
                    break;
                }
                break;
            case 95027:
                if (str.equals("_ui")) {
                    z = true;
                    break;
                }
                break;
        }
        switch (z) {
            case false:
            case true:
            case true:
                return true;
            default:
                return false;
        }
    }

    public static boolean zzl(Intent intent) {
        String stringExtra = intent.getStringExtra("android.intent.extra.REFERRER_NAME");
        return "android-app://com.google.android.googlequicksearchbox/https/www.google.com".equals(stringExtra) || "https://www.google.com".equals(stringExtra) || "android-app://com.google.appcrawler".equals(stringExtra);
    }

    static long zzq(byte[] bArr) {
        zzbp.zzu(bArr);
        zzbp.zzbg(bArr.length > 0);
        int length = bArr.length - 1;
        long j = 0;
        long j2 = 0;
        while (length >= 0 && length >= bArr.length - 8) {
            j2 += (((long) bArr[length]) & 255) << j;
            j += 8;
            length--;
        }
        return j2;
    }

    public static boolean zzw(Context context, String str) {
        try {
            PackageManager packageManager = context.getPackageManager();
            if (packageManager == null) {
                return false;
            }
            ServiceInfo serviceInfo = packageManager.getServiceInfo(new ComponentName(context, str), 4);
            return serviceInfo != null && serviceInfo.enabled;
        } catch (NameNotFoundException e) {
            return false;
        }
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    public final Bundle zza(String str, Bundle bundle, @Nullable List<String> list, boolean z, boolean z2) {
        if (bundle == null) {
            return null;
        }
        Bundle bundle2 = new Bundle(bundle);
        zzcap.zzavl();
        int i = 0;
        for (String str2 : bundle.keySet()) {
            int zzjz;
            if (list == null || !list.contains(str2)) {
                zzjz = z ? zzjz(str2) : 0;
                if (zzjz == 0) {
                    zzjz = zzka(str2);
                }
            } else {
                zzjz = 0;
            }
            if (zzjz != 0) {
                if (zzd(bundle2, zzjz)) {
                    bundle2.putString("_ev", zza(str2, zzcap.zzavo(), true));
                    if (zzjz == 3) {
                        zzb(bundle2, (Object) str2);
                    }
                }
                bundle2.remove(str2);
            } else {
                zzjz = zza(str2, bundle.get(str2), z2);
                if (zzjz == 0 || "_ev".equals(str2)) {
                    if (zzju(str2)) {
                        i++;
                        if (i > 25) {
                            zzauk().zzayc().zze("Event can't contain more then 25 params", zzauf().zzjc(str), zzauf().zzw(bundle));
                            zzd(bundle2, 5);
                            bundle2.remove(str2);
                        }
                    }
                    i = i;
                } else {
                    if (zzd(bundle2, zzjz)) {
                        bundle2.putString("_ev", zza(str2, zzcap.zzavo(), true));
                        zzb(bundle2, bundle.get(str2));
                    }
                    bundle2.remove(str2);
                }
            }
        }
        return bundle2;
    }

    final zzcbc zza(String str, Bundle bundle, String str2, long j, boolean z, boolean z2) {
        if (TextUtils.isEmpty(str)) {
            return null;
        }
        if (zzjw(str) != 0) {
            zzauk().zzayc().zzj("Invalid conditional property event name", zzauf().zzje(str));
            throw new IllegalArgumentException();
        }
        Bundle bundle2 = bundle != null ? new Bundle(bundle) : new Bundle();
        bundle2.putString("_o", str2);
        return new zzcbc(str, new zzcaz(zzx(zza(str, bundle2, Collections.singletonList("_o"), false, false))), str2, j);
    }

    public final void zza(int i, String str, String str2, int i2) {
        zza(null, i, str, str2, i2);
    }

    public final void zza(Bundle bundle, String str, Object obj) {
        if (bundle != null) {
            if (obj instanceof Long) {
                bundle.putLong(str, ((Long) obj).longValue());
            } else if (obj instanceof String) {
                bundle.putString(str, String.valueOf(obj));
            } else if (obj instanceof Double) {
                bundle.putDouble(str, ((Double) obj).doubleValue());
            } else if (str != null) {
                zzauk().zzayf().zze("Not putting event parameter. Invalid value type. name, type", zzauf().zzjd(str), obj != null ? obj.getClass().getSimpleName() : null);
            }
        }
    }

    public final void zza(zzcga zzcga, Object obj) {
        zzbp.zzu(obj);
        zzcga.zzfwi = null;
        zzcga.zziyw = null;
        zzcga.zziwx = null;
        if (obj instanceof String) {
            zzcga.zzfwi = (String) obj;
        } else if (obj instanceof Long) {
            zzcga.zziyw = (Long) obj;
        } else if (obj instanceof Double) {
            zzcga.zziwx = (Double) obj;
        } else {
            zzauk().zzayc().zzj("Ignoring invalid (type) event param value", obj);
        }
    }

    public final void zza(zzcge zzcge, Object obj) {
        zzbp.zzu(obj);
        zzcge.zzfwi = null;
        zzcge.zziyw = null;
        zzcge.zziwx = null;
        if (obj instanceof String) {
            zzcge.zzfwi = (String) obj;
        } else if (obj instanceof Long) {
            zzcge.zziyw = (Long) obj;
        } else if (obj instanceof Double) {
            zzcge.zziwx = (Double) obj;
        } else {
            zzauk().zzayc().zzj("Ignoring invalid (type) user attribute value", obj);
        }
    }

    public final void zza(String str, int i, String str2, String str3, int i2) {
        Bundle bundle = new Bundle();
        zzd(bundle, i);
        if (!TextUtils.isEmpty(str2)) {
            bundle.putString(str2, str3);
        }
        if (i == 6 || i == 7 || i == 2) {
            bundle.putLong("_el", (long) i2);
        }
        zzcap.zzawj();
        this.zzikb.zzaty().zzc("auto", "_err", bundle);
    }

    @WorkerThread
    final long zzai(Context context, String str) {
        zzug();
        zzbp.zzu(context);
        zzbp.zzgf(str);
        PackageManager packageManager = context.getPackageManager();
        MessageDigest zzed = zzed(CommonUtils.MD5_INSTANCE);
        if (zzed == null) {
            zzauk().zzayc().log("Could not get MD5 instance");
            return -1;
        }
        if (packageManager != null) {
            try {
                if (!zzaj(context, str)) {
                    PackageInfo packageInfo = zzbdp.zzcs(context).getPackageInfo(getContext().getPackageName(), 64);
                    if (packageInfo.signatures != null && packageInfo.signatures.length > 0) {
                        return zzq(zzed.digest(packageInfo.signatures[0].toByteArray()));
                    }
                    zzauk().zzaye().log("Could not get signatures");
                    return -1;
                }
            } catch (NameNotFoundException e) {
                zzauk().zzayc().zzj("Package name not found", e);
            }
        }
        return 0;
    }

    public final /* bridge */ /* synthetic */ void zzatt() {
        super.zzatt();
    }

    public final /* bridge */ /* synthetic */ void zzatu() {
        super.zzatu();
    }

    public final /* bridge */ /* synthetic */ void zzatv() {
        super.zzatv();
    }

    public final /* bridge */ /* synthetic */ zzcaf zzatw() {
        return super.zzatw();
    }

    public final /* bridge */ /* synthetic */ zzcam zzatx() {
        return super.zzatx();
    }

    public final /* bridge */ /* synthetic */ zzcdo zzaty() {
        return super.zzaty();
    }

    public final /* bridge */ /* synthetic */ zzcbj zzatz() {
        return super.zzatz();
    }

    public final /* bridge */ /* synthetic */ zzcaw zzaua() {
        return super.zzaua();
    }

    public final /* bridge */ /* synthetic */ zzceg zzaub() {
        return super.zzaub();
    }

    public final /* bridge */ /* synthetic */ zzcec zzauc() {
        return super.zzauc();
    }

    public final /* bridge */ /* synthetic */ zzcbk zzaud() {
        return super.zzaud();
    }

    public final /* bridge */ /* synthetic */ zzcaq zzaue() {
        return super.zzaue();
    }

    public final /* bridge */ /* synthetic */ zzcbm zzauf() {
        return super.zzauf();
    }

    public final /* bridge */ /* synthetic */ zzcfo zzaug() {
        return super.zzaug();
    }

    public final /* bridge */ /* synthetic */ zzcci zzauh() {
        return super.zzauh();
    }

    public final /* bridge */ /* synthetic */ zzcfd zzaui() {
        return super.zzaui();
    }

    public final /* bridge */ /* synthetic */ zzccj zzauj() {
        return super.zzauj();
    }

    public final /* bridge */ /* synthetic */ zzcbo zzauk() {
        return super.zzauk();
    }

    public final /* bridge */ /* synthetic */ zzcbz zzaul() {
        return super.zzaul();
    }

    public final /* bridge */ /* synthetic */ zzcap zzaum() {
        return super.zzaum();
    }

    public final long zzazw() {
        long nextLong;
        if (this.zzixb.get() == 0) {
            synchronized (this.zzixb) {
                nextLong = new Random(System.nanoTime() ^ zzvu().currentTimeMillis()).nextLong();
                int i = this.zzixc + 1;
                this.zzixc = i;
                nextLong += (long) i;
            }
        } else {
            synchronized (this.zzixb) {
                this.zzixb.compareAndSet(-1, 1);
                nextLong = this.zzixb.getAndIncrement();
            }
        }
        return nextLong;
    }

    @WorkerThread
    final SecureRandom zzazx() {
        zzug();
        if (this.zzixa == null) {
            this.zzixa = new SecureRandom();
        }
        return this.zzixa;
    }

    final <T extends Parcelable> T zzb(byte[] bArr, Creator<T> creator) {
        if (bArr == null) {
            return null;
        }
        Parcel obtain = Parcel.obtain();
        T t;
        try {
            obtain.unmarshall(bArr, 0, bArr.length);
            obtain.setDataPosition(0);
            t = (Parcelable) creator.createFromParcel(obtain);
            return t;
        } catch (zzc e) {
            t = zzauk().zzayc();
            t.log("Failed to load parcelable from buffer");
            return null;
        } finally {
            obtain.recycle();
        }
    }

    public final byte[] zzb(zzcgb zzcgb) {
        try {
            byte[] bArr = new byte[zzcgb.zzbjo()];
            zzegg zzi = zzegg.zzi(bArr, 0, bArr.length);
            zzcgb.zza(zzi);
            zzi.zzccd();
            return bArr;
        } catch (IOException e) {
            zzauk().zzayc().zzj("Data loss. Failed to serialize batch", e);
            return null;
        }
    }

    @WorkerThread
    public final boolean zzdu(String str) {
        zzug();
        if (zzbdp.zzcs(getContext()).checkCallingOrSelfPermission(str) == 0) {
            return true;
        }
        zzauk().zzayh().zzj("Permission not granted", str);
        return false;
    }

    public final boolean zzf(long j, long j2) {
        return j == 0 || j2 <= 0 || Math.abs(zzvu().currentTimeMillis() - j) > j2;
    }

    public final int zzjv(String str) {
        return !zzas("event", str) ? 2 : !zza("event", Event.zzikc, str) ? 13 : zzb("event", zzcap.zzavm(), str) ? 0 : 2;
    }

    public final int zzjw(String str) {
        return !zzat("event", str) ? 2 : !zza("event", Event.zzikc, str) ? 13 : zzb("event", zzcap.zzavm(), str) ? 0 : 2;
    }

    public final int zzjx(String str) {
        return !zzas("user property", str) ? 6 : !zza("user property", UserProperty.zzikj, str) ? 15 : zzb("user property", zzcap.zzavn(), str) ? 0 : 6;
    }

    public final int zzjy(String str) {
        return !zzat("user property", str) ? 6 : !zza("user property", UserProperty.zzikj, str) ? 15 : zzb("user property", zzcap.zzavn(), str) ? 0 : 6;
    }

    public final Object zzk(String str, Object obj) {
        if ("_ev".equals(str)) {
            return zza(zzcap.zzavq(), obj, true);
        }
        return zza(zzkd(str) ? zzcap.zzavq() : zzcap.zzavp(), obj, false);
    }

    public final boolean zzkb(String str) {
        if (TextUtils.isEmpty(str)) {
            zzauk().zzayc().log("Missing google_app_id. Firebase Analytics disabled. See https://goo.gl/NAOOOI");
            return false;
        }
        zzbp.zzu(str);
        if (str.matches("^1:\\d+:android:[a-f0-9]+$")) {
            return true;
        }
        zzauk().zzayc().zzj("Invalid google_app_id. Firebase Analytics disabled. See https://goo.gl/NAOOOI. provided id", str);
        return false;
    }

    public final boolean zzke(String str) {
        if (TextUtils.isEmpty(str)) {
            return false;
        }
        String zzaxf = zzaum().zzaxf();
        zzcap.zzawj();
        return zzaxf.equals(str);
    }

    final boolean zzkg(String str) {
        return AppEventsConstants.EVENT_PARAM_VALUE_YES.equals(zzauh().zzap(str, "measurement.upload.blacklist_internal"));
    }

    final boolean zzkh(String str) {
        return AppEventsConstants.EVENT_PARAM_VALUE_YES.equals(zzauh().zzap(str, "measurement.upload.blacklist_public"));
    }

    public final int zzl(String str, Object obj) {
        return "_ldl".equals(str) ? zza("user property referrer", str, zzkc(str), obj, false) : zza("user property", str, zzkc(str), obj, false) ? 0 : 7;
    }

    public final Object zzm(String str, Object obj) {
        return "_ldl".equals(str) ? zza(zzkc(str), obj, true) : zza(zzkc(str), obj, false);
    }

    public final byte[] zzo(byte[] bArr) throws IOException {
        try {
            OutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            GZIPOutputStream gZIPOutputStream = new GZIPOutputStream(byteArrayOutputStream);
            gZIPOutputStream.write(bArr);
            gZIPOutputStream.close();
            byteArrayOutputStream.close();
            return byteArrayOutputStream.toByteArray();
        } catch (IOException e) {
            zzauk().zzayc().zzj("Failed to gzip content", e);
            throw e;
        }
    }

    public final byte[] zzp(byte[] bArr) throws IOException {
        try {
            InputStream byteArrayInputStream = new ByteArrayInputStream(bArr);
            GZIPInputStream gZIPInputStream = new GZIPInputStream(byteArrayInputStream);
            ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
            byte[] bArr2 = new byte[1024];
            while (true) {
                int read = gZIPInputStream.read(bArr2);
                if (read > 0) {
                    byteArrayOutputStream.write(bArr2, 0, read);
                } else {
                    gZIPInputStream.close();
                    byteArrayInputStream.close();
                    return byteArrayOutputStream.toByteArray();
                }
            }
        } catch (IOException e) {
            zzauk().zzayc().zzj("Failed to ungzip content", e);
            throw e;
        }
    }

    public final Bundle zzq(@NonNull Uri uri) {
        Bundle bundle = null;
        if (uri != null) {
            try {
                Object queryParameter;
                Object queryParameter2;
                Object queryParameter3;
                Object queryParameter4;
                if (uri.isHierarchical()) {
                    queryParameter = uri.getQueryParameter("utm_campaign");
                    queryParameter2 = uri.getQueryParameter("utm_source");
                    queryParameter3 = uri.getQueryParameter("utm_medium");
                    queryParameter4 = uri.getQueryParameter("gclid");
                } else {
                    queryParameter = null;
                    queryParameter3 = null;
                    queryParameter4 = null;
                    queryParameter2 = null;
                }
                if (!(TextUtils.isEmpty(queryParameter) && TextUtils.isEmpty(queryParameter2) && TextUtils.isEmpty(queryParameter3) && TextUtils.isEmpty(queryParameter4))) {
                    bundle = new Bundle();
                    if (!TextUtils.isEmpty(queryParameter)) {
                        bundle.putString(Param.CAMPAIGN, queryParameter);
                    }
                    if (!TextUtils.isEmpty(queryParameter2)) {
                        bundle.putString("source", queryParameter2);
                    }
                    if (!TextUtils.isEmpty(queryParameter3)) {
                        bundle.putString(Param.MEDIUM, queryParameter3);
                    }
                    if (!TextUtils.isEmpty(queryParameter4)) {
                        bundle.putString("gclid", queryParameter4);
                    }
                    queryParameter = uri.getQueryParameter("utm_term");
                    if (!TextUtils.isEmpty(queryParameter)) {
                        bundle.putString(Param.TERM, queryParameter);
                    }
                    queryParameter = uri.getQueryParameter("utm_content");
                    if (!TextUtils.isEmpty(queryParameter)) {
                        bundle.putString(Param.CONTENT, queryParameter);
                    }
                    queryParameter = uri.getQueryParameter(Param.ACLID);
                    if (!TextUtils.isEmpty(queryParameter)) {
                        bundle.putString(Param.ACLID, queryParameter);
                    }
                    queryParameter = uri.getQueryParameter(Param.CP1);
                    if (!TextUtils.isEmpty(queryParameter)) {
                        bundle.putString(Param.CP1, queryParameter);
                    }
                    queryParameter = uri.getQueryParameter("anid");
                    if (!TextUtils.isEmpty(queryParameter)) {
                        bundle.putString("anid", queryParameter);
                    }
                }
            } catch (UnsupportedOperationException e) {
                zzauk().zzaye().zzj("Install referrer url isn't a hierarchical URI", e);
            }
        }
        return bundle;
    }

    public final /* bridge */ /* synthetic */ void zzug() {
        super.zzug();
    }

    protected final void zzuh() {
        SecureRandom secureRandom = new SecureRandom();
        long nextLong = secureRandom.nextLong();
        if (nextLong == 0) {
            nextLong = secureRandom.nextLong();
            if (nextLong == 0) {
                zzauk().zzaye().log("Utils falling back to Random for random id");
            }
        }
        this.zzixb.set(nextLong);
    }

    public final /* bridge */ /* synthetic */ zzd zzvu() {
        return super.zzvu();
    }

    final Bundle zzx(Bundle bundle) {
        Bundle bundle2 = new Bundle();
        if (bundle != null) {
            for (String str : bundle.keySet()) {
                Object zzk = zzk(str, bundle.get(str));
                if (zzk == null) {
                    zzauk().zzaye().zzj("Param value can't be null", zzauf().zzjd(str));
                } else {
                    zza(bundle2, str, zzk);
                }
            }
        }
        return bundle2;
    }
}
