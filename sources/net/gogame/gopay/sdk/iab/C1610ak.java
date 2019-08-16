package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.graphics.Color;
import android.view.View;

/* renamed from: net.gogame.gopay.sdk.iab.ak */
final class C1610ak implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1234a;

    /* renamed from: b */
    final /* synthetic */ View f1235b;

    /* renamed from: c */
    final /* synthetic */ C1369aj f1236c;

    C1610ak(C1369aj ajVar, int i, View view) {
        this.f1236c = ajVar;
        this.f1234a = i;
        this.f1235b = view;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1236c.f1059a.f1020K = false;
        this.f1236c.f1059a.f1021L = false;
        this.f1236c.f1059a.f1012C.f1105e = this.f1234a;
        if (this.f1236c.f1059a.f1012C.f1106f != null) {
            this.f1236c.f1059a.f1012C.f1106f.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        this.f1236c.f1059a.f1012C.f1106f = this.f1235b;
        this.f1235b.setBackgroundColor(-1);
        this.f1236c.f1059a.m803a((C1365a) this.f1236c.f1059a.f1012C.getItem(this.f1234a));
    }
}
