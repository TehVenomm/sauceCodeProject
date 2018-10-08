package com.zopim.android.sdk.widget.view;

import android.content.Context;
import android.content.res.TypedArray;
import android.util.AttributeSet;
import android.widget.RelativeLayout;
import com.zopim.android.sdk.C0784R;

public class WidgetView extends RelativeLayout {
    private int mPosition = Anchor.UNKNOWN.getValue();

    public enum Anchor {
        TOP_LEFT(0),
        TOP_RIGHT(1),
        BOTTOM_LEFT(2),
        BOTTOM_RIGHT(3),
        UNKNOWN(-1);
        
        final int position;

        private Anchor(int i) {
            this.position = i;
        }

        public static Anchor getType(int i) {
            for (Anchor anchor : values()) {
                if (anchor.getValue() == i) {
                    return anchor;
                }
            }
            return UNKNOWN;
        }

        public int getValue() {
            return this.position;
        }
    }

    public WidgetView(Context context) {
        super(context);
    }

    public WidgetView(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        TypedArray obtainStyledAttributes = context.getTheme().obtainStyledAttributes(attributeSet, C0784R.styleable.WidgetView, 0, 0);
        try {
            this.mPosition = obtainStyledAttributes.getInteger(C0784R.styleable.WidgetView_anchor, Anchor.UNKNOWN.getValue());
        } finally {
            obtainStyledAttributes.recycle();
        }
    }

    public WidgetView(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
    }

    public Anchor getAnchor() {
        return Anchor.getType(this.mPosition);
    }
}
