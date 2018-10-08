package com.google.android.gms.internal;

import android.os.Handler;
import android.os.Looper;
import java.util.HashMap;
import java.util.Map.Entry;
import java.util.concurrent.atomic.AtomicInteger;

public abstract class zzbvi {
    private Object zzhio = new Object();
    private Handler zzhip;
    private boolean zzhiq;
    private HashMap<String, AtomicInteger> zzhir;
    private int zzhis;

    public zzbvi(Looper looper, int i) {
        this.zzhip = new Handler(looper);
        this.zzhir = new HashMap();
        this.zzhis = 1000;
    }

    private final void zzara() {
        synchronized (this.zzhio) {
            this.zzhiq = false;
            flush();
        }
    }

    public final void flush() {
        synchronized (this.zzhio) {
            for (Entry entry : this.zzhir.entrySet()) {
                zzs((String) entry.getKey(), ((AtomicInteger) entry.getValue()).get());
            }
            this.zzhir.clear();
        }
    }

    protected abstract void zzs(String str, int i);

    public final void zzt(String str, int i) {
        synchronized (this.zzhio) {
            if (!this.zzhiq) {
                this.zzhiq = true;
                this.zzhip.postDelayed(new zzbvj(this), (long) this.zzhis);
            }
            AtomicInteger atomicInteger = (AtomicInteger) this.zzhir.get(str);
            if (atomicInteger == null) {
                atomicInteger = new AtomicInteger();
                this.zzhir.put(str, atomicInteger);
            }
            atomicInteger.addAndGet(i);
        }
    }
}
