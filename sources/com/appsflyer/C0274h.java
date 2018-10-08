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
final class C0274h implements SensorEventListener {
    /* renamed from: ʻ */
    private long f250;
    /* renamed from: ʽ */
    private double f251;
    /* renamed from: ˊ */
    private final int f252;
    @NonNull
    /* renamed from: ˋ */
    private final String f253;
    @NonNull
    /* renamed from: ˎ */
    private final String f254;
    /* renamed from: ˏ */
    private final long[] f255 = new long[2];
    /* renamed from: ॱ */
    private final float[][] f256 = new float[2][];
    /* renamed from: ᐝ */
    private final int f257;

    private C0274h(int i, @Nullable String str, @Nullable String str2) {
        this.f252 = i;
        if (str == null) {
            str = "";
        }
        this.f253 = str;
        if (str2 == null) {
            str2 = "";
        }
        this.f254 = str2;
        int i2 = (i + 31) * 31;
        this.f257 = ((this.f253.hashCode() + i2) * 31) + this.f254.hashCode();
    }

    /* renamed from: ˋ */
    static C0274h m307(Sensor sensor) {
        return new C0274h(sensor.getType(), sensor.getName(), sensor.getVendor());
    }

    /* renamed from: ˋ */
    private static double m306(@NonNull float[] fArr, @NonNull float[] fArr2) {
        double d = 0.0d;
        for (int i = 0; i < Math.min(fArr.length, fArr2.length); i++) {
            d += StrictMath.pow((double) (fArr[i] - fArr2[i]), 2.0d);
        }
        return Math.sqrt(d);
    }

    @NonNull
    /* renamed from: ˏ */
    private static List<Float> m309(@NonNull float[] fArr) {
        List<Float> arrayList = new ArrayList(fArr.length);
        for (float valueOf : fArr) {
            arrayList.add(Float.valueOf(valueOf));
        }
        return arrayList;
    }

    public final void onSensorChanged(SensorEvent sensorEvent) {
        if (sensorEvent != null && sensorEvent.values != null) {
            int i;
            Sensor sensor = sensorEvent.sensor;
            if (sensor == null || sensor.getName() == null || sensor.getVendor() == null) {
                i = 0;
            } else {
                i = 1;
            }
            if (i != 0) {
                i = sensorEvent.sensor.getType();
                String name = sensorEvent.sensor.getName();
                String vendor = sensorEvent.sensor.getVendor();
                long j = sensorEvent.timestamp;
                float[] fArr = sensorEvent.values;
                if (m305(i, name, vendor)) {
                    long currentTimeMillis = System.currentTimeMillis();
                    float[] fArr2 = this.f256[0];
                    if (fArr2 == null) {
                        this.f256[0] = Arrays.copyOf(fArr, fArr.length);
                        this.f255[0] = currentTimeMillis;
                        return;
                    }
                    float[] fArr3 = this.f256[1];
                    if (fArr3 == null) {
                        fArr3 = Arrays.copyOf(fArr, fArr.length);
                        this.f256[1] = fArr3;
                        this.f255[1] = currentTimeMillis;
                        this.f251 = C0274h.m306(fArr2, fArr3);
                    } else if (50000000 <= j - this.f250) {
                        this.f250 = j;
                        if (Arrays.equals(fArr3, fArr)) {
                            this.f255[1] = currentTimeMillis;
                            return;
                        }
                        double ˋ = C0274h.m306(fArr2, fArr);
                        if (ˋ > this.f251) {
                            this.f256[1] = Arrays.copyOf(fArr, fArr.length);
                            this.f255[1] = currentTimeMillis;
                            this.f251 = ˋ;
                        }
                    }
                }
            }
        }
    }

    public final void onAccuracyChanged(Sensor sensor, int i) {
    }

    /* renamed from: ˎ */
    final void m312(@NonNull Map<C0274h, Map<String, Object>> map) {
        m304(map, true);
    }

    /* renamed from: ˋ */
    public final void m311(Map<C0274h, Map<String, Object>> map) {
        m304(map, false);
    }

    /* renamed from: ˊ */
    private boolean m305(int i, @NonNull String str, @NonNull String str2) {
        return this.f252 == i && this.f253.equals(str) && this.f254.equals(str2);
    }

    @NonNull
    /* renamed from: ˎ */
    private Map<String, Object> m308() {
        Map<String, Object> hashMap = new HashMap(7);
        hashMap.put("sT", Integer.valueOf(this.f252));
        hashMap.put("sN", this.f253);
        hashMap.put("sV", this.f254);
        float[] fArr = this.f256[0];
        if (fArr != null) {
            hashMap.put("sVS", C0274h.m309(fArr));
        }
        fArr = this.f256[1];
        if (fArr != null) {
            hashMap.put("sVE", C0274h.m309(fArr));
        }
        return hashMap;
    }

    /* renamed from: ॱ */
    private void m310() {
        int i = 0;
        for (int i2 = 0; i2 < 2; i2++) {
            this.f256[i2] = null;
        }
        while (i < 2) {
            this.f255[i] = 0;
            i++;
        }
        this.f251 = 0.0d;
        this.f250 = 0;
    }

    public final int hashCode() {
        return this.f257;
    }

    public final boolean equals(Object obj) {
        if (obj == this) {
            return true;
        }
        if (!(obj instanceof C0274h)) {
            return false;
        }
        C0274h c0274h = (C0274h) obj;
        return m305(c0274h.f252, c0274h.f253, c0274h.f254);
    }

    /* renamed from: ˊ */
    private void m304(@NonNull Map<C0274h, Map<String, Object>> map, boolean z) {
        int i = 0;
        if (this.f256[0] != null) {
            i = 1;
        }
        if (i != 0) {
            map.put(this, m308());
            if (z) {
                m310();
            }
        } else if (!map.containsKey(this)) {
            map.put(this, m308());
        }
    }
}
