package com.google.android.gms.measurement.internal;

import android.accounts.Account;
import android.accounts.AccountManager;
import android.accounts.AuthenticatorException;
import android.accounts.OperationCanceledException;
import android.content.Context;
import android.content.pm.PackageManager;
import android.content.pm.PackageManager.NameNotFoundException;
import android.support.annotation.WorkerThread;
import android.support.p000v4.content.ContextCompat;
import com.google.android.gms.common.util.Clock;
import java.io.IOException;
import java.util.Calendar;
import java.util.Locale;
import java.util.concurrent.TimeUnit;
import org.apache.commons.lang3.time.DateUtils;

public final class zzac extends zzge {
    private long zzey;
    private String zzez;
    private Boolean zzfa;
    private AccountManager zzfb;
    private Boolean zzfc;
    private long zzfd;

    zzac(zzfj zzfj) {
        super(zzfj);
    }

    public final /* bridge */ /* synthetic */ Context getContext() {
        return super.getContext();
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
        Calendar instance = Calendar.getInstance();
        this.zzey = TimeUnit.MINUTES.convert((long) (instance.get(16) + instance.get(15)), TimeUnit.MILLISECONDS);
        Locale locale = Locale.getDefault();
        String lowerCase = locale.getLanguage().toLowerCase(Locale.ENGLISH);
        String lowerCase2 = locale.getCountry().toLowerCase(Locale.ENGLISH);
        this.zzez = new StringBuilder(String.valueOf(lowerCase).length() + 1 + String.valueOf(lowerCase2).length()).append(lowerCase).append("-").append(lowerCase2).toString();
        return false;
    }

    public final long zzcq() {
        zzbi();
        return this.zzey;
    }

    public final String zzcr() {
        zzbi();
        return this.zzez;
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final long zzcs() {
        zzo();
        return this.zzfd;
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final void zzct() {
        zzo();
        this.zzfc = null;
        this.zzfd = 0;
    }

    /* access modifiers changed from: 0000 */
    @WorkerThread
    public final boolean zzcu() {
        zzo();
        long currentTimeMillis = zzx().currentTimeMillis();
        if (currentTimeMillis - this.zzfd > DateUtils.MILLIS_PER_DAY) {
            this.zzfc = null;
        }
        if (this.zzfc != null) {
            return this.zzfc.booleanValue();
        }
        if (ContextCompat.checkSelfPermission(getContext(), "android.permission.GET_ACCOUNTS") != 0) {
            zzab().zzgo().zzao("Permission error checking for dasher/unicorn accounts");
            this.zzfd = currentTimeMillis;
            this.zzfc = Boolean.valueOf(false);
            return false;
        }
        if (this.zzfb == null) {
            this.zzfb = AccountManager.get(getContext());
        }
        try {
            Account[] accountArr = (Account[]) this.zzfb.getAccountsByTypeAndFeatures("com.google", new String[]{"service_HOSTED"}, null, null).getResult();
            if (accountArr == null || accountArr.length <= 0) {
                Account[] accountArr2 = (Account[]) this.zzfb.getAccountsByTypeAndFeatures("com.google", new String[]{"service_uca"}, null, null).getResult();
                if (accountArr2 != null && accountArr2.length > 0) {
                    this.zzfc = Boolean.valueOf(true);
                    this.zzfd = currentTimeMillis;
                    return true;
                }
                this.zzfd = currentTimeMillis;
                this.zzfc = Boolean.valueOf(false);
                return false;
            }
            this.zzfc = Boolean.valueOf(true);
            this.zzfd = currentTimeMillis;
            return true;
        } catch (AuthenticatorException | OperationCanceledException | IOException e) {
            zzab().zzgl().zza("Exception checking account types", e);
        }
    }

    public final boolean zzj(Context context) {
        if (this.zzfa == null) {
            zzae();
            this.zzfa = Boolean.valueOf(false);
            try {
                PackageManager packageManager = context.getPackageManager();
                if (packageManager != null) {
                    packageManager.getPackageInfo("com.google.android.gms", 128);
                    this.zzfa = Boolean.valueOf(true);
                }
            } catch (NameNotFoundException e) {
            }
        }
        return this.zzfa.booleanValue();
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
