package net.gogame.gopay.sdk;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.graphics.Typeface;
import android.graphics.drawable.GradientDrawable;
import android.graphics.drawable.GradientDrawable.Orientation;
import android.os.Build.VERSION;
import android.support.p000v4.view.ViewCompat;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.LinearLayout;
import android.widget.LinearLayout.LayoutParams;
import android.widget.TextView;
import net.gogame.gopay.sdk.support.DisplayUtils;
import org.onepf.oms.appstore.googleUtils.SkuDetails;

/* renamed from: net.gogame.gopay.sdk.u */
public final class C1657u extends BaseAdapter {

    /* renamed from: a */
    C1363h f1321a;
    /* access modifiers changed from: private */

    /* renamed from: b */
    public final Activity f1322b;
    /* access modifiers changed from: private */

    /* renamed from: c */
    public final C1407m f1323c;

    public C1657u(Activity activity, C1407m mVar) {
        this.f1322b = activity;
        this.f1323c = mVar;
    }

    public final int getCount() {
        if (this.f1321a == null) {
            return 0;
        }
        return this.f1321a.f1010b.size();
    }

    public final Object getItem(int i) {
        return this.f1321a.f1010b.get(i);
    }

    public final long getItemId(int i) {
        return (long) i;
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        SkuDetails skuDetails = (SkuDetails) getItem(i);
        Context context = viewGroup.getContext();
        LinearLayout linearLayout = new LinearLayout(context);
        linearLayout.setOrientation(0);
        linearLayout.setBackgroundColor(-1);
        linearLayout.setPadding(DisplayUtils.pxFromDp(context, 8.0f), DisplayUtils.pxFromDp(context, 8.0f), DisplayUtils.pxFromDp(context, 8.0f), DisplayUtils.pxFromDp(context, 8.0f));
        TextView textView = new TextView(context);
        textView.setTextSize(3, 8.0f);
        textView.setTypeface(Typeface.DEFAULT_BOLD);
        textView.setTextColor(ViewCompat.MEASURED_STATE_MASK);
        textView.setText(skuDetails.getTitle());
        if (VERSION.SDK_INT >= 17) {
            textView.setTextAlignment(5);
        }
        LayoutParams layoutParams = new LayoutParams(-2, -2);
        layoutParams.weight = 1.0f;
        layoutParams.setMargins(DisplayUtils.pxFromDp(context, 6.0f), DisplayUtils.pxFromDp(context, 0.0f), DisplayUtils.pxFromDp(context, 0.0f), DisplayUtils.pxFromDp(context, 0.0f));
        textView.setLayoutParams(layoutParams);
        linearLayout.addView(textView);
        TextView textView2 = new TextView(context);
        textView2.setTextSize(3, 8.0f);
        textView2.setTypeface(Typeface.DEFAULT);
        textView2.setTextColor(Color.parseColor("#818181"));
        textView2.setText(skuDetails.getPrice());
        if (VERSION.SDK_INT >= 17) {
            textView2.setTextAlignment(6);
        }
        LayoutParams layoutParams2 = new LayoutParams(-2, -2);
        layoutParams2.setMargins(DisplayUtils.pxFromDp(context, 0.0f), DisplayUtils.pxFromDp(context, 0.0f), DisplayUtils.pxFromDp(context, 6.0f), DisplayUtils.pxFromDp(context, 0.0f));
        textView2.setLayoutParams(layoutParams2);
        linearLayout.addView(textView2);
        GradientDrawable gradientDrawable = new GradientDrawable(Orientation.TOP_BOTTOM, new int[]{-1, -1});
        gradientDrawable.setStroke(DisplayUtils.pxFromDp(context, 1.5f), Color.parseColor("#87b856"));
        gradientDrawable.setCornerRadius((float) DisplayUtils.pxFromDp(context, 50.0f));
        if (VERSION.SDK_INT >= 16) {
            linearLayout.setBackground(gradientDrawable);
        }
        linearLayout.setOnClickListener(new C1658v(this, skuDetails));
        return linearLayout;
    }
}
