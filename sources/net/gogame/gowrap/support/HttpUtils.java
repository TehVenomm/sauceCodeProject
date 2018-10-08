package net.gogame.gowrap.support;

import com.google.android.gms.nearby.messages.Strategy;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import net.gogame.gowrap.io.utils.IOUtils;

public final class HttpUtils {
    private HttpUtils() {
    }

    public static void drain(HttpURLConnection httpURLConnection) throws IOException {
        InputStream inputStream = null;
        try {
            inputStream = httpURLConnection.getInputStream();
            IOUtils.drain(inputStream);
        } finally {
            IOUtils.closeQuietly(inputStream);
        }
    }

    public static void drainQuietly(HttpURLConnection httpURLConnection) {
        try {
            drain(httpURLConnection);
        } catch (IOException e) {
        }
    }

    public static boolean isSuccessful(int i) {
        return i >= 200 && i < Strategy.TTL_SECONDS_DEFAULT;
    }
}
