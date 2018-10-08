package com.appsflyer.share;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import com.google.android.gms.drive.DriveFile;

/* renamed from: com.appsflyer.share.c */
final class C0294c {
    /* renamed from: ˎ */
    private String f319;

    C0294c() {
    }

    /* renamed from: ˏ */
    final void m362(String str) {
        this.f319 = str;
    }

    /* renamed from: ˏ */
    final void m361(Context context) {
        if (this.f319 != null) {
            context.startActivity(new Intent("android.intent.action.VIEW", Uri.parse(this.f319)).setFlags(DriveFile.MODE_READ_ONLY));
        }
    }
}
