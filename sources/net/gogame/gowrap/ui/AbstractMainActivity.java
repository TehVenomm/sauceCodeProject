package net.gogame.gowrap.ui;

import android.app.Activity;
import android.app.Fragment;
import android.app.FragmentManager;
import android.app.FragmentTransaction;
import android.os.Build.VERSION;
import android.os.Bundle;
import android.util.Log;
import android.view.Window;
import android.view.WindowManager.LayoutParams;
import java.io.File;
import java.io.UnsupportedEncodingException;
import java.net.URLEncoder;
import java.util.regex.Pattern;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.GoWrapImpl.Listener;
import net.gogame.gowrap.InternalConstants;
import net.gogame.gowrap.VipStatus;
import net.gogame.gowrap.integrations.core.CoreSupport;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration;
import net.gogame.gowrap.support.DefaultDownloadManager;
import net.gogame.gowrap.support.DiskLruCache;
import net.gogame.gowrap.support.DownloadManager;
import net.gogame.gowrap.support.LocaleManager;
import net.gogame.gowrap.support.NetworkUtils;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.ui.utils.DisplayUtils;
import net.gogame.gowrap.ui.v2017_1.CommunityFragment;
import net.gogame.gowrap.ui.v2017_1.InternalWebViewFragment;
import net.gogame.gowrap.ui.v2017_1.NewsFeedFragment;
import net.gogame.gowrap.ui.v2017_1.SupportFragment;
import net.gogame.gowrap.ui.v2017_1.WebViewFragment;
import net.gogame.gowrap.ui.v2017_2.NewsFragment;

public abstract class AbstractMainActivity extends Activity implements UIContext {
    private static DiskLruCache diskLruCache;
    private static DownloadManager downloadManager;
    private boolean fullscreen = false;
    private final Listener listener = new C14411();
    protected LocaleConfiguration localeConfiguration;
    protected LocaleManager localeManager = null;
    private Integer preFullscreenOrientation = null;

    /* renamed from: net.gogame.gowrap.ui.AbstractMainActivity$1 */
    class C14411 implements Listener {
        C14411() {
        }

        public void onVipStatusUpdated(VipStatus vipStatus) {
            if (AbstractMainActivity.this.isVipChatEnabled(vipStatus)) {
                AbstractMainActivity.this.enableVipChat();
            } else {
                AbstractMainActivity.this.disableVipChat();
            }
        }

        public void onOffersAvailable() {
            AbstractMainActivity.this.enableOffers();
        }
    }

    protected abstract void enableOffers();

    protected abstract int getFragmentContainerViewId();

    protected abstract void onEnterFullscreen();

    protected abstract void onExitFullscreen();

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        this.localeManager = new LocaleManager(this);
        this.localeManager.setLocale(Wrapper.INSTANCE.getCurrentLocale(this));
        try {
            diskLruCache = DiskLruCache.open(new File(getCacheDir(), "net/gogame/gowrap/cache"), 2, 1, 67108864);
        } catch (Throwable e) {
            Log.e(Constants.TAG, "Exception", e);
        }
        downloadManager = new DefaultDownloadManager(this, diskLruCache);
        this.localeConfiguration = Wrapper.INSTANCE.getLocaleConfiguration(getApplicationContext());
        GoWrapImpl.INSTANCE.checkVipStatus(true);
        GoWrapImpl.INSTANCE.onMenuOpened();
    }

    protected void onResume() {
        super.onResume();
        DisplayUtils.lockOrientation(this);
        GoWrapImpl.INSTANCE.addListener(this.listener);
    }

    protected void onPause() {
        super.onPause();
        GoWrapImpl.INSTANCE.removeListener(this.listener);
    }

    protected void onDestroy() {
        super.onDestroy();
        GoWrapImpl.INSTANCE.onMenuClosed();
    }

    public void onBackPressed() {
        if (!goBack()) {
            super.onBackPressed();
        }
    }

    public boolean isVipChatEnabled() {
        return isVipChatEnabled(GoWrapImpl.INSTANCE.getVipStatus());
    }

    public void pushFragment(Fragment fragment) {
        DisplayUtils.hideSoftKeyboard(this);
        String valueOf = String.valueOf(System.currentTimeMillis());
        FragmentTransaction beginTransaction = getFragmentManager().beginTransaction();
        beginTransaction.replace(getFragmentContainerViewId(), fragment, valueOf);
        beginTransaction.addToBackStack(valueOf);
        beginTransaction.commitAllowingStateLoss();
    }

    public void clearFragments() {
        DisplayUtils.hideSoftKeyboard(this);
        getFragmentManager().popBackStack(null, 1);
    }

    public void loadUrl(String str, boolean z) {
        if (z && str != null) {
            String guid = GoWrapImpl.INSTANCE.getGuid();
            if (guid == null) {
                guid = "";
            }
            try {
                str = str.replaceAll(Pattern.quote("${guid}"), URLEncoder.encode(guid, "UTF-8"));
            } catch (UnsupportedEncodingException e) {
            }
        }
        if (!(getActiveFragment() instanceof WebViewContext) || !((WebViewContext) getActiveFragment()).loadUrl(str)) {
            pushFragment(WebViewFragment.newFragmentWithUrl(str));
        }
    }

    public void loadHtml(String str, String str2) {
        if (!(getActiveFragment() instanceof InternalWebViewContext) || !((InternalWebViewContext) getActiveFragment()).loadHtml(str, str2)) {
            pushFragment(InternalWebViewFragment.newFragmentWithHtml(str, str2));
        }
    }

    public void enterFullscreen(Integer num) {
        if (!this.fullscreen) {
            onEnterFullscreen();
            Window window = getWindow();
            LayoutParams attributes = window.getAttributes();
            attributes.flags |= 1024;
            attributes.flags |= 128;
            window.setAttributes(attributes);
            if (VERSION.SDK_INT >= 14) {
                window.getDecorView().setSystemUiVisibility(1);
            }
            if (num != null) {
                int screenOrientation = DisplayUtils.getScreenOrientation(this);
                if (DisplayUtils.getBaseScreenOrientation(screenOrientation) != DisplayUtils.getBaseScreenOrientation(num.intValue())) {
                    this.preFullscreenOrientation = Integer.valueOf(screenOrientation);
                    if (VERSION.SDK_INT < 21) {
                        setRequestedOrientation(num.intValue());
                    }
                }
            }
            this.fullscreen = true;
        }
    }

    public void exitFullscreen() {
        if (this.fullscreen) {
            if (this.preFullscreenOrientation != null) {
                if (VERSION.SDK_INT < 21) {
                    setRequestedOrientation(this.preFullscreenOrientation.intValue());
                }
                this.preFullscreenOrientation = null;
            }
            Window window = getWindow();
            LayoutParams attributes = window.getAttributes();
            attributes.flags &= -1025;
            attributes.flags &= -129;
            window.setAttributes(attributes);
            if (VERSION.SDK_INT >= 14) {
                window.getDecorView().setSystemUiVisibility(0);
            }
            onExitFullscreen();
            this.fullscreen = false;
        }
    }

    public DownloadManager getDownloadManager() {
        return downloadManager;
    }

    protected boolean canShowLanguageMenu() {
        if (this.localeManager == null || this.localeManager.getSupportedLocaleDescriptors() == null || this.localeManager.getSupportedLocaleDescriptors().size() <= 1) {
            return false;
        }
        return true;
    }

    protected Fragment getActiveFragment() {
        FragmentManager fragmentManager = getFragmentManager();
        if (fragmentManager.getBackStackEntryCount() == 0) {
            return null;
        }
        return fragmentManager.findFragmentByTag(fragmentManager.getBackStackEntryAt(fragmentManager.getBackStackEntryCount() - 1).getName());
    }

    protected void showInitialFragment() {
        if (NetworkUtils.isNetworkAvailable(this)) {
            if (CoreSupport.INSTANCE.getAppId() != null) {
                if (isUseNews2017_2()) {
                    if (!(getActiveFragment() instanceof NewsFragment)) {
                        pushFragment(new NewsFragment());
                    }
                } else if (!(getActiveFragment() instanceof NewsFeedFragment)) {
                    pushFragment(new NewsFeedFragment());
                }
            } else if (!(getActiveFragment() instanceof CommunityFragment)) {
                pushFragment(new CommunityFragment());
            }
        } else if (!(getActiveFragment() instanceof SupportFragment)) {
            pushFragment(new SupportFragment());
        }
    }

    protected boolean isUseNews2017_2() {
        return (Wrapper.INSTANCE.getConfiguration() == null || Wrapper.INSTANCE.getConfiguration().getSettings() == null || !StringUtils.isEquals(Wrapper.INSTANCE.getConfiguration().getSettings().getNewsWidgetVersion(), InternalConstants.VERSION_2017_2)) ? false : true;
    }

    private boolean isVipChatEnabled(VipStatus vipStatus) {
        return CoreSupport.INSTANCE.isForceEnableChat() || !(vipStatus == null || !vipStatus.isVip() || vipStatus.isSuspended());
    }

    private void enableVipChat() {
        Log.v(Constants.TAG, "VIP chat enabled");
        Fragment activeFragment = getActiveFragment();
        if (activeFragment instanceof VipListener) {
            ((VipListener) activeFragment).onEnableVipChat();
        }
    }

    private void disableVipChat() {
        Log.v(Constants.TAG, "VIP chat disabled");
        Fragment activeFragment = getActiveFragment();
        if (activeFragment instanceof VipListener) {
            ((VipListener) activeFragment).onDisableVipChat();
        }
    }

    public boolean goBack() {
        DisplayUtils.hideSoftKeyboard(this);
        FragmentManager fragmentManager = getFragmentManager();
        Fragment activeFragment = getActiveFragment();
        if ((activeFragment instanceof BackPressedListener) && ((BackPressedListener) activeFragment).onBackPressed()) {
            return true;
        }
        if (fragmentManager.getBackStackEntryCount() > 1) {
            try {
                fragmentManager.popBackStack();
                return true;
            } catch (Throwable e) {
                Log.e(Constants.TAG, "Exception", e);
            }
        }
        if (fragmentManager.getBackStackEntryCount() != 1) {
            return false;
        }
        finish();
        return true;
    }
}
