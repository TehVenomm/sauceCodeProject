package net.gogame.gopay.sdk;

import android.view.View;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemSelectedListener;

/* renamed from: net.gogame.gopay.sdk.o */
final class C1383o implements OnItemSelectedListener {
    /* renamed from: a */
    final /* synthetic */ StoreActivity f3575a;

    C1383o(StoreActivity storeActivity) {
        this.f3575a = storeActivity;
    }

    public final void onItemSelected(AdapterView adapterView, View view, int i, long j) {
        C1378j.m3893a(((Country) this.f3575a.f3348d.getItem(i)).getCode());
        this.f3575a.m3774a();
    }

    public final void onNothingSelected(AdapterView adapterView) {
    }
}
