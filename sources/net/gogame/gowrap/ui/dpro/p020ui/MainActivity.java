package net.gogame.gowrap.p019ui.dpro.p020ui;

import android.content.Context;
import android.os.Bundle;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.ProgressBar;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration;
import net.gogame.gowrap.p019ui.AbstractMainActivity;
import net.gogame.gowrap.p019ui.dpro.C1452R;
import net.gogame.gowrap.p019ui.dpro.view.CustomTabbelPanel;
import net.gogame.gowrap.p019ui.dpro.view.CustomTabbelPanel.Listener;
import net.gogame.gowrap.p019ui.utils.ExternalAppLauncher;
import net.gogame.gowrap.p019ui.v2017_1.NewsFeedFragment;
import net.gogame.gowrap.p019ui.v2017_2.HelpFragment;
import net.gogame.gowrap.p019ui.v2017_2.NewsFragment;

/* renamed from: net.gogame.gowrap.ui.dpro.ui.MainActivity */
public class MainActivity extends AbstractMainActivity {
    private ProgressBar progressIndicator;

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setContentView(C1452R.C1454layout.net_gogame_gowrap_dpro_main_ui);
        ((CustomTabbelPanel) findViewById(C1452R.C1453id.net_gogame_gowrap_main_tabbed_panel)).setListener(new Listener() {
            private int currentTabIndex = 0;

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
                                throw th;
                            }
                        case 1:
                            MainActivity.this.clearFragments();
                            MainActivity.this.pushFragment(new HelpFragment());
                            break;
                    }
                    this.currentTabIndex = i;
                }
            }
        });
        findViewById(C1452R.C1453id.net_gogame_gowrap_main_sns_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                LocaleConfiguration localeConfiguration = Wrapper.INSTANCE.getLocaleConfiguration((Context) MainActivity.this);
                if (localeConfiguration != null && localeConfiguration.getFacebookUrl() != null) {
                    ExternalAppLauncher.openUrlInExternalBrowser(MainActivity.this, localeConfiguration.getFacebookUrl());
                }
            }
        });
        showNews();
    }

    /* access modifiers changed from: private */
    public void showNews() {
        if (isUseNews2017_2()) {
            pushFragment(new NewsFragment());
        } else {
            pushFragment(new NewsFeedFragment());
        }
    }

    /* access modifiers changed from: protected */
    public int getFragmentContainerViewId() {
        return C1452R.C1453id.net_gogame_gowrap_main_fragment_container;
    }

    /* access modifiers changed from: protected */
    public void onEnterFullscreen() {
    }

    /* access modifiers changed from: protected */
    public void onExitFullscreen() {
    }

    /* access modifiers changed from: protected */
    public void enableOffers() {
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
