package net.gogame.chat;

import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ProgressBar;
import com.facebook.share.internal.ShareConstants;
import com.squareup.picasso.Picasso;
import com.squareup.picasso.Picasso.LoadedFrom;
import com.squareup.picasso.Target;
import com.zopim.android.sdk.C0785R;

public class ImageViewFragment extends Fragment {
    private Target target;

    @Nullable
    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        View inflate = layoutInflater.inflate(C0785R.layout.net_gogame_chat_fragment_image_view, viewGroup, false);
        ZoomableImageView zoomableImageView = (ZoomableImageView) inflate.findViewById(C0785R.id.imageView);
        final ProgressBar progressBar = (ProgressBar) inflate.findViewById(C0785R.id.progressBar);
        DisplayUtils.hideKeyboard(getActivity(), zoomableImageView.getWindowToken());
        Uri uri = null;
        if (!(getArguments() == null || getArguments().getString(ShareConstants.MEDIA_URI) == null)) {
            uri = Uri.parse(getArguments().getString(ShareConstants.MEDIA_URI));
        }
        if (uri != null) {
            this.target = new ZoomableImageViewTarget(zoomableImageView) {
                public void onBitmapLoaded(Bitmap bitmap, LoadedFrom loadedFrom) {
                    progressBar.setVisibility(8);
                    super.onBitmapLoaded(bitmap, loadedFrom);
                }
            };
            Picasso.with(getActivity()).load(uri).into(this.target);
        }
        return inflate;
    }
}
