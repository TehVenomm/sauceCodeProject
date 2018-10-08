package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.ErrorResponse.Kind;

/* renamed from: com.zopim.android.sdk.api.l */
class C0809l implements ErrorResponse {
    /* renamed from: a */
    private Kind f647a;
    /* renamed from: b */
    private String f648b;
    /* renamed from: c */
    private int f649c;
    /* renamed from: d */
    private String f650d;
    /* renamed from: e */
    private String f651e;
    /* renamed from: f */
    private String f652f;

    /* renamed from: com.zopim.android.sdk.api.l$a */
    static class C0808a {
        /* renamed from: a */
        private Kind f641a;
        /* renamed from: b */
        private String f642b;
        /* renamed from: c */
        private int f643c;
        /* renamed from: d */
        private String f644d;
        /* renamed from: e */
        private String f645e;
        /* renamed from: f */
        private String f646f;

        C0808a() {
        }

        /* renamed from: a */
        public C0808a m595a(int i) {
            this.f643c = i;
            return this;
        }

        /* renamed from: a */
        public C0808a m596a(Kind kind) {
            this.f641a = kind;
            return this;
        }

        /* renamed from: a */
        public C0808a m597a(String str) {
            this.f642b = str;
            return this;
        }

        /* renamed from: a */
        public C0809l m598a() {
            return new C0809l();
        }

        /* renamed from: b */
        public C0808a m599b(String str) {
            this.f644d = str;
            return this;
        }

        /* renamed from: c */
        public C0808a m600c(String str) {
            this.f645e = str;
            return this;
        }
    }

    private C0809l() {
    }

    private C0809l(C0808a c0808a) {
        this.f647a = c0808a.f641a;
        this.f648b = c0808a.f642b;
        this.f649c = c0808a.f643c;
        this.f650d = c0808a.f644d;
        this.f651e = c0808a.f645e;
        this.f652f = c0808a.f646f;
    }

    /* renamed from: a */
    public String mo4229a() {
        return null;
    }

    public String toString() {
        return "kind:" + this.f647a + " reason:" + this.f648b + " status:" + this.f649c + " response:" + this.f651e + " url:" + this.f650d;
    }
}
