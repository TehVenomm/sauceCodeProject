package com.google.android.gms.measurement.internal;

import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;
import android.net.Uri.Builder;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.WorkerThread;
import android.support.p000v4.util.ArrayMap;
import android.text.TextUtils;
import android.util.Pair;
import com.facebook.internal.AnalyticsEvents;
import com.google.android.gms.common.internal.Preconditions;
import com.google.android.gms.common.util.Clock;
import com.google.android.gms.common.util.VisibleForTesting;
import com.google.android.gms.common.wrappers.Wrappers;
import com.google.android.gms.internal.measurement.zzbs.zzc;
import com.google.android.gms.internal.measurement.zzbs.zze;
import com.google.android.gms.internal.measurement.zzbs.zzf;
import com.google.android.gms.internal.measurement.zzbs.zzg;
import com.google.android.gms.internal.measurement.zzbs.zzk;
import com.google.android.gms.internal.measurement.zzbw;
import com.google.android.gms.internal.measurement.zzey;
import com.google.android.gms.internal.measurement.zzx;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.net.MalformedURLException;
import java.net.URL;
import java.nio.ByteBuffer;
import java.nio.channels.FileChannel;
import java.nio.channels.FileLock;
import java.nio.channels.OverlappingFileLockException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import java.util.Map;
import java.util.concurrent.Callable;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import org.apache.commons.lang3.time.DateUtils;
import p017io.fabric.sdk.android.services.network.HttpRequest;

public class zzjg implements zzgh {
    private static volatile zzjg zzsn;
    private boolean zzdh;
    private final zzfj zzj;
    private zzfd zzso;
    private zzej zzsp;
    private zzx zzsq;
    private zzem zzsr;
    private zzjc zzss;
    private zzp zzst;
    private final zzjo zzsu;
    private zzhp zzsv;
    private boolean zzsw;
    private boolean zzsx;
    @VisibleForTesting
    private long zzsy;
    private List<Runnable> zzsz;
    private int zzta;
    private int zztb;
    private boolean zztc;
    private boolean zztd;
    private boolean zzte;
    private FileLock zztf;
    private FileChannel zztg;
    private List<Long> zzth;
    private List<Long> zzti;
    private long zztj;

    final class zza implements zzz {
        zzg zztn;
        List<Long> zzto;
        List<zzc> zztp;
        private long zztq;

        private zza() {
        }

        /* synthetic */ zza(zzjg zzjg, zzjj zzjj) {
            this();
        }

        private static long zza(zzc zzc) {
            return ((zzc.getTimestampMillis() / 1000) / 60) / 60;
        }

        public final boolean zza(long j, zzc zzc) {
            Preconditions.checkNotNull(zzc);
            if (this.zztp == null) {
                this.zztp = new ArrayList();
            }
            if (this.zzto == null) {
                this.zzto = new ArrayList();
            }
            if (this.zztp.size() > 0 && zza((zzc) this.zztp.get(0)) != zza(zzc)) {
                return false;
            }
            long zzuk = this.zztq + ((long) zzc.zzuk());
            if (zzuk >= ((long) Math.max(0, ((Integer) zzak.zzgn.get(null)).intValue()))) {
                return false;
            }
            this.zztq = zzuk;
            this.zztp.add(zzc);
            this.zzto.add(Long.valueOf(j));
            return this.zztp.size() < Math.max(1, ((Integer) zzak.zzgo.get(null)).intValue());
        }

        public final void zzb(zzg zzg) {
            Preconditions.checkNotNull(zzg);
            this.zztn = zzg;
        }
    }

    private zzjg(zzjm zzjm) {
        this(zzjm, null);
    }

    private zzjg(zzjm zzjm, zzfj zzfj) {
        this.zzdh = false;
        Preconditions.checkNotNull(zzjm);
        this.zzj = zzfj.zza(zzjm.zzob, (zzx) null);
        this.zztj = -1;
        zzjo zzjo = new zzjo(this);
        zzjo.initialize();
        this.zzsu = zzjo;
        zzej zzej = new zzej(this);
        zzej.initialize();
        this.zzsp = zzej;
        zzfd zzfd = new zzfd(this);
        zzfd.initialize();
        this.zzso = zzfd;
        this.zzj.zzaa().zza((Runnable) new zzjj(this, zzjm));
    }

    @WorkerThread
    @VisibleForTesting
    private final int zza(FileChannel fileChannel) {
        int i = 0;
        zzo();
        if (fileChannel == null || !fileChannel.isOpen()) {
            this.zzj.zzab().zzgk().zzao("Bad channel to read from");
            return i;
        }
        ByteBuffer allocate = ByteBuffer.allocate(4);
        try {
            fileChannel.position(0);
            int read = fileChannel.read(allocate);
            if (read == 4) {
                allocate.flip();
                return allocate.getInt();
            } else if (read == -1) {
                return i;
            } else {
                this.zzj.zzab().zzgn().zza("Unexpected data length. Bytes read", Integer.valueOf(read));
                return i;
            }
        } catch (IOException e) {
            this.zzj.zzab().zzgk().zza("Failed to read from channel", e);
            return i;
        }
    }

    private final zzn zza(Context context, String str, String str2, boolean z, boolean z2, boolean z3, long j, String str3) {
        String str4;
        String str5 = AnalyticsEvents.PARAMETER_DIALOG_OUTCOME_VALUE_UNKNOWN;
        String str6 = AnalyticsEvents.PARAMETER_DIALOG_OUTCOME_VALUE_UNKNOWN;
        int i = Integer.MIN_VALUE;
        PackageManager packageManager = context.getPackageManager();
        if (packageManager == null) {
            this.zzj.zzab().zzgk().zzao("PackageManager is null, can not log app install information");
            return null;
        }
        try {
            str5 = packageManager.getInstallerPackageName(str);
        } catch (IllegalArgumentException e) {
            this.zzj.zzab().zzgk().zza("Error retrieving installer package name. appId", zzef.zzam(str));
        }
        if (str5 == null) {
            str5 = "manual_install";
        } else if ("com.android.vending".equals(str5)) {
            str5 = "";
        }
        try {
            PackageInfo packageInfo = Wrappers.packageManager(context).getPackageInfo(str, 0);
            if (packageInfo != null) {
                CharSequence applicationLabel = Wrappers.packageManager(context).getApplicationLabel(str);
                str4 = !TextUtils.isEmpty(applicationLabel) ? applicationLabel.toString() : AnalyticsEvents.PARAMETER_DIALOG_OUTCOME_VALUE_UNKNOWN;
                try {
                    str6 = packageInfo.versionName;
                    i = packageInfo.versionCode;
                } catch (NameNotFoundException e2) {
                    this.zzj.zzab().zzgk().zza("Error retrieving newly installed package info. appId, appName", zzef.zzam(str), str4);
                    return null;
                }
            }
            this.zzj.zzae();
            long j2 = 0;
            if (this.zzj.zzad().zzr(str)) {
                j2 = j;
            }
            return new zzn(str, str2, str6, (long) i, str5, this.zzj.zzad().zzao(), this.zzj.zzz().zzc(context, str), (String) null, z, false, "", 0, j2, 0, z2, z3, false, str3, (Boolean) null, 0, null);
        } catch (NameNotFoundException e3) {
            str4 = AnalyticsEvents.PARAMETER_DIALOG_OUTCOME_VALUE_UNKNOWN;
            this.zzj.zzab().zzgk().zza("Error retrieving newly installed package info. appId, appName", zzef.zzam(str), str4);
            return null;
        }
    }

    @VisibleForTesting
    private static void zza(com.google.android.gms.internal.measurement.zzbs.zzc.zza zza2, int i, String str) {
        List zzmj = zza2.zzmj();
        int i2 = 0;
        while (true) {
            int i3 = i2;
            if (i3 >= zzmj.size()) {
                zze zze = (zze) ((zzey) zze.zzng().zzbz("_ev").zzca(str).zzug());
                zza2.zza((zze) ((zzey) zze.zzng().zzbz("_err").zzam(Long.valueOf((long) i).longValue()).zzug())).zza(zze);
                return;
            } else if (!"_err".equals(((zze) zzmj.get(i3)).getName())) {
                i2 = i3 + 1;
            } else {
                return;
            }
        }
    }

    @VisibleForTesting
    private static void zza(com.google.android.gms.internal.measurement.zzbs.zzc.zza zza2, @NonNull String str) {
        List zzmj = zza2.zzmj();
        int i = 0;
        while (true) {
            int i2 = i;
            if (i2 >= zzmj.size()) {
                return;
            }
            if (str.equals(((zze) zzmj.get(i2)).getName())) {
                zza2.zzm(i2);
                return;
            }
            i = i2 + 1;
        }
    }

    @VisibleForTesting
    private final void zza(com.google.android.gms.internal.measurement.zzbs.zzg.zza zza2, long j, boolean z) {
        boolean z2 = false;
        String str = "_lte";
        if (z) {
            str = "_se";
        }
        zzjp zze = zzgy().zze(zza2.zzag(), str);
        zzjp zzjp = (zze == null || zze.value == null) ? new zzjp(zza2.zzag(), "auto", str, this.zzj.zzx().currentTimeMillis(), Long.valueOf(j)) : new zzjp(zza2.zzag(), "auto", str, this.zzj.zzx().currentTimeMillis(), Long.valueOf(((Long) zze.value).longValue() + j));
        zzk zzk = (zzk) ((zzey) zzk.zzqu().zzdb(str).zzbk(this.zzj.zzx().currentTimeMillis()).zzbl(((Long) zzjp.value).longValue()).zzug());
        int i = 0;
        while (true) {
            if (i >= zza2.zznp()) {
                break;
            } else if (str.equals(zza2.zzs(i).getName())) {
                zza2.zza(i, zzk);
                z2 = true;
                break;
            } else {
                i++;
            }
        }
        if (!z2) {
            zza2.zza(zzk);
        }
        if (j > 0) {
            zzgy().zza(zzjp);
            String str2 = "lifetime";
            if (z) {
                str2 = "session-scoped";
            }
            this.zzj.zzab().zzgr().zza("Updated engagement user property. scope, value", str2, zzjp.value);
        }
    }

    private static void zza(zzjh zzjh) {
        if (zzjh == null) {
            throw new IllegalStateException("Upload Component not created");
        } else if (!zzjh.isInitialized()) {
            String valueOf = String.valueOf(zzjh.getClass());
            throw new IllegalStateException(new StringBuilder(String.valueOf(valueOf).length() + 27).append("Component not initialized: ").append(valueOf).toString());
        }
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final void zza(zzjm zzjm) {
        this.zzj.zzaa().zzo();
        zzx zzx = new zzx(this);
        zzx.initialize();
        this.zzsq = zzx;
        this.zzj.zzad().zza((zzu) this.zzso);
        zzp zzp = new zzp(this);
        zzp.initialize();
        this.zzst = zzp;
        zzhp zzhp = new zzhp(this);
        zzhp.initialize();
        this.zzsv = zzhp;
        zzjc zzjc = new zzjc(this);
        zzjc.initialize();
        this.zzss = zzjc;
        this.zzsr = new zzem(this);
        if (this.zzta != this.zztb) {
            this.zzj.zzab().zzgk().zza("Not all upload components initialized", Integer.valueOf(this.zzta), Integer.valueOf(this.zztb));
        }
        this.zzdh = true;
    }

    @WorkerThread
    @VisibleForTesting
    private final boolean zza(int i, FileChannel fileChannel) {
        zzo();
        if (fileChannel == null || !fileChannel.isOpen()) {
            this.zzj.zzab().zzgk().zzao("Bad channel to read from");
            return false;
        }
        ByteBuffer allocate = ByteBuffer.allocate(4);
        allocate.putInt(i);
        allocate.flip();
        try {
            fileChannel.truncate(0);
            fileChannel.write(allocate);
            fileChannel.force(true);
            if (fileChannel.size() == 4) {
                return true;
            }
            this.zzj.zzab().zzgk().zza("Error writing to channel. Bytes written", Long.valueOf(fileChannel.size()));
            return true;
        } catch (IOException e) {
            this.zzj.zzab().zzgk().zza("Failed to write to channel", e);
            return false;
        }
    }

    private final boolean zza(com.google.android.gms.internal.measurement.zzbs.zzc.zza zza2, com.google.android.gms.internal.measurement.zzbs.zzc.zza zza3) {
        String str = null;
        Preconditions.checkArgument("_e".equals(zza2.getName()));
        zzgw();
        zze zza4 = zzjo.zza((zzc) ((zzey) zza2.zzug()), "_sc");
        String zzmy = zza4 == null ? null : zza4.zzmy();
        zzgw();
        zze zza5 = zzjo.zza((zzc) ((zzey) zza3.zzug()), "_pc");
        if (zza5 != null) {
            str = zza5.zzmy();
        }
        if (str == null || !str.equals(zzmy)) {
            return false;
        }
        zzgw();
        zze zza6 = zzjo.zza((zzc) ((zzey) zza2.zzug()), "_et");
        if (!zza6.zzna() || zza6.zznb() <= 0) {
            return true;
        }
        long zznb = zza6.zznb();
        zzgw();
        zze zza7 = zzjo.zza((zzc) ((zzey) zza3.zzug()), "_et");
        long j = (zza7 == null || zza7.zznb() <= 0) ? zznb : zza7.zznb() + zznb;
        zzgw();
        zzjo.zza(zza3, "_et", (Object) Long.valueOf(j));
        zzgw();
        zzjo.zza(zza2, "_fr", (Object) Long.valueOf(1));
        return true;
    }

    @WorkerThread
    private final void zzb(zzf zzf) {
        ArrayMap arrayMap;
        zzo();
        if (!TextUtils.isEmpty(zzf.getGmpAppId()) || (zzs.zzbx() && !TextUtils.isEmpty(zzf.zzah()))) {
            zzs zzad = this.zzj.zzad();
            Builder builder = new Builder();
            String gmpAppId = zzf.getGmpAppId();
            String str = (!TextUtils.isEmpty(gmpAppId) || !zzs.zzbx()) ? gmpAppId : zzf.zzah();
            Builder encodedAuthority = builder.scheme((String) zzak.zzgj.get(null)).encodedAuthority((String) zzak.zzgk.get(null));
            String valueOf = String.valueOf(str);
            encodedAuthority.path(valueOf.length() != 0 ? "config/app/".concat(valueOf) : new String("config/app/")).appendQueryParameter("app_instance_id", zzf.getAppInstanceId()).appendQueryParameter("platform", "android").appendQueryParameter("gmp_version", String.valueOf(zzad.zzao()));
            String uri = builder.build().toString();
            try {
                URL url = new URL(uri);
                this.zzj.zzab().zzgs().zza("Fetching remote configuration", zzf.zzag());
                zzbw zzaw = zzgz().zzaw(zzf.zzag());
                String zzax = zzgz().zzax(zzf.zzag());
                if (zzaw == null || TextUtils.isEmpty(zzax)) {
                    arrayMap = null;
                } else {
                    arrayMap = new ArrayMap();
                    arrayMap.put("If-Modified-Since", zzax);
                }
                this.zztc = true;
                zzej zzjf = zzjf();
                String zzag = zzf.zzag();
                zzjl zzjl = new zzjl(this);
                zzjf.zzo();
                zzjf.zzbi();
                Preconditions.checkNotNull(url);
                Preconditions.checkNotNull(zzjl);
                zzjf.zzaa().zzb((Runnable) new zzen(zzjf, zzag, url, null, arrayMap, zzjl));
            } catch (MalformedURLException e) {
                this.zzj.zzab().zzgk().zza("Failed to parse config URL. Not fetching. appId", zzef.zzam(zzf.zzag()), uri);
            }
        } else {
            zzb(zzf.zzag(), 204, null, null, null);
        }
    }

    @WorkerThread
    private final zzn zzbi(String str) {
        zzf zzab = zzgy().zzab(str);
        if (zzab == null || TextUtils.isEmpty(zzab.zzal())) {
            this.zzj.zzab().zzgr().zza("No app data available; dropping", str);
            return null;
        }
        Boolean zzc = zzc(zzab);
        if (zzc == null || zzc.booleanValue()) {
            return new zzn(str, zzab.getGmpAppId(), zzab.zzal(), zzab.zzam(), zzab.zzan(), zzab.zzao(), zzab.zzap(), (String) null, zzab.isMeasurementEnabled(), false, zzab.getFirebaseInstanceId(), zzab.zzbd(), 0, 0, zzab.zzbe(), zzab.zzbf(), false, zzab.zzah(), zzab.zzbg(), zzab.zzaq(), zzab.zzbh());
        }
        this.zzj.zzab().zzgk().zza("App version does not match; dropping. appId", zzef.zzam(str));
        return null;
    }

    @WorkerThread
    private final Boolean zzc(zzf zzf) {
        try {
            if (zzf.zzam() != -2147483648L) {
                if (zzf.zzam() == ((long) Wrappers.packageManager(this.zzj.getContext()).getPackageInfo(zzf.zzag(), 0).versionCode)) {
                    return Boolean.valueOf(true);
                }
            } else {
                String str = Wrappers.packageManager(this.zzj.getContext()).getPackageInfo(zzf.zzag(), 0).versionName;
                if (zzf.zzal() != null && zzf.zzal().equals(str)) {
                    return Boolean.valueOf(true);
                }
            }
            return Boolean.valueOf(false);
        } catch (NameNotFoundException e) {
            return null;
        }
    }

    /* JADX WARNING: Removed duplicated region for block: B:63:0x0269 A[Catch:{ SQLiteException -> 0x02b5, all -> 0x02ac }] */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void zzd(com.google.android.gms.measurement.internal.zzai r31, com.google.android.gms.measurement.internal.zzn r32) {
        /*
            r30 = this;
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r32)
            r0 = r32
            java.lang.String r2 = r0.packageName
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r2)
            long r28 = java.lang.System.nanoTime()
            r30.zzo()
            r30.zzjj()
            r0 = r32
            java.lang.String r3 = r0.packageName
            com.google.android.gms.measurement.internal.zzjo r2 = r30.zzgw()
            r0 = r31
            r1 = r32
            boolean r2 = r2.zze(r0, r1)
            if (r2 != 0) goto L_0x0027
        L_0x0026:
            return
        L_0x0027:
            r0 = r32
            boolean r2 = r0.zzcq
            if (r2 != 0) goto L_0x0035
            r0 = r30
            r1 = r32
            r0.zzg(r1)
            goto L_0x0026
        L_0x0035:
            com.google.android.gms.measurement.internal.zzfd r2 = r30.zzgz()
            r0 = r31
            java.lang.String r4 = r0.name
            boolean r2 = r2.zzk(r3, r4)
            if (r2 == 0) goto L_0x00f6
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgn()
            java.lang.String r4 = "Dropping blacklisted event. appId"
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r3)
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r6 = r0.zzj
            com.google.android.gms.measurement.internal.zzed r6 = r6.zzy()
            r0 = r31
            java.lang.String r7 = r0.name
            java.lang.String r6 = r6.zzaj(r7)
            r2.zza(r4, r5, r6)
            com.google.android.gms.measurement.internal.zzfd r2 = r30.zzgz()
            boolean r2 = r2.zzbc(r3)
            if (r2 != 0) goto L_0x007c
            com.google.android.gms.measurement.internal.zzfd r2 = r30.zzgz()
            boolean r2 = r2.zzbd(r3)
            if (r2 == 0) goto L_0x00f3
        L_0x007c:
            r2 = 1
            r8 = r2
        L_0x007e:
            if (r8 != 0) goto L_0x00a0
            java.lang.String r2 = "_err"
            r0 = r31
            java.lang.String r4 = r0.name
            boolean r2 = r2.equals(r4)
            if (r2 != 0) goto L_0x00a0
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            com.google.android.gms.measurement.internal.zzjs r2 = r2.zzz()
            r4 = 11
            java.lang.String r5 = "_ev"
            r0 = r31
            java.lang.String r6 = r0.name
            r7 = 0
            r2.zza(r3, r4, r5, r6, r7)
        L_0x00a0:
            if (r8 == 0) goto L_0x0026
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()
            com.google.android.gms.measurement.internal.zzf r3 = r2.zzab(r3)
            if (r3 == 0) goto L_0x0026
            long r4 = r3.zzat()
            long r6 = r3.zzas()
            long r4 = java.lang.Math.max(r4, r6)
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            com.google.android.gms.common.util.Clock r2 = r2.zzx()
            long r6 = r2.currentTimeMillis()
            long r4 = r6 - r4
            long r4 = java.lang.Math.abs(r4)
            com.google.android.gms.measurement.internal.zzdu<java.lang.Long> r2 = com.google.android.gms.measurement.internal.zzak.zzhe
            r6 = 0
            java.lang.Object r2 = r2.get(r6)
            java.lang.Long r2 = (java.lang.Long) r2
            long r6 = r2.longValue()
            int r2 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r2 <= 0) goto L_0x0026
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgr()
            java.lang.String r4 = "Fetching config for blacklisted app"
            r2.zzao(r4)
            r0 = r30
            r0.zzb(r3)
            goto L_0x0026
        L_0x00f3:
            r2 = 0
            r8 = r2
            goto L_0x007e
        L_0x00f6:
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()
            r4 = 2
            boolean r2 = r2.isLoggable(r4)
            if (r2 == 0) goto L_0x0124
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgs()
            java.lang.String r4 = "Logging event"
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r5 = r0.zzj
            com.google.android.gms.measurement.internal.zzed r5 = r5.zzy()
            r0 = r31
            java.lang.String r5 = r5.zzb(r0)
            r2.zza(r4, r5)
        L_0x0124:
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()
            r2.beginTransaction()
            r0 = r30
            r1 = r32
            r0.zzg(r1)     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = "_iap"
            r0 = r31
            java.lang.String r4 = r0.name     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.equals(r4)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x014a
            java.lang.String r2 = "ecommerce_purchase"
            r0 = r31
            java.lang.String r4 = r0.name     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.equals(r4)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x02ed
        L_0x014a:
            r0 = r31
            com.google.android.gms.measurement.internal.zzah r2 = r0.zzfq     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = "currency"
            java.lang.String r2 = r2.getString(r4)     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = "ecommerce_purchase"
            r0 = r31
            java.lang.String r5 = r0.name     // Catch:{ all -> 0x02ac }
            boolean r4 = r4.equals(r5)     // Catch:{ all -> 0x02ac }
            if (r4 == 0) goto L_0x0294
            r0 = r31
            com.google.android.gms.measurement.internal.zzah r4 = r0.zzfq     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "value"
            java.lang.Double r4 = r4.zzah(r5)     // Catch:{ all -> 0x02ac }
            double r4 = r4.doubleValue()     // Catch:{ all -> 0x02ac }
            r6 = 4696837146684686336(0x412e848000000000, double:1000000.0)
            double r4 = r4 * r6
            r6 = 0
            int r6 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r6 != 0) goto L_0x018f
            r0 = r31
            com.google.android.gms.measurement.internal.zzah r4 = r0.zzfq     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "value"
            java.lang.Long r4 = r4.getLong(r5)     // Catch:{ all -> 0x02ac }
            long r4 = r4.longValue()     // Catch:{ all -> 0x02ac }
            double r4 = (double) r4     // Catch:{ all -> 0x02ac }
            r6 = 4696837146684686336(0x412e848000000000, double:1000000.0)
            double r4 = r4 * r6
        L_0x018f:
            r6 = 4890909195324358656(0x43e0000000000000, double:9.223372036854776E18)
            int r6 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r6 > 0) goto L_0x0279
            r6 = -4332462841530417152(0xc3e0000000000000, double:-9.223372036854776E18)
            int r6 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r6 < 0) goto L_0x0279
            long r4 = java.lang.Math.round(r4)     // Catch:{ all -> 0x02ac }
            r8 = r4
        L_0x01a0:
            boolean r4 = android.text.TextUtils.isEmpty(r2)     // Catch:{ all -> 0x02ac }
            if (r4 != 0) goto L_0x0266
            java.util.Locale r4 = java.util.Locale.US     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = r2.toUpperCase(r4)     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = "[A-Z]{3}"
            boolean r4 = r2.matches(r4)     // Catch:{ all -> 0x02ac }
            if (r4 == 0) goto L_0x0266
            java.lang.String r4 = "_ltv_"
            java.lang.String r4 = java.lang.String.valueOf(r4)     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = java.lang.String.valueOf(r2)     // Catch:{ all -> 0x02ac }
            int r5 = r2.length()     // Catch:{ all -> 0x02ac }
            if (r5 == 0) goto L_0x02a5
            java.lang.String r5 = r4.concat(r2)     // Catch:{ all -> 0x02ac }
        L_0x01c8:
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjp r2 = r2.zze(r3, r5)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x01d8
            java.lang.Object r4 = r2.value     // Catch:{ all -> 0x02ac }
            boolean r4 = r4 instanceof java.lang.Long     // Catch:{ all -> 0x02ac }
            if (r4 != 0) goto L_0x02c9
        L_0x01d8:
            com.google.android.gms.measurement.internal.zzx r4 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzs r2 = r2.zzad()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Integer> r6 = com.google.android.gms.measurement.internal.zzak.zzhj     // Catch:{ all -> 0x02ac }
            int r2 = r2.zzb(r3, r6)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r3)     // Catch:{ all -> 0x02ac }
            r4.zzo()     // Catch:{ all -> 0x02ac }
            r4.zzbi()     // Catch:{ all -> 0x02ac }
            android.database.sqlite.SQLiteDatabase r6 = r4.getWritableDatabase()     // Catch:{ SQLiteException -> 0x02b5 }
            java.lang.String r7 = "delete from user_attributes where app_id=? and name in (select name from user_attributes where app_id=? and name like '_ltv_%' order by set_timestamp desc limit ?,10);"
            r10 = 3
            java.lang.String[] r10 = new java.lang.String[r10]     // Catch:{ SQLiteException -> 0x02b5 }
            r11 = 0
            r10[r11] = r3     // Catch:{ SQLiteException -> 0x02b5 }
            r11 = 1
            r10[r11] = r3     // Catch:{ SQLiteException -> 0x02b5 }
            r11 = 2
            int r2 = r2 + -1
            java.lang.String r2 = java.lang.String.valueOf(r2)     // Catch:{ SQLiteException -> 0x02b5 }
            r10[r11] = r2     // Catch:{ SQLiteException -> 0x02b5 }
            r6.execSQL(r7, r10)     // Catch:{ SQLiteException -> 0x02b5 }
        L_0x020e:
            com.google.android.gms.measurement.internal.zzjp r2 = new com.google.android.gms.measurement.internal.zzjp     // Catch:{ all -> 0x02ac }
            r0 = r31
            java.lang.String r4 = r0.origin     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r6 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.common.util.Clock r6 = r6.zzx()     // Catch:{ all -> 0x02ac }
            long r6 = r6.currentTimeMillis()     // Catch:{ all -> 0x02ac }
            java.lang.Long r8 = java.lang.Long.valueOf(r8)     // Catch:{ all -> 0x02ac }
            r2.<init>(r3, r4, r5, r6, r8)     // Catch:{ all -> 0x02ac }
        L_0x0227:
            com.google.android.gms.measurement.internal.zzx r4 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            boolean r4 = r4.zza(r2)     // Catch:{ all -> 0x02ac }
            if (r4 != 0) goto L_0x0266
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "Too many unique user properties are set. Ignoring user property. appId"
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r3)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r7 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzed r7 = r7.zzy()     // Catch:{ all -> 0x02ac }
            java.lang.String r8 = r2.name     // Catch:{ all -> 0x02ac }
            java.lang.String r7 = r7.zzal(r8)     // Catch:{ all -> 0x02ac }
            java.lang.Object r2 = r2.value     // Catch:{ all -> 0x02ac }
            r4.zza(r5, r6, r7, r2)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjs r2 = r2.zzz()     // Catch:{ all -> 0x02ac }
            r4 = 9
            r5 = 0
            r6 = 0
            r7 = 0
            r2.zza(r3, r4, r5, r6, r7)     // Catch:{ all -> 0x02ac }
        L_0x0266:
            r2 = 1
        L_0x0267:
            if (r2 != 0) goto L_0x02ed
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r2.setTransactionSuccessful()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()
            r2.endTransaction()
            goto L_0x0026
        L_0x0279:
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgn()     // Catch:{ all -> 0x02ac }
            java.lang.String r6 = "Data lost. Currency value is too big. appId"
            java.lang.Object r7 = com.google.android.gms.measurement.internal.zzef.zzam(r3)     // Catch:{ all -> 0x02ac }
            java.lang.Double r4 = java.lang.Double.valueOf(r4)     // Catch:{ all -> 0x02ac }
            r2.zza(r6, r7, r4)     // Catch:{ all -> 0x02ac }
            r2 = 0
            goto L_0x0267
        L_0x0294:
            r0 = r31
            com.google.android.gms.measurement.internal.zzah r4 = r0.zzfq     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "value"
            java.lang.Long r4 = r4.getLong(r5)     // Catch:{ all -> 0x02ac }
            long r4 = r4.longValue()     // Catch:{ all -> 0x02ac }
            r8 = r4
            goto L_0x01a0
        L_0x02a5:
            java.lang.String r5 = new java.lang.String     // Catch:{ all -> 0x02ac }
            r5.<init>(r4)     // Catch:{ all -> 0x02ac }
            goto L_0x01c8
        L_0x02ac:
            r2 = move-exception
            com.google.android.gms.measurement.internal.zzx r3 = r30.zzgy()
            r3.endTransaction()
            throw r2
        L_0x02b5:
            r2 = move-exception
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ all -> 0x02ac }
            java.lang.String r6 = "Error pruning currencies. appId"
            java.lang.Object r7 = com.google.android.gms.measurement.internal.zzef.zzam(r3)     // Catch:{ all -> 0x02ac }
            r4.zza(r6, r7, r2)     // Catch:{ all -> 0x02ac }
            goto L_0x020e
        L_0x02c9:
            java.lang.Object r2 = r2.value     // Catch:{ all -> 0x02ac }
            java.lang.Long r2 = (java.lang.Long) r2     // Catch:{ all -> 0x02ac }
            long r10 = r2.longValue()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjp r2 = new com.google.android.gms.measurement.internal.zzjp     // Catch:{ all -> 0x02ac }
            r0 = r31
            java.lang.String r4 = r0.origin     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r6 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.common.util.Clock r6 = r6.zzx()     // Catch:{ all -> 0x02ac }
            long r6 = r6.currentTimeMillis()     // Catch:{ all -> 0x02ac }
            long r8 = r8 + r10
            java.lang.Long r8 = java.lang.Long.valueOf(r8)     // Catch:{ all -> 0x02ac }
            r2.<init>(r3, r4, r5, r6, r8)     // Catch:{ all -> 0x02ac }
            goto L_0x0227
        L_0x02ed:
            r0 = r31
            java.lang.String r2 = r0.name     // Catch:{ all -> 0x02ac }
            boolean r10 = com.google.android.gms.measurement.internal.zzjs.zzbk(r2)     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = "_err"
            r0 = r31
            java.lang.String r4 = r0.name     // Catch:{ all -> 0x02ac }
            boolean r12 = r2.equals(r4)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzx r5 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            long r6 = r30.zzjk()     // Catch:{ all -> 0x02ac }
            r9 = 1
            r11 = 0
            r13 = 0
            r8 = r3
            com.google.android.gms.measurement.internal.zzw r4 = r5.zza(r6, r8, r9, r10, r11, r12, r13)     // Catch:{ all -> 0x02ac }
            long r6 = r4.zzeg     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Integer> r2 = com.google.android.gms.measurement.internal.zzak.zzgp     // Catch:{ all -> 0x02ac }
            r5 = 0
            java.lang.Object r2 = r2.get(r5)     // Catch:{ all -> 0x02ac }
            java.lang.Integer r2 = (java.lang.Integer) r2     // Catch:{ all -> 0x02ac }
            int r2 = r2.intValue()     // Catch:{ all -> 0x02ac }
            long r8 = (long) r2
            long r6 = r6 - r8
            r8 = 0
            int r2 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r2 <= 0) goto L_0x035a
            r8 = 1000(0x3e8, double:4.94E-321)
            long r6 = r6 % r8
            r8 = 1
            int r2 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r2 != 0) goto L_0x034a
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "Data loss. Too many events logged. appId, count"
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzef.zzam(r3)     // Catch:{ all -> 0x02ac }
            long r6 = r4.zzeg     // Catch:{ all -> 0x02ac }
            java.lang.Long r4 = java.lang.Long.valueOf(r6)     // Catch:{ all -> 0x02ac }
            r2.zza(r5, r3, r4)     // Catch:{ all -> 0x02ac }
        L_0x034a:
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r2.setTransactionSuccessful()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()
            r2.endTransaction()
            goto L_0x0026
        L_0x035a:
            if (r10 == 0) goto L_0x03bb
            long r6 = r4.zzef     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Integer> r2 = com.google.android.gms.measurement.internal.zzak.zzgr     // Catch:{ all -> 0x02ac }
            r5 = 0
            java.lang.Object r2 = r2.get(r5)     // Catch:{ all -> 0x02ac }
            java.lang.Integer r2 = (java.lang.Integer) r2     // Catch:{ all -> 0x02ac }
            int r2 = r2.intValue()     // Catch:{ all -> 0x02ac }
            long r8 = (long) r2
            long r6 = r6 - r8
            r8 = 0
            int r2 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r2 <= 0) goto L_0x03bb
            r8 = 1000(0x3e8, double:4.94E-321)
            long r6 = r6 % r8
            r8 = 1
            int r2 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r2 != 0) goto L_0x0397
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "Data loss. Too many public events logged. appId, count"
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r3)     // Catch:{ all -> 0x02ac }
            long r8 = r4.zzef     // Catch:{ all -> 0x02ac }
            java.lang.Long r4 = java.lang.Long.valueOf(r8)     // Catch:{ all -> 0x02ac }
            r2.zza(r5, r6, r4)     // Catch:{ all -> 0x02ac }
        L_0x0397:
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjs r2 = r2.zzz()     // Catch:{ all -> 0x02ac }
            r4 = 16
            java.lang.String r5 = "_ev"
            r0 = r31
            java.lang.String r6 = r0.name     // Catch:{ all -> 0x02ac }
            r7 = 0
            r2.zza(r3, r4, r5, r6, r7)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r2.setTransactionSuccessful()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()
            r2.endTransaction()
            goto L_0x0026
        L_0x03bb:
            if (r12 == 0) goto L_0x0416
            long r6 = r4.zzei     // Catch:{ all -> 0x02ac }
            r2 = 0
            r5 = 1000000(0xf4240, float:1.401298E-39)
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r8 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzs r8 = r8.zzad()     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r9 = r0.packageName     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Integer> r11 = com.google.android.gms.measurement.internal.zzak.zzgq     // Catch:{ all -> 0x02ac }
            int r8 = r8.zzb(r9, r11)     // Catch:{ all -> 0x02ac }
            int r5 = java.lang.Math.min(r5, r8)     // Catch:{ all -> 0x02ac }
            int r2 = java.lang.Math.max(r2, r5)     // Catch:{ all -> 0x02ac }
            long r8 = (long) r2     // Catch:{ all -> 0x02ac }
            long r6 = r6 - r8
            r8 = 0
            int r2 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r2 <= 0) goto L_0x0416
            r8 = 1
            int r2 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r2 != 0) goto L_0x0406
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "Too many error events logged. appId, count"
            java.lang.Object r3 = com.google.android.gms.measurement.internal.zzef.zzam(r3)     // Catch:{ all -> 0x02ac }
            long r6 = r4.zzei     // Catch:{ all -> 0x02ac }
            java.lang.Long r4 = java.lang.Long.valueOf(r6)     // Catch:{ all -> 0x02ac }
            r2.zza(r5, r3, r4)     // Catch:{ all -> 0x02ac }
        L_0x0406:
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r2.setTransactionSuccessful()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()
            r2.endTransaction()
            goto L_0x0026
        L_0x0416:
            r0 = r31
            com.google.android.gms.measurement.internal.zzah r2 = r0.zzfq     // Catch:{ all -> 0x02ac }
            android.os.Bundle r20 = r2.zzcv()     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjs r2 = r2.zzz()     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = "_o"
            r0 = r31
            java.lang.String r5 = r0.origin     // Catch:{ all -> 0x02ac }
            r0 = r20
            r2.zza(r0, r4, r5)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjs r2 = r2.zzz()     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.zzbr(r3)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x0469
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjs r2 = r2.zzz()     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = "_dbg"
            r6 = 1
            java.lang.Long r5 = java.lang.Long.valueOf(r6)     // Catch:{ all -> 0x02ac }
            r0 = r20
            r2.zza(r0, r4, r5)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjs r2 = r2.zzz()     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = "_r"
            r6 = 1
            java.lang.Long r5 = java.lang.Long.valueOf(r6)     // Catch:{ all -> 0x02ac }
            r0 = r20
            r2.zza(r0, r4, r5)     // Catch:{ all -> 0x02ac }
        L_0x0469:
            java.lang.String r2 = "_s"
            r0 = r31
            java.lang.String r4 = r0.name     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.equals(r4)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x04ae
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzs r2 = r2.zzad()     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r4 = r0.packageName     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.zzw(r4)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x04ae
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r4 = r0.packageName     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "_sno"
            com.google.android.gms.measurement.internal.zzjp r2 = r2.zze(r4, r5)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x04ae
            java.lang.Object r4 = r2.value     // Catch:{ all -> 0x02ac }
            boolean r4 = r4 instanceof java.lang.Long     // Catch:{ all -> 0x02ac }
            if (r4 == 0) goto L_0x04ae
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjs r4 = r4.zzz()     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "_sno"
            java.lang.Object r2 = r2.value     // Catch:{ all -> 0x02ac }
            r0 = r20
            r4.zza(r0, r5, r2)     // Catch:{ all -> 0x02ac }
        L_0x04ae:
            java.lang.String r2 = "_s"
            r0 = r31
            java.lang.String r4 = r0.name     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.equals(r4)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x04f1
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzs r2 = r2.zzad()     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r4 = r0.packageName     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r5 = com.google.android.gms.measurement.internal.zzak.zzif     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.zze(r4, r5)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x04f1
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzs r2 = r2.zzad()     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r4 = r0.packageName     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.zzw(r4)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x04f1
            com.google.android.gms.measurement.internal.zzjn r2 = new com.google.android.gms.measurement.internal.zzjn     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = "_sno"
            r6 = 0
            r5 = 0
            r2.<init>(r4, r6, r5)     // Catch:{ all -> 0x02ac }
            r0 = r30
            r1 = r32
            r0.zzc(r2, r1)     // Catch:{ all -> 0x02ac }
        L_0x04f1:
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            long r4 = r2.zzac(r3)     // Catch:{ all -> 0x02ac }
            r6 = 0
            int r2 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r2 <= 0) goto L_0x0518
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgn()     // Catch:{ all -> 0x02ac }
            java.lang.String r6 = "Data lost. Too many events stored on disk, deleted. appId"
            java.lang.Object r7 = com.google.android.gms.measurement.internal.zzef.zzam(r3)     // Catch:{ all -> 0x02ac }
            java.lang.Long r4 = java.lang.Long.valueOf(r4)     // Catch:{ all -> 0x02ac }
            r2.zza(r6, r7, r4)     // Catch:{ all -> 0x02ac }
        L_0x0518:
            com.google.android.gms.measurement.internal.zzaf r11 = new com.google.android.gms.measurement.internal.zzaf     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r12 = r0.zzj     // Catch:{ all -> 0x02ac }
            r0 = r31
            java.lang.String r13 = r0.origin     // Catch:{ all -> 0x02ac }
            r0 = r31
            java.lang.String r15 = r0.name     // Catch:{ all -> 0x02ac }
            r0 = r31
            long r0 = r0.zzfu     // Catch:{ all -> 0x02ac }
            r16 = r0
            r18 = 0
            r14 = r3
            r11.<init>(r12, r13, r14, r15, r16, r18, r20)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = r11.name     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzae r2 = r2.zzc(r3, r4)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x0842
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            long r4 = r2.zzag(r3)     // Catch:{ all -> 0x02ac }
            r6 = 500(0x1f4, double:2.47E-321)
            int r2 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r2 < 0) goto L_0x0590
            if (r10 == 0) goto L_0x0590
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgk()     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = "Too many event names used, ignoring event. appId, name, supported count"
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r3)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r6 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzed r6 = r6.zzy()     // Catch:{ all -> 0x02ac }
            java.lang.String r7 = r11.name     // Catch:{ all -> 0x02ac }
            java.lang.String r6 = r6.zzaj(r7)     // Catch:{ all -> 0x02ac }
            r7 = 500(0x1f4, float:7.0E-43)
            java.lang.Integer r7 = java.lang.Integer.valueOf(r7)     // Catch:{ all -> 0x02ac }
            r2.zza(r4, r5, r6, r7)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjs r2 = r2.zzz()     // Catch:{ all -> 0x02ac }
            r4 = 8
            r5 = 0
            r6 = 0
            r7 = 0
            r2.zza(r3, r4, r5, r6, r7)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()
            r2.endTransaction()
            goto L_0x0026
        L_0x0590:
            com.google.android.gms.measurement.internal.zzae r13 = new com.google.android.gms.measurement.internal.zzae     // Catch:{ all -> 0x02ac }
            java.lang.String r15 = r11.name     // Catch:{ all -> 0x02ac }
            r16 = 0
            r18 = 0
            long r0 = r11.timestamp     // Catch:{ all -> 0x02ac }
            r20 = r0
            r22 = 0
            r24 = 0
            r25 = 0
            r26 = 0
            r27 = 0
            r14 = r3
            r13.<init>(r14, r15, r16, r18, r20, r22, r24, r25, r26, r27)     // Catch:{ all -> 0x02ac }
            r12 = r11
        L_0x05ab:
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r2.zza(r13)     // Catch:{ all -> 0x02ac }
            r30.zzo()     // Catch:{ all -> 0x02ac }
            r30.zzjj()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r12)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r32)     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = r12.zzce     // Catch:{ all -> 0x02ac }
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r2)     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = r12.zzce     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r3 = r0.packageName     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.equals(r3)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.common.internal.Preconditions.checkArgument(r2)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r2 = com.google.android.gms.internal.measurement.zzbs.zzg.zzpr()     // Catch:{ all -> 0x02ac }
            r3 = 1
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r2 = r2.zzp(r3)     // Catch:{ all -> 0x02ac }
            java.lang.String r3 = "android"
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r4 = r2.zzcc(r3)     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r2 = r0.packageName     // Catch:{ all -> 0x02ac }
            boolean r2 = android.text.TextUtils.isEmpty(r2)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x05f0
            r0 = r32
            java.lang.String r2 = r0.packageName     // Catch:{ all -> 0x02ac }
            r4.zzch(r2)     // Catch:{ all -> 0x02ac }
        L_0x05f0:
            r0 = r32
            java.lang.String r2 = r0.zzco     // Catch:{ all -> 0x02ac }
            boolean r2 = android.text.TextUtils.isEmpty(r2)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x0601
            r0 = r32
            java.lang.String r2 = r0.zzco     // Catch:{ all -> 0x02ac }
            r4.zzcg(r2)     // Catch:{ all -> 0x02ac }
        L_0x0601:
            r0 = r32
            java.lang.String r2 = r0.zzcm     // Catch:{ all -> 0x02ac }
            boolean r2 = android.text.TextUtils.isEmpty(r2)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x0612
            r0 = r32
            java.lang.String r2 = r0.zzcm     // Catch:{ all -> 0x02ac }
            r4.zzci(r2)     // Catch:{ all -> 0x02ac }
        L_0x0612:
            r0 = r32
            long r2 = r0.zzcn     // Catch:{ all -> 0x02ac }
            r6 = -2147483648(0xffffffff80000000, double:NaN)
            int r2 = (r2 > r6 ? 1 : (r2 == r6 ? 0 : -1))
            if (r2 == 0) goto L_0x0625
            r0 = r32
            long r2 = r0.zzcn     // Catch:{ all -> 0x02ac }
            int r2 = (int) r2     // Catch:{ all -> 0x02ac }
            r4.zzv(r2)     // Catch:{ all -> 0x02ac }
        L_0x0625:
            r0 = r32
            long r2 = r0.zzr     // Catch:{ all -> 0x02ac }
            r4.zzas(r2)     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r2 = r0.zzcg     // Catch:{ all -> 0x02ac }
            boolean r2 = android.text.TextUtils.isEmpty(r2)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x063d
            r0 = r32
            java.lang.String r2 = r0.zzcg     // Catch:{ all -> 0x02ac }
            r4.zzcm(r2)     // Catch:{ all -> 0x02ac }
        L_0x063d:
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzs r2 = r2.zzad()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r3 = com.google.android.gms.measurement.internal.zzak.zzit     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.zza(r3)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x0855
            java.lang.String r2 = r4.getGmpAppId()     // Catch:{ all -> 0x02ac }
            boolean r2 = android.text.TextUtils.isEmpty(r2)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x0668
            r0 = r32
            java.lang.String r2 = r0.zzcu     // Catch:{ all -> 0x02ac }
            boolean r2 = android.text.TextUtils.isEmpty(r2)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x0668
            r0 = r32
            java.lang.String r2 = r0.zzcu     // Catch:{ all -> 0x02ac }
            r4.zzcq(r2)     // Catch:{ all -> 0x02ac }
        L_0x0668:
            r0 = r32
            long r2 = r0.zzcp     // Catch:{ all -> 0x02ac }
            r6 = 0
            int r2 = (r2 > r6 ? 1 : (r2 == r6 ? 0 : -1))
            if (r2 == 0) goto L_0x0679
            r0 = r32
            long r2 = r0.zzcp     // Catch:{ all -> 0x02ac }
            r4.zzau(r2)     // Catch:{ all -> 0x02ac }
        L_0x0679:
            r0 = r32
            long r2 = r0.zzs     // Catch:{ all -> 0x02ac }
            r4.zzax(r2)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzs r2 = r2.zzad()     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r3 = r0.packageName     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r5 = com.google.android.gms.measurement.internal.zzak.zzin     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.zze(r3, r5)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x06a1
            com.google.android.gms.measurement.internal.zzjo r2 = r30.zzgw()     // Catch:{ all -> 0x02ac }
            java.util.List r2 = r2.zzju()     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x06a1
            r4.zzd(r2)     // Catch:{ all -> 0x02ac }
        L_0x06a1:
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeo r2 = r2.zzac()     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r3 = r0.packageName     // Catch:{ all -> 0x02ac }
            android.util.Pair r3 = r2.zzap(r3)     // Catch:{ all -> 0x02ac }
            if (r3 == 0) goto L_0x0868
            java.lang.Object r2 = r3.first     // Catch:{ all -> 0x02ac }
            java.lang.CharSequence r2 = (java.lang.CharSequence) r2     // Catch:{ all -> 0x02ac }
            boolean r2 = android.text.TextUtils.isEmpty(r2)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x0868
            r0 = r32
            boolean r2 = r0.zzcs     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x06d9
            java.lang.Object r2 = r3.first     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = (java.lang.String) r2     // Catch:{ all -> 0x02ac }
            r4.zzcj(r2)     // Catch:{ all -> 0x02ac }
            java.lang.Object r2 = r3.second     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x06d9
            java.lang.Object r2 = r3.second     // Catch:{ all -> 0x02ac }
            java.lang.Boolean r2 = (java.lang.Boolean) r2     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.booleanValue()     // Catch:{ all -> 0x02ac }
            r4.zzm(r2)     // Catch:{ all -> 0x02ac }
        L_0x06d9:
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzac r2 = r2.zzw()     // Catch:{ all -> 0x02ac }
            r2.zzbi()     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = android.os.Build.MODEL     // Catch:{ all -> 0x02ac }
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r2 = r4.zzce(r2)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r3 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzac r3 = r3.zzw()     // Catch:{ all -> 0x02ac }
            r3.zzbi()     // Catch:{ all -> 0x02ac }
            java.lang.String r3 = android.os.Build.VERSION.RELEASE     // Catch:{ all -> 0x02ac }
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r2 = r2.zzcd(r3)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r3 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzac r3 = r3.zzw()     // Catch:{ all -> 0x02ac }
            long r6 = r3.zzcq()     // Catch:{ all -> 0x02ac }
            int r3 = (int) r6     // Catch:{ all -> 0x02ac }
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r2 = r2.zzt(r3)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r3 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzac r3 = r3.zzw()     // Catch:{ all -> 0x02ac }
            java.lang.String r3 = r3.zzcr()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r2 = r2.zzcf(r3)     // Catch:{ all -> 0x02ac }
            r0 = r32
            long r6 = r0.zzcr     // Catch:{ all -> 0x02ac }
            r2.zzaw(r6)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.isEnabled()     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x0741
            boolean r2 = com.google.android.gms.measurement.internal.zzs.zzbv()     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x0741
            r4.zzag()     // Catch:{ all -> 0x02ac }
            r2 = 0
            boolean r2 = android.text.TextUtils.isEmpty(r2)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x0741
            r2 = 0
            r4.zzcp(r2)     // Catch:{ all -> 0x02ac }
        L_0x0741:
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r3 = r0.packageName     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzf r2 = r2.zzab(r3)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x07da
            com.google.android.gms.measurement.internal.zzf r2 = new com.google.android.gms.measurement.internal.zzf     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r3 = r0.zzj     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r5 = r0.packageName     // Catch:{ all -> 0x02ac }
            r2.<init>(r3, r5)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r3 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjs r3 = r3.zzz()     // Catch:{ all -> 0x02ac }
            java.lang.String r3 = r3.zzjy()     // Catch:{ all -> 0x02ac }
            r2.zza(r3)     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r3 = r0.zzci     // Catch:{ all -> 0x02ac }
            r2.zze(r3)     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r3 = r0.zzcg     // Catch:{ all -> 0x02ac }
            r2.zzb(r3)     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r3 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeo r3 = r3.zzac()     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r5 = r0.packageName     // Catch:{ all -> 0x02ac }
            java.lang.String r3 = r3.zzaq(r5)     // Catch:{ all -> 0x02ac }
            r2.zzd(r3)     // Catch:{ all -> 0x02ac }
            r6 = 0
            r2.zzk(r6)     // Catch:{ all -> 0x02ac }
            r6 = 0
            r2.zze(r6)     // Catch:{ all -> 0x02ac }
            r6 = 0
            r2.zzf(r6)     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r3 = r0.zzcm     // Catch:{ all -> 0x02ac }
            r2.zzf(r3)     // Catch:{ all -> 0x02ac }
            r0 = r32
            long r6 = r0.zzcn     // Catch:{ all -> 0x02ac }
            r2.zzg(r6)     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r3 = r0.zzco     // Catch:{ all -> 0x02ac }
            r2.zzg(r3)     // Catch:{ all -> 0x02ac }
            r0 = r32
            long r6 = r0.zzr     // Catch:{ all -> 0x02ac }
            r2.zzh(r6)     // Catch:{ all -> 0x02ac }
            r0 = r32
            long r6 = r0.zzcp     // Catch:{ all -> 0x02ac }
            r2.zzi(r6)     // Catch:{ all -> 0x02ac }
            r0 = r32
            boolean r3 = r0.zzcq     // Catch:{ all -> 0x02ac }
            r2.setMeasurementEnabled(r3)     // Catch:{ all -> 0x02ac }
            r0 = r32
            long r6 = r0.zzcr     // Catch:{ all -> 0x02ac }
            r2.zzt(r6)     // Catch:{ all -> 0x02ac }
            r0 = r32
            long r6 = r0.zzs     // Catch:{ all -> 0x02ac }
            r2.zzj(r6)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzx r3 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r3.zza(r2)     // Catch:{ all -> 0x02ac }
        L_0x07da:
            java.lang.String r3 = r2.getAppInstanceId()     // Catch:{ all -> 0x02ac }
            boolean r3 = android.text.TextUtils.isEmpty(r3)     // Catch:{ all -> 0x02ac }
            if (r3 != 0) goto L_0x07eb
            java.lang.String r3 = r2.getAppInstanceId()     // Catch:{ all -> 0x02ac }
            r4.zzck(r3)     // Catch:{ all -> 0x02ac }
        L_0x07eb:
            java.lang.String r3 = r2.getFirebaseInstanceId()     // Catch:{ all -> 0x02ac }
            boolean r3 = android.text.TextUtils.isEmpty(r3)     // Catch:{ all -> 0x02ac }
            if (r3 != 0) goto L_0x07fc
            java.lang.String r2 = r2.getFirebaseInstanceId()     // Catch:{ all -> 0x02ac }
            r4.zzcn(r2)     // Catch:{ all -> 0x02ac }
        L_0x07fc:
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r0 = r32
            java.lang.String r3 = r0.packageName     // Catch:{ all -> 0x02ac }
            java.util.List r5 = r2.zzaa(r3)     // Catch:{ all -> 0x02ac }
            r2 = 0
            r3 = r2
        L_0x080a:
            int r2 = r5.size()     // Catch:{ all -> 0x02ac }
            if (r3 >= r2) goto L_0x08d8
            com.google.android.gms.internal.measurement.zzbs$zzk$zza r6 = com.google.android.gms.internal.measurement.zzbs.zzk.zzqu()     // Catch:{ all -> 0x02ac }
            java.lang.Object r2 = r5.get(r3)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjp r2 = (com.google.android.gms.measurement.internal.zzjp) r2     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = r2.name     // Catch:{ all -> 0x02ac }
            com.google.android.gms.internal.measurement.zzbs$zzk$zza r6 = r6.zzdb(r2)     // Catch:{ all -> 0x02ac }
            java.lang.Object r2 = r5.get(r3)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjp r2 = (com.google.android.gms.measurement.internal.zzjp) r2     // Catch:{ all -> 0x02ac }
            long r8 = r2.zztr     // Catch:{ all -> 0x02ac }
            com.google.android.gms.internal.measurement.zzbs$zzk$zza r6 = r6.zzbk(r8)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjo r7 = r30.zzgw()     // Catch:{ all -> 0x02ac }
            java.lang.Object r2 = r5.get(r3)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzjp r2 = (com.google.android.gms.measurement.internal.zzjp) r2     // Catch:{ all -> 0x02ac }
            java.lang.Object r2 = r2.value     // Catch:{ all -> 0x02ac }
            r7.zza(r6, r2)     // Catch:{ all -> 0x02ac }
            r4.zza(r6)     // Catch:{ all -> 0x02ac }
            int r2 = r3 + 1
            r3 = r2
            goto L_0x080a
        L_0x0842:
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r3 = r0.zzj     // Catch:{ all -> 0x02ac }
            long r4 = r2.zzfj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzaf r11 = r11.zza(r3, r4)     // Catch:{ all -> 0x02ac }
            long r4 = r11.timestamp     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzae r13 = r2.zzw(r4)     // Catch:{ all -> 0x02ac }
            r12 = r11
            goto L_0x05ab
        L_0x0855:
            r0 = r32
            java.lang.String r2 = r0.zzcu     // Catch:{ all -> 0x02ac }
            boolean r2 = android.text.TextUtils.isEmpty(r2)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x0668
            r0 = r32
            java.lang.String r2 = r0.zzcu     // Catch:{ all -> 0x02ac }
            r4.zzcq(r2)     // Catch:{ all -> 0x02ac }
            goto L_0x0668
        L_0x0868:
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzac r2 = r2.zzw()     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r3 = r0.zzj     // Catch:{ all -> 0x02ac }
            android.content.Context r3 = r3.getContext()     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.zzj(r3)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x06d9
            r0 = r32
            boolean r2 = r0.zzct     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x06d9
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            android.content.Context r2 = r2.getContext()     // Catch:{ all -> 0x02ac }
            android.content.ContentResolver r2 = r2.getContentResolver()     // Catch:{ all -> 0x02ac }
            java.lang.String r3 = "android_id"
            java.lang.String r2 = android.provider.Settings.Secure.getString(r2, r3)     // Catch:{ all -> 0x02ac }
            if (r2 != 0) goto L_0x08b8
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgn()     // Catch:{ all -> 0x02ac }
            java.lang.String r3 = "null secure ID. appId"
            java.lang.String r5 = r4.zzag()     // Catch:{ all -> 0x02ac }
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzef.zzam(r5)     // Catch:{ all -> 0x02ac }
            r2.zza(r3, r5)     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = "null"
        L_0x08b3:
            r4.zzco(r2)     // Catch:{ all -> 0x02ac }
            goto L_0x06d9
        L_0x08b8:
            boolean r3 = r2.isEmpty()     // Catch:{ all -> 0x02ac }
            if (r3 == 0) goto L_0x08b3
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r3 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r3 = r3.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgn()     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "empty secure ID. appId"
            java.lang.String r6 = r4.zzag()     // Catch:{ all -> 0x02ac }
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r6)     // Catch:{ all -> 0x02ac }
            r3.zza(r5, r6)     // Catch:{ all -> 0x02ac }
            goto L_0x08b3
        L_0x08d8:
            com.google.android.gms.measurement.internal.zzx r3 = r30.zzgy()     // Catch:{ IOException -> 0x0979 }
            com.google.android.gms.internal.measurement.zzgi r2 = r4.zzug()     // Catch:{ IOException -> 0x0979 }
            com.google.android.gms.internal.measurement.zzey r2 = (com.google.android.gms.internal.measurement.zzey) r2     // Catch:{ IOException -> 0x0979 }
            com.google.android.gms.internal.measurement.zzbs$zzg r2 = (com.google.android.gms.internal.measurement.zzbs.zzg) r2     // Catch:{ IOException -> 0x0979 }
            long r14 = r3.zza(r2)     // Catch:{ IOException -> 0x0979 }
            com.google.android.gms.measurement.internal.zzx r13 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzah r2 = r12.zzfq     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x09cd
            com.google.android.gms.measurement.internal.zzah r2 = r12.zzfq     // Catch:{ all -> 0x02ac }
            java.util.Iterator r3 = r2.iterator()     // Catch:{ all -> 0x02ac }
        L_0x08f6:
            boolean r2 = r3.hasNext()     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x0994
            java.lang.String r4 = "_r"
            java.lang.Object r2 = r3.next()     // Catch:{ all -> 0x02ac }
            java.lang.String r2 = (java.lang.String) r2     // Catch:{ all -> 0x02ac }
            boolean r2 = r4.equals(r2)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x08f6
            r2 = 1
        L_0x090b:
            boolean r2 = r13.zza(r12, r14, r2)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x0917
            r2 = 0
            r0 = r30
            r0.zzsy = r2     // Catch:{ all -> 0x02ac }
        L_0x0917:
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            r2.setTransactionSuccessful()     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ all -> 0x02ac }
            r3 = 2
            boolean r2 = r2.isLoggable(r3)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x094a
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgs()     // Catch:{ all -> 0x02ac }
            java.lang.String r3 = "Event recorded"
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzed r4 = r4.zzy()     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = r4.zza(r12)     // Catch:{ all -> 0x02ac }
            r2.zza(r3, r4)     // Catch:{ all -> 0x02ac }
        L_0x094a:
            com.google.android.gms.measurement.internal.zzx r2 = r30.zzgy()
            r2.endTransaction()
            r30.zzjn()
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r2 = r0.zzj
            com.google.android.gms.measurement.internal.zzef r2 = r2.zzab()
            com.google.android.gms.measurement.internal.zzeh r2 = r2.zzgs()
            java.lang.String r3 = "Background event processing time, ms"
            long r4 = java.lang.System.nanoTime()
            long r4 = r4 - r28
            r6 = 500000(0x7a120, double:2.47033E-318)
            long r4 = r4 + r6
            r6 = 1000000(0xf4240, double:4.940656E-318)
            long r4 = r4 / r6
            java.lang.Long r4 = java.lang.Long.valueOf(r4)
            r2.zza(r3, r4)
            goto L_0x0026
        L_0x0979:
            r2 = move-exception
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r3 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzef r3 = r3.zzab()     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzeh r3 = r3.zzgk()     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = "Data loss. Failed to insert raw event metadata. appId"
            java.lang.String r4 = r4.zzag()     // Catch:{ all -> 0x02ac }
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzef.zzam(r4)     // Catch:{ all -> 0x02ac }
            r3.zza(r5, r4, r2)     // Catch:{ all -> 0x02ac }
            goto L_0x0917
        L_0x0994:
            com.google.android.gms.measurement.internal.zzfd r2 = r30.zzgz()     // Catch:{ all -> 0x02ac }
            java.lang.String r3 = r12.zzce     // Catch:{ all -> 0x02ac }
            java.lang.String r4 = r12.name     // Catch:{ all -> 0x02ac }
            boolean r2 = r2.zzl(r3, r4)     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzx r3 = r30.zzgy()     // Catch:{ all -> 0x02ac }
            long r4 = r30.zzjk()     // Catch:{ all -> 0x02ac }
            java.lang.String r6 = r12.zzce     // Catch:{ all -> 0x02ac }
            r7 = 0
            r8 = 0
            r9 = 0
            r10 = 0
            r11 = 0
            com.google.android.gms.measurement.internal.zzw r3 = r3.zza(r4, r6, r7, r8, r9, r10, r11)     // Catch:{ all -> 0x02ac }
            if (r2 == 0) goto L_0x09cd
            long r2 = r3.zzej     // Catch:{ all -> 0x02ac }
            r0 = r30
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x02ac }
            com.google.android.gms.measurement.internal.zzs r4 = r4.zzad()     // Catch:{ all -> 0x02ac }
            java.lang.String r5 = r12.zzce     // Catch:{ all -> 0x02ac }
            int r4 = r4.zzi(r5)     // Catch:{ all -> 0x02ac }
            long r4 = (long) r4
            int r2 = (r2 > r4 ? 1 : (r2 == r4 ? 0 : -1))
            if (r2 >= 0) goto L_0x09cd
            r2 = 1
            goto L_0x090b
        L_0x09cd:
            r2 = 0
            goto L_0x090b
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzjg.zzd(com.google.android.gms.measurement.internal.zzai, com.google.android.gms.measurement.internal.zzn):void");
    }

    /* JADX WARNING: Code restructure failed: missing block: B:169:0x03cf, code lost:
        if (r4 != false) goto L_0x03d1;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:534:0x1021, code lost:
        r4 = e;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:535:0x1022, code lost:
        r14 = r6;
        r5 = r7;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:539:0x102e, code lost:
        r4 = th;
     */
    /* JADX WARNING: Code restructure failed: missing block: B:540:0x102f, code lost:
        r5 = r7;
     */
    /* JADX WARNING: Failed to process nested try/catch */
    /* JADX WARNING: Removed duplicated region for block: B:159:0x0399 A[Catch:{ IOException -> 0x02f7, all -> 0x01ff }] */
    /* JADX WARNING: Removed duplicated region for block: B:223:0x05ca A[Catch:{ IOException -> 0x02f7, all -> 0x01ff }] */
    /* JADX WARNING: Removed duplicated region for block: B:236:0x063e A[Catch:{ IOException -> 0x02f7, all -> 0x01ff }] */
    /* JADX WARNING: Removed duplicated region for block: B:243:0x068e A[Catch:{ IOException -> 0x02f7, all -> 0x01ff }] */
    /* JADX WARNING: Removed duplicated region for block: B:24:0x008a A[Catch:{ IOException -> 0x02f7, all -> 0x01ff }] */
    /* JADX WARNING: Removed duplicated region for block: B:254:0x06e5 A[Catch:{ IOException -> 0x02f7, all -> 0x01ff }] */
    /* JADX WARNING: Removed duplicated region for block: B:28:0x0097 A[Catch:{ IOException -> 0x02f7, all -> 0x01ff }] */
    /* JADX WARNING: Removed duplicated region for block: B:519:0x0fd5 A[Catch:{ IOException -> 0x02f7, all -> 0x01ff }] */
    /* JADX WARNING: Removed duplicated region for block: B:539:0x102e A[ExcHandler: all (th java.lang.Throwable), Splitter:B:16:0x0079] */
    /* JADX WARNING: Unknown top exception splitter block from list: {B:14:0x0052=Splitter:B:14:0x0052, B:76:0x0225=Splitter:B:76:0x0225} */
    @android.support.annotation.WorkerThread
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final boolean zzd(java.lang.String r35, long r36) {
        /*
            r34 = this;
            com.google.android.gms.measurement.internal.zzx r4 = r34.zzgy()
            r4.beginTransaction()
            com.google.android.gms.measurement.internal.zzjg$zza r25 = new com.google.android.gms.measurement.internal.zzjg$zza     // Catch:{ all -> 0x01ff }
            r4 = 0
            r0 = r25
            r1 = r34
            r0.<init>(r1, r4)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzx r16 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            r6 = 0
            r0 = r34
            long r0 = r0.zztj     // Catch:{ all -> 0x01ff }
            r18 = r0
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r25)     // Catch:{ all -> 0x01ff }
            r16.zzo()     // Catch:{ all -> 0x01ff }
            r16.zzbi()     // Catch:{ all -> 0x01ff }
            r8 = 0
            r7 = 0
            android.database.sqlite.SQLiteDatabase r4 = r16.getWritableDatabase()     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r5 = 0
            boolean r5 = android.text.TextUtils.isEmpty(r5)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            if (r5 == 0) goto L_0x0208
            r10 = -1
            int r5 = (r18 > r10 ? 1 : (r18 == r10 ? 0 : -1))
            if (r5 == 0) goto L_0x01a1
            r5 = 2
            java.lang.String[] r5 = new java.lang.String[r5]     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r9 = 0
            java.lang.String r10 = java.lang.String.valueOf(r18)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r5[r9] = r10     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r9 = 1
            java.lang.String r10 = java.lang.String.valueOf(r36)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r5[r9] = r10     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r9 = r5
        L_0x004a:
            r10 = -1
            int r5 = (r18 > r10 ? 1 : (r18 == r10 ? 0 : -1))
            if (r5 == 0) goto L_0x01ae
            java.lang.String r5 = "rowid <= ? and "
        L_0x0052:
            java.lang.String r10 = java.lang.String.valueOf(r5)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            int r10 = r10.length()     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            java.lang.StringBuilder r11 = new java.lang.StringBuilder     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            int r10 = r10 + 148
            r11.<init>(r10)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            java.lang.String r10 = "select app_id, metadata_fingerprint from raw_events where "
            java.lang.StringBuilder r10 = r11.append(r10)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            java.lang.StringBuilder r5 = r10.append(r5)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            java.lang.String r10 = "app_id in (select app_id from apps where config_fetched_time >= ?) order by rowid limit 1;"
            java.lang.StringBuilder r5 = r5.append(r10)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            java.lang.String r5 = r5.toString()     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            android.database.Cursor r7 = r4.rawQuery(r5, r9)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            boolean r5 = r7.moveToFirst()     // Catch:{ SQLiteException -> 0x1021, all -> 0x102e }
            if (r5 != 0) goto L_0x01b2
            if (r7 == 0) goto L_0x0084
            r7.close()     // Catch:{ all -> 0x01ff }
        L_0x0084:
            r0 = r25
            java.util.List<com.google.android.gms.internal.measurement.zzbs$zzc> r4 = r0.zztp     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0094
            r0 = r25
            java.util.List<com.google.android.gms.internal.measurement.zzbs$zzc> r4 = r0.zztp     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.isEmpty()     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x039d
        L_0x0094:
            r4 = 1
        L_0x0095:
            if (r4 != 0) goto L_0x0fd5
            r22 = 0
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r4 = r0.zztn     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = r4.zzuj()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = (com.google.android.gms.internal.measurement.zzey.zza) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r4 = (com.google.android.gms.internal.measurement.zzbs.zzg.zza) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r26 = r4.zznn()     // Catch:{ all -> 0x01ff }
            r18 = 0
            r20 = 0
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r4 = r4.zzad()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r5.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r6 = com.google.android.gms.measurement.internal.zzak.zzii     // Catch:{ all -> 0x01ff }
            boolean r24 = r4.zze(r5, r6)     // Catch:{ all -> 0x01ff }
            r19 = 0
            r16 = -1
            r15 = 0
            r17 = -1
            r4 = 0
            r23 = r4
        L_0x00cd:
            r0 = r25
            java.util.List<com.google.android.gms.internal.measurement.zzbs$zzc> r4 = r0.zztp     // Catch:{ all -> 0x01ff }
            int r4 = r4.size()     // Catch:{ all -> 0x01ff }
            r0 = r23
            if (r0 >= r4) goto L_0x083e
            r0 = r25
            java.util.List<com.google.android.gms.internal.measurement.zzbs$zzc> r4 = r0.zztp     // Catch:{ all -> 0x01ff }
            r0 = r23
            java.lang.Object r4 = r4.get(r0)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r4 = (com.google.android.gms.internal.measurement.zzbs.zzc) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = r4.zzuj()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = (com.google.android.gms.internal.measurement.zzey.zza) r4     // Catch:{ all -> 0x01ff }
            r0 = r4
            com.google.android.gms.internal.measurement.zzbs$zzc$zza r0 = (com.google.android.gms.internal.measurement.zzbs.zzc.zza) r0     // Catch:{ all -> 0x01ff }
            r14 = r0
            com.google.android.gms.measurement.internal.zzfd r4 = r34.zzgz()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r5.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r14.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zzk(r5, r6)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x03a3
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgn()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "Dropping blacklisted raw event. appId"
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r6)     // Catch:{ all -> 0x01ff }
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r7 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzed r7 = r7.zzy()     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = r14.getName()     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r7.zzaj(r8)     // Catch:{ all -> 0x01ff }
            r4.zza(r5, r6, r7)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzfd r4 = r34.zzgz()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r5.zzag()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zzbc(r5)     // Catch:{ all -> 0x01ff }
            if (r4 != 0) goto L_0x0156
            com.google.android.gms.measurement.internal.zzfd r4 = r34.zzgz()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r5.zzag()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zzbd(r5)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x03a0
        L_0x0156:
            r4 = 1
        L_0x0157:
            if (r4 != 0) goto L_0x1009
            java.lang.String r4 = "_err"
            java.lang.String r5 = r14.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.equals(r5)     // Catch:{ all -> 0x01ff }
            if (r4 != 0) goto L_0x1009
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzjs r4 = r4.zzz()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r5.zzag()     // Catch:{ all -> 0x01ff }
            r6 = 11
            java.lang.String r7 = "_ev"
            java.lang.String r8 = r14.getName()     // Catch:{ all -> 0x01ff }
            r9 = 0
            r4.zza(r5, r6, r7, r8, r9)     // Catch:{ all -> 0x01ff }
            r4 = r15
            r6 = r22
            r7 = r16
            r8 = r17
            r9 = r18
            r10 = r20
            r12 = r19
        L_0x018e:
            int r5 = r23 + 1
            r15 = r4
            r22 = r6
            r23 = r5
            r17 = r8
            r16 = r7
            r18 = r9
            r20 = r10
            r19 = r12
            goto L_0x00cd
        L_0x01a1:
            r5 = 1
            java.lang.String[] r5 = new java.lang.String[r5]     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r9 = 0
            java.lang.String r10 = java.lang.String.valueOf(r36)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r5[r9] = r10     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r9 = r5
            goto L_0x004a
        L_0x01ae:
            java.lang.String r5 = ""
            goto L_0x0052
        L_0x01b2:
            r5 = 0
            java.lang.String r6 = r7.getString(r5)     // Catch:{ SQLiteException -> 0x1021, all -> 0x102e }
            r5 = 1
            java.lang.String r5 = r7.getString(r5)     // Catch:{ SQLiteException -> 0x1026, all -> 0x102e }
            r7.close()     // Catch:{ SQLiteException -> 0x1026, all -> 0x102e }
            r13 = r5
            r14 = r6
            r15 = r7
        L_0x01c2:
            java.lang.String r5 = "raw_events_metadata"
            r6 = 1
            java.lang.String[] r6 = new java.lang.String[r6]     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r7 = 0
            java.lang.String r8 = "metadata"
            r6[r7] = r8     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            java.lang.String r7 = "app_id = ? and metadata_fingerprint = ?"
            r8 = 2
            java.lang.String[] r8 = new java.lang.String[r8]     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r9 = 0
            r8[r9] = r14     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r9 = 1
            r8[r9] = r13     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r9 = 0
            r10 = 0
            java.lang.String r11 = "rowid"
            java.lang.String r12 = "2"
            android.database.Cursor r15 = r4.query(r5, r6, r7, r8, r9, r10, r11, r12)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            boolean r5 = r15.moveToFirst()     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            if (r5 != 0) goto L_0x0273
            com.google.android.gms.measurement.internal.zzef r4 = r16.zzab()     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            java.lang.String r5 = "Raw event metadata record is missing. appId"
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r14)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r4.zza(r5, r6)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            if (r15 == 0) goto L_0x0084
            r15.close()     // Catch:{ all -> 0x01ff }
            goto L_0x0084
        L_0x01ff:
            r4 = move-exception
            com.google.android.gms.measurement.internal.zzx r5 = r34.zzgy()
            r5.endTransaction()
            throw r4
        L_0x0208:
            r10 = -1
            int r5 = (r18 > r10 ? 1 : (r18 == r10 ? 0 : -1))
            if (r5 == 0) goto L_0x0259
            r5 = 2
            java.lang.String[] r5 = new java.lang.String[r5]     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r9 = 0
            r10 = 0
            r5[r9] = r10     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r9 = 1
            java.lang.String r10 = java.lang.String.valueOf(r18)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r5[r9] = r10     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r9 = r5
        L_0x021d:
            r10 = -1
            int r5 = (r18 > r10 ? 1 : (r18 == r10 ? 0 : -1))
            if (r5 == 0) goto L_0x0262
            java.lang.String r5 = " and rowid <= ?"
        L_0x0225:
            java.lang.String r10 = java.lang.String.valueOf(r5)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            int r10 = r10.length()     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            java.lang.StringBuilder r11 = new java.lang.StringBuilder     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            int r10 = r10 + 84
            r11.<init>(r10)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            java.lang.String r10 = "select metadata_fingerprint from raw_events where app_id = ?"
            java.lang.StringBuilder r10 = r11.append(r10)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            java.lang.StringBuilder r5 = r10.append(r5)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            java.lang.String r10 = " order by rowid limit 1;"
            java.lang.StringBuilder r5 = r5.append(r10)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            java.lang.String r5 = r5.toString()     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            android.database.Cursor r7 = r4.rawQuery(r5, r9)     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            boolean r5 = r7.moveToFirst()     // Catch:{ SQLiteException -> 0x1021, all -> 0x102e }
            if (r5 != 0) goto L_0x0265
            if (r7 == 0) goto L_0x0084
            r7.close()     // Catch:{ all -> 0x01ff }
            goto L_0x0084
        L_0x0259:
            r5 = 1
            java.lang.String[] r5 = new java.lang.String[r5]     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r9 = 0
            r10 = 0
            r5[r9] = r10     // Catch:{ SQLiteException -> 0x101c, all -> 0x0394 }
            r9 = r5
            goto L_0x021d
        L_0x0262:
            java.lang.String r5 = ""
            goto L_0x0225
        L_0x0265:
            r5 = 0
            java.lang.String r5 = r7.getString(r5)     // Catch:{ SQLiteException -> 0x1021, all -> 0x102e }
            r7.close()     // Catch:{ SQLiteException -> 0x1021, all -> 0x102e }
            r6 = 0
            r13 = r5
            r14 = r6
            r15 = r7
            goto L_0x01c2
        L_0x0273:
            r5 = 0
            byte[] r5 = r15.getBlob(r5)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            com.google.android.gms.internal.measurement.zzel r6 = com.google.android.gms.internal.measurement.zzel.zztq()     // Catch:{ IOException -> 0x02f7 }
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = com.google.android.gms.internal.measurement.zzbs.zzg.zzd(r5, r6)     // Catch:{ IOException -> 0x02f7 }
            boolean r6 = r15.moveToNext()     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            if (r6 == 0) goto L_0x0297
            com.google.android.gms.measurement.internal.zzef r6 = r16.zzab()     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            com.google.android.gms.measurement.internal.zzeh r6 = r6.zzgn()     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            java.lang.String r7 = "Get multiple raw event metadata records, expected one. appId"
            java.lang.Object r8 = com.google.android.gms.measurement.internal.zzef.zzam(r14)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r6.zza(r7, r8)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
        L_0x0297:
            r15.close()     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r0 = r25
            r0.zzb(r5)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r6 = -1
            int r5 = (r18 > r6 ? 1 : (r18 == r6 ? 0 : -1))
            if (r5 == 0) goto L_0x0310
            java.lang.String r7 = "app_id = ? and metadata_fingerprint = ? and rowid <= ?"
            r5 = 3
            java.lang.String[] r8 = new java.lang.String[r5]     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r5 = 0
            r8[r5] = r14
            r5 = 1
            r8[r5] = r13
            r5 = 2
            java.lang.String r6 = java.lang.String.valueOf(r18)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r8[r5] = r6     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
        L_0x02b7:
            java.lang.String r5 = "raw_events"
            r6 = 4
            java.lang.String[] r6 = new java.lang.String[r6]     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r9 = 0
            java.lang.String r10 = "rowid"
            r6[r9] = r10     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r9 = 1
            java.lang.String r10 = "name"
            r6[r9] = r10     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r9 = 2
            java.lang.String r10 = "timestamp"
            r6[r9] = r10     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r9 = 3
            java.lang.String r10 = "data"
            r6[r9] = r10     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r9 = 0
            r10 = 0
            java.lang.String r11 = "rowid"
            r12 = 0
            android.database.Cursor r5 = r4.query(r5, r6, r7, r8, r9, r10, r11, r12)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            boolean r4 = r5.moveToFirst()     // Catch:{ SQLiteException -> 0x031c }
            if (r4 != 0) goto L_0x0335
            com.google.android.gms.measurement.internal.zzef r4 = r16.zzab()     // Catch:{ SQLiteException -> 0x031c }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgn()     // Catch:{ SQLiteException -> 0x031c }
            java.lang.String r6 = "Raw event data disappeared while in transaction. appId"
            java.lang.Object r7 = com.google.android.gms.measurement.internal.zzef.zzam(r14)     // Catch:{ SQLiteException -> 0x031c }
            r4.zza(r6, r7)     // Catch:{ SQLiteException -> 0x031c }
            if (r5 == 0) goto L_0x0084
            r5.close()     // Catch:{ all -> 0x01ff }
            goto L_0x0084
        L_0x02f7:
            r4 = move-exception
            com.google.android.gms.measurement.internal.zzef r5 = r16.zzab()     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            com.google.android.gms.measurement.internal.zzeh r5 = r5.zzgk()     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            java.lang.String r6 = "Data loss. Failed to merge raw event metadata. appId"
            java.lang.Object r7 = com.google.android.gms.measurement.internal.zzef.zzam(r14)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r5.zza(r6, r7, r4)     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            if (r15 == 0) goto L_0x0084
            r15.close()     // Catch:{ all -> 0x01ff }
            goto L_0x0084
        L_0x0310:
            java.lang.String r7 = "app_id = ? and metadata_fingerprint = ?"
            r5 = 2
            java.lang.String[] r8 = new java.lang.String[r5]     // Catch:{ SQLiteException -> 0x1035, all -> 0x102b }
            r5 = 0
            r8[r5] = r14
            r5 = 1
            r8[r5] = r13
            goto L_0x02b7
        L_0x031c:
            r4 = move-exception
        L_0x031d:
            com.google.android.gms.measurement.internal.zzef r6 = r16.zzab()     // Catch:{ all -> 0x1032 }
            com.google.android.gms.measurement.internal.zzeh r6 = r6.zzgk()     // Catch:{ all -> 0x1032 }
            java.lang.String r7 = "Data loss. Error selecting raw event. appId"
            java.lang.Object r8 = com.google.android.gms.measurement.internal.zzef.zzam(r14)     // Catch:{ all -> 0x1032 }
            r6.zza(r7, r8, r4)     // Catch:{ all -> 0x1032 }
            if (r5 == 0) goto L_0x0084
            r5.close()     // Catch:{ all -> 0x01ff }
            goto L_0x0084
        L_0x0335:
            r4 = 0
            long r6 = r5.getLong(r4)     // Catch:{ SQLiteException -> 0x031c }
            r4 = 3
            byte[] r4 = r5.getBlob(r4)     // Catch:{ SQLiteException -> 0x031c }
            com.google.android.gms.internal.measurement.zzbs$zzc$zza r8 = com.google.android.gms.internal.measurement.zzbs.zzc.zzmq()     // Catch:{ IOException -> 0x0375 }
            com.google.android.gms.internal.measurement.zzel r9 = com.google.android.gms.internal.measurement.zzel.zztq()     // Catch:{ IOException -> 0x0375 }
            com.google.android.gms.internal.measurement.zzdh r4 = r8.zzf(r4, r9)     // Catch:{ IOException -> 0x0375 }
            com.google.android.gms.internal.measurement.zzbs$zzc$zza r4 = (com.google.android.gms.internal.measurement.zzbs.zzc.zza) r4     // Catch:{ IOException -> 0x0375 }
            r8 = 1
            java.lang.String r8 = r5.getString(r8)     // Catch:{ SQLiteException -> 0x031c }
            com.google.android.gms.internal.measurement.zzbs$zzc$zza r8 = r4.zzbx(r8)     // Catch:{ SQLiteException -> 0x031c }
            r9 = 2
            long r10 = r5.getLong(r9)     // Catch:{ SQLiteException -> 0x031c }
            r8.zzag(r10)     // Catch:{ SQLiteException -> 0x031c }
            com.google.android.gms.internal.measurement.zzgi r4 = r4.zzug()     // Catch:{ SQLiteException -> 0x031c }
            com.google.android.gms.internal.measurement.zzey r4 = (com.google.android.gms.internal.measurement.zzey) r4     // Catch:{ SQLiteException -> 0x031c }
            com.google.android.gms.internal.measurement.zzbs$zzc r4 = (com.google.android.gms.internal.measurement.zzbs.zzc) r4     // Catch:{ SQLiteException -> 0x031c }
            r0 = r25
            boolean r4 = r0.zza(r6, r4)     // Catch:{ SQLiteException -> 0x031c }
            if (r4 != 0) goto L_0x0387
            if (r5 == 0) goto L_0x0084
            r5.close()     // Catch:{ all -> 0x01ff }
            goto L_0x0084
        L_0x0375:
            r4 = move-exception
            com.google.android.gms.measurement.internal.zzef r6 = r16.zzab()     // Catch:{ SQLiteException -> 0x031c }
            com.google.android.gms.measurement.internal.zzeh r6 = r6.zzgk()     // Catch:{ SQLiteException -> 0x031c }
            java.lang.String r7 = "Data loss. Failed to merge raw event. appId"
            java.lang.Object r8 = com.google.android.gms.measurement.internal.zzef.zzam(r14)     // Catch:{ SQLiteException -> 0x031c }
            r6.zza(r7, r8, r4)     // Catch:{ SQLiteException -> 0x031c }
        L_0x0387:
            boolean r4 = r5.moveToNext()     // Catch:{ SQLiteException -> 0x031c }
            if (r4 != 0) goto L_0x0335
            if (r5 == 0) goto L_0x0084
            r5.close()     // Catch:{ all -> 0x01ff }
            goto L_0x0084
        L_0x0394:
            r4 = move-exception
            r5 = r7
        L_0x0396:
            r15 = r5
        L_0x0397:
            if (r15 == 0) goto L_0x039c
            r15.close()     // Catch:{ all -> 0x01ff }
        L_0x039c:
            throw r4     // Catch:{ all -> 0x01ff }
        L_0x039d:
            r4 = 0
            goto L_0x0095
        L_0x03a0:
            r4 = 0
            goto L_0x0157
        L_0x03a3:
            com.google.android.gms.measurement.internal.zzfd r4 = r34.zzgz()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r5.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r14.getName()     // Catch:{ all -> 0x01ff }
            boolean r27 = r4.zzl(r5, r6)     // Catch:{ all -> 0x01ff }
            if (r27 != 0) goto L_0x03d1
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r14.getName()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r5)     // Catch:{ all -> 0x01ff }
            r4 = -1
            int r6 = r5.hashCode()     // Catch:{ all -> 0x01ff }
            switch(r6) {
                case 94660: goto L_0x040d;
                case 95025: goto L_0x0421;
                case 95027: goto L_0x0417;
                default: goto L_0x03cb;
            }     // Catch:{ all -> 0x01ff }
        L_0x03cb:
            switch(r4) {
                case 0: goto L_0x042b;
                case 1: goto L_0x042b;
                case 2: goto L_0x042b;
                default: goto L_0x03ce;
            }     // Catch:{ all -> 0x01ff }
        L_0x03ce:
            r4 = 0
        L_0x03cf:
            if (r4 == 0) goto L_0x0622
        L_0x03d1:
            r5 = 0
            r6 = 0
            r4 = 0
            r7 = r4
        L_0x03d5:
            int r4 = r14.zzmk()     // Catch:{ all -> 0x01ff }
            if (r7 >= r4) goto L_0x045d
            java.lang.String r4 = "_c"
            com.google.android.gms.internal.measurement.zzbs$zze r8 = r14.zzl(r7)     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = r8.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.equals(r8)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x042d
            com.google.android.gms.internal.measurement.zzbs$zze r4 = r14.zzl(r7)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = r4.zzuj()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = (com.google.android.gms.internal.measurement.zzey.zza) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = (com.google.android.gms.internal.measurement.zzbs.zze.zza) r4     // Catch:{ all -> 0x01ff }
            r8 = 1
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = r4.zzam(r8)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r4 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r4 = (com.google.android.gms.internal.measurement.zzey) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze r4 = (com.google.android.gms.internal.measurement.zzbs.zze) r4     // Catch:{ all -> 0x01ff }
            r14.zza(r7, r4)     // Catch:{ all -> 0x01ff }
            r4 = 1
        L_0x0409:
            int r7 = r7 + 1
            r5 = r4
            goto L_0x03d5
        L_0x040d:
            java.lang.String r6 = "_in"
            boolean r5 = r5.equals(r6)     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x03cb
            r4 = 0
            goto L_0x03cb
        L_0x0417:
            java.lang.String r6 = "_ui"
            boolean r5 = r5.equals(r6)     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x03cb
            r4 = 1
            goto L_0x03cb
        L_0x0421:
            java.lang.String r6 = "_ug"
            boolean r5 = r5.equals(r6)     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x03cb
            r4 = 2
            goto L_0x03cb
        L_0x042b:
            r4 = 1
            goto L_0x03cf
        L_0x042d:
            java.lang.String r4 = "_r"
            com.google.android.gms.internal.measurement.zzbs$zze r8 = r14.zzl(r7)     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = r8.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.equals(r8)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x045b
            com.google.android.gms.internal.measurement.zzbs$zze r4 = r14.zzl(r7)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = r4.zzuj()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = (com.google.android.gms.internal.measurement.zzey.zza) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = (com.google.android.gms.internal.measurement.zzbs.zze.zza) r4     // Catch:{ all -> 0x01ff }
            r8 = 1
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = r4.zzam(r8)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r4 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r4 = (com.google.android.gms.internal.measurement.zzey) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze r4 = (com.google.android.gms.internal.measurement.zzbs.zze) r4     // Catch:{ all -> 0x01ff }
            r6 = 1
            r14.zza(r7, r4)     // Catch:{ all -> 0x01ff }
        L_0x045b:
            r4 = r5
            goto L_0x0409
        L_0x045d:
            if (r5 != 0) goto L_0x0495
            if (r27 == 0) goto L_0x0495
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgs()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "Marking event as conversion"
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r7 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzed r7 = r7.zzy()     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = r14.getName()     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r7.zzaj(r8)     // Catch:{ all -> 0x01ff }
            r4.zza(r5, r7)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = com.google.android.gms.internal.measurement.zzbs.zze.zzng()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "_c"
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = r4.zzbz(r5)     // Catch:{ all -> 0x01ff }
            r8 = 1
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = r4.zzam(r8)     // Catch:{ all -> 0x01ff }
            r14.zza(r4)     // Catch:{ all -> 0x01ff }
        L_0x0495:
            if (r6 != 0) goto L_0x04cb
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgs()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "Marking event as real-time"
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r6 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzed r6 = r6.zzy()     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r14.getName()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzaj(r7)     // Catch:{ all -> 0x01ff }
            r4.zza(r5, r6)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = com.google.android.gms.internal.measurement.zzbs.zze.zzng()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "_r"
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = r4.zzbz(r5)     // Catch:{ all -> 0x01ff }
            r6 = 1
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = r4.zzam(r6)     // Catch:{ all -> 0x01ff }
            r14.zza(r4)     // Catch:{ all -> 0x01ff }
        L_0x04cb:
            com.google.android.gms.measurement.internal.zzx r5 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            long r6 = r34.zzjk()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r4 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = r4.zzag()     // Catch:{ all -> 0x01ff }
            r9 = 0
            r10 = 0
            r11 = 0
            r12 = 0
            r13 = 1
            com.google.android.gms.measurement.internal.zzw r4 = r5.zza(r6, r8, r9, r10, r11, r12, r13)     // Catch:{ all -> 0x01ff }
            long r4 = r4.zzej     // Catch:{ all -> 0x01ff }
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r6 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r6 = r6.zzad()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r7 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r7.zzag()     // Catch:{ all -> 0x01ff }
            int r6 = r6.zzi(r7)     // Catch:{ all -> 0x01ff }
            long r6 = (long) r6     // Catch:{ all -> 0x01ff }
            int r4 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r4 <= 0) goto L_0x1005
            java.lang.String r4 = "_r"
            zza(r14, r4)     // Catch:{ all -> 0x01ff }
        L_0x0504:
            java.lang.String r4 = r14.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = com.google.android.gms.measurement.internal.zzjs.zzbk(r4)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0622
            if (r27 == 0) goto L_0x0622
            com.google.android.gms.measurement.internal.zzx r5 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            long r6 = r34.zzjk()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r4 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = r4.zzag()     // Catch:{ all -> 0x01ff }
            r9 = 0
            r10 = 0
            r11 = 1
            r12 = 0
            r13 = 0
            com.google.android.gms.measurement.internal.zzw r4 = r5.zza(r6, r8, r9, r10, r11, r12, r13)     // Catch:{ all -> 0x01ff }
            long r4 = r4.zzeh     // Catch:{ all -> 0x01ff }
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r6 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r6 = r6.zzad()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r7 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r7.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Integer> r8 = com.google.android.gms.measurement.internal.zzak.zzgs     // Catch:{ all -> 0x01ff }
            int r6 = r6.zzb(r7, r8)     // Catch:{ all -> 0x01ff }
            long r6 = (long) r6     // Catch:{ all -> 0x01ff }
            int r4 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r4 <= 0) goto L_0x0622
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgn()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "Too many conversions. Not logging as conversion. appId"
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r6)     // Catch:{ all -> 0x01ff }
            r4.zza(r5, r6)     // Catch:{ all -> 0x01ff }
            r5 = 0
            r4 = 0
            r6 = -1
            r7 = 0
        L_0x0567:
            int r8 = r14.zzmk()     // Catch:{ all -> 0x01ff }
            if (r7 >= r8) goto L_0x0597
            com.google.android.gms.internal.measurement.zzbs$zze r8 = r14.zzl(r7)     // Catch:{ all -> 0x01ff }
            java.lang.String r9 = "_c"
            java.lang.String r10 = r8.getName()     // Catch:{ all -> 0x01ff }
            boolean r9 = r9.equals(r10)     // Catch:{ all -> 0x01ff }
            if (r9 == 0) goto L_0x0589
            com.google.android.gms.internal.measurement.zzey$zza r4 = r8.zzuj()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = (com.google.android.gms.internal.measurement.zzey.zza) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = (com.google.android.gms.internal.measurement.zzbs.zze.zza) r4     // Catch:{ all -> 0x01ff }
            r6 = r7
        L_0x0586:
            int r7 = r7 + 1
            goto L_0x0567
        L_0x0589:
            java.lang.String r9 = "_err"
            java.lang.String r8 = r8.getName()     // Catch:{ all -> 0x01ff }
            boolean r8 = r9.equals(r8)     // Catch:{ all -> 0x01ff }
            if (r8 == 0) goto L_0x0586
            r5 = 1
            goto L_0x0586
        L_0x0597:
            if (r5 == 0) goto L_0x05e1
            if (r4 == 0) goto L_0x05e1
            r14.zzm(r6)     // Catch:{ all -> 0x01ff }
            r6 = r22
        L_0x05a0:
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r4 = r4.zzad()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r5.zzag()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zzs(r5)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0676
            if (r27 == 0) goto L_0x0676
            java.util.ArrayList r9 = new java.util.ArrayList     // Catch:{ all -> 0x01ff }
            java.util.List r4 = r14.zzmj()     // Catch:{ all -> 0x01ff }
            r9.<init>(r4)     // Catch:{ all -> 0x01ff }
            r7 = -1
            r8 = -1
            r5 = 0
        L_0x05c4:
            int r4 = r9.size()     // Catch:{ all -> 0x01ff }
            if (r5 >= r4) goto L_0x063b
            java.lang.String r10 = "value"
            java.lang.Object r4 = r9.get(r5)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze r4 = (com.google.android.gms.internal.measurement.zzbs.zze) r4     // Catch:{ all -> 0x01ff }
            java.lang.String r4 = r4.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = r10.equals(r4)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0626
            r4 = r5
        L_0x05dd:
            int r5 = r5 + 1
            r7 = r4
            goto L_0x05c4
        L_0x05e1:
            if (r4 == 0) goto L_0x0605
            java.lang.Object r4 = r4.clone()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = (com.google.android.gms.internal.measurement.zzey.zza) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = (com.google.android.gms.internal.measurement.zzbs.zze.zza) r4     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "_err"
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = r4.zzbz(r5)     // Catch:{ all -> 0x01ff }
            r8 = 10
            com.google.android.gms.internal.measurement.zzbs$zze$zza r4 = r4.zzam(r8)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r4 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r4 = (com.google.android.gms.internal.measurement.zzey) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze r4 = (com.google.android.gms.internal.measurement.zzbs.zze) r4     // Catch:{ all -> 0x01ff }
            r14.zza(r6, r4)     // Catch:{ all -> 0x01ff }
            r6 = r22
            goto L_0x05a0
        L_0x0605:
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "Did not find conversion parameter. appId"
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r6)     // Catch:{ all -> 0x01ff }
            r4.zza(r5, r6)     // Catch:{ all -> 0x01ff }
        L_0x0622:
            r6 = r22
            goto L_0x05a0
        L_0x0626:
            java.lang.String r10 = "currency"
            java.lang.Object r4 = r9.get(r5)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze r4 = (com.google.android.gms.internal.measurement.zzbs.zze) r4     // Catch:{ all -> 0x01ff }
            java.lang.String r4 = r4.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = r10.equals(r4)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x1002
            r4 = r7
            r8 = r5
            goto L_0x05dd
        L_0x063b:
            r4 = -1
            if (r7 == r4) goto L_0x0676
            java.lang.Object r4 = r9.get(r7)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze r4 = (com.google.android.gms.internal.measurement.zzbs.zze) r4     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zzna()     // Catch:{ all -> 0x01ff }
            if (r4 != 0) goto L_0x0731
            java.lang.Object r4 = r9.get(r7)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze r4 = (com.google.android.gms.internal.measurement.zzbs.zze) r4     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zznd()     // Catch:{ all -> 0x01ff }
            if (r4 != 0) goto L_0x0731
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgp()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "Value must be specified with a numeric type."
            r4.zzao(r5)     // Catch:{ all -> 0x01ff }
            r14.zzm(r7)     // Catch:{ all -> 0x01ff }
            java.lang.String r4 = "_c"
            zza(r14, r4)     // Catch:{ all -> 0x01ff }
            r4 = 18
            java.lang.String r5 = "value"
            zza(r14, r4, r5)     // Catch:{ all -> 0x01ff }
        L_0x0676:
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r4 = r4.zzad()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r5.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r7 = com.google.android.gms.measurement.internal.zzak.zzih     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zze(r5, r7)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0ff6
            java.lang.String r4 = "_e"
            java.lang.String r5 = r14.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.equals(r5)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0795
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r4 = r14.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r4 = (com.google.android.gms.internal.measurement.zzey) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r4 = (com.google.android.gms.internal.measurement.zzbs.zzc) r4     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "_fr"
            com.google.android.gms.internal.measurement.zzbs$zze r4 = com.google.android.gms.measurement.internal.zzjo.zza(r4, r5)     // Catch:{ all -> 0x01ff }
            if (r4 != 0) goto L_0x0ff6
            if (r15 == 0) goto L_0x078d
            long r4 = r15.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            long r8 = r14.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            long r4 = r4 - r8
            long r4 = java.lang.Math.abs(r4)     // Catch:{ all -> 0x01ff }
            r8 = 1000(0x3e8, double:4.94E-321)
            int r4 = (r4 > r8 ? 1 : (r4 == r8 ? 0 : -1))
            if (r4 > 0) goto L_0x078d
            java.lang.Object r4 = r15.clone()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = (com.google.android.gms.internal.measurement.zzey.zza) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc$zza r4 = (com.google.android.gms.internal.measurement.zzbs.zzc.zza) r4     // Catch:{ all -> 0x01ff }
            r0 = r34
            boolean r5 = r0.zza(r14, r4)     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0785
            r0 = r26
            r1 = r17
            r0.zza(r1, r4)     // Catch:{ all -> 0x01ff }
            r15 = 0
            r19 = 0
            r12 = r19
            r7 = r16
            r8 = r17
            r5 = r15
        L_0x06e3:
            if (r24 != 0) goto L_0x1018
            java.lang.String r4 = "_e"
            java.lang.String r9 = r14.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.equals(r9)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x1018
            int r4 = r14.zzmk()     // Catch:{ all -> 0x01ff }
            if (r4 != 0) goto L_0x07fe
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgn()     // Catch:{ all -> 0x01ff }
            java.lang.String r9 = "Engagement event does not contain any parameters. appId"
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r10 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r10 = r10.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.Object r10 = com.google.android.gms.measurement.internal.zzef.zzam(r10)     // Catch:{ all -> 0x01ff }
            r4.zza(r9, r10)     // Catch:{ all -> 0x01ff }
            r10 = r20
        L_0x0716:
            r0 = r25
            java.util.List<com.google.android.gms.internal.measurement.zzbs$zzc> r9 = r0.zztp     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r4 = r14.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r4 = (com.google.android.gms.internal.measurement.zzey) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r4 = (com.google.android.gms.internal.measurement.zzbs.zzc) r4     // Catch:{ all -> 0x01ff }
            r0 = r23
            r9.set(r0, r4)     // Catch:{ all -> 0x01ff }
            r0 = r26
            r0.zza(r14)     // Catch:{ all -> 0x01ff }
            int r9 = r18 + 1
            r4 = r5
            goto L_0x018e
        L_0x0731:
            r4 = -1
            if (r8 != r4) goto L_0x0759
            r4 = 1
        L_0x0735:
            if (r4 == 0) goto L_0x0676
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgp()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "Value parameter discarded. You must also supply a 3-letter ISO_4217 currency code in the currency parameter."
            r4.zzao(r5)     // Catch:{ all -> 0x01ff }
            r14.zzm(r7)     // Catch:{ all -> 0x01ff }
            java.lang.String r4 = "_c"
            zza(r14, r4)     // Catch:{ all -> 0x01ff }
            r4 = 19
            java.lang.String r5 = "currency"
            zza(r14, r4, r5)     // Catch:{ all -> 0x01ff }
            goto L_0x0676
        L_0x0759:
            java.lang.Object r4 = r9.get(r8)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze r4 = (com.google.android.gms.internal.measurement.zzbs.zze) r4     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r4.zzmy()     // Catch:{ all -> 0x01ff }
            int r4 = r5.length()     // Catch:{ all -> 0x01ff }
            r8 = 3
            if (r4 == r8) goto L_0x076c
            r4 = 1
            goto L_0x0735
        L_0x076c:
            r4 = 0
        L_0x076d:
            int r8 = r5.length()     // Catch:{ all -> 0x01ff }
            if (r4 >= r8) goto L_0x0fff
            int r8 = r5.codePointAt(r4)     // Catch:{ all -> 0x01ff }
            boolean r9 = java.lang.Character.isLetter(r8)     // Catch:{ all -> 0x01ff }
            if (r9 != 0) goto L_0x077f
            r4 = 1
            goto L_0x0735
        L_0x077f:
            int r8 = java.lang.Character.charCount(r8)     // Catch:{ all -> 0x01ff }
            int r4 = r4 + r8
            goto L_0x076d
        L_0x0785:
            r12 = r14
            r7 = r18
            r8 = r17
            r5 = r15
            goto L_0x06e3
        L_0x078d:
            r12 = r14
            r7 = r18
            r8 = r17
            r5 = r15
            goto L_0x06e3
        L_0x0795:
            java.lang.String r4 = "_vs"
            java.lang.String r5 = r14.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.equals(r5)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0ff6
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r4 = r14.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r4 = (com.google.android.gms.internal.measurement.zzey) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r4 = (com.google.android.gms.internal.measurement.zzbs.zzc) r4     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "_et"
            com.google.android.gms.internal.measurement.zzbs$zze r4 = com.google.android.gms.measurement.internal.zzjo.zza(r4, r5)     // Catch:{ all -> 0x01ff }
            if (r4 != 0) goto L_0x0ff6
            if (r19 == 0) goto L_0x07f5
            long r4 = r19.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            long r8 = r14.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            long r4 = r4 - r8
            long r4 = java.lang.Math.abs(r4)     // Catch:{ all -> 0x01ff }
            r8 = 1000(0x3e8, double:4.94E-321)
            int r4 = (r4 > r8 ? 1 : (r4 == r8 ? 0 : -1))
            if (r4 > 0) goto L_0x07f5
            java.lang.Object r4 = r19.clone()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = (com.google.android.gms.internal.measurement.zzey.zza) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc$zza r4 = (com.google.android.gms.internal.measurement.zzbs.zzc.zza) r4     // Catch:{ all -> 0x01ff }
            r0 = r34
            boolean r5 = r0.zza(r4, r14)     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x07ec
            r0 = r26
            r1 = r16
            r0.zza(r1, r4)     // Catch:{ all -> 0x01ff }
            r15 = 0
            r19 = 0
            r12 = r19
            r7 = r16
            r8 = r17
            r5 = r15
            goto L_0x06e3
        L_0x07ec:
            r12 = r19
            r7 = r16
            r8 = r18
            r5 = r14
            goto L_0x06e3
        L_0x07f5:
            r12 = r19
            r7 = r16
            r8 = r18
            r5 = r14
            goto L_0x06e3
        L_0x07fe:
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r4 = r14.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r4 = (com.google.android.gms.internal.measurement.zzey) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r4 = (com.google.android.gms.internal.measurement.zzbs.zzc) r4     // Catch:{ all -> 0x01ff }
            java.lang.String r9 = "_et"
            java.lang.Object r4 = com.google.android.gms.measurement.internal.zzjo.zzb(r4, r9)     // Catch:{ all -> 0x01ff }
            java.lang.Long r4 = (java.lang.Long) r4     // Catch:{ all -> 0x01ff }
            if (r4 != 0) goto L_0x0834
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgn()     // Catch:{ all -> 0x01ff }
            java.lang.String r9 = "Engagement event does not include duration. appId"
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r10 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r10 = r10.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.Object r10 = com.google.android.gms.measurement.internal.zzef.zzam(r10)     // Catch:{ all -> 0x01ff }
            r4.zza(r9, r10)     // Catch:{ all -> 0x01ff }
            r10 = r20
            goto L_0x0716
        L_0x0834:
            long r10 = r4.longValue()     // Catch:{ all -> 0x01ff }
            long r20 = r20 + r10
            r10 = r20
            goto L_0x0716
        L_0x083e:
            if (r24 == 0) goto L_0x089f
            r6 = 0
            r7 = r18
            r4 = r20
        L_0x0845:
            if (r6 >= r7) goto L_0x089d
            r0 = r26
            com.google.android.gms.internal.measurement.zzbs$zzc r8 = r0.zzq(r6)     // Catch:{ all -> 0x01ff }
            java.lang.String r9 = "_e"
            java.lang.String r10 = r8.getName()     // Catch:{ all -> 0x01ff }
            boolean r9 = r9.equals(r10)     // Catch:{ all -> 0x01ff }
            if (r9 == 0) goto L_0x0870
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            java.lang.String r9 = "_fr"
            com.google.android.gms.internal.measurement.zzbs$zze r9 = com.google.android.gms.measurement.internal.zzjo.zza(r8, r9)     // Catch:{ all -> 0x01ff }
            if (r9 == 0) goto L_0x0870
            r0 = r26
            r0.zzr(r6)     // Catch:{ all -> 0x01ff }
            int r7 = r7 + -1
            int r6 = r6 + -1
        L_0x086d:
            int r6 = r6 + 1
            goto L_0x0845
        L_0x0870:
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            java.lang.String r9 = "_et"
            com.google.android.gms.internal.measurement.zzbs$zze r8 = com.google.android.gms.measurement.internal.zzjo.zza(r8, r9)     // Catch:{ all -> 0x01ff }
            if (r8 == 0) goto L_0x086d
            boolean r9 = r8.zzna()     // Catch:{ all -> 0x01ff }
            if (r9 == 0) goto L_0x089b
            long r8 = r8.zznb()     // Catch:{ all -> 0x01ff }
            java.lang.Long r8 = java.lang.Long.valueOf(r8)     // Catch:{ all -> 0x01ff }
        L_0x0889:
            if (r8 == 0) goto L_0x086d
            long r10 = r8.longValue()     // Catch:{ all -> 0x01ff }
            r12 = 0
            int r9 = (r10 > r12 ? 1 : (r10 == r12 ? 0 : -1))
            if (r9 <= 0) goto L_0x086d
            long r8 = r8.longValue()     // Catch:{ all -> 0x01ff }
            long r4 = r4 + r8
            goto L_0x086d
        L_0x089b:
            r8 = 0
            goto L_0x0889
        L_0x089d:
            r20 = r4
        L_0x089f:
            r4 = 0
            r0 = r34
            r1 = r26
            r2 = r20
            r0.zza(r1, r2, r4)     // Catch:{ all -> 0x01ff }
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r4 = r4.zzad()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r26.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r6 = com.google.android.gms.measurement.internal.zzak.zzja     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zze(r5, r6)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0a91
            java.util.List r4 = r26.zznl()     // Catch:{ all -> 0x01ff }
            java.util.Iterator r5 = r4.iterator()     // Catch:{ all -> 0x01ff }
        L_0x08c5:
            boolean r4 = r5.hasNext()     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0ff3
            java.lang.String r6 = "_s"
            java.lang.Object r4 = r5.next()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r4 = (com.google.android.gms.internal.measurement.zzbs.zzc) r4     // Catch:{ all -> 0x01ff }
            java.lang.String r4 = r4.getName()     // Catch:{ all -> 0x01ff }
            boolean r4 = r6.equals(r4)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x08c5
            r4 = 1
        L_0x08de:
            if (r4 == 0) goto L_0x08ed
            com.google.android.gms.measurement.internal.zzx r4 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r26.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "_se"
            r4.zzd(r5, r6)     // Catch:{ all -> 0x01ff }
        L_0x08ed:
            r4 = 1
            r0 = r34
            r1 = r26
            r2 = r20
            r0.zza(r1, r2, r4)     // Catch:{ all -> 0x01ff }
        L_0x08f7:
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r4 = r4.zzad()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r26.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r6 = com.google.android.gms.measurement.internal.zzak.zzij     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zze(r5, r6)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x09a0
            com.google.android.gms.measurement.internal.zzjo r4 = r34.zzgw()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r5 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r5 = r5.zzgs()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "Checking account type status for ad personalization signals"
            r5.zzao(r6)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzfd r5 = r4.zzgz()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r26.zzag()     // Catch:{ all -> 0x01ff }
            boolean r5 = r5.zzba(r6)     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x09a0
            com.google.android.gms.measurement.internal.zzx r5 = r4.zzgy()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r26.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzf r5 = r5.zzab(r6)     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x09a0
            boolean r5 = r5.zzbe()     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x09a0
            com.google.android.gms.measurement.internal.zzac r5 = r4.zzw()     // Catch:{ all -> 0x01ff }
            boolean r5 = r5.zzcu()     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x09a0
            com.google.android.gms.measurement.internal.zzef r5 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r5 = r5.zzgr()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "Turning off ad personalization due to account type"
            r5.zzao(r6)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzk$zza r5 = com.google.android.gms.internal.measurement.zzbs.zzk.zzqu()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "_npa"
            com.google.android.gms.internal.measurement.zzbs$zzk$zza r5 = r5.zzdb(r6)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzac r4 = r4.zzw()     // Catch:{ all -> 0x01ff }
            long r6 = r4.zzcs()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzk$zza r4 = r5.zzbk(r6)     // Catch:{ all -> 0x01ff }
            r6 = 1
            com.google.android.gms.internal.measurement.zzbs$zzk$zza r4 = r4.zzbl(r6)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r4 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r4 = (com.google.android.gms.internal.measurement.zzey) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzk r4 = (com.google.android.gms.internal.measurement.zzbs.zzk) r4     // Catch:{ all -> 0x01ff }
            r6 = 0
            r5 = 0
        L_0x097b:
            int r7 = r26.zznp()     // Catch:{ all -> 0x01ff }
            if (r5 >= r7) goto L_0x1039
            java.lang.String r7 = "_npa"
            r0 = r26
            com.google.android.gms.internal.measurement.zzbs$zzk r8 = r0.zzs(r5)     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = r8.getName()     // Catch:{ all -> 0x01ff }
            boolean r7 = r7.equals(r8)     // Catch:{ all -> 0x01ff }
            if (r7 == 0) goto L_0x0ab4
            r0 = r26
            r0.zza(r5, r4)     // Catch:{ all -> 0x01ff }
            r5 = 1
        L_0x0999:
            if (r5 != 0) goto L_0x09a0
            r0 = r26
            r0.zza(r4)     // Catch:{ all -> 0x01ff }
        L_0x09a0:
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r4 = r26.zznv()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r26.zzag()     // Catch:{ all -> 0x01ff }
            java.util.List r6 = r26.zzno()     // Catch:{ all -> 0x01ff }
            java.util.List r7 = r26.zznl()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.common.internal.Preconditions.checkNotEmpty(r5)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzp r8 = r34.zzgx()     // Catch:{ all -> 0x01ff }
            java.util.List r5 = r8.zza(r5, r7, r6)     // Catch:{ all -> 0x01ff }
            r4.zzc(r5)     // Catch:{ all -> 0x01ff }
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r4 = r4.zzad()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r5.zzag()     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zzm(r5)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0ddd
            java.util.HashMap r27 = new java.util.HashMap     // Catch:{ all -> 0x01ff }
            r27.<init>()     // Catch:{ all -> 0x01ff }
            java.util.ArrayList r28 = new java.util.ArrayList     // Catch:{ all -> 0x01ff }
            r28.<init>()     // Catch:{ all -> 0x01ff }
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzjs r4 = r4.zzz()     // Catch:{ all -> 0x01ff }
            java.security.SecureRandom r29 = r4.zzjw()     // Catch:{ all -> 0x01ff }
            r4 = 0
            r24 = r4
        L_0x09ed:
            int r4 = r26.zznm()     // Catch:{ all -> 0x01ff }
            r0 = r24
            if (r0 >= r4) goto L_0x0da8
            r0 = r26
            r1 = r24
            com.google.android.gms.internal.measurement.zzbs$zzc r4 = r0.zzq(r1)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = r4.zzuj()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey$zza r4 = (com.google.android.gms.internal.measurement.zzey.zza) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc$zza r4 = (com.google.android.gms.internal.measurement.zzbs.zzc.zza) r4     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r4.getName()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "_ep"
            boolean r5 = r5.equals(r6)     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0ab8
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r5 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r5 = (com.google.android.gms.internal.measurement.zzey) r5     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r5 = (com.google.android.gms.internal.measurement.zzbs.zzc) r5     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "_en"
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzjo.zzb(r5, r6)     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = (java.lang.String) r5     // Catch:{ all -> 0x01ff }
            r0 = r27
            java.lang.Object r6 = r0.get(r5)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzae r6 = (com.google.android.gms.measurement.internal.zzae) r6     // Catch:{ all -> 0x01ff }
            if (r6 != 0) goto L_0x0a43
            com.google.android.gms.measurement.internal.zzx r6 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r7 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r7.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzae r6 = r6.zzc(r7, r5)     // Catch:{ all -> 0x01ff }
            r0 = r27
            r0.put(r5, r6)     // Catch:{ all -> 0x01ff }
        L_0x0a43:
            java.lang.Long r5 = r6.zzfm     // Catch:{ all -> 0x01ff }
            if (r5 != 0) goto L_0x0a84
            java.lang.Long r5 = r6.zzfn     // Catch:{ all -> 0x01ff }
            long r8 = r5.longValue()     // Catch:{ all -> 0x01ff }
            r10 = 1
            int r5 = (r8 > r10 ? 1 : (r8 == r10 ? 0 : -1))
            if (r5 <= 0) goto L_0x0a5d
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "_sr"
            java.lang.Long r7 = r6.zzfn     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzjo.zza(r4, r5, r7)     // Catch:{ all -> 0x01ff }
        L_0x0a5d:
            java.lang.Boolean r5 = r6.zzfo     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0a77
            java.lang.Boolean r5 = r6.zzfo     // Catch:{ all -> 0x01ff }
            boolean r5 = r5.booleanValue()     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0a77
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "_efs"
            r6 = 1
            java.lang.Long r6 = java.lang.Long.valueOf(r6)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzjo.zza(r4, r5, r6)     // Catch:{ all -> 0x01ff }
        L_0x0a77:
            com.google.android.gms.internal.measurement.zzgi r5 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r5 = (com.google.android.gms.internal.measurement.zzey) r5     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r5 = (com.google.android.gms.internal.measurement.zzbs.zzc) r5     // Catch:{ all -> 0x01ff }
            r0 = r28
            r0.add(r5)     // Catch:{ all -> 0x01ff }
        L_0x0a84:
            r0 = r26
            r1 = r24
            r0.zza(r1, r4)     // Catch:{ all -> 0x01ff }
        L_0x0a8b:
            int r4 = r24 + 1
            r24 = r4
            goto L_0x09ed
        L_0x0a91:
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r4 = r4.zzad()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r26.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r6 = com.google.android.gms.measurement.internal.zzak.zzjb     // Catch:{ all -> 0x01ff }
            boolean r4 = r4.zze(r5, r6)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x08f7
            com.google.android.gms.measurement.internal.zzx r4 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r26.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "_se"
            r4.zzd(r5, r6)     // Catch:{ all -> 0x01ff }
            goto L_0x08f7
        L_0x0ab4:
            int r5 = r5 + 1
            goto L_0x097b
        L_0x0ab8:
            com.google.android.gms.measurement.internal.zzfd r5 = r34.zzgz()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            long r30 = r5.zzbb(r6)     // Catch:{ all -> 0x01ff }
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r5 = r0.zzj     // Catch:{ all -> 0x01ff }
            r5.zzz()     // Catch:{ all -> 0x01ff }
            long r6 = r4.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            r0 = r30
            long r32 = com.google.android.gms.measurement.internal.zzjs.zzc(r6, r0)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r5 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r5 = (com.google.android.gms.internal.measurement.zzey) r5     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r5 = (com.google.android.gms.internal.measurement.zzbs.zzc) r5     // Catch:{ all -> 0x01ff }
            r6 = 1
            java.lang.Long r6 = java.lang.Long.valueOf(r6)     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = "_dbg"
            boolean r7 = android.text.TextUtils.isEmpty(r7)     // Catch:{ all -> 0x01ff }
            if (r7 != 0) goto L_0x0af1
            if (r6 != 0) goto L_0x0b3b
        L_0x0af1:
            r5 = 0
        L_0x0af2:
            if (r5 != 0) goto L_0x0fee
            com.google.android.gms.measurement.internal.zzfd r5 = r34.zzgz()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r4.getName()     // Catch:{ all -> 0x01ff }
            int r5 = r5.zzm(r6, r7)     // Catch:{ all -> 0x01ff }
            r23 = r5
        L_0x0b0a:
            if (r23 > 0) goto L_0x0b96
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r5 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r5 = r5.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r5 = r5.zzgn()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "Sample rate must be positive. event, rate"
            java.lang.String r7 = r4.getName()     // Catch:{ all -> 0x01ff }
            java.lang.Integer r8 = java.lang.Integer.valueOf(r23)     // Catch:{ all -> 0x01ff }
            r5.zza(r6, r7, r8)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r5 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r5 = (com.google.android.gms.internal.measurement.zzey) r5     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r5 = (com.google.android.gms.internal.measurement.zzbs.zzc) r5     // Catch:{ all -> 0x01ff }
            r0 = r28
            r0.add(r5)     // Catch:{ all -> 0x01ff }
            r0 = r26
            r1 = r24
            r0.zza(r1, r4)     // Catch:{ all -> 0x01ff }
            goto L_0x0a8b
        L_0x0b3b:
            java.util.List r5 = r5.zzmj()     // Catch:{ all -> 0x01ff }
            java.util.Iterator r7 = r5.iterator()     // Catch:{ all -> 0x01ff }
        L_0x0b43:
            boolean r5 = r7.hasNext()     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0b93
            java.lang.Object r5 = r7.next()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zze r5 = (com.google.android.gms.internal.measurement.zzbs.zze) r5     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = "_dbg"
            java.lang.String r9 = r5.getName()     // Catch:{ all -> 0x01ff }
            boolean r8 = r8.equals(r9)     // Catch:{ all -> 0x01ff }
            if (r8 == 0) goto L_0x0b43
            boolean r7 = r6 instanceof java.lang.Long     // Catch:{ all -> 0x01ff }
            if (r7 == 0) goto L_0x0b6d
            long r8 = r5.zznb()     // Catch:{ all -> 0x01ff }
            java.lang.Long r7 = java.lang.Long.valueOf(r8)     // Catch:{ all -> 0x01ff }
            boolean r7 = r6.equals(r7)     // Catch:{ all -> 0x01ff }
            if (r7 != 0) goto L_0x0b8d
        L_0x0b6d:
            boolean r7 = r6 instanceof java.lang.String     // Catch:{ all -> 0x01ff }
            if (r7 == 0) goto L_0x0b7b
            java.lang.String r7 = r5.zzmy()     // Catch:{ all -> 0x01ff }
            boolean r7 = r6.equals(r7)     // Catch:{ all -> 0x01ff }
            if (r7 != 0) goto L_0x0b8d
        L_0x0b7b:
            boolean r7 = r6 instanceof java.lang.Double     // Catch:{ all -> 0x01ff }
            if (r7 == 0) goto L_0x0b90
            double r8 = r5.zzne()     // Catch:{ all -> 0x01ff }
            java.lang.Double r5 = java.lang.Double.valueOf(r8)     // Catch:{ all -> 0x01ff }
            boolean r5 = r6.equals(r5)     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0b90
        L_0x0b8d:
            r5 = 1
            goto L_0x0af2
        L_0x0b90:
            r5 = 0
            goto L_0x0af2
        L_0x0b93:
            r5 = 0
            goto L_0x0af2
        L_0x0b96:
            java.lang.String r5 = r4.getName()     // Catch:{ all -> 0x01ff }
            r0 = r27
            java.lang.Object r5 = r0.get(r5)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzae r5 = (com.google.android.gms.measurement.internal.zzae) r5     // Catch:{ all -> 0x01ff }
            if (r5 != 0) goto L_0x0feb
            com.google.android.gms.measurement.internal.zzx r5 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r4.getName()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzae r5 = r5.zzc(r6, r7)     // Catch:{ all -> 0x01ff }
            if (r5 != 0) goto L_0x0fe8
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r5 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r5 = r5.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r5 = r5.zzgn()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "Event being bundled has no eventAggregate. appId, eventName"
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r7 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r7.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = r4.getName()     // Catch:{ all -> 0x01ff }
            r5.zza(r6, r7, r8)     // Catch:{ all -> 0x01ff }
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r5 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r5 = r5.zzad()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzdu<java.lang.Boolean> r7 = com.google.android.gms.measurement.internal.zzak.zziz     // Catch:{ all -> 0x01ff }
            boolean r5 = r5.zze(r6, r7)     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0c6c
            com.google.android.gms.measurement.internal.zzae r5 = new com.google.android.gms.measurement.internal.zzae     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r4.getName()     // Catch:{ all -> 0x01ff }
            r8 = 1
            r10 = 1
            r12 = 1
            long r14 = r4.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            r16 = 0
            r18 = 0
            r19 = 0
            r20 = 0
            r21 = 0
            r5.<init>(r6, r7, r8, r10, r12, r14, r16, r18, r19, r20, r21)     // Catch:{ all -> 0x01ff }
            r6 = r5
        L_0x0c15:
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r5 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r5 = (com.google.android.gms.internal.measurement.zzey) r5     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r5 = (com.google.android.gms.internal.measurement.zzbs.zzc) r5     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = "_eid"
            java.lang.Object r5 = com.google.android.gms.measurement.internal.zzjo.zzb(r5, r7)     // Catch:{ all -> 0x01ff }
            java.lang.Long r5 = (java.lang.Long) r5     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0c91
            r7 = 1
        L_0x0c2b:
            java.lang.Boolean r10 = java.lang.Boolean.valueOf(r7)     // Catch:{ all -> 0x01ff }
            r7 = 1
            r0 = r23
            if (r0 != r7) goto L_0x0c93
            com.google.android.gms.internal.measurement.zzgi r5 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r5 = (com.google.android.gms.internal.measurement.zzey) r5     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r5 = (com.google.android.gms.internal.measurement.zzbs.zzc) r5     // Catch:{ all -> 0x01ff }
            r0 = r28
            r0.add(r5)     // Catch:{ all -> 0x01ff }
            boolean r5 = r10.booleanValue()     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0c63
            java.lang.Long r5 = r6.zzfm     // Catch:{ all -> 0x01ff }
            if (r5 != 0) goto L_0x0c53
            java.lang.Long r5 = r6.zzfn     // Catch:{ all -> 0x01ff }
            if (r5 != 0) goto L_0x0c53
            java.lang.Boolean r5 = r6.zzfo     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0c63
        L_0x0c53:
            r5 = 0
            r7 = 0
            r8 = 0
            com.google.android.gms.measurement.internal.zzae r5 = r6.zza(r5, r7, r8)     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r4.getName()     // Catch:{ all -> 0x01ff }
            r0 = r27
            r0.put(r6, r5)     // Catch:{ all -> 0x01ff }
        L_0x0c63:
            r0 = r26
            r1 = r24
            r0.zza(r1, r4)     // Catch:{ all -> 0x01ff }
            goto L_0x0a8b
        L_0x0c6c:
            com.google.android.gms.measurement.internal.zzae r5 = new com.google.android.gms.measurement.internal.zzae     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.String r7 = r4.getName()     // Catch:{ all -> 0x01ff }
            r8 = 1
            r10 = 1
            long r12 = r4.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            r14 = 0
            r16 = 0
            r17 = 0
            r18 = 0
            r19 = 0
            r5.<init>(r6, r7, r8, r10, r12, r14, r16, r17, r18, r19)     // Catch:{ all -> 0x01ff }
            r6 = r5
            goto L_0x0c15
        L_0x0c91:
            r7 = 0
            goto L_0x0c2b
        L_0x0c93:
            r0 = r29
            r1 = r23
            int r7 = r0.nextInt(r1)     // Catch:{ all -> 0x01ff }
            if (r7 != 0) goto L_0x0ce8
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "_sr"
            r0 = r23
            long r8 = (long) r0     // Catch:{ all -> 0x01ff }
            java.lang.Long r7 = java.lang.Long.valueOf(r8)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzjo.zza(r4, r5, r7)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r5 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r5 = (com.google.android.gms.internal.measurement.zzey) r5     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r5 = (com.google.android.gms.internal.measurement.zzbs.zzc) r5     // Catch:{ all -> 0x01ff }
            r0 = r28
            r0.add(r5)     // Catch:{ all -> 0x01ff }
            boolean r5 = r10.booleanValue()     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0ccc
            r5 = 0
            r0 = r23
            long r8 = (long) r0     // Catch:{ all -> 0x01ff }
            java.lang.Long r7 = java.lang.Long.valueOf(r8)     // Catch:{ all -> 0x01ff }
            r8 = 0
            com.google.android.gms.measurement.internal.zzae r6 = r6.zza(r5, r7, r8)     // Catch:{ all -> 0x01ff }
        L_0x0ccc:
            java.lang.String r5 = r4.getName()     // Catch:{ all -> 0x01ff }
            long r8 = r4.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            r0 = r32
            com.google.android.gms.measurement.internal.zzae r6 = r6.zza(r8, r0)     // Catch:{ all -> 0x01ff }
            r0 = r27
            r0.put(r5, r6)     // Catch:{ all -> 0x01ff }
        L_0x0cdf:
            r0 = r26
            r1 = r24
            r0.zza(r1, r4)     // Catch:{ all -> 0x01ff }
            goto L_0x0a8b
        L_0x0ce8:
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r7 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzs r7 = r7.zzad()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r8 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = r8.zzag()     // Catch:{ all -> 0x01ff }
            boolean r7 = r7.zzu(r8)     // Catch:{ all -> 0x01ff }
            if (r7 == 0) goto L_0x0d79
            java.lang.Long r7 = r6.zzfl     // Catch:{ all -> 0x01ff }
            if (r7 == 0) goto L_0x0d65
            java.lang.Long r7 = r6.zzfl     // Catch:{ all -> 0x01ff }
            long r8 = r7.longValue()     // Catch:{ all -> 0x01ff }
        L_0x0d08:
            int r7 = (r8 > r32 ? 1 : (r8 == r32 ? 0 : -1))
            if (r7 == 0) goto L_0x0d77
            r7 = 1
        L_0x0d0d:
            if (r7 == 0) goto L_0x0d91
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "_efs"
            r8 = 1
            java.lang.Long r7 = java.lang.Long.valueOf(r8)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzjo.zza(r4, r5, r7)     // Catch:{ all -> 0x01ff }
            r34.zzgw()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "_sr"
            r0 = r23
            long r8 = (long) r0     // Catch:{ all -> 0x01ff }
            java.lang.Long r7 = java.lang.Long.valueOf(r8)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzjo.zza(r4, r5, r7)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r5 = r4.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r5 = (com.google.android.gms.internal.measurement.zzey) r5     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzc r5 = (com.google.android.gms.internal.measurement.zzbs.zzc) r5     // Catch:{ all -> 0x01ff }
            r0 = r28
            r0.add(r5)     // Catch:{ all -> 0x01ff }
            boolean r5 = r10.booleanValue()     // Catch:{ all -> 0x01ff }
            if (r5 == 0) goto L_0x0fe5
            r5 = 0
            r0 = r23
            long r8 = (long) r0     // Catch:{ all -> 0x01ff }
            java.lang.Long r7 = java.lang.Long.valueOf(r8)     // Catch:{ all -> 0x01ff }
            r8 = 1
            java.lang.Boolean r8 = java.lang.Boolean.valueOf(r8)     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzae r5 = r6.zza(r5, r7, r8)     // Catch:{ all -> 0x01ff }
        L_0x0d50:
            java.lang.String r6 = r4.getName()     // Catch:{ all -> 0x01ff }
            long r8 = r4.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            r0 = r32
            com.google.android.gms.measurement.internal.zzae r5 = r5.zza(r8, r0)     // Catch:{ all -> 0x01ff }
            r0 = r27
            r0.put(r6, r5)     // Catch:{ all -> 0x01ff }
            goto L_0x0cdf
        L_0x0d65:
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r7 = r0.zzj     // Catch:{ all -> 0x01ff }
            r7.zzz()     // Catch:{ all -> 0x01ff }
            long r8 = r4.zzmm()     // Catch:{ all -> 0x01ff }
            r0 = r30
            long r8 = com.google.android.gms.measurement.internal.zzjs.zzc(r8, r0)     // Catch:{ all -> 0x01ff }
            goto L_0x0d08
        L_0x0d77:
            r7 = 0
            goto L_0x0d0d
        L_0x0d79:
            long r8 = r6.zzfk     // Catch:{ all -> 0x01ff }
            long r12 = r4.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            long r8 = r12 - r8
            long r8 = java.lang.Math.abs(r8)     // Catch:{ all -> 0x01ff }
            r12 = 86400000(0x5265c00, double:4.2687272E-316)
            int r7 = (r8 > r12 ? 1 : (r8 == r12 ? 0 : -1))
            if (r7 < 0) goto L_0x0d8e
            r7 = 1
            goto L_0x0d0d
        L_0x0d8e:
            r7 = 0
            goto L_0x0d0d
        L_0x0d91:
            boolean r7 = r10.booleanValue()     // Catch:{ all -> 0x01ff }
            if (r7 == 0) goto L_0x0cdf
            java.lang.String r7 = r4.getName()     // Catch:{ all -> 0x01ff }
            r8 = 0
            r9 = 0
            com.google.android.gms.measurement.internal.zzae r5 = r6.zza(r5, r8, r9)     // Catch:{ all -> 0x01ff }
            r0 = r27
            r0.put(r7, r5)     // Catch:{ all -> 0x01ff }
            goto L_0x0cdf
        L_0x0da8:
            int r4 = r28.size()     // Catch:{ all -> 0x01ff }
            int r5 = r26.zznm()     // Catch:{ all -> 0x01ff }
            if (r4 >= r5) goto L_0x0dbb
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r4 = r26.zznn()     // Catch:{ all -> 0x01ff }
            r0 = r28
            r4.zza(r0)     // Catch:{ all -> 0x01ff }
        L_0x0dbb:
            java.util.Set r4 = r27.entrySet()     // Catch:{ all -> 0x01ff }
            java.util.Iterator r5 = r4.iterator()     // Catch:{ all -> 0x01ff }
        L_0x0dc3:
            boolean r4 = r5.hasNext()     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0ddd
            java.lang.Object r4 = r5.next()     // Catch:{ all -> 0x01ff }
            java.util.Map$Entry r4 = (java.util.Map.Entry) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzx r6 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            java.lang.Object r4 = r4.getValue()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzae r4 = (com.google.android.gms.measurement.internal.zzae) r4     // Catch:{ all -> 0x01ff }
            r6.zza(r4)     // Catch:{ all -> 0x01ff }
            goto L_0x0dc3
        L_0x0ddd:
            r4 = 9223372036854775807(0x7fffffffffffffff, double:NaN)
            r0 = r26
            com.google.android.gms.internal.measurement.zzbs$zzg$zza r4 = r0.zzao(r4)     // Catch:{ all -> 0x01ff }
            r6 = -9223372036854775808
            r4.zzap(r6)     // Catch:{ all -> 0x01ff }
            r4 = 0
        L_0x0dee:
            int r5 = r26.zznm()     // Catch:{ all -> 0x01ff }
            if (r4 >= r5) goto L_0x0e27
            r0 = r26
            com.google.android.gms.internal.measurement.zzbs$zzc r5 = r0.zzq(r4)     // Catch:{ all -> 0x01ff }
            long r6 = r5.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            long r8 = r26.zznq()     // Catch:{ all -> 0x01ff }
            int r6 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r6 >= 0) goto L_0x0e0f
            long r6 = r5.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            r0 = r26
            r0.zzao(r6)     // Catch:{ all -> 0x01ff }
        L_0x0e0f:
            long r6 = r5.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            long r8 = r26.zznr()     // Catch:{ all -> 0x01ff }
            int r6 = (r6 > r8 ? 1 : (r6 == r8 ? 0 : -1))
            if (r6 <= 0) goto L_0x0e24
            long r6 = r5.getTimestampMillis()     // Catch:{ all -> 0x01ff }
            r0 = r26
            r0.zzap(r6)     // Catch:{ all -> 0x01ff }
        L_0x0e24:
            int r4 = r4 + 1
            goto L_0x0dee
        L_0x0e27:
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r4 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r8 = r4.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzx r4 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzf r9 = r4.zzab(r8)     // Catch:{ all -> 0x01ff }
            if (r9 != 0) goto L_0x0ed7
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgk()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "Bundling raw events w/o app info. appId"
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r6)     // Catch:{ all -> 0x01ff }
            r4.zza(r5, r6)     // Catch:{ all -> 0x01ff }
        L_0x0e56:
            int r4 = r26.zznm()     // Catch:{ all -> 0x01ff }
            if (r4 <= 0) goto L_0x0e9f
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            r4.zzae()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzfd r4 = r34.zzgz()     // Catch:{ all -> 0x01ff }
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r5 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = r5.zzag()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbw r4 = r4.zzaw(r5)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0e79
            java.lang.Long r5 = r4.zzzk     // Catch:{ all -> 0x01ff }
            if (r5 != 0) goto L_0x0f5c
        L_0x0e79:
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r4 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r4 = r4.getGmpAppId()     // Catch:{ all -> 0x01ff }
            boolean r4 = android.text.TextUtils.isEmpty(r4)     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0f3d
            r4 = -1
            r0 = r26
            r0.zzav(r4)     // Catch:{ all -> 0x01ff }
        L_0x0e8e:
            com.google.android.gms.measurement.internal.zzx r5 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzgi r4 = r26.zzug()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzey r4 = (com.google.android.gms.internal.measurement.zzey) r4     // Catch:{ all -> 0x01ff }
            com.google.android.gms.internal.measurement.zzbs$zzg r4 = (com.google.android.gms.internal.measurement.zzbs.zzg) r4     // Catch:{ all -> 0x01ff }
            r0 = r22
            r5.zza(r4, r0)     // Catch:{ all -> 0x01ff }
        L_0x0e9f:
            com.google.android.gms.measurement.internal.zzx r6 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            r0 = r25
            java.util.List<java.lang.Long> r7 = r0.zzto     // Catch:{ all -> 0x01ff }
            com.google.android.gms.common.internal.Preconditions.checkNotNull(r7)     // Catch:{ all -> 0x01ff }
            r6.zzo()     // Catch:{ all -> 0x01ff }
            r6.zzbi()     // Catch:{ all -> 0x01ff }
            java.lang.StringBuilder r9 = new java.lang.StringBuilder     // Catch:{ all -> 0x01ff }
            java.lang.String r4 = "rowid in ("
            r9.<init>(r4)     // Catch:{ all -> 0x01ff }
            r4 = 0
            r5 = r4
        L_0x0eb9:
            int r4 = r7.size()     // Catch:{ all -> 0x01ff }
            if (r5 >= r4) goto L_0x0f69
            if (r5 == 0) goto L_0x0ec6
            java.lang.String r4 = ","
            r9.append(r4)     // Catch:{ all -> 0x01ff }
        L_0x0ec6:
            java.lang.Object r4 = r7.get(r5)     // Catch:{ all -> 0x01ff }
            java.lang.Long r4 = (java.lang.Long) r4     // Catch:{ all -> 0x01ff }
            long r10 = r4.longValue()     // Catch:{ all -> 0x01ff }
            r9.append(r10)     // Catch:{ all -> 0x01ff }
            int r4 = r5 + 1
            r5 = r4
            goto L_0x0eb9
        L_0x0ed7:
            int r4 = r26.zznm()     // Catch:{ all -> 0x01ff }
            if (r4 <= 0) goto L_0x0e56
            long r6 = r9.zzak()     // Catch:{ all -> 0x01ff }
            r4 = 0
            int r4 = (r6 > r4 ? 1 : (r6 == r4 ? 0 : -1))
            if (r4 == 0) goto L_0x0f31
            r0 = r26
            r0.zzar(r6)     // Catch:{ all -> 0x01ff }
        L_0x0eec:
            long r4 = r9.zzaj()     // Catch:{ all -> 0x01ff }
            r10 = 0
            int r10 = (r4 > r10 ? 1 : (r4 == r10 ? 0 : -1))
            if (r10 != 0) goto L_0x0ef7
            r4 = r6
        L_0x0ef7:
            r6 = 0
            int r6 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1))
            if (r6 == 0) goto L_0x0f35
            r0 = r26
            r0.zzaq(r4)     // Catch:{ all -> 0x01ff }
        L_0x0f02:
            r9.zzau()     // Catch:{ all -> 0x01ff }
            long r4 = r9.zzar()     // Catch:{ all -> 0x01ff }
            int r4 = (int) r4     // Catch:{ all -> 0x01ff }
            r0 = r26
            r0.zzu(r4)     // Catch:{ all -> 0x01ff }
            long r4 = r26.zznq()     // Catch:{ all -> 0x01ff }
            r9.zze(r4)     // Catch:{ all -> 0x01ff }
            long r4 = r26.zznr()     // Catch:{ all -> 0x01ff }
            r9.zzf(r4)     // Catch:{ all -> 0x01ff }
            java.lang.String r4 = r9.zzbc()     // Catch:{ all -> 0x01ff }
            if (r4 == 0) goto L_0x0f39
            r0 = r26
            r0.zzcl(r4)     // Catch:{ all -> 0x01ff }
        L_0x0f28:
            com.google.android.gms.measurement.internal.zzx r4 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            r4.zza(r9)     // Catch:{ all -> 0x01ff }
            goto L_0x0e56
        L_0x0f31:
            r26.zznt()     // Catch:{ all -> 0x01ff }
            goto L_0x0eec
        L_0x0f35:
            r26.zzns()     // Catch:{ all -> 0x01ff }
            goto L_0x0f02
        L_0x0f39:
            r26.zznu()     // Catch:{ all -> 0x01ff }
            goto L_0x0f28
        L_0x0f3d:
            r0 = r34
            com.google.android.gms.measurement.internal.zzfj r4 = r0.zzj     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzef r4 = r4.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r4 = r4.zzgn()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "Did not find measurement config or missing version info. appId"
            r0 = r25
            com.google.android.gms.internal.measurement.zzbs$zzg r6 = r0.zztn     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = r6.zzag()     // Catch:{ all -> 0x01ff }
            java.lang.Object r6 = com.google.android.gms.measurement.internal.zzef.zzam(r6)     // Catch:{ all -> 0x01ff }
            r4.zza(r5, r6)     // Catch:{ all -> 0x01ff }
            goto L_0x0e8e
        L_0x0f5c:
            java.lang.Long r4 = r4.zzzk     // Catch:{ all -> 0x01ff }
            long r4 = r4.longValue()     // Catch:{ all -> 0x01ff }
            r0 = r26
            r0.zzav(r4)     // Catch:{ all -> 0x01ff }
            goto L_0x0e8e
        L_0x0f69:
            java.lang.String r4 = ")"
            r9.append(r4)     // Catch:{ all -> 0x01ff }
            android.database.sqlite.SQLiteDatabase r4 = r6.getWritableDatabase()     // Catch:{ all -> 0x01ff }
            java.lang.String r5 = "raw_events"
            java.lang.String r9 = r9.toString()     // Catch:{ all -> 0x01ff }
            r10 = 0
            int r4 = r4.delete(r5, r9, r10)     // Catch:{ all -> 0x01ff }
            int r5 = r7.size()     // Catch:{ all -> 0x01ff }
            if (r4 == r5) goto L_0x0f9c
            com.google.android.gms.measurement.internal.zzef r5 = r6.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r5 = r5.zzgk()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "Deleted fewer rows from raw events table than expected"
            java.lang.Integer r4 = java.lang.Integer.valueOf(r4)     // Catch:{ all -> 0x01ff }
            int r7 = r7.size()     // Catch:{ all -> 0x01ff }
            java.lang.Integer r7 = java.lang.Integer.valueOf(r7)     // Catch:{ all -> 0x01ff }
            r5.zza(r6, r4, r7)     // Catch:{ all -> 0x01ff }
        L_0x0f9c:
            com.google.android.gms.measurement.internal.zzx r5 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            android.database.sqlite.SQLiteDatabase r4 = r5.getWritableDatabase()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "delete from raw_events_metadata where app_id=? and metadata_fingerprint not in (select distinct metadata_fingerprint from raw_events where app_id=?)"
            r7 = 2
            java.lang.String[] r7 = new java.lang.String[r7]     // Catch:{ SQLiteException -> 0x0fc2 }
            r9 = 0
            r7[r9] = r8     // Catch:{ SQLiteException -> 0x0fc2 }
            r9 = 1
            r7[r9] = r8     // Catch:{ SQLiteException -> 0x0fc2 }
            r4.execSQL(r6, r7)     // Catch:{ SQLiteException -> 0x0fc2 }
        L_0x0fb2:
            com.google.android.gms.measurement.internal.zzx r4 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            r4.setTransactionSuccessful()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzx r4 = r34.zzgy()
            r4.endTransaction()
            r4 = 1
        L_0x0fc1:
            return r4
        L_0x0fc2:
            r4 = move-exception
            com.google.android.gms.measurement.internal.zzef r5 = r5.zzab()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzeh r5 = r5.zzgk()     // Catch:{ all -> 0x01ff }
            java.lang.String r6 = "Failed to remove unused event metadata. appId"
            java.lang.Object r7 = com.google.android.gms.measurement.internal.zzef.zzam(r8)     // Catch:{ all -> 0x01ff }
            r5.zza(r6, r7, r4)     // Catch:{ all -> 0x01ff }
            goto L_0x0fb2
        L_0x0fd5:
            com.google.android.gms.measurement.internal.zzx r4 = r34.zzgy()     // Catch:{ all -> 0x01ff }
            r4.setTransactionSuccessful()     // Catch:{ all -> 0x01ff }
            com.google.android.gms.measurement.internal.zzx r4 = r34.zzgy()
            r4.endTransaction()
            r4 = 0
            goto L_0x0fc1
        L_0x0fe5:
            r5 = r6
            goto L_0x0d50
        L_0x0fe8:
            r6 = r5
            goto L_0x0c15
        L_0x0feb:
            r6 = r5
            goto L_0x0c15
        L_0x0fee:
            r5 = 1
            r23 = r5
            goto L_0x0b0a
        L_0x0ff3:
            r4 = 0
            goto L_0x08de
        L_0x0ff6:
            r12 = r19
            r7 = r16
            r8 = r17
            r5 = r15
            goto L_0x06e3
        L_0x0fff:
            r4 = 0
            goto L_0x0735
        L_0x1002:
            r4 = r7
            goto L_0x05dd
        L_0x1005:
            r22 = 1
            goto L_0x0504
        L_0x1009:
            r4 = r15
            r6 = r22
            r7 = r16
            r8 = r17
            r9 = r18
            r10 = r20
            r12 = r19
            goto L_0x018e
        L_0x1018:
            r10 = r20
            goto L_0x0716
        L_0x101c:
            r4 = move-exception
            r14 = r6
            r5 = r8
            goto L_0x031d
        L_0x1021:
            r4 = move-exception
            r14 = r6
            r5 = r7
            goto L_0x031d
        L_0x1026:
            r4 = move-exception
            r14 = r6
            r5 = r7
            goto L_0x031d
        L_0x102b:
            r4 = move-exception
            goto L_0x0397
        L_0x102e:
            r4 = move-exception
            r5 = r7
            goto L_0x0396
        L_0x1032:
            r4 = move-exception
            goto L_0x0396
        L_0x1035:
            r4 = move-exception
            r5 = r15
            goto L_0x031d
        L_0x1039:
            r5 = r6
            goto L_0x0999
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.measurement.internal.zzjg.zzd(java.lang.String, long):boolean");
    }

    /* access modifiers changed from: private */
    @WorkerThread
    public final zzf zzg(zzn zzn) {
        zzo();
        zzjj();
        Preconditions.checkNotNull(zzn);
        Preconditions.checkNotEmpty(zzn.packageName);
        zzf zzab = zzgy().zzab(zzn.packageName);
        String zzaq = this.zzj.zzac().zzaq(zzn.packageName);
        boolean z = false;
        if (zzab == null) {
            zzab = new zzf(this.zzj, zzn.packageName);
            zzab.zza(this.zzj.zzz().zzjy());
            zzab.zzd(zzaq);
            z = true;
        } else if (!zzaq.equals(zzab.zzai())) {
            zzab.zzd(zzaq);
            zzab.zza(this.zzj.zzz().zzjy());
            z = true;
        }
        if (!TextUtils.equals(zzn.zzcg, zzab.getGmpAppId())) {
            zzab.zzb(zzn.zzcg);
            z = true;
        }
        if (!TextUtils.equals(zzn.zzcu, zzab.zzah())) {
            zzab.zzc(zzn.zzcu);
            z = true;
        }
        if (!TextUtils.isEmpty(zzn.zzci) && !zzn.zzci.equals(zzab.getFirebaseInstanceId())) {
            zzab.zze(zzn.zzci);
            z = true;
        }
        if (!(zzn.zzr == 0 || zzn.zzr == zzab.zzao())) {
            zzab.zzh(zzn.zzr);
            z = true;
        }
        if (!TextUtils.isEmpty(zzn.zzcm) && !zzn.zzcm.equals(zzab.zzal())) {
            zzab.zzf(zzn.zzcm);
            z = true;
        }
        if (zzn.zzcn != zzab.zzam()) {
            zzab.zzg(zzn.zzcn);
            z = true;
        }
        if (zzn.zzco != null && !zzn.zzco.equals(zzab.zzan())) {
            zzab.zzg(zzn.zzco);
            z = true;
        }
        if (zzn.zzcp != zzab.zzap()) {
            zzab.zzi(zzn.zzcp);
            z = true;
        }
        if (zzn.zzcq != zzab.isMeasurementEnabled()) {
            zzab.setMeasurementEnabled(zzn.zzcq);
            z = true;
        }
        if (!TextUtils.isEmpty(zzn.zzdp) && !zzn.zzdp.equals(zzab.zzbb())) {
            zzab.zzh(zzn.zzdp);
            z = true;
        }
        if (zzn.zzcr != zzab.zzbd()) {
            zzab.zzt(zzn.zzcr);
            z = true;
        }
        if (zzn.zzcs != zzab.zzbe()) {
            zzab.zzb(zzn.zzcs);
            z = true;
        }
        if (zzn.zzct != zzab.zzbf()) {
            zzab.zzc(zzn.zzct);
            z = true;
        }
        if (this.zzj.zzad().zze(zzn.packageName, zzak.zzij) && zzn.zzcv != zzab.zzbg()) {
            zzab.zza(zzn.zzcv);
            z = true;
        }
        if (!(zzn.zzs == 0 || zzn.zzs == zzab.zzaq())) {
            zzab.zzj(zzn.zzs);
            z = true;
        }
        if (z) {
            zzgy().zza(zzab);
        }
        return zzab;
    }

    private final zzem zzjg() {
        if (this.zzsr != null) {
            return this.zzsr;
        }
        throw new IllegalStateException("Network broadcast receiver not created");
    }

    private final zzjc zzjh() {
        zza((zzjh) this.zzss);
        return this.zzss;
    }

    private final long zzjk() {
        long currentTimeMillis = this.zzj.zzx().currentTimeMillis();
        zzeo zzac = this.zzj.zzac();
        zzac.zzbi();
        zzac.zzo();
        long j = zzac.zzln.get();
        if (j == 0) {
            j = 1 + ((long) zzac.zzz().zzjw().nextInt(86400000));
            zzac.zzln.set(j);
        }
        return ((((j + currentTimeMillis) / 1000) / 60) / 60) / 24;
    }

    private final boolean zzjm() {
        zzo();
        zzjj();
        return zzgy().zzcd() || !TextUtils.isEmpty(zzgy().zzby());
    }

    @WorkerThread
    private final void zzjn() {
        long max;
        long j;
        zzo();
        zzjj();
        if (zzjr() || this.zzj.zzad().zza(zzak.zzim)) {
            if (this.zzsy > 0) {
                long abs = DateUtils.MILLIS_PER_HOUR - Math.abs(this.zzj.zzx().elapsedRealtime() - this.zzsy);
                if (abs > 0) {
                    this.zzj.zzab().zzgs().zza("Upload has been suspended. Will update scheduling later in approximately ms", Long.valueOf(abs));
                    zzjg().unregister();
                    zzjh().cancel();
                    return;
                }
                this.zzsy = 0;
            }
            if (!this.zzj.zzie() || !zzjm()) {
                this.zzj.zzab().zzgs().zzao("Nothing to upload or uploading impossible");
                zzjg().unregister();
                zzjh().cancel();
                return;
            }
            long currentTimeMillis = this.zzj.zzx().currentTimeMillis();
            long max2 = Math.max(0, ((Long) zzak.zzhf.get(null)).longValue());
            boolean z = zzgy().zzce() || zzgy().zzbz();
            if (z) {
                String zzbu = this.zzj.zzad().zzbu();
                max = (TextUtils.isEmpty(zzbu) || ".none.".equals(zzbu)) ? Math.max(0, ((Long) zzak.zzgz.get(null)).longValue()) : Math.max(0, ((Long) zzak.zzha.get(null)).longValue());
            } else {
                max = Math.max(0, ((Long) zzak.zzgy.get(null)).longValue());
            }
            long j2 = this.zzj.zzac().zzlj.get();
            long j3 = this.zzj.zzac().zzlk.get();
            long max3 = Math.max(zzgy().zzcb(), zzgy().zzcc());
            if (max3 == 0) {
                j = 0;
            } else {
                long abs2 = currentTimeMillis - Math.abs(max3 - currentTimeMillis);
                long abs3 = currentTimeMillis - Math.abs(j3 - currentTimeMillis);
                long max4 = Math.max(currentTimeMillis - Math.abs(j2 - currentTimeMillis), abs3);
                long j4 = abs2 + max2;
                if (z && max4 > 0) {
                    j4 = Math.min(abs2, max4) + max;
                }
                long j5 = !zzgw().zzb(max4, max) ? max + max4 : j4;
                if (abs3 != 0 && abs3 >= abs2) {
                    int i = 0;
                    while (true) {
                        long j6 = j5;
                        if (i >= Math.min(20, Math.max(0, ((Integer) zzak.zzhh.get(null)).intValue()))) {
                            j = 0;
                            break;
                        }
                        j5 = (Math.max(0, ((Long) zzak.zzhg.get(null)).longValue()) * (1 << i)) + j6;
                        if (j5 > abs3) {
                            break;
                        }
                        i++;
                    }
                }
                j = j5;
            }
            if (j == 0) {
                this.zzj.zzab().zzgs().zzao("Next upload time is 0");
                zzjg().unregister();
                zzjh().cancel();
            } else if (!zzjf().zzgv()) {
                this.zzj.zzab().zzgs().zzao("No network");
                zzjg().zzha();
                zzjh().cancel();
            } else {
                long j7 = this.zzj.zzac().zzll.get();
                long max5 = Math.max(0, ((Long) zzak.zzgw.get(null)).longValue());
                long j8 = !zzgw().zzb(j7, max5) ? Math.max(j, max5 + j7) : j;
                zzjg().unregister();
                long currentTimeMillis2 = j8 - this.zzj.zzx().currentTimeMillis();
                if (currentTimeMillis2 <= 0) {
                    currentTimeMillis2 = Math.max(0, ((Long) zzak.zzhb.get(null)).longValue());
                    this.zzj.zzac().zzlj.set(this.zzj.zzx().currentTimeMillis());
                }
                this.zzj.zzab().zzgs().zza("Upload scheduled in approximately ms", Long.valueOf(currentTimeMillis2));
                zzjh().zzv(currentTimeMillis2);
            }
        }
    }

    @WorkerThread
    private final void zzjo() {
        zzo();
        if (this.zztc || this.zztd || this.zzte) {
            this.zzj.zzab().zzgs().zza("Not stopping services. fetch, network, upload", Boolean.valueOf(this.zztc), Boolean.valueOf(this.zztd), Boolean.valueOf(this.zzte));
            return;
        }
        this.zzj.zzab().zzgs().zzao("Stopping uploading service(s)");
        if (this.zzsz != null) {
            for (Runnable run : this.zzsz) {
                run.run();
            }
            this.zzsz.clear();
        }
    }

    @WorkerThread
    @VisibleForTesting
    private final boolean zzjp() {
        zzo();
        if (!this.zzj.zzad().zza(zzak.zzjh) || this.zztf == null || !this.zztf.isValid()) {
            try {
                this.zztg = new RandomAccessFile(new File(this.zzj.getContext().getFilesDir(), "google_app_measurement.db"), "rw").getChannel();
                this.zztf = this.zztg.tryLock();
                if (this.zztf != null) {
                    this.zzj.zzab().zzgs().zzao("Storage concurrent access okay");
                    return true;
                }
                this.zzj.zzab().zzgk().zzao("Storage concurrent data access panic");
                return false;
            } catch (FileNotFoundException e) {
                this.zzj.zzab().zzgk().zza("Failed to acquire storage lock", e);
            } catch (IOException e2) {
                this.zzj.zzab().zzgk().zza("Failed to access storage lock file", e2);
            } catch (OverlappingFileLockException e3) {
                this.zzj.zzab().zzgn().zza("Storage lock already acquired", e3);
            }
        } else {
            this.zzj.zzab().zzgs().zzao("Storage concurrent access okay");
            return true;
        }
    }

    @WorkerThread
    private final boolean zzjr() {
        zzo();
        zzjj();
        return this.zzsw;
    }

    public static zzjg zzm(Context context) {
        Preconditions.checkNotNull(context);
        Preconditions.checkNotNull(context.getApplicationContext());
        if (zzsn == null) {
            synchronized (zzjg.class) {
                try {
                    if (zzsn == null) {
                        zzsn = new zzjg(new zzjm(context));
                    }
                } finally {
                    while (true) {
                        Class<zzjg> cls = zzjg.class;
                    }
                }
            }
        }
        return zzsn;
    }

    @WorkerThread
    private final void zzo() {
        this.zzj.zzaa().zzo();
    }

    public final Context getContext() {
        return this.zzj.getContext();
    }

    /* access modifiers changed from: protected */
    @WorkerThread
    public final void start() {
        this.zzj.zzaa().zzo();
        zzgy().zzca();
        if (this.zzj.zzac().zzlj.get() == 0) {
            this.zzj.zzac().zzlj.set(this.zzj.zzx().currentTimeMillis());
        }
        zzjn();
    }

    /* JADX INFO: finally extract failed */
    /* access modifiers changed from: 0000 */
    @WorkerThread
    @VisibleForTesting
    public final void zza(int i, Throwable th, byte[] bArr, String str) {
        zzx zzgy;
        zzo();
        zzjj();
        if (bArr == null) {
            try {
                bArr = new byte[0];
            } catch (Throwable th2) {
                this.zztd = false;
                zzjo();
                throw th2;
            }
        }
        List<Long> list = this.zzth;
        this.zzth = null;
        if ((i == 200 || i == 204) && th == null) {
            try {
                this.zzj.zzac().zzlj.set(this.zzj.zzx().currentTimeMillis());
                this.zzj.zzac().zzlk.set(0);
                zzjn();
                this.zzj.zzab().zzgs().zza("Successful upload. Got network response. code, size", Integer.valueOf(i), Integer.valueOf(bArr.length));
                zzgy().beginTransaction();
                try {
                    for (Long l : list) {
                        try {
                            zzgy = zzgy();
                            long longValue = l.longValue();
                            zzgy.zzo();
                            zzgy.zzbi();
                            if (zzgy.getWritableDatabase().delete("queue", "rowid=?", new String[]{String.valueOf(longValue)}) != 1) {
                                throw new SQLiteException("Deleted fewer rows from queue than expected");
                            }
                        } catch (SQLiteException e) {
                            zzgy.zzab().zzgk().zza("Failed to delete a bundle in a queue table", e);
                            throw e;
                        } catch (SQLiteException e2) {
                            if (this.zzti == null || !this.zzti.contains(l)) {
                                throw e2;
                            }
                        }
                    }
                    zzgy().setTransactionSuccessful();
                    zzgy().endTransaction();
                    this.zzti = null;
                    if (!zzjf().zzgv() || !zzjm()) {
                        this.zztj = -1;
                        zzjn();
                    } else {
                        zzjl();
                    }
                    this.zzsy = 0;
                } catch (Throwable th3) {
                    zzgy().endTransaction();
                    throw th3;
                }
            } catch (SQLiteException e3) {
                this.zzj.zzab().zzgk().zza("Database error while trying to delete uploaded bundles", e3);
                this.zzsy = this.zzj.zzx().elapsedRealtime();
                this.zzj.zzab().zzgs().zza("Disable upload, time", Long.valueOf(this.zzsy));
            }
        } else {
            this.zzj.zzab().zzgs().zza("Network upload failed. Will retry later. code, error", Integer.valueOf(i), th);
            this.zzj.zzac().zzlk.set(this.zzj.zzx().currentTimeMillis());
            if (i == 503 || i == 429) {
                this.zzj.zzac().zzll.set(this.zzj.zzx().currentTimeMillis());
            }
            zzgy().zzb(list);
            zzjn();
        }
        this.zztd = false;
        zzjo();
    }

    public final zzfc zzaa() {
        return this.zzj.zzaa();
    }

    public final zzef zzab() {
        return this.zzj.zzab();
    }

    public final zzs zzad() {
        return this.zzj.zzad();
    }

    public final zzr zzae() {
        return this.zzj.zzae();
    }

    /* access modifiers changed from: 0000 */
    public final void zzb(zzjh zzjh) {
        this.zzta++;
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzb(zzjn zzjn, zzn zzn) {
        long j;
        int i = 0;
        zzo();
        zzjj();
        if (TextUtils.isEmpty(zzn.zzcg) && TextUtils.isEmpty(zzn.zzcu)) {
            return;
        }
        if (!zzn.zzcq) {
            zzg(zzn);
            return;
        }
        int zzbm = this.zzj.zzz().zzbm(zzjn.name);
        if (zzbm != 0) {
            this.zzj.zzz();
            String zza2 = zzjs.zza(zzjn.name, 24, true);
            if (zzjn.name != null) {
                i = zzjn.name.length();
            }
            this.zzj.zzz().zza(zzn.packageName, zzbm, "_ev", zza2, i);
            return;
        }
        int zzc = this.zzj.zzz().zzc(zzjn.name, zzjn.getValue());
        if (zzc != 0) {
            this.zzj.zzz();
            String zza3 = zzjs.zza(zzjn.name, 24, true);
            Object value = zzjn.getValue();
            if (value != null && ((value instanceof String) || (value instanceof CharSequence))) {
                i = String.valueOf(value).length();
            }
            this.zzj.zzz().zza(zzn.packageName, zzc, "_ev", zza3, i);
            return;
        }
        Object zzd = this.zzj.zzz().zzd(zzjn.name, zzjn.getValue());
        if (zzd != null) {
            if ("_sid".equals(zzjn.name) && this.zzj.zzad().zzw(zzn.packageName)) {
                long j2 = zzjn.zztr;
                String str = zzjn.origin;
                zzjp zze = zzgy().zze(zzn.packageName, "_sno");
                if (zze == null || !(zze.value instanceof Long)) {
                    if (zze != null) {
                        this.zzj.zzab().zzgn().zza("Retrieved last session number from database does not contain a valid (long) value", zze.value);
                    }
                    if (this.zzj.zzad().zze(zzn.packageName, zzak.zzie)) {
                        zzae zzc2 = zzgy().zzc(zzn.packageName, "_s");
                        if (zzc2 != null) {
                            long j3 = zzc2.zzfg;
                            this.zzj.zzab().zzgs().zza("Backfill the session number. Last used session number", Long.valueOf(j3));
                            j = j3;
                        }
                    }
                    j = 0;
                } else {
                    j = ((Long) zze.value).longValue();
                }
                zzb(new zzjn("_sno", j2, Long.valueOf(j + 1), str), zzn);
            }
            zzjp zzjp = new zzjp(zzn.packageName, zzjn.origin, zzjn.name, zzjn.zztr, zzd);
            this.zzj.zzab().zzgr().zza("Setting user property", this.zzj.zzy().zzal(zzjp.name), zzd);
            zzgy().beginTransaction();
            try {
                zzg(zzn);
                boolean zza4 = zzgy().zza(zzjp);
                zzgy().setTransactionSuccessful();
                if (zza4) {
                    this.zzj.zzab().zzgr().zza("User property set", this.zzj.zzy().zzal(zzjp.name), zzjp.value);
                } else {
                    this.zzj.zzab().zzgk().zza("Too many unique user properties are set. Ignoring user property", this.zzj.zzy().zzal(zzjp.name), zzjp.value);
                    this.zzj.zzz().zza(zzn.packageName, 9, (String) null, (String) null, 0);
                }
            } finally {
                zzgy().endTransaction();
            }
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzb(zzq zzq, zzn zzn) {
        boolean z = true;
        Preconditions.checkNotNull(zzq);
        Preconditions.checkNotEmpty(zzq.packageName);
        Preconditions.checkNotNull(zzq.origin);
        Preconditions.checkNotNull(zzq.zzdw);
        Preconditions.checkNotEmpty(zzq.zzdw.name);
        zzo();
        zzjj();
        if (TextUtils.isEmpty(zzn.zzcg) && TextUtils.isEmpty(zzn.zzcu)) {
            return;
        }
        if (!zzn.zzcq) {
            zzg(zzn);
            return;
        }
        zzq zzq2 = new zzq(zzq);
        zzq2.active = false;
        zzgy().beginTransaction();
        try {
            zzq zzf = zzgy().zzf(zzq2.packageName, zzq2.zzdw.name);
            if (zzf != null && !zzf.origin.equals(zzq2.origin)) {
                this.zzj.zzab().zzgn().zza("Updating a conditional user property with different origin. name, origin, origin (from DB)", this.zzj.zzy().zzal(zzq2.zzdw.name), zzq2.origin, zzf.origin);
            }
            if (zzf != null && zzf.active) {
                zzq2.origin = zzf.origin;
                zzq2.creationTimestamp = zzf.creationTimestamp;
                zzq2.triggerTimeout = zzf.triggerTimeout;
                zzq2.triggerEventName = zzf.triggerEventName;
                zzq2.zzdy = zzf.zzdy;
                zzq2.active = zzf.active;
                zzq2.zzdw = new zzjn(zzq2.zzdw.name, zzf.zzdw.zztr, zzq2.zzdw.getValue(), zzf.zzdw.origin);
                z = false;
            } else if (TextUtils.isEmpty(zzq2.triggerEventName)) {
                zzq2.zzdw = new zzjn(zzq2.zzdw.name, zzq2.creationTimestamp, zzq2.zzdw.getValue(), zzq2.zzdw.origin);
                zzq2.active = true;
            } else {
                z = false;
            }
            if (zzq2.active) {
                zzjn zzjn = zzq2.zzdw;
                zzjp zzjp = new zzjp(zzq2.packageName, zzq2.origin, zzjn.name, zzjn.zztr, zzjn.getValue());
                if (zzgy().zza(zzjp)) {
                    this.zzj.zzab().zzgr().zza("User property updated immediately", zzq2.packageName, this.zzj.zzy().zzal(zzjp.name), zzjp.value);
                } else {
                    this.zzj.zzab().zzgk().zza("(2)Too many active user properties, ignoring", zzef.zzam(zzq2.packageName), this.zzj.zzy().zzal(zzjp.name), zzjp.value);
                }
                if (z && zzq2.zzdy != null) {
                    zzd(new zzai(zzq2.zzdy, zzq2.creationTimestamp), zzn);
                }
            }
            if (zzgy().zza(zzq2)) {
                this.zzj.zzab().zzgr().zza("Conditional property added", zzq2.packageName, this.zzj.zzy().zzal(zzq2.zzdw.name), zzq2.zzdw.getValue());
            } else {
                this.zzj.zzab().zzgk().zza("Too many conditional properties, ignoring", zzef.zzam(zzq2.packageName), this.zzj.zzy().zzal(zzq2.zzdw.name), zzq2.zzdw.getValue());
            }
            zzgy().setTransactionSuccessful();
        } finally {
            zzgy().endTransaction();
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    @VisibleForTesting
    public final void zzb(String str, int i, Throwable th, byte[] bArr, Map<String, List<String>> map) {
        boolean z = true;
        zzo();
        zzjj();
        Preconditions.checkNotEmpty(str);
        if (bArr == null) {
            try {
                bArr = new byte[0];
            } catch (Throwable th2) {
                this.zztc = false;
                zzjo();
                throw th2;
            }
        }
        this.zzj.zzab().zzgs().zza("onConfigFetched. Response size", Integer.valueOf(bArr.length));
        zzgy().beginTransaction();
        zzf zzab = zzgy().zzab(str);
        boolean z2 = (i == 200 || i == 204 || i == 304) && th == null;
        if (zzab == null) {
            this.zzj.zzab().zzgn().zza("App does not exist in onConfigFetched. appId", zzef.zzam(str));
        } else if (z2 || i == 404) {
            List list = map != null ? (List) map.get(HttpRequest.HEADER_LAST_MODIFIED) : null;
            String str2 = (list == null || list.size() <= 0) ? null : (String) list.get(0);
            if (i == 404 || i == 304) {
                if (zzgz().zzaw(str) == null && !zzgz().zza(str, null, null)) {
                    zzgy().endTransaction();
                    this.zztc = false;
                    zzjo();
                    return;
                }
            } else if (!zzgz().zza(str, bArr, str2)) {
                zzgy().endTransaction();
                this.zztc = false;
                zzjo();
                return;
            }
            zzab.zzl(this.zzj.zzx().currentTimeMillis());
            zzgy().zza(zzab);
            if (i == 404) {
                this.zzj.zzab().zzgp().zza("Config not found. Using empty config. appId", str);
            } else {
                this.zzj.zzab().zzgs().zza("Successfully fetched config. Got network response. code, size", Integer.valueOf(i), Integer.valueOf(bArr.length));
            }
            if (!zzjf().zzgv() || !zzjm()) {
                zzjn();
            } else {
                zzjl();
            }
        } else {
            zzab.zzm(this.zzj.zzx().currentTimeMillis());
            zzgy().zza(zzab);
            this.zzj.zzab().zzgs().zza("Fetching config failed. code, error", Integer.valueOf(i), th);
            zzgz().zzay(str);
            this.zzj.zzac().zzlk.set(this.zzj.zzx().currentTimeMillis());
            if (!(i == 503 || i == 429)) {
                z = false;
            }
            if (z) {
                this.zzj.zzac().zzll.set(this.zzj.zzx().currentTimeMillis());
            }
            zzjn();
        }
        zzgy().setTransactionSuccessful();
        zzgy().endTransaction();
        this.zztc = false;
        zzjo();
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzc(zzai zzai, zzn zzn) {
        List<zzq> zzb;
        List<zzq> zzb2;
        List<zzq> zzb3;
        Preconditions.checkNotNull(zzn);
        Preconditions.checkNotEmpty(zzn.packageName);
        zzo();
        zzjj();
        String str = zzn.packageName;
        long j = zzai.zzfu;
        if (zzgw().zze(zzai, zzn)) {
            if (!zzn.zzcq) {
                zzg(zzn);
                return;
            }
            if (this.zzj.zzad().zze(str, zzak.zzix) && zzn.zzcw != null) {
                if (zzn.zzcw.contains(zzai.name)) {
                    Bundle zzcv = zzai.zzfq.zzcv();
                    zzcv.putLong("ga_safelisted", 1);
                    zzai = new zzai(zzai.name, new zzah(zzcv), zzai.origin, zzai.zzfu);
                } else {
                    this.zzj.zzab().zzgr().zza("Dropping non-safelisted event. appId, event name, origin", str, zzai.name, zzai.origin);
                    return;
                }
            }
            zzgy().beginTransaction();
            try {
                zzx zzgy = zzgy();
                Preconditions.checkNotEmpty(str);
                zzgy.zzo();
                zzgy.zzbi();
                if (j < 0) {
                    zzgy.zzab().zzgn().zza("Invalid time querying timed out conditional properties", zzef.zzam(str), Long.valueOf(j));
                    zzb = Collections.emptyList();
                } else {
                    zzb = zzgy.zzb("active=0 and app_id=? and abs(? - creation_timestamp) > trigger_timeout", new String[]{str, String.valueOf(j)});
                }
                for (zzq zzq : zzb) {
                    if (zzq != null) {
                        this.zzj.zzab().zzgr().zza("User property timed out", zzq.packageName, this.zzj.zzy().zzal(zzq.zzdw.name), zzq.zzdw.getValue());
                        if (zzq.zzdx != null) {
                            zzd(new zzai(zzq.zzdx, j), zzn);
                        }
                        zzgy().zzg(str, zzq.zzdw.name);
                    }
                }
                zzx zzgy2 = zzgy();
                Preconditions.checkNotEmpty(str);
                zzgy2.zzo();
                zzgy2.zzbi();
                if (j < 0) {
                    zzgy2.zzab().zzgn().zza("Invalid time querying expired conditional properties", zzef.zzam(str), Long.valueOf(j));
                    zzb2 = Collections.emptyList();
                } else {
                    zzb2 = zzgy2.zzb("active<>0 and app_id=? and abs(? - triggered_timestamp) > time_to_live", new String[]{str, String.valueOf(j)});
                }
                ArrayList arrayList = new ArrayList(zzb2.size());
                for (zzq zzq2 : zzb2) {
                    if (zzq2 != null) {
                        this.zzj.zzab().zzgr().zza("User property expired", zzq2.packageName, this.zzj.zzy().zzal(zzq2.zzdw.name), zzq2.zzdw.getValue());
                        zzgy().zzd(str, zzq2.zzdw.name);
                        if (zzq2.zzdz != null) {
                            arrayList.add(zzq2.zzdz);
                        }
                        zzgy().zzg(str, zzq2.zzdw.name);
                    }
                }
                ArrayList arrayList2 = arrayList;
                int size = arrayList2.size();
                int i = 0;
                while (i < size) {
                    Object obj = arrayList2.get(i);
                    i++;
                    zzd(new zzai((zzai) obj, j), zzn);
                }
                zzx zzgy3 = zzgy();
                String str2 = zzai.name;
                Preconditions.checkNotEmpty(str);
                Preconditions.checkNotEmpty(str2);
                zzgy3.zzo();
                zzgy3.zzbi();
                if (j < 0) {
                    zzgy3.zzab().zzgn().zza("Invalid time querying triggered conditional properties", zzef.zzam(str), zzgy3.zzy().zzaj(str2), Long.valueOf(j));
                    zzb3 = Collections.emptyList();
                } else {
                    zzb3 = zzgy3.zzb("active=0 and app_id=? and trigger_event_name=? and abs(? - creation_timestamp) <= trigger_timeout", new String[]{str, str2, String.valueOf(j)});
                }
                ArrayList arrayList3 = new ArrayList(zzb3.size());
                for (zzq zzq3 : zzb3) {
                    if (zzq3 != null) {
                        zzjn zzjn = zzq3.zzdw;
                        zzjp zzjp = new zzjp(zzq3.packageName, zzq3.origin, zzjn.name, j, zzjn.getValue());
                        if (zzgy().zza(zzjp)) {
                            this.zzj.zzab().zzgr().zza("User property triggered", zzq3.packageName, this.zzj.zzy().zzal(zzjp.name), zzjp.value);
                        } else {
                            this.zzj.zzab().zzgk().zza("Too many active user properties, ignoring", zzef.zzam(zzq3.packageName), this.zzj.zzy().zzal(zzjp.name), zzjp.value);
                        }
                        if (zzq3.zzdy != null) {
                            arrayList3.add(zzq3.zzdy);
                        }
                        zzq3.zzdw = new zzjn(zzjp);
                        zzq3.active = true;
                        zzgy().zza(zzq3);
                    }
                }
                zzd(zzai, zzn);
                ArrayList arrayList4 = arrayList3;
                int size2 = arrayList4.size();
                int i2 = 0;
                while (i2 < size2) {
                    Object obj2 = arrayList4.get(i2);
                    i2++;
                    zzd(new zzai((zzai) obj2, j), zzn);
                }
                zzgy().setTransactionSuccessful();
            } finally {
                zzgy().endTransaction();
            }
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzc(zzjn zzjn, zzn zzn) {
        zzo();
        zzjj();
        if (TextUtils.isEmpty(zzn.zzcg) && TextUtils.isEmpty(zzn.zzcu)) {
            return;
        }
        if (!zzn.zzcq) {
            zzg(zzn);
        } else if (!this.zzj.zzad().zze(zzn.packageName, zzak.zzij)) {
            this.zzj.zzab().zzgr().zza("Removing user property", this.zzj.zzy().zzal(zzjn.name));
            zzgy().beginTransaction();
            try {
                zzg(zzn);
                zzgy().zzd(zzn.packageName, zzjn.name);
                zzgy().setTransactionSuccessful();
                this.zzj.zzab().zzgr().zza("User property removed", this.zzj.zzy().zzal(zzjn.name));
            } finally {
                zzgy().endTransaction();
            }
        } else if (!"_npa".equals(zzjn.name) || zzn.zzcv == null) {
            this.zzj.zzab().zzgr().zza("Removing user property", this.zzj.zzy().zzal(zzjn.name));
            zzgy().beginTransaction();
            try {
                zzg(zzn);
                zzgy().zzd(zzn.packageName, zzjn.name);
                zzgy().setTransactionSuccessful();
                this.zzj.zzab().zzgr().zza("User property removed", this.zzj.zzy().zzal(zzjn.name));
            } finally {
                zzgy().endTransaction();
            }
        } else {
            this.zzj.zzab().zzgr().zzao("Falling back to manifest metadata value for ad personalization");
            zzb(new zzjn("_npa", this.zzj.zzx().currentTimeMillis(), Long.valueOf(zzn.zzcv.booleanValue() ? 1 : 0), "auto"), zzn);
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzc(zzq zzq, zzn zzn) {
        Preconditions.checkNotNull(zzq);
        Preconditions.checkNotEmpty(zzq.packageName);
        Preconditions.checkNotNull(zzq.zzdw);
        Preconditions.checkNotEmpty(zzq.zzdw.name);
        zzo();
        zzjj();
        if (TextUtils.isEmpty(zzn.zzcg) && TextUtils.isEmpty(zzn.zzcu)) {
            return;
        }
        if (!zzn.zzcq) {
            zzg(zzn);
            return;
        }
        zzgy().beginTransaction();
        try {
            zzg(zzn);
            zzq zzf = zzgy().zzf(zzq.packageName, zzq.zzdw.name);
            if (zzf != null) {
                this.zzj.zzab().zzgr().zza("Removing conditional user property", zzq.packageName, this.zzj.zzy().zzal(zzq.zzdw.name));
                zzgy().zzg(zzq.packageName, zzq.zzdw.name);
                if (zzf.active) {
                    zzgy().zzd(zzq.packageName, zzq.zzdw.name);
                }
                if (zzq.zzdz != null) {
                    Bundle bundle = null;
                    if (zzq.zzdz.zzfq != null) {
                        bundle = zzq.zzdz.zzfq.zzcv();
                    }
                    zzd(this.zzj.zzz().zza(zzq.packageName, zzq.zzdz.name, bundle, zzf.origin, zzq.zzdz.zzfu, true, false), zzn);
                }
            } else {
                this.zzj.zzab().zzgn().zza("Conditional user property doesn't exist", zzef.zzam(zzq.packageName), this.zzj.zzy().zzal(zzq.zzdw.name));
            }
            zzgy().setTransactionSuccessful();
        } finally {
            zzgy().endTransaction();
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzd(zzai zzai, String str) {
        zzf zzab = zzgy().zzab(str);
        if (zzab == null || TextUtils.isEmpty(zzab.zzal())) {
            this.zzj.zzab().zzgr().zza("No app data available; dropping event", str);
            return;
        }
        Boolean zzc = zzc(zzab);
        if (zzc == null) {
            if (!"_ui".equals(zzai.name)) {
                this.zzj.zzab().zzgn().zza("Could not find package. appId", zzef.zzam(str));
            }
        } else if (!zzc.booleanValue()) {
            this.zzj.zzab().zzgk().zza("App version does not match; dropping event. appId", zzef.zzam(str));
            return;
        }
        zzai zzai2 = zzai;
        zzc(zzai2, new zzn(str, zzab.getGmpAppId(), zzab.zzal(), zzab.zzam(), zzab.zzan(), zzab.zzao(), zzab.zzap(), (String) null, zzab.isMeasurementEnabled(), false, zzab.getFirebaseInstanceId(), zzab.zzbd(), 0, 0, zzab.zzbe(), zzab.zzbf(), false, zzab.zzah(), zzab.zzbg(), zzab.zzaq(), zzab.zzbh()));
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    @VisibleForTesting
    public final void zzd(zzn zzn) {
        if (this.zzth != null) {
            this.zzti = new ArrayList();
            this.zzti.addAll(this.zzth);
        }
        zzx zzgy = zzgy();
        String str = zzn.packageName;
        Preconditions.checkNotEmpty(str);
        zzgy.zzo();
        zzgy.zzbi();
        try {
            SQLiteDatabase writableDatabase = zzgy.getWritableDatabase();
            String[] strArr = new String[1];
            strArr[0] = str;
            int delete = writableDatabase.delete("apps", "app_id=?", strArr);
            int delete2 = writableDatabase.delete("events", "app_id=?", strArr);
            int delete3 = writableDatabase.delete("user_attributes", "app_id=?", strArr);
            int delete4 = writableDatabase.delete("conditional_properties", "app_id=?", strArr);
            int delete5 = writableDatabase.delete("raw_events", "app_id=?", strArr);
            int delete6 = writableDatabase.delete("raw_events_metadata", "app_id=?", strArr);
            int delete7 = writableDatabase.delete("queue", "app_id=?", strArr);
            int delete8 = writableDatabase.delete("main_event_params", "app_id=?", strArr) + delete + 0 + delete2 + delete3 + delete4 + delete5 + delete6 + delete7 + writableDatabase.delete("audience_filter_values", "app_id=?", strArr);
            if (delete8 > 0) {
                zzgy.zzab().zzgs().zza("Reset analytics data. app, records", str, Integer.valueOf(delete8));
            }
        } catch (SQLiteException e) {
            zzgy.zzab().zzgk().zza("Error resetting analytics data. appId, error", zzef.zzam(str), e);
        }
        zzn zza2 = zza(this.zzj.getContext(), zzn.packageName, zzn.zzcg, zzn.zzcq, zzn.zzcs, zzn.zzct, zzn.zzdr, zzn.zzcu);
        if (zzn.zzcq) {
            zzf(zza2);
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zze(zzn zzn) {
        zzo();
        zzjj();
        Preconditions.checkNotEmpty(zzn.packageName);
        zzg(zzn);
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zze(zzq zzq) {
        zzn zzbi = zzbi(zzq.packageName);
        if (zzbi != null) {
            zzb(zzq, zzbi);
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzf(zzn zzn) {
        int i;
        zzf zzab;
        PackageInfo packageInfo;
        ApplicationInfo applicationInfo;
        boolean z;
        zzx zzgy;
        String zzag;
        zzo();
        zzjj();
        Preconditions.checkNotNull(zzn);
        Preconditions.checkNotEmpty(zzn.packageName);
        if (!TextUtils.isEmpty(zzn.zzcg) || !TextUtils.isEmpty(zzn.zzcu)) {
            zzf zzab2 = zzgy().zzab(zzn.packageName);
            if (zzab2 != null && TextUtils.isEmpty(zzab2.getGmpAppId()) && !TextUtils.isEmpty(zzn.zzcg)) {
                zzab2.zzl(0);
                zzgy().zza(zzab2);
                zzgz().zzaz(zzn.packageName);
            }
            if (!zzn.zzcq) {
                zzg(zzn);
                return;
            }
            long j = zzn.zzdr;
            if (j == 0) {
                j = this.zzj.zzx().currentTimeMillis();
            }
            if (this.zzj.zzad().zze(zzn.packageName, zzak.zzij)) {
                this.zzj.zzw().zzct();
            }
            int i2 = zzn.zzds;
            if (i2 == 0 || i2 == 1) {
                i = i2;
            } else {
                this.zzj.zzab().zzgn().zza("Incorrect app type, assuming installed app. appId, appType", zzef.zzam(zzn.packageName), Integer.valueOf(i2));
                i = 0;
            }
            zzgy().beginTransaction();
            try {
                if (this.zzj.zzad().zze(zzn.packageName, zzak.zzij)) {
                    zzjp zze = zzgy().zze(zzn.packageName, "_npa");
                    if (zze == null || "auto".equals(zze.origin)) {
                        if (zzn.zzcv != null) {
                            zzjn zzjn = new zzjn("_npa", j, Long.valueOf(zzn.zzcv.booleanValue() ? 1 : 0), "auto");
                            if (zze == null || !zze.value.equals(zzjn.zzts)) {
                                zzb(zzjn, zzn);
                            }
                        } else if (zze != null) {
                            zzc(new zzjn("_npa", j, null, "auto"), zzn);
                        }
                    }
                }
                zzab = zzgy().zzab(zzn.packageName);
                if (zzab != null) {
                    this.zzj.zzz();
                    if (zzjs.zza(zzn.zzcg, zzab.getGmpAppId(), zzn.zzcu, zzab.zzah())) {
                        this.zzj.zzab().zzgn().zza("New GMP App Id passed in. Removing cached database data. appId", zzef.zzam(zzab.zzag()));
                        zzgy = zzgy();
                        zzag = zzab.zzag();
                        zzgy.zzbi();
                        zzgy.zzo();
                        Preconditions.checkNotEmpty(zzag);
                        SQLiteDatabase writableDatabase = zzgy.getWritableDatabase();
                        String[] strArr = new String[1];
                        strArr[0] = zzag;
                        int delete = writableDatabase.delete("events", "app_id=?", strArr);
                        int delete2 = writableDatabase.delete("user_attributes", "app_id=?", strArr);
                        int delete3 = writableDatabase.delete("conditional_properties", "app_id=?", strArr);
                        int delete4 = writableDatabase.delete("apps", "app_id=?", strArr);
                        int delete5 = writableDatabase.delete("raw_events", "app_id=?", strArr);
                        int delete6 = writableDatabase.delete("audience_filter_values", "app_id=?", strArr) + delete + 0 + delete2 + delete3 + delete4 + delete5 + writableDatabase.delete("raw_events_metadata", "app_id=?", strArr) + writableDatabase.delete("event_filters", "app_id=?", strArr) + writableDatabase.delete("property_filters", "app_id=?", strArr);
                        if (delete6 > 0) {
                            zzgy.zzab().zzgs().zza("Deleted application data. app, records", zzag, Integer.valueOf(delete6));
                        }
                        zzab = null;
                    }
                }
            } catch (SQLiteException e) {
                zzgy.zzab().zzgk().zza("Error deleting application data. appId, error", zzef.zzam(zzag), e);
            } catch (Throwable th) {
                zzgy().endTransaction();
                throw th;
            }
            if (zzab != null) {
                if (zzab.zzam() != -2147483648L) {
                    if (zzab.zzam() != zzn.zzcn) {
                        Bundle bundle = new Bundle();
                        bundle.putString("_pv", zzab.zzal());
                        zzc(new zzai("_au", new zzah(bundle), "auto", j), zzn);
                    }
                } else if (zzab.zzal() != null && !zzab.zzal().equals(zzn.zzcm)) {
                    Bundle bundle2 = new Bundle();
                    bundle2.putString("_pv", zzab.zzal());
                    zzc(new zzai("_au", new zzah(bundle2), "auto", j), zzn);
                }
            }
            zzg(zzn);
            zzae zzae = null;
            if (i == 0) {
                zzae = zzgy().zzc(zzn.packageName, "_f");
            } else if (i == 1) {
                zzae = zzgy().zzc(zzn.packageName, "_v");
            }
            if (zzae == null) {
                long j2 = (1 + (j / DateUtils.MILLIS_PER_HOUR)) * DateUtils.MILLIS_PER_HOUR;
                if (i == 0) {
                    zzb(new zzjn("_fot", j, Long.valueOf(j2), "auto"), zzn);
                    if (this.zzj.zzad().zzt(zzn.zzcg)) {
                        zzo();
                        this.zzj.zzht().zzat(zzn.packageName);
                    }
                    zzo();
                    zzjj();
                    Bundle bundle3 = new Bundle();
                    bundle3.putLong("_c", 1);
                    bundle3.putLong("_r", 1);
                    bundle3.putLong("_uwa", 0);
                    bundle3.putLong("_pfo", 0);
                    bundle3.putLong("_sys", 0);
                    bundle3.putLong("_sysu", 0);
                    if (this.zzj.zzad().zzz(zzn.packageName)) {
                        bundle3.putLong("_et", 1);
                    }
                    if (zzn.zzdt) {
                        bundle3.putLong("_dac", 1);
                    }
                    if (this.zzj.getContext().getPackageManager() == null) {
                        this.zzj.zzab().zzgk().zza("PackageManager is null, first open report might be inaccurate. appId", zzef.zzam(zzn.packageName));
                    } else {
                        try {
                            packageInfo = Wrappers.packageManager(this.zzj.getContext()).getPackageInfo(zzn.packageName, 0);
                        } catch (NameNotFoundException e2) {
                            this.zzj.zzab().zzgk().zza("Package info is null, first open report might be inaccurate. appId", zzef.zzam(zzn.packageName), e2);
                            packageInfo = null;
                        }
                        if (packageInfo != null) {
                            if (packageInfo.firstInstallTime != 0) {
                                if (packageInfo.firstInstallTime != packageInfo.lastUpdateTime) {
                                    bundle3.putLong("_uwa", 1);
                                    z = false;
                                } else {
                                    z = true;
                                }
                                zzb(new zzjn("_fi", j, Long.valueOf(z ? 1 : 0), "auto"), zzn);
                            }
                        }
                        try {
                            applicationInfo = Wrappers.packageManager(this.zzj.getContext()).getApplicationInfo(zzn.packageName, 0);
                        } catch (NameNotFoundException e3) {
                            this.zzj.zzab().zzgk().zza("Application info is null, first open report might be inaccurate. appId", zzef.zzam(zzn.packageName), e3);
                            applicationInfo = null;
                        }
                        if (applicationInfo != null) {
                            if ((applicationInfo.flags & 1) != 0) {
                                bundle3.putLong("_sys", 1);
                            }
                            if ((applicationInfo.flags & 128) != 0) {
                                bundle3.putLong("_sysu", 1);
                            }
                        }
                    }
                    zzx zzgy2 = zzgy();
                    String str = zzn.packageName;
                    Preconditions.checkNotEmpty(str);
                    zzgy2.zzo();
                    zzgy2.zzbi();
                    long zzj2 = zzgy2.zzj(str, "first_open_count");
                    if (zzj2 >= 0) {
                        bundle3.putLong("_pfo", zzj2);
                    }
                    zzc(new zzai("_f", new zzah(bundle3), "auto", j), zzn);
                } else if (i == 1) {
                    zzb(new zzjn("_fvt", j, Long.valueOf(j2), "auto"), zzn);
                    zzo();
                    zzjj();
                    Bundle bundle4 = new Bundle();
                    bundle4.putLong("_c", 1);
                    bundle4.putLong("_r", 1);
                    if (this.zzj.zzad().zzz(zzn.packageName)) {
                        bundle4.putLong("_et", 1);
                    }
                    if (zzn.zzdt) {
                        bundle4.putLong("_dac", 1);
                    }
                    zzc(new zzai("_v", new zzah(bundle4), "auto", j), zzn);
                }
                if (!this.zzj.zzad().zze(zzn.packageName, zzak.zzii)) {
                    Bundle bundle5 = new Bundle();
                    bundle5.putLong("_et", 1);
                    if (this.zzj.zzad().zzz(zzn.packageName)) {
                        bundle5.putLong("_fr", 1);
                    }
                    zzc(new zzai("_e", new zzah(bundle5), "auto", j), zzn);
                }
            } else if (zzn.zzdq) {
                zzc(new zzai("_cd", new zzah(new Bundle()), "auto", j), zzn);
            }
            zzgy().setTransactionSuccessful();
            zzgy().endTransaction();
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzf(zzq zzq) {
        zzn zzbi = zzbi(zzq.packageName);
        if (zzbi != null) {
            zzc(zzq, zzbi);
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzf(Runnable runnable) {
        zzo();
        if (this.zzsz == null) {
            this.zzsz = new ArrayList();
        }
        this.zzsz.add(runnable);
    }

    public final zzjo zzgw() {
        zza((zzjh) this.zzsu);
        return this.zzsu;
    }

    public final zzp zzgx() {
        zza((zzjh) this.zzst);
        return this.zzst;
    }

    public final zzx zzgy() {
        zza((zzjh) this.zzsq);
        return this.zzsq;
    }

    public final zzfd zzgz() {
        zza((zzjh) this.zzso);
        return this.zzso;
    }

    /* access modifiers changed from: 0000 */
    public final String zzh(zzn zzn) {
        try {
            return (String) this.zzj.zzaa().zza((Callable<V>) new zzjk<V>(this, zzn)).get(30000, TimeUnit.MILLISECONDS);
        } catch (InterruptedException | ExecutionException | TimeoutException e) {
            this.zzj.zzab().zzgk().zza("Failed to get app instance id. appId", zzef.zzam(zzn.packageName), e);
            return null;
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zzj(boolean z) {
        zzjn();
    }

    public final zzej zzjf() {
        zza((zzjh) this.zzsp);
        return this.zzsp;
    }

    public final zzhp zzji() {
        zza((zzjh) this.zzsv);
        return this.zzsv;
    }

    /* access modifiers changed from: 0000 */
    public final void zzjj() {
        if (!this.zzdh) {
            throw new IllegalStateException("UploadController is not initialized");
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzjl() {
        String zzby;
        Object obj;
        List list;
        String str;
        zzo();
        zzjj();
        this.zzte = true;
        try {
            this.zzj.zzae();
            Boolean zzit = this.zzj.zzs().zzit();
            if (zzit == null) {
                this.zzj.zzab().zzgn().zzao("Upload data called on the client side before use of service was decided");
                this.zzte = false;
                zzjo();
            } else if (zzit.booleanValue()) {
                this.zzj.zzab().zzgk().zzao("Upload called in the client side when service should be used");
                this.zzte = false;
                zzjo();
            } else if (this.zzsy > 0) {
                zzjn();
                this.zzte = false;
                zzjo();
            } else {
                zzo();
                if (this.zzth != null) {
                    this.zzj.zzab().zzgs().zzao("Uploading requested multiple times");
                    this.zzte = false;
                    zzjo();
                } else if (!zzjf().zzgv()) {
                    this.zzj.zzab().zzgs().zzao("Network not connected, ignoring upload request");
                    zzjn();
                    this.zzte = false;
                    zzjo();
                } else {
                    long currentTimeMillis = this.zzj.zzx().currentTimeMillis();
                    zzd((String) null, currentTimeMillis - zzs.zzbt());
                    long j = this.zzj.zzac().zzlj.get();
                    if (j != 0) {
                        this.zzj.zzab().zzgr().zza("Uploading events. Elapsed time since last upload attempt (ms)", Long.valueOf(Math.abs(currentTimeMillis - j)));
                    }
                    zzby = zzgy().zzby();
                    if (!TextUtils.isEmpty(zzby)) {
                        if (this.zztj == -1) {
                            this.zztj = zzgy().zzcf();
                        }
                        List zza2 = zzgy().zza(zzby, this.zzj.zzad().zzb(zzby, zzak.zzgl), Math.max(0, this.zzj.zzad().zzb(zzby, zzak.zzgm)));
                        if (!zza2.isEmpty()) {
                            Iterator it = zza2.iterator();
                            while (true) {
                                if (!it.hasNext()) {
                                    obj = null;
                                    break;
                                }
                                zzg zzg = (zzg) ((Pair) it.next()).first;
                                if (!TextUtils.isEmpty(zzg.zzot())) {
                                    obj = zzg.zzot();
                                    break;
                                }
                            }
                            if (obj != null) {
                                int i = 0;
                                while (true) {
                                    if (i >= zza2.size()) {
                                        break;
                                    }
                                    zzg zzg2 = (zzg) ((Pair) zza2.get(i)).first;
                                    if (!TextUtils.isEmpty(zzg2.zzot()) && !zzg2.zzot().equals(obj)) {
                                        list = zza2.subList(0, i);
                                        break;
                                    }
                                    i++;
                                }
                            }
                            list = zza2;
                            com.google.android.gms.internal.measurement.zzbs.zzf.zza zznj = zzf.zznj();
                            int size = list.size();
                            ArrayList arrayList = new ArrayList(list.size());
                            boolean z = zzs.zzbv() && this.zzj.zzad().zzl(zzby);
                            for (int i2 = 0; i2 < size; i2++) {
                                com.google.android.gms.internal.measurement.zzbs.zzg.zza zza3 = (com.google.android.gms.internal.measurement.zzbs.zzg.zza) ((zzg) ((Pair) list.get(i2)).first).zzuj();
                                arrayList.add((Long) ((Pair) list.get(i2)).second);
                                com.google.android.gms.internal.measurement.zzbs.zzg.zza zzan = zza3.zzat(this.zzj.zzad().zzao()).zzan(currentTimeMillis);
                                this.zzj.zzae();
                                zzan.zzn(false);
                                if (!z) {
                                    zza3.zznw();
                                }
                                if (this.zzj.zzad().zze(zzby, zzak.zzis)) {
                                    zza3.zzay(zzgw().zza(((zzg) ((zzey) zza3.zzug())).toByteArray()));
                                }
                                zznj.zza(zza3);
                            }
                            Object obj2 = this.zzj.zzab().isLoggable(2) ? zzgw().zza((zzf) ((zzey) zznj.zzug())) : null;
                            zzgw();
                            byte[] byteArray = ((zzf) ((zzey) zznj.zzug())).toByteArray();
                            str = (String) zzak.zzgv.get(null);
                            URL url = new URL(str);
                            Preconditions.checkArgument(!arrayList.isEmpty());
                            if (this.zzth != null) {
                                this.zzj.zzab().zzgk().zzao("Set uploading progress before finishing the previous upload");
                            } else {
                                this.zzth = new ArrayList(arrayList);
                            }
                            this.zzj.zzac().zzlk.set(currentTimeMillis);
                            String str2 = "?";
                            if (size > 0) {
                                str2 = zznj.zzo(0).zzag();
                            }
                            this.zzj.zzab().zzgs().zza("Uploading data. app, uncompressed size, data", str2, Integer.valueOf(byteArray.length), obj2);
                            this.zztd = true;
                            zzej zzjf = zzjf();
                            zzji zzji = new zzji(this, zzby);
                            zzjf.zzo();
                            zzjf.zzbi();
                            Preconditions.checkNotNull(url);
                            Preconditions.checkNotNull(byteArray);
                            Preconditions.checkNotNull(zzji);
                            zzjf.zzaa().zzb((Runnable) new zzen(zzjf, zzby, url, byteArray, null, zzji));
                        }
                    } else {
                        this.zztj = -1;
                        String zzu = zzgy().zzu(currentTimeMillis - zzs.zzbt());
                        if (!TextUtils.isEmpty(zzu)) {
                            zzf zzab = zzgy().zzab(zzu);
                            if (zzab != null) {
                                zzb(zzab);
                            }
                        }
                    }
                    this.zzte = false;
                    zzjo();
                }
            }
        } catch (MalformedURLException e) {
            this.zzj.zzab().zzgk().zza("Failed to parse upload URL. Not uploading. appId", zzef.zzam(zzby), str);
        } catch (Throwable th) {
            this.zzte = false;
            zzjo();
            throw th;
        }
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzjq() {
        zzo();
        zzjj();
        if (!this.zzsx) {
            this.zzsx = true;
            zzo();
            zzjj();
            if ((this.zzj.zzad().zza(zzak.zzim) || zzjr()) && zzjp()) {
                int zza2 = zza(this.zztg);
                int zzgf = this.zzj.zzr().zzgf();
                zzo();
                if (zza2 > zzgf) {
                    this.zzj.zzab().zzgk().zza("Panic: can't downgrade version. Previous, current version", Integer.valueOf(zza2), Integer.valueOf(zzgf));
                } else if (zza2 < zzgf) {
                    if (zza(zzgf, this.zztg)) {
                        this.zzj.zzab().zzgs().zza("Storage version upgraded. Previous, current version", Integer.valueOf(zza2), Integer.valueOf(zzgf));
                    } else {
                        this.zzj.zzab().zzgk().zza("Storage version upgrade failed. Previous, current version", Integer.valueOf(zza2), Integer.valueOf(zzgf));
                    }
                }
            }
        }
        if (!this.zzsw && !this.zzj.zzad().zza(zzak.zzim)) {
            this.zzj.zzab().zzgq().zzao("This instance being marked as an uploader");
            this.zzsw = true;
            zzjn();
        }
    }

    /* access modifiers changed from: 0000 */
    public final void zzjs() {
        this.zztb++;
    }

    /* access modifiers changed from: 0000 */
    public final zzfj zzjt() {
        return this.zzj;
    }

    public final Clock zzx() {
        return this.zzj.zzx();
    }

    public final zzed zzy() {
        return this.zzj.zzy();
    }

    public final zzjs zzz() {
        return this.zzj.zzz();
    }
}
