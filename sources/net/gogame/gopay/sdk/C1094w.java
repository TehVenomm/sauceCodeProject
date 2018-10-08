package net.gogame.gopay.sdk;

import android.app.Activity;
import android.view.View;
import android.view.ViewGroup.LayoutParams;
import android.widget.LinearLayout;
import android.widget.ListView;
import net.gogame.gopay.sdk.support.DisplayUtils;

/* renamed from: net.gogame.gopay.sdk.w */
public final class C1094w extends LinearLayout {
    /* renamed from: a */
    private final C1092u f1258a;

    public C1094w(Activity activity, C1065m c1065m) {
        super(activity);
        this.f1258a = new C1092u(activity, c1065m);
        setOrientation(1);
        setBackgroundColor(-1);
        View listView = new ListView(activity);
        listView.setHeaderDividersEnabled(false);
        listView.setFooterDividersEnabled(false);
        listView.setDivider(null);
        listView.setDividerHeight(DisplayUtils.pxFromDp(activity, 6.0f));
        listView.setAdapter(this.f1258a);
        listView.setPadding(DisplayUtils.pxFromDp(activity, 4.0f), DisplayUtils.pxFromDp(activity, 8.0f), DisplayUtils.pxFromDp(activity, 4.0f), DisplayUtils.pxFromDp(activity, 27.0f));
        listView.setClipToPadding(false);
        LayoutParams layoutParams = new LinearLayout.LayoutParams(-1, 0);
        layoutParams.weight = 1.0f;
        listView.setLayoutParams(layoutParams);
        addView(listView);
    }

    public final void setData(C1034h c1034h) {
        C1092u c1092u = this.f1258a;
        c1092u.f1253a = c1034h;
        c1092u.notifyDataSetChanged();
    }
}
