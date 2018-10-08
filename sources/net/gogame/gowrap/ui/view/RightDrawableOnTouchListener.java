package net.gogame.gowrap.ui.view;

import android.graphics.drawable.Drawable;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnTouchListener;
import android.widget.TextView;

public abstract class RightDrawableOnTouchListener implements OnTouchListener {
    public static final int DEFAULT_FUZZ = 10;
    private final int fuzz;

    public abstract boolean onDrawableTouch(MotionEvent motionEvent);

    public RightDrawableOnTouchListener() {
        this(10);
    }

    public RightDrawableOnTouchListener(int i) {
        this.fuzz = i;
    }

    public boolean onTouch(View view, MotionEvent motionEvent) {
        if (view instanceof TextView) {
            Drawable[] compoundDrawables = ((TextView) view).getCompoundDrawables();
            Drawable drawable = null;
            if (compoundDrawables.length == 4) {
                drawable = compoundDrawables[2];
            }
            if (motionEvent.getAction() == 0 && drawable != null) {
                int x = (int) motionEvent.getX();
                int y = (int) motionEvent.getY();
                if (x >= (view.getRight() - drawable.getBounds().width()) - this.fuzz && x <= (view.getRight() - view.getPaddingRight()) + this.fuzz && y >= view.getPaddingTop() - this.fuzz && y <= (view.getHeight() - view.getPaddingBottom()) + this.fuzz) {
                    return onDrawableTouch(motionEvent);
                }
            }
        }
        return false;
    }
}
