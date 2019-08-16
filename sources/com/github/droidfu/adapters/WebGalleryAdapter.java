package com.github.droidfu.adapters;

import android.content.Context;
import android.graphics.drawable.Drawable;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.FrameLayout;
import android.widget.FrameLayout.LayoutParams;
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
        /* access modifiers changed from: private */
        public WebImageView webImageView;

        private ViewHolder() {
        }
    }

    public WebGalleryAdapter(Context context2) {
        initialize(context2, null, null, null);
    }

    public WebGalleryAdapter(Context context2, List<String> list) {
        initialize(context2, list, null, null);
    }

    public WebGalleryAdapter(Context context2, List<String> list, int i) {
        initialize(context2, list, context2.getResources().getDrawable(i), null);
    }

    public WebGalleryAdapter(Context context2, List<String> list, int i, int i2) {
        Drawable drawable = null;
        Drawable drawable2 = i == -1 ? null : context2.getResources().getDrawable(i);
        if (i2 != -1) {
            drawable = context2.getResources().getDrawable(i2);
        }
        initialize(context2, list, drawable2, drawable);
    }

    private void initialize(Context context2, List<String> list, Drawable drawable, Drawable drawable2) {
        this.imageUrls = list;
        this.context = context2;
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
        WebImageView access$100;
        String str = (String) getItem(i);
        if (view == 0) {
            access$100 = new WebImageView(this.context, null, this.progressDrawable, this.errorDrawable, false);
            LayoutParams layoutParams = new LayoutParams(-2, -2);
            layoutParams.gravity = 17;
            access$100.setLayoutParams(layoutParams);
            r11 = new FrameLayout(this.context);
            r11.setLayoutParams(new Gallery.LayoutParams(-1, -1));
            r11.addView(access$100, 0);
            ViewHolder viewHolder = new ViewHolder();
            viewHolder.webImageView = access$100;
            r11.setTag(viewHolder);
            r11 = r11;
        } else {
            access$100 = ((ViewHolder) view.getTag()).webImageView;
            r11 = view;
        }
        access$100.reset();
        access$100.setImageUrl(str);
        access$100.loadImage();
        onGetView(i, r11, viewGroup);
        return r11;
    }

    /* access modifiers changed from: protected */
    public void onGetView(int i, View view, ViewGroup viewGroup) {
    }

    public void setImageUrls(List<String> list) {
        this.imageUrls = list;
    }

    public void setProgressDrawable(Drawable drawable) {
        this.progressDrawable = drawable;
    }
}
