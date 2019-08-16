package net.gogame.gopay.sdk.iab;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.support.p000v4.view.GravityCompat;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.LinearLayout;
import android.widget.LinearLayout.LayoutParams;
import android.widget.TextView;
import net.gogame.gopay.sdk.C1359a;
import net.gogame.gopay.sdk.C1636k;
import net.gogame.gopay.sdk.support.C1415m;
import net.gogame.gopay.sdk.support.DisplayUtils;

/* renamed from: net.gogame.gopay.sdk.iab.b */
public final class C1379b extends C1359a {
    public C1379b(Context context) {
        super(context);
    }

    /* renamed from: a */
    private View m846a(int i, View view) {
        if (view == 0) {
            r13 = new LinearLayout(this.f987a);
            r13.setWeightSum(1.0f);
            r13.setBackgroundColor(0);
            r13.setOrientation(0);
            r13.setGravity(GravityCompat.START);
            ImageView imageView = new ImageView(this.f987a);
            imageView.setImageResource(17301514);
            imageView.setScaleType(ScaleType.FIT_CENTER);
            imageView.setPadding(mo21497a(0), mo21497a(0), mo21497a(0), mo21497a(0));
            imageView.setVisibility(4);
            imageView.setTag(Integer.valueOf(2));
            LayoutParams layoutParams = new LayoutParams(-2, -1, 0.0f);
            layoutParams.gravity = 16;
            layoutParams.setMargins(mo21497a(1), 0, mo21497a(2), 0);
            r13.addView(imageView, layoutParams);
            TextView textView = new TextView(this.f987a);
            textView.setBackgroundColor(0);
            textView.setTextColor(Color.parseColor("#000000"));
            textView.setTextSize(2, 14.0f);
            textView.setPadding(0, 0, 0, 0);
            textView.setGravity(8388627);
            textView.setTag(Integer.valueOf(1));
            textView.setMaxLines(1);
            LayoutParams layoutParams2 = new LayoutParams(-1, -1, 1.0f);
            layoutParams2.gravity = 16;
            layoutParams2.setMargins(mo21497a(1), mo21497a(0), mo21497a(1), mo21497a(0));
            r13.addView(textView, layoutParams2);
            ImageView imageView2 = new ImageView(this.f987a);
            Bitmap e = C1415m.m931e();
            if (e != null) {
                imageView2.setImageBitmap(e);
            }
            imageView2.setScaleType(ScaleType.FIT_XY);
            imageView2.setPadding(mo21497a(1), mo21497a(1), mo21497a(1), mo21497a(1));
            imageView2.setVisibility(4);
            imageView2.setTag(Integer.valueOf(3));
            LayoutParams layoutParams3 = new LayoutParams(DisplayUtils.pxFromDp(this.f987a, 14.0f), DisplayUtils.pxFromDp(this.f987a, 8.0f), 0.0f);
            layoutParams3.gravity = 16;
            layoutParams3.setMargins(mo21497a(2), 0, mo21497a(10), 0);
            r13.addView(imageView2, layoutParams3);
            r13.setTag(new C1623bv(imageView, textView, i));
            view = r13;
        }
        View findViewWithTag = view.findViewWithTag(Integer.valueOf(3));
        if (findViewWithTag != null) {
            findViewWithTag.setVisibility(4);
        }
        C1623bv bvVar = (C1623bv) view.getTag();
        bvVar.f1262c = i;
        bvVar.f1260a.setVisibility(4);
        bvVar.f1261b.setText(((C1636k) getItem(bvVar.f1262c)).getDisplayName());
        C1415m.m929c(mo21498a(), ((C1636k) getItem(bvVar.f1262c)).getDisplayIcon(), new C1624c(this, bvVar, i));
        return view;
    }

    public final View getDropDownView(int i, View view, ViewGroup viewGroup) {
        View a = m846a(i, view);
        a.setBackgroundColor(0);
        a.setLayoutParams(new AbsListView.LayoutParams(-1, mo21497a(30)));
        return a;
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        View a = m846a(i, view);
        a.setBackgroundColor(0);
        a.setLayoutParams(new ViewGroup.LayoutParams(-1, mo21497a(30)));
        View findViewWithTag = a.findViewWithTag(Integer.valueOf(3));
        if (findViewWithTag != null && getCount() > 1) {
            findViewWithTag.setVisibility(0);
        }
        return a;
    }
}
