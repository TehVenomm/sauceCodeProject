package net.gogame.gopay.sdk.iab;

import android.content.DialogInterface;
import android.content.DialogInterface.OnClickListener;
import android.graphics.Color;
import android.view.View;

/* renamed from: net.gogame.gopay.sdk.iab.ak */
final class C1610ak implements OnClickListener {

    /* renamed from: a */
    final /* synthetic */ int f1246a;

    /* renamed from: b */
    final /* synthetic */ View f1247b;

    /* renamed from: c */
    final /* synthetic */ C1369aj f1248c;

    C1610ak(C1369aj ajVar, int i, View view) {
        this.f1248c = ajVar;
        this.f1246a = i;
        this.f1247b = view;
    }

    public final void onClick(DialogInterface dialogInterface, int i) {
        this.f1248c.f1065a.f1026K = false;
        this.f1248c.f1065a.f1027L = false;
        this.f1248c.f1065a.f1018C.f1111e = this.f1246a;
        if (this.f1248c.f1065a.f1018C.f1112f != null) {
            this.f1248c.f1065a.f1018C.f1112f.setBackgroundColor(Color.rgb(241, 241, 241));
        }
        this.f1248c.f1065a.f1018C.f1112f = this.f1247b;
        this.f1247b.setBackgroundColor(-1);
        this.f1248c.f1065a.m803a((C1365a) this.f1248c.f1065a.f1018C.getItem(this.f1246a));
    }
}
