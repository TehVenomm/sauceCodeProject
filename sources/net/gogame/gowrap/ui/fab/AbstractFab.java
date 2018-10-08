package net.gogame.gowrap.ui.fab;

import android.content.Context;
import android.view.MotionEvent;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.RelativeLayout.LayoutParams;
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.ui.fab.Fab.ClickListener;
import net.gogame.gowrap.ui.utils.DisplayUtils;

public abstract class AbstractFab implements Fab {
    private ClickListener clickListener;

    protected void fireClickListener(MotionEvent motionEvent) {
        if (this.clickListener != null) {
            this.clickListener.onClick(this, motionEvent);
        }
    }

    protected View createImageView(Context context, int i) {
        View relativeLayout = new RelativeLayout(context);
        relativeLayout.setPadding(0, DisplayUtils.pxFromDp(context, 8.0f), 0, 0);
        relativeLayout.setClipToPadding(false);
        relativeLayout.setLayoutParams(new LayoutParams(-2, DisplayUtils.pxFromDp(context, 200.0f)));
        View imageView = new ImageView(context);
        imageView.setImageDrawable(context.getResources().getDrawable(i));
        relativeLayout.addView(imageView);
        imageView = new ImageView(context);
        imageView.setImageResource(C1426R.drawable.net_gogame_gowrap_server_down_fab_icon);
        ViewGroup.LayoutParams layoutParams = new LayoutParams(-2, -2);
        layoutParams.setMargins(DisplayUtils.pxFromDp(context, 20.0f), DisplayUtils.pxFromDp(context, 5.0f), 0, 0);
        imageView.setLayoutParams(layoutParams);
        imageView.setVisibility(8);
        relativeLayout.addView(imageView);
        return relativeLayout;
    }

    public void setClickListener(ClickListener clickListener) {
        this.clickListener = clickListener;
    }
}
