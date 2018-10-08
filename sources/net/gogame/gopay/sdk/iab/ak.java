package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.graphics.Color;
import android.view.View;

final class ak implements OnClickListener {
    /* renamed from: a */
    final /* synthetic */ int f3450a;
    /* renamed from: b */
    final /* synthetic */ View f3451b;
    /* renamed from: c */
    final /* synthetic */ aj f3452c;

    ak(aj ajVar, int i, View view) {
        this.f3452c = ajVar;
        this.f3450a = i;
        this.f3451b = view;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f3452c.f3449a.f3403K = false;
        this.f3452c.f3449a.f3404L = false;
        this.f3452c.f3449a.f3395C.f3532e = this.f3450a;
        if (this.f3452c.f3449a.f3395C.f3533f != null) {
            this.f3452c.f3449a.f3395C.f3533f.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        this.f3452c.f3449a.f3395C.f3533f = this.f3451b;
        this.f3451b.setBackgroundColor(-1);
        this.f3452c.f3449a.m3820a((C1341a) this.f3452c.f3449a.f3395C.getItem(this.f3450a));
    }
}
