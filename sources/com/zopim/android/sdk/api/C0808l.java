package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.ErrorResponse.Kind;

/* renamed from: com.zopim.android.sdk.api.l */
class C0808l implements ErrorResponse {
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
    static class C0807a {
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

        C0807a() {
        }

        /* renamed from: a */
        public C0807a m595a(int i) {
            this.f643c = i;
            return this;
        }

        /* renamed from: a */
        public C0807a m596a(Kind kind) {
            this.f641a = kind;
            return this;
        }

        /* renamed from: a */
        public C0807a m597a(String str) {
            this.f642b = str;
            return this;
        }

        /* renamed from: a */
        public C0808l m598a() {
            return new C0808l();
        }

        /* renamed from: b */
        public C0807a m599b(String str) {
            this.f644d = str;
            return this;
        }

        /* renamed from: c */
        public C0807a m600c(String str) {
            this.f645e = str;
            return this;
        }
    }

    private C0808l() {
    }

    private C0808l(C0807a c0807a) {
        this.f647a = c0807a.f641a;
        this.f648b = c0807a.f642b;
        this.f649c = c0807a.f643c;
        this.f650d = c0807a.f644d;
        this.f651e = c0807a.f645e;
        this.f652f = c0807a.f646f;
    }

    /* renamed from: a */
    public String mo4231a() {
        return null;
    }

    public String toString() {
        return "kind:" + this.f647a + " reason:" + this.f648b + " status:" + this.f649c + " response:" + this.f651e + " url:" + this.f650d;
    }
}
