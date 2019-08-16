package com.google.android.gms.measurement.internal;

import android.content.ComponentName;
import android.content.Context;
import android.content.Intent;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.content.pm.ServiceInfo;
import android.net.Uri;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Parcel;
import android.os.Parcelable;
import android.os.RemoteException;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.annotation.WorkerThread;
import android.support.p000v4.app.NotificationCompat;
import android.text.TextUtils;
import com.google.android.gms.common.GoogleApiAvailabilityLight;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.Clock;
import com.google.android.gms.common.util.CollectionUtils;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.common.wrappers.Wrappers;
import com.google.android.gms.internal.measurement.zzp;
import com.google.android.gms.measurement.api.AppMeasurementSdk.ConditionalUserProperty;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import java.io.ByteArrayInputStream;
import java.math.BigInteger;
import java.net.MalformedURLException;
import java.net.URL;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.security.SecureRandom;
import java.security.cert.CertificateException;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;
import java.util.Locale;
import java.util.Random;
import java.util.concurrent.atomic.AtomicLong;
import javax.security.auth.x500.X500Principal;
import org.apache.commons.lang3.time.DateUtils;
import p017io.fabric.sdk.android.services.events.EventsFilesManager;

public final class zzjs extends zzge {
    private static final String[] zztw = {"firebase_", "google_", "ga_"};
    private static final List<String> zzua = Collections.unmodifiableList(Arrays.asList(new String[]{"source", Param.MEDIUM, Param.CAMPAIGN, Param.TERM, Param.CONTENT}));
    private int zzag;
    private SecureRandom zztx;
    private final AtomicLong zzty = new AtomicLong(0);
    private Integer zztz = null;

    zzjs(zzfj zzfj) {
        super(zzfj);
    }

    static MessageDigest getMessageDigest() {
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= 2) {
                return null;
            }
            try {
                MessageDigest instance = MessageDigest.getInstance("MD5");
                if (instance != null) {
                    return instance;
                }
                i = i2 + 1;
            } catch (NoSuchAlgorithmException e) {
            }
        }
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
        if (obj instanceof Boolean) {
            return Long.valueOf(((Boolean) obj).booleanValue() ? 1 : 0);
        } else if (obj instanceof Float) {
            return Double.valueOf(((Float) obj).doubleValue());
        } else {
            if ((obj instanceof String) || (obj instanceof Character) || (obj instanceof CharSequence)) {
                return zza(String.valueOf(obj), i, z);
            }
            return null;
        }
    }

    public static String zza(String str, int i, boolean z) {
        if (str == null) {
            return null;
        }
        if (str.codePointCount(0, str.length()) <= i) {
            return str;
        }
        if (z) {
            return String.valueOf(str.substring(0, str.offsetByCodePoints(0, i))).concat("...");
        }
        return null;
    }

    private static boolean zza(Bundle bundle, int i) {
        if (bundle.getLong("_err") != 0) {
            return false;
        }
        bundle.putLong("_err", (long) i);
        return true;
    }

    static boolean zza(Boolean bool, Boolean bool2) {
        if (bool == null && bool2 == null) {
            return true;
        }
        if (bool == null) {
            return false;
        }
        return bool.equals(bool2);
    }

    private final boolean zza(String str, String str2, int i, Object obj, boolean z) {
        Parcelable[] parcelableArr;
        if (obj == null || (obj instanceof Long) || (obj instanceof Float) || (obj instanceof Integer) || (obj instanceof Byte) || (obj instanceof Short) || (obj instanceof Boolean) || (obj instanceof Double)) {
            return true;
        }
        if ((obj instanceof String) || (obj instanceof Character) || (obj instanceof CharSequence)) {
            String valueOf = String.valueOf(obj);
            if (valueOf.codePointCount(0, valueOf.length()) <= i) {
                return true;
            }
            zzab().zzgp().zza("Value is too long; discarded. Value kind, name, value length", str, str2, Integer.valueOf(valueOf.length()));
            return false;
        } else if ((obj instanceof Bundle) && z) {
            return true;
        } else {
            if ((obj instanceof Parcelable[]) && z) {
                for (Parcelable parcelable : (Parcelable[]) obj) {
                    if (!(parcelable instanceof Bundle)) {
                        zzab().zzgp().zza("All Parcelable[] elements must be of type Bundle. Value type, name", parcelable.getClass(), str2);
                        return false;
                    }
                }
                return true;
            } else if (!(obj instanceof ArrayList) || !z) {
                return false;
            } else {
                ArrayList arrayList = (ArrayList) obj;
                int size = arrayList.size();
                int i2 = 0;
                while (i2 < size) {
                    Object obj2 = arrayList.get(i2);
                    i2++;
                    if (!(obj2 instanceof Bundle)) {
                        zzab().zzgp().zza("All ArrayList elements must be of type Bundle. Value type, name", obj2.getClass(), str2);
                        return false;
                    }
                }
                return true;
            }
        }
    }

    static boolean zza(String str, String str2, String str3, String str4) {
        boolean isEmpty = TextUtils.isEmpty(str);
        boolean isEmpty2 = TextUtils.isEmpty(str2);
        if (!isEmpty && !isEmpty2) {
            return !str.equals(str2);
        }
        if (isEmpty && isEmpty2) {
            return (TextUtils.isEmpty(str3) || TextUtils.isEmpty(str4)) ? !TextUtils.isEmpty(str4) : !str3.equals(str4);
        }
        if (isEmpty || !isEmpty2) {
            return TextUtils.isEmpty(str3) || !str3.equals(str4);
        }
        if (TextUtils.isEmpty(str4)) {
            return false;
        }
        return TextUtils.isEmpty(str3) || !str3.equals(str4);
    }

    static byte[] zza(Parcelable parcelable) {
        if (parcelable == null) {
            return null;
        }
        Parcel obtain = Parcel.obtain();
        try {
            parcelable.writeToParcel(obtain, 0);
            return obtain.marshall();
        } finally {
            obtain.recycle();
        }
    }

    private static void zzb(Bundle bundle, Object obj) {
        Preconditions.checkNotNull(bundle);
        if (obj == null) {
            return;
        }
        if ((obj instanceof String) || (obj instanceof CharSequence)) {
            bundle.putLong("_el", (long) String.valueOf(obj).length());
        }
    }

    private static boolean zzb(Context context, String str) {
        try {
            PackageManager packageManager = context.getPackageManager();
            if (packageManager == null) {
                return false;
            }
            ServiceInfo serviceInfo = packageManager.getServiceInfo(new ComponentName(context, str), 0);
            return serviceInfo != null && serviceInfo.enabled;
        } catch (NameNotFoundException e) {
            return false;
        }
    }

    static boolean zzb(Context context, boolean z) {
        Preconditions.checkNotNull(context);
        return VERSION.SDK_INT >= 24 ? zzb(context, "com.google.android.gms.measurement.AppMeasurementJobService") : zzb(context, "com.google.android.gms.measurement.AppMeasurementService");
    }

    static boolean zzb(@Nullable List<String> list, @Nullable List<String> list2) {
        if (list == null && list2 == null) {
            return true;
        }
        if (list == null) {
            return false;
        }
        return list.equals(list2);
    }

    static Bundle[] zzb(Object obj) {
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

    static boolean zzbk(String str) {
        Preconditions.checkNotEmpty(str);
        return str.charAt(0) != '_' || str.equals("_ep");
    }

    @VisibleForTesting
    private static boolean zzbn(String str) {
        Preconditions.checkNotNull(str);
        return str.matches("^(1:\\d+:android:[a-f0-9]+|ca-app-pub-.*)$");
    }

    private static int zzbo(String str) {
        if ("_ldl".equals(str)) {
            return 2048;
        }
        return "_id".equals(str) ? 256 : 36;
    }

    static boolean zzbq(String str) {
        return !TextUtils.isEmpty(str) && str.startsWith(EventsFilesManager.ROLL_OVER_FILE_NAME_SEPARATOR);
    }

    public static long zzc(long j, long j2) {
        return ((60000 * j2) + j) / DateUtils.MILLIS_PER_DAY;
    }

    public static Bundle zzc(List<zzjn> list) {
        Bundle bundle = new Bundle();
        if (list != null) {
            for (zzjn zzjn : list) {
                if (zzjn.zzkr != null) {
                    bundle.putString(zzjn.name, zzjn.zzkr);
                } else if (zzjn.zzts != null) {
                    bundle.putLong(zzjn.name, zzjn.zzts.longValue());
                } else if (zzjn.zztu != null) {
                    bundle.putDouble(zzjn.name, zzjn.zztu.doubleValue());
                }
            }
        }
        return bundle;
    }

    static boolean zzc(Intent intent) {
        String stringExtra = intent.getStringExtra("android.intent.extra.REFERRER_NAME");
        return "android-app://com.google.android.googlequicksearchbox/https/www.google.com".equals(stringExtra) || "https://www.google.com".equals(stringExtra) || "android-app://com.google.appcrawler".equals(stringExtra);
    }

    @VisibleForTesting
    static long zzd(byte[] bArr) {
        Preconditions.checkNotNull(bArr);
        Preconditions.checkState(bArr.length > 0);
        long j = 0;
        int length = bArr.length - 1;
        int i = 0;
        while (length >= 0 && length >= bArr.length - 8) {
            i += 8;
            length--;
            j = ((((long) bArr[length]) & 255) << i) + j;
        }
        return j;
    }

    public static ArrayList<Bundle> zzd(List<zzq> list) {
        if (list == null) {
            return new ArrayList<>(0);
        }
        ArrayList arrayList = new ArrayList(list.size());
        for (zzq zzq : list) {
            Bundle bundle = new Bundle();
            bundle.putString("app_id", zzq.packageName);
            bundle.putString("origin", zzq.origin);
            bundle.putLong(ConditionalUserProperty.CREATION_TIMESTAMP, zzq.creationTimestamp);
            bundle.putString("name", zzq.zzdw.name);
            zzgg.zza(bundle, zzq.zzdw.getValue());
            bundle.putBoolean(ConditionalUserProperty.ACTIVE, zzq.active);
            if (zzq.triggerEventName != null) {
                bundle.putString(ConditionalUserProperty.TRIGGER_EVENT_NAME, zzq.triggerEventName);
            }
            if (zzq.zzdx != null) {
                bundle.putString(ConditionalUserProperty.TIMED_OUT_EVENT_NAME, zzq.zzdx.name);
                if (zzq.zzdx.zzfq != null) {
                    bundle.putBundle(ConditionalUserProperty.TIMED_OUT_EVENT_PARAMS, zzq.zzdx.zzfq.zzcv());
                }
            }
            bundle.putLong(ConditionalUserProperty.TRIGGER_TIMEOUT, zzq.triggerTimeout);
            if (zzq.zzdy != null) {
                bundle.putString(ConditionalUserProperty.TRIGGERED_EVENT_NAME, zzq.zzdy.name);
                if (zzq.zzdy.zzfq != null) {
                    bundle.putBundle(ConditionalUserProperty.TRIGGERED_EVENT_PARAMS, zzq.zzdy.zzfq.zzcv());
                }
            }
            bundle.putLong(ConditionalUserProperty.TRIGGERED_TIMESTAMP, zzq.zzdw.zztr);
            bundle.putLong(ConditionalUserProperty.TIME_TO_LIVE, zzq.timeToLive);
            if (zzq.zzdz != null) {
                bundle.putString(ConditionalUserProperty.EXPIRED_EVENT_NAME, zzq.zzdz.name);
                if (zzq.zzdz.zzfq != null) {
                    bundle.putBundle(ConditionalUserProperty.EXPIRED_EVENT_PARAMS, zzq.zzdz.zzfq.zzcv());
                }
            }
            arrayList.add(bundle);
        }
        return arrayList;
    }

    @VisibleForTesting
    private final boolean zzd(Context context, String str) {
        X500Principal x500Principal = new X500Principal("CN=Android Debug,O=Android,C=US");
        try {
            PackageInfo packageInfo = Wrappers.packageManager(context).getPackageInfo(str, 64);
            if (!(packageInfo == null || packageInfo.signatures == null || packageInfo.signatures.length <= 0)) {
                return ((X509Certificate) CertificateFactory.getInstance("X.509").generateCertificate(new ByteArrayInputStream(packageInfo.signatures[0].toByteArray()))).getSubjectX500Principal().equals(x500Principal);
            }
        } catch (CertificateException e) {
            zzab().zzgk().zza("Error obtaining certificate", e);
        } catch (NameNotFoundException e2) {
            zzab().zzgk().zza("Package name not found", e2);
        }
        return true;
    }

    public static Bundle zzh(Bundle bundle) {
        if (bundle == null) {
            return new Bundle();
        }
        Bundle bundle2 = new Bundle(bundle);
        for (String str : bundle2.keySet()) {
            Object obj = bundle2.get(str);
            if (obj instanceof Bundle) {
                bundle2.putBundle(str, new Bundle((Bundle) obj));
            } else if (obj instanceof Parcelable[]) {
                Parcelable[] parcelableArr = (Parcelable[]) obj;
                for (int i = 0; i < parcelableArr.length; i++) {
                    if (parcelableArr[i] instanceof Bundle) {
                        parcelableArr[i] = new Bundle((Bundle) parcelableArr[i]);
                    }
                }
            } else if (obj instanceof List) {
                List list = (List) obj;
                for (int i2 = 0; i2 < list.size(); i2++) {
                    Object obj2 = list.get(i2);
                    if (obj2 instanceof Bundle) {
                        list.set(i2, new Bundle((Bundle) obj2));
                    }
                }
            }
        }
        return bundle2;
    }

    static boolean zzs(String str, String str2) {
        if (str == null && str2 == null) {
            return true;
        }
        if (str == null) {
            return false;
        }
        return str.equals(str2);
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    /* access modifiers changed from: 0000 */
    public final Bundle zza(@NonNull Uri uri) {
        String str;
        String str2;
        String str3;
        String str4;
        Bundle bundle = null;
        if (uri != null) {
            try {
                if (uri.isHierarchical()) {
                    str2 = uri.getQueryParameter("utm_campaign");
                    str4 = uri.getQueryParameter("utm_source");
                    str = uri.getQueryParameter("utm_medium");
                    str3 = uri.getQueryParameter("gclid");
                } else {
                    str = null;
                    str2 = null;
                    str3 = null;
                    str4 = null;
                }
                if (!TextUtils.isEmpty(str2) || !TextUtils.isEmpty(str4) || !TextUtils.isEmpty(str) || !TextUtils.isEmpty(str3)) {
                    bundle = new Bundle();
                    if (!TextUtils.isEmpty(str2)) {
                        bundle.putString(Param.CAMPAIGN, str2);
                    }
                    if (!TextUtils.isEmpty(str4)) {
                        bundle.putString("source", str4);
                    }
                    if (!TextUtils.isEmpty(str)) {
                        bundle.putString(Param.MEDIUM, str);
                    }
                    if (!TextUtils.isEmpty(str3)) {
                        bundle.putString("gclid", str3);
                    }
                    String queryParameter = uri.getQueryParameter("utm_term");
                    if (!TextUtils.isEmpty(queryParameter)) {
                        bundle.putString(Param.TERM, queryParameter);
                    }
                    String queryParameter2 = uri.getQueryParameter("utm_content");
                    if (!TextUtils.isEmpty(queryParameter2)) {
                        bundle.putString(Param.CONTENT, queryParameter2);
                    }
                    String queryParameter3 = uri.getQueryParameter(Param.ACLID);
                    if (!TextUtils.isEmpty(queryParameter3)) {
                        bundle.putString(Param.ACLID, queryParameter3);
                    }
                    String queryParameter4 = uri.getQueryParameter(Param.CP1);
                    if (!TextUtils.isEmpty(queryParameter4)) {
                        bundle.putString(Param.CP1, queryParameter4);
                    }
                    String queryParameter5 = uri.getQueryParameter("anid");
                    if (!TextUtils.isEmpty(queryParameter5)) {
                        bundle.putString("anid", queryParameter5);
                    }
                }
            } catch (UnsupportedOperationException e) {
                zzab().zzgn().zza("Install referrer url isn't a hierarchical URI", e);
            }
        }
        return bundle;
    }

    /* access modifiers changed from: 0000 */
    /* JADX WARNING: Removed duplicated region for block: B:51:0x00d1  */
    /* JADX WARNING: Removed duplicated region for block: B:77:0x0142  */
    /* JADX WARNING: Removed duplicated region for block: B:80:0x017e  */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public final android.os.Bundle zza(java.lang.String r10, java.lang.String r11, android.os.Bundle r12, @android.support.annotation.Nullable java.util.List<java.lang.String> r13, boolean r14, boolean r15) {
        /*
            r9 = this;
            r0 = 0
            if (r12 == 0) goto L_0x0183
            android.os.Bundle r7 = new android.os.Bundle
            r7.<init>(r12)
            com.google.android.gms.measurement.internal.zzs r0 = r9.zzad()
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r1 = com.google.android.gms.measurement.internal.zzak.zziw
            boolean r0 = r0.zze(r10, r1)
            if (r0 == 0) goto L_0x006d
            java.util.TreeSet r0 = new java.util.TreeSet
            java.util.Set r1 = r12.keySet()
            r0.<init>(r1)
        L_0x001d:
            java.util.Iterator r8 = r0.iterator()
            r0 = 0
            r6 = r0
        L_0x0023:
            boolean r0 = r8.hasNext()
            if (r0 == 0) goto L_0x0182
            java.lang.Object r2 = r8.next()
            java.lang.String r2 = (java.lang.String) r2
            r0 = 0
            r1 = 0
            if (r13 == 0) goto L_0x0039
            boolean r3 = r13.contains(r2)
            if (r3 != 0) goto L_0x004f
        L_0x0039:
            if (r14 == 0) goto L_0x0184
            java.lang.String r0 = "event param"
            boolean r0 = r9.zzp(r0, r2)
            if (r0 != 0) goto L_0x0072
            r0 = 3
        L_0x0044:
            if (r0 != 0) goto L_0x004f
            java.lang.String r0 = "event param"
            boolean r0 = r9.zzq(r0, r2)
            if (r0 != 0) goto L_0x008c
            r0 = 3
        L_0x004f:
            if (r0 == 0) goto L_0x00a6
            boolean r1 = zza(r7, r0)
            if (r1 == 0) goto L_0x0069
            java.lang.String r1 = "_ev"
            r3 = 40
            r4 = 1
            java.lang.String r3 = zza(r2, r3, r4)
            r7.putString(r1, r3)
            r1 = 3
            if (r0 != r1) goto L_0x0069
            zzb(r7, r2)
        L_0x0069:
            r7.remove(r2)
            goto L_0x0023
        L_0x006d:
            java.util.Set r0 = r12.keySet()
            goto L_0x001d
        L_0x0072:
            java.lang.String r0 = "event param"
            r1 = 0
            boolean r0 = r9.zza(r0, r1, r2)
            if (r0 != 0) goto L_0x007e
            r0 = 14
            goto L_0x0044
        L_0x007e:
            java.lang.String r0 = "event param"
            r1 = 40
            boolean r0 = r9.zza(r0, r1, r2)
            if (r0 != 0) goto L_0x008a
            r0 = 3
            goto L_0x0044
        L_0x008a:
            r0 = 0
            goto L_0x0044
        L_0x008c:
            java.lang.String r0 = "event param"
            r1 = 0
            boolean r0 = r9.zza(r0, r1, r2)
            if (r0 != 0) goto L_0x0098
            r0 = 14
            goto L_0x004f
        L_0x0098:
            java.lang.String r0 = "event param"
            r1 = 40
            boolean r0 = r9.zza(r0, r1, r2)
            if (r0 != 0) goto L_0x00a4
            r0 = 3
            goto L_0x004f
        L_0x00a4:
            r0 = 0
            goto L_0x004f
        L_0x00a6:
            java.lang.Object r4 = r12.get(r2)
            r9.zzo()
            if (r15 == 0) goto L_0x010b
            boolean r0 = r4 instanceof android.os.Parcelable[]
            if (r0 == 0) goto L_0x00fb
            r0 = r4
            android.os.Parcelable[] r0 = (android.os.Parcelable[]) r0
            int r0 = r0.length
        L_0x00b7:
            r1 = 1000(0x3e8, float:1.401E-42)
            if (r0 <= r1) goto L_0x0109
            com.google.android.gms.measurement.internal.zzef r1 = r9.zzab()
            com.google.android.gms.measurement.internal.zzeh r1 = r1.zzgp()
            java.lang.String r3 = "Parameter array is too long; discarded. Value kind, name, array length"
            java.lang.String r5 = "param"
            java.lang.Integer r0 = java.lang.Integer.valueOf(r0)
            r1.zza(r3, r5, r2, r0)
            r0 = 0
        L_0x00cf:
            if (r0 != 0) goto L_0x010b
            r0 = 17
        L_0x00d3:
            if (r0 == 0) goto L_0x013c
            java.lang.String r1 = "_ev"
            boolean r1 = r1.equals(r2)
            if (r1 != 0) goto L_0x013c
            boolean r0 = zza(r7, r0)
            if (r0 == 0) goto L_0x00f6
            java.lang.String r0 = "_ev"
            r1 = 40
            r3 = 1
            java.lang.String r1 = zza(r2, r1, r3)
            r7.putString(r0, r1)
            java.lang.Object r0 = r12.get(r2)
            zzb(r7, r0)
        L_0x00f6:
            r7.remove(r2)
            goto L_0x0023
        L_0x00fb:
            boolean r0 = r4 instanceof java.util.ArrayList
            if (r0 == 0) goto L_0x0107
            r0 = r4
            java.util.ArrayList r0 = (java.util.ArrayList) r0
            int r0 = r0.size()
            goto L_0x00b7
        L_0x0107:
            r0 = 1
            goto L_0x00cf
        L_0x0109:
            r0 = 1
            goto L_0x00cf
        L_0x010b:
            com.google.android.gms.measurement.internal.zzs r0 = r9.zzad()
            boolean r0 = r0.zzn(r10)
            if (r0 == 0) goto L_0x011b
            boolean r0 = zzbq(r11)
            if (r0 != 0) goto L_0x0121
        L_0x011b:
            boolean r0 = zzbq(r2)
            if (r0 == 0) goto L_0x012f
        L_0x0121:
            java.lang.String r1 = "param"
            r3 = 256(0x100, float:3.59E-43)
            r0 = r9
            r5 = r15
            boolean r0 = r0.zza(r1, r2, r3, r4, r5)
        L_0x012b:
            if (r0 == 0) goto L_0x013a
            r0 = 0
            goto L_0x00d3
        L_0x012f:
            java.lang.String r1 = "param"
            r3 = 100
            r0 = r9
            r5 = r15
            boolean r0 = r0.zza(r1, r2, r3, r4, r5)
            goto L_0x012b
        L_0x013a:
            r0 = 4
            goto L_0x00d3
        L_0x013c:
            boolean r0 = zzbk(r2)
            if (r0 == 0) goto L_0x017e
            int r0 = r6 + 1
            r1 = 25
            if (r0 <= r1) goto L_0x017f
            java.lang.StringBuilder r1 = new java.lang.StringBuilder
            r3 = 48
            r1.<init>(r3)
            java.lang.String r3 = "Event can't contain more than 25 params"
            java.lang.StringBuilder r1 = r1.append(r3)
            java.lang.String r1 = r1.toString()
            com.google.android.gms.measurement.internal.zzef r3 = r9.zzab()
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgm()
            com.google.android.gms.measurement.internal.zzed r4 = r9.zzy()
            java.lang.String r4 = r4.zzaj(r11)
            com.google.android.gms.measurement.internal.zzed r5 = r9.zzy()
            java.lang.String r5 = r5.zzc(r12)
            r3.zza(r1, r4, r5)
            r1 = 5
            zza(r7, r1)
            r7.remove(r2)
            r6 = r0
            goto L_0x0023
        L_0x017e:
            r0 = r6
        L_0x017f:
            r6 = r0
            goto L_0x0023
        L_0x0182:
            r0 = r7
        L_0x0183:
            return r0
        L_0x0184:
            r0 = r1
            goto L_0x0044
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzjs.zza(java.lang.String, java.lang.String, android.os.Bundle, java.util.List, boolean, boolean):android.os.Bundle");
    }

    /* access modifiers changed from: 0000 */
    public final zzai zza(String str, String str2, Bundle bundle, String str3, long j, boolean z, boolean z2) {
        if (TextUtils.isEmpty(str2)) {
            return null;
        }
        if (zzbl(str2) != 0) {
            zzab().zzgk().zza("Invalid conditional property event name", zzy().zzal(str2));
            throw new IllegalArgumentException();
        }
        Bundle bundle2 = bundle != null ? new Bundle(bundle) : new Bundle();
        bundle2.putString("_o", str3);
        zzai zzai = new zzai(str2, new zzah(zzg(zza(str, str2, bundle2, CollectionUtils.listOf("_o"), false, false))), str3, j);
        return zzai;
    }

    public final URL zza(long j, @NonNull String str, @NonNull String str2) {
        try {
            Preconditions.checkNotEmpty(str2);
            Preconditions.checkNotEmpty(str);
            return new URL(String.format("https://www.googleadservices.com/pagead/conversion/app/deeplink?id_type=adid&sdk_version=%s&rdid=%s&bundleid=%s", new Object[]{String.format("v%s.%s", new Object[]{Long.valueOf(j), Integer.valueOf(zzjx())}), str2, str}));
        } catch (IllegalArgumentException | MalformedURLException e) {
            zzab().zzgk().zza("Failed to create BOW URL for Deferred Deep Link. exception", e.getMessage());
            return null;
        }
    }

    public final void zza(int i, String str, String str2, int i2) {
        zza((String) null, i, str, str2, i2);
    }

    /* access modifiers changed from: 0000 */
    public final void zza(Bundle bundle, String str, Object obj) {
        if (bundle != null) {
            if (obj instanceof Long) {
                bundle.putLong(str, ((Long) obj).longValue());
            } else if (obj instanceof String) {
                bundle.putString(str, String.valueOf(obj));
            } else if (obj instanceof Double) {
                bundle.putDouble(str, ((Double) obj).doubleValue());
            } else if (str != null) {
                zzab().zzgp().zza("Not putting event parameter. Invalid value type. name, type", zzy().zzak(str), obj != null ? obj.getClass().getSimpleName() : null);
            }
        }
    }

    public final void zza(zzp zzp, int i) {
        Bundle bundle = new Bundle();
        bundle.putInt("r", i);
        try {
            zzp.zzb(bundle);
        } catch (RemoteException e) {
            this.zzj.zzab().zzgn().zza("Error returning int value to wrapper", e);
        }
    }

    public final void zza(zzp zzp, long j) {
        Bundle bundle = new Bundle();
        bundle.putLong("r", j);
        try {
            zzp.zzb(bundle);
        } catch (RemoteException e) {
            this.zzj.zzab().zzgn().zza("Error returning long value to wrapper", e);
        }
    }

    public final void zza(zzp zzp, Bundle bundle) {
        try {
            zzp.zzb(bundle);
        } catch (RemoteException e) {
            this.zzj.zzab().zzgn().zza("Error returning bundle value to wrapper", e);
        }
    }

    public final void zza(zzp zzp, ArrayList<Bundle> arrayList) {
        Bundle bundle = new Bundle();
        bundle.putParcelableArrayList("r", arrayList);
        try {
            zzp.zzb(bundle);
        } catch (RemoteException e) {
            this.zzj.zzab().zzgn().zza("Error returning bundle list to wrapper", e);
        }
    }

    public final void zza(zzp zzp, boolean z) {
        Bundle bundle = new Bundle();
        bundle.putBoolean("r", z);
        try {
            zzp.zzb(bundle);
        } catch (RemoteException e) {
            this.zzj.zzab().zzgn().zza("Error returning boolean value to wrapper", e);
        }
    }

    public final void zza(zzp zzp, byte[] bArr) {
        Bundle bundle = new Bundle();
        bundle.putByteArray("r", bArr);
        try {
            zzp.zzb(bundle);
        } catch (RemoteException e) {
            this.zzj.zzab().zzgn().zza("Error returning byte array to wrapper", e);
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zza(String str, int i, String str2, String str3, int i2) {
        Bundle bundle = new Bundle();
        zza(bundle, i);
        if (zzad().zze(str, zzak.zzip)) {
            if (!TextUtils.isEmpty(str2) && !TextUtils.isEmpty(str3)) {
                bundle.putString(str2, str3);
            }
        } else if (!TextUtils.isEmpty(str2)) {
            bundle.putString(str2, str3);
        }
        if (i == 6 || i == 7 || i == 2) {
            bundle.putLong("_el", (long) i2);
        }
        this.zzj.zzae();
        this.zzj.zzq().logEvent("auto", "_err", bundle);
    }

    /* access modifiers changed from: 0000 */
    public final boolean zza(String str, int i, String str2) {
        if (str2 == null) {
            zzab().zzgm().zza("Name is required and can't be null. Type", str);
            return false;
        } else if (str2.codePointCount(0, str2.length()) <= i) {
            return true;
        } else {
            zzab().zzgm().zza("Name is too long. Type, maximum supported length, name", str, Integer.valueOf(i), str2);
            return false;
        }
    }

    /* access modifiers changed from: 0000 */
    public final boolean zza(String str, String[] strArr, String str2) {
        boolean z;
        boolean z2;
        if (str2 == null) {
            zzab().zzgm().zza("Name is required and can't be null. Type", str);
            return false;
        }
        Preconditions.checkNotNull(str2);
        String[] strArr2 = zztw;
        int length = strArr2.length;
        int i = 0;
        while (true) {
            if (i >= length) {
                z = false;
                break;
            } else if (str2.startsWith(strArr2[i])) {
                z = true;
                break;
            } else {
                i++;
            }
        }
        if (z) {
            zzab().zzgm().zza("Name starts with reserved prefix. Type, name", str, str2);
            return false;
        }
        if (strArr != null) {
            Preconditions.checkNotNull(strArr);
            int length2 = strArr.length;
            int i2 = 0;
            while (true) {
                if (i2 >= length2) {
                    z2 = false;
                    break;
                } else if (zzs(str2, strArr[i2])) {
                    z2 = true;
                    break;
                } else {
                    i2++;
                }
            }
            if (z2) {
                zzab().zzgm().zza("Name is reserved. Type, name", str, str2);
                return false;
            }
        }
        return true;
    }

    public final /* bridge */ /* synthetic */ zzfc zzaa() {
        return super.zzaa();
    }

    public final /* bridge */ /* synthetic */ zzef zzab() {
        return super.zzab();
    }

    public final /* bridge */ /* synthetic */ zzeo zzac() {
        return super.zzac();
    }

    public final /* bridge */ /* synthetic */ zzs zzad() {
        return super.zzad();
    }

    public final /* bridge */ /* synthetic */ zzr zzae() {
        return super.zzae();
    }

    /* access modifiers changed from: 0000 */
    public final Object zzb(String str, Object obj) {
        int i = 256;
        if ("_ev".equals(str)) {
            return zza(256, obj, true);
        }
        if (!zzbq(str)) {
            i = 100;
        }
        return zza(i, obj, false);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzb(Bundle bundle, long j) {
        long j2 = bundle.getLong("_et");
        if (j2 != 0) {
            zzab().zzgn().zza("Params already contained engagement", Long.valueOf(j2));
        }
        bundle.putLong("_et", j2 + j);
    }

    public final void zzb(zzp zzp, String str) {
        Bundle bundle = new Bundle();
        bundle.putString("r", str);
        try {
            zzp.zzb(bundle);
        } catch (RemoteException e) {
            this.zzj.zzab().zzgn().zza("Error returning string value to wrapper", e);
        }
    }

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return true;
    }

    /* access modifiers changed from: 0000 */
    public final int zzbl(String str) {
        if (!zzq(NotificationCompat.CATEGORY_EVENT, str)) {
            return 2;
        }
        if (!zza(NotificationCompat.CATEGORY_EVENT, zzgj.zzpn, str)) {
            return 13;
        }
        return zza(NotificationCompat.CATEGORY_EVENT, 40, str) ? 0 : 2;
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void zzbl() {
        zzo();
        SecureRandom secureRandom = new SecureRandom();
        long nextLong = secureRandom.nextLong();
        if (nextLong == 0) {
            nextLong = secureRandom.nextLong();
            if (nextLong == 0) {
                zzab().zzgn().zzao("Utils falling back to Random for random id");
            }
        }
        this.zzty.set(nextLong);
    }

    /* access modifiers changed from: 0000 */
    public final int zzbm(String str) {
        if (!zzq("user property", str)) {
            return 6;
        }
        if (!zza("user property", zzgl.zzpp, str)) {
            return 15;
        }
        return zza("user property", 24, str) ? 0 : 6;
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzbp(String str) {
        zzo();
        if (Wrappers.packageManager(getContext()).checkCallingOrSelfPermission(str) == 0) {
            return true;
        }
        zzab().zzgr().zza("Permission not granted", str);
        return false;
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzbr(String str) {
        if (TextUtils.isEmpty(str)) {
            return false;
        }
        String zzbu = zzad().zzbu();
        zzae();
        return zzbu.equals(str);
    }

    /* access modifiers changed from: 0000 */
    public final int zzc(String str, Object obj) {
        return "_ldl".equals(str) ? zza("user property referrer", str, zzbo(str), obj, false) : zza("user property", str, zzbo(str), obj, false) ? 0 : 7;
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final long zzc(Context context, String str) {
        zzo();
        Preconditions.checkNotNull(context);
        Preconditions.checkNotEmpty(str);
        PackageManager packageManager = context.getPackageManager();
        MessageDigest messageDigest = getMessageDigest();
        if (messageDigest == null) {
            zzab().zzgk().zzao("Could not get MD5 instance");
            return -1;
        }
        if (packageManager != null) {
            try {
                if (!zzd(context, str)) {
                    PackageInfo packageInfo = Wrappers.packageManager(context).getPackageInfo(getContext().getPackageName(), 64);
                    if (packageInfo.signatures != null && packageInfo.signatures.length > 0) {
                        return zzd(messageDigest.digest(packageInfo.signatures[0].toByteArray()));
                    }
                    zzab().zzgn().zzao("Could not get signatures");
                    return -1;
                }
            } catch (NameNotFoundException e) {
                zzab().zzgk().zza("Package name not found", e);
            }
        }
        return 0;
    }

    public final int zzd(int i) {
        return GoogleApiAvailabilityLight.getInstance().isGooglePlayServicesAvailable(getContext(), 12451000);
    }

    /* access modifiers changed from: 0000 */
    public final Object zzd(String str, Object obj) {
        return "_ldl".equals(str) ? zza(zzbo(str), obj, true) : zza(zzbo(str), obj, false);
    }

    /* access modifiers changed from: 0000 */
    public final Bundle zzg(Bundle bundle) {
        Bundle bundle2 = new Bundle();
        if (bundle != null) {
            for (String str : bundle.keySet()) {
                Object zzb = zzb(str, bundle.get(str));
                if (zzb == null) {
                    zzab().zzgp().zza("Param value can't be null", zzy().zzak(str));
                } else {
                    zza(bundle2, str, zzb);
                }
            }
        }
        return bundle2;
    }

    public final long zzjv() {
        long andIncrement;
        if (this.zzty.get() == 0) {
            synchronized (this.zzty) {
                long nextLong = new Random(System.nanoTime() ^ zzx().currentTimeMillis()).nextLong();
                int i = this.zzag + 1;
                this.zzag = i;
                andIncrement = nextLong + ((long) i);
            }
        } else {
            synchronized (this.zzty) {
                this.zzty.compareAndSet(-1, 1);
                andIncrement = this.zzty.getAndIncrement();
            }
        }
        return andIncrement;
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final SecureRandom zzjw() {
        zzo();
        if (this.zztx == null) {
            this.zztx = new SecureRandom();
        }
        return this.zztx;
    }

    public final int zzjx() {
        if (this.zztz == null) {
            this.zztz = Integer.valueOf(GoogleApiAvailabilityLight.getInstance().getApkVersion(getContext()) / 1000);
        }
        return this.zztz.intValue();
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final String zzjy() {
        byte[] bArr = new byte[16];
        zzjw().nextBytes(bArr);
        return String.format(Locale.US, "%032x", new Object[]{new BigInteger(1, bArr)});
    }

    public final /* bridge */ /* synthetic */ void zzl() {
        super.zzl();
    }

    public final /* bridge */ /* synthetic */ void zzm() {
        super.zzm();
    }

    public final /* bridge */ /* synthetic */ void zzn() {
        super.zzn();
    }

    public final /* bridge */ /* synthetic */ void zzo() {
        super.zzo();
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzp(String str, String str2) {
        if (str2 == null) {
            zzab().zzgm().zza("Name is required and can't be null. Type", str);
            return false;
        } else if (str2.length() == 0) {
            zzab().zzgm().zza("Name is required and can't be empty. Type", str);
            return false;
        } else {
            int codePointAt = str2.codePointAt(0);
            if (!Character.isLetter(codePointAt)) {
                zzab().zzgm().zza("Name must start with a letter. Type, name", str, str2);
                return false;
            }
            int length = str2.length();
            int charCount = Character.charCount(codePointAt);
            while (charCount < length) {
                int codePointAt2 = str2.codePointAt(charCount);
                if (codePointAt2 == 95 || Character.isLetterOrDigit(codePointAt2)) {
                    charCount += Character.charCount(codePointAt2);
                } else {
                    zzab().zzgm().zza("Name must consist of letters, digits or _ (underscores). Type, name", str, str2);
                    return false;
                }
            }
            return true;
        }
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzq(String str, String str2) {
        if (str2 == null) {
            zzab().zzgm().zza("Name is required and can't be null. Type", str);
            return false;
        } else if (str2.length() == 0) {
            zzab().zzgm().zza("Name is required and can't be empty. Type", str);
            return false;
        } else {
            int codePointAt = str2.codePointAt(0);
            if (Character.isLetter(codePointAt) || codePointAt == 95) {
                int length = str2.length();
                int charCount = Character.charCount(codePointAt);
                while (charCount < length) {
                    int codePointAt2 = str2.codePointAt(charCount);
                    if (codePointAt2 == 95 || Character.isLetterOrDigit(codePointAt2)) {
                        charCount += Character.charCount(codePointAt2);
                    } else {
                        zzab().zzgm().zza("Name must consist of letters, digits or _ (underscores). Type, name", str, str2);
                        return false;
                    }
                }
                return true;
            }
            zzab().zzgm().zza("Name must start with a letter or _ (underscore). Type, name", str, str2);
            return false;
        }
    }

    /* access modifiers changed from: 0000 */
    public final boolean zzr(String str, String str2) {
        if (!TextUtils.isEmpty(str)) {
            if (!zzbn(str)) {
                if (!this.zzj.zzhw()) {
                    return false;
                }
                zzab().zzgm().zza("Invalid google_app_id. Firebase Analytics disabled. See https://goo.gl/NAOOOI. provided id", zzef.zzam(str));
                return false;
            }
        } else if (!TextUtils.isEmpty(str2)) {
            if (!zzbn(str2)) {
                zzab().zzgm().zza("Invalid admob_app_id. Analytics disabled.", zzef.zzam(str2));
                return false;
            }
        } else if (!this.zzj.zzhw()) {
            return false;
        } else {
            zzab().zzgm().zzao("Missing google_app_id. Firebase Analytics disabled. See https://goo.gl/NAOOOI");
            return false;
        }
        return true;
    }

    public final /* bridge */ /* synthetic */ zzac zzw() {
        return super.zzw();
    }

    public final /* bridge */ /* synthetic */ Clock zzx() {
        return super.zzx();
    }

    public final /* bridge */ /* synthetic */ zzed zzy() {
        return super.zzy();
    }

    public final /* bridge */ /* synthetic */ zzjs zzz() {
        return super.zzz();
    }
}
