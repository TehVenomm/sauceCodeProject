package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.graphics.Color;
import android.view.View;

/* renamed from: net.gogame.gopay.sdk.iab.ah */
final class C1608ah implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1230a;

    /* renamed from: b */
    final /* synthetic */ View f1231b;

    /* renamed from: c */
    final /* synthetic */ C1368ag f1232c;

    C1608ah(C1368ag agVar, int i, View view) {
        this.f1232c = agVar;
        this.f1230a = i;
        this.f1231b = view;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1232c.f1058a.f1020K = false;
        this.f1232c.f1058a.f1021L = false;
        this.f1232c.f1058a.f1012C.f1105e = this.f1230a;
        if (this.f1232c.f1058a.f1012C.f1106f != null) {
            this.f1232c.f1058a.f1012C.f1106f.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        this.f1232c.f1058a.f1012C.f1106f = this.f1231b;
        this.f1231b.setBackgroundColor(-1);
        this.f1232c.f1058a.m803a((C1365a) this.f1232c.f1058a.f1012C.getItem(this.f1230a));
    }
}
