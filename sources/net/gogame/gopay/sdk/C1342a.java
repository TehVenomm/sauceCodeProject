package net.gogame.gopay.sdk;

import android.content.Context;
import android.widget.BaseAdapter;
import java.util.List;
import net.gogame.gopay.sdk.support.DisplayUtils;

/* renamed from: net.gogame.gopay.sdk.a */
public abstract class C1342a extends BaseAdapter {
    /* renamed from: a */
    public final Context f3350a;
    /* renamed from: b */
    protected String f3351b;
    /* renamed from: c */
    public List f3352c;

    public C1342a(Context context) {
        this.f3350a = context;
    }

    /* renamed from: a */
    public final int m3780a(int i) {
        return DisplayUtils.pxFromDp(this.f3350a, (float) i);
    }

    /* renamed from: a */
    public final String m3781a() {
        return this.f3351b;
    }

    /* renamed from: a */
    public final void m3782a(String str, List list) {
        this.f3351b = str;
        this.f3352c = list;
        notifyDataSetChanged();
    }

    public int getCount() {
        return this.f3352c == null ? 0 : this.f3352c.size();
    }

    public Object getItem(int i) {
        return this.f3352c.get(i);
    }

    public long getItemId(int i) {
        return (long) i;
    }
}
