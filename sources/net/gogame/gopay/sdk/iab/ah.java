package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.graphics.Color;
import android.view.View;

final class ah implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f3445a;
    /* renamed from: b */
    final /* synthetic */ View f3446b;
    /* renamed from: c */
    final /* synthetic */ ag f3447c;

    ah(ag agVar, int i, View view) {
        this.f3447c = agVar;
        this.f3445a = i;
        this.f3446b = view;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f3447c.f3444a.f3403K = false;
        this.f3447c.f3444a.f3404L = false;
        this.f3447c.f3444a.f3395C.f3532e = this.f3445a;
        if (this.f3447c.f3444a.f3395C.f3533f != null) {
            this.f3447c.f3444a.f3395C.f3533f.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        this.f3447c.f3444a.f3395C.f3533f = this.f3446b;
        this.f3446b.setBackgroundColor(-1);
        this.f3447c.f3444a.m3820a((C1341a) this.f3447c.f3444a.f3395C.getItem(this.f3445a));
    }
}
