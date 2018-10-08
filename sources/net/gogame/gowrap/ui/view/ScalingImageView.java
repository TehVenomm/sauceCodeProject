package net.gogame.gowrap.ui.view;

import android.content.Context;
import android.util.AttributeSet;
import android.widget.ImageView;

public class ScalingImageView extends ImageView {
    public ScalingImageView(Context context) {
        super(context);
    }

    public ScalingImageView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    public ScalingImageView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
    }

    /* JADX WARNING: inconsistent code. */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    protected void onMeasure(int r11, int r12) {
        /*
        r10 = this;
        r8 = -2147483648; // 0xffffffff80000000 float:-0.0 double:NaN;
        r1 = r10.getDrawable();
        if (r1 == 0) goto L_0x007b;
    L_0x0008:
        r0 = r1.getIntrinsicWidth();
        r1 = r1.getIntrinsicHeight();
        r2 = (float) r0;
        r1 = (float) r1;
        r3 = r2 / r1;
        r4 = android.view.View.MeasureSpec.getMode(r11);
        r1 = android.view.View.MeasureSpec.getSize(r11);
        r2 = r10.getPaddingLeft();
        r1 = r1 - r2;
        r2 = r10.getPaddingRight();
        r2 = r1 - r2;
        r5 = android.view.View.MeasureSpec.getMode(r12);
        r6 = android.view.View.MeasureSpec.getSize(r12);
        r1 = r10.getPaddingTop();
        r1 = r6 - r1;
        r7 = r10.getPaddingBottom();
        r1 = r1 - r7;
        switch(r4) {
            case -2147483648: goto L_0x0065;
            case 1073741824: goto L_0x003e;
            default: goto L_0x003d;
        };
    L_0x003d:
        r2 = r0;
    L_0x003e:
        r0 = (float) r2;
        r0 = r0 / r3;
        r0 = (int) r0;
        switch(r5) {
            case -2147483648: goto L_0x006a;
            case 1073741824: goto L_0x0074;
            default: goto L_0x0044;
        };
    L_0x0044:
        r1 = r2;
    L_0x0045:
        r2 = r10.getPaddingLeft();
        r1 = r1 + r2;
        r2 = r10.getPaddingRight();
        r1 = r1 + r2;
        r2 = r10.getPaddingTop();
        r0 = r0 + r2;
        r2 = r10.getPaddingBottom();
        r0 = r0 + r2;
        r1 = android.view.View.MeasureSpec.makeMeasureSpec(r1, r8);
        r0 = android.view.View.MeasureSpec.makeMeasureSpec(r0, r8);
        super.onMeasure(r1, r0);
    L_0x0064:
        return;
    L_0x0065:
        r2 = java.lang.Math.min(r0, r2);
        goto L_0x003e;
    L_0x006a:
        if (r6 <= 0) goto L_0x0044;
    L_0x006c:
        r0 = java.lang.Math.min(r0, r1);
        r1 = (float) r0;
        r1 = r1 * r3;
        r1 = (int) r1;
        goto L_0x0045;
    L_0x0074:
        r0 = (float) r1;
        r0 = r0 * r3;
        r0 = (int) r0;
        r9 = r1;
        r1 = r0;
        r0 = r9;
        goto L_0x0045;
    L_0x007b:
        super.onMeasure(r11, r12);
        goto L_0x0064;
        */
        throw new UnsupportedOperationException("Method not decompiled: net.gogame.gowrap.ui.view.ScalingImageView.onMeasure(int, int):void");
    }
}
