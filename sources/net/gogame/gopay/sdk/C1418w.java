package net.gogame.gopay.sdk;

import android.app.Activity;
import android.widget.LinearLayout;
import android.widget.LinearLayout.LayoutParams;
import android.widget.ListView;
import net.gogame.gopay.sdk.support.DisplayUtils;

/* renamed from: net.gogame.gopay.sdk.w */
public final class C1418w extends LinearLayout {

    /* renamed from: a */
    private final C1657u f1182a;

    public C1418w(Activity activity, C1407m mVar) {
        super(activity);
        this.f1182a = new C1657u(activity, mVar);
        setOrientation(1);
        setBackgroundColor(-1);
        ListView listView = new ListView(activity);
        listView.setHeaderDividersEnabled(false);
        listView.setFooterDividersEnabled(false);
        listView.setDivider(null);
        listView.setDividerHeight(DisplayUtils.pxFromDp(activity, 6.0f));
        listView.setAdapter(this.f1182a);
        listView.setPadding(DisplayUtils.pxFromDp(activity, 4.0f), DisplayUtils.pxFromDp(activity, 8.0f), DisplayUtils.pxFromDp(activity, 4.0f), DisplayUtils.pxFromDp(activity, 27.0f));
        listView.setClipToPadding(false);
        LayoutParams layoutParams = new LayoutParams(-1, 0);
        layoutParams.weight = 1.0f;
        listView.setLayoutParams(layoutParams);
        addView(listView);
    }

    public final void setData(C1363h hVar) {
        C1657u uVar = this.f1182a;
        uVar.f1321a = hVar;
        uVar.notifyDataSetChanged();
    }
}
