package com.facebook.unity;

import android.net.Uri;
import android.os.Bundle;
import com.facebook.share.internal.ShareFeedContent.Builder;
import com.facebook.share.model.ShareLinkContent;
import com.facebook.share.widget.ShareDialog.Mode;

class FBDialogUtils {
    FBDialogUtils() {
    }

    public static Builder createFeedContentBuilder(Bundle bundle) {
        Builder builder = new Builder();
        if (bundle.containsKey("toId")) {
            builder.setToId(bundle.getString("toId"));
        }
        if (bundle.containsKey("link")) {
            builder.setLink(bundle.getString("link"));
        }
        if (bundle.containsKey("linkName")) {
            builder.setLinkName(bundle.getString("linkName"));
        }
        if (bundle.containsKey("linkCaption")) {
            builder.setLinkCaption(bundle.getString("linkCaption"));
        }
        if (bundle.containsKey("linkDescription")) {
            builder.setLinkDescription(bundle.getString("linkDescription"));
        }
        if (bundle.containsKey("picture")) {
            builder.setPicture(bundle.getString("picture"));
        }
        if (bundle.containsKey("mediaSource")) {
            builder.setMediaSource(bundle.getString("mediaSource"));
        }
        return builder;
    }

    public static ShareLinkContent.Builder createShareContentBuilder(Bundle bundle) {
        ShareLinkContent.Builder builder = new ShareLinkContent.Builder();
        if (bundle.containsKey("content_title")) {
            builder.setContentTitle(bundle.getString("content_title"));
        }
        if (bundle.containsKey("content_description")) {
            builder.setContentDescription(bundle.getString("content_description"));
        }
        if (bundle.containsKey("content_url")) {
            builder.setContentUrl(Uri.parse(bundle.getString("content_url")));
        }
        if (bundle.containsKey("photo_url")) {
            builder.setImageUrl(Uri.parse(bundle.getString("photo_url")));
        }
        return builder;
    }

    public static Mode intToMode(int i) {
        switch (i) {
            case 0:
                return Mode.AUTOMATIC;
            case 1:
                return Mode.NATIVE;
            case 2:
                return Mode.WEB;
            case 3:
                return Mode.FEED;
            default:
                return null;
        }
    }
}
