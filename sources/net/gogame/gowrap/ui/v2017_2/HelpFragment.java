package net.gogame.gowrap.p019ui.v2017_2;

import android.app.Fragment;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.p019ui.UIContext;
import net.gogame.gowrap.p019ui.dialog.CustomDialog;
import net.gogame.gowrap.p019ui.dialog.CustomDialog.Type;
import net.gogame.gowrap.p019ui.v2017_1.SupportFormFragment;
import net.gogame.gowrap.p019ui.v2017_1.SupportFragment;

/* renamed from: net.gogame.gowrap.ui.v2017_2.HelpFragment */
public class HelpFragment extends Fragment {
    /* access modifiers changed from: private */
    public UIContext uiContext;

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        View inflate = layoutInflater.inflate(C1423R.C1425layout.net_gogame_gowrap_v2017_2_fragment_help, viewGroup, false);
        if (getActivity() instanceof UIContext) {
            this.uiContext = (UIContext) getActivity();
        }
        inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_faq_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                HelpFragment.this.uiContext.pushFragment(new SupportFragment());
            }
        });
        inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                HelpFragment.this.uiContext.pushFragment(new SupportFormFragment());
            }
        });
        inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_chat_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (HelpFragment.this.uiContext.isVipChatEnabled() || Wrapper.INSTANCE.isChatBotEnabled()) {
                    GoWrapImpl.INSTANCE.startChat();
                } else {
                    CustomDialog.newBuilder(HelpFragment.this.getActivity()).withType(Type.ALERT).withTitle(C1423R.string.net_gogame_gowrap_support_title).withMessage(C1423R.string.net_gogame_gowrap_support_chat_vip_only_message).build().show();
                }
            }
        });
        return inflate;
    }
}
