package com.google.android.gms.measurement.internal;

import android.content.Context;
import android.support.annotation.GuardedBy;
import android.text.TextUtils;
import android.util.Log;
import android.util.Pair;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.Clock;
import com.google.android.gms.common.util.VisibleForTesting;

public final class zzef extends zzge {
    /* access modifiers changed from: private */
    public char zzkg = ((char) 0);
    @GuardedBy("this")
    private String zzkh;
    private final zzeh zzki = new zzeh(this, 6, false, false);
    private final zzeh zzkj = new zzeh(this, 6, true, false);
    private final zzeh zzkk = new zzeh(this, 6, false, true);
    private final zzeh zzkl = new zzeh(this, 5, false, false);
    private final zzeh zzkm = new zzeh(this, 5, true, false);
    private final zzeh zzkn = new zzeh(this, 5, false, true);
    private final zzeh zzko = new zzeh(this, 4, false, false);
    private final zzeh zzkp = new zzeh(this, 3, false, false);
    private final zzeh zzkq = new zzeh(this, 2, false, false);
    /* access modifiers changed from: private */
    public long zzr = -1;

    zzef(zzfj zzfj) {
        super(zzfj);
    }

    @VisibleForTesting
    private static String zza(boolean z, Object obj) {
        if (obj == null) {
            return "";
        }
        Object obj2 = obj instanceof Integer ? Long.valueOf((long) ((Integer) obj).intValue()) : obj;
        if (obj2 instanceof Long) {
            if (!z) {
                return String.valueOf(obj2);
            }
            if (Math.abs(((Long) obj2).longValue()) < 100) {
                return String.valueOf(obj2);
            }
            String str = String.valueOf(obj2).charAt(0) == '-' ? "-" : "";
            String valueOf = String.valueOf(Math.abs(((Long) obj2).longValue()));
            return new StringBuilder(String.valueOf(str).length() + 43 + String.valueOf(str).length()).append(str).append(Math.round(Math.pow(10.0d, (double) (valueOf.length() - 1)))).append("...").append(str).append(Math.round(Math.pow(10.0d, (double) valueOf.length()) - 1.0d)).toString();
        } else if (obj2 instanceof Boolean) {
            return String.valueOf(obj2);
        } else {
            if (!(obj2 instanceof Throwable)) {
                return obj2 instanceof zzeg ? ((zzeg) obj2).zzkr : z ? "-" : String.valueOf(obj2);
            }
            Throwable th = (Throwable) obj2;
            StringBuilder sb = new StringBuilder(z ? th.getClass().getName() : th.toString());
            String zzan = zzan(zzfj.class.getCanonicalName());
            StackTraceElement[] stackTrace = th.getStackTrace();
            int length = stackTrace.length;
            int i = 0;
            while (true) {
                if (i >= length) {
                    break;
                }
                StackTraceElement stackTraceElement = stackTrace[i];
                if (!stackTraceElement.isNativeMethod()) {
                    String className = stackTraceElement.getClassName();
                    if (className != null && zzan(className).equals(zzan)) {
                        sb.append(": ");
                        sb.append(stackTraceElement);
                        break;
                    }
                }
                i++;
            }
            return sb.toString();
        }
    }

    static String zza(boolean z, String str, Object obj, Object obj2, Object obj3) {
        if (str == null) {
            str = "";
        }
        String zza = zza(z, obj);
        String zza2 = zza(z, obj2);
        String zza3 = zza(z, obj3);
        StringBuilder sb = new StringBuilder();
        String str2 = "";
        if (!TextUtils.isEmpty(str)) {
            sb.append(str);
            str2 = ": ";
        }
        if (!TextUtils.isEmpty(zza)) {
            sb.append(str2);
            sb.append(zza);
            str2 = ", ";
        }
        if (!TextUtils.isEmpty(zza2)) {
            sb.append(str2);
            sb.append(zza2);
            str2 = ", ";
        }
        if (!TextUtils.isEmpty(zza3)) {
            sb.append(str2);
            sb.append(zza3);
        }
        return sb.toString();
    }

    protected static Object zzam(String str) {
        if (str == null) {
            return null;
        }
        return new zzeg(str);
    }

    private static String zzan(String str) {
        if (TextUtils.isEmpty(str)) {
            return "";
        }
        int lastIndexOf = str.lastIndexOf(46);
        return lastIndexOf != -1 ? str.substring(0, lastIndexOf) : str;
    }

    @VisibleForTesting
    private final String zzgt() {
        String str;
        synchronized (this) {
            if (this.zzkh == null) {
                if (this.zzj.zzhz() != null) {
                    this.zzkh = this.zzj.zzhz();
                } else {
                    this.zzkh = zzs.zzbm();
                }
            }
            str = this.zzkh;
        }
        return str;
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    /* access modifiers changed from: protected */
    @VisibleForTesting
    public final boolean isLoggable(int i) {
        return Log.isLoggable(zzgt(), i);
    }

    /* access modifiers changed from: protected */
    @VisibleForTesting
    public final void zza(int i, String str) {
        Log.println(i, zzgt(), str);
    }

    /* access modifiers changed from: protected */
    public final void zza(int i, boolean z, boolean z2, String str, Object obj, Object obj2, Object obj3) {
        int i2 = 0;
        if (!z && isLoggable(i)) {
            zza(i, zza(false, str, obj, obj2, obj3));
        }
        if (!z2 && i >= 5) {
            Preconditions.checkNotNull(str);
            zzfc zzhu = this.zzj.zzhu();
            if (zzhu == null) {
                zza(6, "Scheduler not set. Not logging error/warn");
            } else if (!zzhu.isInitialized()) {
                zza(6, "Scheduler not initialized. Not logging error/warn");
            } else {
                if (i >= 0) {
                    i2 = i;
                }
                if (i2 >= 9) {
                    i2 = 8;
                }
                zzhu.zza((Runnable) new zzee(this, i2, str, obj, obj2, obj3));
            }
        }
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

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return false;
    }

    public final zzeh zzgk() {
        return this.zzki;
    }

    public final zzeh zzgl() {
        return this.zzkj;
    }

    public final zzeh zzgm() {
        return this.zzkk;
    }

    public final zzeh zzgn() {
        return this.zzkl;
    }

    public final zzeh zzgo() {
        return this.zzkm;
    }

    public final zzeh zzgp() {
        return this.zzkn;
    }

    public final zzeh zzgq() {
        return this.zzko;
    }

    public final zzeh zzgr() {
        return this.zzkp;
    }

    public final zzeh zzgs() {
        return this.zzkq;
    }

    public final String zzgu() {
        Pair<String, Long> zzhl = zzac().zzli.zzhl();
        if (zzhl == null || zzhl == zzeo.zzlg) {
            return null;
        }
        String valueOf = String.valueOf(zzhl.second);
        String str = (String) zzhl.first;
        return new StringBuilder(String.valueOf(valueOf).length() + 1 + String.valueOf(str).length()).append(valueOf).append(":").append(str).toString();
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
