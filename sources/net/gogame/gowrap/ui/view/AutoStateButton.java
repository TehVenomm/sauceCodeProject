package net.gogame.gowrap.p019ui.view;

import android.content.Context;
import android.graphics.ColorFilter;
import android.graphics.LightingColorFilter;
import android.graphics.drawable.Drawable;
import android.graphics.drawable.LayerDrawable;
import android.util.AttributeSet;
import android.widget.Button;

/* renamed from: net.gogame.gowrap.ui.view.AutoStateButton */
public class AutoStateButton extends Button {

    /* renamed from: net.gogame.gowrap.ui.view.AutoStateButton$BackgroundDrawable */
    private class BackgroundDrawable extends LayerDrawable {
        private int disabledAlpha = 100;
        private int fullAlpha = 255;
        private ColorFilter pressedFilter = new LightingColorFilter(-3355444, 1);

        public BackgroundDrawable(Drawable drawable) {
            super(new Drawable[]{drawable});
        }

        /* access modifiers changed from: protected */
        public boolean onStateChange(int[] iArr) {
            boolean z = false;
            boolean z2 = false;
            for (int i : iArr) {
                if (i == 16842910) {
                    z2 = true;
                } else if (i == 16842919) {
                    z = true;
                }
            }
            mutate();
            if (z2 && z) {
                setColorFilter(this.pressedFilter);
            } else if (!z2) {
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
