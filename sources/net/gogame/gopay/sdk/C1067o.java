package net.gogame.gopay.sdk;

import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;

/* renamed from: net.gogame.gopay.sdk.o */
final class C1067o implements OnItemSelectedListener {
    /* renamed from: a */
    final /* synthetic */ StoreActivity f1187a;

    C1067o(StoreActivity storeActivity) {
        this.f1187a = storeActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        C1062j.m868a(((Country) this.f1187a.f960d.getItem(i)).getCode());
        this.f1187a.m749a();
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
