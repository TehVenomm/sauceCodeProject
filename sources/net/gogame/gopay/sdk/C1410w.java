package net.gogame.gopay.sdk;

import android.app.Activity;
import android.view.View;
import android.view.ViewGroup.LayoutParams;
import android.widget.LinearLayout;
import android.widget.ListView;
import net.gogame.gopay.sdk.support.DisplayUtils;

/* renamed from: net.gogame.gopay.sdk.w */
public final class C1410w extends LinearLayout {
    /* renamed from: a */
    private final C1408u f3646a;

    public C1410w(Activity activity, C1381m c1381m) {
        super(activity);
        this.f3646a = new C1408u(activity, c1381m);
        setOrientation(1);
        setBackgroundColor(-1);
        View listView = new ListView(activity);
        listView.setHeaderDividersEnabled(false);
        listView.setFooterDividersEnabled(false);
        listView.setDivider(null);
        listView.setDividerHeight(DisplayUtils.pxFromDp(activity, 6.0f));
        listView.setAdapter(this.f3646a);
        listView.setPadding(DisplayUtils.pxFromDp(activity, 4.0f), DisplayUtils.pxFromDp(activity, 8.0f), DisplayUtils.pxFromDp(activity, 4.0f), DisplayUtils.pxFromDp(activity, 27.0f));
        listView.setClipToPadding(false);
        LayoutParams layoutParams = new LinearLayout.LayoutParams(-1, 0);
        layoutParams.weight = 1.0f;
        listView.setLayoutParams(layoutParams);
        addView(listView);
    }

    public final void setData(C1350h c1350h) {
        C1408u c1408u = this.f3646a;
        c1408u.f3641a = c1350h;
        c1408u.notifyDataSetChanged();
    }
}
