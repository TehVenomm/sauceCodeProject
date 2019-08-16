package net.gogame.gopay.sdk;

import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;

/* renamed from: net.gogame.gopay.sdk.o */
final class C1409o implements OnItemSelectedListener {

    /* renamed from: a */
    final /* synthetic */ StoreActivity f1128a;

    C1409o(StoreActivity storeActivity) {
        this.f1128a = storeActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        C1406j.m868a(((Country) this.f1128a.f985d.getItem(i)).getCode());
        this.f1128a.m762a();
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
