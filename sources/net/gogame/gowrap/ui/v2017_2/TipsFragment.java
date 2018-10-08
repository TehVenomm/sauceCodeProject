package net.gogame.gowrap.ui.v2017_2;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import net.gogame.gowrap.C1426R;
import net.gogame.gowrap.ui.utils.ExternalAppLauncher;

public class TipsFragment extends Fragment {
    private static final String MORE_TIPS_URL = "https://dp-guide.gogame.net/";
    private static final String TIP_1_URL = "https://dp-guide.gogame.net/forging-logic/";
    private static final String TIP_2_URL = "https://dp-guide.gogame.net/5-tips-newbies/";
    private static final String TIP_3_URL = "https://dp-guide.gogame.net/beginners-guide/ ";

    /* renamed from: net.gogame.gowrap.ui.v2017_2.TipsFragment$1 */
    class C15591 implements OnClickListener {
        C15591() {
        }

        public void onClick(View view) {
            ExternalAppLauncher.openUrlInExternalBrowser(TipsFragment.this.getActivity(), TipsFragment.TIP_1_URL);
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_2.TipsFragment$2 */
    class C15602 implements OnClickListener {
        C15602() {
        }

        public void onClick(View view) {
            ExternalAppLauncher.openUrlInExternalBrowser(TipsFragment.this.getActivity(), TipsFragment.TIP_2_URL);
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_2.TipsFragment$3 */
    class C15613 implements OnClickListener {
        C15613() {
        }

        public void onClick(View view) {
            ExternalAppLauncher.openUrlInExternalBrowser(TipsFragment.this.getActivity(), TipsFragment.TIP_3_URL);
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_2.TipsFragment$4 */
    class C15624 implements OnClickListener {
        C15624() {
        }

        public void onClick(View view) {
            ExternalAppLauncher.openUrlInExternalBrowser(TipsFragment.this.getActivity(), TipsFragment.MORE_TIPS_URL);
        }
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        View inflate = layoutInflater.inflate(C1426R.layout.net_gogame_gowrap_v2017_2_fragment_tips, viewGroup, false);
        inflate.findViewById(C1426R.id.net_gogame_gowrap_tip1_button).setOnClickListener(new C15591());
        inflate.findViewById(C1426R.id.net_gogame_gowrap_tip2_button).setOnClickListener(new C15602());
        inflate.findViewById(C1426R.id.net_gogame_gowrap_tip3_button).setOnClickListener(new C15613());
        inflate.findViewById(C1426R.id.net_gogame_gowrap_more_tips_button).setOnClickListener(new C15624());
        return inflate;
    }
}
