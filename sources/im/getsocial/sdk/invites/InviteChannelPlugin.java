package im.getsocial.sdk.invites;

import android.content.Context;
import im.getsocial.sdk.internal.p033c.p041b.XdbacJlTDQ;
import im.getsocial.sdk.internal.p033c.p041b.ztWNWCuZiM;

public abstract class InviteChannelPlugin {
    @XdbacJlTDQ
    /* renamed from: a */
    Context f2232a;

    protected InviteChannelPlugin() {
        ztWNWCuZiM.m1221a((Object) this);
    }

    public Context getContext() {
        return this.f2232a;
    }

    public abstract boolean isAvailableForDevice(InviteChannel inviteChannel);

    public abstract void presentChannelInterface(InviteChannel inviteChannel, InvitePackage invitePackage, InviteCallback inviteCallback);
}
