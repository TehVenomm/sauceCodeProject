package net.gogame.gopay.sdk;

import android.content.Context;
import android.widget.BaseAdapter;
import java.util.List;
import net.gogame.gopay.sdk.support.DisplayUtils;

/* renamed from: net.gogame.gopay.sdk.a */
public abstract class C1359a extends BaseAdapter {

    /* renamed from: a */
    public final Context f993a;

    /* renamed from: b */
    protected String f994b;

    /* renamed from: c */
    public List f995c;

    public C1359a(Context context) {
        this.f993a = context;
    }

    /* renamed from: a */
    public final int mo21497a(int i) {
        return DisplayUtils.pxFromDp(this.f993a, (float) i);
    }

    /* renamed from: a */
    public final String mo21498a() {
        return this.f994b;
    }

    /* renamed from: a */
    public final void mo21499a(String str, List list) {
        this.f994b = str;
        this.f995c = list;
        notifyDataSetChanged();
    }

    public int getCount() {
        if (this.f995c == null) {
            return 0;
        }
        return this.f995c.size();
    }

    public Object getItem(int i) {
        return this.f995c.get(i);
    }

    public long getItemId(int i) {
        return (long) i;
    }
}
