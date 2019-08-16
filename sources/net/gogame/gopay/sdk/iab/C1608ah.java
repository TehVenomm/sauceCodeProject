package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.graphics.Color;
import android.view.View;

/* renamed from: net.gogame.gopay.sdk.iab.ah */
final class C1608ah implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1242a;

    /* renamed from: b */
    final /* synthetic */ View f1243b;

    /* renamed from: c */
    final /* synthetic */ C1368ag f1244c;

    C1608ah(C1368ag agVar, int i, View view) {
        this.f1244c = agVar;
        this.f1242a = i;
        this.f1243b = view;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1244c.f1064a.f1026K = false;
        this.f1244c.f1064a.f1027L = false;
        this.f1244c.f1064a.f1018C.f1111e = this.f1242a;
        if (this.f1244c.f1064a.f1018C.f1112f != null) {
            this.f1244c.f1064a.f1018C.f1112f.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        this.f1244c.f1064a.f1018C.f1112f = this.f1243b;
        this.f1243b.setBackgroundColor(-1);
        this.f1244c.f1064a.m803a((C1365a) this.f1244c.f1064a.f1018C.getItem(this.f1242a));
    }
}
