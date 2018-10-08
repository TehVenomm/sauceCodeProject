package jp.colopl.util;

import android.net.Uri;
import android.text.TextUtils;
import android.util.Log;
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStream;
import java.io.InputStreamReader;
import java.io.UnsupportedEncodingException;
import java.net.URI;
import java.net.URISyntaxException;
import java.util.List;
import org.apache.commons.lang3.StringUtils;
import org.apache.http.NameValuePair;
import org.apache.http.client.utils.URLEncodedUtils;

public class StringUtil {
    private static final String TAG = "StringUtil";

    public static String convertToString(InputStream inputStream) {
        IOException e;
        UnsupportedEncodingException e2;
        Throwable th;
        Object obj;
        String str = null;
        BufferedReader bufferedReader;
        try {
            try {
                bufferedReader = new BufferedReader(new InputStreamReader(inputStream, "UTF-8"));
                try {
                    StringBuilder stringBuilder = new StringBuilder();
                    while (true) {
                        String readLine = bufferedReader.readLine();
                        if (readLine == null) {
                            break;
                        }
                        stringBuilder.append(readLine).append(StringUtils.LF);
                    }
                    str = stringBuilder.toString();
                    try {
                        inputStream.close();
                    } catch (IOException e3) {
                    }
                    if (bufferedReader != null) {
                        try {
                            bufferedReader.close();
                        } catch (IOException e4) {
                            Log.e("StringUtil:", e4.toString());
                        }
                    }
                } catch (UnsupportedEncodingException e5) {
                    e2 = e5;
                    try {
                        Log.e("StringUtil Error:", e2.getMessage());
                        try {
                            inputStream.close();
                        } catch (IOException e6) {
                        }
                        if (bufferedReader != null) {
                            try {
                                bufferedReader.close();
                            } catch (IOException e42) {
                                Log.e("StringUtil:", e42.toString());
                            }
                        }
                        return str;
                    } catch (Throwable th2) {
                        th = th2;
                        try {
                            inputStream.close();
                        } catch (IOException e7) {
                        }
                        if (bufferedReader != null) {
                            try {
                                bufferedReader.close();
                            } catch (IOException e422) {
                                Log.e("StringUtil:", e422.toString());
                            }
                        }
                        throw th;
                    }
                } catch (IOException e8) {
                    e422 = e8;
                    Log.e("StringUtil Error:", e422.toString());
                    try {
                        inputStream.close();
                    } catch (IOException e9) {
                    }
                    if (bufferedReader != null) {
                        try {
                            bufferedReader.close();
                        } catch (IOException e4222) {
                            Log.e("StringUtil:", e4222.toString());
                        }
                    }
                    return str;
                } catch (Throwable th3) {
                    th = th3;
                    inputStream.close();
                    if (bufferedReader != null) {
                        bufferedReader.close();
                    }
                    throw th;
                }
            } catch (UnsupportedEncodingException e10) {
                e2 = e10;
                obj = str;
                Log.e("StringUtil Error:", e2.getMessage());
                inputStream.close();
                if (bufferedReader != null) {
                    bufferedReader.close();
                }
                return str;
            } catch (IOException e11) {
                e4222 = e11;
                obj = str;
                Log.e("StringUtil Error:", e4222.toString());
                inputStream.close();
                if (bufferedReader != null) {
                    bufferedReader.close();
                }
                return str;
            } catch (Throwable th4) {
                bufferedReader = str;
                th = th4;
                inputStream.close();
                if (bufferedReader != null) {
                    bufferedReader.close();
                }
                throw th;
            }
        } catch (UnsupportedEncodingException e12) {
            e2 = e12;
            obj = str;
            Log.e("StringUtil Error:", e2.getMessage());
            inputStream.close();
            if (bufferedReader != null) {
                bufferedReader.close();
            }
            return str;
        } catch (IOException e13) {
            e4222 = e13;
            bufferedReader = str;
            Log.e("StringUtil Error:", e4222.toString());
            inputStream.close();
            if (bufferedReader != null) {
                bufferedReader.close();
            }
            return str;
        } catch (Throwable th42) {
            obj = str;
            th = th42;
            inputStream.close();
            if (bufferedReader != null) {
                bufferedReader.close();
            }
            throw th;
        }
        return str;
    }

    public static String generateHashByJoiningColon(String str, String[] strArr) {
        String str2 = null;
        try {
            str2 = Crypto.getMD5withSalt(TextUtils.join(":", strArr) + ":", str);
        } catch (Exception e) {
            e.printStackTrace();
        }
        return str2;
    }

    public static String getQueryParamter(String str, String str2) {
        try {
            List<NameValuePair> parse = URLEncodedUtils.parse(new URI(str), "UTF-8");
            if (parse == null) {
                return null;
            }
            for (NameValuePair nameValuePair : parse) {
                String name = nameValuePair.getName();
                String value = nameValuePair.getValue();
                if (name.equals(str2)) {
                    return value;
                }
            }
            return null;
        } catch (URISyntaxException e) {
            Log.e(TAG, "failed to new URI object. url = " + str + ", Exception = " + e.toString());
            return null;
        }
    }

    public static boolean isMarketUrl(String str) {
        Uri parse = Uri.parse(str);
        String host = parse.getHost();
        String scheme = parse.getScheme();
        return scheme.equals("auonemkt") || scheme.equals("market") || host.endsWith("market.auone.jp") || host.endsWith("market.android.com");
    }

    public static boolean isSameHost(String str, String str2) {
        if (str == null || str2 == null) {
            return false;
        }
        Uri parse = Uri.parse(str);
        if (parse.getHost() == null) {
            return false;
        }
        Uri parse2 = Uri.parse(str2);
        return parse2.getHost() != null ? parse.getHost().equals(parse2.getHost()) : false;
    }
}
