package net.gogame.gowrap.p019ui;

import android.app.Dialog;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.view.View.OnClickListener;
import android.widget.AdapterView;
import android.widget.AdapterView.OnItemClickListener;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.ProgressBar;
import java.util.List;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.p019ui.v2017_1.CommunityFragment;
import net.gogame.gowrap.p019ui.v2017_1.ContactSupportFragment;
import net.gogame.gowrap.p019ui.v2017_1.SupportFragment;
import net.gogame.gowrap.support.BuildInfo;
import net.gogame.gowrap.support.LocaleDescriptor;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.MainActivity */
public class MainActivity extends AbstractMainActivity {
    private View navbar;
    private ProgressBar progressIndicator;

    /* access modifiers changed from: protected */
    public void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setContentView(C1423R.C1425layout.net_gogame_gowrap_main_ui);
        this.navbar = findViewById(C1423R.C1424id.net_gogame_gowrap_navbar);
        this.navbar.setOnClickListener(new OnClickListener() {
            private int clicks = 0;

            public void onClick(View view) {
                this.clicks++;
                if (this.clicks >= 10) {
                    BuildInfo.showBuildInfoDialog(view.getContext());
                }
            }
        });
        findViewById(C1423R.C1424id.net_gogame_gowrap_back_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                MainActivity.this.onBackPressed();
            }
        });
        findViewById(C1423R.C1424id.net_gogame_gowrap_info_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                MainActivity.this.showInitialFragment();
            }
        });
        View findViewById = findViewById(C1423R.C1424id.net_gogame_gowrap_language_button);
        findViewById.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                MainActivity.this.showLanguageMenu();
            }
        });
        if (canShowLanguageMenu()) {
            findViewById.setVisibility(0);
        } else {
            findViewById.setVisibility(8);
        }
        findViewById(C1423R.C1424id.net_gogame_gowrap_community_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (!(MainActivity.this.getActiveFragment() instanceof CommunityFragment)) {
                    MainActivity.this.pushFragment(new CommunityFragment());
                }
            }
        });
        findViewById(C1423R.C1424id.net_gogame_gowrap_help_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (!(MainActivity.this.getActiveFragment() instanceof SupportFragment)) {
                    MainActivity.this.pushFragment(new SupportFragment());
                }
            }
        });
        findViewById(C1423R.C1424id.net_gogame_gowrap_contact_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (!(MainActivity.this.getActiveFragment() instanceof ContactSupportFragment)) {
                    MainActivity.this.pushFragment(new ContactSupportFragment());
                }
            }
        });
        findViewById(C1423R.C1424id.net_gogame_gowrap_offers_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (GoWrapImpl.INSTANCE.hasOffers()) {
                    GoWrapImpl.INSTANCE.showOffers();
                }
            }
        });
        findViewById(C1423R.C1424id.net_gogame_gowrap_close_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                MainActivity.this.finish();
            }
        });
        showInitialFragment();
    }

    /* access modifiers changed from: protected */
    public int getFragmentContainerViewId() {
        return C1423R.C1424id.net_gogame_gowrap_main_fragment_container;
    }

    /* access modifiers changed from: protected */
    public void onEnterFullscreen() {
        this.navbar.setVisibility(8);
    }

    /* access modifiers changed from: protected */
    public void onExitFullscreen() {
        this.navbar.setVisibility(0);
    }

    /* access modifiers changed from: protected */
    public void enableOffers() {
        Log.v(Constants.TAG, "Offers enabled");
        findViewById(C1423R.C1424id.net_gogame_gowrap_offers_button).setVisibility(0);
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

    /* access modifiers changed from: private */
    public void showLanguageMenu() {
        if (canShowLanguageMenu()) {
            String currentLocale = Wrapper.INSTANCE.getCurrentLocale(this);
            List supportedLocaleDescriptors = this.localeManager.getSupportedLocaleDescriptors();
            final int i = 0;
            while (true) {
                if (i >= supportedLocaleDescriptors.size()) {
                    i = -1;
                    break;
                } else if (StringUtils.isEquals(currentLocale, ((LocaleDescriptor) supportedLocaleDescriptors.get(i)).getId())) {
                    break;
                } else {
                    i++;
                }
            }
            final ArrayAdapter arrayAdapter = new ArrayAdapter(this, C1423R.C1425layout.net_gogame_gowrap_default_listview_item, C1423R.C1424id.net_gogame_gowrap_text_view, this.localeManager.getSupportedLocaleDescriptors());
            View inflate = getLayoutInflater().inflate(C1423R.C1425layout.net_gogame_gowrap_default_listview, null, false);
            ListView listView = (ListView) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_list_view);
            listView.setAdapter(arrayAdapter);
            if (i >= 0) {
                listView.setItemChecked(i, true);
            }
            final Dialog dialog = new Dialog(this, C1423R.style.net_gogame_gowrap_dialog);
            dialog.setCanceledOnTouchOutside(true);
            dialog.setContentView(inflate);
            listView.setOnItemClickListener(new OnItemClickListener() {
                public void onItemClick(AdapterView<?> adapterView, View view, int i, long j) {
                    if (i != i) {
                        MainActivity.this.localeManager.setLocale(((LocaleDescriptor) arrayAdapter.getItem(i)).getId());
                        MainActivity.this.recreate();
                    }
                    dialog.dismiss();
                }
            });
            dialog.show();
        }
    }
}
