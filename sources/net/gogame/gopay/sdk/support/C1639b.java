package net.gogame.gopay.sdk.support;

import android.content.ClipData;
import android.content.ClipboardManager;
import android.content.Context;
import android.os.Build.VERSION;
import android.view.View;
import android.view.View.OnLongClickListener;
import android.widget.Toast;

/* renamed from: net.gogame.gopay.sdk.support.b */
public final class C1639b implements OnLongClickListener {

    /* renamed from: a */
    final /* synthetic */ String f1303a;

    /* renamed from: b */
    final /* synthetic */ Context f1304b;

    public C1639b(String str, Context context) {
        this.f1303a = str;
        this.f1304b = context;
    }

    public final boolean onLongClick(View view) {
        if (VERSION.SDK_INT >= 11) {
            ((ClipboardManager) this.f1304b.getSystemService("clipboard")).setPrimaryClip(ClipData.newPlainText("Build info", this.f1303a));
            Toast.makeText(this.f1304b, "Build info copied to clipboard", 0).show();
        }
        return false;
    }
}
