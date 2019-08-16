package net.gogame.gopay.sdk;

import android.content.Context;
import android.widget.BaseAdapter;
import java.util.List;
import net.gogame.gopay.sdk.support.DisplayUtils;

/* renamed from: net.gogame.gopay.sdk.a */
public abstract class C1359a extends BaseAdapter {

    /* renamed from: a */
    public final Context f987a;

    /* renamed from: b */
    protected String f988b;

    /* renamed from: c */
    public List f989c;

    public C1359a(Context context) {
        this.f987a = context;
    }

    /* renamed from: a */
    public final int mo21497a(int i) {
        return DisplayUtils.pxFromDp(this.f987a, (float) i);
    }

    /* renamed from: a */
    public final String mo21498a() {
        return this.f988b;
    }

    /* renamed from: a */
    public final void mo21499a(String str, List list) {
        this.f988b = str;
        this.f989c = list;
        notifyDataSetChanged();
    }

    public int getCount() {
        if (this.f989c == null) {
            return 0;
        }
        return this.f989c.size();
    }

    public Object getItem(int i) {
        return this.f989c.get(i);
    }

    public long getItemId(int i) {
        return (long) i;
    }
}
