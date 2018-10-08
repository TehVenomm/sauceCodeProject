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
final class C0270f {
    /* renamed from: ʻ */
    private static final BitSet f233 = new BitSet(6);
    /* renamed from: ʽ */
    private static volatile C0270f f234;
    /* renamed from: ॱॱ */
    private static final Handler f235 = new Handler(Looper.getMainLooper());
    /* renamed from: ʼ */
    final Runnable f236 = new C02684(this);
    /* renamed from: ˊ */
    final Runnable f237 = new C02672(this);
    /* renamed from: ˊॱ */
    private boolean f238;
    /* renamed from: ˋ */
    final Handler f239;
    /* renamed from: ˎ */
    final Object f240 = new Object();
    /* renamed from: ˏ */
    boolean f241;
    /* renamed from: ˏॱ */
    private final SensorManager f242;
    /* renamed from: ॱ */
    final Runnable f243 = new C02695(this);
    /* renamed from: ॱˊ */
    private final Map<C0274h, Map<String, Object>> f244 = new HashMap(f233.size());
    /* renamed from: ᐝ */
    private final Map<C0274h, C0274h> f245 = new HashMap(f233.size());

    /* renamed from: com.appsflyer.f$2 */
    class C02672 implements Runnable {
        /* renamed from: ˎ */
        private /* synthetic */ C0270f f228;

        C02672(C0270f c0270f) {
            this.f228 = c0270f;
        }

        public final void run() {
            synchronized (this.f228.f240) {
                this.f228.m296();
                this.f228.f239.postDelayed(this.f228.f243, 500);
                this.f228.f241 = true;
            }
        }
    }

    /* renamed from: com.appsflyer.f$4 */
    class C02684 implements Runnable {
        /* renamed from: ˋ */
        private /* synthetic */ C0270f f229;

        C02684(C0270f c0270f) {
            this.f229 = c0270f;
        }

        public final void run() {
            synchronized (this.f229.f240) {
                if (this.f229.f241) {
                    this.f229.f239.removeCallbacks(this.f229.f237);
                    this.f229.f239.removeCallbacks(this.f229.f243);
                    this.f229.m298();
                    this.f229.f241 = false;
                }
            }
        }
    }

    /* renamed from: com.appsflyer.f$5 */
    class C02695 implements Runnable {
        /* renamed from: ˋ */
        private static String f230;
        /* renamed from: ˎ */
        private static String f231;
        /* renamed from: ˏ */
        private /* synthetic */ C0270f f232;

        C02695(C0270f c0270f) {
            this.f232 = c0270f;
        }

        public final void run() {
            synchronized (this.f232.f240) {
                this.f232.m298();
                this.f232.f239.postDelayed(this.f232.f237, 1800000);
            }
        }

        C02695() {
        }

        /* renamed from: ˊ */
        static void m292(String str) {
            f230 = str;
            StringBuilder stringBuilder = new StringBuilder();
            int i = 0;
            while (i < str.length()) {
                if (i == 0 || i == str.length() - 1) {
                    stringBuilder.append(str.charAt(i));
                } else {
                    stringBuilder.append("*");
                }
                i++;
            }
            f231 = stringBuilder.toString();
        }

        /* renamed from: ॱ */
        static void m293(String str) {
            if (f230 == null) {
                C02695.m292(AppsFlyerProperties.getInstance().getString(AppsFlyerProperties.AF_KEY));
            }
            if (f230 != null && str.contains(f230)) {
                AFLogger.afInfoLog(str.replace(f230, f231));
            }
        }
    }

    static {
        f233.set(1);
        f233.set(2);
        f233.set(4);
    }

    private C0270f(@NonNull SensorManager sensorManager, Handler handler) {
        this.f242 = sensorManager;
        this.f239 = handler;
    }

    /* renamed from: ˏ */
    static C0270f m295(Context context) {
        return C0270f.m294((SensorManager) context.getApplicationContext().getSystemService("sensor"), f235);
    }

    /* renamed from: ˊ */
    private static C0270f m294(SensorManager sensorManager, Handler handler) {
        if (f234 == null) {
            synchronized (C0270f.class) {
                if (f234 == null) {
                    f234 = new C0270f(sensorManager, handler);
                }
            }
        }
        return f234;
    }

    /* renamed from: ˊ */
    final void m296() {
        try {
            for (Sensor sensor : this.f242.getSensorList(-1)) {
                boolean z;
                int type = sensor.getType();
                if (type < 0 || !f233.get(type)) {
                    z = false;
                } else {
                    z = true;
                }
                if (z) {
                    C0274h ˋ = C0274h.m307(sensor);
                    if (!this.f245.containsKey(ˋ)) {
                        this.f245.put(ˋ, ˋ);
                    }
                    this.f242.registerListener((SensorEventListener) this.f245.get(ˋ), sensor, 0);
                }
            }
        } catch (Throwable th) {
        }
        this.f238 = true;
    }

    /* renamed from: ॱ */
    final void m298() {
        try {
            if (!this.f245.isEmpty()) {
                for (C0274h c0274h : this.f245.values()) {
                    this.f242.unregisterListener(c0274h);
                    c0274h.m312(this.f244);
                }
            }
        } catch (Throwable th) {
        }
        this.f238 = false;
    }

    @NonNull
    /* renamed from: ˋ */
    final List<Map<String, Object>> m297() {
        List<Map<String, Object>> emptyList;
        synchronized (this.f240) {
            if (!this.f245.isEmpty() && this.f238) {
                for (C0274h ˋ : this.f245.values()) {
                    ˋ.m311(this.f244);
                }
            }
            if (this.f244.isEmpty()) {
                emptyList = Collections.emptyList();
            } else {
                emptyList = new ArrayList(this.f244.values());
            }
        }
        return emptyList;
    }
}
