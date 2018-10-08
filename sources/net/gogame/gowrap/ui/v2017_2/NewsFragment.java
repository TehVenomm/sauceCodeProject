package net.gogame.gowrap.ui.v2017_2;

import android.app.Fragment;
import android.graphics.drawable.Drawable;
import android.net.Uri;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.os.Parcelable;
import android.util.JsonReader;
import android.util.Log;
import android.view.GestureDetector;
import android.view.GestureDetector.SimpleOnGestureListener;
import android.view.LayoutInflater;
import android.view.MotionEvent;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.View.OnTouchListener;
import android.view.ViewGroup;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ImageSwitcher;
import android.widget.ImageView;
import android.widget.ListView;
import android.widget.ProgressBar;
import android.widget.TextView;
import android.widget.ViewSwitcher.ViewFactory;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.Reader;
import java.net.URL;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Timer;
import java.util.TimerTask;
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.core.CoreSupport;
import net.gogame.gowrap.io.utils.IOUtils;
import net.gogame.gowrap.model.news.Article;
import net.gogame.gowrap.model.news.Banner;
import net.gogame.gowrap.model.news.NewsFeed;
import net.gogame.gowrap.support.DownloadManager.DownloadResult;
import net.gogame.gowrap.support.DownloadManager.Request.Builder;
import net.gogame.gowrap.support.DownloadManager.Target;
import net.gogame.gowrap.support.DownloadUtils;
import net.gogame.gowrap.support.DownloadUtils.Callback;
import net.gogame.gowrap.support.DownloadUtils.FileTarget;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.ui.UIContext;
import net.gogame.gowrap.ui.download.DownloadResultSource;
import net.gogame.gowrap.ui.utils.ExternalAppLauncher;
import net.gogame.gowrap.ui.utils.ImageUtils;

public class NewsFragment extends Fragment {
    private static final long BANNER_AUTOROTATE_PERIOD = 4000;
    private static final String KEY_BANNERS = "banners";
    private static final String KEY_BANNER_VIEW = "bannerView";
    private static final String KEY_LIST_ADAPTER = "listAdapter";
    private static final String KEY_LIST_VIEW = "listView";
    private TextView bannerPeriodTextView;
    private ImageSwitcher bannerView;
    private ArrayList<Banner> banners;
    private int currentBannerIndex;
    private final DateFormat dateTimeFormat = new SimpleDateFormat("d/M/y HH:mm");
    private NewsListAdapter listAdapter;
    private ListView listView;
    private ProgressBar progressBar;
    private boolean reverseBannerSlideDirection = false;
    private Bundle savedInstanceState;
    private Timer timer;
    private UIContext uiContext;

    /* renamed from: net.gogame.gowrap.ui.v2017_2.NewsFragment$1 */
    class C15461 implements OnItemClickListener {
        C15461() {
        }

        public void onItemClick(AdapterView<?> adapterView, View view, int i, long j) {
            Article article = (Article) NewsFragment.this.listAdapter.getItem(i);
            if (NewsFragment.this.uiContext != null && article != null) {
                NewsFragment.this.listAdapter.markAsRead(article);
                NewsFragment.this.uiContext.pushFragment(NewsArticleFragment.create(article));
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_2.NewsFragment$3 */
    class C15483 implements OnClickListener {
        C15483() {
        }

        public void onClick(View view) {
            if (NewsFragment.this.banners != null && !NewsFragment.this.banners.isEmpty() && NewsFragment.this.currentBannerIndex < NewsFragment.this.banners.size()) {
                Banner banner = (Banner) NewsFragment.this.banners.get(NewsFragment.this.currentBannerIndex);
                if (banner != null) {
                    NewsFragment.this.launchUrl(banner.getLink());
                }
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_2.NewsFragment$4 */
    class C15504 implements OnTouchListener {
        private final GestureDetector gestureDetector = new GestureDetector(NewsFragment.this.getActivity(), new C15491());

        /* renamed from: net.gogame.gowrap.ui.v2017_2.NewsFragment$4$1 */
        class C15491 extends SimpleOnGestureListener {
            private final int SWIPE_MAX_OFF_PATH = 100;
            private final int SWIPE_MIN_DISTANCE = 100;
            private final int SWIPE_THRESHOLD_VELOCITY = 200;

            C15491() {
            }

            public boolean onFling(MotionEvent motionEvent, MotionEvent motionEvent2, float f, float f2) {
                int x = (int) (motionEvent2.getX() - motionEvent.getX());
                if (Math.abs((int) (motionEvent2.getY() - motionEvent.getY())) > 100 || Math.abs(x) < 100 || Math.abs(f) < 200.0f) {
                    return false;
                }
                if (x > 0) {
                    NewsFragment.this.reverseBannerSlideDirection = true;
                    NewsFragment.this.advanceBanner();
                    return true;
                } else if (x >= 0) {
                    return false;
                } else {
                    NewsFragment.this.reverseBannerSlideDirection = false;
                    NewsFragment.this.advanceBanner();
                    return true;
                }
            }

            public boolean onDown(MotionEvent motionEvent) {
                return true;
            }

            public boolean onSingleTapConfirmed(MotionEvent motionEvent) {
                if (NewsFragment.this.banners != null) {
                    Banner banner = (Banner) NewsFragment.this.banners.get(NewsFragment.this.currentBannerIndex);
                    if (banner != null) {
                        NewsFragment.this.launchUrl(banner.getLink());
                    }
                }
                return true;
            }
        }

        C15504() {
        }

        public boolean onTouch(View view, MotionEvent motionEvent) {
            return this.gestureDetector.onTouchEvent(motionEvent);
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_2.NewsFragment$7 */
    class C15577 implements Target {
        C15577() {
        }

        public void onDownloadStarted(Drawable drawable) {
        }

        public void onDownloadSucceeded(DownloadResult downloadResult) {
            try {
                NewsFragment.this.bannerView.setImageDrawable(ImageUtils.getSampledBitmapDrawable(NewsFragment.this.bannerView.getContext(), new DownloadResultSource(downloadResult), Integer.valueOf(NewsFragment.this.bannerView.getWidth()), Integer.valueOf(NewsFragment.this.bannerView.getHeight())));
            } catch (Throwable th) {
                Log.e(Constants.TAG, "Exception", th);
            }
        }

        public void onDownloadFailed(Drawable drawable) {
        }
    }

    private class BannerTimerTask extends TimerTask {
        private BannerTimerTask() {
        }

        public void run() {
            NewsFragment.this.advanceBanner();
        }
    }

    private Parcelable getParcelable(String str) {
        if (this.savedInstanceState == null || str == null) {
            return null;
        }
        return this.savedInstanceState.getParcelable(str);
    }

    public View onCreateView(final LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        if (getActivity() instanceof UIContext) {
            this.uiContext = (UIContext) getActivity();
        }
        View inflate = layoutInflater.inflate(C1426R.layout.net_gogame_gowrap_v2017_2_fragment_news, viewGroup, false);
        this.progressBar = (ProgressBar) inflate.findViewById(C1426R.id.net_gogame_gowrap_progress_indicator);
        this.listAdapter = new NewsListAdapter(getActivity());
        this.listAdapter.onRestoreInstanceState(getParcelable(KEY_LIST_ADAPTER));
        this.listView = (ListView) inflate.findViewById(C1426R.id.net_gogame_gowrap_news_listview);
        this.listView.setAdapter(this.listAdapter);
        this.listView.setOnItemClickListener(new C15461());
        this.bannerView = (ImageSwitcher) inflate.findViewById(C1426R.id.net_gogame_gowrap_news_banners);
        this.bannerView.setFactory(new ViewFactory() {
            public View makeView() {
                ImageView imageView = (ImageView) layoutInflater.inflate(C1426R.layout.net_gogame_gowrap_v2017_2_fragment_news_banner, NewsFragment.this.bannerView, false);
                if (VERSION.SDK_INT >= 21) {
                    imageView.setClipToOutline(true);
                }
                return imageView;
            }
        });
        this.bannerView.setAnimateFirstView(false);
        this.bannerView.setOnClickListener(new C15483());
        this.bannerView.setOnTouchListener(new C15504());
        this.bannerPeriodTextView = (TextView) inflate.findViewById(C1426R.id.net_gogame_gowrap_news_banner_period);
        if (this.savedInstanceState != null) {
            if (getParcelable(KEY_LIST_VIEW) != null) {
                this.listView.onRestoreInstanceState(getParcelable(KEY_LIST_VIEW));
            }
            if (getParcelable(KEY_BANNER_VIEW) != null) {
                this.banners = (ArrayList) ((Bundle) getParcelable(KEY_BANNER_VIEW)).getSerializable(KEY_BANNERS);
                showBanner(0);
            }
        } else if (CoreSupport.INSTANCE.getAppId() != null) {
            try {
                URL url = new URL("http://gw-sites.gogame.net/news/" + CoreSupport.INSTANCE.getAppId() + "/news_android.json");
                final File file = new File(getActivity().getCacheDir(), "net/gogame/gowrap/news.json");
                file.getParentFile().mkdirs();
                if (this.progressBar != null) {
                    this.progressBar.setVisibility(0);
                }
                DownloadUtils.download(getActivity(), url, new FileTarget(file), file.isFile(), new Callback() {

                    /* renamed from: net.gogame.gowrap.ui.v2017_2.NewsFragment$5$1 */
                    class C15511 implements Runnable {
                        C15511() {
                        }

                        public void run() {
                            C15555.this.hideProgressBar();
                        }
                    }

                    /* renamed from: net.gogame.gowrap.ui.v2017_2.NewsFragment$5$3 */
                    class C15533 implements Runnable {
                        C15533() {
                        }

                        public void run() {
                            C15555.this.hideProgressBar();
                            C15555.this.showError();
                        }
                    }

                    /* renamed from: net.gogame.gowrap.ui.v2017_2.NewsFragment$5$4 */
                    class C15544 implements Runnable {
                        C15544() {
                        }

                        public void run() {
                            NewsFragment.this.getView().findViewById(C1426R.id.net_gogame_gowrap_error_container).setVisibility(0);
                        }
                    }

                    private void hideProgressBar() {
                        if (NewsFragment.this.progressBar != null) {
                            NewsFragment.this.progressBar.setVisibility(8);
                        }
                    }

                    private NewsFeed readFeed(File file) throws IOException {
                        JsonReader jsonReader;
                        InputStream fileInputStream = new FileInputStream(file);
                        try {
                            Reader inputStreamReader = new InputStreamReader(fileInputStream, "UTF-8");
                            try {
                                jsonReader = new JsonReader(inputStreamReader);
                                NewsFeed newsFeed = new NewsFeed(jsonReader);
                                jsonReader.close();
                                IOUtils.closeQuietly(inputStreamReader);
                                return newsFeed;
                            } catch (Throwable th) {
                                IOUtils.closeQuietly(inputStreamReader);
                            }
                        } finally {
                            IOUtils.closeQuietly(fileInputStream);
                        }
                    }

                    public void onDownloadSucceeded() {
                        if (NewsFragment.this.getActivity() != null) {
                            try {
                                NewsFragment.this.getActivity().runOnUiThread(new C15511());
                                final NewsFeed readFeed = readFeed(file);
                                NewsFragment.this.getActivity().runOnUiThread(new Runnable() {
                                    public void run() {
                                        NewsFragment.this.updateUI(readFeed);
                                    }
                                });
                            } catch (Throwable th) {
                                Log.e(Constants.TAG, "Exception", th);
                                showError();
                            }
                        }
                    }

                    public void onDownloadFailed() {
                        if (NewsFragment.this.getActivity() != null) {
                            try {
                                NewsFragment.this.getActivity().runOnUiThread(new C15533());
                            } catch (Throwable e) {
                                Log.e(Constants.TAG, "Exception", e);
                                showError();
                            }
                        }
                    }

                    private void showError() {
                        if (NewsFragment.this.getView() != null) {
                            NewsFragment.this.getActivity().runOnUiThread(new C15544());
                        }
                    }
                });
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
        return inflate;
    }

    private void advanceBanner() {
        if (this.reverseBannerSlideDirection) {
            showPreviousBanner();
        } else {
            showNextBanner();
        }
    }

    private void showNextBanner() {
        if (this.banners != null && !this.banners.isEmpty()) {
            int size = (this.currentBannerIndex + 1) % this.banners.size();
            if (size != this.currentBannerIndex) {
                Animation loadAnimation = AnimationUtils.loadAnimation(getActivity(), C1426R.anim.net_gogame_gowrap_slide_in_right);
                Animation loadAnimation2 = AnimationUtils.loadAnimation(getActivity(), C1426R.anim.net_gogame_gowrap_slide_out_left);
                this.bannerView.setInAnimation(loadAnimation);
                this.bannerView.setOutAnimation(loadAnimation2);
                showBanner(size);
            }
        }
    }

    private void showPreviousBanner() {
        if (this.banners != null && !this.banners.isEmpty()) {
            int size = ((this.currentBannerIndex + this.banners.size()) - 1) % this.banners.size();
            if (size != this.currentBannerIndex) {
                Animation loadAnimation = AnimationUtils.loadAnimation(getActivity(), C1426R.anim.net_gogame_gowrap_slide_in_left);
                Animation loadAnimation2 = AnimationUtils.loadAnimation(getActivity(), C1426R.anim.net_gogame_gowrap_slide_out_right);
                this.bannerView.setInAnimation(loadAnimation);
                this.bannerView.setOutAnimation(loadAnimation2);
                showBanner(size);
            }
        }
    }

    private void showBanner(int i) {
        if (this.banners != null && i < this.banners.size()) {
            doShowBanner((Banner) this.banners.get(i));
            this.currentBannerIndex = i;
        }
    }

    private void doShowBanner(final Banner banner) {
        if (this.uiContext != null && banner != null) {
            getActivity().runOnUiThread(new Runnable() {
                public void run() {
                    if (banner.getStartDateTime() != null && banner.getEndDateTime() != null) {
                        NewsFragment.this.bannerPeriodTextView.setText(NewsFragment.this.getString(C1426R.string.net_gogame_gowrap_news_banner_time_period_format, new Object[]{NewsFragment.this.dateTimeFormat.format(new Date(banner.getStartDateTime().longValue())), NewsFragment.this.dateTimeFormat.format(new Date(banner.getEndDateTime().longValue()))}));
                    } else if (banner.getStartDateTime() != null && banner.getEndDateTime() == null) {
                        NewsFragment.this.bannerPeriodTextView.setText(NewsFragment.this.getString(C1426R.string.net_gogame_gowrap_news_banner_time_period_from_format, new Object[]{NewsFragment.this.dateTimeFormat.format(new Date(banner.getStartDateTime().longValue()))}));
                    } else if (banner.getStartDateTime() != null || banner.getEndDateTime() == null) {
                        NewsFragment.this.bannerPeriodTextView.setText(null);
                    } else {
                        NewsFragment.this.bannerPeriodTextView.setText(NewsFragment.this.getString(C1426R.string.net_gogame_gowrap_news_banner_time_period_until_format, new Object[]{NewsFragment.this.dateTimeFormat.format(new Date(banner.getEndDateTime().longValue()))}));
                    }
                }
            });
            this.uiContext.getDownloadManager().download(Builder.newBuilder(banner.getImageUrl()).into(new C15577()));
        }
    }

    private boolean isInPeriod(long j, Long l, Long l2) {
        return (l == null || j >= l.longValue()) && (l2 == null || j <= l2.longValue());
    }

    private void updateUI(NewsFeed newsFeed) {
        if (newsFeed == null) {
            this.bannerView.setImageDrawable(null);
            this.listAdapter.setElements(null);
            return;
        }
        long currentTimeMillis = System.currentTimeMillis();
        ArrayList arrayList = new ArrayList();
        if (newsFeed.getBanners() != null) {
            for (Banner banner : newsFeed.getBanners()) {
                if (banner != null && isInPeriod(currentTimeMillis, banner.getStartDateTime(), banner.getEndDateTime())) {
                    arrayList.add(banner);
                }
            }
        }
        this.banners = arrayList;
        List arrayList2 = new ArrayList();
        if (newsFeed.getArticles() != null) {
            for (Article article : newsFeed.getArticles()) {
                if (article != null && isInPeriod(currentTimeMillis, article.getStartDateTime(), article.getEndDateTime())) {
                    arrayList2.add(article);
                }
            }
        }
        this.listAdapter.setElements(arrayList2);
        showBanner(0);
    }

    public void onResume() {
        super.onResume();
        if (this.timer == null) {
            this.timer = new Timer();
            this.timer.schedule(new BannerTimerTask(), BANNER_AUTOROTATE_PERIOD, BANNER_AUTOROTATE_PERIOD);
        }
    }

    public void onPause() {
        super.onPause();
        if (this.timer != null) {
            this.timer.cancel();
            this.timer = null;
        }
        Bundle bundle = new Bundle();
        if (this.listAdapter != null) {
            bundle.putParcelable(KEY_LIST_ADAPTER, this.listAdapter.onSaveInstanceState());
        }
        if (this.listView != null) {
            bundle.putParcelable(KEY_LIST_VIEW, this.listView.onSaveInstanceState());
        }
        Parcelable bundle2 = new Bundle();
        bundle2.putSerializable(KEY_BANNERS, this.banners);
        bundle.putParcelable(KEY_BANNER_VIEW, bundle2);
        this.savedInstanceState = bundle;
    }

    private void launchUrl(String str) {
        if (str != null) {
            try {
                Uri parse = Uri.parse(str);
                if (StringUtils.isEquals(parse.getScheme(), "article")) {
                    try {
                        long parseLong = Long.parseLong(parse.getSchemeSpecificPart());
                        int i = 0;
                        while (i < this.listAdapter.getCount()) {
                            Article article = (Article) this.listAdapter.getItem(i);
                            if (article == null || article.getId() != parseLong || this.uiContext == null) {
                                i++;
                            } else {
                                this.uiContext.pushFragment(NewsArticleFragment.create(article));
                                return;
                            }
                        }
                    } catch (Exception e) {
                    }
                } else if (!GoWrapImpl.INSTANCE.handleCustomUri(str)) {
                    ExternalAppLauncher.openUrlInExternalBrowser(getActivity(), str);
                }
            } catch (Throwable e2) {
                Log.e(Constants.TAG, "Exception", e2);
            }
        }
    }
}
