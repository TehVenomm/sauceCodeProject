package im.getsocial.sdk.ui.internal.views;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.util.AttributeSet;
import android.view.View.MeasureSpec;
import android.widget.ImageView;
import android.widget.ImageView.ScaleType;

public class AspectRatioImageView extends ImageView {
    /* renamed from: a */
    private double f3107a = -1.0d;

    public AspectRatioImageView(Context context) {
        super(context);
    }

    public AspectRatioImageView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    public AspectRatioImageView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
    }

    /* renamed from: a */
    public final void m3483a(double d) {
        this.f3107a = d;
    }

    protected void onMeasure(int i, int i2) {
        if (this.f3107a == -1.0d) {
            super.onMeasure(i, i2);
            return;
        }
        int size = MeasureSpec.getSize(i2);
        int i3 = (int) (((double) size) * this.f3107a);
        Drawable drawable = getDrawable();
        if (drawable != null) {
            int intrinsicWidth = drawable.getIntrinsicWidth();
            int intrinsicHeight = drawable.getIntrinsicHeight();
            if (!(intrinsicWidth == i3 || intrinsicHeight == size)) {
                if (intrinsicWidth >= i3 || intrinsicHeight >= size || i3 / intrinsicWidth <= 4) {
                    setScaleType(ScaleType.CENTER_CROP);
                } else {
                    setScaleType(ScaleType.FIT_XY);
                }
            }
        }
        setMeasuredDimension(i3, size);
    }
}
