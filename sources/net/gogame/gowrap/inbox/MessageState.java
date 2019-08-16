package net.gogame.gowrap.inbox;

import java.io.Serializable;

public class MessageState implements Serializable {

    /* renamed from: id */
    private long f1394id;
    private boolean read;
    private long timestamp;
    private String type;

    public String getType() {
        return this.type;
    }

    public void setType(String str) {
        this.type = str;
    }

    public long getId() {
        return this.f1394id;
    }

    public void setId(long j) {
        this.f1394id = j;
    }

    public long getTimestamp() {
        return this.timestamp;
    }

    public void setTimestamp(long j) {
        this.timestamp = j;
    }

    public boolean isRead() {
        return this.read;
    }

    public void setRead(boolean z) {
        this.read = z;
    }
}
