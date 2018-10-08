package net.gogame.gowrap.ui.dpro.ui;

import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ProgressBar;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration;
import net.gogame.gowrap.ui.AbstractMainActivity;
import net.gogame.gowrap.ui.dpro.C1471R;
import net.gogame.gowrap.ui.dpro.view.CustomTabbelPanel;
import net.gogame.gowrap.ui.dpro.view.CustomTabbelPanel.Listener;
import net.gogame.gowrap.ui.utils.ExternalAppLauncher;
import net.gogame.gowrap.ui.v2017_1.NewsFeedFragment;
import net.gogame.gowrap.ui.v2017_2.HelpFragment;
import net.gogame.gowrap.ui.v2017_2.NewsFragment;
import net.gogame.gowrap.ui.v2017_2.TipsFragment;

public class MainActivity extends AbstractMainActivity {
    private ProgressBar progressIndicator;

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.MainActivity$1 */
    class C14761 implements Listener {
        private int currentTabIndex = 0;

        C14761() {
        }

        public void onClose() {
            MainActivity.this.finish();
        }

        public void onTabSelected(int i) {
            if (i != this.currentTabIndex) {
                switch (i) {
                    case 0:
                        try {
                            MainActivity.this.clearFragments();
                            MainActivity.this.showNews();
                            break;
                        } catch (Throwable th) {
                            this.currentTabIndex = i;
                        }
                    case 1:
                        MainActivity.this.clearFragments();
                        MainActivity.this.pushFragment(new TipsFragment());
                        break;
                    case 2:
                        MainActivity.this.clearFragments();
                        MainActivity.this.pushFragment(new RankingFragment());
                        break;
                    case 3:
                        MainActivity.this.clearFragments();
                        MainActivity.this.pushFragment(new HelpFragment());
                        break;
                }
                this.currentTabIndex = i;
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.dpro.ui.MainActivity$2 */
    class C14772 implements OnClickListener {
        C14772() {
        }

        public void onClick(View view) {
            LocaleConfiguration localeConfiguration = Wrapper.INSTANCE.getLocaleConfiguration(MainActivity.this);
            if (localeConfiguration != null && localeConfiguration.getFacebookUrl() != null) {
                ExternalAppLauncher.openUrlInExternalBrowser(MainActivity.this, localeConfiguration.getFacebookUrl());
            }
        }
    }

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setContentView(C1471R.layout.net_gogame_gowrap_dpro_main_ui);
        ((CustomTabbelPanel) findViewById(C1471R.id.net_gogame_gowrap_main_tabbed_panel)).setListener(new C14761());
        findViewById(C1471R.id.net_gogame_gowrap_main_sns_button).setOnClickListener(new C14772());
        showNews();
    }

    private void showNews() {
        if (isUseNews2017_2()) {
            pushFragment(new NewsFragment());
        } else {
            pushFragment(new NewsFeedFragment());
        }
    }

    protected int getFragmentContainerViewId() {
        return C1471R.id.net_gogame_gowrap_main_fragment_container;
    }

    protected void onEnterFullscreen() {
    }

    protected void onExitFullscreen() {
    }

    protected void enableOffers() {
    }

    public String getGuid() {
        return GoWrapImpl.INSTANCE.getGuid();
    }

    public void onLoadingStarted() {
        if (this.progressIndicator != null) {
            this.progressIndicator.setVisibility(0);
        }
    }

    public void onLoadingFinished() {
        if (this.progressIndicator != null) {
            this.progressIndicator.setVisibility(8);
        }
    }
}
