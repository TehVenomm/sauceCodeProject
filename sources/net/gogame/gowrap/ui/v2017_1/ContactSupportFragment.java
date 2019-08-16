package net.gogame.gowrap.p019ui.v2017_1;

import android.app.Fragment;
import android.content.Context;
import android.os.Bundle;
import android.view.LayoutInflater;
import android.view.View;
import android.view.View.OnClickListener;
import android.view.ViewGroup;
import net.gogame.gowrap.C1423R;
import net.gogame.gowrap.GoWrapImpl;
import net.gogame.gowrap.integrations.core.Wrapper;
import net.gogame.gowrap.p019ui.UIContext;
import net.gogame.gowrap.p019ui.VipListener;
import net.gogame.gowrap.p019ui.dialog.CustomDialog;
import net.gogame.gowrap.p019ui.dialog.CustomDialog.Type;

/* renamed from: net.gogame.gowrap.ui.v2017_1.ContactSupportFragment */
public class ContactSupportFragment extends Fragment implements VipListener {
    private SupportCustomImageButton chatButton;

    public View onCreateView(LayoutInflater layoutInflater, ViewGroup viewGroup, Bundle bundle) {
        final UIContext uIContext;
        View inflate = layoutInflater.inflate(C1423R.C1425layout.net_gogame_gowrap_fragment_contact_support, viewGroup, false);
        final Context context = viewGroup.getContext();
        if (context instanceof UIContext) {
            uIContext = (UIContext) context;
        } else {
            uIContext = null;
        }
        inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_form_button).setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (uIContext != null) {
                    uIContext.pushFragment(new SupportFormFragment());
                }
            }
        });
        this.chatButton = (SupportCustomImageButton) inflate.findViewById(C1423R.C1424id.net_gogame_gowrap_support_chat_button);
        updateChatButton(uIContext.isVipChatEnabled(), Wrapper.INSTANCE.isChatBotEnabled());
        this.chatButton.setOnClickListener(new OnClickListener() {
            public void onClick(View view) {
                if (uIContext.isVipChatEnabled() || Wrapper.INSTANCE.isChatBotEnabled()) {
                    GoWrapImpl.INSTANCE.startChat();
                } else {
                    CustomDialog.newBuilder(context).withType(Type.ALERT).withTitle(C1423R.string.net_gogame_gowrap_support_title).withMessage(C1423R.string.net_gogame_gowrap_support_chat_vip_only_message).build().show();
                }
            }
        });
        return inflate;
    }

    private void updateChatButton(boolean z, boolean z2) {
        this.chatButton.setMasked(!z && !z2);
    }

    public void onEnableVipChat() {
        updateChatButton(true, Wrapper.INSTANCE.isChatBotEnabled());
    }

    public void onDisableVipChat() {
        updateChatButton(false, Wrapper.INSTANCE.isChatBotEnabled());
    }
}
