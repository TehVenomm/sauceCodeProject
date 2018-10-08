package net.gogame.gowrap.sdk;

public class VipStatus {
    private boolean suspended;
    private String suspensionMessage;
    private boolean vip;

    public String getSuspensionMessage() {
        return this.suspensionMessage;
    }

    public boolean isSuspended() {
        return this.suspended;
    }

    public boolean isVip() {
        return this.vip;
    }

    public void setSuspended(boolean z) {
        this.suspended = z;
    }

    public void setSuspensionMessage(String str) {
        this.suspensionMessage = str;
    }

    public void setVip(boolean z) {
        this.vip = z;
    }
}
