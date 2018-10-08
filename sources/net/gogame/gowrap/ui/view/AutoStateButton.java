package net.gogame.gowrap.ui.view;

import android.content.Context;
import android.graphics.ColorFilter;
import android.graphics.LightingColorFilter;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.LayerDrawable;
import android.util.AttributeSet;
import android.widget.Button;

public class AutoStateButton extends Button {

    private class BackgroundDrawable extends LayerDrawable {
        private int disabledAlpha = 100;
        private int fullAlpha = 255;
        private ColorFilter pressedFilter = new LightingColorFilter(-3355444, 1);

        public BackgroundDrawable(Drawable drawable) {
            super(new Drawable[]{drawable});
        }

        protected boolean onStateChange(int[] iArr) {
            Object obj = null;
            Object obj2 = null;
            for (int i : iArr) {
                if (i == 16842910) {
                    obj2 = 1;
                } else if (i == 16842919) {
                    int i2 = 1;
                }
            }
            mutate();
            if (obj2 != null && r0 != null) {
                setColorFilter(this.pressedFilter);
            } else if (obj2 == null) {
                setColorFilter(null);
                setAlpha(this.disabledAlpha);
            } else {
                setColorFilter(null);
                setAlpha(this.fullAlpha);
            }
            invalidateSelf();
            return super.onStateChange(iArr);
        }

        public boolean isStateful() {
            return true;
        }
    }

    public AutoStateButton(Context context) {
        super(context);
    }

    public AutoStateButton(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
    }

    public AutoStateButton(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
    }

    public void setBackgroundDrawable(Drawable drawable) {
        super.setBackgroundDrawable(new BackgroundDrawable(drawable));
    }
}
