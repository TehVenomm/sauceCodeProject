package im.getsocial.sdk.internal.unity;

import im.getsocial.sdk.invites.InviteCallback;
import im.getsocial.sdk.invites.InviteChannel;
import im.getsocial.sdk.invites.InviteChannelPlugin;
import im.getsocial.sdk.invites.InvitePackage;

public class InviteChannelPluginAdapter extends InviteChannelPlugin {
    /* renamed from: b */
    private final InviteChannelPluginInterface f2233b;

    public interface InviteChannelPluginInterface {
        boolean isAvailableForDevice(InviteChannel inviteChannel);

        void presentChannelInterface(InviteChannel inviteChannel, InvitePackage invitePackage, InviteCallback inviteCallback);
    }

    public InviteChannelPluginAdapter(InviteChannelPluginInterface inviteChannelPluginInterface) {
        this.f2233b = inviteChannelPluginInterface;
    }

    public boolean isAvailableForDevice(InviteChannel inviteChannel) {
        return this.f2233b.isAvailableForDevice(inviteChannel);
    }

    public void presentChannelInterface(InviteChannel inviteChannel, InvitePackage invitePackage, InviteCallback inviteCallback) {
        this.f2233b.presentChannelInterface(inviteChannel, invitePackage, inviteCallback);
    }
}
