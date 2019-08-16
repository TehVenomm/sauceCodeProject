package com.fasterxml.jackson.core.util;

import com.appsflyer.share.Constants;
import com.facebook.internal.ServerProtocol;
import com.fasterxml.jackson.core.Version;
import com.fasterxml.jackson.core.Versioned;
import java.io.Closeable;
import java.io.IOException;
import java.io.InputStream;
import java.util.Properties;
import java.util.regex.Pattern;

public class VersionUtil {
    private static final Pattern V_SEP = Pattern.compile("[-_./;:]");

    /* renamed from: _v */
    private final Version f424_v;

    protected VersionUtil() {
        Version version = null;
        try {
            version = versionFor(getClass());
        } catch (Exception e) {
            System.err.println("ERROR: Failed to load Version information from " + getClass());
        }
        if (version == null) {
            version = Version.unknownVersion();
        }
        this.f424_v = version;
    }

    public Version version() {
        return this.f424_v;
    }

    public static Version versionFor(Class<?> cls) {
        Version packageVersionFor = packageVersionFor(cls);
        return packageVersionFor == null ? Version.unknownVersion() : packageVersionFor;
    }

    public static Version packageVersionFor(Class<?> cls) {
        Version version;
        Class cls2;
        try {
            cls2 = Class.forName(cls.getPackage().getName() + ".PackageVersion", true, cls.getClassLoader());
            version = ((Versioned) cls2.newInstance()).version();
        } catch (Exception e) {
            throw new IllegalArgumentException("Failed to get Versioned out of " + cls2);
        } catch (Exception e2) {
            version = null;
        }
        return version == null ? Version.unknownVersion() : version;
    }

    @Deprecated
    public static Version mavenVersionFor(ClassLoader classLoader, String str, String str2) {
        InputStream resourceAsStream = classLoader.getResourceAsStream("META-INF/maven/" + str.replaceAll("\\.", Constants.URL_PATH_DELIMITER) + Constants.URL_PATH_DELIMITER + str2 + "/pom.properties");
        if (resourceAsStream != null) {
            try {
                Properties properties = new Properties();
                properties.load(resourceAsStream);
                return parseVersion(properties.getProperty(ServerProtocol.FALLBACK_DIALOG_PARAM_VERSION), properties.getProperty("groupId"), properties.getProperty("artifactId"));
            } catch (IOException e) {
            } finally {
                _close(resourceAsStream);
            }
        }
        return Version.unknownVersion();
    }

    public static Version parseVersion(String str, String str2, String str3) {
        int i = 0;
        if (str != null) {
            String trim = str.trim();
            if (trim.length() > 0) {
                String[] split = V_SEP.split(trim);
                int parseVersionPart = parseVersionPart(split[0]);
                int i2 = split.length > 1 ? parseVersionPart(split[1]) : 0;
                if (split.length > 2) {
                    i = parseVersionPart(split[2]);
                }
                return new Version(parseVersionPart, i2, i, split.length > 3 ? split[3] : null, str2, str3);
            }
        }
        return Version.unknownVersion();
    }

    protected static int parseVersionPart(String str) {
        int length = str.length();
        int i = 0;
        int i2 = 0;
        while (i < length) {
            char charAt = str.charAt(i);
            if (charAt > '9' || charAt < '0') {
                break;
            }
            i++;
            i2 = (charAt - '0') + (i2 * 10);
        }
        return i2;
    }

    private static final void _close(Closeable closeable) {
        try {
            closeable.close();
        } catch (IOException e) {
        }
    }

    public static final void throwInternal() {
        throw new RuntimeException("Internal error: this code path should never get executed");
    }
}
