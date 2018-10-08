package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.graphics.Color;
import android.view.View;

final class ah implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f1057a;
    /* renamed from: b */
    final /* synthetic */ View f1058b;
    /* renamed from: c */
    final /* synthetic */ ag f1059c;

    ah(ag agVar, int i, View view) {
        this.f1059c = agVar;
        this.f1057a = i;
        this.f1058b = view;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1059c.f1056a.f1015K = false;
        this.f1059c.f1056a.f1016L = false;
        this.f1059c.f1056a.f1007C.f1144e = this.f1057a;
        if (this.f1059c.f1056a.f1007C.f1145f != null) {
            this.f1059c.f1056a.f1007C.f1145f.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        this.f1059c.f1056a.f1007C.f1145f = this.f1058b;
        this.f1058b.setBackgroundColor(-1);
        this.f1059c.f1056a.m795a((C1025a) this.f1059c.f1056a.f1007C.getItem(this.f1057a));
    }
}
