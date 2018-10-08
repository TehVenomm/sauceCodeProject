package net.gogame.gopay.sdk.support;

import android.content.ClipData;
import android.content.ClipboardManager;
import android.content.Context;
import android.os.Build.VERSION;
import android.view.View;
import android.view.View.OnLongClickListener;
import android.widget.Toast;

/* renamed from: net.gogame.gopay.sdk.support.b */
public final class C1073b implements OnLongClickListener {
    /* renamed from: a */
    final /* synthetic */ String f1193a;
    /* renamed from: b */
    final /* synthetic */ Context f1194b;

    public C1073b(String str, Context context) {
        this.f1193a = str;
        this.f1194b = context;
    }

    public final boolean onLongClick(View view) {
        if (VERSION.SDK_INT >= 11) {
            ((ClipboardManager) this.f1194b.getSystemService("clipboard")).setPrimaryClip(ClipData.newPlainText("Build info", this.f1193a));
            Toast.makeText(this.f1194b, "Build info copied to clipboard", 0).show();
        }
        return false;
    }
}
