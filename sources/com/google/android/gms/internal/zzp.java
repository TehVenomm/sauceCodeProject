package com.google.android.gms.internal;

import android.net.Uri;
import android.os.Handler;
import android.os.Looper;
import android.text.TextUtils;
import java.util.Collections;
import java.util.Map;

public abstract class zzp<T> implements Comparable<zzp<T>> {
    private final zza zzab;
    private final int zzac;
    private final String zzad;
    private final int zzae;
    private final zzu zzaf;
    private Integer zzag;
    private zzs zzah;
    private boolean zzai;
    private boolean zzaj;
    private boolean zzak;
    private boolean zzal;
    private zzx zzam;
    private zzc zzan;

    public zzp(int i, String str, zzu zzu) {
        int hashCode;
        this.zzab = zza.zzbi ? new zza() : null;
        this.zzai = true;
        this.zzaj = false;
        this.zzak = false;
        this.zzal = false;
        this.zzan = null;
        this.zzac = i;
        this.zzad = str;
        this.zzaf = zzu;
        this.zzam = new zzg();
        if (!TextUtils.isEmpty(str)) {
            Uri parse = Uri.parse(str);
            if (parse != null) {
                String host = parse.getHost();
                if (host != null) {
                    hashCode = host.hashCode();
                    this.zzae = hashCode;
                }
            }
        }
        hashCode = 0;
        this.zzae = hashCode;
    }

    public static String zzf() {
        String valueOf = String.valueOf("UTF-8");
        return valueOf.length() != 0 ? "application/x-www-form-urlencoded; charset=".concat(valueOf) : new String("application/x-www-form-urlencoded; charset=");
    }

    public /* synthetic */ int compareTo(Object obj) {
        zzp zzp = (zzp) obj;
        zzr zzr = zzr.NORMAL;
        zzr zzr2 = zzr.NORMAL;
        return zzr == zzr2 ? this.zzag.intValue() - zzp.zzag.intValue() : zzr2.ordinal() - zzr.ordinal();
    }

    public Map<String, String> getHeaders() throws zza {
        return Collections.emptyMap();
    }

    public final int getMethod() {
        return this.zzac;
    }

    public final String getUrl() {
        return this.zzad;
    }

    public String toString() {
        String valueOf = String.valueOf(Integer.toHexString(this.zzae));
        valueOf = valueOf.length() != 0 ? "0x".concat(valueOf) : new String("0x");
        String str = this.zzad;
        String valueOf2 = String.valueOf(zzr.NORMAL);
        String valueOf3 = String.valueOf(this.zzag);
        return new StringBuilder(((((String.valueOf("[ ] ").length() + 3) + String.valueOf(str).length()) + String.valueOf(valueOf).length()) + String.valueOf(valueOf2).length()) + String.valueOf(valueOf3).length()).append("[ ] ").append(str).append(" ").append(valueOf).append(" ").append(valueOf2).append(" ").append(valueOf3).toString();
    }

    public final zzp<?> zza(int i) {
        this.zzag = Integer.valueOf(i);
        return this;
    }

    public final zzp<?> zza(zzc zzc) {
        this.zzan = zzc;
        return this;
    }

    public final zzp<?> zza(zzs zzs) {
        this.zzah = zzs;
        return this;
    }

    protected abstract zzt<T> zza(zzn zzn);

    protected abstract void zza(T t);

    public final void zzb(zzaa zzaa) {
        if (this.zzaf != null) {
            this.zzaf.zzd(zzaa);
        }
    }

    public final void zzb(String str) {
        if (zza.zzbi) {
            this.zzab.zza(str, Thread.currentThread().getId());
        }
    }

    public final int zzc() {
        return this.zzae;
    }

    final void zzc(String str) {
        if (this.zzah != null) {
            this.zzah.zzd(this);
        }
        if (zza.zzbi) {
            long id = Thread.currentThread().getId();
            if (Looper.myLooper() != Looper.getMainLooper()) {
                new Handler(Looper.getMainLooper()).post(new zzq(this, str, id));
                return;
            }
            this.zzab.zza(str, id);
            this.zzab.zzc(toString());
        }
    }

    public final String zzd() {
        return this.zzad;
    }

    public final zzc zze() {
        return this.zzan;
    }

    public byte[] zzg() throws zza {
        return null;
    }

    public final boolean zzh() {
        return this.zzai;
    }

    public final int zzi() {
        return this.zzam.zza();
    }

    public final zzx zzj() {
        return this.zzam;
    }

    public final void zzk() {
        this.zzak = true;
    }

    public final boolean zzl() {
        return this.zzak;
    }
}
