package com.facebook.share.widget;

import android.app.Activity;
import android.app.Fragment;
import android.content.Context;
import android.graphics.Bitmap;
import android.net.Uri;
import android.os.Bundle;
import com.facebook.AccessToken;
import com.facebook.FacebookCallback;
import com.facebook.appevents.AppEventsLogger;
import com.facebook.internal.AnalyticsEvents;
import com.facebook.internal.AppCall;
import com.facebook.internal.CallbackManagerImpl;
import com.facebook.internal.CallbackManagerImpl.RequestCodeOffset;
import com.facebook.internal.DialogFeature;
import com.facebook.internal.DialogPresenter;
import com.facebook.internal.DialogPresenter.ParameterProvider;
import com.facebook.internal.FacebookDialogBase;
import com.facebook.internal.FragmentWrapper;
import com.facebook.internal.NativeAppCallAttachmentStore;
import com.facebook.internal.NativeAppCallAttachmentStore.Attachment;
import com.facebook.internal.Utility;
import com.facebook.share.Sharer;
import com.facebook.share.Sharer.Result;
import com.facebook.share.internal.CameraEffectFeature;
import com.facebook.share.internal.LegacyNativeDialogParameters;
import com.facebook.share.internal.NativeDialogParameters;
import com.facebook.share.internal.OpenGraphActionDialogFeature;
import com.facebook.share.internal.ShareContentValidation;
import com.facebook.share.internal.ShareDialogFeature;
import com.facebook.share.internal.ShareFeedContent;
import com.facebook.share.internal.ShareInternalUtility;
import com.facebook.share.internal.ShareStoryFeature;
import com.facebook.share.internal.WebDialogParameters;
import com.facebook.share.model.ShareCameraEffectContent;
import com.facebook.share.model.ShareContent;
import com.facebook.share.model.ShareLinkContent;
import com.facebook.share.model.ShareMediaContent;
import com.facebook.share.model.ShareOpenGraphContent;
import com.facebook.share.model.SharePhoto;
import com.facebook.share.model.SharePhotoContent;
import com.facebook.share.model.SharePhotoContent.Builder;
import com.facebook.share.model.ShareStoryContent;
import com.facebook.share.model.ShareVideoContent;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

public final class ShareDialog extends FacebookDialogBase<ShareContent, Result> implements Sharer {
    private static final int DEFAULT_REQUEST_CODE = RequestCodeOffset.Share.toRequestCode();
    private static final String FEED_DIALOG = "feed";
    private static final String TAG = ShareDialog.class.getSimpleName();
    private static final String WEB_OG_SHARE_DIALOG = "share_open_graph";
    public static final String WEB_SHARE_DIALOG = "share";
    private boolean isAutomaticMode;
    private boolean shouldFailOnDataError;

    private class CameraEffectHandler extends ModeHandler {
        private CameraEffectHandler() {
            super();
        }

        public boolean canShow(ShareContent shareContent, boolean z) {
            return (shareContent instanceof ShareCameraEffectContent) && ShareDialog.canShowNative(shareContent.getClass());
        }

        public AppCall createAppCall(final ShareContent shareContent) {
            ShareContentValidation.validateForNativeShare(shareContent);
            final AppCall createBaseAppCall = ShareDialog.this.createBaseAppCall();
            final boolean shouldFailOnDataError = ShareDialog.this.getShouldFailOnDataError();
            DialogPresenter.setupAppCallForNativeDialog(createBaseAppCall, new ParameterProvider() {
                public Bundle getLegacyParameters() {
                    return LegacyNativeDialogParameters.create(createBaseAppCall.getCallId(), shareContent, shouldFailOnDataError);
                }

                public Bundle getParameters() {
                    return NativeDialogParameters.create(createBaseAppCall.getCallId(), shareContent, shouldFailOnDataError);
                }
            }, ShareDialog.getFeature(shareContent.getClass()));
            return createBaseAppCall;
        }

        public Object getMode() {
            return Mode.NATIVE;
        }
    }

    private class FeedHandler extends ModeHandler {
        private FeedHandler() {
            super();
        }

        public boolean canShow(ShareContent shareContent, boolean z) {
            return (shareContent instanceof ShareLinkContent) || (shareContent instanceof ShareFeedContent);
        }

        public AppCall createAppCall(ShareContent shareContent) {
            Bundle createForFeed;
            ShareDialog.this.logDialogShare(ShareDialog.this.getActivityContext(), shareContent, Mode.FEED);
            AppCall createBaseAppCall = ShareDialog.this.createBaseAppCall();
            if (shareContent instanceof ShareLinkContent) {
                ShareLinkContent shareLinkContent = (ShareLinkContent) shareContent;
                ShareContentValidation.validateForWebShare(shareLinkContent);
                createForFeed = WebDialogParameters.createForFeed(shareLinkContent);
            } else {
                createForFeed = WebDialogParameters.createForFeed((ShareFeedContent) shareContent);
            }
            DialogPresenter.setupAppCallForWebDialog(createBaseAppCall, ShareDialog.FEED_DIALOG, createForFeed);
            return createBaseAppCall;
        }

        public Object getMode() {
            return Mode.FEED;
        }
    }

    public enum Mode {
        AUTOMATIC,
        NATIVE,
        WEB,
        FEED
    }

    private class NativeHandler extends ModeHandler {
        private NativeHandler() {
            super();
        }

        public boolean canShow(ShareContent shareContent, boolean z) {
            boolean z2;
            if (shareContent == null || (shareContent instanceof ShareCameraEffectContent) || (shareContent instanceof ShareStoryContent)) {
                return false;
            }
            if (!z) {
                z2 = shareContent.getShareHashtag() != null ? DialogPresenter.canPresentNativeDialogWithFeature(ShareDialogFeature.HASHTAG) : true;
                if ((shareContent instanceof ShareLinkContent) && !Utility.isNullOrEmpty(((ShareLinkContent) shareContent).getQuote())) {
                    z2 &= DialogPresenter.canPresentNativeDialogWithFeature(ShareDialogFeature.LINK_SHARE_QUOTES);
                }
            } else {
                z2 = true;
            }
            return z2 && ShareDialog.canShowNative(shareContent.getClass());
        }

        public AppCall createAppCall(final ShareContent shareContent) {
            ShareDialog.this.logDialogShare(ShareDialog.this.getActivityContext(), shareContent, Mode.NATIVE);
            ShareContentValidation.validateForNativeShare(shareContent);
            final AppCall createBaseAppCall = ShareDialog.this.createBaseAppCall();
            final boolean shouldFailOnDataError = ShareDialog.this.getShouldFailOnDataError();
            DialogPresenter.setupAppCallForNativeDialog(createBaseAppCall, new ParameterProvider() {
                public Bundle getLegacyParameters() {
                    return LegacyNativeDialogParameters.create(createBaseAppCall.getCallId(), shareContent, shouldFailOnDataError);
                }

                public Bundle getParameters() {
                    return NativeDialogParameters.create(createBaseAppCall.getCallId(), shareContent, shouldFailOnDataError);
                }
            }, ShareDialog.getFeature(shareContent.getClass()));
            return createBaseAppCall;
        }

        public Object getMode() {
            return Mode.NATIVE;
        }
    }

    private class ShareStoryHandler extends ModeHandler {
        private ShareStoryHandler() {
            super();
        }

        public boolean canShow(ShareContent shareContent, boolean z) {
            return (shareContent instanceof ShareStoryContent) && ShareDialog.canShowNative(shareContent.getClass());
        }

        public AppCall createAppCall(final ShareContent shareContent) {
            ShareContentValidation.validateForStoryShare(shareContent);
            final AppCall createBaseAppCall = ShareDialog.this.createBaseAppCall();
            final boolean shouldFailOnDataError = ShareDialog.this.getShouldFailOnDataError();
            DialogPresenter.setupAppCallForNativeDialog(createBaseAppCall, new ParameterProvider() {
                public Bundle getLegacyParameters() {
                    return LegacyNativeDialogParameters.create(createBaseAppCall.getCallId(), shareContent, shouldFailOnDataError);
                }

                public Bundle getParameters() {
                    return NativeDialogParameters.create(createBaseAppCall.getCallId(), shareContent, shouldFailOnDataError);
                }
            }, ShareDialog.getFeature(shareContent.getClass()));
            return createBaseAppCall;
        }

        public Object getMode() {
            return Mode.NATIVE;
        }
    }

    private class WebShareHandler extends ModeHandler {
        private WebShareHandler() {
            super();
        }

        private SharePhotoContent createAndMapAttachments(SharePhotoContent sharePhotoContent, UUID uuid) {
            Builder readFrom = new Builder().readFrom(sharePhotoContent);
            ArrayList arrayList = new ArrayList();
            ArrayList arrayList2 = new ArrayList();
            int i = 0;
            while (true) {
                int i2 = i;
                if (i2 < sharePhotoContent.getPhotos().size()) {
                    SharePhoto sharePhoto = (SharePhoto) sharePhotoContent.getPhotos().get(i2);
                    Bitmap bitmap = sharePhoto.getBitmap();
                    if (bitmap != null) {
                        Attachment createAttachment = NativeAppCallAttachmentStore.createAttachment(uuid, bitmap);
                        sharePhoto = new SharePhoto.Builder().readFrom(sharePhoto).setImageUrl(Uri.parse(createAttachment.getAttachmentUrl())).setBitmap(null).build();
                        arrayList2.add(createAttachment);
                    }
                    arrayList.add(sharePhoto);
                    i = i2 + 1;
                } else {
                    readFrom.setPhotos(arrayList);
                    NativeAppCallAttachmentStore.addAttachments(arrayList2);
                    return readFrom.build();
                }
            }
        }

        private String getActionName(ShareContent shareContent) {
            if ((shareContent instanceof ShareLinkContent) || (shareContent instanceof SharePhotoContent)) {
                return "share";
            }
            if (shareContent instanceof ShareOpenGraphContent) {
                return ShareDialog.WEB_OG_SHARE_DIALOG;
            }
            return null;
        }

        public boolean canShow(ShareContent shareContent, boolean z) {
            return shareContent != null && ShareDialog.canShowWebCheck(shareContent);
        }

        public AppCall createAppCall(ShareContent shareContent) {
            ShareDialog.this.logDialogShare(ShareDialog.this.getActivityContext(), shareContent, Mode.WEB);
            AppCall createBaseAppCall = ShareDialog.this.createBaseAppCall();
            ShareContentValidation.validateForWebShare(shareContent);
            Bundle create = shareContent instanceof ShareLinkContent ? WebDialogParameters.create((ShareLinkContent) shareContent) : shareContent instanceof SharePhotoContent ? WebDialogParameters.create(createAndMapAttachments((SharePhotoContent) shareContent, createBaseAppCall.getCallId())) : WebDialogParameters.create((ShareOpenGraphContent) shareContent);
            DialogPresenter.setupAppCallForWebDialog(createBaseAppCall, getActionName(shareContent), create);
            return createBaseAppCall;
        }

        public Object getMode() {
            return Mode.WEB;
        }
    }

    public ShareDialog(Activity activity) {
        super(activity, DEFAULT_REQUEST_CODE);
        this.shouldFailOnDataError = false;
        this.isAutomaticMode = true;
        ShareInternalUtility.registerStaticShareCallback(DEFAULT_REQUEST_CODE);
    }

    ShareDialog(Activity activity, int i) {
        super(activity, i);
        this.shouldFailOnDataError = false;
        this.isAutomaticMode = true;
        ShareInternalUtility.registerStaticShareCallback(i);
    }

    public ShareDialog(Fragment fragment) {
        this(new FragmentWrapper(fragment));
    }

    ShareDialog(Fragment fragment, int i) {
        this(new FragmentWrapper(fragment), i);
    }

    public ShareDialog(android.support.p000v4.app.Fragment fragment) {
        this(new FragmentWrapper(fragment));
    }

    ShareDialog(android.support.p000v4.app.Fragment fragment, int i) {
        this(new FragmentWrapper(fragment), i);
    }

    private ShareDialog(FragmentWrapper fragmentWrapper) {
        super(fragmentWrapper, DEFAULT_REQUEST_CODE);
        this.shouldFailOnDataError = false;
        this.isAutomaticMode = true;
        ShareInternalUtility.registerStaticShareCallback(DEFAULT_REQUEST_CODE);
    }

    private ShareDialog(FragmentWrapper fragmentWrapper, int i) {
        super(fragmentWrapper, i);
        this.shouldFailOnDataError = false;
        this.isAutomaticMode = true;
        ShareInternalUtility.registerStaticShareCallback(i);
    }

    public static boolean canShow(Class<? extends ShareContent> cls) {
        return canShowWebTypeCheck(cls) || canShowNative(cls);
    }

    /* access modifiers changed from: private */
    public static boolean canShowNative(Class<? extends ShareContent> cls) {
        DialogFeature feature = getFeature(cls);
        return feature != null && DialogPresenter.canPresentNativeDialogWithFeature(feature);
    }

    /* access modifiers changed from: private */
    public static boolean canShowWebCheck(ShareContent shareContent) {
        if (!canShowWebTypeCheck(shareContent.getClass())) {
            return false;
        }
        if (shareContent instanceof ShareOpenGraphContent) {
            try {
                ShareInternalUtility.toJSONObjectForWeb((ShareOpenGraphContent) shareContent);
            } catch (Exception e) {
                Utility.logd(TAG, "canShow returned false because the content of the Opem Graph object can't be shared via the web dialog", e);
                return false;
            }
        }
        return true;
    }

    private static boolean canShowWebTypeCheck(Class<? extends ShareContent> cls) {
        return ShareLinkContent.class.isAssignableFrom(cls) || ShareOpenGraphContent.class.isAssignableFrom(cls) || (SharePhotoContent.class.isAssignableFrom(cls) && AccessToken.isCurrentAccessTokenActive());
    }

    /* access modifiers changed from: private */
    public static DialogFeature getFeature(Class<? extends ShareContent> cls) {
        if (ShareLinkContent.class.isAssignableFrom(cls)) {
            return ShareDialogFeature.SHARE_DIALOG;
        }
        if (SharePhotoContent.class.isAssignableFrom(cls)) {
            return ShareDialogFeature.PHOTOS;
        }
        if (ShareVideoContent.class.isAssignableFrom(cls)) {
            return ShareDialogFeature.VIDEO;
        }
        if (ShareOpenGraphContent.class.isAssignableFrom(cls)) {
            return OpenGraphActionDialogFeature.OG_ACTION_DIALOG;
        }
        if (ShareMediaContent.class.isAssignableFrom(cls)) {
            return ShareDialogFeature.MULTIMEDIA;
        }
        if (ShareCameraEffectContent.class.isAssignableFrom(cls)) {
            return CameraEffectFeature.SHARE_CAMERA_EFFECT;
        }
        if (ShareStoryContent.class.isAssignableFrom(cls)) {
            return ShareStoryFeature.SHARE_STORY_ASSET;
        }
        return null;
    }

    /* access modifiers changed from: private */
    public void logDialogShare(Context context, ShareContent shareContent, Mode mode) {
        String str;
        if (this.isAutomaticMode) {
            mode = Mode.AUTOMATIC;
        }
        switch (mode) {
            case AUTOMATIC:
                str = AnalyticsEvents.PARAMETER_SHARE_DIALOG_SHOW_AUTOMATIC;
                break;
            case WEB:
                str = AnalyticsEvents.PARAMETER_SHARE_DIALOG_SHOW_WEB;
                break;
            case NATIVE:
                str = AnalyticsEvents.PARAMETER_SHARE_DIALOG_SHOW_NATIVE;
                break;
            default:
                str = "unknown";
                break;
        }
        DialogFeature feature = getFeature(shareContent.getClass());
        String str2 = feature == ShareDialogFeature.SHARE_DIALOG ? "status" : feature == ShareDialogFeature.PHOTOS ? AnalyticsEvents.PARAMETER_SHARE_DIALOG_CONTENT_PHOTO : feature == ShareDialogFeature.VIDEO ? "video" : feature == OpenGraphActionDialogFeature.OG_ACTION_DIALOG ? "open_graph" : "unknown";
        AppEventsLogger newLogger = AppEventsLogger.newLogger(context);
        Bundle bundle = new Bundle();
        bundle.putString("fb_share_dialog_show", str);
        bundle.putString(AnalyticsEvents.PARAMETER_SHARE_DIALOG_CONTENT_TYPE, str2);
        newLogger.logSdkEvent("fb_share_dialog_show", null, bundle);
    }

    public static void show(Activity activity, ShareContent shareContent) {
        new ShareDialog(activity).show(shareContent);
    }

    public static void show(Fragment fragment, ShareContent shareContent) {
        show(new FragmentWrapper(fragment), shareContent);
    }

    public static void show(android.support.p000v4.app.Fragment fragment, ShareContent shareContent) {
        show(new FragmentWrapper(fragment), shareContent);
    }

    private static void show(FragmentWrapper fragmentWrapper, ShareContent shareContent) {
        new ShareDialog(fragmentWrapper).show(shareContent);
    }

    /* JADX WARNING: Incorrect type for immutable var: ssa=com.facebook.share.widget.ShareDialog$Mode, code=java.lang.Object, for r3v0, types: [com.facebook.share.widget.ShareDialog$Mode, java.lang.Object] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public boolean canShow(com.facebook.share.model.ShareContent r2, java.lang.Object r3) {
        /*
            r1 = this;
            com.facebook.share.widget.ShareDialog$Mode r0 = com.facebook.share.widget.ShareDialog.Mode.AUTOMATIC
            if (r3 != r0) goto L_0x0006
            java.lang.Object r3 = BASE_AUTOMATIC_MODE
        L_0x0006:
            boolean r0 = r1.canShowImpl(r2, r3)
            return r0
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.share.widget.ShareDialog.canShow(com.facebook.share.model.ShareContent, com.facebook.share.widget.ShareDialog$Mode):boolean");
    }

    /* access modifiers changed from: protected */
    public AppCall createBaseAppCall() {
        return new AppCall(getRequestCode());
    }

    /* access modifiers changed from: protected */
    public List<ModeHandler> getOrderedModeHandlers() {
        ArrayList arrayList = new ArrayList();
        arrayList.add(new NativeHandler());
        arrayList.add(new FeedHandler());
        arrayList.add(new WebShareHandler());
        arrayList.add(new CameraEffectHandler());
        arrayList.add(new ShareStoryHandler());
        return arrayList;
    }

    public boolean getShouldFailOnDataError() {
        return this.shouldFailOnDataError;
    }

    /* access modifiers changed from: protected */
    public void registerCallbackImpl(CallbackManagerImpl callbackManagerImpl, FacebookCallback<Result> facebookCallback) {
        ShareInternalUtility.registerSharerCallback(getRequestCode(), callbackManagerImpl, facebookCallback);
    }

    public void setShouldFailOnDataError(boolean z) {
        this.shouldFailOnDataError = z;
    }

    /* JADX WARNING: Incorrect type for immutable var: ssa=com.facebook.share.widget.ShareDialog$Mode, code=java.lang.Object, for r3v0, types: [com.facebook.share.widget.ShareDialog$Mode, java.lang.Object] */
    /* Code decompiled incorrectly, please refer to instructions dump. */
    public void show(com.facebook.share.model.ShareContent r2, java.lang.Object r3) {
        /*
            r1 = this;
            com.facebook.share.widget.ShareDialog$Mode r0 = com.facebook.share.widget.ShareDialog.Mode.AUTOMATIC
            if (r3 != r0) goto L_0x0011
            r0 = 1
        L_0x0005:
            r1.isAutomaticMode = r0
            boolean r0 = r1.isAutomaticMode
            if (r0 == 0) goto L_0x000d
            java.lang.Object r3 = BASE_AUTOMATIC_MODE
        L_0x000d:
            r1.showImpl(r2, r3)
            return
        L_0x0011:
            r0 = 0
            goto L_0x0005
        */
        throw new UnsupportedOperationException("Method not decompiled: com.facebook.share.widget.ShareDialog.show(com.facebook.share.model.ShareContent, com.facebook.share.widget.ShareDialog$Mode):void");
    }
}
