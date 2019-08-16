package net.gogame.gowrap.p019ui.v2017_1;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.res.TypedArray;
import android.graphics.drawable.Drawable;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import net.gogame.gowrap.C1423R;

/* renamed from: net.gogame.gowrap.ui.v2017_1.SupportCustomImageButton */
public class SupportCustomImageButton extends AbstractCustomImageButton {
    public SupportCustomImageButton(Context context) {
        super(context);
        init(null);
    }

    public SupportCustomImageButton(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        init(attributeSet);
    }

    public SupportCustomImageButton(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        init(attributeSet);
    }

    @TargetApi(21)
    public SupportCustomImageButton(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
        init(attributeSet);
    }

    /* access modifiers changed from: protected */
    public void init(AttributeSet attributeSet) {
        addView(((LayoutInflater) getContext().getSystemService("layout_inflater")).inflate(C1423R.C1425layout.net_gogame_gowrap_support_image_button, this, false));
        TypedArray obtainStyledAttributes = getContext().getTheme().obtainStyledAttributes(attributeSet, C1423R.styleable.CustomImageButton, 0, 0);
        try {
            Drawable drawable = obtainStyledAttributes.getDrawable(C1423R.styleable.CustomImageButton_image);
            String string = obtainStyledAttributes.getString(C1423R.styleable.CustomImageButton_caption);
            String string2 = obtainStyledAttributes.getString(C1423R.styleable.CustomImageButton_subcaption);
            int i = obtainStyledAttributes.getInt(C1423R.styleable.CustomImageButton_level, 0);
            setImage(drawable);
            setCaption(string);
            setSubCaption(string2);
            setLevel(i);
        } finally {
            obtainStyledAttributes.recycle();
        }
    }
}
