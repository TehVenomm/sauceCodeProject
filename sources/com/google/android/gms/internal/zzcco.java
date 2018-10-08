package com.google.android.gms.internal;

import android.app.Application;
import android.content.Context;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageInfo;
import android.content.pm.PackageManager.NameNotFoundException;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteException;
import android.net.Uri.Builder;
import android.os.Build;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Size;
import android.support.annotation.WorkerThread;
import android.support.v4.util.ArrayMap;
import android.text.TextUtils;
import android.util.Pair;
import com.google.android.gms.common.internal.zzbp;
import com.google.android.gms.common.util.zzd;
import com.google.android.gms.common.util.zzh;
import com.google.android.gms.measurement.AppMeasurement;
import com.google.firebase.analytics.FirebaseAnalytics;
import com.google.firebase.analytics.FirebaseAnalytics.Event;
import com.google.firebase.analytics.FirebaseAnalytics.Param;
import io.fabric.sdk.android.services.common.AbstractSpiCall;
import io.fabric.sdk.android.services.network.HttpRequest;
import java.io.File;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.RandomAccessFile;
import java.net.MalformedURLException;
import java.net.URL;
import java.nio.ByteBuffer;
import java.nio.channels.FileChannel;
import java.nio.channels.FileLock;
import java.util.ArrayList;
import java.util.Collection;
import java.util.Collections;
import java.util.Iterator;
import java.util.List;
import java.util.Locale;
import java.util.Map;
import java.util.concurrent.ExecutionException;
import java.util.concurrent.TimeUnit;
import java.util.concurrent.TimeoutException;
import java.util.concurrent.atomic.AtomicReference;
import org.apache.commons.lang3.time.DateUtils;

public class zzcco {
    private static volatile zzcco zzisi;
    private final Context mContext;
    private final zzd zzasl = zzh.zzalc();
    private final boolean zzdoj;
    private final zzcap zzisj = new zzcap(this);
    private final zzcbz zzisk;
    private final zzcbo zzisl;
    private final zzccj zzism;
    private final zzcfd zzisn;
    private final zzcci zziso;
    private final AppMeasurement zzisp;
    private final FirebaseAnalytics zzisq;
    private final zzcfo zzisr;
    private final zzcbm zziss;
    private final zzcaq zzist;
    private final zzcbk zzisu;
    private final zzcbs zzisv;
    private final zzcec zzisw;
    private final zzceg zzisx;
    private final zzcaw zzisy;
    private final zzcdo zzisz;
    private final zzcbj zzita;
    private final zzcbx zzitb;
    private final zzcfj zzitc;
    private final zzcam zzitd;
    private final zzcaf zzite;
    private boolean zzitf;
    private Boolean zzitg;
    private long zzith;
    private FileLock zziti;
    private FileChannel zzitj;
    private List<Long> zzitk;
    private List<Runnable> zzitl;
    private int zzitm;
    private int zzitn;
    private long zzito = -1;
    private long zzitp;
    private boolean zzitq;
    private boolean zzitr;
    private boolean zzits;
    private final long zzitt = this.zzasl.currentTimeMillis();

    final class zza implements zzcas {
        List<zzcfz> zzaom;
        private /* synthetic */ zzcco zzitu;
        zzcgc zzitv;
        List<Long> zzitw;
        private long zzitx;

        private zza(zzcco zzcco) {
            this.zzitu = zzcco;
        }

        private static long zza(zzcfz zzcfz) {
            return ((zzcfz.zziyt.longValue() / 1000) / 60) / 60;
        }

        public final boolean zza(long j, zzcfz zzcfz) {
            zzbp.zzu(zzcfz);
            if (this.zzaom == null) {
                this.zzaom = new ArrayList();
            }
            if (this.zzitw == null) {
                this.zzitw = new ArrayList();
            }
            if (this.zzaom.size() > 0 && zza((zzcfz) this.zzaom.get(0)) != zza(zzcfz)) {
                return false;
            }
            long zzbjo = this.zzitx + ((long) zzcfz.zzbjo());
            if (zzbjo >= ((long) zzcap.zzawq())) {
                return false;
            }
            this.zzitx = zzbjo;
            this.zzaom.add(zzcfz);
            this.zzitw.add(Long.valueOf(j));
            return this.zzaom.size() < zzcap.zzawr();
        }

        public final void zzb(zzcgc zzcgc) {
            zzbp.zzu(zzcgc);
            this.zzitv = zzcgc;
        }
    }

    private zzcco(zzcdn zzcdn) {
        zzcbq zzayg;
        zzbp.zzu(zzcdn);
        this.mContext = zzcdn.mContext;
        zzcdm zzcbz = new zzcbz(this);
        zzcbz.initialize();
        this.zzisk = zzcbz;
        zzcbz = new zzcbo(this);
        zzcbz.initialize();
        this.zzisl = zzcbz;
        zzauk().zzayg().zzj("App measurement is starting up, version", Long.valueOf(zzcap.zzauu()));
        zzcap.zzawj();
        zzauk().zzayg().log("To enable debug logging run: adb shell setprop log.tag.FA VERBOSE");
        zzcbz = new zzcfo(this);
        zzcbz.initialize();
        this.zzisr = zzcbz;
        zzcbz = new zzcbm(this);
        zzcbz.initialize();
        this.zziss = zzcbz;
        zzcbz = new zzcaw(this);
        zzcbz.initialize();
        this.zzisy = zzcbz;
        zzcbz = new zzcbj(this);
        zzcbz.initialize();
        this.zzita = zzcbz;
        zzcap.zzawj();
        String appId = zzcbz.getAppId();
        if (zzaug().zzke(appId)) {
            zzayg = zzauk().zzayg();
            appId = "Faster debug mode event logging enabled. To disable, run:\n  adb shell setprop debug.firebase.analytics.app .none.";
        } else {
            zzayg = zzauk().zzayg();
            appId = String.valueOf(appId);
            appId = appId.length() != 0 ? "To enable faster debug mode event logging run:\n  adb shell setprop debug.firebase.analytics.app ".concat(appId) : new String("To enable faster debug mode event logging run:\n  adb shell setprop debug.firebase.analytics.app ");
        }
        zzayg.log(appId);
        zzauk().zzayh().log("Debug-level message logging enabled");
        zzcbz = new zzcaq(this);
        zzcbz.initialize();
        this.zzist = zzcbz;
        zzcbz = new zzcbk(this);
        zzcbz.initialize();
        this.zzisu = zzcbz;
        zzcbz = new zzcam(this);
        zzcbz.initialize();
        this.zzitd = zzcbz;
        this.zzite = new zzcaf(this);
        zzcbz = new zzcbs(this);
        zzcbz.initialize();
        this.zzisv = zzcbz;
        zzcbz = new zzcec(this);
        zzcbz.initialize();
        this.zzisw = zzcbz;
        zzcbz = new zzceg(this);
        zzcbz.initialize();
        this.zzisx = zzcbz;
        zzcbz = new zzcdo(this);
        zzcbz.initialize();
        this.zzisz = zzcbz;
        zzcbz = new zzcfj(this);
        zzcbz.initialize();
        this.zzitc = zzcbz;
        this.zzitb = new zzcbx(this);
        this.zzisp = new AppMeasurement(this);
        this.zzisq = new FirebaseAnalytics(this);
        zzcbz = new zzcfd(this);
        zzcbz.initialize();
        this.zzisn = zzcbz;
        zzcbz = new zzcci(this);
        zzcbz.initialize();
        this.zziso = zzcbz;
        zzcbz = new zzccj(this);
        zzcbz.initialize();
        this.zzism = zzcbz;
        if (this.zzitm != this.zzitn) {
            zzauk().zzayc().zze("Not all components initialized", Integer.valueOf(this.zzitm), Integer.valueOf(this.zzitn));
        }
        this.zzdoj = true;
        zzcap.zzawj();
        if (this.mContext.getApplicationContext() instanceof Application) {
            zzcdl zzaty = zzaty();
            if (zzaty.getContext().getApplicationContext() instanceof Application) {
                Application application = (Application) zzaty.getContext().getApplicationContext();
                if (zzaty.zziuk == null) {
                    zzaty.zziuk = new zzceb(zzaty);
                }
                application.unregisterActivityLifecycleCallbacks(zzaty.zziuk);
                application.registerActivityLifecycleCallbacks(zzaty.zziuk);
                zzaty.zzauk().zzayi().log("Registered activity lifecycle callback");
            }
        } else {
            zzauk().zzaye().log("Application context is not an Application");
        }
        this.zzism.zzg(new zzccp(this));
    }

    @WorkerThread
    private final int zza(FileChannel fileChannel) {
        int i = 0;
        zzauj().zzug();
        if (fileChannel == null || !fileChannel.isOpen()) {
            zzauk().zzayc().log("Bad chanel to read from");
        } else {
            ByteBuffer allocate = ByteBuffer.allocate(4);
            try {
                fileChannel.position(0);
                int read = fileChannel.read(allocate);
                if (read == 4) {
                    allocate.flip();
                    i = allocate.getInt();
                } else if (read != -1) {
                    zzauk().zzaye().zzj("Unexpected data length. Bytes read", Integer.valueOf(read));
                }
            } catch (IOException e) {
                zzauk().zzayc().zzj("Failed to read from channel", e);
            }
        }
        return i;
    }

    private final void zza(zzcax zzcax, zzcak zzcak) {
        zzauj().zzug();
        zzwh();
        zzbp.zzu(zzcax);
        zzbp.zzu(zzcak);
        zzbp.zzgf(zzcax.mAppId);
        zzbp.zzbh(zzcax.mAppId.equals(zzcak.packageName));
        zzcgc zzcgc = new zzcgc();
        zzcgc.zziyz = Integer.valueOf(1);
        zzcgc.zzizh = AbstractSpiCall.ANDROID_CLIENT_TYPE;
        zzcgc.zzch = zzcak.packageName;
        zzcgc.zzilo = zzcak.zzilo;
        zzcgc.zzhtl = zzcak.zzhtl;
        zzcgc.zzizu = zzcak.zzilu == -2147483648L ? null : Integer.valueOf((int) zzcak.zzilu);
        zzcgc.zzizl = Long.valueOf(zzcak.zzilp);
        zzcgc.zziln = zzcak.zziln;
        zzcgc.zzizq = zzcak.zzilq == 0 ? null : Long.valueOf(zzcak.zzilq);
        Pair zzjh = zzaul().zzjh(zzcak.packageName);
        if (!(zzjh == null || TextUtils.isEmpty((CharSequence) zzjh.first))) {
            zzcgc.zzizn = (String) zzjh.first;
            zzcgc.zzizo = (Boolean) zzjh.second;
        }
        zzaua().zzwh();
        zzcgc.zzizi = Build.MODEL;
        zzaua().zzwh();
        zzcgc.zzcy = VERSION.RELEASE;
        zzcgc.zzizk = Integer.valueOf((int) zzaua().zzaxv());
        zzcgc.zzizj = zzaua().zzaxw();
        zzcgc.zzizm = null;
        zzcgc.zzizc = null;
        zzcgc.zzizd = null;
        zzcgc.zzize = null;
        zzcgc.zzizy = Long.valueOf(zzcak.zzilw);
        if (isEnabled() && zzcap.zzaxg()) {
            zzatz();
            zzcgc.zzizz = null;
        }
        zzcaj zziw = zzaue().zziw(zzcak.packageName);
        if (zziw == null) {
            zziw = new zzcaj(this, zzcak.packageName);
            zziw.zzim(zzatz().zzaxz());
            zziw.zzip(zzcak.zzilv);
            zziw.zzin(zzcak.zziln);
            zziw.zzio(zzaul().zzji(zzcak.packageName));
            zziw.zzaq(0);
            zziw.zzal(0);
            zziw.zzam(0);
            zziw.setAppVersion(zzcak.zzhtl);
            zziw.zzan(zzcak.zzilu);
            zziw.zziq(zzcak.zzilo);
            zziw.zzao(zzcak.zzilp);
            zziw.zzap(zzcak.zzilq);
            zziw.setMeasurementEnabled(zzcak.zzils);
            zziw.zzaz(zzcak.zzilw);
            zzaue().zza(zziw);
        }
        zzcgc.zzizp = zziw.getAppInstanceId();
        zzcgc.zzilv = zziw.zzaup();
        List zziv = zzaue().zziv(zzcak.packageName);
        zzcgc.zzizb = new zzcge[zziv.size()];
        for (int i = 0; i < zziv.size(); i++) {
            zzcge zzcge = new zzcge();
            zzcgc.zzizb[i] = zzcge;
            zzcge.name = ((zzcfn) zziv.get(i)).mName;
            zzcge.zzjad = Long.valueOf(((zzcfn) zziv.get(i)).zziwy);
            zzaug().zza(zzcge, ((zzcfn) zziv.get(i)).mValue);
        }
        try {
            boolean z;
            long zza = zzaue().zza(zzcgc);
            zzcaq zzaue = zzaue();
            if (zzcax.zzinc != null) {
                Iterator it = zzcax.zzinc.iterator();
                while (it.hasNext()) {
                    if ("_r".equals((String) it.next())) {
                        z = true;
                        break;
                    }
                }
                z = zzauh().zzar(zzcax.mAppId, zzcax.mName);
                zzcar zza2 = zzaue().zza(zzaze(), zzcax.mAppId, false, false, false, false, false);
                if (z && zza2.zzimv < ((long) this.zzisj.zzis(zzcax.mAppId))) {
                    z = true;
                    if (zzaue.zza(zzcax, zza, z)) {
                        this.zzitp = 0;
                    }
                }
            }
            z = false;
            if (zzaue.zza(zzcax, zza, z)) {
                this.zzitp = 0;
            }
        } catch (IOException e) {
            zzauk().zzayc().zze("Data loss. Failed to insert raw event metadata. appId", zzcbo.zzjf(zzcgc.zzch), e);
        }
    }

    private static void zza(zzcdl zzcdl) {
        if (zzcdl == null) {
            throw new IllegalStateException("Component not created");
        }
    }

    private static void zza(zzcdm zzcdm) {
        if (zzcdm == null) {
            throw new IllegalStateException("Component not created");
        } else if (!zzcdm.isInitialized()) {
            throw new IllegalStateException("Component not initialized");
        }
    }

    @WorkerThread
    private final boolean zza(int i, FileChannel fileChannel) {
        zzauj().zzug();
        if (fileChannel == null || !fileChannel.isOpen()) {
            zzauk().zzayc().log("Bad chanel to read from");
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
            zzauk().zzayc().zzj("Error writing to channel. Bytes written", Long.valueOf(fileChannel.size()));
            return true;
        } catch (IOException e) {
            zzauk().zzayc().zzj("Failed to write to channel", e);
            return false;
        }
    }

    private final zzcfy[] zza(String str, zzcge[] zzcgeArr, zzcfz[] zzcfzArr) {
        zzbp.zzgf(str);
        return zzatx().zza(str, zzcfzArr, zzcgeArr);
    }

    static void zzatt() {
        zzcap.zzawj();
        throw new IllegalStateException("Unexpected call on client side");
    }

    private final zzcbx zzaza() {
        if (this.zzitb != null) {
            return this.zzitb;
        }
        throw new IllegalStateException("Network broadcast receiver not created");
    }

    private final zzcfj zzazb() {
        zza(this.zzitc);
        return this.zzitc;
    }

    @WorkerThread
    private final boolean zzazc() {
        zzauj().zzug();
        try {
            this.zzitj = new RandomAccessFile(new File(this.mContext.getFilesDir(), zzcap.zzawh()), "rw").getChannel();
            this.zziti = this.zzitj.tryLock();
            if (this.zziti != null) {
                zzauk().zzayi().log("Storage concurrent access okay");
                return true;
            }
            zzauk().zzayc().log("Storage concurrent data access panic");
            return false;
        } catch (FileNotFoundException e) {
            zzauk().zzayc().zzj("Failed to acquire storage lock", e);
        } catch (IOException e2) {
            zzauk().zzayc().zzj("Failed to access storage lock file", e2);
        }
    }

    private final long zzaze() {
        long currentTimeMillis = this.zzasl.currentTimeMillis();
        zzcdl zzaul = zzaul();
        zzaul.zzwh();
        zzaul.zzug();
        long j = zzaul.zziqk.get();
        if (j == 0) {
            j = (long) (zzaul.zzaug().zzazx().nextInt(86400000) + 1);
            zzaul.zziqk.set(j);
        }
        return ((((j + currentTimeMillis) / 1000) / 60) / 60) / 24;
    }

    private final boolean zzazg() {
        zzauj().zzug();
        zzwh();
        return zzaue().zzaxm() || !TextUtils.isEmpty(zzaue().zzaxh());
    }

    @WorkerThread
    private final void zzazh() {
        zzauj().zzug();
        zzwh();
        if (zzazk()) {
            long abs;
            if (this.zzitp > 0) {
                abs = DateUtils.MILLIS_PER_HOUR - Math.abs(this.zzasl.elapsedRealtime() - this.zzitp);
                if (abs > 0) {
                    zzauk().zzayi().zzj("Upload has been suspended. Will update scheduling later in approximately ms", Long.valueOf(abs));
                    zzaza().unregister();
                    zzazb().cancel();
                    return;
                }
                this.zzitp = 0;
            }
            if (zzayu() && zzazg()) {
                long currentTimeMillis = this.zzasl.currentTimeMillis();
                long zzaxc = zzcap.zzaxc();
                Object obj = (zzaue().zzaxn() || zzaue().zzaxi()) ? 1 : null;
                if (obj != null) {
                    CharSequence zzaxf = this.zzisj.zzaxf();
                    abs = (TextUtils.isEmpty(zzaxf) || ".none.".equals(zzaxf)) ? zzcap.zzawx() : zzcap.zzawy();
                } else {
                    abs = zzcap.zzaww();
                }
                long j = zzaul().zziqg.get();
                long j2 = zzaul().zziqh.get();
                long max = Math.max(zzaue().zzaxk(), zzaue().zzaxl());
                if (max == 0) {
                    abs = 0;
                } else {
                    max = currentTimeMillis - Math.abs(max - currentTimeMillis);
                    j2 = currentTimeMillis - Math.abs(j2 - currentTimeMillis);
                    j = Math.max(currentTimeMillis - Math.abs(j - currentTimeMillis), j2);
                    currentTimeMillis = max + zzaxc;
                    if (obj != null && j > 0) {
                        currentTimeMillis = Math.min(max, j) + abs;
                    }
                    abs = !zzaug().zzf(j, abs) ? abs + j : currentTimeMillis;
                    if (j2 != 0 && j2 >= max) {
                        for (int i = 0; i < zzcap.zzaxe(); i++) {
                            abs += ((long) (1 << i)) * zzcap.zzaxd();
                            if (abs > j2) {
                                break;
                            }
                        }
                        abs = 0;
                    }
                }
                if (abs == 0) {
                    zzauk().zzayi().log("Next upload time is 0");
                    zzaza().unregister();
                    zzazb().cancel();
                    return;
                } else if (zzayz().zzyu()) {
                    currentTimeMillis = zzaul().zziqi.get();
                    long zzawv = zzcap.zzawv();
                    if (!zzaug().zzf(currentTimeMillis, zzawv)) {
                        abs = Math.max(abs, currentTimeMillis + zzawv);
                    }
                    zzaza().unregister();
                    abs -= this.zzasl.currentTimeMillis();
                    if (abs <= 0) {
                        abs = zzcap.zzawz();
                        zzaul().zziqg.set(this.zzasl.currentTimeMillis());
                    }
                    zzauk().zzayi().zzj("Upload scheduled in approximately ms", Long.valueOf(abs));
                    zzazb().zzs(abs);
                    return;
                } else {
                    zzauk().zzayi().log("No network");
                    zzaza().zzyr();
                    zzazb().cancel();
                    return;
                }
            }
            zzauk().zzayi().log("Nothing to upload or uploading impossible");
            zzaza().unregister();
            zzazb().cancel();
        }
    }

    @WorkerThread
    private final boolean zzazk() {
        zzauj().zzug();
        zzwh();
        return this.zzitf;
    }

    @WorkerThread
    private final void zzazl() {
        zzauj().zzug();
        if (this.zzitq || this.zzitr || this.zzits) {
            zzauk().zzayi().zzd("Not stopping services. fetch, network, upload", Boolean.valueOf(this.zzitq), Boolean.valueOf(this.zzitr), Boolean.valueOf(this.zzits));
            return;
        }
        zzauk().zzayi().log("Stopping uploading service(s)");
        if (this.zzitl != null) {
            for (Runnable run : this.zzitl) {
                run.run();
            }
            this.zzitl.clear();
        }
    }

    @WorkerThread
    private final void zzb(zzcaj zzcaj) {
        zzauj().zzug();
        if (TextUtils.isEmpty(zzcaj.getGmpAppId())) {
            zzb(zzcaj.getAppId(), 204, null, null, null);
            return;
        }
        String gmpAppId = zzcaj.getGmpAppId();
        String appInstanceId = zzcaj.getAppInstanceId();
        Builder builder = new Builder();
        Builder encodedAuthority = builder.scheme((String) zzcbe.zzinw.get()).encodedAuthority((String) zzcbe.zzinx.get());
        String valueOf = String.valueOf(gmpAppId);
        encodedAuthority.path(valueOf.length() != 0 ? "config/app/".concat(valueOf) : new String("config/app/")).appendQueryParameter("app_instance_id", appInstanceId).appendQueryParameter("platform", AbstractSpiCall.ANDROID_CLIENT_TYPE).appendQueryParameter("gmp_version", "11200");
        String uri = builder.build().toString();
        try {
            Map map;
            URL url = new URL(uri);
            zzauk().zzayi().zzj("Fetching remote configuration", zzcaj.getAppId());
            zzcfw zzjn = zzauh().zzjn(zzcaj.getAppId());
            CharSequence zzjo = zzauh().zzjo(zzcaj.getAppId());
            if (zzjn == null || TextUtils.isEmpty(zzjo)) {
                map = null;
            } else {
                map = new ArrayMap();
                map.put("If-Modified-Since", zzjo);
            }
            this.zzitq = true;
            zzcdl zzayz = zzayz();
            appInstanceId = zzcaj.getAppId();
            zzcbu zzccs = new zzccs(this);
            zzayz.zzug();
            zzayz.zzwh();
            zzbp.zzu(url);
            zzbp.zzu(zzccs);
            zzayz.zzauj().zzh(new zzcbw(zzayz, appInstanceId, url, null, map, zzccs));
        } catch (MalformedURLException e) {
            zzauk().zzayc().zze("Failed to parse config URL. Not fetching. appId", zzcbo.zzjf(zzcaj.getAppId()), uri);
        }
    }

    @WorkerThread
    private final void zzc(zzcbc zzcbc, zzcak zzcak) {
        zzbp.zzu(zzcak);
        zzbp.zzgf(zzcak.packageName);
        long nanoTime = System.nanoTime();
        zzauj().zzug();
        zzwh();
        String str = zzcak.packageName;
        zzaug();
        if (!zzcfo.zzd(zzcbc, zzcak)) {
            return;
        }
        if (!zzcak.zzils) {
            zzf(zzcak);
        } else if (zzauh().zzaq(str, zzcbc.name)) {
            zzauk().zzaye().zze("Dropping blacklisted event. appId", zzcbo.zzjf(str), zzauf().zzjc(zzcbc.name));
            Object obj = (zzaug().zzkg(str) || zzaug().zzkh(str)) ? 1 : null;
            if (obj == null && !"_err".equals(zzcbc.name)) {
                zzaug().zza(str, 11, "_ev", zzcbc.name, 0);
            }
            if (obj != null) {
                zzcaj zziw = zzaue().zziw(str);
                if (zziw != null) {
                    if (Math.abs(this.zzasl.currentTimeMillis() - Math.max(zziw.zzauz(), zziw.zzauy())) > zzcap.zzawn()) {
                        zzauk().zzayh().log("Fetching config for blacklisted app");
                        zzb(zziw);
                    }
                }
            }
        } else {
            Bundle zzaxy;
            if (zzauk().zzad(2)) {
                zzauk().zzayi().zzj("Logging event", zzauf().zzb(zzcbc));
            }
            zzaue().beginTransaction();
            zzcdl zzaue;
            try {
                zzaxy = zzcbc.zzinj.zzaxy();
                zzf(zzcak);
                if ("_iap".equals(zzcbc.name) || Event.ECOMMERCE_PURCHASE.equals(zzcbc.name)) {
                    long round;
                    Object string = zzaxy.getString(Param.CURRENCY);
                    if (Event.ECOMMERCE_PURCHASE.equals(zzcbc.name)) {
                        double d = zzaxy.getDouble(Param.VALUE) * 1000000.0d;
                        if (d == 0.0d) {
                            d = ((double) zzaxy.getLong(Param.VALUE)) * 1000000.0d;
                        }
                        if (d > 9.223372036854776E18d || d < -9.223372036854776E18d) {
                            zzauk().zzaye().zze("Data lost. Currency value is too big. appId", zzcbo.zzjf(str), Double.valueOf(d));
                            zzaue().setTransactionSuccessful();
                            zzaue().endTransaction();
                            return;
                        }
                        round = Math.round(d);
                    } else {
                        round = zzaxy.getLong(Param.VALUE);
                    }
                    if (!TextUtils.isEmpty(string)) {
                        String toUpperCase = string.toUpperCase(Locale.US);
                        if (toUpperCase.matches("[A-Z]{3}")) {
                            String valueOf = String.valueOf("_ltv_");
                            toUpperCase = String.valueOf(toUpperCase);
                            String concat = toUpperCase.length() != 0 ? valueOf.concat(toUpperCase) : new String(valueOf);
                            zzcfn zzaj = zzaue().zzaj(str, concat);
                            if (zzaj == null || !(zzaj.mValue instanceof Long)) {
                                zzaue = zzaue();
                                int zzb = this.zzisj.zzb(str, zzcbe.zziow);
                                zzbp.zzgf(str);
                                zzaue.zzug();
                                zzaue.zzwh();
                                zzaue.getWritableDatabase().execSQL("delete from user_attributes where app_id=? and name in (select name from user_attributes where app_id=? and name like '_ltv_%' order by set_timestamp desc limit ?,10);", new String[]{str, str, String.valueOf(zzb - 1)});
                                zzaj = new zzcfn(str, zzcbc.zzilz, concat, this.zzasl.currentTimeMillis(), Long.valueOf(round));
                            } else {
                                zzaj = new zzcfn(str, zzcbc.zzilz, concat, this.zzasl.currentTimeMillis(), Long.valueOf(round + ((Long) zzaj.mValue).longValue()));
                            }
                            if (!zzaue().zza(zzaj)) {
                                zzauk().zzayc().zzd("Too many unique user properties are set. Ignoring user property. appId", zzcbo.zzjf(str), zzauf().zzje(zzaj.mName), zzaj.mValue);
                                zzaug().zza(str, 9, null, null, 0);
                            }
                        }
                    }
                }
            } catch (SQLiteException e) {
                zzaue.zzauk().zzayc().zze("Error pruning currencies. appId", zzcbo.zzjf(str), e);
            } catch (Throwable th) {
                zzaue().endTransaction();
            }
            boolean zzju = zzcfo.zzju(zzcbc.name);
            boolean equals = "_err".equals(zzcbc.name);
            zzcar zza = zzaue().zza(zzaze(), str, true, zzju, false, equals, false);
            long zzavv = zza.zzims - zzcap.zzavv();
            if (zzavv > 0) {
                if (zzavv % 1000 == 1) {
                    zzauk().zzayc().zze("Data loss. Too many events logged. appId, count", zzcbo.zzjf(str), Long.valueOf(zza.zzims));
                }
                zzaue().setTransactionSuccessful();
                zzaue().endTransaction();
                return;
            }
            zzcay zzcay;
            if (zzju) {
                zzavv = zza.zzimr - zzcap.zzavw();
                if (zzavv > 0) {
                    if (zzavv % 1000 == 1) {
                        zzauk().zzayc().zze("Data loss. Too many public events logged. appId, count", zzcbo.zzjf(str), Long.valueOf(zza.zzimr));
                    }
                    zzaug().zza(str, 16, "_ev", zzcbc.name, 0);
                    zzaue().setTransactionSuccessful();
                    zzaue().endTransaction();
                    return;
                }
            }
            if (equals) {
                zzavv = zza.zzimu - ((long) Math.max(0, Math.min(1000000, this.zzisj.zzb(zzcak.packageName, zzcbe.zziod))));
                if (zzavv > 0) {
                    if (zzavv == 1) {
                        zzauk().zzayc().zze("Too many error events logged. appId, count", zzcbo.zzjf(str), Long.valueOf(zza.zzimu));
                    }
                    zzaue().setTransactionSuccessful();
                    zzaue().endTransaction();
                    return;
                }
            }
            zzaug().zza(zzaxy, "_o", zzcbc.zzilz);
            if (zzaug().zzke(str)) {
                zzaug().zza(zzaxy, "_dbg", Long.valueOf(1));
                zzaug().zza(zzaxy, "_r", Long.valueOf(1));
            }
            zzavv = zzaue().zzix(str);
            if (zzavv > 0) {
                zzauk().zzaye().zze("Data lost. Too many events stored on disk, deleted. appId", zzcbo.zzjf(str), Long.valueOf(zzavv));
            }
            zzcax zzcax = new zzcax(this, zzcbc.zzilz, str, zzcbc.name, zzcbc.zzink, 0, zzaxy);
            zzcay zzah = zzaue().zzah(str, zzcax.mName);
            if (zzah == null) {
                long zzja = zzaue().zzja(str);
                zzcap.zzavu();
                if (zzja >= 500) {
                    zzauk().zzayc().zzd("Too many event names used, ignoring event. appId, name, supported count", zzcbo.zzjf(str), zzauf().zzjc(zzcax.mName), Integer.valueOf(zzcap.zzavu()));
                    zzaug().zza(str, 8, null, null, 0);
                    zzaue().endTransaction();
                    return;
                }
                zzcay = new zzcay(str, zzcax.mName, 0, 0, zzcax.zzfcw);
            } else {
                zzcax = zzcax.zza(this, zzah.zzinf);
                zzcay = zzah.zzbb(zzcax.zzfcw);
            }
            zzaue().zza(zzcay);
            zza(zzcax, zzcak);
            zzaue().setTransactionSuccessful();
            if (zzauk().zzad(2)) {
                zzauk().zzayi().zzj("Event recorded", zzauf().zza(zzcax));
            }
            zzaue().endTransaction();
            zzazh();
            zzauk().zzayi().zzj("Background event processing time, ms", Long.valueOf(((System.nanoTime() - nanoTime) + 500000) / 1000000));
        }
    }

    public static zzcco zzdm(Context context) {
        zzbp.zzu(context);
        zzbp.zzu(context.getApplicationContext());
        if (zzisi == null) {
            synchronized (zzcco.class) {
                try {
                    if (zzisi == null) {
                        zzisi = new zzcco(new zzcdn(context));
                    }
                } catch (Throwable th) {
                    while (true) {
                        Class cls = zzcco.class;
                    }
                }
            }
        }
        return zzisi;
    }

    @WorkerThread
    private final void zzf(zzcak zzcak) {
        Object obj = 1;
        zzauj().zzug();
        zzwh();
        zzbp.zzu(zzcak);
        zzbp.zzgf(zzcak.packageName);
        zzcaj zziw = zzaue().zziw(zzcak.packageName);
        String zzji = zzaul().zzji(zzcak.packageName);
        Object obj2 = null;
        if (zziw == null) {
            zziw = new zzcaj(this, zzcak.packageName);
            zziw.zzim(zzatz().zzaxz());
            zziw.zzio(zzji);
            obj2 = 1;
        } else if (!zzji.equals(zziw.zzauo())) {
            zziw.zzio(zzji);
            zziw.zzim(zzatz().zzaxz());
            int i = 1;
        }
        if (!(TextUtils.isEmpty(zzcak.zziln) || zzcak.zziln.equals(zziw.getGmpAppId()))) {
            zziw.zzin(zzcak.zziln);
            obj2 = 1;
        }
        if (!(TextUtils.isEmpty(zzcak.zzilv) || zzcak.zzilv.equals(zziw.zzaup()))) {
            zziw.zzip(zzcak.zzilv);
            obj2 = 1;
        }
        if (!(zzcak.zzilp == 0 || zzcak.zzilp == zziw.zzauu())) {
            zziw.zzao(zzcak.zzilp);
            obj2 = 1;
        }
        if (!(TextUtils.isEmpty(zzcak.zzhtl) || zzcak.zzhtl.equals(zziw.zzul()))) {
            zziw.setAppVersion(zzcak.zzhtl);
            obj2 = 1;
        }
        if (zzcak.zzilu != zziw.zzaus()) {
            zziw.zzan(zzcak.zzilu);
            obj2 = 1;
        }
        if (!(zzcak.zzilo == null || zzcak.zzilo.equals(zziw.zzaut()))) {
            zziw.zziq(zzcak.zzilo);
            obj2 = 1;
        }
        if (zzcak.zzilq != zziw.zzauv()) {
            zziw.zzap(zzcak.zzilq);
            obj2 = 1;
        }
        if (zzcak.zzils != zziw.zzauw()) {
            zziw.setMeasurementEnabled(zzcak.zzils);
            obj2 = 1;
        }
        if (!(TextUtils.isEmpty(zzcak.zzilr) || zzcak.zzilr.equals(zziw.zzavh()))) {
            zziw.zzir(zzcak.zzilr);
            obj2 = 1;
        }
        if (zzcak.zzilw != zziw.zzavj()) {
            zziw.zzaz(zzcak.zzilw);
        } else {
            obj = obj2;
        }
        if (obj != null) {
            zzaue().zza(zziw);
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final boolean zzg(java.lang.String r21, long r22) {
        /*
        r20 = this;
        r2 = r20.zzaue();
        r2.beginTransaction();
        r15 = new com.google.android.gms.internal.zzcco$zza;	 Catch:{ all -> 0x019e }
        r2 = 0;
        r0 = r20;
        r15.<init>();	 Catch:{ all -> 0x019e }
        r14 = r20.zzaue();	 Catch:{ all -> 0x019e }
        r4 = 0;
        r0 = r20;
        r0 = r0.zzito;	 Catch:{ all -> 0x019e }
        r16 = r0;
        com.google.android.gms.common.internal.zzbp.zzu(r15);	 Catch:{ all -> 0x019e }
        r14.zzug();	 Catch:{ all -> 0x019e }
        r14.zzwh();	 Catch:{ all -> 0x019e }
        r6 = 0;
        r5 = 0;
        r2 = r14.getWritableDatabase();	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3 = 0;
        r3 = android.text.TextUtils.isEmpty(r3);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        if (r3 == 0) goto L_0x01a7;
    L_0x0030:
        r8 = -1;
        r3 = (r16 > r8 ? 1 : (r16 == r8 ? 0 : -1));
        if (r3 == 0) goto L_0x0140;
    L_0x0036:
        r3 = 2;
        r3 = new java.lang.String[r3];	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r7 = 0;
        r8 = java.lang.String.valueOf(r16);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3[r7] = r8;	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r7 = 1;
        r8 = java.lang.String.valueOf(r22);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3[r7] = r8;	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r7 = r3;
    L_0x0048:
        r8 = -1;
        r3 = (r16 > r8 ? 1 : (r16 == r8 ? 0 : -1));
        if (r3 == 0) goto L_0x014d;
    L_0x004e:
        r3 = "rowid <= ? and ";
    L_0x0050:
        r8 = java.lang.String.valueOf(r3);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r8 = r8.length();	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r9 = new java.lang.StringBuilder;	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r8 = r8 + 148;
        r9.<init>(r8);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r8 = "select app_id, metadata_fingerprint from raw_events where ";
        r8 = r9.append(r8);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3 = r8.append(r3);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r8 = "app_id in (select app_id from apps where config_fetched_time >= ?) order by rowid limit 1;";
        r3 = r3.append(r8);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3 = r3.toString();	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r5 = r2.rawQuery(r3, r7);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3 = r5.moveToFirst();	 Catch:{ SQLiteException -> 0x07a9, all -> 0x07b6 }
        if (r3 != 0) goto L_0x0151;
    L_0x007d:
        if (r5 == 0) goto L_0x0082;
    L_0x007f:
        r5.close();	 Catch:{ all -> 0x019e }
    L_0x0082:
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        if (r2 == 0) goto L_0x008e;
    L_0x0086:
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.isEmpty();	 Catch:{ all -> 0x019e }
        if (r2 == 0) goto L_0x0337;
    L_0x008e:
        r2 = 1;
    L_0x008f:
        if (r2 != 0) goto L_0x0780;
    L_0x0091:
        r12 = 0;
        r0 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r16 = r0;
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.size();	 Catch:{ all -> 0x019e }
        r2 = new com.google.android.gms.internal.zzcfz[r2];	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zziza = r2;	 Catch:{ all -> 0x019e }
        r13 = 0;
        r2 = 0;
        r14 = r2;
    L_0x00a5:
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.size();	 Catch:{ all -> 0x019e }
        if (r14 >= r2) goto L_0x05c8;
    L_0x00ad:
        r3 = r20.zzauh();	 Catch:{ all -> 0x019e }
        r2 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r4 = r2.zzch;	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.name;	 Catch:{ all -> 0x019e }
        r2 = r3.zzaq(r4, r2);	 Catch:{ all -> 0x019e }
        if (r2 == 0) goto L_0x033d;
    L_0x00c5:
        r2 = r20.zzauk();	 Catch:{ all -> 0x019e }
        r3 = r2.zzaye();	 Catch:{ all -> 0x019e }
        r4 = "Dropping blacklisted raw event. appId";
        r2 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r2 = r2.zzch;	 Catch:{ all -> 0x019e }
        r5 = com.google.android.gms.internal.zzcbo.zzjf(r2);	 Catch:{ all -> 0x019e }
        r6 = r20.zzauf();	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.name;	 Catch:{ all -> 0x019e }
        r2 = r6.zzjc(r2);	 Catch:{ all -> 0x019e }
        r3.zze(r4, r5, r2);	 Catch:{ all -> 0x019e }
        r2 = r20.zzaug();	 Catch:{ all -> 0x019e }
        r3 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r3 = r3.zzch;	 Catch:{ all -> 0x019e }
        r2 = r2.zzkg(r3);	 Catch:{ all -> 0x019e }
        if (r2 != 0) goto L_0x0108;
    L_0x00fa:
        r2 = r20.zzaug();	 Catch:{ all -> 0x019e }
        r3 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r3 = r3.zzch;	 Catch:{ all -> 0x019e }
        r2 = r2.zzkh(r3);	 Catch:{ all -> 0x019e }
        if (r2 == 0) goto L_0x033a;
    L_0x0108:
        r2 = 1;
    L_0x0109:
        if (r2 != 0) goto L_0x07a0;
    L_0x010b:
        r3 = "_err";
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.name;	 Catch:{ all -> 0x019e }
        r2 = r3.equals(r2);	 Catch:{ all -> 0x019e }
        if (r2 != 0) goto L_0x07a0;
    L_0x011d:
        r2 = r20.zzaug();	 Catch:{ all -> 0x019e }
        r3 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r3 = r3.zzch;	 Catch:{ all -> 0x019e }
        r4 = 11;
        r5 = "_ev";
        r6 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r6 = r6.get(r14);	 Catch:{ all -> 0x019e }
        r6 = (com.google.android.gms.internal.zzcfz) r6;	 Catch:{ all -> 0x019e }
        r6 = r6.name;	 Catch:{ all -> 0x019e }
        r7 = 0;
        r2.zza(r3, r4, r5, r6, r7);	 Catch:{ all -> 0x019e }
        r2 = r12;
        r3 = r13;
    L_0x0139:
        r4 = r14 + 1;
        r12 = r2;
        r14 = r4;
        r13 = r3;
        goto L_0x00a5;
    L_0x0140:
        r3 = 1;
        r3 = new java.lang.String[r3];	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r7 = 0;
        r8 = java.lang.String.valueOf(r22);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3[r7] = r8;	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r7 = r3;
        goto L_0x0048;
    L_0x014d:
        r3 = "";
        goto L_0x0050;
    L_0x0151:
        r3 = 0;
        r4 = r5.getString(r3);	 Catch:{ SQLiteException -> 0x07a9, all -> 0x07b6 }
        r3 = 1;
        r3 = r5.getString(r3);	 Catch:{ SQLiteException -> 0x07ae, all -> 0x07b6 }
        r5.close();	 Catch:{ SQLiteException -> 0x07ae, all -> 0x07b6 }
        r11 = r3;
        r12 = r4;
        r13 = r5;
    L_0x0161:
        r3 = "raw_events_metadata";
        r4 = 1;
        r4 = new java.lang.String[r4];	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r5 = 0;
        r6 = "metadata";
        r4[r5] = r6;	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r5 = "app_id = ? and metadata_fingerprint = ?";
        r6 = 2;
        r6 = new java.lang.String[r6];	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r7 = 0;
        r6[r7] = r12;	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r7 = 1;
        r6[r7] = r11;	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r7 = 0;
        r8 = 0;
        r9 = "rowid";
        r10 = "2";
        r13 = r2.query(r3, r4, r5, r6, r7, r8, r9, r10);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r3 = r13.moveToFirst();	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        if (r3 != 0) goto L_0x0212;
    L_0x0186:
        r2 = r14.zzauk();	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r2 = r2.zzayc();	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r3 = "Raw event metadata record is missing. appId";
        r4 = com.google.android.gms.internal.zzcbo.zzjf(r12);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r2.zzj(r3, r4);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        if (r13 == 0) goto L_0x0082;
    L_0x0199:
        r13.close();	 Catch:{ all -> 0x019e }
        goto L_0x0082;
    L_0x019e:
        r2 = move-exception;
        r3 = r20.zzaue();
        r3.endTransaction();
        throw r2;
    L_0x01a7:
        r8 = -1;
        r3 = (r16 > r8 ? 1 : (r16 == r8 ? 0 : -1));
        if (r3 == 0) goto L_0x01f8;
    L_0x01ad:
        r3 = 2;
        r3 = new java.lang.String[r3];	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r7 = 0;
        r8 = 0;
        r3[r7] = r8;	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r7 = 1;
        r8 = java.lang.String.valueOf(r16);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3[r7] = r8;	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r7 = r3;
    L_0x01bc:
        r8 = -1;
        r3 = (r16 > r8 ? 1 : (r16 == r8 ? 0 : -1));
        if (r3 == 0) goto L_0x0201;
    L_0x01c2:
        r3 = " and rowid <= ?";
    L_0x01c4:
        r8 = java.lang.String.valueOf(r3);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r8 = r8.length();	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r9 = new java.lang.StringBuilder;	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r8 = r8 + 84;
        r9.<init>(r8);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r8 = "select metadata_fingerprint from raw_events where app_id = ?";
        r8 = r9.append(r8);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3 = r8.append(r3);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r8 = " order by rowid limit 1;";
        r3 = r3.append(r8);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3 = r3.toString();	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r5 = r2.rawQuery(r3, r7);	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r3 = r5.moveToFirst();	 Catch:{ SQLiteException -> 0x07a9, all -> 0x07b6 }
        if (r3 != 0) goto L_0x0204;
    L_0x01f1:
        if (r5 == 0) goto L_0x0082;
    L_0x01f3:
        r5.close();	 Catch:{ all -> 0x019e }
        goto L_0x0082;
    L_0x01f8:
        r3 = 1;
        r3 = new java.lang.String[r3];	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r7 = 0;
        r8 = 0;
        r3[r7] = r8;	 Catch:{ SQLiteException -> 0x07a4, all -> 0x032e }
        r7 = r3;
        goto L_0x01bc;
    L_0x0201:
        r3 = "";
        goto L_0x01c4;
    L_0x0204:
        r3 = 0;
        r3 = r5.getString(r3);	 Catch:{ SQLiteException -> 0x07a9, all -> 0x07b6 }
        r5.close();	 Catch:{ SQLiteException -> 0x07a9, all -> 0x07b6 }
        r4 = 0;
        r11 = r3;
        r12 = r4;
        r13 = r5;
        goto L_0x0161;
    L_0x0212:
        r3 = 0;
        r3 = r13.getBlob(r3);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r4 = 0;
        r5 = r3.length;	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r3 = com.google.android.gms.internal.zzegf.zzh(r3, r4, r5);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r4 = new com.google.android.gms.internal.zzcgc;	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r4.<init>();	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r4.zza(r3);	 Catch:{ IOException -> 0x029a }
        r3 = r13.moveToNext();	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        if (r3 == 0) goto L_0x023c;
    L_0x022b:
        r3 = r14.zzauk();	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r3 = r3.zzaye();	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r5 = "Get multiple raw event metadata records, expected one. appId";
        r6 = com.google.android.gms.internal.zzcbo.zzjf(r12);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r3.zzj(r5, r6);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
    L_0x023c:
        r13.close();	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r15.zzb(r4);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r4 = -1;
        r3 = (r16 > r4 ? 1 : (r16 == r4 ? 0 : -1));
        if (r3 == 0) goto L_0x02b3;
    L_0x0248:
        r5 = "app_id = ? and metadata_fingerprint = ? and rowid <= ?";
        r3 = 3;
        r6 = new java.lang.String[r3];	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r3 = 0;
        r6[r3] = r12;
        r3 = 1;
        r6[r3] = r11;
        r3 = 2;
        r4 = java.lang.String.valueOf(r16);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r6[r3] = r4;	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
    L_0x025a:
        r3 = "raw_events";
        r4 = 4;
        r4 = new java.lang.String[r4];	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r7 = 0;
        r8 = "rowid";
        r4[r7] = r8;	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r7 = 1;
        r8 = "name";
        r4[r7] = r8;	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r7 = 2;
        r8 = "timestamp";
        r4[r7] = r8;	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r7 = 3;
        r8 = "data";
        r4[r7] = r8;	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r7 = 0;
        r8 = 0;
        r9 = "rowid";
        r10 = 0;
        r3 = r2.query(r3, r4, r5, r6, r7, r8, r9, r10);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r2 = r3.moveToFirst();	 Catch:{ SQLiteException -> 0x02bf }
        if (r2 != 0) goto L_0x02d8;
    L_0x0282:
        r2 = r14.zzauk();	 Catch:{ SQLiteException -> 0x02bf }
        r2 = r2.zzaye();	 Catch:{ SQLiteException -> 0x02bf }
        r4 = "Raw event data disappeared while in transaction. appId";
        r5 = com.google.android.gms.internal.zzcbo.zzjf(r12);	 Catch:{ SQLiteException -> 0x02bf }
        r2.zzj(r4, r5);	 Catch:{ SQLiteException -> 0x02bf }
        if (r3 == 0) goto L_0x0082;
    L_0x0295:
        r3.close();	 Catch:{ all -> 0x019e }
        goto L_0x0082;
    L_0x029a:
        r2 = move-exception;
        r3 = r14.zzauk();	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r3 = r3.zzayc();	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r4 = "Data loss. Failed to merge raw event metadata. appId";
        r5 = com.google.android.gms.internal.zzcbo.zzjf(r12);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r3.zze(r4, r5, r2);	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        if (r13 == 0) goto L_0x0082;
    L_0x02ae:
        r13.close();	 Catch:{ all -> 0x019e }
        goto L_0x0082;
    L_0x02b3:
        r5 = "app_id = ? and metadata_fingerprint = ?";
        r3 = 2;
        r6 = new java.lang.String[r3];	 Catch:{ SQLiteException -> 0x07bd, all -> 0x07b3 }
        r3 = 0;
        r6[r3] = r12;
        r3 = 1;
        r6[r3] = r11;
        goto L_0x025a;
    L_0x02bf:
        r2 = move-exception;
    L_0x02c0:
        r4 = r14.zzauk();	 Catch:{ all -> 0x07ba }
        r4 = r4.zzayc();	 Catch:{ all -> 0x07ba }
        r5 = "Data loss. Error selecting raw event. appId";
        r6 = com.google.android.gms.internal.zzcbo.zzjf(r12);	 Catch:{ all -> 0x07ba }
        r4.zze(r5, r6, r2);	 Catch:{ all -> 0x07ba }
        if (r3 == 0) goto L_0x0082;
    L_0x02d3:
        r3.close();	 Catch:{ all -> 0x019e }
        goto L_0x0082;
    L_0x02d8:
        r2 = 0;
        r4 = r3.getLong(r2);	 Catch:{ SQLiteException -> 0x02bf }
        r2 = 3;
        r2 = r3.getBlob(r2);	 Catch:{ SQLiteException -> 0x02bf }
        r6 = 0;
        r7 = r2.length;	 Catch:{ SQLiteException -> 0x02bf }
        r2 = com.google.android.gms.internal.zzegf.zzh(r2, r6, r7);	 Catch:{ SQLiteException -> 0x02bf }
        r6 = new com.google.android.gms.internal.zzcfz;	 Catch:{ SQLiteException -> 0x02bf }
        r6.<init>();	 Catch:{ SQLiteException -> 0x02bf }
        r6.zza(r2);	 Catch:{ IOException -> 0x030f }
        r2 = 1;
        r2 = r3.getString(r2);	 Catch:{ SQLiteException -> 0x02bf }
        r6.name = r2;	 Catch:{ SQLiteException -> 0x02bf }
        r2 = 2;
        r8 = r3.getLong(r2);	 Catch:{ SQLiteException -> 0x02bf }
        r2 = java.lang.Long.valueOf(r8);	 Catch:{ SQLiteException -> 0x02bf }
        r6.zziyt = r2;	 Catch:{ SQLiteException -> 0x02bf }
        r2 = r15.zza(r4, r6);	 Catch:{ SQLiteException -> 0x02bf }
        if (r2 != 0) goto L_0x0321;
    L_0x0308:
        if (r3 == 0) goto L_0x0082;
    L_0x030a:
        r3.close();	 Catch:{ all -> 0x019e }
        goto L_0x0082;
    L_0x030f:
        r2 = move-exception;
        r4 = r14.zzauk();	 Catch:{ SQLiteException -> 0x02bf }
        r4 = r4.zzayc();	 Catch:{ SQLiteException -> 0x02bf }
        r5 = "Data loss. Failed to merge raw event. appId";
        r6 = com.google.android.gms.internal.zzcbo.zzjf(r12);	 Catch:{ SQLiteException -> 0x02bf }
        r4.zze(r5, r6, r2);	 Catch:{ SQLiteException -> 0x02bf }
    L_0x0321:
        r2 = r3.moveToNext();	 Catch:{ SQLiteException -> 0x02bf }
        if (r2 != 0) goto L_0x02d8;
    L_0x0327:
        if (r3 == 0) goto L_0x0082;
    L_0x0329:
        r3.close();	 Catch:{ all -> 0x019e }
        goto L_0x0082;
    L_0x032e:
        r2 = move-exception;
        r3 = r5;
    L_0x0330:
        r13 = r3;
    L_0x0331:
        if (r13 == 0) goto L_0x0336;
    L_0x0333:
        r13.close();	 Catch:{ all -> 0x019e }
    L_0x0336:
        throw r2;	 Catch:{ all -> 0x019e }
    L_0x0337:
        r2 = 0;
        goto L_0x008f;
    L_0x033a:
        r2 = 0;
        goto L_0x0109;
    L_0x033d:
        r3 = r20.zzauh();	 Catch:{ all -> 0x019e }
        r2 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r4 = r2.zzch;	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.name;	 Catch:{ all -> 0x019e }
        r17 = r3.zzar(r4, r2);	 Catch:{ all -> 0x019e }
        if (r17 != 0) goto L_0x0368;
    L_0x0355:
        r20.zzaug();	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.name;	 Catch:{ all -> 0x019e }
        r2 = com.google.android.gms.internal.zzcfo.zzki(r2);	 Catch:{ all -> 0x019e }
        if (r2 == 0) goto L_0x05c6;
    L_0x0368:
        r3 = 0;
        r4 = 0;
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.zziys;	 Catch:{ all -> 0x019e }
        if (r2 != 0) goto L_0x0383;
    L_0x0376:
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r5 = 0;
        r5 = new com.google.android.gms.internal.zzcga[r5];	 Catch:{ all -> 0x019e }
        r2.zziys = r5;	 Catch:{ all -> 0x019e }
    L_0x0383:
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r6 = r2.zziys;	 Catch:{ all -> 0x019e }
        r7 = r6.length;	 Catch:{ all -> 0x019e }
        r2 = 0;
        r5 = r2;
        r2 = r3;
    L_0x0391:
        if (r5 >= r7) goto L_0x03c2;
    L_0x0393:
        r3 = r6[r5];
        r8 = "_c";
        r9 = r3.name;	 Catch:{ all -> 0x019e }
        r8 = r8.equals(r9);	 Catch:{ all -> 0x019e }
        if (r8 == 0) goto L_0x03ae;
    L_0x039f:
        r8 = 1;
        r2 = java.lang.Long.valueOf(r8);	 Catch:{ all -> 0x019e }
        r3.zziyw = r2;	 Catch:{ all -> 0x019e }
        r2 = 1;
        r3 = r4;
    L_0x03a9:
        r4 = r5 + 1;
        r5 = r4;
        r4 = r3;
        goto L_0x0391;
    L_0x03ae:
        r8 = "_r";
        r9 = r3.name;	 Catch:{ all -> 0x019e }
        r8 = r8.equals(r9);	 Catch:{ all -> 0x019e }
        if (r8 == 0) goto L_0x079d;
    L_0x03b8:
        r8 = 1;
        r4 = java.lang.Long.valueOf(r8);	 Catch:{ all -> 0x019e }
        r3.zziyw = r4;	 Catch:{ all -> 0x019e }
        r3 = 1;
        goto L_0x03a9;
    L_0x03c2:
        if (r2 != 0) goto L_0x0422;
    L_0x03c4:
        if (r17 == 0) goto L_0x0422;
    L_0x03c6:
        r2 = r20.zzauk();	 Catch:{ all -> 0x019e }
        r3 = r2.zzayi();	 Catch:{ all -> 0x019e }
        r5 = "Marking event as conversion";
        r6 = r20.zzauf();	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.name;	 Catch:{ all -> 0x019e }
        r2 = r6.zzjc(r2);	 Catch:{ all -> 0x019e }
        r3.zzj(r5, r2);	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r3 = r2.zziys;	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.zziys;	 Catch:{ all -> 0x019e }
        r2 = r2.length;	 Catch:{ all -> 0x019e }
        r2 = r2 + 1;
        r2 = java.util.Arrays.copyOf(r3, r2);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcga[]) r2;	 Catch:{ all -> 0x019e }
        r3 = new com.google.android.gms.internal.zzcga;	 Catch:{ all -> 0x019e }
        r3.<init>();	 Catch:{ all -> 0x019e }
        r5 = "_c";
        r3.name = r5;	 Catch:{ all -> 0x019e }
        r6 = 1;
        r5 = java.lang.Long.valueOf(r6);	 Catch:{ all -> 0x019e }
        r3.zziyw = r5;	 Catch:{ all -> 0x019e }
        r5 = r2.length;	 Catch:{ all -> 0x019e }
        r5 = r5 + -1;
        r2[r5] = r3;	 Catch:{ all -> 0x019e }
        r3 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r3 = r3.get(r14);	 Catch:{ all -> 0x019e }
        r3 = (com.google.android.gms.internal.zzcfz) r3;	 Catch:{ all -> 0x019e }
        r3.zziys = r2;	 Catch:{ all -> 0x019e }
    L_0x0422:
        if (r4 != 0) goto L_0x0480;
    L_0x0424:
        r2 = r20.zzauk();	 Catch:{ all -> 0x019e }
        r3 = r2.zzayi();	 Catch:{ all -> 0x019e }
        r4 = "Marking event as real-time";
        r5 = r20.zzauf();	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.name;	 Catch:{ all -> 0x019e }
        r2 = r5.zzjc(r2);	 Catch:{ all -> 0x019e }
        r3.zzj(r4, r2);	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r3 = r2.zziys;	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.zziys;	 Catch:{ all -> 0x019e }
        r2 = r2.length;	 Catch:{ all -> 0x019e }
        r2 = r2 + 1;
        r2 = java.util.Arrays.copyOf(r3, r2);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcga[]) r2;	 Catch:{ all -> 0x019e }
        r3 = new com.google.android.gms.internal.zzcga;	 Catch:{ all -> 0x019e }
        r3.<init>();	 Catch:{ all -> 0x019e }
        r4 = "_r";
        r3.name = r4;	 Catch:{ all -> 0x019e }
        r4 = 1;
        r4 = java.lang.Long.valueOf(r4);	 Catch:{ all -> 0x019e }
        r3.zziyw = r4;	 Catch:{ all -> 0x019e }
        r4 = r2.length;	 Catch:{ all -> 0x019e }
        r4 = r4 + -1;
        r2[r4] = r3;	 Catch:{ all -> 0x019e }
        r3 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r3 = r3.get(r14);	 Catch:{ all -> 0x019e }
        r3 = (com.google.android.gms.internal.zzcfz) r3;	 Catch:{ all -> 0x019e }
        r3.zziys = r2;	 Catch:{ all -> 0x019e }
    L_0x0480:
        r3 = r20.zzaue();	 Catch:{ all -> 0x019e }
        r4 = r20.zzaze();	 Catch:{ all -> 0x019e }
        r2 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r6 = r2.zzch;	 Catch:{ all -> 0x019e }
        r7 = 0;
        r8 = 0;
        r9 = 0;
        r10 = 0;
        r11 = 1;
        r2 = r3.zza(r4, r6, r7, r8, r9, r10, r11);	 Catch:{ all -> 0x019e }
        r2 = r2.zzimv;	 Catch:{ all -> 0x019e }
        r0 = r20;
        r4 = r0.zzisj;	 Catch:{ all -> 0x019e }
        r5 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r5 = r5.zzch;	 Catch:{ all -> 0x019e }
        r4 = r4.zzis(r5);	 Catch:{ all -> 0x019e }
        r4 = (long) r4;	 Catch:{ all -> 0x019e }
        r2 = (r2 > r4 ? 1 : (r2 == r4 ? 0 : -1));
        if (r2 <= 0) goto L_0x079a;
    L_0x04a8:
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r3 = 0;
    L_0x04b1:
        r4 = r2.zziys;	 Catch:{ all -> 0x019e }
        r4 = r4.length;	 Catch:{ all -> 0x019e }
        if (r3 >= r4) goto L_0x04e2;
    L_0x04b6:
        r4 = "_r";
        r5 = r2.zziys;	 Catch:{ all -> 0x019e }
        r5 = r5[r3];	 Catch:{ all -> 0x019e }
        r5 = r5.name;	 Catch:{ all -> 0x019e }
        r4 = r4.equals(r5);	 Catch:{ all -> 0x019e }
        if (r4 == 0) goto L_0x0556;
    L_0x04c4:
        r4 = r2.zziys;	 Catch:{ all -> 0x019e }
        r4 = r4.length;	 Catch:{ all -> 0x019e }
        r4 = r4 + -1;
        r4 = new com.google.android.gms.internal.zzcga[r4];	 Catch:{ all -> 0x019e }
        if (r3 <= 0) goto L_0x04d4;
    L_0x04cd:
        r5 = r2.zziys;	 Catch:{ all -> 0x019e }
        r6 = 0;
        r7 = 0;
        java.lang.System.arraycopy(r5, r6, r4, r7, r3);	 Catch:{ all -> 0x019e }
    L_0x04d4:
        r5 = r4.length;	 Catch:{ all -> 0x019e }
        if (r3 >= r5) goto L_0x04e0;
    L_0x04d7:
        r5 = r2.zziys;	 Catch:{ all -> 0x019e }
        r6 = r3 + 1;
        r7 = r4.length;	 Catch:{ all -> 0x019e }
        r7 = r7 - r3;
        java.lang.System.arraycopy(r5, r6, r4, r3, r7);	 Catch:{ all -> 0x019e }
    L_0x04e0:
        r2.zziys = r4;	 Catch:{ all -> 0x019e }
    L_0x04e2:
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r2 = r2.name;	 Catch:{ all -> 0x019e }
        r2 = com.google.android.gms.internal.zzcfo.zzju(r2);	 Catch:{ all -> 0x019e }
        if (r2 == 0) goto L_0x05c6;
    L_0x04f2:
        if (r17 == 0) goto L_0x05c6;
    L_0x04f4:
        r3 = r20.zzaue();	 Catch:{ all -> 0x019e }
        r4 = r20.zzaze();	 Catch:{ all -> 0x019e }
        r2 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r6 = r2.zzch;	 Catch:{ all -> 0x019e }
        r7 = 0;
        r8 = 0;
        r9 = 1;
        r10 = 0;
        r11 = 0;
        r2 = r3.zza(r4, r6, r7, r8, r9, r10, r11);	 Catch:{ all -> 0x019e }
        r2 = r2.zzimt;	 Catch:{ all -> 0x019e }
        r0 = r20;
        r4 = r0.zzisj;	 Catch:{ all -> 0x019e }
        r5 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r5 = r5.zzch;	 Catch:{ all -> 0x019e }
        r6 = com.google.android.gms.internal.zzcbe.zziof;	 Catch:{ all -> 0x019e }
        r4 = r4.zzb(r5, r6);	 Catch:{ all -> 0x019e }
        r4 = (long) r4;	 Catch:{ all -> 0x019e }
        r2 = (r2 > r4 ? 1 : (r2 == r4 ? 0 : -1));
        if (r2 <= 0) goto L_0x05c6;
    L_0x051e:
        r2 = r20.zzauk();	 Catch:{ all -> 0x019e }
        r2 = r2.zzaye();	 Catch:{ all -> 0x019e }
        r3 = "Too many conversions. Not logging as conversion. appId";
        r4 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r4 = r4.zzch;	 Catch:{ all -> 0x019e }
        r4 = com.google.android.gms.internal.zzcbo.zzjf(r4);	 Catch:{ all -> 0x019e }
        r2.zzj(r3, r4);	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r4 = 0;
        r5 = 0;
        r7 = r2.zziys;	 Catch:{ all -> 0x019e }
        r8 = r7.length;	 Catch:{ all -> 0x019e }
        r3 = 0;
        r6 = r3;
        r3 = r4;
    L_0x0543:
        if (r6 >= r8) goto L_0x0567;
    L_0x0545:
        r4 = r7[r6];
        r9 = "_c";
        r10 = r4.name;	 Catch:{ all -> 0x019e }
        r9 = r9.equals(r10);	 Catch:{ all -> 0x019e }
        if (r9 == 0) goto L_0x055a;
    L_0x0551:
        r5 = r6 + 1;
        r6 = r5;
        r5 = r4;
        goto L_0x0543;
    L_0x0556:
        r3 = r3 + 1;
        goto L_0x04b1;
    L_0x055a:
        r9 = "_err";
        r4 = r4.name;	 Catch:{ all -> 0x019e }
        r4 = r9.equals(r4);	 Catch:{ all -> 0x019e }
        if (r4 == 0) goto L_0x0797;
    L_0x0564:
        r3 = 1;
        r4 = r5;
        goto L_0x0551;
    L_0x0567:
        if (r3 == 0) goto L_0x05a1;
    L_0x0569:
        if (r5 == 0) goto L_0x05a1;
    L_0x056b:
        r3 = r2.zziys;	 Catch:{ all -> 0x019e }
        r3 = r3.length;	 Catch:{ all -> 0x019e }
        r3 = r3 + -1;
        r7 = new com.google.android.gms.internal.zzcga[r3];	 Catch:{ all -> 0x019e }
        r4 = 0;
        r8 = r2.zziys;	 Catch:{ all -> 0x019e }
        r9 = r8.length;	 Catch:{ all -> 0x019e }
        r3 = 0;
        r6 = r3;
    L_0x0578:
        if (r6 >= r9) goto L_0x0587;
    L_0x057a:
        r10 = r8[r6];
        if (r10 == r5) goto L_0x0794;
    L_0x057e:
        r3 = r4 + 1;
        r7[r4] = r10;
    L_0x0582:
        r4 = r6 + 1;
        r6 = r4;
        r4 = r3;
        goto L_0x0578;
    L_0x0587:
        r2.zziys = r7;	 Catch:{ all -> 0x019e }
        r3 = r12;
    L_0x058a:
        r0 = r16;
        r4 = r0.zziza;	 Catch:{ all -> 0x019e }
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.get(r14);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz) r2;	 Catch:{ all -> 0x019e }
        r4[r13] = r2;	 Catch:{ all -> 0x019e }
        r2 = r13 + 1;
        r18 = r3;
        r3 = r2;
        r2 = r18;
        goto L_0x0139;
    L_0x05a1:
        if (r5 == 0) goto L_0x05b1;
    L_0x05a3:
        r2 = "_err";
        r5.name = r2;	 Catch:{ all -> 0x019e }
        r2 = 10;
        r2 = java.lang.Long.valueOf(r2);	 Catch:{ all -> 0x019e }
        r5.zziyw = r2;	 Catch:{ all -> 0x019e }
        r3 = r12;
        goto L_0x058a;
    L_0x05b1:
        r2 = r20.zzauk();	 Catch:{ all -> 0x019e }
        r2 = r2.zzayc();	 Catch:{ all -> 0x019e }
        r3 = "Did not find conversion parameter. appId";
        r4 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r4 = r4.zzch;	 Catch:{ all -> 0x019e }
        r4 = com.google.android.gms.internal.zzcbo.zzjf(r4);	 Catch:{ all -> 0x019e }
        r2.zzj(r3, r4);	 Catch:{ all -> 0x019e }
    L_0x05c6:
        r3 = r12;
        goto L_0x058a;
    L_0x05c8:
        r2 = r15.zzaom;	 Catch:{ all -> 0x019e }
        r2 = r2.size();	 Catch:{ all -> 0x019e }
        if (r13 >= r2) goto L_0x05de;
    L_0x05d0:
        r0 = r16;
        r2 = r0.zziza;	 Catch:{ all -> 0x019e }
        r2 = java.util.Arrays.copyOf(r2, r13);	 Catch:{ all -> 0x019e }
        r2 = (com.google.android.gms.internal.zzcfz[]) r2;	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zziza = r2;	 Catch:{ all -> 0x019e }
    L_0x05de:
        r2 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r2 = r2.zzch;	 Catch:{ all -> 0x019e }
        r3 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r3 = r3.zzizb;	 Catch:{ all -> 0x019e }
        r0 = r16;
        r4 = r0.zziza;	 Catch:{ all -> 0x019e }
        r0 = r20;
        r2 = r0.zza(r2, r3, r4);	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zzizt = r2;	 Catch:{ all -> 0x019e }
        r2 = 9223372036854775807; // 0x7fffffffffffffff float:NaN double:NaN;
        r2 = java.lang.Long.valueOf(r2);	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zzizd = r2;	 Catch:{ all -> 0x019e }
        r2 = -9223372036854775808;
        r2 = java.lang.Long.valueOf(r2);	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zzize = r2;	 Catch:{ all -> 0x019e }
        r2 = 0;
    L_0x060c:
        r0 = r16;
        r3 = r0.zziza;	 Catch:{ all -> 0x019e }
        r3 = r3.length;	 Catch:{ all -> 0x019e }
        if (r2 >= r3) goto L_0x064c;
    L_0x0613:
        r0 = r16;
        r3 = r0.zziza;	 Catch:{ all -> 0x019e }
        r3 = r3[r2];	 Catch:{ all -> 0x019e }
        r4 = r3.zziyt;	 Catch:{ all -> 0x019e }
        r4 = r4.longValue();	 Catch:{ all -> 0x019e }
        r0 = r16;
        r6 = r0.zzizd;	 Catch:{ all -> 0x019e }
        r6 = r6.longValue();	 Catch:{ all -> 0x019e }
        r4 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1));
        if (r4 >= 0) goto L_0x0631;
    L_0x062b:
        r4 = r3.zziyt;	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zzizd = r4;	 Catch:{ all -> 0x019e }
    L_0x0631:
        r4 = r3.zziyt;	 Catch:{ all -> 0x019e }
        r4 = r4.longValue();	 Catch:{ all -> 0x019e }
        r0 = r16;
        r6 = r0.zzize;	 Catch:{ all -> 0x019e }
        r6 = r6.longValue();	 Catch:{ all -> 0x019e }
        r4 = (r4 > r6 ? 1 : (r4 == r6 ? 0 : -1));
        if (r4 <= 0) goto L_0x0649;
    L_0x0643:
        r3 = r3.zziyt;	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zzize = r3;	 Catch:{ all -> 0x019e }
    L_0x0649:
        r2 = r2 + 1;
        goto L_0x060c;
    L_0x064c:
        r2 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r6 = r2.zzch;	 Catch:{ all -> 0x019e }
        r2 = r20.zzaue();	 Catch:{ all -> 0x019e }
        r7 = r2.zziw(r6);	 Catch:{ all -> 0x019e }
        if (r7 != 0) goto L_0x06de;
    L_0x065a:
        r2 = r20.zzauk();	 Catch:{ all -> 0x019e }
        r2 = r2.zzayc();	 Catch:{ all -> 0x019e }
        r3 = "Bundling raw events w/o app info. appId";
        r4 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r4 = r4.zzch;	 Catch:{ all -> 0x019e }
        r4 = com.google.android.gms.internal.zzcbo.zzjf(r4);	 Catch:{ all -> 0x019e }
        r2.zzj(r3, r4);	 Catch:{ all -> 0x019e }
    L_0x066f:
        r0 = r16;
        r2 = r0.zziza;	 Catch:{ all -> 0x019e }
        r2 = r2.length;	 Catch:{ all -> 0x019e }
        if (r2 <= 0) goto L_0x06a8;
    L_0x0676:
        com.google.android.gms.internal.zzcap.zzawj();	 Catch:{ all -> 0x019e }
        r2 = r20.zzauh();	 Catch:{ all -> 0x019e }
        r3 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r3 = r3.zzch;	 Catch:{ all -> 0x019e }
        r2 = r2.zzjn(r3);	 Catch:{ all -> 0x019e }
        if (r2 == 0) goto L_0x068b;
    L_0x0687:
        r3 = r2.zziyh;	 Catch:{ all -> 0x019e }
        if (r3 != 0) goto L_0x0761;
    L_0x068b:
        r2 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r2 = r2.zziln;	 Catch:{ all -> 0x019e }
        r2 = android.text.TextUtils.isEmpty(r2);	 Catch:{ all -> 0x019e }
        if (r2 == 0) goto L_0x074a;
    L_0x0695:
        r2 = -1;
        r2 = java.lang.Long.valueOf(r2);	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zzizx = r2;	 Catch:{ all -> 0x019e }
    L_0x069f:
        r2 = r20.zzaue();	 Catch:{ all -> 0x019e }
        r0 = r16;
        r2.zza(r0, r12);	 Catch:{ all -> 0x019e }
    L_0x06a8:
        r2 = r20.zzaue();	 Catch:{ all -> 0x019e }
        r3 = r15.zzitw;	 Catch:{ all -> 0x019e }
        r2.zzae(r3);	 Catch:{ all -> 0x019e }
        r3 = r20.zzaue();	 Catch:{ all -> 0x019e }
        r2 = r3.getWritableDatabase();	 Catch:{ all -> 0x019e }
        r4 = "delete from raw_events_metadata where app_id=? and metadata_fingerprint not in (select distinct metadata_fingerprint from raw_events where app_id=?)";
        r5 = 2;
        r5 = new java.lang.String[r5];	 Catch:{ SQLiteException -> 0x0769 }
        r7 = 0;
        r5[r7] = r6;	 Catch:{ SQLiteException -> 0x0769 }
        r7 = 1;
        r5[r7] = r6;	 Catch:{ SQLiteException -> 0x0769 }
        r2.execSQL(r4, r5);	 Catch:{ SQLiteException -> 0x0769 }
    L_0x06c7:
        r2 = r20.zzaue();	 Catch:{ all -> 0x019e }
        r2.setTransactionSuccessful();	 Catch:{ all -> 0x019e }
        r0 = r16;
        r2 = r0.zziza;	 Catch:{ all -> 0x019e }
        r2 = r2.length;	 Catch:{ all -> 0x019e }
        if (r2 <= 0) goto L_0x077d;
    L_0x06d5:
        r2 = 1;
    L_0x06d6:
        r3 = r20.zzaue();
        r3.endTransaction();
    L_0x06dd:
        return r2;
    L_0x06de:
        r0 = r16;
        r2 = r0.zziza;	 Catch:{ all -> 0x019e }
        r2 = r2.length;	 Catch:{ all -> 0x019e }
        if (r2 <= 0) goto L_0x066f;
    L_0x06e5:
        r2 = r7.zzaur();	 Catch:{ all -> 0x019e }
        r4 = 0;
        r4 = (r2 > r4 ? 1 : (r2 == r4 ? 0 : -1));
        if (r4 == 0) goto L_0x0746;
    L_0x06ef:
        r4 = java.lang.Long.valueOf(r2);	 Catch:{ all -> 0x019e }
    L_0x06f3:
        r0 = r16;
        r0.zzizg = r4;	 Catch:{ all -> 0x019e }
        r4 = r7.zzauq();	 Catch:{ all -> 0x019e }
        r8 = 0;
        r8 = (r4 > r8 ? 1 : (r4 == r8 ? 0 : -1));
        if (r8 != 0) goto L_0x0791;
    L_0x0701:
        r4 = 0;
        r4 = (r2 > r4 ? 1 : (r2 == r4 ? 0 : -1));
        if (r4 == 0) goto L_0x0748;
    L_0x0707:
        r2 = java.lang.Long.valueOf(r2);	 Catch:{ all -> 0x019e }
    L_0x070b:
        r0 = r16;
        r0.zzizf = r2;	 Catch:{ all -> 0x019e }
        r7.zzava();	 Catch:{ all -> 0x019e }
        r2 = r7.zzaux();	 Catch:{ all -> 0x019e }
        r2 = (int) r2;	 Catch:{ all -> 0x019e }
        r2 = java.lang.Integer.valueOf(r2);	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zzizr = r2;	 Catch:{ all -> 0x019e }
        r0 = r16;
        r2 = r0.zzizd;	 Catch:{ all -> 0x019e }
        r2 = r2.longValue();	 Catch:{ all -> 0x019e }
        r7.zzal(r2);	 Catch:{ all -> 0x019e }
        r0 = r16;
        r2 = r0.zzize;	 Catch:{ all -> 0x019e }
        r2 = r2.longValue();	 Catch:{ all -> 0x019e }
        r7.zzam(r2);	 Catch:{ all -> 0x019e }
        r2 = r7.zzavi();	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zzilr = r2;	 Catch:{ all -> 0x019e }
        r2 = r20.zzaue();	 Catch:{ all -> 0x019e }
        r2.zza(r7);	 Catch:{ all -> 0x019e }
        goto L_0x066f;
    L_0x0746:
        r4 = 0;
        goto L_0x06f3;
    L_0x0748:
        r2 = 0;
        goto L_0x070b;
    L_0x074a:
        r2 = r20.zzauk();	 Catch:{ all -> 0x019e }
        r2 = r2.zzaye();	 Catch:{ all -> 0x019e }
        r3 = "Did not find measurement config or missing version info. appId";
        r4 = r15.zzitv;	 Catch:{ all -> 0x019e }
        r4 = r4.zzch;	 Catch:{ all -> 0x019e }
        r4 = com.google.android.gms.internal.zzcbo.zzjf(r4);	 Catch:{ all -> 0x019e }
        r2.zzj(r3, r4);	 Catch:{ all -> 0x019e }
        goto L_0x069f;
    L_0x0761:
        r2 = r2.zziyh;	 Catch:{ all -> 0x019e }
        r0 = r16;
        r0.zzizx = r2;	 Catch:{ all -> 0x019e }
        goto L_0x069f;
    L_0x0769:
        r2 = move-exception;
        r3 = r3.zzauk();	 Catch:{ all -> 0x019e }
        r3 = r3.zzayc();	 Catch:{ all -> 0x019e }
        r4 = "Failed to remove unused event metadata. appId";
        r5 = com.google.android.gms.internal.zzcbo.zzjf(r6);	 Catch:{ all -> 0x019e }
        r3.zze(r4, r5, r2);	 Catch:{ all -> 0x019e }
        goto L_0x06c7;
    L_0x077d:
        r2 = 0;
        goto L_0x06d6;
    L_0x0780:
        r2 = r20.zzaue();	 Catch:{ all -> 0x019e }
        r2.setTransactionSuccessful();	 Catch:{ all -> 0x019e }
        r2 = r20.zzaue();
        r2.endTransaction();
        r2 = 0;
        goto L_0x06dd;
    L_0x0791:
        r2 = r4;
        goto L_0x0701;
    L_0x0794:
        r3 = r4;
        goto L_0x0582;
    L_0x0797:
        r4 = r5;
        goto L_0x0551;
    L_0x079a:
        r12 = 1;
        goto L_0x04e2;
    L_0x079d:
        r3 = r4;
        goto L_0x03a9;
    L_0x07a0:
        r2 = r12;
        r3 = r13;
        goto L_0x0139;
    L_0x07a4:
        r2 = move-exception;
        r12 = r4;
        r3 = r6;
        goto L_0x02c0;
    L_0x07a9:
        r2 = move-exception;
        r12 = r4;
        r3 = r5;
        goto L_0x02c0;
    L_0x07ae:
        r2 = move-exception;
        r12 = r4;
        r3 = r5;
        goto L_0x02c0;
    L_0x07b3:
        r2 = move-exception;
        goto L_0x0331;
    L_0x07b6:
        r2 = move-exception;
        r3 = r5;
        goto L_0x0330;
    L_0x07ba:
        r2 = move-exception;
        goto L_0x0330;
    L_0x07bd:
        r2 = move-exception;
        r3 = r13;
        goto L_0x02c0;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.android.gms.internal.zzcco.zzg(java.lang.String, long):boolean");
    }

    @WorkerThread
    private final zzcak zzjr(String str) {
        zzcaj zziw = zzaue().zziw(str);
        if (zziw == null || TextUtils.isEmpty(zziw.zzul())) {
            zzauk().zzayh().zzj("No app data available; dropping", str);
            return null;
        }
        try {
            String str2 = zzbdp.zzcs(this.mContext).getPackageInfo(str, 0).versionName;
            if (!(zziw.zzul() == null || zziw.zzul().equals(str2))) {
                zzauk().zzaye().zzj("App version does not match; dropping. appId", zzcbo.zzjf(str));
                return null;
            }
        } catch (NameNotFoundException e) {
        }
        return new zzcak(str, zziw.getGmpAppId(), zziw.zzul(), zziw.zzaus(), zziw.zzaut(), zziw.zzauu(), zziw.zzauv(), null, zziw.zzauw(), false, zziw.zzaup(), zziw.zzavj(), 0, 0);
    }

    public final Context getContext() {
        return this.mContext;
    }

    @WorkerThread
    public final boolean isEnabled() {
        boolean z = false;
        zzauj().zzug();
        zzwh();
        if (this.zzisj.zzawk()) {
            return false;
        }
        Boolean zzit = this.zzisj.zzit("firebase_analytics_collection_enabled");
        if (zzit != null) {
            z = zzit.booleanValue();
        } else if (!zzcap.zzaie()) {
            z = true;
        }
        return zzaul().zzbn(z);
    }

    @WorkerThread
    protected final void start() {
        zzauj().zzug();
        zzaue().zzaxj();
        if (zzaul().zziqg.get() == 0) {
            zzaul().zziqg.set(this.zzasl.currentTimeMillis());
        }
        if (Long.valueOf(zzaul().zziql.get()).longValue() == 0) {
            zzauk().zzayi().zzj("Persisting first open", Long.valueOf(this.zzitt));
            zzaul().zziql.set(this.zzitt);
        }
        if (zzayu()) {
            zzcap.zzawj();
            if (!TextUtils.isEmpty(zzatz().getGmpAppId())) {
                String zzayl = zzaul().zzayl();
                if (zzayl == null) {
                    zzaul().zzjj(zzatz().getGmpAppId());
                } else if (!zzayl.equals(zzatz().getGmpAppId())) {
                    zzauk().zzayg().log("Rechecking which service to use due to a GMP App Id change");
                    zzaul().zzayo();
                    this.zzisx.disconnect();
                    this.zzisx.zzxe();
                    zzaul().zzjj(zzatz().getGmpAppId());
                    zzaul().zziql.set(this.zzitt);
                    zzaul().zziqm.zzjl(null);
                }
            }
            zzaty().zzjk(zzaul().zziqm.zzayq());
            zzcap.zzawj();
            if (!TextUtils.isEmpty(zzatz().getGmpAppId())) {
                zzcdl zzaty = zzaty();
                zzaty.zzug();
                zzaty.zzatu();
                zzaty.zzwh();
                if (zzaty.zzikb.zzayu()) {
                    zzaty.zzaub().zzazp();
                    String zzayp = zzaty.zzaul().zzayp();
                    if (!TextUtils.isEmpty(zzayp)) {
                        zzaty.zzaua().zzwh();
                        if (!zzayp.equals(VERSION.RELEASE)) {
                            Bundle bundle = new Bundle();
                            bundle.putString("_po", zzayp);
                            zzaty.zzc("auto", "_ou", bundle);
                        }
                    }
                }
                zzaub().zza(new AtomicReference());
            }
        } else if (isEnabled()) {
            if (!zzaug().zzdu("android.permission.INTERNET")) {
                zzauk().zzayc().log("App is missing INTERNET permission");
            }
            if (!zzaug().zzdu("android.permission.ACCESS_NETWORK_STATE")) {
                zzauk().zzayc().log("App is missing ACCESS_NETWORK_STATE permission");
            }
            zzcap.zzawj();
            if (!zzbdp.zzcs(this.mContext).zzalq()) {
                if (!zzccf.zzj(this.mContext, false)) {
                    zzauk().zzayc().log("AppMeasurementReceiver not registered/enabled");
                }
                if (!zzcez.zzk(this.mContext, false)) {
                    zzauk().zzayc().log("AppMeasurementService not registered/enabled");
                }
            }
            zzauk().zzayc().log("Uploading is not possible. App measurement disabled");
        }
        zzazh();
    }

    @WorkerThread
    protected final void zza(int i, Throwable th, byte[] bArr) {
        zzauj().zzug();
        zzwh();
        if (bArr == null) {
            try {
                bArr = new byte[0];
            } catch (Throwable th2) {
                this.zzitr = false;
                zzazl();
            }
        }
        List<Long> list = this.zzitk;
        this.zzitk = null;
        if ((i == 200 || i == 204) && th == null) {
            try {
                zzaul().zziqg.set(this.zzasl.currentTimeMillis());
                zzaul().zziqh.set(0);
                zzazh();
                zzauk().zzayi().zze("Successful upload. Got network response. code, size", Integer.valueOf(i), Integer.valueOf(bArr.length));
                zzaue().beginTransaction();
                zzcdl zzaue;
                try {
                    for (Long l : list) {
                        zzaue = zzaue();
                        long longValue = l.longValue();
                        zzaue.zzug();
                        zzaue.zzwh();
                        if (zzaue.getWritableDatabase().delete("queue", "rowid=?", new String[]{String.valueOf(longValue)}) != 1) {
                            throw new SQLiteException("Deleted fewer rows from queue than expected");
                        }
                    }
                    zzaue().setTransactionSuccessful();
                    zzaue().endTransaction();
                    if (zzayz().zzyu() && zzazg()) {
                        zzazf();
                    } else {
                        this.zzito = -1;
                        zzazh();
                    }
                    this.zzitp = 0;
                } catch (SQLiteException e) {
                    zzaue.zzauk().zzayc().zzj("Failed to delete a bundle in a queue table", e);
                    throw e;
                } catch (Throwable th3) {
                    zzaue().endTransaction();
                }
            } catch (SQLiteException e2) {
                zzauk().zzayc().zzj("Database error while trying to delete uploaded bundles", e2);
                this.zzitp = this.zzasl.elapsedRealtime();
                zzauk().zzayi().zzj("Disable upload, time", Long.valueOf(this.zzitp));
            }
        } else {
            zzauk().zzayi().zze("Network upload failed. Will retry later. code, error", Integer.valueOf(i), th);
            zzaul().zziqh.set(this.zzasl.currentTimeMillis());
            boolean z = i == 503 || i == 429;
            if (z) {
                zzaul().zziqi.set(this.zzasl.currentTimeMillis());
            }
            zzazh();
        }
        this.zzitr = false;
        zzazl();
    }

    @WorkerThread
    public final byte[] zza(@NonNull zzcbc zzcbc, @Size(min = 1) String str) {
        zzwh();
        zzauj().zzug();
        zzatt();
        zzbp.zzu(zzcbc);
        zzbp.zzgf(str);
        zzego zzcgb = new zzcgb();
        zzaue().beginTransaction();
        try {
            zzcaj zziw = zzaue().zziw(str);
            byte[] bArr;
            if (zziw == null) {
                zzauk().zzayh().zzj("Log and bundle not available. package_name", str);
                bArr = new byte[0];
                return bArr;
            } else if (zziw.zzauw()) {
                long j;
                zzcgc zzcgc = new zzcgc();
                zzcgb.zziyx = new zzcgc[]{zzcgc};
                zzcgc.zziyz = Integer.valueOf(1);
                zzcgc.zzizh = AbstractSpiCall.ANDROID_CLIENT_TYPE;
                zzcgc.zzch = zziw.getAppId();
                zzcgc.zzilo = zziw.zzaut();
                zzcgc.zzhtl = zziw.zzul();
                long zzaus = zziw.zzaus();
                zzcgc.zzizu = zzaus == -2147483648L ? null : Integer.valueOf((int) zzaus);
                zzcgc.zzizl = Long.valueOf(zziw.zzauu());
                zzcgc.zziln = zziw.getGmpAppId();
                zzcgc.zzizq = Long.valueOf(zziw.zzauv());
                if (isEnabled() && zzcap.zzaxg() && this.zzisj.zziu(zzcgc.zzch)) {
                    zzatz();
                    zzcgc.zzizz = null;
                }
                Pair zzjh = zzaul().zzjh(zziw.getAppId());
                if (!(zzjh == null || TextUtils.isEmpty((CharSequence) zzjh.first))) {
                    zzcgc.zzizn = (String) zzjh.first;
                    zzcgc.zzizo = (Boolean) zzjh.second;
                }
                zzaua().zzwh();
                zzcgc.zzizi = Build.MODEL;
                zzaua().zzwh();
                zzcgc.zzcy = VERSION.RELEASE;
                zzcgc.zzizk = Integer.valueOf((int) zzaua().zzaxv());
                zzcgc.zzizj = zzaua().zzaxw();
                zzcgc.zzizp = zziw.getAppInstanceId();
                zzcgc.zzilv = zziw.zzaup();
                List zziv = zzaue().zziv(zziw.getAppId());
                zzcgc.zzizb = new zzcge[zziv.size()];
                for (int i = 0; i < zziv.size(); i++) {
                    zzcge zzcge = new zzcge();
                    zzcgc.zzizb[i] = zzcge;
                    zzcge.name = ((zzcfn) zziv.get(i)).mName;
                    zzcge.zzjad = Long.valueOf(((zzcfn) zziv.get(i)).zziwy);
                    zzaug().zza(zzcge, ((zzcfn) zziv.get(i)).mValue);
                }
                Bundle zzaxy = zzcbc.zzinj.zzaxy();
                if ("_iap".equals(zzcbc.name)) {
                    zzaxy.putLong("_c", 1);
                    zzauk().zzayh().log("Marking in-app purchase as real-time");
                    zzaxy.putLong("_r", 1);
                }
                zzaxy.putString("_o", zzcbc.zzilz);
                if (zzaug().zzke(zzcgc.zzch)) {
                    zzaug().zza(zzaxy, "_dbg", Long.valueOf(1));
                    zzaug().zza(zzaxy, "_r", Long.valueOf(1));
                }
                zzcay zzah = zzaue().zzah(str, zzcbc.name);
                if (zzah == null) {
                    zzaue().zza(new zzcay(str, zzcbc.name, 1, 0, zzcbc.zzink));
                    j = 0;
                } else {
                    j = zzah.zzinf;
                    zzaue().zza(zzah.zzbb(zzcbc.zzink).zzaxx());
                }
                zzcax zzcax = new zzcax(this, zzcbc.zzilz, str, zzcbc.name, zzcbc.zzink, j, zzaxy);
                zzcfz zzcfz = new zzcfz();
                zzcgc.zziza = new zzcfz[]{zzcfz};
                zzcfz.zziyt = Long.valueOf(zzcax.zzfcw);
                zzcfz.name = zzcax.mName;
                zzcfz.zziyu = Long.valueOf(zzcax.zzinb);
                zzcfz.zziys = new zzcga[zzcax.zzinc.size()];
                Iterator it = zzcax.zzinc.iterator();
                int i2 = 0;
                while (it.hasNext()) {
                    String str2 = (String) it.next();
                    zzcga zzcga = new zzcga();
                    zzcfz.zziys[i2] = zzcga;
                    zzcga.name = str2;
                    zzaug().zza(zzcga, zzcax.zzinc.get(str2));
                    i2++;
                }
                zzcgc.zzizt = zza(zziw.getAppId(), zzcgc.zzizb, zzcgc.zziza);
                zzcgc.zzizd = zzcfz.zziyt;
                zzcgc.zzize = zzcfz.zziyt;
                long zzaur = zziw.zzaur();
                zzcgc.zzizg = zzaur != 0 ? Long.valueOf(zzaur) : null;
                zzaus = zziw.zzauq();
                if (zzaus == 0) {
                    zzaus = zzaur;
                }
                zzcgc.zzizf = zzaus != 0 ? Long.valueOf(zzaus) : null;
                zziw.zzava();
                zzcgc.zzizr = Integer.valueOf((int) zziw.zzaux());
                zzcgc.zzizm = Long.valueOf(zzcap.zzauu());
                zzcgc.zzizc = Long.valueOf(this.zzasl.currentTimeMillis());
                zzcgc.zzizs = Boolean.TRUE;
                zziw.zzal(zzcgc.zzizd.longValue());
                zziw.zzam(zzcgc.zzize.longValue());
                zzaue().zza(zziw);
                zzaue().setTransactionSuccessful();
                zzaue().endTransaction();
                try {
                    bArr = new byte[zzcgb.zzbjo()];
                    zzegg zzi = zzegg.zzi(bArr, 0, bArr.length);
                    zzcgb.zza(zzi);
                    zzi.zzccd();
                    return zzaug().zzo(bArr);
                } catch (IOException e) {
                    zzauk().zzayc().zze("Data loss. Failed to bundle and serialize. appId", zzcbo.zzjf(str), e);
                    return null;
                }
            } else {
                zzauk().zzayh().zzj("Log and bundle disabled. package_name", str);
                bArr = new byte[0];
                zzaue().endTransaction();
                return bArr;
            }
        } finally {
            zzaue().endTransaction();
        }
    }

    public final zzcaf zzatw() {
        zza(this.zzite);
        return this.zzite;
    }

    public final zzcam zzatx() {
        zza(this.zzitd);
        return this.zzitd;
    }

    public final zzcdo zzaty() {
        zza(this.zzisz);
        return this.zzisz;
    }

    public final zzcbj zzatz() {
        zza(this.zzita);
        return this.zzita;
    }

    public final zzcaw zzaua() {
        zza(this.zzisy);
        return this.zzisy;
    }

    public final zzceg zzaub() {
        zza(this.zzisx);
        return this.zzisx;
    }

    public final zzcec zzauc() {
        zza(this.zzisw);
        return this.zzisw;
    }

    public final zzcbk zzaud() {
        zza(this.zzisu);
        return this.zzisu;
    }

    public final zzcaq zzaue() {
        zza(this.zzist);
        return this.zzist;
    }

    public final zzcbm zzauf() {
        zza(this.zziss);
        return this.zziss;
    }

    public final zzcfo zzaug() {
        zza(this.zzisr);
        return this.zzisr;
    }

    public final zzcci zzauh() {
        zza(this.zziso);
        return this.zziso;
    }

    public final zzcfd zzaui() {
        zza(this.zzisn);
        return this.zzisn;
    }

    public final zzccj zzauj() {
        zza(this.zzism);
        return this.zzism;
    }

    public final zzcbo zzauk() {
        zza(this.zzisl);
        return this.zzisl;
    }

    public final zzcbz zzaul() {
        zza(this.zzisk);
        return this.zzisk;
    }

    public final zzcap zzaum() {
        return this.zzisj;
    }

    @WorkerThread
    protected final boolean zzayu() {
        boolean z = false;
        zzwh();
        zzauj().zzug();
        if (this.zzitg == null || this.zzith == 0 || !(this.zzitg == null || this.zzitg.booleanValue() || Math.abs(this.zzasl.elapsedRealtime() - this.zzith) <= 1000)) {
            this.zzith = this.zzasl.elapsedRealtime();
            zzcap.zzawj();
            if (zzaug().zzdu("android.permission.INTERNET") && zzaug().zzdu("android.permission.ACCESS_NETWORK_STATE") && (zzbdp.zzcs(this.mContext).zzalq() || (zzccf.zzj(this.mContext, false) && zzcez.zzk(this.mContext, false)))) {
                z = true;
            }
            this.zzitg = Boolean.valueOf(z);
            if (this.zzitg.booleanValue()) {
                this.zzitg = Boolean.valueOf(zzaug().zzkb(zzatz().getGmpAppId()));
            }
        }
        return this.zzitg.booleanValue();
    }

    public final zzcbo zzayv() {
        return (this.zzisl == null || !this.zzisl.isInitialized()) ? null : this.zzisl;
    }

    final zzccj zzayw() {
        return this.zzism;
    }

    public final AppMeasurement zzayx() {
        return this.zzisp;
    }

    public final FirebaseAnalytics zzayy() {
        return this.zzisq;
    }

    public final zzcbs zzayz() {
        zza(this.zzisv);
        return this.zzisv;
    }

    final long zzazd() {
        Long valueOf = Long.valueOf(zzaul().zziql.get());
        return valueOf.longValue() == 0 ? this.zzitt : Math.min(this.zzitt, valueOf.longValue());
    }

    @WorkerThread
    public final void zzazf() {
        zzauj().zzug();
        zzwh();
        this.zzits = true;
        String zzaxh;
        String zzawt;
        try {
            zzcap.zzawj();
            Boolean zzayn = zzaul().zzayn();
            if (zzayn == null) {
                zzauk().zzaye().log("Upload data called on the client side before use of service was decided");
                this.zzits = false;
                zzazl();
            } else if (zzayn.booleanValue()) {
                zzauk().zzayc().log("Upload called in the client side when service should be used");
                this.zzits = false;
                zzazl();
            } else if (this.zzitp > 0) {
                zzazh();
                this.zzits = false;
                zzazl();
            } else {
                zzauj().zzug();
                if ((this.zzitk != null ? 1 : null) != null) {
                    zzauk().zzayi().log("Uploading requested multiple times");
                    this.zzits = false;
                    zzazl();
                } else if (zzayz().zzyu()) {
                    long currentTimeMillis = this.zzasl.currentTimeMillis();
                    zzg(null, currentTimeMillis - zzcap.zzawu());
                    long j = zzaul().zziqg.get();
                    if (j != 0) {
                        zzauk().zzayh().zzj("Uploading events. Elapsed time since last upload attempt (ms)", Long.valueOf(Math.abs(currentTimeMillis - j)));
                    }
                    zzaxh = zzaue().zzaxh();
                    Object zzba;
                    if (TextUtils.isEmpty(zzaxh)) {
                        this.zzito = -1;
                        zzba = zzaue().zzba(currentTimeMillis - zzcap.zzawu());
                        if (!TextUtils.isEmpty(zzba)) {
                            zzcaj zziw = zzaue().zziw(zzba);
                            if (zziw != null) {
                                zzb(zziw);
                            }
                        }
                    } else {
                        if (this.zzito == -1) {
                            this.zzito = zzaue().zzaxo();
                        }
                        List<Pair> zzl = zzaue().zzl(zzaxh, this.zzisj.zzb(zzaxh, zzcbe.zziny), Math.max(0, this.zzisj.zzb(zzaxh, zzcbe.zzinz)));
                        if (!zzl.isEmpty()) {
                            zzcgc zzcgc;
                            Object obj;
                            int i;
                            List subList;
                            for (Pair pair : zzl) {
                                zzcgc = (zzcgc) pair.first;
                                if (!TextUtils.isEmpty(zzcgc.zzizn)) {
                                    obj = zzcgc.zzizn;
                                    break;
                                }
                            }
                            obj = null;
                            if (obj != null) {
                                for (i = 0; i < zzl.size(); i++) {
                                    zzcgc = (zzcgc) ((Pair) zzl.get(i)).first;
                                    if (!TextUtils.isEmpty(zzcgc.zzizn) && !zzcgc.zzizn.equals(obj)) {
                                        subList = zzl.subList(0, i);
                                        break;
                                    }
                                }
                            }
                            subList = zzl;
                            zzcgb zzcgb = new zzcgb();
                            zzcgb.zziyx = new zzcgc[subList.size()];
                            Collection arrayList = new ArrayList(subList.size());
                            Object obj2 = (zzcap.zzaxg() && this.zzisj.zziu(zzaxh)) ? 1 : null;
                            for (i = 0; i < zzcgb.zziyx.length; i++) {
                                zzcgb.zziyx[i] = (zzcgc) ((Pair) subList.get(i)).first;
                                arrayList.add((Long) ((Pair) subList.get(i)).second);
                                zzcgb.zziyx[i].zzizm = Long.valueOf(zzcap.zzauu());
                                zzcgb.zziyx[i].zzizc = Long.valueOf(currentTimeMillis);
                                zzcgb.zziyx[i].zzizs = Boolean.valueOf(zzcap.zzawj());
                                if (obj2 == null) {
                                    zzcgb.zziyx[i].zzizz = null;
                                }
                            }
                            zzba = zzauk().zzad(2) ? zzauf().zza(zzcgb) : null;
                            obj = zzaug().zzb(zzcgb);
                            zzawt = zzcap.zzawt();
                            URL url = new URL(zzawt);
                            zzbp.zzbh(!arrayList.isEmpty());
                            if (this.zzitk != null) {
                                zzauk().zzayc().log("Set uploading progress before finishing the previous upload");
                            } else {
                                this.zzitk = new ArrayList(arrayList);
                            }
                            zzaul().zziqh.set(currentTimeMillis);
                            obj2 = "?";
                            if (zzcgb.zziyx.length > 0) {
                                obj2 = zzcgb.zziyx[0].zzch;
                            }
                            zzauk().zzayi().zzd("Uploading data. app, uncompressed size, data", obj2, Integer.valueOf(obj.length), zzba);
                            this.zzitr = true;
                            zzcdl zzayz = zzayz();
                            zzcbu zzccr = new zzccr(this);
                            zzayz.zzug();
                            zzayz.zzwh();
                            zzbp.zzu(url);
                            zzbp.zzu(obj);
                            zzbp.zzu(zzccr);
                            zzayz.zzauj().zzh(new zzcbw(zzayz, zzaxh, url, obj, null, zzccr));
                        }
                    }
                    this.zzits = false;
                    zzazl();
                } else {
                    zzauk().zzayi().log("Network not connected, ignoring upload request");
                    zzazh();
                    this.zzits = false;
                    zzazl();
                }
            }
        } catch (MalformedURLException e) {
            zzauk().zzayc().zze("Failed to parse upload URL. Not uploading. appId", zzcbo.zzjf(zzaxh), zzawt);
        } catch (Throwable th) {
            this.zzits = false;
            zzazl();
        }
    }

    final void zzazi() {
        this.zzitn++;
    }

    @WorkerThread
    final void zzazj() {
        zzauj().zzug();
        zzwh();
        if (!this.zzitf) {
            zzauk().zzayg().log("This instance being marked as an uploader");
            zzauj().zzug();
            zzwh();
            if (zzazk() && zzazc()) {
                int zza = zza(this.zzitj);
                int zzaya = zzatz().zzaya();
                zzauj().zzug();
                if (zza > zzaya) {
                    zzauk().zzayc().zze("Panic: can't downgrade version. Previous, current version", Integer.valueOf(zza), Integer.valueOf(zzaya));
                } else if (zza < zzaya) {
                    if (zza(zzaya, this.zzitj)) {
                        zzauk().zzayi().zze("Storage version upgraded. Previous, current version", Integer.valueOf(zza), Integer.valueOf(zzaya));
                    } else {
                        zzauk().zzayc().zze("Storage version upgrade failed. Previous, current version", Integer.valueOf(zza), Integer.valueOf(zzaya));
                    }
                }
            }
            this.zzitf = true;
            zzazh();
        }
    }

    @WorkerThread
    final void zzb(zzcan zzcan, zzcak zzcak) {
        boolean z = true;
        zzbp.zzu(zzcan);
        zzbp.zzgf(zzcan.packageName);
        zzbp.zzu(zzcan.zzilz);
        zzbp.zzu(zzcan.zzima);
        zzbp.zzgf(zzcan.zzima.name);
        zzauj().zzug();
        zzwh();
        if (!TextUtils.isEmpty(zzcak.zziln)) {
            if (zzcak.zzils) {
                zzcan zzcan2 = new zzcan(zzcan);
                zzcan2.zzimc = false;
                zzaue().beginTransaction();
                try {
                    zzcan zzak = zzaue().zzak(zzcan2.packageName, zzcan2.zzima.name);
                    if (!(zzak == null || zzak.zzilz.equals(zzcan2.zzilz))) {
                        zzauk().zzaye().zzd("Updating a conditional user property with different origin. name, origin, origin (from DB)", zzauf().zzje(zzcan2.zzima.name), zzcan2.zzilz, zzak.zzilz);
                    }
                    if (zzak != null && zzak.zzimc) {
                        zzcan2.zzilz = zzak.zzilz;
                        zzcan2.zzimb = zzak.zzimb;
                        zzcan2.zzimf = zzak.zzimf;
                        zzcan2.zzimd = zzak.zzimd;
                        zzcan2.zzimg = zzak.zzimg;
                        zzcan2.zzimc = zzak.zzimc;
                        zzcan2.zzima = new zzcfl(zzcan2.zzima.name, zzak.zzima.zziwu, zzcan2.zzima.getValue(), zzak.zzima.zzilz);
                        z = false;
                    } else if (TextUtils.isEmpty(zzcan2.zzimd)) {
                        zzcan2.zzima = new zzcfl(zzcan2.zzima.name, zzcan2.zzimb, zzcan2.zzima.getValue(), zzcan2.zzima.zzilz);
                        zzcan2.zzimc = true;
                    } else {
                        z = false;
                    }
                    if (zzcan2.zzimc) {
                        zzcfl zzcfl = zzcan2.zzima;
                        zzcfn zzcfn = new zzcfn(zzcan2.packageName, zzcan2.zzilz, zzcfl.name, zzcfl.zziwu, zzcfl.getValue());
                        if (zzaue().zza(zzcfn)) {
                            zzauk().zzayh().zzd("User property updated immediately", zzcan2.packageName, zzauf().zzje(zzcfn.mName), zzcfn.mValue);
                        } else {
                            zzauk().zzayc().zzd("(2)Too many active user properties, ignoring", zzcbo.zzjf(zzcan2.packageName), zzauf().zzje(zzcfn.mName), zzcfn.mValue);
                        }
                        if (z && zzcan2.zzimg != null) {
                            zzc(new zzcbc(zzcan2.zzimg, zzcan2.zzimb), zzcak);
                        }
                    }
                    if (zzaue().zza(zzcan2)) {
                        zzauk().zzayh().zzd("Conditional property added", zzcan2.packageName, zzauf().zzje(zzcan2.zzima.name), zzcan2.zzima.getValue());
                    } else {
                        zzauk().zzayc().zzd("Too many conditional properties, ignoring", zzcbo.zzjf(zzcan2.packageName), zzauf().zzje(zzcan2.zzima.name), zzcan2.zzima.getValue());
                    }
                    zzaue().setTransactionSuccessful();
                } finally {
                    zzaue().endTransaction();
                }
            } else {
                zzf(zzcak);
            }
        }
    }

    @WorkerThread
    final void zzb(zzcbc zzcbc, zzcak zzcak) {
        zzbp.zzu(zzcak);
        zzbp.zzgf(zzcak.packageName);
        zzauj().zzug();
        zzwh();
        String str = zzcak.packageName;
        long j = zzcbc.zzink;
        zzaug();
        if (!zzcfo.zzd(zzcbc, zzcak)) {
            return;
        }
        if (zzcak.zzils) {
            zzaue().beginTransaction();
            try {
                List emptyList;
                Object obj;
                zzcdl zzaue = zzaue();
                zzbp.zzgf(str);
                zzaue.zzug();
                zzaue.zzwh();
                if (j < 0) {
                    zzaue.zzauk().zzaye().zze("Invalid time querying timed out conditional properties", zzcbo.zzjf(str), Long.valueOf(j));
                    emptyList = Collections.emptyList();
                } else {
                    emptyList = zzaue.zzc("active=0 and app_id=? and abs(? - creation_timestamp) > trigger_timeout", new String[]{str, String.valueOf(j)});
                }
                for (zzcan zzcan : r2) {
                    if (zzcan != null) {
                        zzauk().zzayh().zzd("User property timed out", zzcan.packageName, zzauf().zzje(zzcan.zzima.name), zzcan.zzima.getValue());
                        if (zzcan.zzime != null) {
                            zzc(new zzcbc(zzcan.zzime, j), zzcak);
                        }
                        zzaue().zzal(str, zzcan.zzima.name);
                    }
                }
                zzaue = zzaue();
                zzbp.zzgf(str);
                zzaue.zzug();
                zzaue.zzwh();
                if (j < 0) {
                    zzaue.zzauk().zzaye().zze("Invalid time querying expired conditional properties", zzcbo.zzjf(str), Long.valueOf(j));
                    emptyList = Collections.emptyList();
                } else {
                    emptyList = zzaue.zzc("active<>0 and app_id=? and abs(? - triggered_timestamp) > time_to_live", new String[]{str, String.valueOf(j)});
                }
                List arrayList = new ArrayList(r2.size());
                for (zzcan zzcan2 : r2) {
                    if (zzcan2 != null) {
                        zzauk().zzayh().zzd("User property expired", zzcan2.packageName, zzauf().zzje(zzcan2.zzima.name), zzcan2.zzima.getValue());
                        zzaue().zzai(str, zzcan2.zzima.name);
                        if (zzcan2.zzimi != null) {
                            arrayList.add(zzcan2.zzimi);
                        }
                        zzaue().zzal(str, zzcan2.zzima.name);
                    }
                }
                ArrayList arrayList2 = (ArrayList) arrayList;
                int size = arrayList2.size();
                int i = 0;
                while (i < size) {
                    obj = arrayList2.get(i);
                    i++;
                    zzc(new zzcbc((zzcbc) obj, j), zzcak);
                }
                zzaue = zzaue();
                String str2 = zzcbc.name;
                zzbp.zzgf(str);
                zzbp.zzgf(str2);
                zzaue.zzug();
                zzaue.zzwh();
                if (j < 0) {
                    zzaue.zzauk().zzaye().zzd("Invalid time querying triggered conditional properties", zzcbo.zzjf(str), zzaue.zzauf().zzjc(str2), Long.valueOf(j));
                    emptyList = Collections.emptyList();
                } else {
                    emptyList = zzaue.zzc("active=0 and app_id=? and trigger_event_name=? and abs(? - creation_timestamp) <= trigger_timeout", new String[]{str, str2, String.valueOf(j)});
                }
                List arrayList3 = new ArrayList(r2.size());
                for (zzcan zzcan3 : r2) {
                    if (zzcan3 != null) {
                        zzcfl zzcfl = zzcan3.zzima;
                        zzcfn zzcfn = new zzcfn(zzcan3.packageName, zzcan3.zzilz, zzcfl.name, j, zzcfl.getValue());
                        if (zzaue().zza(zzcfn)) {
                            zzauk().zzayh().zzd("User property triggered", zzcan3.packageName, zzauf().zzje(zzcfn.mName), zzcfn.mValue);
                        } else {
                            zzauk().zzayc().zzd("Too many active user properties, ignoring", zzcbo.zzjf(zzcan3.packageName), zzauf().zzje(zzcfn.mName), zzcfn.mValue);
                        }
                        if (zzcan3.zzimg != null) {
                            arrayList3.add(zzcan3.zzimg);
                        }
                        zzcan3.zzima = new zzcfl(zzcfn);
                        zzcan3.zzimc = true;
                        zzaue().zza(zzcan3);
                    }
                }
                zzc(zzcbc, zzcak);
                arrayList2 = (ArrayList) arrayList3;
                int size2 = arrayList2.size();
                i = 0;
                while (i < size2) {
                    obj = arrayList2.get(i);
                    i++;
                    zzc(new zzcbc((zzcbc) obj, j), zzcak);
                }
                zzaue().setTransactionSuccessful();
            } finally {
                zzaue().endTransaction();
            }
        } else {
            zzf(zzcak);
        }
    }

    @WorkerThread
    final void zzb(zzcbc zzcbc, String str) {
        zzcaj zziw = zzaue().zziw(str);
        if (zziw == null || TextUtils.isEmpty(zziw.zzul())) {
            zzauk().zzayh().zzj("No app data available; dropping event", str);
            return;
        }
        try {
            String str2 = zzbdp.zzcs(this.mContext).getPackageInfo(str, 0).versionName;
            if (!(zziw.zzul() == null || zziw.zzul().equals(str2))) {
                zzauk().zzaye().zzj("App version does not match; dropping event. appId", zzcbo.zzjf(str));
                return;
            }
        } catch (NameNotFoundException e) {
            if (!"_ui".equals(zzcbc.name)) {
                zzauk().zzaye().zzj("Could not find package. appId", zzcbo.zzjf(str));
            }
        }
        zzcbc zzcbc2 = zzcbc;
        zzb(zzcbc2, new zzcak(str, zziw.getGmpAppId(), zziw.zzul(), zziw.zzaus(), zziw.zzaut(), zziw.zzauu(), zziw.zzauv(), null, zziw.zzauw(), false, zziw.zzaup(), zziw.zzavj(), 0, 0));
    }

    final void zzb(zzcdm zzcdm) {
        this.zzitm++;
    }

    @WorkerThread
    final void zzb(zzcfl zzcfl, zzcak zzcak) {
        int i = 0;
        zzauj().zzug();
        zzwh();
        if (!TextUtils.isEmpty(zzcak.zziln)) {
            if (zzcak.zzils) {
                int zzjy = zzaug().zzjy(zzcfl.name);
                String zza;
                if (zzjy != 0) {
                    zzaug();
                    zza = zzcfo.zza(zzcfl.name, zzcap.zzavn(), true);
                    if (zzcfl.name != null) {
                        i = zzcfl.name.length();
                    }
                    zzaug().zza(zzcak.packageName, zzjy, "_ev", zza, i);
                    return;
                }
                zzjy = zzaug().zzl(zzcfl.name, zzcfl.getValue());
                if (zzjy != 0) {
                    zzaug();
                    zza = zzcfo.zza(zzcfl.name, zzcap.zzavn(), true);
                    Object value = zzcfl.getValue();
                    if (value != null && ((value instanceof String) || (value instanceof CharSequence))) {
                        i = String.valueOf(value).length();
                    }
                    zzaug().zza(zzcak.packageName, zzjy, "_ev", zza, i);
                    return;
                }
                Object zzm = zzaug().zzm(zzcfl.name, zzcfl.getValue());
                if (zzm != null) {
                    zzcfn zzcfn = new zzcfn(zzcak.packageName, zzcfl.zzilz, zzcfl.name, zzcfl.zziwu, zzm);
                    zzauk().zzayh().zze("Setting user property", zzauf().zzje(zzcfn.mName), zzm);
                    zzaue().beginTransaction();
                    try {
                        zzf(zzcak);
                        boolean zza2 = zzaue().zza(zzcfn);
                        zzaue().setTransactionSuccessful();
                        if (zza2) {
                            zzauk().zzayh().zze("User property set", zzauf().zzje(zzcfn.mName), zzcfn.mValue);
                        } else {
                            zzauk().zzayc().zze("Too many unique user properties are set. Ignoring user property", zzauf().zzje(zzcfn.mName), zzcfn.mValue);
                            zzaug().zza(zzcak.packageName, 9, null, null, 0);
                        }
                        zzaue().endTransaction();
                        return;
                    } catch (Throwable th) {
                        zzaue().endTransaction();
                    }
                } else {
                    return;
                }
            }
            zzf(zzcak);
        }
    }

    @WorkerThread
    final void zzb(String str, int i, Throwable th, byte[] bArr, Map<String, List<String>> map) {
        boolean z = true;
        zzauj().zzug();
        zzwh();
        zzbp.zzgf(str);
        if (bArr == null) {
            try {
                bArr = new byte[0];
            } catch (Throwable th2) {
                this.zzitq = false;
                zzazl();
            }
        }
        zzauk().zzayi().zzj("onConfigFetched. Response size", Integer.valueOf(bArr.length));
        zzaue().beginTransaction();
        zzcaj zziw = zzaue().zziw(str);
        boolean z2 = (i == 200 || i == 204 || i == 304) && th == null;
        if (zziw == null) {
            zzauk().zzaye().zzj("App does not exist in onConfigFetched. appId", zzcbo.zzjf(str));
        } else if (z2 || i == 404) {
            List list = map != null ? (List) map.get(HttpRequest.HEADER_LAST_MODIFIED) : null;
            String str2 = (list == null || list.size() <= 0) ? null : (String) list.get(0);
            if (i == 404 || i == 304) {
                if (zzauh().zzjn(str) == null && !zzauh().zzb(str, null, null)) {
                    zzaue().endTransaction();
                    this.zzitq = false;
                    zzazl();
                    return;
                }
            } else if (!zzauh().zzb(str, bArr, str2)) {
                zzaue().endTransaction();
                this.zzitq = false;
                zzazl();
                return;
            }
            zziw.zzar(this.zzasl.currentTimeMillis());
            zzaue().zza(zziw);
            if (i == 404) {
                zzauk().zzayf().zzj("Config not found. Using empty config. appId", str);
            } else {
                zzauk().zzayi().zze("Successfully fetched config. Got network response. code, size", Integer.valueOf(i), Integer.valueOf(bArr.length));
            }
            if (zzayz().zzyu() && zzazg()) {
                zzazf();
            } else {
                zzazh();
            }
        } else {
            zziw.zzas(this.zzasl.currentTimeMillis());
            zzaue().zza(zziw);
            zzauk().zzayi().zze("Fetching config failed. code, error", Integer.valueOf(i), th);
            zzauh().zzjp(str);
            zzaul().zziqh.set(this.zzasl.currentTimeMillis());
            if (!(i == 503 || i == 429)) {
                z = false;
            }
            if (z) {
                zzaul().zziqi.set(this.zzasl.currentTimeMillis());
            }
            zzazh();
        }
        zzaue().setTransactionSuccessful();
        zzaue().endTransaction();
        this.zzitq = false;
        zzazl();
    }

    public final void zzbo(boolean z) {
        zzazh();
    }

    @WorkerThread
    final void zzc(zzcan zzcan, zzcak zzcak) {
        zzbp.zzu(zzcan);
        zzbp.zzgf(zzcan.packageName);
        zzbp.zzu(zzcan.zzima);
        zzbp.zzgf(zzcan.zzima.name);
        zzauj().zzug();
        zzwh();
        if (!TextUtils.isEmpty(zzcak.zziln)) {
            if (zzcak.zzils) {
                zzaue().beginTransaction();
                try {
                    zzf(zzcak);
                    zzcan zzak = zzaue().zzak(zzcan.packageName, zzcan.zzima.name);
                    if (zzak != null) {
                        zzauk().zzayh().zze("Removing conditional user property", zzcan.packageName, zzauf().zzje(zzcan.zzima.name));
                        zzaue().zzal(zzcan.packageName, zzcan.zzima.name);
                        if (zzak.zzimc) {
                            zzaue().zzai(zzcan.packageName, zzcan.zzima.name);
                        }
                        if (zzcan.zzimi != null) {
                            Bundle bundle = null;
                            if (zzcan.zzimi.zzinj != null) {
                                bundle = zzcan.zzimi.zzinj.zzaxy();
                            }
                            zzc(zzaug().zza(zzcan.zzimi.name, bundle, zzak.zzilz, zzcan.zzimi.zzink, true, false), zzcak);
                        }
                    } else {
                        zzauk().zzaye().zze("Conditional user property doesn't exist", zzcbo.zzjf(zzcan.packageName), zzauf().zzje(zzcan.zzima.name));
                    }
                    zzaue().setTransactionSuccessful();
                } finally {
                    zzaue().endTransaction();
                }
            } else {
                zzf(zzcak);
            }
        }
    }

    @WorkerThread
    final void zzc(zzcfl zzcfl, zzcak zzcak) {
        zzauj().zzug();
        zzwh();
        if (!TextUtils.isEmpty(zzcak.zziln)) {
            if (zzcak.zzils) {
                zzauk().zzayh().zzj("Removing user property", zzauf().zzje(zzcfl.name));
                zzaue().beginTransaction();
                try {
                    zzf(zzcak);
                    zzaue().zzai(zzcak.packageName, zzcfl.name);
                    zzaue().setTransactionSuccessful();
                    zzauk().zzayh().zzj("User property removed", zzauf().zzje(zzcfl.name));
                } finally {
                    zzaue().endTransaction();
                }
            } else {
                zzf(zzcak);
            }
        }
    }

    final void zzd(zzcak zzcak) {
        zzauj().zzug();
        zzwh();
        zzbp.zzgf(zzcak.packageName);
        zzf(zzcak);
    }

    @WorkerThread
    final void zzd(zzcan zzcan) {
        zzcak zzjr = zzjr(zzcan.packageName);
        if (zzjr != null) {
            zzb(zzcan, zzjr);
        }
    }

    @WorkerThread
    public final void zze(zzcak zzcak) {
        zzauj().zzug();
        zzwh();
        zzbp.zzu(zzcak);
        zzbp.zzgf(zzcak.packageName);
        if (!TextUtils.isEmpty(zzcak.zziln)) {
            zzcaj zziw = zzaue().zziw(zzcak.packageName);
            if (!(zziw == null || !TextUtils.isEmpty(zziw.getGmpAppId()) || TextUtils.isEmpty(zzcak.zziln))) {
                zziw.zzar(0);
                zzaue().zza(zziw);
                zzauh().zzjq(zzcak.packageName);
            }
            if (zzcak.zzils) {
                int i;
                Bundle bundle;
                long j = zzcak.zzilx;
                if (j == 0) {
                    j = this.zzasl.currentTimeMillis();
                }
                int i2 = zzcak.zzily;
                if (i2 == 0 || i2 == 1) {
                    i = i2;
                } else {
                    zzauk().zzaye().zze("Incorrect app type, assuming installed app. appId, appType", zzcbo.zzjf(zzcak.packageName), Integer.valueOf(i2));
                    i = 0;
                }
                zzaue().beginTransaction();
                zzcdl zzaue;
                String appId;
                try {
                    zziw = zzaue().zziw(zzcak.packageName);
                    if (!(zziw == null || zziw.getGmpAppId() == null || zziw.getGmpAppId().equals(zzcak.zziln))) {
                        zzauk().zzaye().zzj("New GMP App Id passed in. Removing cached database data. appId", zzcbo.zzjf(zziw.getAppId()));
                        zzaue = zzaue();
                        appId = zziw.getAppId();
                        zzaue.zzwh();
                        zzaue.zzug();
                        zzbp.zzgf(appId);
                        SQLiteDatabase writableDatabase = zzaue.getWritableDatabase();
                        String[] strArr = new String[1];
                        strArr[0] = appId;
                        int delete = writableDatabase.delete("events", "app_id=?", strArr);
                        int delete2 = writableDatabase.delete("user_attributes", "app_id=?", strArr);
                        int delete3 = writableDatabase.delete("conditional_properties", "app_id=?", strArr);
                        int delete4 = writableDatabase.delete("apps", "app_id=?", strArr);
                        int delete5 = writableDatabase.delete("raw_events", "app_id=?", strArr);
                        int delete6 = writableDatabase.delete("raw_events_metadata", "app_id=?", strArr);
                        i2 = writableDatabase.delete("audience_filter_values", "app_id=?", strArr) + ((((((((delete + 0) + delete2) + delete3) + delete4) + delete5) + delete6) + writableDatabase.delete("event_filters", "app_id=?", strArr)) + writableDatabase.delete("property_filters", "app_id=?", strArr));
                        if (i2 > 0) {
                            zzaue.zzauk().zzayi().zze("Deleted application data. app, records", appId, Integer.valueOf(i2));
                        }
                        zziw = null;
                    }
                } catch (SQLiteException e) {
                    zzaue.zzauk().zzayc().zze("Error deleting application data. appId, error", zzcbo.zzjf(appId), e);
                } catch (Throwable th) {
                    zzaue().endTransaction();
                }
                if (zziw != null) {
                    if (!(zziw.zzul() == null || zziw.zzul().equals(zzcak.zzhtl))) {
                        bundle = new Bundle();
                        bundle.putString("_pv", zziw.zzul());
                        zzb(new zzcbc("_au", new zzcaz(bundle), "auto", j), zzcak);
                    }
                }
                zzf(zzcak);
                zzcay zzcay = null;
                if (i == 0) {
                    zzcay = zzaue().zzah(zzcak.packageName, "_f");
                } else if (i == 1) {
                    zzcay = zzaue().zzah(zzcak.packageName, "_v");
                }
                if (zzcay == null) {
                    long j2 = (1 + (j / DateUtils.MILLIS_PER_HOUR)) * DateUtils.MILLIS_PER_HOUR;
                    if (i == 0) {
                        zzb(new zzcfl("_fot", j, Long.valueOf(j2), "auto"), zzcak);
                        zzauj().zzug();
                        zzwh();
                        Bundle bundle2 = new Bundle();
                        bundle2.putLong("_c", 1);
                        bundle2.putLong("_r", 1);
                        bundle2.putLong("_uwa", 0);
                        bundle2.putLong("_pfo", 0);
                        bundle2.putLong("_sys", 0);
                        bundle2.putLong("_sysu", 0);
                        if (this.mContext.getPackageManager() == null) {
                            zzauk().zzayc().zzj("PackageManager is null, first open report might be inaccurate. appId", zzcbo.zzjf(zzcak.packageName));
                        } else {
                            ApplicationInfo applicationInfo;
                            PackageInfo packageInfo = null;
                            try {
                                packageInfo = zzbdp.zzcs(this.mContext).getPackageInfo(zzcak.packageName, 0);
                            } catch (NameNotFoundException e2) {
                                zzauk().zzayc().zze("Package info is null, first open report might be inaccurate. appId", zzcbo.zzjf(zzcak.packageName), e2);
                            }
                            if (packageInfo != null) {
                                if (packageInfo.firstInstallTime != 0) {
                                    Object obj = null;
                                    if (packageInfo.firstInstallTime != packageInfo.lastUpdateTime) {
                                        bundle2.putLong("_uwa", 1);
                                    } else {
                                        obj = 1;
                                    }
                                    zzb(new zzcfl("_fi", j, Long.valueOf(obj != null ? 1 : 0), "auto"), zzcak);
                                }
                            }
                            try {
                                applicationInfo = zzbdp.zzcs(this.mContext).getApplicationInfo(zzcak.packageName, 0);
                            } catch (NameNotFoundException e22) {
                                zzauk().zzayc().zze("Application info is null, first open report might be inaccurate. appId", zzcbo.zzjf(zzcak.packageName), e22);
                                applicationInfo = null;
                            }
                            if (applicationInfo != null) {
                                if ((applicationInfo.flags & 1) != 0) {
                                    bundle2.putLong("_sys", 1);
                                }
                                if ((applicationInfo.flags & 128) != 0) {
                                    bundle2.putLong("_sysu", 1);
                                }
                            }
                        }
                        zzcdl zzaue2 = zzaue();
                        String str = zzcak.packageName;
                        zzbp.zzgf(str);
                        zzaue2.zzug();
                        zzaue2.zzwh();
                        j2 = zzaue2.zzao(str, "first_open_count");
                        if (j2 >= 0) {
                            bundle2.putLong("_pfo", j2);
                        }
                        zzb(new zzcbc("_f", new zzcaz(bundle2), "auto", j), zzcak);
                    } else if (i == 1) {
                        zzb(new zzcfl("_fvt", j, Long.valueOf(j2), "auto"), zzcak);
                        zzauj().zzug();
                        zzwh();
                        bundle = new Bundle();
                        bundle.putLong("_c", 1);
                        bundle.putLong("_r", 1);
                        zzb(new zzcbc("_v", new zzcaz(bundle), "auto", j), zzcak);
                    }
                    bundle = new Bundle();
                    bundle.putLong("_et", 1);
                    zzb(new zzcbc("_e", new zzcaz(bundle), "auto", j), zzcak);
                } else if (zzcak.zzilt) {
                    zzb(new zzcbc("_cd", new zzcaz(new Bundle()), "auto", j), zzcak);
                }
                zzaue().setTransactionSuccessful();
                zzaue().endTransaction();
                return;
            }
            zzf(zzcak);
        }
    }

    @WorkerThread
    final void zze(zzcan zzcan) {
        zzcak zzjr = zzjr(zzcan.packageName);
        if (zzjr != null) {
            zzc(zzcan, zzjr);
        }
    }

    @WorkerThread
    final void zzi(Runnable runnable) {
        zzauj().zzug();
        if (this.zzitl == null) {
            this.zzitl = new ArrayList();
        }
        this.zzitl.add(runnable);
    }

    public final String zzjs(String str) {
        Object e;
        try {
            return (String) zzauj().zzd(new zzccq(this, str)).get(30000, TimeUnit.MILLISECONDS);
        } catch (TimeoutException e2) {
            e = e2;
        } catch (InterruptedException e3) {
            e = e3;
        } catch (ExecutionException e4) {
            e = e4;
        }
        zzauk().zzayc().zze("Failed to get app instance id. appId", zzcbo.zzjf(str), e);
        return null;
    }

    public final zzd zzvu() {
        return this.zzasl;
    }

    final void zzwh() {
        if (!this.zzdoj) {
            throw new IllegalStateException("AppMeasurement is not initialized");
        }
    }
}
