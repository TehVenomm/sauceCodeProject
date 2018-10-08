package net.gogame.gowrap.integrations;

import java.util.Map;

public interface CanTrackEvent {
    void trackEvent(String str, String str2);

    void trackEvent(String str, String str2, long j);

    void trackEvent(String str, String str2, Map<String, Object> map);
}
