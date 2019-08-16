package net.gogame.chat;

public class AgentTypingEntry extends ChatEntry {
    private boolean typing;

    public AgentTypingEntry() {
    }

    public AgentTypingEntry(boolean z) {
        this.typing = z;
    }

    public boolean isTyping() {
        return this.typing;
    }

    public void setTyping(boolean z) {
        this.typing = z;
    }
}
