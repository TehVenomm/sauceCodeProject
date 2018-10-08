package com.google.firebase.messaging;

import android.content.Context;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.text.TextUtils;
import android.util.Log;
import com.google.android.gms.internal.zzegn;
import com.google.android.gms.internal.zzehl;
import com.google.android.gms.internal.zzehm;
import com.google.android.gms.measurement.AppMeasurement;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.util.ArrayList;
import java.util.List;

public final class zzc {
    private static Bundle zza(@NonNull zzehm zzehm) {
        return zzba(zzehm.zzngv, zzehm.zzngw);
    }

    @Nullable
    private static Object zza(@NonNull zzehm zzehm, @NonNull String str, @NonNull zzb zzb) {
        Object newInstance;
        Throwable e;
        Object obj = null;
        try {
            Class cls = Class.forName("com.google.android.gms.measurement.AppMeasurement$ConditionalUserProperty");
            Bundle zza = zza(zzehm);
            newInstance = cls.getConstructor(new Class[0]).newInstance(new Object[0]);
            try {
                cls.getField("mOrigin").set(newInstance, str);
                cls.getField("mCreationTimestamp").set(newInstance, Long.valueOf(zzehm.zzngx));
                cls.getField("mName").set(newInstance, zzehm.zzngv);
                cls.getField("mValue").set(newInstance, zzehm.zzngw);
                if (!TextUtils.isEmpty(zzehm.zzngy)) {
                    obj = zzehm.zzngy;
                }
                cls.getField("mTriggerEventName").set(newInstance, obj);
                cls.getField("mTimedOutEventName").set(newInstance, !TextUtils.isEmpty(zzehm.zznhd) ? zzehm.zznhd : zzb.zzbnq());
                cls.getField("mTimedOutEventParams").set(newInstance, zza);
                cls.getField("mTriggerTimeout").set(newInstance, Long.valueOf(zzehm.zzngz));
                cls.getField("mTriggeredEventName").set(newInstance, !TextUtils.isEmpty(zzehm.zznhb) ? zzehm.zznhb : zzb.zzbnp());
                cls.getField("mTriggeredEventParams").set(newInstance, zza);
                cls.getField("mTimeToLive").set(newInstance, Long.valueOf(zzehm.zzgbv));
                cls.getField("mExpiredEventName").set(newInstance, !TextUtils.isEmpty(zzehm.zznhe) ? zzehm.zznhe : zzb.zzbnr());
                cls.getField("mExpiredEventParams").set(newInstance, zza);
            } catch (ClassNotFoundException e2) {
                e = e2;
                Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
                return newInstance;
            } catch (NoSuchMethodException e3) {
                e = e3;
                Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
                return newInstance;
            } catch (IllegalAccessException e4) {
                e = e4;
                Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
                return newInstance;
            } catch (InvocationTargetException e5) {
                e = e5;
                Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
                return newInstance;
            } catch (NoSuchFieldException e6) {
                e = e6;
                Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
                return newInstance;
            } catch (InstantiationException e7) {
                e = e7;
                Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
                return newInstance;
            }
        } catch (Throwable e8) {
            e = e8;
            newInstance = null;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            return newInstance;
        } catch (Throwable e82) {
            e = e82;
            newInstance = null;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            return newInstance;
        } catch (Throwable e822) {
            e = e822;
            newInstance = null;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            return newInstance;
        } catch (Throwable e8222) {
            e = e8222;
            newInstance = null;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            return newInstance;
        } catch (Throwable e82222) {
            e = e82222;
            newInstance = null;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            return newInstance;
        } catch (Throwable e822222) {
            e = e822222;
            newInstance = null;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            return newInstance;
        }
        return newInstance;
    }

    private static String zza(@Nullable zzehm zzehm, @NonNull zzb zzb) {
        return (zzehm == null || TextUtils.isEmpty(zzehm.zznhc)) ? zzb.zzbns() : zzehm.zznhc;
    }

    private static List<Object> zza(@NonNull AppMeasurement appMeasurement, @NonNull String str) {
        List<Object> list;
        Throwable e;
        Object obj;
        ArrayList arrayList = new ArrayList();
        try {
            Method declaredMethod = AppMeasurement.class.getDeclaredMethod("getConditionalUserProperties", new Class[]{String.class, String.class});
            declaredMethod.setAccessible(true);
            list = (List) declaredMethod.invoke(appMeasurement, new Object[]{str, ""});
        } catch (NoSuchMethodException e2) {
            e = e2;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            obj = arrayList;
            if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                Log.v("FirebaseAbtUtil", new StringBuilder(String.valueOf(str).length() + 55).append("Number of currently set _Es for origin: ").append(str).append(" is ").append(list.size()).toString());
            }
            return list;
        } catch (IllegalAccessException e3) {
            e = e3;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            obj = arrayList;
            if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                Log.v("FirebaseAbtUtil", new StringBuilder(String.valueOf(str).length() + 55).append("Number of currently set _Es for origin: ").append(str).append(" is ").append(list.size()).toString());
            }
            return list;
        } catch (InvocationTargetException e4) {
            e = e4;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            obj = arrayList;
            if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                Log.v("FirebaseAbtUtil", new StringBuilder(String.valueOf(str).length() + 55).append("Number of currently set _Es for origin: ").append(str).append(" is ").append(list.size()).toString());
            }
            return list;
        }
        if (Log.isLoggable("FirebaseAbtUtil", 2)) {
            Log.v("FirebaseAbtUtil", new StringBuilder(String.valueOf(str).length() + 55).append("Number of currently set _Es for origin: ").append(str).append(" is ").append(list.size()).toString());
        }
        return list;
    }

    private static void zza(@NonNull Context context, @NonNull String str, @NonNull String str2, @NonNull String str3, @NonNull String str4) {
        Throwable e;
        if (Log.isLoggable("FirebaseAbtUtil", 2)) {
            String valueOf = String.valueOf(str);
            Log.v("FirebaseAbtUtil", valueOf.length() != 0 ? "_CE(experimentId) called by ".concat(valueOf) : new String("_CE(experimentId) called by "));
        }
        if (zzeg(context)) {
            AppMeasurement zzct = zzct(context);
            try {
                Method declaredMethod = AppMeasurement.class.getDeclaredMethod("clearConditionalUserProperty", new Class[]{String.class, String.class, Bundle.class});
                declaredMethod.setAccessible(true);
                if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                    Log.v("FirebaseAbtUtil", new StringBuilder((String.valueOf(str2).length() + 17) + String.valueOf(str3).length()).append("Clearing _E: [").append(str2).append(", ").append(str3).append("]").toString());
                }
                declaredMethod.invoke(zzct, new Object[]{str2, str4, zzba(str2, str3)});
            } catch (NoSuchMethodException e2) {
                e = e2;
                Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            } catch (IllegalAccessException e3) {
                e = e3;
                Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            } catch (InvocationTargetException e4) {
                e = e4;
                Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            }
        }
    }

    public static void zza(@NonNull Context context, @NonNull String str, @NonNull byte[] bArr, @NonNull zzb zzb, int i) {
        Throwable e;
        if (Log.isLoggable("FirebaseAbtUtil", 2)) {
            String valueOf = String.valueOf(str);
            Log.v("FirebaseAbtUtil", valueOf.length() != 0 ? "_SE called by ".concat(valueOf) : new String("_SE called by "));
        }
        if (zzeg(context)) {
            AppMeasurement zzct = zzct(context);
            zzehm zzaj = zzaj(bArr);
            if (zzaj != null) {
                try {
                    Class.forName("com.google.android.gms.measurement.AppMeasurement$ConditionalUserProperty");
                    Object obj = null;
                    for (Object next : zza(zzct, str)) {
                        Object next2;
                        String zzas = zzas(next2);
                        String zzat = zzat(next2);
                        long longValue = ((Long) Class.forName("com.google.android.gms.measurement.AppMeasurement$ConditionalUserProperty").getField("mCreationTimestamp").get(next2)).longValue();
                        if (zzaj.zzngv.equals(zzas) && zzaj.zzngw.equals(zzat)) {
                            if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                                Log.v("FirebaseAbtUtil", new StringBuilder((String.valueOf(zzas).length() + 23) + String.valueOf(zzat).length()).append("_E is already set. [").append(zzas).append(", ").append(zzat).append("]").toString());
                            }
                            obj = 1;
                        } else {
                            next2 = null;
                            zzehl[] zzehlArr = zzaj.zznhg;
                            int length = zzehlArr.length;
                            int i2 = 0;
                            while (i2 < length) {
                                if (zzehlArr[i2].zzngv.equals(zzas)) {
                                    if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                                        Log.v("FirebaseAbtUtil", new StringBuilder((String.valueOf(zzas).length() + 33) + String.valueOf(zzat).length()).append("_E is found in the _OE list. [").append(zzas).append(", ").append(zzat).append("]").toString());
                                    }
                                    next2 = 1;
                                    if (next2 != null) {
                                        continue;
                                    } else if (zzaj.zzngx > longValue) {
                                        if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                                            Log.v("FirebaseAbtUtil", new StringBuilder((String.valueOf(zzas).length() + 115) + String.valueOf(zzat).length()).append("Clearing _E as it was not in the _OE list, andits start time is older than the start time of the _E to be set. [").append(zzas).append(", ").append(zzat).append("]").toString());
                                        }
                                        zza(context, str, zzas, zzat, zza(zzaj, zzb));
                                    } else if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                                        Log.v("FirebaseAbtUtil", new StringBuilder((String.valueOf(zzas).length() + 109) + String.valueOf(zzat).length()).append("_E was not found in the _OE list, but not clearing it as it has a new start time than the _E to be set.  [").append(zzas).append(", ").append(zzat).append("]").toString());
                                    }
                                } else {
                                    i2++;
                                }
                            }
                            if (next2 != null) {
                                continue;
                            } else if (zzaj.zzngx > longValue) {
                                if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                                    Log.v("FirebaseAbtUtil", new StringBuilder((String.valueOf(zzas).length() + 115) + String.valueOf(zzat).length()).append("Clearing _E as it was not in the _OE list, andits start time is older than the start time of the _E to be set. [").append(zzas).append(", ").append(zzat).append("]").toString());
                                }
                                zza(context, str, zzas, zzat, zza(zzaj, zzb));
                            } else if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                                Log.v("FirebaseAbtUtil", new StringBuilder((String.valueOf(zzas).length() + 109) + String.valueOf(zzat).length()).append("_E was not found in the _OE list, but not clearing it as it has a new start time than the _E to be set.  [").append(zzas).append(", ").append(zzat).append("]").toString());
                            }
                        }
                    }
                    if (obj == null) {
                        zza(zzct, context, str, zzaj, zzb, 1);
                        return;
                    } else if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                        valueOf = zzaj.zzngv;
                        String str2 = zzaj.zzngw;
                        Log.v("FirebaseAbtUtil", new StringBuilder((String.valueOf(valueOf).length() + 44) + String.valueOf(str2).length()).append("_E is already set. Not setting it again [").append(valueOf).append(", ").append(str2).append("]").toString());
                        return;
                    } else {
                        return;
                    }
                } catch (ClassNotFoundException e2) {
                    e = e2;
                } catch (IllegalAccessException e3) {
                    e = e3;
                } catch (NoSuchFieldException e4) {
                    e = e4;
                }
            } else if (Log.isLoggable("FirebaseAbtUtil", 2)) {
                Log.v("FirebaseAbtUtil", "_SE failed; either _P was not set, or we couldn't deserialize the _P.");
                return;
            } else {
                return;
            }
        }
        return;
        Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private static void zza(@android.support.annotation.NonNull com.google.android.gms.measurement.AppMeasurement r8, @android.support.annotation.NonNull android.content.Context r9, @android.support.annotation.NonNull java.lang.String r10, @android.support.annotation.NonNull com.google.android.gms.internal.zzehm r11, @android.support.annotation.NonNull com.google.firebase.messaging.zzb r12, int r13) {
        /*
        r1 = 1;
        r2 = 2;
        r0 = "FirebaseAbtUtil";
        r0 = android.util.Log.isLoggable(r0, r2);
        if (r0 == 0) goto L_0x0043;
    L_0x000a:
        r0 = r11.zzngv;
        r2 = r11.zzngw;
        r3 = "FirebaseAbtUtil";
        r4 = new java.lang.StringBuilder;
        r5 = java.lang.String.valueOf(r0);
        r5 = r5.length();
        r5 = r5 + 7;
        r6 = java.lang.String.valueOf(r2);
        r6 = r6.length();
        r5 = r5 + r6;
        r4.<init>(r5);
        r5 = "_SEI: ";
        r4 = r4.append(r5);
        r0 = r4.append(r0);
        r4 = " ";
        r0 = r0.append(r4);
        r0 = r0.append(r2);
        r0 = r0.toString();
        android.util.Log.v(r3, r0);
    L_0x0043:
        r0 = "com.google.android.gms.measurement.AppMeasurement$ConditionalUserProperty";
        java.lang.Class.forName(r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = zza(r8, r10);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = zzb(r8, r10);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = zza(r8, r10);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r3.size();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r3 < r0) goto L_0x00a7;
    L_0x005a:
        r0 = r11.zznhf;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r0 == 0) goto L_0x0122;
    L_0x005e:
        r0 = r11.zznhf;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
    L_0x0060:
        if (r0 != r1) goto L_0x0125;
    L_0x0062:
        r0 = 0;
        r0 = r2.get(r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r1 = zzas(r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = zzat(r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = "FirebaseAbtUtil";
        r4 = 2;
        r3 = android.util.Log.isLoggable(r3, r4);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r3 == 0) goto L_0x00a0;
    L_0x0078:
        r3 = java.lang.String.valueOf(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r3.length();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = new java.lang.StringBuilder;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r3 + 38;
        r4.<init>(r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = "FirebaseAbtUtil";
        r5 = "Clearing _E due to overflow policy: [";
        r4 = r4.append(r5);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = r4.append(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r5 = "]";
        r4 = r4.append(r5);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = r4.toString();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        android.util.Log.v(r3, r4);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
    L_0x00a0:
        r3 = zza(r11, r12);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        zza(r9, r10, r1, r0, r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
    L_0x00a7:
        r0 = r2.iterator();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
    L_0x00ab:
        r1 = r0.hasNext();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r1 == 0) goto L_0x0170;
    L_0x00b1:
        r1 = r0.next();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = zzas(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r1 = zzat(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r11.zzngv;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r2.equals(r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r3 == 0) goto L_0x00ab;
    L_0x00c5:
        r3 = r11.zzngw;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r1.equals(r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r3 != 0) goto L_0x00ab;
    L_0x00cd:
        r3 = "FirebaseAbtUtil";
        r4 = 2;
        r3 = android.util.Log.isLoggable(r3, r4);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r3 == 0) goto L_0x00ab;
    L_0x00d6:
        r3 = java.lang.String.valueOf(r2);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r3.length();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = java.lang.String.valueOf(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = r4.length();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r5 = new java.lang.StringBuilder;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r3 + 77;
        r3 = r3 + r4;
        r5.<init>(r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = "FirebaseAbtUtil";
        r4 = "Clearing _E, as only one _V of the same _E can be set atany given time: [";
        r4 = r5.append(r4);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = r4.append(r2);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r5 = ", ";
        r4 = r4.append(r5);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = r4.append(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r5 = "].";
        r4 = r4.append(r5);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = r4.toString();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        android.util.Log.v(r3, r4);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = zza(r11, r12);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        zza(r9, r10, r2, r1, r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        goto L_0x00ab;
    L_0x0119:
        r0 = move-exception;
    L_0x011a:
        r1 = "FirebaseAbtUtil";
        r2 = "Could not complete the operation due to an internal error.";
        android.util.Log.e(r1, r2, r0);
    L_0x0121:
        return;
    L_0x0122:
        r0 = r1;
        goto L_0x0060;
    L_0x0125:
        r0 = "FirebaseAbtUtil";
        r1 = 2;
        r0 = android.util.Log.isLoggable(r0, r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r0 == 0) goto L_0x0121;
    L_0x012e:
        r0 = r11.zzngv;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r1 = r11.zzngw;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = java.lang.String.valueOf(r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = r2.length();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = java.lang.String.valueOf(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r3.length();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = new java.lang.StringBuilder;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = r2 + 44;
        r2 = r2 + r3;
        r4.<init>(r2);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = "FirebaseAbtUtil";
        r3 = "_E won't be set due to overflow policy. [";
        r3 = r4.append(r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = r3.append(r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = ", ";
        r0 = r0.append(r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = r0.append(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r1 = "]";
        r0 = r0.append(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = r0.toString();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        android.util.Log.v(r2, r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        goto L_0x0121;
    L_0x016e:
        r0 = move-exception;
        goto L_0x011a;
    L_0x0170:
        r1 = zza(r11, r10, r12);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r1 != 0) goto L_0x01c3;
    L_0x0176:
        r0 = "FirebaseAbtUtil";
        r1 = 2;
        r0 = android.util.Log.isLoggable(r0, r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r0 == 0) goto L_0x0121;
    L_0x017f:
        r0 = r11.zzngv;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r1 = r11.zzngw;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = java.lang.String.valueOf(r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = r2.length();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = java.lang.String.valueOf(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r3.length();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = new java.lang.StringBuilder;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = r2 + 42;
        r2 = r2 + r3;
        r4.<init>(r2);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = "FirebaseAbtUtil";
        r3 = "Could not create _CUP for: [";
        r3 = r4.append(r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = r3.append(r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = ", ";
        r0 = r0.append(r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = r0.append(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r1 = "]. Skipping.";
        r0 = r0.append(r1);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = r0.toString();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        android.util.Log.v(r2, r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        goto L_0x0121;
    L_0x01c0:
        r0 = move-exception;
        goto L_0x011a;
    L_0x01c3:
        r0 = "FirebaseAbtUtil";
        r2 = 2;
        r0 = android.util.Log.isLoggable(r0, r2);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        if (r0 == 0) goto L_0x0220;
    L_0x01cc:
        r0 = r11.zzngv;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = r11.zzngw;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r3 = r11.zzngy;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = java.lang.String.valueOf(r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = r4.length();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r5 = java.lang.String.valueOf(r2);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r5 = r5.length();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r6 = java.lang.String.valueOf(r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r6 = r6.length();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r7 = new java.lang.StringBuilder;	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = r4 + 27;
        r4 = r4 + r5;
        r4 = r4 + r6;
        r7.<init>(r4);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r4 = "FirebaseAbtUtil";
        r5 = "Setting _CUP for _E: [";
        r5 = r7.append(r5);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = r5.append(r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r5 = ", ";
        r0 = r0.append(r5);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = r0.append(r2);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = ", ";
        r0 = r0.append(r2);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = r0.append(r3);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r2 = "]";
        r0 = r0.append(r2);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        r0 = r0.toString();	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        android.util.Log.v(r4, r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
    L_0x0220:
        r0 = com.google.android.gms.measurement.AppMeasurement.class;
        r2 = "setConditionalUserProperty";
        r3 = 1;
        r3 = new java.lang.Class[r3];	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        r4 = 0;
        r5 = "com.google.android.gms.measurement.AppMeasurement$ConditionalUserProperty";
        r5 = java.lang.Class.forName(r5);	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        r3[r4] = r5;	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        r2 = r0.getDeclaredMethod(r2, r3);	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        r0 = 1;
        r2.setAccessible(r0);	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        r0 = r11.zznha;	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        r0 = android.text.TextUtils.isEmpty(r0);	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        if (r0 != 0) goto L_0x025e;
    L_0x0240:
        r0 = r11.zznha;	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
    L_0x0242:
        r3 = zza(r11);	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        r8.logEventInternal(r10, r0, r3);	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        r0 = 1;
        r0 = new java.lang.Object[r0];	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        r3 = 0;
        r0[r3] = r1;	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        r2.invoke(r8, r0);	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        goto L_0x0121;
    L_0x0254:
        r0 = move-exception;
    L_0x0255:
        r1 = "FirebaseAbtUtil";
        r2 = "Could not complete the operation due to an internal error.";
        android.util.Log.e(r1, r2, r0);	 Catch:{ ClassNotFoundException -> 0x0119, IllegalAccessException -> 0x016e, NoSuchFieldException -> 0x01c0 }
        goto L_0x0121;
    L_0x025e:
        r0 = r12.zzbno();	 Catch:{ ClassNotFoundException -> 0x0254, NoSuchMethodException -> 0x0265, IllegalAccessException -> 0x0267, InvocationTargetException -> 0x0263, NoSuchFieldException -> 0x01c0 }
        goto L_0x0242;
    L_0x0263:
        r0 = move-exception;
        goto L_0x0255;
    L_0x0265:
        r0 = move-exception;
        goto L_0x0255;
    L_0x0267:
        r0 = move-exception;
        goto L_0x0255;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.messaging.zzc.zza(com.google.android.gms.measurement.AppMeasurement, android.content.Context, java.lang.String, com.google.android.gms.internal.zzehm, com.google.firebase.messaging.zzb, int):void");
    }

    @Nullable
    private static zzehm zzaj(@NonNull byte[] bArr) {
        try {
            return zzehm.zzay(bArr);
        } catch (zzegn e) {
            return null;
        }
    }

    private static String zzas(@NonNull Object obj) throws ClassNotFoundException, NoSuchFieldException, IllegalAccessException {
        return (String) Class.forName("com.google.android.gms.measurement.AppMeasurement$ConditionalUserProperty").getField("mName").get(obj);
    }

    private static String zzat(@NonNull Object obj) throws ClassNotFoundException, NoSuchFieldException, IllegalAccessException {
        return (String) Class.forName("com.google.android.gms.measurement.AppMeasurement$ConditionalUserProperty").getField("mValue").get(obj);
    }

    private static int zzb(@NonNull AppMeasurement appMeasurement, @NonNull String str) {
        Throwable e;
        try {
            Method declaredMethod = AppMeasurement.class.getDeclaredMethod("getMaxUserProperties", new Class[]{String.class});
            declaredMethod.setAccessible(true);
            return ((Integer) declaredMethod.invoke(appMeasurement, new Object[]{str})).intValue();
        } catch (NoSuchMethodException e2) {
            e = e2;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            return 20;
        } catch (IllegalAccessException e3) {
            e = e3;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            return 20;
        } catch (InvocationTargetException e4) {
            e = e4;
            Log.e("FirebaseAbtUtil", "Could not complete the operation due to an internal error.", e);
            return 20;
        }
    }

    private static Bundle zzba(@NonNull String str, @NonNull String str2) {
        Bundle bundle = new Bundle();
        bundle.putString(str, str2);
        return bundle;
    }

    @Nullable
    private static AppMeasurement zzct(Context context) {
        try {
            return AppMeasurement.getInstance(context);
        } catch (NoClassDefFoundError e) {
            return null;
        }
    }

    private static boolean zzeg(Context context) {
        if (zzct(context) != null) {
            try {
                Class.forName("com.google.android.gms.measurement.AppMeasurement$ConditionalUserProperty");
                return true;
            } catch (ClassNotFoundException e) {
                if (!Log.isLoggable("FirebaseAbtUtil", 2)) {
                    return false;
                }
                Log.v("FirebaseAbtUtil", "Firebase Analytics library is missing support for abt. Please update to a more recent version.");
                return false;
            }
        } else if (!Log.isLoggable("FirebaseAbtUtil", 2)) {
            return false;
        } else {
            Log.v("FirebaseAbtUtil", "Firebase Analytics not available");
            return false;
        }
    }
}
