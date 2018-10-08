package net.gogame.gowrap.inbox;

import java.util.List;

public interface MessageStateManager {
    void deleteMessageStates(long j);

    void deleteMessageStates(String str, long j);

    List<MessageState> getMessageStates(String str, long j);

    void setMessageState(String str, long j, long j2, boolean z);
}
