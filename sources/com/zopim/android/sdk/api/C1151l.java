package com.zopim.android.sdk.api;

import com.zopim.android.sdk.api.ErrorResponse.Kind;

/* renamed from: com.zopim.android.sdk.api.l */
class C1151l implements ErrorResponse {

    /* renamed from: a */
    private Kind f684a;

    /* renamed from: b */
    private String f685b;

    /* renamed from: c */
    private int f686c;

    /* renamed from: d */
    private String f687d;

    /* renamed from: e */
    private String f688e;

    /* renamed from: f */
    private String f689f;

    /* renamed from: com.zopim.android.sdk.api.l$a */
    static class C1152a {
        /* access modifiers changed from: private */

        /* renamed from: a */
        public Kind f690a;
        /* access modifiers changed from: private */

        /* renamed from: b */
        public String f691b;
        /* access modifiers changed from: private */

        /* renamed from: c */
        public int f692c;
        /* access modifiers changed from: private */

        /* renamed from: d */
        public String f693d;
        /* access modifiers changed from: private */

        /* renamed from: e */
        public String f694e;
        /* access modifiers changed from: private */

        /* renamed from: f */
        public String f695f;

        C1152a() {
        }

        /* renamed from: a */
        public C1152a mo20656a(int i) {
            this.f692c = i;
            return this;
        }

        /* renamed from: a */
        public C1152a mo20657a(Kind kind) {
            this.f690a = kind;
            return this;
        }

        /* renamed from: a */
        public C1152a mo20658a(String str) {
            this.f691b = str;
            return this;
        }

        /* renamed from: a */
        public C1151l mo20659a() {
            return new C1151l(this);
        }

        /* renamed from: b */
        public C1152a mo20660b(String str) {
            this.f693d = str;
            return this;
        }

        /* renamed from: c */
        public C1152a mo20661c(String str) {
            this.f694e = str;
            return this;
        }
    }

    private C1151l() {
    }

    private C1151l(C1152a aVar) {
        this.f684a = aVar.f690a;
        this.f685b = aVar.f691b;
        this.f686c = aVar.f692c;
        this.f687d = aVar.f693d;
        this.f688e = aVar.f694e;
        this.f689f = aVar.f695f;
    }

    /* renamed from: a */
    public String mo20621a() {
        return null;
    }

    public String toString() {
        return "kind:" + this.f684a + " reason:" + this.f685b + " status:" + this.f686c + " response:" + this.f688e + " url:" + this.f687d;
    }
}
