package net.gogame.gowrap;

public class VipStatus {
    private boolean suspended;
    private String suspensionMessage;
    private boolean vip;

    public boolean isVip() {
        return this.vip;
    }

    public void setVip(boolean z) {
        this.vip = z;
    }

    public boolean isSuspended() {
        return this.suspended;
    }

    public void setSuspended(boolean z) {
        this.suspended = z;
    }

    public String getSuspensionMessage() {
        return this.suspensionMessage;
    }

    public void setSuspensionMessage(String str) {
        this.suspensionMessage = str;
    }
}
