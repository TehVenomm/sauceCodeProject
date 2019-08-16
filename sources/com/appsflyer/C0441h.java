package com.appsflyer;

import android.hardware.Sensor;
import android.hardware.SensorEvent;
import android.hardware.SensorEventListener;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import java.util.ArrayList;
import java.util.Arrays;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

/* renamed from: com.appsflyer.h */
final class C0441h implements SensorEventListener {

    /* renamed from: ʻ */
    private long f271;

    /* renamed from: ʽ */
    private double f272;

    /* renamed from: ˊ */
    private final int f273;
    @NonNull

    /* renamed from: ˋ */
    private final String f274;
    @NonNull

    /* renamed from: ˎ */
    private final String f275;

    /* renamed from: ˏ */
    private final long[] f276 = new long[2];

    /* renamed from: ॱ */
    private final float[][] f277 = new float[2][];

    /* renamed from: ᐝ */
    private final int f278;

    private C0441h(int i, @Nullable String str, @Nullable String str2) {
        this.f273 = i;
        if (str == null) {
            str = "";
        }
        this.f274 = str;
        if (str2 == null) {
            str2 = "";
        }
        this.f275 = str2;
        int i2 = (i + 31) * 31;
        this.f278 = ((this.f274.hashCode() + i2) * 31) + this.f275.hashCode();
    }

    /* renamed from: ˋ */
    static C0441h m298(Sensor sensor) {
        return new C0441h(sensor.getType(), sensor.getName(), sensor.getVendor());
    }

    /* renamed from: ˋ */
    private static double m297(@NonNull float[] fArr, @NonNull float[] fArr2) {
        double d = 0.0d;
        for (int i = 0; i < Math.min(fArr.length, fArr2.length); i++) {
            d += StrictMath.pow((double) (fArr[i] - fArr2[i]), 2.0d);
        }
        return Math.sqrt(d);
    }

    @NonNull
    /* renamed from: ˏ */
    private static List<Float> m300(@NonNull float[] fArr) {
        ArrayList arrayList = new ArrayList(fArr.length);
        for (float valueOf : fArr) {
            arrayList.add(Float.valueOf(valueOf));
        }
        return arrayList;
    }

    public final void onSensorChanged(SensorEvent sensorEvent) {
        boolean z;
        if (sensorEvent != null && sensorEvent.values != null) {
            Sensor sensor = sensorEvent.sensor;
            if (sensor == null || sensor.getName() == null || sensor.getVendor() == null) {
                z = false;
            } else {
                z = true;
            }
            if (z) {
                int type = sensorEvent.sensor.getType();
                String name = sensorEvent.sensor.getName();
                String vendor = sensorEvent.sensor.getVendor();
                long j = sensorEvent.timestamp;
                float[] fArr = sensorEvent.values;
                if (m296(type, name, vendor)) {
                    long currentTimeMillis = System.currentTimeMillis();
                    float[] fArr2 = this.f277[0];
                    if (fArr2 == null) {
                        this.f277[0] = Arrays.copyOf(fArr, fArr.length);
                        this.f276[0] = currentTimeMillis;
                        return;
                    }
                    float[] fArr3 = this.f277[1];
                    if (fArr3 == null) {
                        float[] copyOf = Arrays.copyOf(fArr, fArr.length);
                        this.f277[1] = copyOf;
                        this.f276[1] = currentTimeMillis;
                        this.f272 = m297(fArr2, copyOf);
                    } else if (50000000 <= j - this.f271) {
                        this.f271 = j;
                        if (Arrays.equals(fArr3, fArr)) {
                            this.f276[1] = currentTimeMillis;
                            return;
                        }
                        double r2 = m297(fArr2, fArr);
                        if (r2 > this.f272) {
                            this.f277[1] = Arrays.copyOf(fArr, fArr.length);
                            this.f276[1] = currentTimeMillis;
                            this.f272 = r2;
                        }
                    }
                }
            }
        }
    }

    public final void onAccuracyChanged(Sensor sensor, int i) {
    }

    /* access modifiers changed from: 0000 */
    /* renamed from: ˎ */
    public final void mo6576(@NonNull Map<C0441h, Map<String, Object>> map) {
        m295(map, true);
    }

    /* renamed from: ˋ */
    public final void mo6575(Map<C0441h, Map<String, Object>> map) {
        m295(map, false);
    }

    /* renamed from: ˊ */
    private boolean m296(int i, @NonNull String str, @NonNull String str2) {
        return this.f273 == i && this.f274.equals(str) && this.f275.equals(str2);
    }

    @NonNull
    /* renamed from: ˎ */
    private Map<String, Object> m299() {
        HashMap hashMap = new HashMap(7);
        hashMap.put("sT", Integer.valueOf(this.f273));
        hashMap.put("sN", this.f274);
        hashMap.put("sV", this.f275);
        float[] fArr = this.f277[0];
        if (fArr != null) {
            hashMap.put("sVS", m300(fArr));
        }
        float[] fArr2 = this.f277[1];
        if (fArr2 != null) {
            hashMap.put("sVE", m300(fArr2));
        }
        return hashMap;
    }

    /* renamed from: ॱ */
    private void m301() {
        for (int i = 0; i < 2; i++) {
            this.f277[i] = null;
        }
        for (int i2 = 0; i2 < 2; i2++) {
            this.f276[i2] = 0;
        }
        this.f272 = 0.0d;
        this.f271 = 0;
    }

    public final int hashCode() {
        return this.f278;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof C0441h)) {
            return false;
        }
        C0441h hVar = (C0441h) obj;
        return m296(hVar.f273, hVar.f274, hVar.f275);
    }

    /* renamed from: ˊ */
    private void m295(@NonNull Map<C0441h, Map<String, Object>> map, boolean z) {
        boolean z2 = false;
        if (this.f277[0] != null) {
            z2 = true;
        }
        if (z2) {
            map.put(this, m299());
            if (z) {
                m301();
            }
        } else if (!map.containsKey(this)) {
            map.put(this, m299());
        }
    }
}
