package net.gogame.gowrap.ui;

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
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.Constants;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.support.BuildInfo;
import net.gogame.gowrap.support.LocaleDescriptor;
import net.gogame.gowrap.support.StringUtils;
import net.gogame.gowrap.ui.v2017_1.CommunityFragment;
import net.gogame.gowrap.ui.v2017_1.ContactSupportFragment;
import net.gogame.gowrap.ui.v2017_1.SupportFragment;

public class MainActivity extends AbstractMainActivity {
    private View navbar;
    private ProgressBar progressIndicator;

    /* renamed from: net.gogame.gowrap.ui.MainActivity$1 */
    class C14421 implements OnClickListener {
        private int clicks = 0;

        C14421() {
        }

        public void onClick(View view) {
            this.clicks++;
            if (this.clicks >= 10) {
                BuildInfo.showBuildInfoDialog(view.getContext());
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.MainActivity$2 */
    class C14432 implements OnClickListener {
        C14432() {
        }

        public void onClick(View view) {
            MainActivity.this.onBackPressed();
        }
    }

    /* renamed from: net.gogame.gowrap.ui.MainActivity$3 */
    class C14443 implements OnClickListener {
        C14443() {
        }

        public void onClick(View view) {
            MainActivity.this.showInitialFragment();
        }
    }

    /* renamed from: net.gogame.gowrap.ui.MainActivity$4 */
    class C14454 implements OnClickListener {
        C14454() {
        }

        public void onClick(View view) {
            MainActivity.this.showLanguageMenu();
        }
    }

    /* renamed from: net.gogame.gowrap.ui.MainActivity$5 */
    class C14465 implements OnClickListener {
        C14465() {
        }

        public void onClick(View view) {
            if (!(MainActivity.this.getActiveFragment() instanceof CommunityFragment)) {
                MainActivity.this.pushFragment(new CommunityFragment());
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.MainActivity$6 */
    class C14476 implements OnClickListener {
        C14476() {
        }

        public void onClick(View view) {
            if (!(MainActivity.this.getActiveFragment() instanceof SupportFragment)) {
                MainActivity.this.pushFragment(new SupportFragment());
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.MainActivity$7 */
    class C14487 implements OnClickListener {
        C14487() {
        }

        public void onClick(View view) {
            if (!(MainActivity.this.getActiveFragment() instanceof ContactSupportFragment)) {
                MainActivity.this.pushFragment(new ContactSupportFragment());
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.MainActivity$8 */
    class C14498 implements OnClickListener {
        C14498() {
        }

        public void onClick(View view) {
            if (GoWrapImpl.INSTANCE.hasOffers()) {
                GoWrapImpl.INSTANCE.showOffers();
            }
        }
    }

    /* renamed from: net.gogame.gowrap.ui.MainActivity$9 */
    class C14509 implements OnClickListener {
        C14509() {
        }

        public void onClick(View view) {
            MainActivity.this.finish();
        }
    }

    protected void onCreate(Bundle bundle) {
        super.onCreate(bundle);
        setContentView(C1426R.layout.net_gogame_gowrap_main_ui);
        this.navbar = findViewById(C1426R.id.net_gogame_gowrap_navbar);
        this.navbar.setOnClickListener(new C14421());
        findViewById(C1426R.id.net_gogame_gowrap_back_button).setOnClickListener(new C14432());
        findViewById(C1426R.id.net_gogame_gowrap_info_button).setOnClickListener(new C14443());
        View findViewById = findViewById(C1426R.id.net_gogame_gowrap_language_button);
        findViewById.setOnClickListener(new C14454());
        if (canShowLanguageMenu()) {
            findViewById.setVisibility(0);
        } else {
            findViewById.setVisibility(8);
        }
        findViewById(C1426R.id.net_gogame_gowrap_community_button).setOnClickListener(new C14465());
        findViewById(C1426R.id.net_gogame_gowrap_help_button).setOnClickListener(new C14476());
        findViewById(C1426R.id.net_gogame_gowrap_contact_button).setOnClickListener(new C14487());
        findViewById(C1426R.id.net_gogame_gowrap_offers_button).setOnClickListener(new C14498());
        findViewById(C1426R.id.net_gogame_gowrap_close_button).setOnClickListener(new C14509());
        showInitialFragment();
    }

    protected int getFragmentContainerViewId() {
        return C1426R.id.net_gogame_gowrap_main_fragment_container;
    }

    protected void onEnterFullscreen() {
        this.navbar.setVisibility(8);
    }

    protected void onExitFullscreen() {
        this.navbar.setVisibility(0);
    }

    protected void enableOffers() {
        Log.v(Constants.TAG, "Offers enabled");
        findViewById(C1426R.id.net_gogame_gowrap_offers_button).setVisibility(0);
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

    private void showLanguageMenu() {
        if (canShowLanguageMenu()) {
            String currentLocale = Wrapper.INSTANCE.getCurrentLocale(this);
            List supportedLocaleDescriptors = this.localeManager.getSupportedLocaleDescriptors();
            int i = 0;
            while (i < supportedLocaleDescriptors.size()) {
                if (StringUtils.isEquals(currentLocale, ((LocaleDescriptor) supportedLocaleDescriptors.get(i)).getId())) {
                    break;
                }
                i++;
            }
            i = -1;
            final Object arrayAdapter = new ArrayAdapter(this, C1426R.layout.net_gogame_gowrap_default_listview_item, C1426R.id.net_gogame_gowrap_text_view, this.localeManager.getSupportedLocaleDescriptors());
            View inflate = getLayoutInflater().inflate(C1426R.layout.net_gogame_gowrap_default_listview, null, false);
            ListView listView = (ListView) inflate.findViewById(C1426R.id.net_gogame_gowrap_list_view);
            listView.setAdapter(arrayAdapter);
            if (i >= 0) {
                listView.setItemChecked(i, true);
            }
            final Dialog dialog = new Dialog(this, C1426R.style.net_gogame_gowrap_dialog);
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
