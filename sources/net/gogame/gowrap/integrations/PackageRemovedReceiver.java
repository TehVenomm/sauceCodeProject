package net.gogame.gowrap.integrations;

public class PackageRemovedReceiver extends AbstractMultiBroadcastReceiver {
    public PackageRemovedReceiver() {
        super("PackageRemovedReceiver", "net.gogame.gowrap.packageRemovedReceiver.");
    }
}
