package net.gogame.gowrap.ui.v2017_1;

import android.app.Fragment;
import android.os.Bundle;
import android.util.JsonReader;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import android.view.ViewGroup.LayoutParams;
import android.widget.ProgressBar;
import android.widget.ScrollView;
import com.facebook.share.internal.ShareConstants;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.net.URL;
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.integrations.core.CoreSupport;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.io.utils.IOUtils;
import net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration;
import net.gogame.gowrap.model.feed.Feed;
import net.gogame.gowrap.model.feed.Feed.Item;
import net.gogame.gowrap.support.DownloadManager.Listener;
import net.gogame.gowrap.support.DownloadManager.Request.Builder;
import net.gogame.gowrap.support.DownloadUtils;
import net.gogame.gowrap.support.DownloadUtils.Callback;
import net.gogame.gowrap.support.DownloadUtils.FileTarget;
import net.gogame.gowrap.support.JSONUtils;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.ui.UIContext;
import net.gogame.gowrap.ui.layout.CustomGridLayout;
import net.gogame.gowrap.ui.utils.ExternalAppLauncher;
import net.gogame.gowrap.ui.utils.ShareHelper;

public class NewsFeedFragment extends Fragment {
    private static final String CUSTOM_GRID_LAYOUT_BUNDLE_NAME = "customGridLayout";
    private static final String CUSTOM_GRID_LAYOUT_BUNDLE_PROPERTY_NAME_CHILD_COUNT = "childCount";
    private static final int DEFAULT_PAGE_SIZE = 2;
    private static final boolean LAUNCH_URL_EXTERNALLY = false;
    private static final String SCROLL_VIEW_BUNDLE_NAME = "scrollView";
    private static final String SCROLL_VIEW_BUNDLE_PROPERTY_NAME_SCROLL_X = "scrollX";
    private static final String SCROLL_VIEW_BUNDLE_PROPERTY_NAME_SCROLL_Y = "scrollY";
    private static final boolean VIDEO_ENABLED = false;
    private CustomGridLayout customGridLayout;
    private Listener downloadManagerListener = new C15101();
    private boolean downloadingFeed;
    private Feed feed;
    private View moreButton;
    private int pageSize = 2;
    private ProgressBar progressBar;
    private ProgressBar progressBar2;
    private Bundle savedInstanceState;
    private ScrollView scrollView;
    private UIContext uiContext;
    private boolean viewExists = false;
    private View visitSiteForMoreButton;

    /* renamed from: net.gogame.gowrap.ui.v2017_1.NewsFeedFragment$1 */
    class C15101 implements Listener {
        C15101() {
        }

        public void onDownloadsStarted() {
            NewsFeedFragment.this.updateProgress();
        }

        public void onDownloadsFinished() {
            NewsFeedFragment.this.updateProgress();
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_1.NewsFeedFragment$3 */
    class C15163 implements OnClickListener {
        C15163() {
        }

        public void onClick(View view) {
            NewsFeedFragment.this.appendItems(NewsFeedFragment.this.pageSize);
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_1.NewsFeedFragment$4 */
    class C15174 implements OnClickListener {
        C15174() {
        }

        public void onClick(View view) {
            LocaleConfiguration localeConfiguration = Wrapper.INSTANCE.getLocaleConfiguration(NewsFeedFragment.this.getActivity());
            if (localeConfiguration != null && localeConfiguration.getFacebookUrl() != null) {
                ExternalAppLauncher.openUrlInExternalBrowser(NewsFeedFragment.this.getActivity(), localeConfiguration.getFacebookUrl());
            }
        }
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        View inflate = layoutInflater.inflate(C1426R.layout.net_gogame_gowrap_fragment_newsfeed, viewGroup, false);
        this.customGridLayout = (CustomGridLayout) inflate.findViewById(C1426R.id.net_gogame_gowrap_newsfeed);
        this.viewExists = true;
        if (getActivity() instanceof UIContext) {
            this.uiContext = (UIContext) getActivity();
        }
        this.progressBar = (ProgressBar) inflate.findViewById(C1426R.id.net_gogame_gowrap_progressBar);
        this.progressBar2 = (ProgressBar) inflate.findViewById(C1426R.id.net_gogame_gowrap_progressBar2);
        this.scrollView = (ScrollView) inflate.findViewById(C1426R.id.net_gogame_gowrap_newsfeed_scroll_view);
        if (CoreSupport.INSTANCE.getAppId() != null) {
            try {
                URL url = new URL("http://gw-sites.gogame.net/sites/" + CoreSupport.INSTANCE.getAppId() + "/data/feed.json");
                final File file = new File(getActivity().getCacheDir(), "net/gogame/gowrap/feed.json");
                file.getParentFile().mkdirs();
                this.downloadingFeed = true;
                updateProgress();
                DownloadUtils.download(getActivity(), url, new FileTarget(file), file.isFile(), new Callback() {

                    /* renamed from: net.gogame.gowrap.ui.v2017_1.NewsFeedFragment$2$1 */
                    class C15111 implements Runnable {
                        C15111() {
                        }

                        public void run() {
                            NewsFeedFragment.this.updateProgress();
                        }
                    }

                    /* renamed from: net.gogame.gowrap.ui.v2017_1.NewsFeedFragment$2$2 */
                    class C15122 implements Runnable {
                        C15122() {
                        }

                        public void run() {
                            NewsFeedFragment.this.initializeNewsFeed();
                        }
                    }

                    /* renamed from: net.gogame.gowrap.ui.v2017_1.NewsFeedFragment$2$3 */
                    class C15133 implements Runnable {
                        C15133() {
                        }

                        public void run() {
                            NewsFeedFragment.this.updateProgress();
                            NewsFeedFragment.this.uiContext.pushFragment(new CommunityFragment());
                        }
                    }

                    /* renamed from: net.gogame.gowrap.ui.v2017_1.NewsFeedFragment$2$4 */
                    class C15144 implements Runnable {
                        C15144() {
                        }

                        public void run() {
                            NewsFeedFragment.this.updateProgress();
                            NewsFeedFragment.this.uiContext.pushFragment(new CommunityFragment());
                        }
                    }

                    public void onDownloadSucceeded() {
                        NewsFeedFragment.this.downloadingFeed = false;
                        if (NewsFeedFragment.this.getActivity() != null) {
                            try {
                                NewsFeedFragment.this.getActivity().runOnUiThread(new C15111());
                                NewsFeedFragment.this.feed = NewsFeedFragment.this.readFeed(file);
                                NewsFeedFragment.this.getActivity().runOnUiThread(new C15122());
                            } catch (Throwable th) {
                                Log.e(Constants.TAG, "Exception", th);
                                try {
                                    NewsFeedFragment.this.getActivity().runOnUiThread(new C15133());
                                } catch (Throwable th2) {
                                    Log.e(Constants.TAG, "Exception", th2);
                                }
                            }
                        }
                    }

                    public void onDownloadFailed() {
                        NewsFeedFragment.this.downloadingFeed = false;
                        if (NewsFeedFragment.this.getActivity() != null) {
                            try {
                                NewsFeedFragment.this.getActivity().runOnUiThread(new C15144());
                            } catch (Throwable e) {
                                Log.e(Constants.TAG, "Exception", e);
                            }
                        }
                    }
                });
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
        this.moreButton = inflate.findViewById(C1426R.id.net_gogame_gowrap_newsfeed_button_more);
        this.moreButton.setOnClickListener(new C15163());
        this.visitSiteForMoreButton = inflate.findViewById(C1426R.id.net_gogame_gowrap_newsfeed_button_visit_site_for_more);
        this.visitSiteForMoreButton.setOnClickListener(new C15174());
        return inflate;
    }

    public void onDestroyView() {
        this.viewExists = false;
        super.onDestroyView();
    }

    public void onResume() {
        super.onResume();
        if (this.uiContext != null) {
            this.uiContext.getDownloadManager().addListener(this.downloadManagerListener);
        }
    }

    public void onPause() {
        super.onPause();
        if (this.uiContext != null) {
            this.uiContext.getDownloadManager().removeListener(this.downloadManagerListener);
        }
        Bundle bundle = new Bundle();
        if (this.customGridLayout != null) {
            Bundle bundle2 = new Bundle();
            bundle2.putInt(CUSTOM_GRID_LAYOUT_BUNDLE_PROPERTY_NAME_CHILD_COUNT, this.customGridLayout.getChildCount());
            bundle.putBundle(CUSTOM_GRID_LAYOUT_BUNDLE_NAME, bundle2);
        }
        if (this.scrollView != null) {
            bundle2 = new Bundle();
            bundle2.putInt(SCROLL_VIEW_BUNDLE_PROPERTY_NAME_SCROLL_X, this.scrollView.getScrollX());
            bundle2.putInt(SCROLL_VIEW_BUNDLE_PROPERTY_NAME_SCROLL_Y, this.scrollView.getScrollY());
            bundle.putBundle(SCROLL_VIEW_BUNDLE_NAME, bundle2);
        }
        this.savedInstanceState = bundle;
    }

    private void updateProgress() {
        if (this.uiContext != null) {
            boolean z = this.downloadingFeed || this.uiContext.getDownloadManager().isDownloading();
            updateProgress(this.progressBar, z);
            updateProgress(this.progressBar2, z);
        }
    }

    private void updateProgress(ProgressBar progressBar, boolean z) {
        if (progressBar != null) {
            if (z) {
                progressBar.setVisibility(0);
            } else {
                progressBar.setVisibility(8);
            }
        }
    }

    private void initializeNewsFeed() {
        if (!this.viewExists) {
            return;
        }
        if (this.savedInstanceState != null) {
            Bundle bundle = this.savedInstanceState.getBundle(CUSTOM_GRID_LAYOUT_BUNDLE_NAME);
            if (bundle != null) {
                appendItems(bundle.getInt(CUSTOM_GRID_LAYOUT_BUNDLE_PROPERTY_NAME_CHILD_COUNT, this.pageSize));
            } else {
                appendItems(this.pageSize);
            }
            bundle = this.savedInstanceState.getBundle(SCROLL_VIEW_BUNDLE_NAME);
            if (bundle != null) {
                final View childAt = this.scrollView.getChildAt(0);
                if (this.scrollView != null && childAt != null) {
                    final int i = bundle.getInt(SCROLL_VIEW_BUNDLE_PROPERTY_NAME_SCROLL_X, 0);
                    final int i2 = bundle.getInt(SCROLL_VIEW_BUNDLE_PROPERTY_NAME_SCROLL_Y, 0);
                    this.scrollView.postDelayed(new Runnable() {
                        public void run() {
                            if (childAt.getHeight() >= i2) {
                                NewsFeedFragment.this.scrollView.scrollTo(i, i2);
                                return;
                            }
                            NewsFeedFragment.this.scrollView.scrollTo(i, childAt.getHeight());
                            NewsFeedFragment.this.scrollView.postDelayed(this, 1000);
                        }
                    }, 1000);
                    return;
                }
                return;
            }
            return;
        }
        appendItems(this.pageSize);
    }

    private String sanitizeMessage(String str) {
        return str.replaceAll("[\\n\\r\\s]+", " ");
    }

    private Double getAspectRatio(Integer num, Integer num2) {
        if (num == null || num2 == null) {
            return null;
        }
        return Double.valueOf(num.doubleValue() / num2.doubleValue());
    }

    private void appendItems(int i) {
        if (this.feed != null && this.feed.getItems() != null) {
            for (int i2 = 0; i2 < i && this.customGridLayout.getChildCount() < this.feed.getItems().size(); i2++) {
                View photoNewsFeedItemView;
                int childCount = this.customGridLayout.getChildCount();
                final Item item = (Item) this.feed.getItems().get(childCount);
                Double aspectRatio = getAspectRatio(item.getMediaWidth(), item.getMediaHeight());
                try {
                    if (StringUtils.isEquals(item.getType(), ShareConstants.VIDEO_URL)) {
                        photoNewsFeedItemView = new PhotoNewsFeedItemView(getActivity());
                        if (this.uiContext != null) {
                            this.uiContext.getDownloadManager().download(Builder.newBuilder(item.getMediaPreview()).into(photoNewsFeedItemView));
                        }
                    } else if (StringUtils.isEquals(item.getType(), "PHOTO")) {
                        photoNewsFeedItemView = new PhotoNewsFeedItemView(getActivity());
                        if (this.uiContext != null) {
                            this.uiContext.getDownloadManager().download(Builder.newBuilder(item.getMediaSource()).into(photoNewsFeedItemView));
                        }
                    } else {
                    }
                } catch (Throwable e) {
                    Log.e(Constants.TAG, "Exception", e);
                    photoNewsFeedItemView = null;
                }
                if (photoNewsFeedItemView != null) {
                    photoNewsFeedItemView.setTimestamp(item.getCreatedTime());
                    photoNewsFeedItemView.setMessage(sanitizeMessage(item.getMessage()));
                    photoNewsFeedItemView.setButtonImage(getActivity().getResources().getDrawable(C1426R.drawable.net_gogame_gowrap_icon_share));
                    photoNewsFeedItemView.setAspectRatio(aspectRatio.doubleValue());
                    photoNewsFeedItemView.setPosition(childCount);
                    if (!(this.uiContext == null || (item.getLink() == null && item.getArticleLink() == null))) {
                        photoNewsFeedItemView.setOnClickListener(new OnClickListener() {
                            public void onClick(View view) {
                                if (item.getArticleLink() != null) {
                                    NewsFeedFragment.this.uiContext.loadUrl(item.getArticleLink(), false);
                                } else {
                                    NewsFeedFragment.this.uiContext.loadUrl(item.getLink(), false);
                                }
                            }
                        });
                        if (item.getLink() != null) {
                            photoNewsFeedItemView.setButtonOnClickListener(new OnClickListener() {
                                public void onClick(View view) {
                                    ShareHelper.share(NewsFeedFragment.this.getActivity(), item.getLink());
                                }
                            });
                        }
                    }
                    LayoutParams layoutParams = new CustomGridLayout.LayoutParams(-1, -2);
                    layoutParams.gravity = 1;
                    layoutParams.topMargin = 0;
                    layoutParams.bottomMargin = 0;
                    layoutParams.leftMargin = 0;
                    layoutParams.rightMargin = 0;
                    this.customGridLayout.addView(photoNewsFeedItemView, layoutParams);
                }
            }
            if (this.feed.getItems().isEmpty() || this.customGridLayout.getChildCount() >= this.feed.getItems().size()) {
                this.moreButton.setVisibility(8);
                this.moreButton.setSelected(false);
                this.visitSiteForMoreButton.setVisibility(0);
                this.visitSiteForMoreButton.setSelected(true);
                return;
            }
            this.moreButton.setVisibility(0);
            this.moreButton.setSelected(true);
        }
    }

    private Feed readFeed(File file) throws IOException {
        JsonReader jsonReader;
        InputStream fileInputStream = new FileInputStream(file);
        try {
            Reader inputStreamReader = new InputStreamReader(fileInputStream, "UTF-8");
            try {
                jsonReader = new JsonReader(inputStreamReader);
                Feed feed = new Feed(jsonReader);
                JSONUtils.closeQuietly(jsonReader);
                IOUtils.closeQuietly(inputStreamReader);
                return feed;
            } catch (Throwable th) {
                IOUtils.closeQuietly(inputStreamReader);
            }
        } finally {
            IOUtils.closeQuietly(fileInputStream);
        }
    }
}
