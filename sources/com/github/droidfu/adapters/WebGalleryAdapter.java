package com.github.droidfu.adapters;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.view.View;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.BaseAdapter;
import android.widget.FrameLayout;
import android.widget.Gallery;
import com.github.droidfu.widgets.WebImageView;
import java.util.List;

public class WebGalleryAdapter extends BaseAdapter {
    public static final int NO_DRAWABLE = -1;
    private Context context;
    private Drawable errorDrawable;
    private List<String> imageUrls;
    private Drawable progressDrawable;

    private static final class ViewHolder {
        private WebImageView webImageView;

        private ViewHolder() {
        }
    }

    public WebGalleryAdapter(Context context) {
        initialize(context, null, null, null);
    }

    public WebGalleryAdapter(Context context, List<String> list) {
        initialize(context, list, null, null);
    }

    public WebGalleryAdapter(Context context, List<String> list, int i) {
        initialize(context, list, context.getResources().getDrawable(i), null);
    }

    public WebGalleryAdapter(Context context, List<String> list, int i, int i2) {
        Drawable drawable = null;
        Drawable drawable2 = i == -1 ? null : context.getResources().getDrawable(i);
        if (i2 != -1) {
            drawable = context.getResources().getDrawable(i2);
        }
        initialize(context, list, drawable2, drawable);
    }

    private void initialize(Context context, List<String> list, Drawable drawable, Drawable drawable2) {
        this.imageUrls = list;
        this.context = context;
        this.progressDrawable = drawable;
        this.errorDrawable = drawable2;
    }

    public int getCount() {
        return this.imageUrls.size();
    }

    public List<String> getImageUrls() {
        return this.imageUrls;
    }

    public Object getItem(int i) {
        return this.imageUrls.get(i);
    }

    public long getItemId(int i) {
        return (long) i;
    }

    public Drawable getProgressDrawable() {
        return this.progressDrawable;
    }

    public View getView(int i, View view, ViewGroup viewGroup) {
        WebImageView webImageView;
        String str = (String) getItem(i);
        if (view == null) {
            webImageView = new WebImageView(this.context, null, this.progressDrawable, this.errorDrawable, false);
            LayoutParams layoutParams = new FrameLayout.LayoutParams(-2, -2);
            layoutParams.gravity = 17;
            webImageView.setLayoutParams(layoutParams);
            view = new FrameLayout(this.context);
            view.setLayoutParams(new Gallery.LayoutParams(-1, -1));
            view.addView(webImageView, 0);
            ViewHolder viewHolder = new ViewHolder();
            viewHolder.webImageView = webImageView;
            view.setTag(viewHolder);
        } else {
            webImageView = ((ViewHolder) view.getTag()).webImageView;
        }
        webImageView.reset();
        webImageView.setImageUrl(str);
        webImageView.loadImage();
        onGetView(i, view, viewGroup);
        return view;
    }

    protected void onGetView(int i, View view, ViewGroup viewGroup) {
    }

    public void setImageUrls(List<String> list) {
        this.imageUrls = list;
    }

    public void setProgressDrawable(Drawable drawable) {
        this.progressDrawable = drawable;
    }
}
