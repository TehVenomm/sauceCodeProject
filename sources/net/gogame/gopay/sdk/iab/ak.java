package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.graphics.Color;
import android.view.View;

final class ak implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f1062a;
    /* renamed from: b */
    final /* synthetic */ View f1063b;
    /* renamed from: c */
    final /* synthetic */ aj f1064c;

    ak(aj ajVar, int i, View view) {
        this.f1064c = ajVar;
        this.f1062a = i;
        this.f1063b = view;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1064c.f1061a.f1015K = false;
        this.f1064c.f1061a.f1016L = false;
        this.f1064c.f1061a.f1007C.f1144e = this.f1062a;
        if (this.f1064c.f1061a.f1007C.f1145f != null) {
            this.f1064c.f1061a.f1007C.f1145f.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        this.f1064c.f1061a.f1007C.f1145f = this.f1063b;
        this.f1063b.setBackgroundColor(-1);
        this.f1064c.f1061a.m795a((C1025a) this.f1064c.f1061a.f1007C.getItem(this.f1062a));
    }
}
