package net.gogame.gopay.sdk.support;

import android.content.ClipData;
import android.content.ClipboardManager;
import android.content.Context;
import android.os.Build.VERSION;
import android.view.View;
import android.view.View.OnLongClickListener;
import android.widget.Toast;

/* renamed from: net.gogame.gopay.sdk.support.b */
public final class C1389b implements OnLongClickListener {
    /* renamed from: a */
    final /* synthetic */ String f3581a;
    /* renamed from: b */
    final /* synthetic */ Context f3582b;

    public C1389b(String str, Context context) {
        this.f3581a = str;
        this.f3582b = context;
    }

    public final boolean onLongClick(View view) {
        if (VERSION.SDK_INT >= 11) {
            ((ClipboardManager) this.f3582b.getSystemService("clipboard")).setPrimaryClip(ClipData.newPlainText("Build info", this.f3581a));
            Toast.makeText(this.f3582b, "Build info copied to clipboard", 0).show();
        }
        return false;
    }
}
