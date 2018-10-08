package net.gogame.gowrap.inbox;

public class MessageStateManagerException extends Exception {
    public MessageStateManagerException(String str) {
        super(str);
    }

    public MessageStateManagerException(String str, Throwable th) {
        super(str, th);
    }

    public MessageStateManagerException(Throwable th) {
        super(th);
    }
}
