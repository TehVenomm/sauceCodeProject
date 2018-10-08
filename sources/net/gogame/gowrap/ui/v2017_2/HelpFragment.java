package net.gogame.gowrap.ui.v2017_2;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import net.gogame.gowrap.C1110R;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.ui.UIContext;
import net.gogame.gowrap.ui.dialog.CustomDialog;
import net.gogame.gowrap.ui.dialog.CustomDialog.Type;
import net.gogame.gowrap.ui.v2017_1.SupportFormFragment;
import net.gogame.gowrap.ui.v2017_1.SupportFragment;

public class HelpFragment extends Fragment {
    private UIContext uiContext;

    /* renamed from: net.gogame.gowrap.ui.v2017_2.HelpFragment$1 */
    class C12241 implements OnClickListener {
        C12241() {
        }

        public void onClick(View view) {
            HelpFragment.this.uiContext.pushFragment(new SupportFragment());
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_2.HelpFragment$2 */
    class C12252 implements OnClickListener {
        C12252() {
        }

        public void onClick(View view) {
            HelpFragment.this.uiContext.pushFragment(new SupportFormFragment());
        }
    }

    /* renamed from: net.gogame.gowrap.ui.v2017_2.HelpFragment$3 */
    class C12263 implements OnClickListener {
        C12263() {
        }

        public void onClick(View view) {
            if (HelpFragment.this.uiContext.isVipChatEnabled() || Wrapper.INSTANCE.isChatBotEnabled()) {
                GoWrapImpl.INSTANCE.startChat();
            } else {
                CustomDialog.newBuilder(HelpFragment.this.getActivity()).withType(Type.ALERT).withTitle(C1110R.string.net_gogame_gowrap_support_title).withMessage(C1110R.string.net_gogame_gowrap_support_chat_vip_only_message).build().show();
            }
        }
    }

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        View inflate = layoutInflater.inflate(C1110R.layout.net_gogame_gowrap_v2017_2_fragment_help, viewGroup, false);
        if (getActivity() instanceof UIContext) {
            this.uiContext = (UIContext) getActivity();
        }
        inflate.findViewById(C1110R.id.net_gogame_gowrap_faq_button).setOnClickListener(new C12241());
        inflate.findViewById(C1110R.id.net_gogame_gowrap_support_form_button).setOnClickListener(new C12252());
        inflate.findViewById(C1110R.id.net_gogame_gowrap_support_chat_button).setOnClickListener(new C12263());
        return inflate;
    }
}
