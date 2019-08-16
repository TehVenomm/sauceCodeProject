package net.gogame.gowrap.p019ui.view;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.util.AttributeSet;
import android.view.View.MeasureSpec;
import android.widget.ImageView;

/* renamed from: net.gogame.gowrap.ui.view.ScalingImageView */
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

    /* access modifiers changed from: protected */
    public void onMeasure(int i, int i2) {
        Drawable drawable = getDrawable();
        if (drawable != null) {
            int intrinsicWidth = drawable.getIntrinsicWidth();
            float intrinsicHeight = ((float) intrinsicWidth) / ((float) drawable.getIntrinsicHeight());
            int mode = MeasureSpec.getMode(i);
            int size = (MeasureSpec.getSize(i) - getPaddingLeft()) - getPaddingRight();
            int mode2 = MeasureSpec.getMode(i2);
            int size2 = MeasureSpec.getSize(i2);
            int paddingTop = (size2 - getPaddingTop()) - getPaddingBottom();
            switch (mode) {
                case Integer.MIN_VALUE:
                    size = Math.min(intrinsicWidth, size);
                    break;
                case 1073741824:
                    break;
                default:
                    size = intrinsicWidth;
                    break;
            }
            int i3 = (int) (((float) size) / intrinsicHeight);
            switch (mode2) {
                case Integer.MIN_VALUE:
                    if (size2 > 0) {
                        i3 = Math.min(i3, paddingTop);
                        size = (int) (((float) i3) * intrinsicHeight);
                        break;
                    }
                    break;
                case 1073741824:
                    size = (int) (((float) paddingTop) * intrinsicHeight);
                    i3 = paddingTop;
                    break;
            }
            super.onMeasure(MeasureSpec.makeMeasureSpec(getPaddingLeft() + size + getPaddingRight(), Integer.MIN_VALUE), MeasureSpec.makeMeasureSpec(i3 + getPaddingTop() + getPaddingBottom(), Integer.MIN_VALUE));
            return;
        }
        super.onMeasure(i, i2);
    }
}
