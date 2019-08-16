package com.appsflyer;

import android.content.Context;
import android.hardware.Sensor;
import android.hardware.SensorEventListener;
import android.hardware.SensorManager;
import android.os.Handler;
import android.os.Looper;
import android.support.annotation.NonNull;
import java.util.ArrayList;
import java.util.BitSet;
import java.util.Collections;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/* renamed from: com.appsflyer.f */
final class C0434f {

    /* renamed from: ʻ */
    private static final BitSet f249 = new BitSet(6);

    /* renamed from: ʽ */
    private static volatile C0434f f250;

    /* renamed from: ॱॱ */
    private static final Handler f251 = new Handler(Looper.getMainLooper());

    /* renamed from: ʼ */
    final Runnable f252 = new Runnable() {
        public final void run() {
            synchronized (C0434f.this.f256) {
                if (C0434f.this.f257) {
                    C0434f.this.f255.removeCallbacks(C0434f.this.f253);
                    C0434f.this.f255.removeCallbacks(C0434f.this.f259);
                    C0434f.this.mo6564();
                    C0434f.this.f257 = false;
                }
            }
        }
    };

    /* renamed from: ˊ */
    final Runnable f253 = new Runnable() {
        public final void run() {
            synchronized (C0434f.this.f256) {
                C0434f.this.mo6562();
                C0434f.this.f255.postDelayed(C0434f.this.f259, 500);
                C0434f.this.f257 = true;
            }
        }
    };

    /* renamed from: ˊॱ */
    private boolean f254;

    /* renamed from: ˋ */
    final Handler f255;

    /* renamed from: ˎ */
    final Object f256 = new Object();

    /* renamed from: ˏ */
    boolean f257;

    /* renamed from: ˏॱ */
    private final SensorManager f258;

    /* renamed from: ॱ */
    final Runnable f259 = new C04375();

    /* renamed from: ॱˊ */
    private final Map<C0441h, Map<String, Object>> f260 = new HashMap(f249.size());

    /* renamed from: ᐝ */
    private final Map<C0441h, C0441h> f261 = new HashMap(f249.size());

    /* renamed from: com.appsflyer.f$5 */
    class C04375 implements Runnable {

        /* renamed from: ˋ */
        private static String f264;

        /* renamed from: ˎ */
        private static String f265;

        C04375() {
        }

        public final void run() {
            synchronized (C0434f.this.f256) {
                C0434f.this.mo6564();
                C0434f.this.f255.postDelayed(C0434f.this.f253, 1800000);
            }
        }

        C04375() {
        }

        /* renamed from: ˊ */
        static void m288(String str) {
            f264 = str;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.length(); i++) {
                if (i == 0 || i == str.length() - 1) {
                    sb.append(str.charAt(i));
                } else {
                    sb.append("*");
                }
            }
            f265 = sb.toString();
        }

        /* renamed from: ॱ */
        static void m289(String str) {
            if (f264 == null) {
                m288(AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY));
            }
            if (f264 != null && str.contains(f264)) {
                AFLogger.afInfoLog(str.replace(f264, f265));
            }
        }
    }

    static {
        f249.set(1);
        f249.set(2);
        f249.set(4);
    }

    private C0434f(@NonNull SensorManager sensorManager, Handler handler) {
        this.f258 = sensorManager;
        this.f255 = handler;
    }

    /* renamed from: ˏ */
    static C0434f m284(Context context) {
        return m283((SensorManager) context.getApplicationContext().getSystemService("sensor"), f251);
    }

    /* renamed from: ˊ */
    private static C0434f m283(SensorManager sensorManager, Handler handler) {
        if (f250 == null) {
            synchronized (C0434f.class) {
                if (f250 == null) {
                    f250 = new C0434f(sensorManager, handler);
                }
            }
        }
        return f250;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˊ */
    public final void mo6562() {
        boolean z;
        try {
            for (Sensor sensor : this.f258.getSensorList(-1)) {
                int type = sensor.getType();
                if (type < 0 || !f249.get(type)) {
                    z = false;
                } else {
                    z = true;
                }
                if (z) {
                    C0441h r1 = C0441h.m298(sensor);
                    if (!this.f261.containsKey(r1)) {
                        this.f261.put(r1, r1);
                    }
                    this.f258.registerListener((SensorEventListener) this.f261.get(r1), sensor, 0);
                }
            }
        } catch (Throwable th) {
        }
        this.f254 = true;
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ॱ */
    public final void mo6564() {
        try {
            if (!this.f261.isEmpty()) {
                for (C0441h hVar : this.f261.values()) {
                    this.f258.unregisterListener(hVar);
                    hVar.mo6576(this.f260);
                }
            }
        } catch (Throwable th) {
        }
        this.f254 = false;
    }

    /* access modifiers changed from: 0000 */
    @NonNull
    /* renamed from: ˋ */
    public final List<Map<String, Object>> mo6563() {
        List<Map<String, Object>> arrayList;
        synchronized (this.f256) {
            if (!this.f261.isEmpty() && this.f254) {
                for (C0441h r0 : this.f261.values()) {
                    r0.mo6575(this.f260);
                }
            }
            if (this.f260.isEmpty()) {
                arrayList = Collections.emptyList();
            } else {
                arrayList = new ArrayList<>(this.f260.values());
            }
        }
        return arrayList;
    }
}
