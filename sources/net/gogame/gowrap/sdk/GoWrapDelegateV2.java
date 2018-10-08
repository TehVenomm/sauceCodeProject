package net.gogame.gowrap.sdk;

public interface GoWrapDelegateV2 extends GoWrapDelegate {
    void onCustomUrl(String str);

    void onMenuClosed();

    void onMenuOpened();

    void onOffersAvailable();
}
