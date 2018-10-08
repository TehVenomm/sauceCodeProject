package com.zopim.android.sdk.chatlog;

import android.support.v7.widget.RecyclerView.ViewHolder;
import android.util.Log;
import android.view.View;
import android.widget.TextView;
import com.zopim.android.sdk.C0784R;

/* renamed from: com.zopim.android.sdk.chatlog.h */
final class C0840h extends ViewHolder {
    /* renamed from: a */
    private static final String f812a = C0840h.class.getSimpleName();
    /* renamed from: b */
    private TextView f813b;

    public C0840h(View view) {
        super(view);
        this.f813b = (TextView) view.findViewById(C0784R.id.message_text);
    }

    /* renamed from: a */
    public void m685a(aa aaVar) {
        if (aaVar == null) {
            Log.e(f812a, "Item must not be null");
        } else {
            this.f813b.setText(aaVar.f745i);
        }
    }
}
