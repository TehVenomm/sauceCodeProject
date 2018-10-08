package net.gogame.gowrap.ui.layout;

import android.content.Context;
import android.os.Build.VERSION;
import android.util.AttributeSet;
import android.view.View;
import android.view.View.MeasureSpec;
import android.widget.FrameLayout;

public class LandscapeFrameLayout extends FrameLayout {
    private boolean rotate = false;

    public LandscapeFrameLayout(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    protected void onMeasure(int i, int i2) {
        if (VERSION.SDK_INT < 21 || MeasureSpec.getMode(i) != 1073741824 || MeasureSpec.getMode(i2) != 1073741824 || MeasureSpec.getSize(i) >= MeasureSpec.getSize(i2)) {
            super.onMeasure(i, i2);
            setMeasuredDimension(MeasureSpec.getSize(i), MeasureSpec.getSize(i2));
            this.rotate = false;
            return;
        }
        super.onMeasure(i2, i);
        setMeasuredDimension(MeasureSpec.getSize(i), MeasureSpec.getSize(i2));
        this.rotate = true;
    }

    protected void onLayout(boolean z, int i, int i2, int i3, int i4) {
        if (this.rotate) {
            int childCount = getChildCount();
            for (int i5 = 0; i5 < childCount; i5++) {
                View childAt = getChildAt(i5);
                if (childAt != null) {
                    childAt.setRotation(90.0f);
                    int i6 = i3 - i;
                    int i7 = i4 - i2;
                    childAt.setTranslationX((float) ((i6 - i7) / 2));
                    childAt.setTranslationY((float) ((i7 - i6) / 2));
                }
            }
            super.onLayout(z, 0, 0, i4, i3);
            return;
        }
        super.onLayout(z, i, i2, i3, i4);
    }
}
