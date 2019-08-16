package com.appsflyer;

import android.content.Context;
import android.content.Intent;
import android.content.IntentFilter;
import android.support.annotation.NonNull;
import com.facebook.internal.FacebookRequestErrorClassification;
import com.google.firebase.analytics.FirebaseAnalytics.Param;

/* renamed from: com.appsflyer.c */
final class C0429c {

    /* renamed from: ˊ */
    private IntentFilter f235 = new IntentFilter("android.intent.action.BATTERY_CHANGED");

    /* renamed from: com.appsflyer.c$c */
    static final class C0430c {

        /* renamed from: ˊ */
        static final C0429c f236 = new C0429c();
    }

    /* renamed from: com.appsflyer.c$e */
    static final class C0431e {

        /* renamed from: ˋ */
        private final String f237;

        /* renamed from: ˎ */
        private final float f238;

        C0431e(float f, String str) {
            this.f238 = f;
            this.f237 = str;
        }

        /* access modifiers changed from: 0000 */
        /* renamed from: ˎ */
        public final float mo6541() {
            return this.f238;
        }

        /* access modifiers changed from: 0000 */
        /* renamed from: ॱ */
        public final String mo6542() {
            return this.f237;
        }

        C0431e() {
        }
    }

    C0429c() {
    }

    /* access modifiers changed from: 0000 */
    @NonNull
    /* renamed from: ˊ */
    public final C0431e mo6540(Context context) {
        String str = null;
        float f = 0.0f;
        try {
            Intent registerReceiver = context.registerReceiver(null, this.f235);
            if (registerReceiver != null) {
                if (2 == registerReceiver.getIntExtra("status", -1)) {
                    switch (registerReceiver.getIntExtra("plugged", -1)) {
                        case 1:
                            str = "ac";
                            break;
                        case 2:
                            str = "usb";
                            break;
                        case 4:
                            str = "wireless";
                            break;
                        default:
                            str = FacebookRequestErrorClassification.KEY_OTHER;
                            break;
                    }
                } else {
                    str = "no";
                }
                int intExtra = registerReceiver.getIntExtra(Param.LEVEL, -1);
                int intExtra2 = registerReceiver.getIntExtra("scale", -1);
                if (!(-1 == intExtra || -1 == intExtra2)) {
                    f = (100.0f * ((float) intExtra)) / ((float) intExtra2);
                }
            }
        } catch (Throwable th) {
        }
        return new C0431e(f, str);
    }
}
