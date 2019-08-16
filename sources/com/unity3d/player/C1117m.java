package com.unity3d.player;

import java.lang.reflect.Method;
import java.util.HashMap;

/* renamed from: com.unity3d.player.m */
final class C1117m {

    /* renamed from: a */
    private HashMap f608a = new HashMap();

    /* renamed from: b */
    private Class f609b = null;

    /* renamed from: c */
    private Object f610c = null;

    /* renamed from: com.unity3d.player.m$a */
    final class C1118a {

        /* renamed from: a */
        public Class[] f611a;

        /* renamed from: b */
        public Method f612b = null;

        public C1118a(Class[] clsArr) {
            this.f611a = clsArr;
        }
    }

    public C1117m(Class cls, Object obj) {
        this.f609b = cls;
        this.f610c = obj;
    }

    /* renamed from: a */
    private void m558a(String str, C1118a aVar) {
        try {
            aVar.f612b = this.f609b.getMethod(str, aVar.f611a);
        } catch (Exception e) {
            C1104e.Log(6, "Exception while trying to get method " + str + ". " + e.getLocalizedMessage());
            aVar.f612b = null;
        }
    }

    /* renamed from: a */
    public final Object mo20538a(String str, Object... objArr) {
        Object obj;
        if (!this.f608a.containsKey(str)) {
            C1104e.Log(6, "No definition for method " + str + " can be found");
            return null;
        }
        C1118a aVar = (C1118a) this.f608a.get(str);
        if (aVar.f612b == null) {
            m558a(str, aVar);
        }
        if (aVar.f612b == null) {
            C1104e.Log(6, "Unable to create method: " + str);
            return null;
        }
        try {
            obj = objArr.length == 0 ? aVar.f612b.invoke(this.f610c, new Object[0]) : aVar.f612b.invoke(this.f610c, objArr);
        } catch (Exception e) {
            C1104e.Log(6, "Error trying to call delegated method " + str + ". " + e.getLocalizedMessage());
            obj = null;
        }
        return obj;
    }

    /* renamed from: a */
    public final void mo20539a(String str, Class[] clsArr) {
        this.f608a.put(str, new C1118a(clsArr));
    }
}
