package net.gogame.gowrap.ui.v2017_1;

import android.annotation.TargetApi;
import android.content.Context;
import android.graphics.drawable.Drawable;
import android.util.AttributeSet;
import android.view.View;
import android.widget.FrameLayout;
import android.widget.ImageView;
import android.widget.TextView;
import net.gogame.gowrap.C1426R;

public abstract class AbstractCustomImageButton extends FrameLayout {
    protected abstract void init(AttributeSet attributeSet);

    public AbstractCustomImageButton(Context context) {
        super(context);
        init(null);
    }

    public AbstractCustomImageButton(Context context, AttributeSet attributeSet) {
        super(context, attributeSet);
        init(attributeSet);
    }

    public AbstractCustomImageButton(Context context, AttributeSet attributeSet, int i) {
        super(context, attributeSet, i);
        init(attributeSet);
    }

    @TargetApi(21)
    public AbstractCustomImageButton(Context context, AttributeSet attributeSet, int i, int i2) {
        super(context, attributeSet, i, i2);
        init(attributeSet);
    }

    public void setLevel(int i) {
        ((ImageView) findViewById(C1426R.id.net_gogame_gowrap_image_button_content_background)).setImageLevel(i);
    }

    public void setImage(Drawable drawable) {
        ((ImageView) findViewById(C1426R.id.net_gogame_gowrap_image_button_image)).setImageDrawable(drawable);
    }

    public void setCaption(String str) {
        TextView textView = (TextView) findViewById(C1426R.id.net_gogame_gowrap_image_button_caption);
        if (str != null) {
            textView.setText(str);
            textView.setVisibility(0);
            return;
        }
        textView.setVisibility(8);
    }

    public void setSubCaption(String str) {
        TextView textView = (TextView) findViewById(C1426R.id.net_gogame_gowrap_image_button_subcaption);
        if (textView == null) {
            return;
        }
        if (str != null) {
            textView.setText(str);
            textView.setVisibility(0);
            return;
        }
        textView.setVisibility(8);
    }

    public void setMasked(boolean z) {
        View findViewById = findViewById(C1426R.id.net_gogame_gowrap_image_button_mask);
        if (findViewById == null) {
            return;
        }
        if (z) {
            findViewById.setVisibility(0);
        } else {
            findViewById.setVisibility(4);
        }
    }
}
