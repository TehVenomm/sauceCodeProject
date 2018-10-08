package net.gogame.gopay.sdk;

import android.content.Context;
import android.widget.BaseAdapter;
import java.util.List;
import net.gogame.gopay.sdk.support.DisplayUtils;

/* renamed from: net.gogame.gopay.sdk.a */
public abstract class C1026a extends BaseAdapter {
    /* renamed from: a */
    public final Context f962a;
    /* renamed from: b */
    protected String f963b;
    /* renamed from: c */
    public List f964c;

    public C1026a(Context context) {
        this.f962a = context;
    }

    /* renamed from: a */
    public final int m755a(int i) {
        return DisplayUtils.pxFromDp(this.f962a, (float) i);
    }

    /* renamed from: a */
    public final String m756a() {
        return this.f963b;
    }

    /* renamed from: a */
    public final void m757a(String str, List list) {
        this.f963b = str;
        this.f964c = list;
        notifyDataSetChanged();
    }

    public int getCount() {
        return this.f964c == null ? 0 : this.f964c.size();
    }

    public Object getItem(int i) {
        return this.f964c.get(i);
    }

    public long getItemId(int i) {
        return (long) i;
    }
}
