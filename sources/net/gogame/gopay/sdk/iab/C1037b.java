package net.gogame.gopay.sdk.iab;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.support.v4.view.GravityCompat;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.AbsListView;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.LinearLayout;
import android.widget.TextView;
import net.gogame.gopay.sdk.C1026a;
import net.gogame.gopay.sdk.C1063k;
import net.gogame.gopay.sdk.support.C1084m;
import net.gogame.gopay.sdk.support.DisplayUtils;

/* renamed from: net.gogame.gopay.sdk.iab.b */
public final class C1037b extends C1026a {
    public C1037b(Context context) {
        super(context);
    }

    /* renamed from: a */
    private View m840a(int i, View view) {
        View imageView;
        if (view == null) {
            view = new LinearLayout(this.f962a);
            view.setWeightSum(1.0f);
            view.setBackgroundColor(0);
            view.setOrientation(0);
            view.setGravity(GravityCompat.START);
            imageView = new ImageView(this.f962a);
            imageView.setImageResource(17301514);
            imageView.setScaleType(ScaleType.FIT_CENTER);
            imageView.setPadding(m755a(0), m755a(0), m755a(0), m755a(0));
            imageView.setVisibility(4);
            imageView.setTag(Integer.valueOf(2));
            LayoutParams layoutParams = new LinearLayout.LayoutParams(-2, -1, 0.0f);
            layoutParams.gravity = 16;
            layoutParams.setMargins(m755a(1), 0, m755a(2), 0);
            view.addView(imageView, layoutParams);
            View textView = new TextView(this.f962a);
            textView.setBackgroundColor(0);
            textView.setTextColor(Color.parseColor("#000000"));
            textView.setTextSize(2, 14.0f);
            textView.setPadding(0, 0, 0, 0);
            textView.setGravity(8388627);
            textView.setTag(Integer.valueOf(1));
            textView.setMaxLines(1);
            LayoutParams layoutParams2 = new LinearLayout.LayoutParams(-1, -1, 1.0f);
            layoutParams2.gravity = 16;
            layoutParams2.setMargins(m755a(1), m755a(0), m755a(1), m755a(0));
            view.addView(textView, layoutParams2);
            View imageView2 = new ImageView(this.f962a);
            Bitmap e = C1084m.m934e();
            if (e != null) {
                imageView2.setImageBitmap(e);
            }
            imageView2.setScaleType(ScaleType.FIT_XY);
            imageView2.setPadding(m755a(1), m755a(1), m755a(1), m755a(1));
            imageView2.setVisibility(4);
            imageView2.setTag(Integer.valueOf(3));
            LayoutParams layoutParams3 = new LinearLayout.LayoutParams(DisplayUtils.pxFromDp(this.f962a, 14.0f), DisplayUtils.pxFromDp(this.f962a, 8.0f), 0.0f);
            layoutParams3.gravity = 16;
            layoutParams3.setMargins(m755a(2), 0, m755a(10), 0);
            view.addView(imageView2, layoutParams3);
            view.setTag(new bv(imageView, textView, i));
        }
        imageView = view.findViewWithTag(Integer.valueOf(3));
        if (imageView != null) {
            imageView.setVisibility(4);
        }
        bv bvVar = (bv) view.getTag();
        bvVar.f1128c = i;
        bvVar.f1126a.setVisibility(4);
        bvVar.f1127b.setText(((C1063k) getItem(bvVar.f1128c)).getDisplayName());
        C1084m.m932c(m756a(), ((C1063k) getItem(bvVar.f1128c)).getDisplayIcon(), new C1040c(this, bvVar, i));
        return view;
    }

    public final View getDropDownView(int i, View view, ViewGroup viewGroup) {
        View a = m840a(i, view);
        a.setBackgroundColor(0);
        a.setLayoutParams(new AbsListView.LayoutParams(-1, m755a(30)));
        return a;
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        View a = m840a(i, view);
        a.setBackgroundColor(0);
        a.setLayoutParams(new LayoutParams(-1, m755a(30)));
        View findViewWithTag = a.findViewWithTag(Integer.valueOf(3));
        if (findViewWithTag != null && getCount() > 1) {
            findViewWithTag.setVisibility(0);
        }
        return a;
    }
}
