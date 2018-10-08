package net.gogame.gowrap.ui.v2017_1;

import android.annotation.TargetApi;
import android.content.Context;
import android.content.res.TypedArray;
import android.graphics.drawable.Drawable;
import android.util.AttributeSet;
import android.view.LayoutInflater;
import net.gogame.gowrap.C1426R;

public class CommunityCustomImageButton extends AbstractCustomImageButton {
    public CommunityCustomImageButton(Context context) {
        super(context);
        init(null);
    }

    public CommunityCustomImageButton(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        init(attributeSet);
    }

    public CommunityCustomImageButton(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        init(attributeSet);
    }

    @TargetApi(21)
    public CommunityCustomImageButton(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
        init(attributeSet);
    }

    protected void init(AttributeSet attributeSet) {
        addView(((LayoutInflater) getContext().getSystemService("layout_inflater")).inflate(C1426R.layout.net_gogame_gowrap_community_image_button, this, false));
        TypedArray obtainStyledAttributes = getContext().getTheme().obtainStyledAttributes(attributeSet, C1426R.styleable.CustomImageButton, 0, 0);
        try {
            Drawable drawable = obtainStyledAttributes.getDrawable(C1426R.styleable.CustomImageButton_image);
            String string = obtainStyledAttributes.getString(C1426R.styleable.CustomImageButton_caption);
            String string2 = obtainStyledAttributes.getString(C1426R.styleable.CustomImageButton_subcaption);
            int i = obtainStyledAttributes.getInt(C1426R.styleable.CustomImageButton_level, 0);
            setImage(drawable);
            setCaption(string);
            setSubCaption(string2);
            setLevel(i);
        } finally {
            obtainStyledAttributes.recycle();
        }
    }
}
