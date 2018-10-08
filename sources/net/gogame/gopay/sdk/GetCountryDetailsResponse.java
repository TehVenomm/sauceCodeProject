package net.gogame.gopay.sdk;

import java.util.List;
import java.util.Map;

public class GetCountryDetailsResponse {
    /* renamed from: a */
    private final String f3342a;
    /* renamed from: b */
    private final List f3343b;
    /* renamed from: c */
    private final Map f3344c;

    public GetCountryDetailsResponse(String str, List list, Map map) {
        this.f3342a = str;
        this.f3343b = list;
        this.f3344c = map;
    }

    public Map getBaseUrls() {
        return this.f3344c;
    }

    public List getCountries() {
        return this.f3343b;
    }

    public String getCountry() {
        return this.f3342a;
    }
}
