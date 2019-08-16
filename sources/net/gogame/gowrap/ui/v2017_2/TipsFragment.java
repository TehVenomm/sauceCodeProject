package net.gogame.gowrap.p019ui.v2017_2;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.p019ui.utils.ExternalAppLauncher;

/* renamed from: net.gogame.gowrap.ui.v2017_2.TipsFragment */
public class TipsFragment extends Fragment {
    private static final String MORE_TIPS_URL = "https://dp-guide.gogame.net/";
    private static final String TIP_1_URL = "https://dp-guide.gogame.net/forging-logic/";
    private static final String TIP_2_URL = "https://dp-guide.gogame.net/5-tips-newbies/";
    private static final String TIP_3_URL = "https://dp-guide.gogame.net/beginners-guide/ ";

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        View inflate = layoutInflater.inflate(C1423R.C1425layout.net_gogame_gowrap_v2017_2_fragment_tips, viewGroup, false);
        inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_tip1_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                ExternalAppLauncher.openUrlInExternalBrowser(TipsFragment.this.getActivity(), TipsFragment.TIP_1_URL);
            }
        });
        inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_tip2_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                ExternalAppLauncher.openUrlInExternalBrowser(TipsFragment.this.getActivity(), TipsFragment.TIP_2_URL);
            }
        });
        inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_tip3_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                ExternalAppLauncher.openUrlInExternalBrowser(TipsFragment.this.getActivity(), TipsFragment.TIP_3_URL);
            }
        });
        inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_more_tips_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                ExternalAppLauncher.openUrlInExternalBrowser(TipsFragment.this.getActivity(), TipsFragment.MORE_TIPS_URL);
            }
        });
        return inflate;
    }
}
