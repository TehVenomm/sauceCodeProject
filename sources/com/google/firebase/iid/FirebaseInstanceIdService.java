package com.google.firebase.iid;

import android.app.AlarmManager;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.net.ConnectivityManager;
import android.net.NetworkInfo;
import android.os.Bundle;
import android.os.SystemClock;
import android.support.annotation.Nullable;
import android.support.annotation.VisibleForTesting;
import android.support.annotation.WorkerThread;
import android.util.Log;
import jp.colopl.gcm.RegistrarHelper;

public class FirebaseInstanceIdService extends zzb {
    @VisibleForTesting
    private static Object zzmjc = new Object();
    @VisibleForTesting
    private static boolean zzmjd = false;
    private boolean zzmje = false;

    static class zza extends BroadcastReceiver {
        @Nullable
        private static BroadcastReceiver receiver;
        private int zzmjf;

        private zza(int i) {
            this.zzmjf = i;
        }

        static void zzl(Context context, int i) {
            synchronized (zza.class) {
                try {
                    if (receiver == null) {
                        receiver = new zza(i);
                        context.getApplicationContext().registerReceiver(receiver, new IntentFilter("android.net.conn.CONNECTIVITY_CHANGE"));
                    }
                } catch (Throwable th) {
                    Class cls = zza.class;
                }
            }
        }

        public void onReceive(Context context, Intent intent) {
            synchronized (zza.class) {
                try {
                    zza zza = receiver;
                    if (zza != this) {
                    } else if (FirebaseInstanceIdService.zzem(context)) {
                        if (Log.isLoggable("FirebaseInstanceId", 3)) {
                            Log.d("FirebaseInstanceId", "connectivity changed. starting background sync.");
                        }
                        context.getApplicationContext().unregisterReceiver(this);
                        receiver = null;
                        zzq.zzbyp().zze(context, FirebaseInstanceIdService.zzfw(this.zzmjf));
                    }
                } finally {
                    Class cls = zza.class;
                }
            }
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    static void zza(android.content.Context r2, com.google.firebase.iid.FirebaseInstanceId r3) {
        /*
        r1 = zzmjc;
        monitor-enter(r1);
        r0 = zzmjd;	 Catch:{ all -> 0x0026 }
        if (r0 == 0) goto L_0x0009;
    L_0x0007:
        monitor-exit(r1);	 Catch:{ all -> 0x0026 }
    L_0x0008:
        return;
    L_0x0009:
        monitor-exit(r1);	 Catch:{ all -> 0x0026 }
        r0 = r3.zzbyi();
        if (r0 == 0) goto L_0x0022;
    L_0x0010:
        r1 = com.google.firebase.iid.zzj.zzhtl;
        r0 = r0.zzqa(r1);
        if (r0 != 0) goto L_0x0022;
    L_0x0018:
        r0 = com.google.firebase.iid.FirebaseInstanceId.zzbyk();
        r0 = r0.zzbyn();
        if (r0 == 0) goto L_0x0008;
    L_0x0022:
        zzel(r2);
        goto L_0x0008;
    L_0x0026:
        r0 = move-exception;
        monitor-exit(r1);	 Catch:{ all -> 0x0026 }
        throw r0;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.iid.FirebaseInstanceIdService.zza(android.content.Context, com.google.firebase.iid.FirebaseInstanceId):void");
    }

    private final void zza(Intent intent, String str) {
        boolean zzem = zzem(this);
        int intExtra = intent == null ? 10 : intent.getIntExtra("next_retry_delay_in_seconds", 0);
        if (intExtra < 10 && !zzem) {
            intExtra = 30;
        } else if (intExtra < 10) {
            intExtra = 10;
        } else if (intExtra > 28800) {
            intExtra = 28800;
        }
        Log.d("FirebaseInstanceId", new StringBuilder(String.valueOf(str).length() + 47).append("background sync failed: ").append(str).append(", retry in ").append(intExtra).append("s").toString());
        synchronized (zzmjc) {
            ((AlarmManager) getSystemService("alarm")).set(3, SystemClock.elapsedRealtime() + ((long) (intExtra * 1000)), zzq.zza(this, 0, zzfw(intExtra << 1), 134217728));
            zzmjd = true;
        }
        if (!zzem) {
            if (this.zzmje) {
                Log.d("FirebaseInstanceId", "device not connected. Connectivity change received registered");
            }
            zza.zzl(this, intExtra);
        }
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    private final void zza(android.content.Intent r9, boolean r10, boolean r11) {
        /*
        r8 = this;
        r3 = 1;
        r2 = 0;
        r1 = zzmjc;
        monitor-enter(r1);
        r0 = 0;
        zzmjd = r0;	 Catch:{ all -> 0x0010 }
        monitor-exit(r1);	 Catch:{ all -> 0x0010 }
        r0 = com.google.firebase.iid.zzl.zzdg(r8);
        if (r0 != 0) goto L_0x0013;
    L_0x000f:
        return;
    L_0x0010:
        r0 = move-exception;
        monitor-exit(r1);	 Catch:{ all -> 0x0010 }
        throw r0;
    L_0x0013:
        r0 = com.google.firebase.iid.FirebaseInstanceId.getInstance();
        r1 = r0.zzbyi();
        if (r1 == 0) goto L_0x0025;
    L_0x001d:
        r4 = com.google.firebase.iid.zzj.zzhtl;
        r4 = r1.zzqa(r4);
        if (r4 == 0) goto L_0x0063;
    L_0x0025:
        r2 = r0.zzbyj();	 Catch:{ IOException -> 0x004b, SecurityException -> 0x005a }
        if (r2 == 0) goto L_0x0054;
    L_0x002b:
        r3 = r8.zzmje;	 Catch:{ IOException -> 0x004b, SecurityException -> 0x005a }
        if (r3 == 0) goto L_0x0036;
    L_0x002f:
        r3 = "FirebaseInstanceId";
        r4 = "get master token succeeded";
        android.util.Log.d(r3, r4);	 Catch:{ IOException -> 0x004b, SecurityException -> 0x005a }
    L_0x0036:
        zza(r8, r0);	 Catch:{ IOException -> 0x004b, SecurityException -> 0x005a }
        if (r11 != 0) goto L_0x0047;
    L_0x003b:
        if (r1 == 0) goto L_0x0047;
    L_0x003d:
        if (r1 == 0) goto L_0x000f;
    L_0x003f:
        r0 = r1.zzkmz;	 Catch:{ IOException -> 0x004b, SecurityException -> 0x005a }
        r0 = r2.equals(r0);	 Catch:{ IOException -> 0x004b, SecurityException -> 0x005a }
        if (r0 != 0) goto L_0x000f;
    L_0x0047:
        r8.onTokenRefresh();	 Catch:{ IOException -> 0x004b, SecurityException -> 0x005a }
        goto L_0x000f;
    L_0x004b:
        r0 = move-exception;
        r0 = r0.getMessage();
        r8.zza(r9, r0);
        goto L_0x000f;
    L_0x0054:
        r0 = "returned token is null";
        r8.zza(r9, r0);	 Catch:{ IOException -> 0x004b, SecurityException -> 0x005a }
        goto L_0x000f;
    L_0x005a:
        r0 = move-exception;
        r1 = "FirebaseInstanceId";
        r2 = "Unable to get master token";
        android.util.Log.e(r1, r2, r0);
        goto L_0x000f;
    L_0x0063:
        r4 = com.google.firebase.iid.FirebaseInstanceId.zzbyk();
        r0 = r4.zzbyn();
    L_0x006b:
        if (r0 == 0) goto L_0x00d2;
    L_0x006d:
        r1 = "!";
        r1 = r0.split(r1);
        r5 = r1.length;
        r6 = 2;
        if (r5 != r6) goto L_0x0086;
    L_0x0077:
        r5 = r1[r2];
        r6 = r1[r3];
        r1 = -1;
        r7 = r5.hashCode();	 Catch:{ IOException -> 0x00b5 }
        switch(r7) {
            case 83: goto L_0x008e;
            case 84: goto L_0x0083;
            case 85: goto L_0x0098;
            default: goto L_0x0083;
        };
    L_0x0083:
        switch(r1) {
            case 0: goto L_0x00a2;
            case 1: goto L_0x00bf;
            default: goto L_0x0086;
        };
    L_0x0086:
        r4.zzpu(r0);
        r0 = r4.zzbyn();
        goto L_0x006b;
    L_0x008e:
        r7 = "S";
        r5 = r5.equals(r7);	 Catch:{ IOException -> 0x00b5 }
        if (r5 == 0) goto L_0x0083;
    L_0x0096:
        r1 = r2;
        goto L_0x0083;
    L_0x0098:
        r7 = "U";
        r5 = r5.equals(r7);	 Catch:{ IOException -> 0x00b5 }
        if (r5 == 0) goto L_0x0083;
    L_0x00a0:
        r1 = r3;
        goto L_0x0083;
    L_0x00a2:
        r1 = com.google.firebase.iid.FirebaseInstanceId.getInstance();	 Catch:{ IOException -> 0x00b5 }
        r1.zzpr(r6);	 Catch:{ IOException -> 0x00b5 }
        r1 = r8.zzmje;	 Catch:{ IOException -> 0x00b5 }
        if (r1 == 0) goto L_0x0086;
    L_0x00ad:
        r1 = "FirebaseInstanceId";
        r5 = "subscribe operation succeeded";
        android.util.Log.d(r1, r5);	 Catch:{ IOException -> 0x00b5 }
        goto L_0x0086;
    L_0x00b5:
        r0 = move-exception;
        r0 = r0.getMessage();
        r8.zza(r9, r0);
        goto L_0x000f;
    L_0x00bf:
        r1 = com.google.firebase.iid.FirebaseInstanceId.getInstance();	 Catch:{ IOException -> 0x00b5 }
        r1.zzps(r6);	 Catch:{ IOException -> 0x00b5 }
        r1 = r8.zzmje;	 Catch:{ IOException -> 0x00b5 }
        if (r1 == 0) goto L_0x0086;
    L_0x00ca:
        r1 = "FirebaseInstanceId";
        r5 = "unsubscribe operation succeeded";
        android.util.Log.d(r1, r5);	 Catch:{ IOException -> 0x00b5 }
        goto L_0x0086;
    L_0x00d2:
        r0 = "FirebaseInstanceId";
        r1 = "topic sync succeeded";
        android.util.Log.d(r0, r1);
        goto L_0x000f;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.iid.FirebaseInstanceIdService.zza(android.content.Intent, boolean, boolean):void");
    }

    static void zzel(Context context) {
        if (zzl.zzdg(context) != null) {
            synchronized (zzmjc) {
                if (!zzmjd) {
                    zzq.zzbyp().zze(context, zzfw(0));
                    zzmjd = true;
                }
            }
        }
    }

    private static boolean zzem(Context context) {
        NetworkInfo activeNetworkInfo = ((ConnectivityManager) context.getSystemService("connectivity")).getActiveNetworkInfo();
        return activeNetworkInfo != null && activeNetworkInfo.isConnected();
    }

    private static Intent zzfw(int i) {
        Intent intent = new Intent("ACTION_TOKEN_REFRESH_RETRY");
        intent.putExtra("next_retry_delay_in_seconds", i);
        return intent;
    }

    private static String zzp(Intent intent) {
        String stringExtra = intent.getStringExtra("subtype");
        return stringExtra == null ? "" : stringExtra;
    }

    private final zzj zzpt(String str) {
        if (str == null) {
            return zzj.zza(this, null);
        }
        Bundle bundle = new Bundle();
        bundle.putString("subtype", str);
        return zzj.zza(this, bundle);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void handleIntent(android.content.Intent r11) {
        /*
        r10 = this;
        r1 = 0;
        r9 = 1;
        r0 = r11.getAction();
        if (r0 != 0) goto L_0x000a;
    L_0x0008:
        r0 = "";
    L_0x000a:
        r2 = r0.hashCode();
        switch(r2) {
            case -1737547627: goto L_0x0092;
            default: goto L_0x0011;
        };
    L_0x0011:
        r0 = -1;
    L_0x0012:
        switch(r0) {
            case 0: goto L_0x009d;
            default: goto L_0x0015;
        };
    L_0x0015:
        r0 = zzp(r11);
        r2 = r10.zzpt(r0);
        r3 = "CMD";
        r3 = r11.getStringExtra(r3);
        r4 = r10.zzmje;
        if (r4 == 0) goto L_0x0077;
    L_0x0027:
        r4 = r11.getExtras();
        r4 = java.lang.String.valueOf(r4);
        r5 = "FirebaseInstanceId";
        r6 = new java.lang.StringBuilder;
        r7 = java.lang.String.valueOf(r0);
        r7 = r7.length();
        r7 = r7 + 18;
        r8 = java.lang.String.valueOf(r3);
        r8 = r8.length();
        r7 = r7 + r8;
        r8 = java.lang.String.valueOf(r4);
        r8 = r8.length();
        r7 = r7 + r8;
        r6.<init>(r7);
        r7 = "Service command ";
        r6 = r6.append(r7);
        r6 = r6.append(r0);
        r7 = " ";
        r6 = r6.append(r7);
        r6 = r6.append(r3);
        r7 = " ";
        r6 = r6.append(r7);
        r4 = r6.append(r4);
        r4 = r4.toString();
        android.util.Log.d(r5, r4);
    L_0x0077:
        r4 = "unregistered";
        r4 = r11.getStringExtra(r4);
        if (r4 == 0) goto L_0x00a1;
    L_0x007f:
        r1 = com.google.firebase.iid.zzj.zzbyl();
        if (r0 != 0) goto L_0x0087;
    L_0x0085:
        r0 = "";
    L_0x0087:
        r1.zzhu(r0);
        r0 = com.google.firebase.iid.zzj.zzbym();
        r0.zzi(r11);
    L_0x0091:
        return;
    L_0x0092:
        r2 = "ACTION_TOKEN_REFRESH_RETRY";
        r0 = r0.equals(r2);
        if (r0 == 0) goto L_0x0011;
    L_0x009a:
        r0 = r1;
        goto L_0x0012;
    L_0x009d:
        r10.zza(r11, r1, r1);
        goto L_0x0091;
    L_0x00a1:
        r4 = "gcm.googleapis.com/refresh";
        r5 = "from";
        r5 = r11.getStringExtra(r5);
        r4 = r4.equals(r5);
        if (r4 == 0) goto L_0x00ba;
    L_0x00af:
        r2 = com.google.firebase.iid.zzj.zzbyl();
        r2.zzhu(r0);
        r10.zza(r11, r1, r9);
        goto L_0x0091;
    L_0x00ba:
        r4 = "RST";
        r4 = r4.equals(r3);
        if (r4 == 0) goto L_0x00c9;
    L_0x00c2:
        r2.zzasq();
        r10.zza(r11, r9, r9);
        goto L_0x0091;
    L_0x00c9:
        r4 = "RST_FULL";
        r4 = r4.equals(r3);
        if (r4 == 0) goto L_0x00e9;
    L_0x00d1:
        r0 = com.google.firebase.iid.zzj.zzbyl();
        r0 = r0.isEmpty();
        if (r0 != 0) goto L_0x0091;
    L_0x00db:
        r2.zzasq();
        r0 = com.google.firebase.iid.zzj.zzbyl();
        r0.zzasu();
        r10.zza(r11, r9, r9);
        goto L_0x0091;
    L_0x00e9:
        r2 = "SYNC";
        r2 = r2.equals(r3);
        if (r2 == 0) goto L_0x00fc;
    L_0x00f1:
        r2 = com.google.firebase.iid.zzj.zzbyl();
        r2.zzhu(r0);
        r10.zza(r11, r1, r9);
        goto L_0x0091;
    L_0x00fc:
        r0 = "PING";
        r0 = r0.equals(r3);
        if (r0 == 0) goto L_0x0091;
    L_0x0104:
        r0 = r11.getExtras();
        r1 = com.google.firebase.iid.zzl.zzdg(r10);
        if (r1 != 0) goto L_0x0117;
    L_0x010e:
        r0 = "FirebaseInstanceId";
        r1 = "Unable to respond to ping due to missing target package";
        android.util.Log.w(r0, r1);
        goto L_0x0091;
    L_0x0117:
        r2 = new android.content.Intent;
        r3 = "com.google.android.gcm.intent.SEND";
        r2.<init>(r3);
        r2.setPackage(r1);
        r2.putExtras(r0);
        com.google.firebase.iid.zzl.zzd(r10, r2);
        r0 = "google.to";
        r1 = "google.com/iid";
        r2.putExtra(r0, r1);
        r0 = "google.message_id";
        r1 = com.google.firebase.iid.zzl.zzast();
        r2.putExtra(r0, r1);
        r0 = "com.google.android.gtalkservice.permission.GTALK_SERVICE";
        r10.sendOrderedBroadcast(r2, r0);
        goto L_0x0091;
        */
        throw new UnsupportedOperationException("Method not decompiled: com.google.firebase.iid.FirebaseInstanceIdService.handleIntent(android.content.Intent):void");
    }

    @WorkerThread
    public void onTokenRefresh() {
    }

    protected final Intent zzn(Intent intent) {
        return (Intent) zzq.zzbyp().zzmjq.poll();
    }

    public final boolean zzo(Intent intent) {
        this.zzmje = Log.isLoggable("FirebaseInstanceId", 3);
        if (intent.getStringExtra("error") == null && intent.getStringExtra(RegistrarHelper.PROPERTY_REG_ID) == null) {
            return false;
        }
        String zzp = zzp(intent);
        if (this.zzmje) {
            String valueOf = String.valueOf(zzp);
            Log.d("FirebaseInstanceId", valueOf.length() != 0 ? "Register result in service ".concat(valueOf) : new String("Register result in service "));
        }
        zzpt(zzp);
        zzj.zzbym().zzi(intent);
        return true;
    }
}
