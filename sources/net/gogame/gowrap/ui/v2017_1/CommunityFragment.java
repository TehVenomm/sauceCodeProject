package net.gogame.gowrap.p019ui.v2017_1;

import android.app.Fragment;
import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.model.configuration.Configuration.Integrations.Core.LocaleConfiguration;
import net.gogame.gowrap.p019ui.UIContext;
import net.gogame.gowrap.p019ui.utils.ExternalAppLauncher;
import net.gogame.gowrap.support.StringUtils;

/* renamed from: net.gogame.gowrap.ui.v2017_1.CommunityFragment */
public class CommunityFragment extends Fragment {
    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        View inflate = layoutInflater.inflate(C1423R.C1425layout.net_gogame_gowrap_fragment_community, viewGroup, false);
        LocaleConfiguration localeConfiguration = Wrapper.INSTANCE.getLocaleConfiguration((Context) getActivity());
        if (localeConfiguration != null) {
            setup(inflate, C1423R.C1424id.net_gogame_gowrap_community_header, localeConfiguration.getWhatsNewUrl(), false);
            setup(inflate, C1423R.C1424id.net_gogame_gowrap_facebook_button, localeConfiguration.getFacebookUrl());
            setup(inflate, C1423R.C1424id.net_gogame_gowrap_twitter_button, localeConfiguration.getTwitterUrl());
            setup(inflate, C1423R.C1424id.net_gogame_gowrap_instagram_button, localeConfiguration.getInstagramUrl());
            setup(inflate, C1423R.C1424id.net_gogame_gowrap_youtube_button, localeConfiguration.getYoutubeUrl());
            setup(inflate, C1423R.C1424id.net_gogame_gowrap_wiki_button, localeConfiguration.getWikiUrl());
            setup(inflate, C1423R.C1424id.net_gogame_gowrap_forum_button, localeConfiguration.getForumUrl());
        }
        return inflate;
    }

    private void setup(View view, int i, String str) {
        setup(view, i, str, true);
    }

    private void setup(View view, int i, String str, boolean z) {
        View findViewById = view.findViewById(i);
        if (findViewById != null) {
            final String trimToNull = StringUtils.trimToNull(str);
            if (trimToNull != null && (getActivity() instanceof UIContext)) {
                UIContext uIContext = (UIContext) getActivity();
                findViewById.setVisibility(0);
                findViewById.setOnClickListener(new OnClickListener() {
                    public void onClick(View view) {
                        ExternalAppLauncher.openUrlInExternalBrowser(CommunityFragment.this.getActivity(), trimToNull);
                    }
                });
            } else if (z) {
                findViewById.setVisibility(8);
            }
        }
    }
}
