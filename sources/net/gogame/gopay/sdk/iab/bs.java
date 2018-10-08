package net.gogame.gopay.sdk.iab;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Color;
import android.graphics.drawable.BitmapDrawable;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.GradientDrawable;
import android.graphics.drawable.LayerDrawable;
import android.os.Build.VERSION;
import android.support.v4.view.ViewCompat;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AbsListView.LayoutParams;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;
import android.widget.RelativeLayout;
import android.widget.TextView;
import java.lang.ref.WeakReference;
import net.gogame.gopay.sdk.C1342a;
import net.gogame.gopay.sdk.support.C1400m;

public final class bs extends C1342a {
    /* renamed from: d */
    WeakReference f3504d;
    /* renamed from: e */
    int f3505e = 0;
    /* renamed from: f */
    C1341a f3506f;

    public bs(Context context, C1361i c1361i) {
        super(context);
        this.f3504d = new WeakReference(c1361i);
    }

    /* renamed from: c */
    private C1341a m3873c(int i) {
        return i == 0 ? this.f3506f : (C1341a) ((C1361i) this.f3504d.get()).getItem(m3876b(i).intValue());
    }

    /* renamed from: a */
    public final void m3874a(C1341a c1341a) {
        this.f3506f = c1341a;
        notifyDataSetChanged();
    }

    /* renamed from: a */
    final void m3875a(bv bvVar, Bitmap bitmap) {
        if (bitmap == null) {
            bvVar.f3514a.setVisibility(4);
            bvVar.f3515b.setVisibility(0);
            return;
        }
        int width = bitmap.getWidth();
        int height = bitmap.getHeight();
        Drawable bitmapDrawable = new BitmapDrawable(this.f3350a.getResources(), bitmap);
        bitmapDrawable.setBounds(0, 0, width, height);
        bvVar.f3514a.setImageDrawable(bitmapDrawable);
        bvVar.f3514a.setVisibility(0);
        bvVar.f3515b.setVisibility(4);
    }

    /* renamed from: b */
    public final Integer m3876b(int i) {
        return i == 0 ? (Integer) super.getItem(0) : (Integer) super.getItem(i - 1);
    }

    public final int getCount() {
        return (this.f3506f != null ? 1 : 0) + super.getCount();
    }

    public final /* synthetic */ Object getItem(int i) {
        return m3876b(i);
    }

    public final View getView(int i, View view, ViewGroup viewGroup) {
        if (view == null) {
            view = new RelativeLayout(this.f3350a);
            view.setLayoutParams(new LayoutParams(m3780a(80), -1));
            View imageView = new ImageView(this.f3350a);
            imageView.setTag(Integer.valueOf(1));
            imageView.setScaleType(ScaleType.FIT_CENTER);
            ViewGroup.LayoutParams layoutParams = new RelativeLayout.LayoutParams(m3780a(77), m3780a(49));
            layoutParams.addRule(13);
            view.addView(imageView, layoutParams);
            View textView = new TextView(this.f3350a);
            textView.setBackgroundColor(0);
            textView.setTextColor(ViewCompat.MEASURED_STATE_MASK);
            textView.setGravity(17);
            textView.setTextSize(3, 6.0f);
            textView.setTag(Integer.valueOf(2));
            ViewGroup.LayoutParams layoutParams2 = new RelativeLayout.LayoutParams(-1, -1);
            layoutParams2.addRule(13);
            layoutParams2.setMargins(m3780a(1), 0, m3780a(1), 0);
            view.addView(textView, layoutParams2);
            view.setTag(new bv(imageView, textView, i));
        }
        if (i == this.f3505e) {
            int a = m3780a(2);
            GradientDrawable gradientDrawable = new GradientDrawable();
            gradientDrawable.setStroke(a, Color.rgb(92, 176, 59));
            gradientDrawable.setColor(-1);
            gradientDrawable.setGradientType(2);
            Drawable layerDrawable = new LayerDrawable(new Drawable[]{gradientDrawable});
            layerDrawable.setLayerInset(0, -a, -a, -a, 0);
            view.setAlpha(1.0f);
            if (VERSION.SDK_INT >= 16) {
                view.setBackground(layerDrawable);
            } else {
                view.setBackgroundDrawable(layerDrawable);
            }
        } else {
            view.setAlpha(0.5f);
            view.setBackgroundColor(0);
        }
        bv bvVar = (bv) view.getTag();
        bvVar.f3516c = i;
        bvVar.f3515b.setText(m3873c(bvVar.f3516c).getDisplayName());
        bvVar.f3514a.setVisibility(4);
        if (m3781a() == null) {
            bvVar.f3515b.setVisibility(0);
        }
        C1341a c = m3873c(bvVar.f3516c);
        if (c instanceof C1359f) {
            m3875a(bvVar, C1400m.m3963i());
        } else {
            C1400m.m3955b(m3781a(), c.getDisplayIcon(), new bt(this, bvVar, i));
        }
        return view;
    }
}
