package com.google.android.gms.internal;

import android.support.annotation.WorkerThread;
import android.text.TextUtils;
import com.google.android.gms.common.internal.zzbp;

final class zzcaj {
    private final String mAppId;
    private String zzcxs;
    private String zzdmh;
    private String zzgah;
    private final zzcco zzikb;
    private String zziks;
    private String zzikt;
    private long zziku;
    private long zzikv;
    private long zzikw;
    private long zzikx;
    private String zziky;
    private long zzikz;
    private long zzila;
    private boolean zzilb;
    private long zzilc;
    private long zzild;
    private long zzile;
    private long zzilf;
    private long zzilg;
    private long zzilh;
    private long zzili;
    private String zzilj;
    private boolean zzilk;
    private long zzill;
    private long zzilm;

    @WorkerThread
    zzcaj(zzcco zzcco, String str) {
        zzbp.zzu(zzcco);
        zzbp.zzgf(str);
        this.zzikb = zzcco;
        this.mAppId = str;
        this.zzikb.zzauj().zzug();
    }

    @WorkerThread
    public final String getAppId() {
        this.zzikb.zzauj().zzug();
        return this.mAppId;
    }

    @WorkerThread
    public final String getAppInstanceId() {
        this.zzikb.zzauj().zzug();
        return this.zzgah;
    }

    @WorkerThread
    public final String getGmpAppId() {
        this.zzikb.zzauj().zzug();
        return this.zzcxs;
    }

    @WorkerThread
    public final void setAppVersion(String str) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (!zzcfo.zzau(this.zzdmh, str) ? 1 : 0) | this.zzilk;
        this.zzdmh = str;
    }

    @WorkerThread
    public final void setMeasurementEnabled(boolean z) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzilb != z ? 1 : 0) | this.zzilk;
        this.zzilb = z;
    }

    @WorkerThread
    public final void zzal(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzikv != j ? 1 : 0) | this.zzilk;
        this.zzikv = j;
    }

    @WorkerThread
    public final void zzam(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzikw != j ? 1 : 0) | this.zzilk;
        this.zzikw = j;
    }

    @WorkerThread
    public final void zzan(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzikx != j ? 1 : 0) | this.zzilk;
        this.zzikx = j;
    }

    @WorkerThread
    public final void zzao(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzikz != j ? 1 : 0) | this.zzilk;
        this.zzikz = j;
    }

    @WorkerThread
    public final void zzap(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzila != j ? 1 : 0) | this.zzilk;
        this.zzila = j;
    }

    @WorkerThread
    public final void zzaq(long j) {
        int i = 0;
        zzbp.zzbh(j >= 0);
        this.zzikb.zzauj().zzug();
        boolean z = this.zzilk;
        if (this.zziku != j) {
            i = 1;
        }
        this.zzilk = z | i;
        this.zziku = j;
    }

    @WorkerThread
    public final void zzar(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzill != j ? 1 : 0) | this.zzilk;
        this.zzill = j;
    }

    @WorkerThread
    public final void zzas(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzilm != j ? 1 : 0) | this.zzilk;
        this.zzilm = j;
    }

    @WorkerThread
    public final void zzat(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzild != j ? 1 : 0) | this.zzilk;
        this.zzild = j;
    }

    @WorkerThread
    public final void zzau(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzile != j ? 1 : 0) | this.zzilk;
        this.zzile = j;
    }

    @WorkerThread
    public final void zzaun() {
        this.zzikb.zzauj().zzug();
        this.zzilk = false;
    }

    @WorkerThread
    public final String zzauo() {
        this.zzikb.zzauj().zzug();
        return this.zziks;
    }

    @WorkerThread
    public final String zzaup() {
        this.zzikb.zzauj().zzug();
        return this.zzikt;
    }

    @WorkerThread
    public final long zzauq() {
        this.zzikb.zzauj().zzug();
        return this.zzikv;
    }

    @WorkerThread
    public final long zzaur() {
        this.zzikb.zzauj().zzug();
        return this.zzikw;
    }

    @WorkerThread
    public final long zzaus() {
        this.zzikb.zzauj().zzug();
        return this.zzikx;
    }

    @WorkerThread
    public final String zzaut() {
        this.zzikb.zzauj().zzug();
        return this.zziky;
    }

    @WorkerThread
    public final long zzauu() {
        this.zzikb.zzauj().zzug();
        return this.zzikz;
    }

    @WorkerThread
    public final long zzauv() {
        this.zzikb.zzauj().zzug();
        return this.zzila;
    }

    @WorkerThread
    public final boolean zzauw() {
        this.zzikb.zzauj().zzug();
        return this.zzilb;
    }

    @WorkerThread
    public final long zzaux() {
        this.zzikb.zzauj().zzug();
        return this.zziku;
    }

    @WorkerThread
    public final long zzauy() {
        this.zzikb.zzauj().zzug();
        return this.zzill;
    }

    @WorkerThread
    public final long zzauz() {
        this.zzikb.zzauj().zzug();
        return this.zzilm;
    }

    @WorkerThread
    public final void zzav(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzilf != j ? 1 : 0) | this.zzilk;
        this.zzilf = j;
    }

    @WorkerThread
    public final void zzava() {
        this.zzikb.zzauj().zzug();
        long j = this.zziku + 1;
        if (j > 2147483647L) {
            this.zzikb.zzauk().zzaye().zzj("Bundle index overflow. appId", zzcbo.zzjf(this.mAppId));
            j = 0;
        }
        this.zzilk = true;
        this.zziku = j;
    }

    @WorkerThread
    public final long zzavb() {
        this.zzikb.zzauj().zzug();
        return this.zzild;
    }

    @WorkerThread
    public final long zzavc() {
        this.zzikb.zzauj().zzug();
        return this.zzile;
    }

    @WorkerThread
    public final long zzavd() {
        this.zzikb.zzauj().zzug();
        return this.zzilf;
    }

    @WorkerThread
    public final long zzave() {
        this.zzikb.zzauj().zzug();
        return this.zzilg;
    }

    @WorkerThread
    public final long zzavf() {
        this.zzikb.zzauj().zzug();
        return this.zzili;
    }

    @WorkerThread
    public final long zzavg() {
        this.zzikb.zzauj().zzug();
        return this.zzilh;
    }

    @WorkerThread
    public final String zzavh() {
        this.zzikb.zzauj().zzug();
        return this.zzilj;
    }

    @WorkerThread
    public final String zzavi() {
        this.zzikb.zzauj().zzug();
        String str = this.zzilj;
        zzir(null);
        return str;
    }

    @WorkerThread
    public final long zzavj() {
        this.zzikb.zzauj().zzug();
        return this.zzilc;
    }

    @WorkerThread
    public final void zzaw(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzilg != j ? 1 : 0) | this.zzilk;
        this.zzilg = j;
    }

    @WorkerThread
    public final void zzax(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzili != j ? 1 : 0) | this.zzilk;
        this.zzili = j;
    }

    @WorkerThread
    public final void zzay(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzilh != j ? 1 : 0) | this.zzilk;
        this.zzilh = j;
    }

    @WorkerThread
    public final void zzaz(long j) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (this.zzilc != j ? 1 : 0) | this.zzilk;
        this.zzilc = j;
    }

    @WorkerThread
    public final void zzim(String str) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (!zzcfo.zzau(this.zzgah, str) ? 1 : 0) | this.zzilk;
        this.zzgah = str;
    }

    @WorkerThread
    public final void zzin(String str) {
        this.zzikb.zzauj().zzug();
        if (TextUtils.isEmpty(str)) {
            str = null;
        }
        this.zzilk = (!zzcfo.zzau(this.zzcxs, str) ? 1 : 0) | this.zzilk;
        this.zzcxs = str;
    }

    @WorkerThread
    public final void zzio(String str) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (!zzcfo.zzau(this.zziks, str) ? 1 : 0) | this.zzilk;
        this.zziks = str;
    }

    @WorkerThread
    public final void zzip(String str) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (!zzcfo.zzau(this.zzikt, str) ? 1 : 0) | this.zzilk;
        this.zzikt = str;
    }

    @WorkerThread
    public final void zziq(String str) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (!zzcfo.zzau(this.zziky, str) ? 1 : 0) | this.zzilk;
        this.zziky = str;
    }

    @WorkerThread
    public final void zzir(String str) {
        this.zzikb.zzauj().zzug();
        this.zzilk = (!zzcfo.zzau(this.zzilj, str) ? 1 : 0) | this.zzilk;
        this.zzilj = str;
    }

    @WorkerThread
    public final String zzul() {
        this.zzikb.zzauj().zzug();
        return this.zzdmh;
    }
}
