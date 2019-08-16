package com.google.android.gms.measurement.internal;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.Nullable;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.Clock;
import java.util.concurrent.atomic.AtomicReference;

public final class zzed extends zzge {
    private static final AtomicReference<String[]> zzjx = new AtomicReference<>();
    private static final AtomicReference<String[]> zzjy = new AtomicReference<>();
    private static final AtomicReference<String[]> zzjz = new AtomicReference<>();

    zzed(zzfj zzfj) {
        super(zzfj);
    }

    @Nullable
    private static String zza(String str, String[] strArr, String[] strArr2, AtomicReference<String[]> atomicReference) {
        int i = 0;
        Preconditions.checkNotNull(strArr);
        Preconditions.checkNotNull(strArr2);
        Preconditions.checkNotNull(atomicReference);
        Preconditions.checkArgument(strArr.length == strArr2.length);
        while (true) {
            if (i >= strArr.length) {
                break;
            } else if (zzjs.zzs(str, strArr[i])) {
                synchronized (atomicReference) {
                    String[] strArr3 = (String[]) atomicReference.get();
                    if (strArr3 == null) {
                        strArr3 = new String[strArr2.length];
                        atomicReference.set(strArr3);
                    }
                    if (strArr3[i] == null) {
                        StringBuilder sb = new StringBuilder();
                        sb.append(strArr2[i]);
                        sb.append("(");
                        sb.append(strArr[i]);
                        sb.append(")");
                        strArr3[i] = sb.toString();
                    }
                    str = strArr3[i];
                }
            } else {
                i++;
            }
        }
        return str;
    }

    @Nullable
    private final String zzb(zzah zzah) {
        if (zzah == null) {
            return null;
        }
        return !zzgj() ? zzah.toString() : zzc(zzah.zzcv());
    }

    private final boolean zzgj() {
        zzae();
        return this.zzj.zzhw() && this.zzj.zzab().isLoggable(3);
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
    }

    /* access modifiers changed from: protected */
    @Nullable
    public final String zza(zzaf zzaf) {
        if (zzaf == null) {
            return null;
        }
        if (!zzgj()) {
            return zzaf.toString();
        }
        StringBuilder sb = new StringBuilder();
        sb.append("Event{appId='");
        sb.append(zzaf.zzce);
        sb.append("', name='");
        sb.append(zzaj(zzaf.name));
        sb.append("', params=");
        sb.append(zzb(zzaf.zzfq));
        sb.append("}");
        return sb.toString();
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
    @Nullable
    public final String zzaj(String str) {
        if (str == null) {
            return null;
        }
        return zzgj() ? zza(str, zzgj.zzpo, zzgj.zzpn, zzjx) : str;
    }

    /* access modifiers changed from: protected */
    @Nullable
    public final String zzak(String str) {
        if (str == null) {
            return null;
        }
        return zzgj() ? zza(str, zzgi.zzpm, zzgi.zzpl, zzjy) : str;
    }

    /* access modifiers changed from: protected */
    @Nullable
    public final String zzal(String str) {
        if (str == null) {
            return null;
        }
        if (!zzgj()) {
            return str;
        }
        if (!str.startsWith("_exp_")) {
            return zza(str, zzgl.zzpq, zzgl.zzpp, zzjz);
        }
        StringBuilder sb = new StringBuilder();
        sb.append("experiment_id");
        sb.append("(");
        sb.append(str);
        sb.append(")");
        return sb.toString();
    }

    /* access modifiers changed from: protected */
    @Nullable
    public final String zzb(zzai zzai) {
        if (zzai == null) {
            return null;
        }
        if (!zzgj()) {
            return zzai.toString();
        }
        StringBuilder sb = new StringBuilder();
        sb.append("origin=");
        sb.append(zzai.origin);
        sb.append(",name=");
        sb.append(zzaj(zzai.name));
        sb.append(",params=");
        sb.append(zzb(zzai.zzfq));
        return sb.toString();
    }

    /* access modifiers changed from: protected */
    public final boolean zzbk() {
        return false;
    }

    /* access modifiers changed from: protected */
    @Nullable
    public final String zzc(Bundle bundle) {
        if (bundle == null) {
            return null;
        }
        if (!zzgj()) {
            return bundle.toString();
        }
        StringBuilder sb = new StringBuilder();
        for (String str : bundle.keySet()) {
            if (sb.length() != 0) {
                sb.append(", ");
            } else {
                sb.append("Bundle[{");
            }
            sb.append(zzak(str));
            sb.append("=");
            sb.append(bundle.get(str));
        }
        sb.append("}]");
        return sb.toString();
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
